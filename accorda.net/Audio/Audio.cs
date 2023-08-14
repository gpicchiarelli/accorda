using NAudio.Wave;
using NAudio.Dsp;

namespace Accorda.Audio
{
    public class Audio
    {
        private WaveInEvent waveIn;
        private BufferedWaveProvider bufferedWaveProvider;
        private const int sampleRate = 44100;
        private const int bufferSize = 1024;
        private float[] buffer;
        private Complex[] complexBuffer;
        private BiQuadFilter filter;

        public BufferedWaveProvider BufferedWave => bufferedWaveProvider;

        public event EventHandler<double> DominantFrequencyDetected;

        public Audio(int InputDeviceSelector = 0)
        {
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = InputDeviceSelector;
            waveIn.BufferMilliseconds = bufferSize * 1000 / sampleRate;
            waveIn.WaveFormat = new WaveFormat(sampleRate, 1); // 44100 Hz sample rate, 1 channel (mono)

            buffer = new float[bufferSize];
            complexBuffer = new Complex[bufferSize];

            // Configura il filtro passa-basso
            filter = BiQuadFilter.LowPassFilter(sampleRate, 1000, (float)0.7071);


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
        private double CalculateMagnitude(Complex complex)
        {
            return Math.Sqrt(complex.X * complex.X + complex.Y * complex.Y);
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded / 2; i++)
            {
                short sample = (short)((e.Buffer[2 * i + 1] << 8) | e.Buffer[2 * i]);
                buffer[i] = (float)sample / short.MaxValue;
                buffer[i] = (float)filter.Transform(buffer[i]);
                complexBuffer[i].X = buffer[i];
                complexBuffer[i].Y = 0;
            }

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
            DominantFrequencyDetected?.Invoke(this, double.Round(frequency, 2));
        }
    }
}
