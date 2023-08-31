using NAudio.Dsp;
using NAudio.Wave;

namespace Accorda.Audio
{
    /// <summary>
    /// 
    /// </summary>
    public class Audio
    {
        /// <summary>
        /// The wave in
        /// </summary>
        private readonly WaveInEvent waveIn;
        /// <summary>
        /// The sample rate
        /// </summary>
        private const int sampleRate = 44100;
        /// <summary>
        /// The buffer size
        /// </summary>
        private const int bufferSize = 1024;
        /// <summary>
        /// The buffer
        /// </summary>
        private readonly float[] buffer;
        /// <summary>
        /// The complex buffer
        /// </summary>
        private readonly Complex[] complexBuffer;
        /// <summary>
        /// The filter
        /// </summary>
        private readonly BiQuadFilter filter;
        /// <summary>
        /// The stability threshold
        /// </summary>
        private readonly double stabilityThreshold = 3;
        /// <summary>
        /// The frequency history
        /// </summary>
        private readonly List<double> frequencyHistory;
        /// <summary>
        /// The stable window samples
        /// </summary>
        private readonly int stableWindowSamples = sampleRate;
        /// <summary>
        /// The magnitude threshold
        /// </summary>
        private double magnitudeThreshold = 0.001;


        /// <summary>
        /// Gets the buffered wave.
        /// </summary>
        /// <value>
        /// The buffered wave.
        /// </value>
        public BufferedWaveProvider BufferedWave { get; }

        /// <summary>
        /// Occurs when [dominant frequency detected].
        /// </summary>
        public event EventHandler<double> DominantFrequencyDetected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        /// <param name="InputDeviceSelector">The input device selector.</param>
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
            waveIn.DataAvailable += WaveIn_DataAvailable;
            StartRecording();
        }

        /// <summary>
        /// Elencas the dispositivi ingresso.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Datis the grafico.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WaveInEventArgs"/> instance containing the event data.</param>
        private void DatiGrafico(object? sender, WaveInEventArgs e)
        {
            BufferedWave.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// Starts the recording.
        /// </summary>
        private void StartRecording()
        {
            waveIn.StartRecording();
        }

        /// <summary>
        /// Stops the recording.
        /// </summary>
        public void StopRecording()
        {
            waveIn.StopRecording();
        }

        /// <summary>
        /// Calculates the magnitude.
        /// </summary>
        /// <param name="complex">The complex.</param>
        /// <returns></returns>
        private double CalculateMagnitude(Complex complex)
        {
            return Math.Sqrt((complex.X * complex.X) + (complex.Y * complex.Y));
        }

        /// <summary>
        /// Gets the average frequency.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether this instance is stable.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is stable; otherwise, <c>false</c>.
        /// </returns>
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


        /// <summary>
        /// Handles the DataAvailable event of the WaveIn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WaveInEventArgs"/> instance containing the event data.</param>
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
