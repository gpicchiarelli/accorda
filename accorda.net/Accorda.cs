using accorda.Audio;

namespace accorda.net
{
    public partial class Accorda : Form
    {
        public Accorda()
        {
            InitializeComponent();
            accorda.Audio.Audio audioRecorder = new accorda.Audio.Audio();
            audioRecorder.DominantFrequencyDetected += AudioRecorder_DominantFrequencyDetected;

        }

        private static void AudioRecorder_DominantFrequencyDetected(object sender, double dominantFrequency)
        {
            Console.WriteLine($"Armonica dominante rilevata: {dominantFrequency} Hz");
        }
    }
}