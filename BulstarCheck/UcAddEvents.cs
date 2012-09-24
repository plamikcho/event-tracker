
namespace BulstarCheck
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public delegate void UcAddEventHandler(object sender, UcAddEventArgs e);

    public class UcAddEventArgs : EventArgs
    {
        public string TextValue1 { get; private set; }
        public string TextValue2 { get; private set; }
        public string TextValue3 { get; private set; }
        public string TextValue4 { get; private set; }

        public UcAddEventArgs(string t1, string t2, string t3, string t4)
        {
            this.TextValue1 = t1;
            this.TextValue2 = t2;
            this.TextValue3 = t3;
            this.TextValue4 = t4;
        }
    }
}