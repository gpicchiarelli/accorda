using System.Reflection;
using System.Text;
using Markdig;

namespace Accorda
{
    internal partial class Informazioni : Form
    {
        public Informazioni()
        {
            InitializeComponent();
            InitializeAppInfo();
        }

        private void InitializeAppInfo()
        {
            Text = $"Informazioni su {AssemblyTitle}";
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = $"Versione {AssemblyVersion}";
            labelCopyright.Text = $"© {DateTime.Now.Year} {AssemblyCompany}";
            labelCompanyName.Text = AssemblyCompany;

            string authors = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(Markdown.ToPlainText(Properties.Resources.AUTHORS)));
            string contributing = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(Markdown.ToPlainText(Properties.Resources.CONTRIBUTING)));
            string license = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(Markdown.ToPlainText(Properties.Resources.LICENSE)));
            string readme = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(Markdown.ToPlainText(Properties.Resources.README)));

            textBoxDescription.Text =
                $"🎵 Questo software è rilasciato sotto licenza MIT. 🎵" +
                $"{Environment.NewLine}Per maggiori dettagli, consulta il file LICENSE:{Environment.NewLine}{license}{Environment.NewLine}" +
                $"🌟 Repository GitHub: [github.com/gpicchiarelli/accorda](https://github.com/gpicchiarelli/accorda) 🌟" +
                $"{Environment.NewLine}📜 Per informazioni sull'utilizzo, leggi il file [README.md](https://github.com/gpicchiarelli/accorda/blob/main/README.md):{Environment.NewLine}{readme}{Environment.NewLine}" +
                $"{Environment.NewLine}💡 Se desideri contribuire, consulta [CONTRIBUTING.md](https://github.com/gpicchiarelli/accorda/blob/main/CONTRIBUTING.md):{Environment.NewLine}{contributing}{Environment.NewLine}" +
                $"{Environment.NewLine}👥 Autore/i: {authors}";
        }

        #region Funzioni di accesso attributo assembly

        private string GetAttributeValue<T>(Func<T, string> valueGetter)
            where T : Attribute
        {
            T? attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<T>();
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
            Close();
        }
    }
}