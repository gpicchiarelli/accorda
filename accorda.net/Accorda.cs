namespace Accorda.net
{
    public partial class Accorda : Form
    {
        private Audio.Audio audioRecorder { set; get; }

        public Accorda()
        {
            audioRecorder = new Audio.Audio();
            InitializeComponent();
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;
            _ = selezionaCorda.Items.Add("Corda 1 - Mi (alto) [E] " + NoteMusicali.Mi_Alto + "Hz");
            _ = selezionaCorda.Items.Add("Corda 2 - Si [B] " + NoteMusicali.Si + "Hz");
            _ = selezionaCorda.Items.Add("Corda 3 - Sol [G] " + NoteMusicali.Sol + "Hz");
            _ = selezionaCorda.Items.Add("Corda 4 - Re [D] " + NoteMusicali.Re + "Hz");
            _ = selezionaCorda.Items.Add("Corda 5 - La [A] " + NoteMusicali.La + "Hz");
            _ = selezionaCorda.Items.Add("Corda 6 - Mi (basso) [E] " + NoteMusicali.Mi_Basso + "Hz");
            selezionaCorda.SelectedIndex = 0;
        }

        private void AggiornaFrequenza(double dominantFrequency)
        {
            if (dominante.InvokeRequired)
            {
                void safeWrite() { AggiornaFrequenza(dominantFrequency); }
                _ = dominante.Invoke(AggiornaFrequenza, dominantFrequency);
            }
            else
            {
                dominante.Text = dominantFrequency.ToString();
            }
        }

        private void AudioRecorder_DominantFrequencyDetected(object sender, double dominantFrequency)
        {
            AggiornaFrequenza(dominantFrequency);
        }

        private void Accorda_Load(object sender, EventArgs e)
        {
            DispositiviIngresso.Items.AddRange(audioRecorder.ElencaDispositiviIngresso().ToArray());
            DispositiviIngresso.SelectedIndex = 0;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (selezionaCorda.SelectedIndex != -1)
            {
                var frequenza = 0.0;
                switch (selezionaCorda.SelectedIndex)
                {
                    case 0:
                        frequenza = NoteMusicali.Mi_Alto;
                        break;
                    case 1:
                        frequenza = NoteMusicali.Si;
                        break;
                    case 2:
                        frequenza = NoteMusicali.Sol;
                        break;
                    case 3:
                        frequenza = NoteMusicali.Re;
                        break;
                    case 4:
                        frequenza = NoteMusicali.La;
                        break;
                    case 5:
                        frequenza = NoteMusicali.Mi_Basso;
                        break;
                }
                var frequency = double.Parse(dominante.Text);
                double frequencyDifference = Math.Abs(frequency - frequenza);
                int sgn = frequency.CompareTo(frequenza);
                // Calcola la percentuale di differenza rispetto alla frequenza attesa
                double differencePercentage = (frequencyDifference / frequenza) * 100.0;
                int progressValue = 0;
                if (sgn > 0)
                {
                    // Calcola quanto la ProgressBar deve riempirsi in base alla percentuale di differenza
                    progressValue = 50 + (int)(differencePercentage / 2.0); // Imposta il 50% come valore di riferimento
                } 
                if(sgn < 0)
                {
                    progressValue = 50 - (int)(differencePercentage / 2.0); // Imposta il 50% come valore di riferimento
                }
                if (sgn == 0) 
                {
                    progressValue = 50 - (int)(differencePercentage / 2.0); // Imposta il 50% come valore di riferimento
                }
                // Limita il valore tra 0 e 100
                progressValue = Math.Max(0, Math.Min(100, progressValue));

                // Imposta il valore della ProgressBar
                progressBar1.Value = progressValue;

                // Cambia il colore della ProgressBar in base alla differenza
                if (progressBar1.Value == 50)
                {
                    progressBar1.ForeColor = Color.Green; // Accordato
                }
                else
                {
                    progressBar1.ForeColor = Color.Red; // Non accordato
                }
            }
        }

        private void Accorda_Shown(object sender, EventArgs e)
        {
        }

        private void Accorda_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioRecorder?.StopRecording();
        }

        private void DispositiviIngresso_SelectedIndexChanged(object sender, EventArgs e)
        {
            audioRecorder = new Audio.Audio(DispositiviIngresso.SelectedIndex);

        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void chiudiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void informazioniToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Informazioni i = new();
            _ = i.ShowDialog();
        }

        private void selezionaCorda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}