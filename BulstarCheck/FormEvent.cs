
namespace BulstarCheck
{
    using System;
    using System.Windows.Forms;

    public partial class FormEvent : Form
    {
        private DataTier db;

        public FormEvent()
        {
            InitializeComponent();
        }

        private void FormEvent_Load(object sender, EventArgs e)
        {            
            Util.SetupGrid(dataGridView1);
            db = new DataTier(Util.GetCountyId());
            db.LoadDataEvent(dataGridView1);
        }

        private void FormEvent_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                db.InjectDataBeforeCommitEvent(dataGridView1, e.RowIndex);
            }
        }

        private void toolStripBtnAddNew_Click(object sender, EventArgs e)
        {
            FormAddEvent f = new FormAddEvent();
            f.RecordAdded += new EventHandler(f_RecordAdded);
            f.Show();
        }

        void f_RecordAdded(object sender, EventArgs e)
        {
            db.LoadDataEvent(dataGridView1);
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            Util.CursorHand(this, dataGridView1, e.ColumnIndex);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rc = dataGridView1.RowCount;
            Util.DeleteCellClickEvent(db, dataGridView1, e.RowIndex, e.ColumnIndex);
            if (rc == dataGridView1.RowCount)
            {
                Util.OpenReportReg(db, dataGridView1, e.RowIndex, e.ColumnIndex);
            }
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            db.CommitGridEvent(dataGridView1);            
        }
    }
}