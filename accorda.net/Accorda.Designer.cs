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
            SuspendLayout();
            // 
            // dominante
            // 
            resources.ApplyResources(dominante, "dominante");
            dominante.Name = "dominante";
            dominante.ReadOnly = true;
            dominante.TextChanged += richTextBox1_TextChanged;
            // 
            // Accorda
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dominante);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Accorda";
            TopMost = true;
            FormClosing += Accorda_FormClosing;
            Load += Accorda_Load;
            Shown += Accorda_Shown;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox dominante;
    }
}