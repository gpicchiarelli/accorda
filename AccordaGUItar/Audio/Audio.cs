using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Statistics;
using NAudio.Dsp;
using NAudio.Wave;


namespace AccordaGUItar.Audio
{
    public class Audio
    {
        private readonly WaveInEvent waveIn;
        private const int sampleRate = 44100;
        private const int bufferSize = 2048;
        private readonly float[] buffer;
        private readonly Complex[] complexBuffer;
        private readonly BiQuadFilter filter;

        // Evento per notificare la frequenza media calcolata
        public event EventHandler<double> SmoothedFrequencyDetected;

        // Aggiunto un threshold per il volume minimo rilevabile
        private double volumeThreshold = 0.005;

        public Audio(int InputDeviceSelector = 0)
        {
            waveIn = new WaveInEvent
            {
                DeviceNumber = InputDeviceSelector,
                BufferMilliseconds = bufferSize * 1000 * 2 / sampleRate,
                WaveFormat = new WaveFormat(sampleRate, 1)
            };

            filter = BiQuadFilter.LowPassFilter(44100, 20000, 1);

            buffer = new float[bufferSize];
            complexBuffer = new Complex[bufferSize];
            waveIn.DataAvailable += WaveIn_DataAvailable;
            StartRecording();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (e.Buffer.Length >= 2 * bufferSize)
            {
                double maxVolume = CalculateMaxVolume(e);
                if (maxVolume > volumeThreshold)
                {
                    double[] autocorrelation = CalculateAutocorrelation(e.Buffer);
                    double[] autocorrelationAbsolutes = new double[autocorrelation.Length];
                    for(int u = 0; u < autocorrelation.Length; u++) {
                        autocorrelationAbsolutes[u] = Math.Abs(autocorrelation[u]);
                    }
                    double averageCorrelationValue = autocorrelationAbsolutes.Average();
                    if (averageCorrelationValue > correlationThreshold)
                    {
                        byte[] audioBuffer = e.Buffer; // Esempio di buffer audio
                        Complex32[] complexBuffer = new Complex32[bufferSize];

                        // Converti i dati audio in formato complesso
                        for (int i = 0; i < bufferSize; i++)
                        {
                            short sample = (short)((audioBuffer[(2 * i) + 1] << 8) | audioBuffer[2 * i]);
                            complexBuffer[i] = new Complex32(sample / (float)short.MaxValue, 0);
                        }

                        // Esegui la FFT
                        Fourier.Forward(complexBuffer, FourierOptions.Default);

                        // Calcola lo spettro delle frequenze
                        double[] spectrum = new double[bufferSize / 2];
                        for (int i = 0; i < bufferSize / 2; i++)
                        {
                            spectrum[i] = complexBuffer[i].MagnitudeSquared();
                        }

                        // Trova la frequenza dominante nello spettro
                        int dominantIndex = spectrum.ToList().IndexOf(spectrum.Max());
                        double dominantFrequency = (double)dominantIndex * sampleRate / bufferSize;

                        // Calcola la frequenza media delle altre frequenze rispetto alla dominante
                        double sum = 0.0;
                        int count = 0;

                        for (int i = 0; i < spectrum.Length; i++)
                        {
                            if (i != dominantIndex)
                            {
                                sum += (double)i * sampleRate / bufferSize;
                                count++;
                            }
                        }
                        double averageFrequency = sum / count;
                        SmoothedFrequencyDetected?.Invoke(this, averageFrequency);
                    }
                }
            }
        }

        private const int correlationWindowSize = bufferSize; // Dimensione della finestra per l'autocorrelazione
        private const double correlationThreshold = 0.80; // Soglia di autocorrelazione

        public double[] CalculateAutocorrelation(byte[] buffer)
        {
            double[] samples = new double[correlationWindowSize];

            // Copia i dati audio nel buffer in un array di double normalizzato
            for (int i = 0; i < correlationWindowSize / 2; i++)
            {
                short sample = (short)((buffer[(2 * i) + 1] << 8) | buffer[2 * i]);
                samples[i] = (double)sample / short.MaxValue;
            }
            // Calcola la funzione di autocorrelazione usando MathNet.Numerics
            double[] autocorrelation = Correlation.Auto(samples);
            return autocorrelation;
        }

        private double CalculateMaxVolume(WaveInEventArgs e)
        {
            double maxVolume = 0.0;

            for (int i = 0; i < e.BytesRecorded / 2; i++)
            {
                short sample = (short)((e.Buffer[(2 * i) + 1] << 8) | e.Buffer[2 * i]);
                double normalizedSample = (double)sample / short.MaxValue;
                double volume = Math.Abs(normalizedSample);

                if (volume > maxVolume)
                {
                    maxVolume = volume;
                }
            }

            return maxVolume;
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