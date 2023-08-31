using accorda.Note;

namespace Accorda.net
{
    /// <summary>
    /// GUI for main windows
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Accorda : Form
    {
        /// <summary>
        /// Gets or sets the audio recorder.
        /// </summary>
        /// <value>
        /// The audio recorder.
        /// </value>
        private Audio.Audio audioRecorder { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Accorda"/> class.
        /// </summary>
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

        /// <summary>
        /// Aggiornas the frequenza.
        /// </summary>
        /// <param name="dominantFrequency">The dominant frequency.</param>
        private void AggiornaFrequenza(double dominantFrequency)
        {
            if (dominante.InvokeRequired)
            {
                _ = dominante.Invoke(AggiornaFrequenza, dominantFrequency);
            }
            else
            {
                dominante.Text = dominantFrequency.ToString();
            }
        }

        /// <summary>
        /// Audioes the recorder dominant frequency detected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="dominantFrequency">The dominant frequency.</param>
        private void AudioRecorder_DominantFrequencyDetected(object sender, double dominantFrequency) => AggiornaFrequenza(dominantFrequency);

        /// <summary>
        /// Handles the Load event of the Accorda control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Accorda_Load(object sender, EventArgs e)
        {
            DispositiviIngresso.Items.AddRange(audioRecorder.ElencaDispositiviIngresso().ToArray());
            DispositiviIngresso.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the TextChanged event of the richTextBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (selezionaCorda.SelectedIndex != -1)
            {
                var selectedString = selezionaCorda.SelectedIndex;

                double targetFrequency = 0.0;
                switch (selectedString)
                {
                    case 0:
                        targetFrequency = NoteMusicali.Mi_Alto;
                        break;
                    case 1:
                        targetFrequency = NoteMusicali.Si;
                        break;
                    case 2:
                        targetFrequency = NoteMusicali.Sol;
                        break;
                    case 3:
                        targetFrequency = NoteMusicali.Re;
                        break;
                    case 4:
                        targetFrequency = NoteMusicali.La;
                        break;
                    case 5:
                        targetFrequency = NoteMusicali.Mi_Basso;
                        break;
                }

                double currentFrequency = double.Parse(dominante.Text);
                double frequencyDifference = Math.Abs(currentFrequency - targetFrequency);
                int sgn = currentFrequency.CompareTo(targetFrequency);
                double differencePercentage = (frequencyDifference / targetFrequency) * 100.0;

                const int ReferenceValue = 50;
                int progressValue = ReferenceValue - (int)(differencePercentage / 2.0);
                progressValue = Math.Max(0, Math.Min(100, progressValue));

                progressBar1.Value = progressValue;

                progressBar1.ForeColor = progressValue == ReferenceValue ? Color.Green : Color.Red;
            }
        }

        /// <summary>
        /// Handles the Shown event of the Accorda control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Accorda_Shown(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the FormClosing event of the Accorda control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void Accorda_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioRecorder?.StopRecording();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DispositiviIngresso control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DispositiviIngresso_SelectedIndexChanged(object sender, EventArgs e)
        {
            audioRecorder = new Audio.Audio(DispositiviIngresso.SelectedIndex);

        }

        /// <summary>
        /// Handles the Click event of the chiudiToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the chiudiToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chiudiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the informazioniToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void informazioniToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Informazioni i = new();
            _ = i.ShowDialog();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the selezionaCorda control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void selezionaCorda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}