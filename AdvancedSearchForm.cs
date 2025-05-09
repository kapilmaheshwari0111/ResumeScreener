using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SQLite;

namespace ResumeScrenner
{
    public partial class AdvancedSearchForm : Form
    {
        private AdvancedSearch _advancedSearch;
        private Form1 _parentForm;

        public AdvancedSearchForm(SQLiteConnection connection, Form1 parentForm)
        {
            InitializeComponent();
            _advancedSearch = new AdvancedSearch(connection);
            _parentForm = parentForm;
            InitializeStatusComboBox();
        }

        private void InitializeStatusComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Accepted");
            cmbStatus.Items.Add("Rejected");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> filters = new Dictionary<string, object>();

            // Add filters only if they have values
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                filters.Add("Name", txtName.Text);

            if (!string.IsNullOrWhiteSpace(numExperienceMin.Text) && int.TryParse(numExperienceMin.Text, out int expMin))
                filters.Add("ExperienceMin", expMin);

            if (!string.IsNullOrWhiteSpace(numExperienceMax.Text) && int.TryParse(numExperienceMax.Text, out int expMax))
                filters.Add("ExperienceMax", expMax);

            if (!string.IsNullOrWhiteSpace(numScoreMin.Text) && int.TryParse(numScoreMin.Text, out int scoreMin))
                filters.Add("ScoreMin", scoreMin);

            if (!string.IsNullOrWhiteSpace(numScoreMax.Text) && int.TryParse(numScoreMax.Text, out int scoreMax))
                filters.Add("ScoreMax", scoreMax);

            if (!string.IsNullOrWhiteSpace(txtSkills.Text))
                filters.Add("Skills", txtSkills.Text);

            if (!string.IsNullOrWhiteSpace(txtCourse.Text))
                filters.Add("Course", txtCourse.Text);

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                filters.Add("Email", txtEmail.Text);

            if (!string.IsNullOrWhiteSpace(cmbStatus.Text))
                filters.Add("Status", cmbStatus.Text);

            if (filters.Count == 0)
            {
                MessageBox.Show("Please enter at least one search filter.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable results = _advancedSearch.Search(filters);
            _parentForm.UpdateDataGridView(results);
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            numExperienceMin.Clear();
            numExperienceMax.Clear();
            numScoreMin.Clear();
            numScoreMax.Clear();
            txtSkills.Clear();
            txtCourse.Clear();
            txtEmail.Clear();
            cmbStatus.SelectedIndex = -1;
        }
    }
}