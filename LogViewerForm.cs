using System;
using System.Windows.Forms;

namespace ResumeScrenner
{
    public partial class LogViewerForm : Form
    {
        public LogViewerForm(string[] logs)
        {
            InitializeComponent();
            LoadLogs(logs);
        }

        private void LoadLogs(string[] logs)
        {
            txtLogs.Clear();
            foreach (string log in logs)
            {
                txtLogs.AppendText(log + Environment.NewLine);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            string[] logs = Logger.GetRecentLogs();
            LoadLogs(logs);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all logs?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Logger.ClearLogs();
                txtLogs.Clear();
                txtLogs.AppendText("Logs have been cleared." + Environment.NewLine);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLevel = cmbLogLevel.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedLevel) || selectedLevel == "All")
            {
                string[] logs = Logger.GetRecentLogs();
                LoadLogs(logs);
                return;
            }

            // Filter logs by selected level
            string[] allLogs = Logger.GetRecentLogs();
            System.Collections.Generic.List<string> filteredLogs = new System.Collections.Generic.List<string>();

            foreach (string log in allLogs)
            {
                if (log.Contains($"[{selectedLevel}]"))
                {
                    filteredLogs.Add(log);
                }
            }

            LoadLogs(filteredLogs.ToArray());
        }
    }
}