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
            int indiceDispositivoSelezionato = InputDevices.SelectedIndex;
            audioRecorder = new Audio.Audio(indiceDispositivoSelezionato);
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;
        }

        private void AudioRecorder_DominantFrequencyDetected(object sender, double frequenzaDominante)
        {
            Dispatcher.Invoke(() =>
            {
                FrequenzaAttuale.Text = frequenzaDominante.ToString("F2");
                AvviaAccordatura();
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
                SelezionaCorda.SelectedIndex = -1;
            }
            else
            {
                if (SelezionaCorda.SelectedIndex != -1)
                {
                    ComboBoxItem cordaSelezionata = (ComboBoxItem)SelezionaCorda.SelectedItem;
                    if (cordaSelezionata is not null)
                    {
                        string cordaInfo = cordaSelezionata.Content.ToString();
                        if (cordaInfo is not null)
                        {
                            double targetFrequency = GetTargetFrequency();
                            AvviaAccordatura();
                        }
                    }
                }
            }
        }

        private void AvviaAccordatura()
        {
            if (FrequenzaAttuale.Text.Trim() != String.Empty)
            {
                double targetFrequency = GetTargetFrequency();
                double currentFrequency = double.Parse(FrequenzaAttuale.Text);
            }
        }

        private double GetTargetFrequency()
        {
            ComboBoxItem cordaSelezionata = (ComboBoxItem)SelezionaCorda.SelectedItem;
            if (cordaSelezionata is not null)
            {
                string cordaInfo = cordaSelezionata?.Content.ToString();

                if (cordaInfo is null) return 0;

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
            }
            return 0.0;
        }

        private void AccordaturaProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}