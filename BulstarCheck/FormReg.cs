
namespace BulstarCheck
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    public partial class FormReg : Form
    {
        private DataTier db;

        public FormReg()
        {
            InitializeComponent();            
        }

        private void txtReg_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Enter && txtReg.Text.Length > 0)
            {
                db.ProcessBarcode(txtReg, lblPerson, dataGridView1);
                Util.BarcodePrepare(txtReg);
            }            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReport fr = new FormReport();
            fr.Show();
            DataTable dt = db.GetEventReport();
            fr.ShowReport("ReportEvent.rdlc", "DataSetReg", dt);
        }

        private void FormReg_Load(object sender, EventArgs e)
        {
            db = new DataTier(Util.GetCountyId());
            Util.SetupGrid(dataGridView1);
            if (!db.LoadRegEvent(lblEvent))
            {
                linkAddEvent.Visible = true;
            }
            else
            {
                linkAddEvent.Visible = false;
                db.LoadDataEventPresence(dataGridView1);
                Util.BarcodePrepare(txtReg);
            }
        }

        private void FormReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Util.DeleteCellClickReg(db, dataGridView1, txtReg, e.RowIndex, e.ColumnIndex);
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            Util.CursorHand(this, dataGridView1, e.ColumnIndex);
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            db.InjectDataBeforeCommitEventPresence(dataGridView1, e.RowIndex);
        }

        private void linkAddEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormMain fmref = Application.OpenForms[typeof(FormMain).Name] as FormMain;
            fmref.StartChildForm(new FormEvent());
        }
    }
}