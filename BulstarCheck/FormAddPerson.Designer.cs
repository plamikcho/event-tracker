namespace BulstarCheck
{
    partial class FormAddPerson
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddPerson));
            this.ucAdd1 = new BulstarCheck.UcAdd();
            this.SuspendLayout();
            // 
            // ucAdd1
            // 
            this.ucAdd1.ButtonName = null;
            this.ucAdd1.Location = new System.Drawing.Point(12, 8);
            this.ucAdd1.Name = "ucAdd1";
            this.ucAdd1.Size = new System.Drawing.Size(352, 142);
            this.ucAdd1.TabIndex = 0;
            this.ucAdd1.Txt4Visible = false;
            // 
            // FormAddPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 199);
            this.Controls.Add(this.ucAdd1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddPerson";
            this.Text = "Добавяне на нов участник";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAddPerson_FormClosed);
            this.Load += new System.EventHandler(this.FormAddPerson_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcAdd ucAdd1;
    }
}