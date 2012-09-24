
namespace BulstarCheck
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using BulstarCheck.Properties;
    using System.Diagnostics;
    using System.Reflection;

    public class Util
    {
        private Util()
        {
        }

        /// <summary>
        /// Gets assembly version
        /// </summary>
        /// <value>The version</value>
        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public static string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static bool ObjectIsNull(object obj) 
        {
            return (null == obj);
        }

        public static void OpenChildForm(Form theForm) 
        {
            List<string> toClose = new List<string>();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != theForm.Name && f.TopLevel == false)
                {
                    toClose.Add(f.Name);
                }
            }
            foreach (string item in toClose)
            {
                Application.OpenForms[item].Close();
            }
            theForm.Show();
        }

        public static void CloseAllForms()
        {
            List<string> toClose = new List<string>();
            foreach (Form f in Application.OpenForms)
            {
                if (!f.TopLevel)
                {
                    toClose.Add(f.Name);
                }
            }
            foreach (string item in toClose)
            {
                Application.OpenForms[item].Close();
            }
        }

        public static int GetCountyId()
        {
            int ret = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(FormMain))
                {
                    ret = (f as FormMain).CountyId;
                    break;
                }
            }
            return ret;
        }

        public static bool ExitApplication()
        {
            DialogResult dlg = MessageBox.Show(Messages.application_exit,
                Messages.application_end, MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            
            if (dlg == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetupGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.AutoResizeColumns();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public static void JumpToLastRow(DataGridView dgv)
        {
            int jumpToRow = dgv.Rows.GetLastRow(DataGridViewElementStates.None);
            if (dgv.Rows.Count >= jumpToRow && jumpToRow >= 1)
            {
                dgv.FirstDisplayedScrollingRowIndex = jumpToRow;
                dgv.ClearSelection();
                dgv.Rows[jumpToRow].Selected = true;                
            }
        }

        public static void JumpToFirstRow(DataGridView dgv)
        {
            int jumpToRow = dgv.Rows.GetFirstRow(DataGridViewElementStates.None);
            if (dgv.Rows.Count >= jumpToRow && jumpToRow >= 0)
            {
                dgv.FirstDisplayedScrollingRowIndex = jumpToRow;
                dgv.ClearSelection();
                dgv.Rows[jumpToRow].Selected = true;
            }
        }

        public static void CursorHand(Form theForm, DataGridView dgv, int columnIndex)
        {
            if (columnIndex == (dgv.Columns.Count - 1))
            {
                theForm.Cursor = Cursors.Hand;
            }
            else
            {
                theForm.Cursor = Cursors.Default;
            }
        }

        public static void DeleteCellClickPerson(DataTier db, DataGridView dgv, int rowIndex,int columnIndex, bool active)
        {
            if (rowIndex >= 0 && columnIndex >= 0 && !dgv.Rows[rowIndex].IsNewRow && 
                dgv.Rows[rowIndex].Cells[columnIndex].OwningColumn == dgv.Columns["Active"])
            {
                DialogResult dlg = MessageBox.Show(Messages.person_activate, Messages.application_confirm,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dlg == DialogResult.OK)
                {
                    db.ActivateDeactivatePerson((int)dgv.Rows[rowIndex].Cells["Id"].Value, 
                        active ? (byte)0 : (byte)1);
                }
                db.LoadDataPerson(dgv, active);
            }
        }

        public static void DeleteCellClickEvent(DataTier db, DataGridView dgv, int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0 && columnIndex >= 0 && !dgv.Rows[rowIndex].IsNewRow &&
                dgv.Rows[rowIndex].Cells[columnIndex].OwningColumn == dgv.Columns["Delete"])
            {
                DialogResult dlg = MessageBox.Show(Messages.event_confirm_delete, Messages.application_confirm,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dlg == DialogResult.OK)
                {
                    db.RemoveEvent((int)dgv.Rows[rowIndex].Cells["Id"].Value);
                    db.LoadDataEvent(dgv);
                }
            }
        }

        public static void DeleteCellClickReg(DataTier db, DataGridView dgv, TextBox txtReg,int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0 && columnIndex >= 0 && !dgv.Rows[rowIndex].IsNewRow &&
                dgv.Rows[rowIndex].Cells[columnIndex].OwningColumn == dgv.Columns["Delete"])
            {
                DialogResult dlg = MessageBox.Show(Messages.reg_confirm, Messages.application_confirm,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dlg == DialogResult.OK)
                {
                    dgv.Rows.RemoveAt(rowIndex);
                    db.CommitGridEventPresence(dgv);
                    Util.BarcodePrepare(txtReg);
                }
            }
        }

        public static void OpenReportReg(DataTier db, DataGridView dgv, int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0 && columnIndex >= 0 && !dgv.Rows[rowIndex].IsNewRow &&
                dgv.Rows[rowIndex].Cells[columnIndex].OwningColumn == dgv.Columns["Report"])
            {
                FormReport fr = new FormReport();
                fr.Show();
                int eventId = int.Parse(dgv.Rows[rowIndex].Cells["Id"].Value.ToString());
                fr.ShowReport("ReportEvent.rdlc", "DataSetReg", db.GetEventReport(eventId));
            }
        }

        public static void OpenReportPerson(DataTier db, DataGridView dgv, int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0 && columnIndex >= 0 && !dgv.Rows[rowIndex].IsNewRow &&
                dgv.Rows[rowIndex].Cells[columnIndex].OwningColumn == dgv.Columns["Report"])
            {
                FormReport fr = new FormReport();
                fr.Show();
                int personId = int.Parse(dgv.Rows[rowIndex].Cells["Id"].Value.ToString());
                fr.ShowReport("ReportPersonEvent.rdlc", "DataSetPersonEvent", db.GetPersonEvent(personId));
            }
        }

        public static bool ConvertState(int comboIndex)
        {
            return (comboIndex == 0) ? true : false;
        }

        public static void BarcodePrepare(TextBox txtReg)
        {
            txtReg.Text = string.Empty;
            txtReg.Focus();
        }        
    }
}