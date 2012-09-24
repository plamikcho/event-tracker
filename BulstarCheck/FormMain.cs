
namespace BulstarCheck
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Windows.Forms;

    public partial class FormMain : Form
    {
        public Dictionary<string, bool> ChildsLoaded { get; private set; }
        public int CountyId { get; private set; }
        DataTier db;

        public FormMain()
        {
            InitializeComponent();
            InitializeMembers();
            this.CountyId = int.Parse(ConfigurationManager.AppSettings.Get("city"));
            db = new DataTier(CountyId);
            toolStripStatusLblCity.Text = db.GetCountyById;
        }
        
        private void InitializeMembers()
        {
            if (Util.ObjectIsNull(this.ChildsLoaded))
            {
                this.ChildsLoaded = new Dictionary<string, bool>();
            }
        }

        public void StartChildForm(Form theForm)
        {
            if (!this.ChildsLoaded.ContainsKey(theForm.Name))
            {
                this.ChildsLoaded.Add(theForm.Name, false);
            }
            theForm.TopLevel = false;
            theForm.Parent = this;
            theForm.Dock = DockStyle.Fill;
            theForm.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(theForm);
            this.ChildsLoaded[theForm.Name] = true;
            Util.OpenChildForm(theForm);            
        }

        private void personToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormList formPersons = new FormList();
            StartChildForm(formPersons);
        }

        private void regToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReg formReg = new FormReg();
            StartChildForm(formReg);
        }

        private void eventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEvent formEvent = new FormEvent();
            StartChildForm(formEvent);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Util.ExitApplication())
            {
                e.Cancel = true;
            }
            else
            {
                Util.CloseAllForms();
                db.Dispose();
                Application.ExitThread();
            }
        }

        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp fh = new FormHelp();
            fh.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormStart fs = new FormStart();
            StartChildForm(fs);
        }        
    }
}