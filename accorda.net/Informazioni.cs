using System;
using System.Reflection;
using System.Windows.Forms;

namespace Accorda
{
    partial class Informazioni : Form
    {
        public Informazioni()
        {
            InitializeComponent();
            InitializeAppInfo();
        }

        private void InitializeAppInfo()
        {
            this.Text = $"Informazioni su {AssemblyTitle}";
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = $"Versione {AssemblyVersion}";
            this.labelCopyright.Text = $"© {DateTime.Now.Year} {AssemblyCompany}";
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text =
                $"{AssemblyDescription}{Environment.NewLine}{Environment.NewLine}" +
                "🎵 Questo software è rilasciato sotto licenza MIT. 🎵" +
                $"{Environment.NewLine}Per maggiori dettagli, consulta il file LICENSE.{Environment.NewLine}{Environment.NewLine}" +
                "🌟 Repository GitHub: https://github.com/tuousername/tuoprogetto 🌟" +
                $"{Environment.NewLine}📜 Leggi il file README.md per informazioni sull'utilizzo. 📜" +
                $"{Environment.NewLine}💡 Se desideri contribuire, consulta CONTRIBUTING.md. 💡";
        }

        #region Funzioni di accesso attributo assembly

        private string GetAttributeValue<T>(Func<T, string> valueGetter)
            where T : Attribute
        {
            var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<T>();
            return attribute != null ? valueGetter(attribute) : "";
        }

        public string AssemblyTitle => GetAttributeValue<AssemblyTitleAttribute>(a => a.Title);
        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string AssemblyDescription => GetAttributeValue<AssemblyDescriptionAttribute>(a => a.Description);
        public string AssemblyProduct => GetAttributeValue<AssemblyProductAttribute>(a => a.Product);
        public string AssemblyCopyright => GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright);
        public string AssemblyCompany => GetAttributeValue<AssemblyCompanyAttribute>(a => a.Company);

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}