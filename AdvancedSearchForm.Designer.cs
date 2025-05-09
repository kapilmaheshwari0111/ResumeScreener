namespace ResumeScrenner
{
    partial class AdvancedSearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblExperience;
        private System.Windows.Forms.TextBox numExperienceMin;
        private System.Windows.Forms.Label lblExperienceRange;
        private System.Windows.Forms.TextBox numExperienceMax;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.TextBox numScoreMin;
        private System.Windows.Forms.Label lblScoreRange;
        private System.Windows.Forms.TextBox numScoreMax;
        private System.Windows.Forms.Label lblSkills;
        private System.Windows.Forms.TextBox txtSkills;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.TextBox txtCourse;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;

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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblExperience = new System.Windows.Forms.Label();
            this.numExperienceMin = new System.Windows.Forms.TextBox();
            this.lblExperienceRange = new System.Windows.Forms.Label();
            this.numExperienceMax = new System.Windows.Forms.TextBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.numScoreMin = new System.Windows.Forms.TextBox();
            this.lblScoreRange = new System.Windows.Forms.Label();
            this.numScoreMax = new System.Windows.Forms.TextBox();
            this.lblSkills = new System.Windows.Forms.Label();
            this.txtSkills = new System.Windows.Forms.TextBox();
            this.lblCourse = new System.Windows.Forms.Label();
            this.txtCourse = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Name
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 20);
            this.lblName.Text = "Name:";
            this.txtName.Location = new System.Drawing.Point(120, 20);
            this.txtName.Size = new System.Drawing.Size(200, 20);

            // Experience
            this.lblExperience.AutoSize = true;
            this.lblExperience.Location = new System.Drawing.Point(12, 50);
            this.lblExperience.Text = "Experience (Years):";
            this.numExperienceMin.Location = new System.Drawing.Point(120, 50);
            this.numExperienceMin.Size = new System.Drawing.Size(60, 20);
            this.lblExperienceRange.AutoSize = true;
            this.lblExperienceRange.Location = new System.Drawing.Point(190, 50);
            this.lblExperienceRange.Text = "to";
            this.numExperienceMax.Location = new System.Drawing.Point(210, 50);
            this.numExperienceMax.Size = new System.Drawing.Size(60, 20);

            // Score
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(12, 80);
            this.lblScore.Text = "Score:";
            this.numScoreMin.Location = new System.Drawing.Point(120, 80);
            this.numScoreMin.Size = new System.Drawing.Size(60, 20);
            this.lblScoreRange.AutoSize = true;
            this.lblScoreRange.Location = new System.Drawing.Point(190, 80);
            this.lblScoreRange.Text = "to";
            this.numScoreMax.Location = new System.Drawing.Point(210, 80);
            this.numScoreMax.Size = new System.Drawing.Size(60, 20);

            // Skills
            this.lblSkills.AutoSize = true;
            this.lblSkills.Location = new System.Drawing.Point(12, 110);
            this.lblSkills.Text = "Skills:";
            this.txtSkills.Location = new System.Drawing.Point(120, 110);
            this.txtSkills.Size = new System.Drawing.Size(200, 20);

            // Course
            this.lblCourse.AutoSize = true;
            this.lblCourse.Location = new System.Drawing.Point(12, 140);
            this.lblCourse.Text = "Course:";
            this.txtCourse.Location = new System.Drawing.Point(120, 140);
            this.txtCourse.Size = new System.Drawing.Size(200, 20);

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 170);
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(120, 170);
            this.txtEmail.Size = new System.Drawing.Size(200, 20);

            // Status
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 200);
            this.lblStatus.Text = "Status:";
            this.cmbStatus.Location = new System.Drawing.Point(120, 200);
            this.cmbStatus.Size = new System.Drawing.Size(200, 20);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Buttons
            this.btnSearch.Location = new System.Drawing.Point(60, 240);
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            this.btnCancel.Location = new System.Drawing.Point(150, 240);
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            this.btnClear.Location = new System.Drawing.Point(240, 240);
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(350, 290);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblExperience);
            this.Controls.Add(this.numExperienceMin);
            this.Controls.Add(this.lblExperienceRange);
            this.Controls.Add(this.numExperienceMax);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.numScoreMin);
            this.Controls.Add(this.lblScoreRange);
            this.Controls.Add(this.numScoreMax);
            this.Controls.Add(this.lblSkills);
            this.Controls.Add(this.txtSkills);
            this.Controls.Add(this.lblCourse);
            this.Controls.Add(this.txtCourse);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Search";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}