namespace BulstarCheck
{
    partial class FormAddEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddEvent));
            this.ucAdd1 = new BulstarCheck.UcAdd();
            this.SuspendLayout();
            // 
            // ucAdd1
            // 
            this.ucAdd1.ButtonName = null;
            this.ucAdd1.Location = new System.Drawing.Point(12, 38);
            this.ucAdd1.Name = "ucAdd1";
            this.ucAdd1.Size = new System.Drawing.Size(375, 155);
            this.ucAdd1.TabIndex = 0;
            this.ucAdd1.Txt4Visible = false;
            // 
            // FormAddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 261);
            this.Controls.Add(this.ucAdd1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddEvent";
            this.Text = "Добавяне на събитие";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAddEvent_FormClosed);
            this.Load += new System.EventHandler(this.FormAddEvent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcAdd ucAdd1;
    }
}