
namespace BulstarCheck
{
    using System;
    using System.Windows.Forms;

    public partial class FormAddEvent : Form
    {
        private DataTier db;
        public event EventHandler RecordAdded;

        public FormAddEvent()
        {
            InitializeComponent();
        }

        private void FormAddEvent_Load(object sender, EventArgs e)
        {
            db = new DataTier(Util.GetCountyId());
            ucAdd1.Txt4Visible = false;
            ucAdd1.InitializePass(
                new string[] {Messages.event_name, Messages.event_desc, Messages.event_date });
            ucAdd1.ButtonName = Messages.application_add_button;            
            ucAdd1.ButtonClicked += new UcAddEventHandler(ucAdd1_ButtonClicked);
        }

        void ucAdd1_ButtonClicked(object sender, UcAddEventArgs e)
        {
            Validator.ValidateEvent(e.TextValue1, e.TextValue2, e.TextValue3);
            db.InsertEvent(e.TextValue1, e.TextValue2, 
                DateTime.Parse(e.TextValue3,System.Globalization.CultureInfo.CurrentCulture));
            RecordAdded(this, e);
            this.Close();
        }

        private void FormAddEvent_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}