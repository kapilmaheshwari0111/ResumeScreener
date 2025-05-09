using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace ResumeScrenner
{
    public partial class ResumeDetailsForm : Form
    {
        private SQLiteConnection _conn;
        private int _resumeId;
        private string _currentStatus;
        private string _candidateName;
        private string _candidateEmail;

        public ResumeDetailsForm(int id, SQLiteConnection connection)
        {
            InitializeComponent();
            _conn = connection;
            _resumeId = id;
            LoadResumeDetails();
        }

        private void LoadResumeDetails()
        {
            try
            {
                string query = "SELECT * FROM Resumes WHERE ID = @ID";
                SQLiteCommand cmd = new SQLiteCommand(query, _conn);
                cmd.Parameters.AddWithValue("@ID", _resumeId);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _candidateName = reader["Name"].ToString();
                        lblName.Text = $"Name: {_candidateName}";

                        lblExperience.Text = $"Experience: {reader["Experience"]} years";
                        lblSkill1.Text = $"Skill 1: {reader["Skill1"]}";
                        lblSkill2.Text = $"Skill 2: {reader["Skill2"]}";
                        lblSkill3.Text = $"Skill 3: {reader["Skill3"]}";
                        lblCourse.Text = $"Course: {reader["Course"]}";
                        lblScore.Text = $"Score: {reader["Score"]}";

                        _candidateEmail = reader["Email"].ToString();
                        lblEmail.Text = $"Email: {_candidateEmail}";

                        _currentStatus = reader["Status"].ToString();
                        lblStatus.Text = $"Status: {_currentStatus}";

                        // Set status color
                        switch (_currentStatus)
                        {
                            case "Accepted":
                                lblStatus.ForeColor = Color.Green;
                                break;
                            case "Rejected":
                                lblStatus.ForeColor = Color.Red;
                                break;
                            default:
                                lblStatus.ForeColor = Color.Blue;
                                break;
                        }

                        // Enable/disable buttons based on current status
                        btnAccept.Enabled = _currentStatus != "Accepted";
                        btnReject.Enabled = _currentStatus != "Rejected";
                    }
                    else
                    {
                        MessageBox.Show("Resume not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error loading resume details: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentStatus == "Accepted")
                    return;

                UpdateStatus("Accepted");

                // Send email notification
                EmailService emailService = new EmailService(
     "smtp.example.com",
     587,
     "username@example.com",
     "password",
     "noreply@yourcompany.com",
     "Resume Screener"
 );

                bool emailSent = emailService.SendCandidateStatusEmail(_candidateName, _candidateEmail, "Accepted");

                string message = LocalizationManager.GetString("Message_CandidateAccepted");
                if (!emailSent)
                {
                    message += " However, the email notification could not be sent.";
                }

                MessageBox.Show(
                    message,
                    LocalizationManager.GetString("Dialog_Info"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Refresh the form to show updated status
                LoadResumeDetails();

                Logger.Log(LogLevel.Info, $"Resume ID {_resumeId} ({_candidateName}) accepted");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error accepting resume: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentStatus == "Rejected")
                    return;

                UpdateStatus("Rejected");

                // Send email notification
                EmailService emailService = new EmailService(
                    "smtp.example.com",
                    587,
                    "username@example.com",
                    "password",
                    "noreply@yourcompany.com",
                    "Resume Screener"
                );

                bool emailSent = emailService.SendCandidateStatusEmail(_candidateName, _candidateEmail, "Rejected");

                string message = LocalizationManager.GetString("Message_CandidateRejected");
                if (!emailSent)
                {
                    message += " However, the email notification could not be sent.";
                }

                MessageBox.Show(
                    message,
                    LocalizationManager.GetString("Dialog_Info"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Refresh the form to show updated status
                LoadResumeDetails();

                Logger.Log(LogLevel.Info, $"Resume ID {_resumeId} ({_candidateName}) rejected");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error rejecting resume: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatus(string status)
        {
            string query = "UPDATE Resumes SET Status = @Status WHERE ID = @ID";
            SQLiteCommand cmd = new SQLiteCommand(query, _conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@ID", _resumeId);
            cmd.ExecuteNonQuery();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}