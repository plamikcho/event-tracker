
namespace BulstarCheck
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using Microsoft.Reporting.WinForms;

    public partial class FormReport : Form
    {
        private ReportViewer rv;

        public FormReport()
        {
            InitializeComponent();
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            rv = new ReportViewer();
            this.Controls.Add(rv);
            rv.Dock = DockStyle.Fill;
        }

        private void FormReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            rv.Dispose();
        }

        public void ShowReport(string reportFileName, string reportDataSetName, DataTable data) 
        {
            rv.LocalReport.ReportPath = Application.StartupPath + @"\" + reportFileName;            
            ReportDataSource rd = new ReportDataSource(reportDataSetName, data);            
            rv.LocalReport.DataSources.Clear();
            rv.LocalReport.DataSources.Add(rd);
            rv.LocalReport.Refresh();
            rv.RefreshReport();
        }
    }
}