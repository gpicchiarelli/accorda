using System;
using System.Windows;
using NAudio.Wave;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using accorda.Note;

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
            else
            {
                // Calcola la soglia in base alla corda selezionata
                ComboBoxItem cordaSelezionata = (ComboBoxItem)SelezionaCorda.SelectedItem;
                string cordaInfo = cordaSelezionata.Content.ToString();
                double soglia = CalcolaSogliaAccordatura(cordaInfo);

                // Avvia l'accordatura con la nuova soglia
                AvviaAccordatura(soglia);
            }
        }

        private double CalcolaSogliaAccordatura(string cordaInfo)
        {
            double soglia = 0.0;
            if (cordaInfo.Contains("Mi (alto)"))
            {
                soglia = 1.0; // Imposta la soglia per la corda 1
            }
            else if (cordaInfo.Contains("Si"))
            {
                soglia = 2.0; // Imposta la soglia per la corda 2
            }
            else if (cordaInfo.Contains("Sol"))
            {
                soglia = 3.0; // Imposta la soglia per la corda 3
            }
            else if (cordaInfo.Contains("Re"))
            {
                soglia = 4.0; // Imposta la soglia per la corda 4
            }
            else if (cordaInfo.Contains("La"))
            {
                soglia = 5.0; // Imposta la soglia per la corda 5
            }
            else if (cordaInfo.Contains("Mi (basso)"))
            {
                soglia = 6.0; // Imposta la soglia per la corda 6
            }
            return soglia;
        }
        private void AvviaAccordatura(double soglia)
        {
            if (FrequenzaAttuale.Text.Trim() != String.Empty)
            {
                double targetFrequency = GetTargetFrequency();// Ottieni la frequenza target in base alla corda selezionata dal ComboBox
                double currentFrequency = double.Parse(FrequenzaAttuale.Text);

                double frequencyDifference = Math.Abs(currentFrequency - targetFrequency);
                double differencePercentage = (frequencyDifference / targetFrequency) * 100.0;

                const int MaxValue = 100;
                int progressValue = MaxValue - (int)differencePercentage;
                progressValue = Math.Max(0, Math.Min(MaxValue, progressValue));

                AccordaturaProgressBar.Value = progressValue;
                AccordaturaProgressBar.Foreground = progressValue == MaxValue ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            }
        }

        private double GetTargetFrequency()
        {
            ComboBoxItem cordaSelezionata = (ComboBoxItem)SelezionaCorda.SelectedItem;
            string cordaInfo = cordaSelezionata.Content.ToString();

            if (cordaInfo.Contains("Mi (alto)"))
            {
                return NoteMusicali.Mi_Alto;
            }
            else if (cordaInfo.Contains("Si"))
            {
                return NoteMusicali.Si;
            }
            else if (cordaInfo.Contains("Sol"))
            {
                return NoteMusicali.Sol;
            }
            else if (cordaInfo.Contains("Re"))
            {
                return NoteMusicali.Re;
            }
            else if (cordaInfo.Contains("La"))
            {
                return NoteMusicali.La;
            }
            else if (cordaInfo.Contains("Mi (basso)"))
            {
                return NoteMusicali.Mi_Basso;
            }

            return 0.0; // Valore predefinito se non viene riconosciuta una corda
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