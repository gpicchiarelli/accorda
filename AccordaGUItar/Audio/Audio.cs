using System;
using System.Collections.Generic;
using NAudio.Dsp;
using NAudio.Wave;

namespace Accorda.Audio
{
    public class Audio
    {
        private readonly WaveInEvent waveIn;
        private const int sampleRate = 44100;
        private const int bufferSize = 1024;
        private readonly float[] buffer;
        private readonly Complex[] complexBuffer;
        private readonly BiQuadFilter filter;

        // Aggiunto un evento per rilevare la frequenza istantanea
        public event EventHandler<double> DominantFrequencyDetected;

        // Aggiunto un threshold per il volume minimo rilevabile
        private double volumeThreshold = 0.1;

        public Audio(int InputDeviceSelector = 0)
        {
            waveIn = new WaveInEvent
            {
                DeviceNumber = InputDeviceSelector,
                BufferMilliseconds = bufferSize * 1000 / sampleRate,
                WaveFormat = new WaveFormat(sampleRate, 1)
            };

            buffer = new float[bufferSize];
            complexBuffer = new Complex[bufferSize];
            filter = BiQuadFilter.LowPassFilter(sampleRate, 1000, (float)0.7071);

            waveIn.DataAvailable += WaveIn_DataAvailable;
            StartRecording();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            double maxVolume = 0.0;

            for (int i = 0; i < e.BytesRecorded / 2; i++)
            {
                short sample = (short)((e.Buffer[(2 * i) + 1] << 8) | e.Buffer[2 * i]);
                buffer[i] = (float)sample / short.MaxValue;
                buffer[i] = filter.Transform(buffer[i]);
                complexBuffer[i].X = buffer[i];
                complexBuffer[i].Y = 0;

                double volume = Math.Abs(buffer[i]);
                if (volume > maxVolume)
                {
                    maxVolume = volume;
                }
            }

            if (maxVolume > volumeThreshold)
            {
                FastFourierTransform.FFT(true, (int)Math.Log(bufferSize, 2.0), complexBuffer);

                int maxIndex = 0;
                double maxMagnitude = 0;

                for (int i = 0; i < bufferSize / 2; i++)
                {
                    double magnitude = CalculateMagnitude(complexBuffer[i]);
                    if (magnitude > maxMagnitude)
                    {
                        maxMagnitude = magnitude;
                        maxIndex = i;
                    }
                }
                double frequency = maxIndex * sampleRate / bufferSize;
                DominantFrequencyDetected?.Invoke(this, frequency);
            }
        }

        private double CalculateMagnitude(Complex complex)
        {
            return Math.Sqrt((complex.X * complex.X) + (complex.Y * complex.Y));
        }

        public List<string> ElencaDispositiviIngresso()
        {
            int inputDeviceCount = WaveInEvent.DeviceCount;
            List<string> dispositivi = new();
            for (int deviceIndex = 0; deviceIndex < inputDeviceCount; deviceIndex++)
            {
                WaveInCapabilities deviceInfo = WaveInEvent.GetCapabilities(deviceIndex);
                dispositivi.Add($"Dispositivo {deviceIndex + 1}: {deviceInfo.ProductName}");
            }
            return dispositivi;
        }

        private void StartRecording()
        {
            waveIn.StartRecording();
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
        }

        // Aggiunto un metodo per impostare il threshold di volume
        public void SetVolumeThreshold(double threshold)
        {
            volumeThreshold = threshold;
        }
    }
}