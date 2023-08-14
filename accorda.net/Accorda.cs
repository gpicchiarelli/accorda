using accorda.Audio;

namespace accorda.net
{
    public partial class Accorda : Form
    {
        public Accorda()
        {
            InitializeComponent();
            var audioRecorder = new Audio.Audio();
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;

        }

        private static void AudioRecorder_DominantFrequencyDetected(object sender, double dominantFrequency)
        {
            richTextBox1.Text = dominantFrequency.ToString();
        }

        private void Accorda_Load(object sender, EventArgs e)
        {

        }
    }
}