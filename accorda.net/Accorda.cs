using accorda.Audio;
using Microsoft.VisualBasic.Devices;

namespace accorda.net
{
    public partial class Accorda : Form
    {
        private Audio.Audio audioRecorder { set; get; }

        public Accorda()
        {
            var audioRecorder = new Audio.Audio();
            InitializeComponent();
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;
        }

        private void AudioRecorder_DominantFrequencyDetected(object sender, double dominantFrequency)
        {
            dominante.Text = dominantFrequency.ToString();
        }

        private void Accorda_Load(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Accorda_Shown(object sender, EventArgs e)
        {
            audioRecorder?.StartRecording();
        }

        private void Accorda_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioRecorder?.StopRecording();
        }

        private async Task Accorda_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            await audioRecorder?.StopRecordingAsync();
        }

        private async Task Accorda_ShownAsync(object sender, EventArgs e)
        {
            await audioRecorder?.StartRecordingAsync();
        }
    }
}