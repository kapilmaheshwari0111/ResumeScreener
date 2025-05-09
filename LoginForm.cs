using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ResumeScrenner
{
    public partial class LoginForm : Form
    {
        private UserAuthentication _auth;
        private SQLiteConnection _conn;
        public bool IsAuthenticated { get; private set; }
        public string CurrentUser { get; private set; }
        public string CurrentUserRole { get; private set; }

        public LoginForm(SQLiteConnection conn)
        {
            InitializeComponent();
            _conn = conn;
            _auth = new UserAuthentication(_conn);
            IsAuthenticated = false;
            CurrentUser = string.Empty;
            CurrentUserRole = string.Empty;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_auth.ValidateUser(username, password))
            {
                IsAuthenticated = true;
                CurrentUser = username;
                CurrentUserRole = _auth.GetUserRole(username);
                Logger.Log(LogLevel.Info, $"User {username} logged in successfully");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                Logger.Log(LogLevel.Warning, $"Failed login attempt for user {username}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }
    }
}