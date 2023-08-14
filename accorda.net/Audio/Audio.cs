using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Dsp;
using NAudio.Wave.SampleProviders;


namespace accorda.Audio
{
    public class Audio
    {
        private WaveInEvent waveIn;
        private Complex[] fftBuffer;
        private int fftLength = 1024;
        private BufferedWaveProvider bufferedWaveProvider;

        public BufferedWaveProvider BufferedWave => bufferedWaveProvider;

        public event EventHandler<double> DominantFrequencyDetected;

        public Audio(int InputDeviceSelector = 0)
        {
            fftBuffer = new Complex[fftLength];
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = InputDeviceSelector;
            waveIn.BufferMilliseconds = 500;
            waveIn.WaveFormat = new WaveFormat(44100, 1); // 44100 Hz sample rate, 1 channel (mono)

            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);
            bufferedWaveProvider.BufferLength = 4096;
            bufferedWaveProvider.DiscardOnBufferOverflow = true;

            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.DataAvailable += DatiGrafico;
            StartRecording();
        }

        public List<string> ElencaDispositiviIngresso() 
        {
            int inputDeviceCount = WaveInEvent.DeviceCount;
            var dispositivi = new List<string>();
            for (int deviceIndex = 0; deviceIndex < inputDeviceCount; deviceIndex++)
            {
                WaveInCapabilities deviceInfo = WaveInEvent.GetCapabilities(deviceIndex);
                dispositivi.Add($"Dispositivo {deviceIndex + 1}: {deviceInfo.ProductName}");
            }
            return dispositivi;
        }

        private void DatiGrafico(object? sender, WaveInEventArgs e)
        {
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void StartRecording() => waveIn.StartRecording();
        public void StopRecording() => waveIn.StopRecording();

        public async Task StopRecordingAsync()
        {
            waveIn.StopRecording();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < fftBuffer.Length; i++)
            {
                int index = i * 2;
                short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index]);

                fftBuffer[i].X = (float)(sample / 32768.0);
                fftBuffer[i].Y = 0;
            }

            FastFourierTransform.FFT(true, (int)Math.Log(fftLength, 2.0), fftBuffer);

            double[] magnitudes = fftBuffer.Select(c => Math.Sqrt(c.X * c.X + c.Y * c.Y)).ToArray();

            // Trova la frequenza dell'armonica dominante
            int dominantIndex = Array.IndexOf(magnitudes, magnitudes.Max());
            double sampleRate = waveIn.WaveFormat.SampleRate;
            double dominantFrequency = dominantIndex * sampleRate / fftLength;

            DominantFrequencyDetected?.Invoke(this, double.Round(dominantFrequency,2));
        }
    }
}
