
namespace BulstarCheck
{
    using System;
    using System.Windows.Forms;

    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            lblVer.Text = Util.AssemblyVersion;
            lblCopy.Text = Util.AssemblyCopyright;
        }
    }
}