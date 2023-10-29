using System;
using System.Windows;
using NAudio.Wave;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Accorda
{
    public partial class AccordaGUI : Window
    {
        private Audio.Audio audioRecorder;

        public AccordaGUI()
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
        private void LeggiLicenza_Click(object sender, RoutedEventArgs e)
        {
            // Codice per visualizzare la licenza
        }

        private void Chiudi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelezionaCorda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelezionaCorda.SelectedIndex == 0)
            {
                SelezionaCorda.SelectedIndex = -1; // Imposta la selezione su -1 per disabilitare la selezione della riga vuota
            }
        }

        private void AccordaturaProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Valore minimo per la conferma dell'accordatura
            const double ValoreMinimoAccordatura = 95.0;

            double valoreAttuale = AccordaturaProgressBar.Value;
            if (valoreAttuale >= ValoreMinimoAccordatura)
            {
                AccordaturaProgressBar.Foreground = new SolidColorBrush(Colors.Green); // Colore verde per l'accordatura corretta
            }
            else
            {
                AccordaturaProgressBar.Foreground = new SolidColorBrush(Colors.Red); // Colore rosso per l'accordatura errata
            }
        }

    }
}