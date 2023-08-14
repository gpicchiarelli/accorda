using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Dsp;

namespace accorda.Audio
{
    public class Audio
    {
        private WaveInEvent waveIn;
        private Complex[] fftBuffer;
        private int fftLength = 1024;

        public event EventHandler<double> DominantFrequencyDetected;

        public Audio()
        {
            fftBuffer = new Complex[fftLength];
            waveIn = new WaveInEvent();
            waveIn.BufferMilliseconds = 500;
            waveIn.WaveFormat = new WaveFormat(44100, 1); // 44100 Hz sample rate, 1 channel (mono)
            waveIn.DataAvailable += WaveIn_DataAvailable;
            StartRecording();
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

            DominantFrequencyDetected?.Invoke(this, dominantFrequency);
        }
    }
}
