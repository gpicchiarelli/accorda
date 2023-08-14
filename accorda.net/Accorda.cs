using accorda.Audio;
using Microsoft.VisualBasic.Devices;
using static System.Net.Mime.MediaTypeNames;

namespace accorda.net
{
    public partial class Accorda : Form
    {
        private Audio.Audio audioRecorder { set; get; }
        private object sem1 { set; get; }

        public Accorda()
        {
            sem1 = new object();
            audioRecorder = new Audio.Audio();
            InitializeComponent();
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;
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
    }
}