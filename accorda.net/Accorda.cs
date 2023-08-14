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
            // audioRecorder.BufferedWave.
            //waveViewer1.WaveStream = audioRecorder.BufferedWave;
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
    }
}