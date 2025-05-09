using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ResumeScrenner
{
    public partial class ChangePasswordForm : Form
    {
        private UserAuthentication _auth;
        private string _username;

        public ChangePasswordForm(SQLiteConnection conn, string username)
        {
            InitializeComponent();
            _auth = new UserAuthentication(conn);
            _username = username;
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(currentPassword) ||
                string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Password Too Short", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
            }

            // Attempt to change password
            if (_auth.ChangePassword(_username, currentPassword, newPassword))
            {
                Logger.Log(LogLevel.Info, $"Password changed for user: {_username}");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Current password is incorrect.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPassword.Clear();
                txtCurrentPassword.Focus();
                Logger.Log(LogLevel.Warning, $"Failed password change attempt for user: {_username}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}