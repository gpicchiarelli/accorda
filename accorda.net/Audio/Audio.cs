using NAudio.Dsp;
using NAudio.Wave;

namespace Accorda.Audio
{
    public class Audio
    {
        private readonly WaveInEvent waveIn;
        private const int sampleRate = 44100;
        private const int bufferSize = 512;
        private readonly float[] buffer;
        private readonly Complex[] complexBuffer;
        private readonly BiQuadFilter filter;
        private readonly double stabilityThreshold = 1; 
        private readonly List<double> frequencyHistory;
        private readonly int stableWindowSamples = sampleRate;
        private double magnitudeThreshold = 0.004; 


        public BufferedWaveProvider BufferedWave { get; }

        public event EventHandler<double> DominantFrequencyDetected;

        public Audio(int InputDeviceSelector = 0)
        {
            waveIn = new WaveInEvent
            {
                DeviceNumber = InputDeviceSelector,
                BufferMilliseconds = bufferSize * 1000 / sampleRate,
                WaveFormat = new WaveFormat(sampleRate, 1) // 44100 Hz sample rate, 1 channel (mono)
            };

            buffer = new float[bufferSize];
            complexBuffer = new Complex[bufferSize];

            // Configura il filtro passa-basso
            filter = BiQuadFilter.LowPassFilter(sampleRate, 1000, (float)0.7071);
            frequencyHistory = new();

            BufferedWave = new BufferedWaveProvider(waveIn.WaveFormat)
            {
                BufferLength = 4096,
                DiscardOnBufferOverflow = true
            };

            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.DataAvailable += DatiGrafico;
            StartRecording();
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

        private void DatiGrafico(object? sender, WaveInEventArgs e)
        {
            BufferedWave.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void StartRecording()
        {
            waveIn.StartRecording();
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
        }

        private double CalculateMagnitude(Complex complex)
        {
            return Math.Sqrt((complex.X * complex.X) + (complex.Y * complex.Y));
        }

        private double GetAverageFrequency()
        {
            if (frequencyHistory.Count == 0)
            {
                return 0;
            }

            double sum = 0;
            foreach (double frequency in frequencyHistory)
            {
                sum += frequency;
            }

            return sum / frequencyHistory.Count;
        }

        private bool IsStable()
        {
            double sumOfSquares = 0;
            double average = GetAverageFrequency();

            foreach (double frequency in frequencyHistory)
            {
                sumOfSquares += Math.Pow(frequency - average, 2);
            }

            double standardDeviation = Math.Sqrt(sumOfSquares / frequencyHistory.Count);
            return standardDeviation < stabilityThreshold;
        }


        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded / 2; i++)
            {
                short sample = (short)((e.Buffer[(2 * i) + 1] << 8) | e.Buffer[2 * i]);
                buffer[i] = (float)sample / short.MaxValue;
                buffer[i] = filter.Transform(buffer[i]);
                complexBuffer[i].X = buffer[i];
                complexBuffer[i].Y = 0;
            }

            FastFourierTransform.FFT(true, (int)Math.Log(bufferSize, 2.0), complexBuffer);

            int maxIndex = 0;
            double maxMagnitude = 0;

            for (int i = 0; i < bufferSize / 2; i++)
            {
                double magnitude = CalculateMagnitude(complexBuffer[i]);
                if (magnitude > maxMagnitude && magnitude > magnitudeThreshold)
                {
                    maxMagnitude = magnitude;
                    maxIndex = i;
                }
            }
            double frequency = maxIndex * sampleRate / bufferSize;
            frequency = double.Round(frequency, 2);
            if (frequencyHistory.Count == 0)
            {
                frequencyHistory.Add(frequency);
            }
            else 
            {
                if (IsStable())
                {
                    DominantFrequencyDetected?.Invoke(this, frequency);
                }
                else 
                {
                    frequencyHistory.Clear();
                }
            }            
        }
    }
}
