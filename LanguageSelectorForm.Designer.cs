namespace ResumeScrenner
{
    partial class LanguageSelectorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cmbLanguages;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmbLanguages = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Language Label
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(20, 30);
            this.lblLanguage.Size = new System.Drawing.Size(80, 13);
            this.lblLanguage.Text = "Select Language:";

            // Languages ComboBox
            this.cmbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguages.FormattingEnabled = true;
            this.cmbLanguages.Location = new System.Drawing.Point(120, 30);
            this.cmbLanguages.Size = new System.Drawing.Size(180, 21);

            // OK Button
            this.btnOk.Location = new System.Drawing.Point(80, 80);
            this.btnOk.Size = new System.Drawing.Size(80, 25);
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);

            // Cancel Button
            this.btnCancel.Location = new System.Drawing.Point(180, 80);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // Form
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(320, 120);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.cmbLanguages);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LanguageSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Language Selection";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}