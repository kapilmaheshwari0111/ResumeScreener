using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace ResumeScrenner
{
    public class UserAuthentication
    {
        private readonly SQLiteConnection _conn;

        public UserAuthentication(SQLiteConnection connection)
        {
            _conn = connection;
            InitializeUserTable();
        }

        private void InitializeUserTable()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Users (
                                      ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                      Username TEXT UNIQUE,
                                      PasswordHash TEXT,
                                      Salt TEXT,
                                      FullName TEXT,
                                      Email TEXT,
                                      Role TEXT,
                                      LastLogin TEXT)";

            SQLiteCommand cmd = new SQLiteCommand(createTableQuery, _conn);
            cmd.ExecuteNonQuery();

            // Check if admin user exists, if not create it
            string checkAdminQuery = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
            SQLiteCommand checkCmd = new SQLiteCommand(checkAdminQuery, _conn);
            int adminCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (adminCount == 0)
            {
                CreateUser("admin", "admin123", "Administrator", "admin@example.com", "Admin");
                Logger.Log(LogLevel.Info, "Admin user created with default credentials");
            }
        }

        public bool CreateUser(string username, string password, string fullName, string email, string role)
        {
            try
            {
                string salt = GenerateSalt();
                string passwordHash = HashPassword(password, salt);

                string query = @"INSERT INTO Users (Username, PasswordHash, Salt, FullName, Email, Role, LastLogin) 
                               VALUES (@Username, @PasswordHash, @Salt, @FullName, @Email, @Role, @LastLogin)";

                SQLiteCommand cmd = new SQLiteCommand(query, _conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@Salt", salt);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();
                Logger.Log(LogLevel.Info, $"User created: {username}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error creating user: {ex.Message}");
                return false;
            }
        }

        public bool ValidateUser(string username, string password)
        {
            string query = "SELECT PasswordHash, Salt FROM Users WHERE Username = @Username";
            SQLiteCommand cmd = new SQLiteCommand(query, _conn);
            cmd.Parameters.AddWithValue("@Username", username);

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string storedHash = reader["PasswordHash"].ToString();
                    string salt = reader["Salt"].ToString();
                    string computedHash = HashPassword(password, salt);

                    if (storedHash == computedHash)
                    {
                        // Update last login time
                        string updateQuery = "UPDATE Users SET LastLogin = @LastLogin WHERE Username = @Username";
                        SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, _conn);
                        updateCmd.Parameters.AddWithValue("@LastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        updateCmd.Parameters.AddWithValue("@Username", username);
                        updateCmd.ExecuteNonQuery();

                        Logger.Log(LogLevel.Info, $"User logged in: {username}");
                        return true;
                    }
                }
            }

            Logger.Log(LogLevel.Warning, $"Failed login attempt for user: {username}");
            return false;
        }

        public string GetUserRole(string username)
        {
            string query = "SELECT Role FROM Users WHERE Username = @Username";
            SQLiteCommand cmd = new SQLiteCommand(query, _conn);
            cmd.Parameters.AddWithValue("@Username", username);

            object result = cmd.ExecuteScalar();
            return result != null ? result.ToString() : string.Empty;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] combined = Encoding.UTF8.GetBytes(password + salt);
                byte[] hash = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hash);
            }
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword))
                return false;

            try
            {
                string salt = GenerateSalt();
                string passwordHash = HashPassword(newPassword, salt);

                string query = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt WHERE Username = @Username";
                SQLiteCommand cmd = new SQLiteCommand(query, _conn);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@Salt", salt);
                cmd.Parameters.AddWithValue("@Username", username);

                cmd.ExecuteNonQuery();
                Logger.Log(LogLevel.Info, $"Password changed for user: {username}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error changing password: {ex.Message}");
                return false;
            }
        }
    }
}