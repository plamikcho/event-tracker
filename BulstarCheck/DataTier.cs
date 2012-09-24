
namespace BulstarCheck
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    public sealed class DataTier : IDisposable
    {
        private int countyId;
        private BindingSource bs;
        private RegcheckDataSet ds;
        private RegcheckDataSetTableAdapters.event_presenceTableAdapter epda;
        private RegcheckDataSetTableAdapters.eventsTableAdapter eda;
        private RegcheckDataSetTableAdapters.personsTableAdapter pda;
        private RegcheckDataSetTableAdapters.countyTableAdapter cda;

        public int LastEventId { get; private set; }

        private DataTier()
	    {
	    }

        public DataTier(int countyId)
        {
            this.countyId = countyId;
            this.LastEventId = 0;
            bs = new BindingSource();
            ds = new RegcheckDataSet();
            cda = new RegcheckDataSetTableAdapters.countyTableAdapter();
            epda = new RegcheckDataSetTableAdapters.event_presenceTableAdapter();
            eda = new RegcheckDataSetTableAdapters.eventsTableAdapter();
            pda = new RegcheckDataSetTableAdapters.personsTableAdapter();            
        }

        public string GetCountyById
        {
            get
            {
                if (ds.county.Rows.Count == 0)
                {
                    cda.Fill(ds.county);
                }
                DataRow[] ar = ds.county.Select(string.Format("Id={0}", this.countyId));
                return ar[0][ds.county.DescriptionColumn.ColumnName].ToString();
            }
        }

        public bool LoadRegEvent(Label lblEvent)
        {
            eda.FillLast(ds.events, countyId);
            if (ds.events.Rows.Count > 0)
            {
                DataRow re = ds.events.Rows[0];
                lblEvent.Text = string.Format("{0}\r\n{1}\r\n{2}", 
                    re[ds.events.EventNameColumn], re[ds.events.EventDescriptionColumn], 
                    re[ds.events.DateModifiedColumn]);
                LastEventId = (int)re[ds.events.IdColumn];
                return true;
            }
            else
            {
                lblEvent.Text = string.Empty;
                // todo: open form to add event
                return false;
            }
        }

        public bool ParticipantExists(string lpk)
        {
            RegcheckDataSet.event_presenceDataTable dt =
                epda.GetPersonEvent(lpk, (int)ds.events.Rows[0][ds.events.IdColumn], countyId);
            if (dt.Rows.Count > 0)
            {
                // exists
                return true;
            }
            return false;
        }

        public bool EventExists
        {
            get 
            {
                return this.eda.GetDataLast(this.countyId).Count > 0 ? true : false;
            }
        }

        public bool ParticipantActive(string lpk)
        {
            DataTable dt = pda.GetDataByLpk(lpk, this.countyId);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                if (int.Parse(r[ds.persons.ActiveColumn.ColumnName].ToString()) == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public void ProcessBarcode(TextBox txtReg, Label lblPerson, DataGridView dgv)
        {
            pda.FillByLpk(ds.persons, txtReg.Text, countyId);
            if (ds.persons.Rows.Count > 0)
            {
                DataRow rp = ds.persons.Rows[0];
                lblPerson.Text = string.Format("{0}\r\n{1}\r\n{2}", rp[ds.persons.FirstNameColumn],
                    rp[ds.persons.SecondNameColumn], rp[ds.persons.FamilyNameColumn]);
                if (this.EventExists)
                {
                    if (!ParticipantExists(txtReg.Text))
                    {
                        if (this.ParticipantActive(txtReg.Text))
                        {
                            epda.Insert((int)rp[ds.persons.IdColumn],
                                (int)ds.events.Rows[0][ds.events.IdColumn], DateTime.Now, this.countyId);
                            LoadDataEventPresence(dgv);
                        }
                        else
                        {
                            lblPerson.Text = Messages.person_inactive;
                        }
                    }
                }
                else
                {
                    lblPerson.Text = Messages.event_missing;
                }
            }
            else
            {
                lblPerson.Text = Messages.person_missing;
            }
        }
        
        public void LoadDataEventPresence(DataGridView dgv)
        {
            epda.FillOrdered(ds.event_presence, countyId, this.LastEventId);
            bs.DataSource = ds.event_presence;
            dgv.DataSource = bs;
        }

        public void CommitGridEventPresence(DataGridView dgv)
        {
            Util.JumpToFirstRow(dgv);
            dgv.BindingContext[bs].EndCurrentEdit();
            this.epda.Update(this.ds.event_presence);
            this.LoadDataEventPresence(dgv);
        }

        public void InjectDataBeforeCommitEventPresence(DataGridView dgv, int rowIndex)
        {
            DataGridViewRow row = dgv.Rows[rowIndex];
            if (null != row)
            {
                DataGridViewCell cell1 = row.Cells[ds.event_presence.DateModifiedColumn.ColumnName];
                cell1.Value = DateTime.Now;

                DataGridViewCell cell2 = row.Cells[ds.event_presence.CountyIdColumn.ColumnName];
                cell2.Value = this.countyId;
            }
        }

        public DataTable GetDataPerson()
        {
            return pda.GetData(this.countyId);
        }

        public void LoadDataPerson(DataGridView dgv, bool active)
        {
            if (active)
            {
                pda.FillActive(ds.persons, countyId);
            }
            else
            {
                pda.FillInactive(ds.persons, countyId);
            }
            bs.DataSource = ds.persons;
            dgv.DataSource = bs;
        }

        public void LoadDataSearchPerson(string search, bool active,DataGridView dgv)
        {
            byte activeb = active ? (byte)1 : (byte)0;
            pda.FillActiveSearch(ds.persons, string.Format("%{0}%", search), this.countyId, activeb);
            bs.DataSource = ds.persons;
            dgv.DataSource = bs;
        }

        public void InsertPerson(string lpk, string firstName, string secondName, string familyName)
        {
            pda.Insert(lpk, firstName, secondName, familyName, DateTime.Now, 1, this.countyId);
        }

        public void ActivateDeactivatePerson(int id, byte active)
        {
            pda.UpdateActive(active, id, this.countyId);
        }

        public void InjectDataBeforeCommitPerson(DataGridView dgv, int rowIndex)
        {
            DataGridViewRow row = dgv.Rows[rowIndex];
            if (null != row)
            {
                DataGridViewCell cell1 = row.Cells[ds.persons.DateModifiedColumn.ColumnName];
                cell1.Value = DateTime.Now;

                DataGridViewCell cell2 = row.Cells[ds.persons.CountyIdColumn.ColumnName];
                cell2.Value = this.countyId;
            }
        }

        public void CommitGridPerson(DataGridView dgv, bool active)
        {
            dgv.BindingContext[bs].EndCurrentEdit();
            this.pda.Update(this.ds.persons);
            this.LoadDataPerson(dgv, active);
        }

        public void LoadDataEvent(DataGridView dgv)
        {
            eda.Fill(ds.events, this.countyId);
            bs.DataSource = ds.events;
            dgv.DataSource = bs;
        }

        public void CommitGridEvent(DataGridView dgv)
        {
            Util.JumpToFirstRow(dgv);
            dgv.BindingContext[bs].EndCurrentEdit();
            this.eda.Update(this.ds.events);
            this.LoadDataEvent(dgv);
        }
        
        public void InjectDataBeforeCommitEvent(DataGridView dgv, int rowIndex)
        {
            DataGridViewRow row = dgv.Rows[rowIndex];
            if (null != row)
            {
                DataGridViewCell cell2 = row.Cells[ds.events.CountyIdColumn.ColumnName];
                cell2.Value = this.countyId;
            }
        }

        public void InsertEvent(string name, string description, DateTime date)
        {
            eda.Insert(name, description, date, countyId);
        }

        public void RemoveEvent(int id)
        {
            eda.Delete(id);
        }

        public DataTable GetEventReport()
        {
            return epda.GetDataReport(this.countyId, this.LastEventId);
        }

        public DataTable GetEventReport(int eventId)
        {
            return epda.GetDataReport(this.countyId, eventId);
        }

        public DataTable GetPersonEvent(int personId)
        {
            return epda.GetDataReportRegs(this.countyId, personId);
        }

        public void Dispose()
        {
            if (!Util.ObjectIsNull(cda))
            {
                cda.Dispose();
            }
            if (!Util.ObjectIsNull(ds))
            {
                ds.Dispose();
            }
            if (!Util.ObjectIsNull(bs))
            {
                bs.CancelEdit();
                bs.Dispose();
            }
            if (!Util.ObjectIsNull(epda))
            {
                epda.Connection.Close();
                epda.Dispose();
            }
            if (!Util.ObjectIsNull(eda))
            {
                eda.Connection.Close();
                eda.Dispose();
            }
            if (!Util.ObjectIsNull(pda))
            {
                pda.Connection.Close();
                pda.Dispose();
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}