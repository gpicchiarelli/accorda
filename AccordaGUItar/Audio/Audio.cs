using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using NAudio.Dsp;
using NAudio.Wave;

namespace AccordaGUItar.Audio
{
    public class Audio
    {
        private readonly WaveInEvent waveIn;
        private const int sampleRate = 44100;
        private const int bufferSize = 65536;
        private readonly double[] buffer;
        private readonly Complex[] complexBuffer;

        // Evento per notificare la frequenza media calcolata
        public event EventHandler<double> SmoothedFrequencyDetected;

        // Aggiunto un threshold per il volume minimo rilevabile
        private double volumeThreshold = 0.01;

        public Audio(int InputDeviceSelector = 0)
        {
            waveIn = new WaveInEvent
            {
                DeviceNumber = InputDeviceSelector,
                BufferMilliseconds = bufferSize * 1000 / sampleRate,
                WaveFormat = new WaveFormat(sampleRate, 1)
            };

            buffer = new double[bufferSize];
            complexBuffer = new Complex[bufferSize];
            waveIn.DataAvailable += WaveIn_DataAvailable;
            StartRecording();
        }

        private double CalculateFrequencyFromFFT(double[] buffer)
        {
            Complex32[] fftBuffer = new Complex32[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                fftBuffer[i] = new Complex32((float)buffer[i], 0);
            }

            Fourier.Forward(fftBuffer, FourierOptions.NoScaling);

            int indexOfMaxValue = fftBuffer.Select((value, index) => new { Value = value.Magnitude, Index = index })
                                            .OrderByDescending(x => x.Value)
                                            .First().Index;

            double fundamentalFrequency = indexOfMaxValue * sampleRate / buffer.Length;

            // Trova la prima armonica            
            return fundamentalFrequency;
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded / 2; i++)
            {
                short sample = (short)((e.Buffer[(2 * i) + 1] << 8) | e.Buffer[2 * i]);
                buffer[i] = (double)sample / short.MaxValue;
            }
            double maxVolume = buffer.Max(Math.Abs);
            if (maxVolume > volumeThreshold)
            {
                double frequency = CalculateFrequencyFromFFT(buffer);
                float cutoffFrequency = (float)(frequency * 0.1); // filtraggio

                BiQuadFilter filter = BiQuadFilter.LowPassFilter(sampleRate, cutoffFrequency, 1.0f);
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = filter.Transform((float)buffer[i]);
                }
                frequency = CalculateFrequencyFromFFT(buffer);
                SmoothedFrequencyDetected?.Invoke(this, frequency);
            }
        }

        public List<string> ElencaDispositiviIngresso()
        {
            int inputDeviceCount = WaveInEvent.DeviceCount;
            List<string> dispositivi = [];
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
