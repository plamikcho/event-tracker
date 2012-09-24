
namespace BulstarCheck
{
    using System;
    using System.Windows.Forms;
    
    public partial class UcAdd : UserControl
    {
        public string ButtonName { get; set; }        
        public event UcAddEventHandler ButtonClicked;
        public bool Txt4Visible { get; set; }

        public UcAdd()
        {            
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ButtonClicked(sender, new UcAddEventArgs(txt1.Text, txt2.Text, txt3.Text, txt4.Text));
        }

        public void InitializePass(string[] args) 
        {
            label1.Text = args[0];
            label2.Text = args[1];
            label3.Text = args[2];
            label4.Visible = Txt4Visible;
            txt4.Visible = Txt4Visible;
            if (Txt4Visible)
            {
                label4.Text = args[3];
            }
        }
    }
}