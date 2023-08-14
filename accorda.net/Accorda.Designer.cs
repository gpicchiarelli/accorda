namespace accorda.net
{
    partial class Accorda
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Accorda));
            dominante = new RichTextBox();
            waveViewer1 = new NAudio.Gui.WaveViewer();
            DispositiviIngresso = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // dominante
            // 
            resources.ApplyResources(dominante, "dominante");
            dominante.Name = "dominante";
            dominante.ReadOnly = true;
            dominante.TextChanged += richTextBox1_TextChanged;
            // 
            // waveViewer1
            // 
            resources.ApplyResources(waveViewer1, "waveViewer1");
            waveViewer1.Name = "waveViewer1";
            waveViewer1.SamplesPerPixel = 128;
            waveViewer1.StartPosition = 0L;
            waveViewer1.WaveStream = null;
            // 
            // DispositiviIngresso
            // 
            DispositiviIngresso.DropDownStyle = ComboBoxStyle.DropDownList;
            resources.ApplyResources(DispositiviIngresso, "DispositiviIngresso");
            DispositiviIngresso.FormattingEnabled = true;
            DispositiviIngresso.Name = "DispositiviIngresso";
            DispositiviIngresso.SelectedIndexChanged += DispositiviIngresso_SelectedIndexChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // Accorda
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(DispositiviIngresso);
            Controls.Add(waveViewer1);
            Controls.Add(dominante);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Accorda";
            TopMost = true;
            FormClosing += Accorda_FormClosing;
            Load += Accorda_Load;
            Shown += Accorda_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox dominante;
        private NAudio.Gui.WaveViewer waveViewer1;
        private ComboBox DispositiviIngresso;
        private Label label1;
    }
}