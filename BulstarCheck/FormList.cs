
namespace BulstarCheck
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class FormList : Form
    {
        private DataTier db;
        public DataGridView Dgv { get { return dataGridView1; } }

        public FormList()
        {
            InitializeComponent();            
        }

        private void FormList_Load(object sender, EventArgs e)
        {
            Util.SetupGrid(dataGridView1);
            db = new DataTier(Util.GetCountyId());
            toolStripComboBox1.SelectedIndex = 0;
            db.LoadDataPerson(dataGridView1, Util.ConvertState(toolStripComboBox1.SelectedIndex));
        }

        private void FormList_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.CommitGridPerson(dataGridView1, Util.ConvertState(toolStripComboBox1.SelectedIndex));
            db.LoadDataPerson(dataGridView1, Util.ConvertState(toolStripComboBox1.SelectedIndex));
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                db.InjectDataBeforeCommitPerson(dataGridView1, e.RowIndex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rc = dataGridView1.RowCount;
            Util.DeleteCellClickPerson(db, dataGridView1, e.RowIndex, e.ColumnIndex, 
                Util.ConvertState(toolStripComboBox1.SelectedIndex));
            if (rc == dataGridView1.RowCount)
            {
                Util.OpenReportPerson(db, dataGridView1, e.RowIndex, e.ColumnIndex);
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            Util.CursorHand(this, dataGridView1, e.ColumnIndex);
        }

        private void FormList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dataGridView1.CancelEdit();
            this.dataGridView1.Dispose();
        }

        private void toolStripBtnAddNew_Click(object sender, EventArgs e)
        {
            FormAddPerson f = new FormAddPerson();
            f.RecordAdded += new EventHandler(f_RecordAdded);
            f.Show();
        }

        private void f_RecordAdded(object sender, EventArgs e)
        {
            db.LoadDataPerson(dataGridView1, Util.ConvertState(toolStripComboBox1.SelectedIndex));            
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            db.CommitGridPerson(dataGridView1, Util.ConvertState(toolStripComboBox1.SelectedIndex));
        }

        private void toolStripBtnSearch_Click(object sender, EventArgs e)
        {            
            if (toolStripTxtSearch.Text.Length > 0)
            {                
                db.LoadDataSearchPerson(toolStripTxtSearch.Text, 
                    Util.ConvertState(toolStripComboBox1.SelectedIndex), dataGridView1);
            }
            else
            {
                db.LoadDataPerson(dataGridView1, Util.ConvertState(toolStripComboBox1.SelectedIndex));
            }
        }

        private void toolStripBtnPrint_Click(object sender, EventArgs e)
        {
            FormReport fr = new FormReport();
            fr.Show();
            fr.ShowReport("ReportPerson.rdlc", "DataSetPerson", db.GetDataPerson());
        }
    }
}