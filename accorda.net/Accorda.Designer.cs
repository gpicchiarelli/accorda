namespace Accorda.net
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
            DispositiviIngresso = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            menuStrip1 = new MenuStrip();
            chiudiToolStripMenuItem = new ToolStripMenuItem();
            chiudiToolStripMenuItem1 = new ToolStripMenuItem();
            informazioniToolStripMenuItem = new ToolStripMenuItem();
            informazioniToolStripMenuItem1 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dominante
            // 
            resources.ApplyResources(dominante, "dominante");
            dominante.Name = "dominante";
            dominante.ReadOnly = true;
            dominante.TextChanged += richTextBox1_TextChanged;
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
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { chiudiToolStripMenuItem, informazioniToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // chiudiToolStripMenuItem
            // 
            chiudiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chiudiToolStripMenuItem1 });
            chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            resources.ApplyResources(chiudiToolStripMenuItem, "chiudiToolStripMenuItem");
            chiudiToolStripMenuItem.Click += chiudiToolStripMenuItem_Click;
            // 
            // chiudiToolStripMenuItem1
            // 
            chiudiToolStripMenuItem1.Name = "chiudiToolStripMenuItem1";
            resources.ApplyResources(chiudiToolStripMenuItem1, "chiudiToolStripMenuItem1");
            chiudiToolStripMenuItem1.Click += chiudiToolStripMenuItem1_Click;
            // 
            // informazioniToolStripMenuItem
            // 
            informazioniToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { informazioniToolStripMenuItem1 });
            informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
            resources.ApplyResources(informazioniToolStripMenuItem, "informazioniToolStripMenuItem");
            // 
            // informazioniToolStripMenuItem1
            // 
            informazioniToolStripMenuItem1.Name = "informazioniToolStripMenuItem1";
            resources.ApplyResources(informazioniToolStripMenuItem1, "informazioniToolStripMenuItem1");
            informazioniToolStripMenuItem1.Click += informazioniToolStripMenuItem1_Click;
            // 
            // Accorda
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DispositiviIngresso);
            Controls.Add(dominante);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Accorda";
            FormClosing += Accorda_FormClosing;
            Load += Accorda_Load;
            Shown += Accorda_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox dominante;
        private ComboBox DispositiviIngresso;
        private Label label1;
        private Label label2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem chiudiToolStripMenuItem;
        private ToolStripMenuItem informazioniToolStripMenuItem;
        private ToolStripMenuItem chiudiToolStripMenuItem1;
        private ToolStripMenuItem informazioniToolStripMenuItem1;
    }
}