
using System.Diagnostics;
using System.Data;
namespace BulstarCheck {    
    
    public partial class RegcheckDataSet {

        partial class personsDataTable
        {
            protected override void OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs pcevent)
            {
                
                base.OnPropertyChanging(pcevent);
            }

            protected override void OnColumnChanging(System.Data.DataColumnChangeEventArgs e)
            {
                //Debug.WriteLine(e.Column.ColumnName);
                //DataRow r = e.Row;
                //r[this.UserModifierColumn.ColumnName] = "plamend";
                //r[this.ApplicationColumn.ColumnName] = 1;
                //r[this.ApplicationColumn.ColumnName] = 1;
                //Debug.WriteLine(e.ProposedValue.ToString());
                base.OnColumnChanging(e);
            }

            protected override void OnTableNewRow(DataTableNewRowEventArgs e)
            {
                
                base.OnTableNewRow(e);
            }
        }
    }
}

namespace BulstarCheck.RegcheckDataSetTableAdapters {
    
    
    public partial class personsTableAdapter {
        
        
        
    }
}
