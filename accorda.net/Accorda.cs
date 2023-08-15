using Accorda.Audio;
using Microsoft.VisualBasic.Devices;
using static System.Net.Mime.MediaTypeNames;
using NAudio;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;

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
            selezionaCorda.Items.Add("Corda 1 - Mi (alto) [E] " + NoteMusicali.Mi_Alto + "Hz");
            selezionaCorda.Items.Add("Corda 2 - Si [B] " + NoteMusicali.Si + "Hz");
            selezionaCorda.Items.Add("Corda 3 - Sol [G] " + NoteMusicali.Sol + "Hz");
            selezionaCorda.Items.Add("Corda 4 - Re [D] " + NoteMusicali.Re + "Hz");
            selezionaCorda.Items.Add("Corda 5 - La [A] " + NoteMusicali.La + "Hz");
            selezionaCorda.Items.Add("Corda 6 - Mi (basso) [E] " + NoteMusicali.Mi_Basso + "Hz");
            selezionaCorda.SelectedIndex = 0;
        }

        private void AggiornaFrequenza(double dominantFrequency)
        {
            if (dominante.InvokeRequired)
            {
                Action safeWrite = delegate { AggiornaFrequenza(dominantFrequency); };
                dominante.Invoke(AggiornaFrequenza, dominantFrequency);
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
            Informazioni i = new Informazioni();
            i.ShowDialog();
        }
    }
}