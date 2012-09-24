
namespace BulstarCheck
{
    using System;
    using System.Windows.Forms;

    public partial class FormAddPerson : Form
    {
        private DataTier db;
        public event EventHandler RecordAdded;
        
        public FormAddPerson()
        {
            InitializeComponent();
        }

        private void FormAddPerson_Load(object sender, EventArgs e)
        {
            db = new DataTier(Util.GetCountyId());
            ucAdd1.Txt4Visible = true;
            ucAdd1.InitializePass(
                new string[] {
                    Messages.person_lpk, Messages.person_fname, 
                    Messages.person_sname, Messages.person_lname});
            ucAdd1.ButtonName = Messages.application_add_button;            
            ucAdd1.ButtonClicked += new UcAddEventHandler(ucAdd1_ButtonClicked);
        }

        private void ucAdd1_ButtonClicked(object sender, UcAddEventArgs e)
        {
            Validator.ValidatePerson(e.TextValue1, e.TextValue2, e.TextValue3, e.TextValue4);
            db.InsertPerson(e.TextValue1, e.TextValue2, e.TextValue3, e.TextValue4);
            RecordAdded(this, e);
            this.Close();
        }

        private void FormAddPerson_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();            
        }
    }
}