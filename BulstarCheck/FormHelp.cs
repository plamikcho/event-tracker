
namespace BulstarCheck
{
    using System.Windows.Forms;

    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        private void FormHelp_Load(object sender, System.EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\resources\userguide.pdf");
        }
    }
}