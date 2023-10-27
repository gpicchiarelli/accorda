using System;
using System.Windows;
using NAudio.Wave;
using System.Collections.Generic;

namespace Accorda
{
    public partial class MainWindow : Window
    {
        private Audio.Audio audioRecorder;

        public MainWindow()
        {
            InitializeComponent();

            audioRecorder = new Audio.Audio();
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;

            InizializzaDispositiviIngresso();
        }

        private void InizializzaDispositiviIngresso()
        {
            List<string> dispositiviIngresso = audioRecorder.ElencaDispositiviIngresso();
            InputDevices.ItemsSource = dispositiviIngresso;
            InputDevices.SelectedIndex = 0;
        }

        private void InputDevices_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Aggiorna il dispositivo in ingresso selezionato
            int indiceDispositivoSelezionato = InputDevices.SelectedIndex;
            audioRecorder = new Audio.Audio(indiceDispositivoSelezionato);
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;
        }

        private void AudioRecorder_DominantFrequencyDetected(object sender, double frequenzaDominante)
        {
            Dispatcher.Invoke(() =>
            {
                FrequenzaAttuale.Text = frequenzaDominante.ToString("F2");
            });
        }
    }
}