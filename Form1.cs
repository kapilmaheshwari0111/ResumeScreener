using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace ResumeScrenner
{
    public partial class Form1 : Form
    {
        
        private DataTable resumeDataTable;
        private string dbFile = "resumes.db";
        private EmailService emailService;
        private UserAuthentication userAuth;
        private string currentUser;
        private string currentUserRole;
        private System.Data.SQLite.SQLiteConnection conn;
        public Form1()
        {
            InitializeComponent();

            // Initialize localization before any UI is shown
            LocalizationManager.Initialize();

            InitializeDatabase();
            InitializeUI();
            InitializeServices();

            // Show login form
            ShowLoginForm();
        }

        private void InitializeDatabase()
        {
            try
            {
                if (!File.Exists(dbFile))
                {
                    SQLiteConnection.CreateFile(dbFile);
                    Logger.Log(LogLevel.Info, "Database file created");
                }

                conn = new SQLiteConnection($"Data Source={dbFile};Version=3;");
                conn.Open();
                Logger.Log(LogLevel.Info, "Database connection opened");

                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Resumes (
                                          ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                          Name TEXT,
                                          Experience INTEGER,
                                          Skill1 TEXT,
                                          Skill2 TEXT,
                                          Skill3 TEXT,
                                          Course TEXT,
                                          Score INTEGER,
                                          Email TEXT,
                                          Status TEXT DEFAULT 'Pending')";

                SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn);
                cmd.ExecuteNonQuery();
                Logger.Log(LogLevel.Info, "Resumes table initialized");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Database initialization error: {ex.Message}");
                MessageBox.Show($"Error initializing database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeUI()
        {
            try
            {
                // Set form text and control texts based on current language
                this.Text = LocalizationManager.GetString("MainForm_Title");
                btnLoadResumes.Text = LocalizationManager.GetString("Button_LoadResumes");
                btnSearch.Text = LocalizationManager.GetString("Button_Search");
                btnRankCandidates.Text = LocalizationManager.GetString("Button_RankCandidates");
                btnAdvancedSearch.Text = LocalizationManager.GetString("Button_AdvancedSearch");
                btnExportCsv.Text = LocalizationManager.GetString("Button_ExportCSV");
                btnSort.Text = LocalizationManager.GetString("Button_Sort");

                // Initialize sorting options
                cmbSortOptions.Items.Clear();
                cmbSortOptions.Items.Add("Experience (Years)");
                cmbSortOptions.Items.Add("Name");
                cmbSortOptions.Items.Add("Score");

                // Initialize the menu
                InitializeMenu();

                // Enable/disable controls based on authentication
                UpdateUIBasedOnAuthentication(false);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"UI initialization error: {ex.Message}");
            }
        }

        private void InitializeMenu()
        {
            // Create main menu
            mainMenu = new MenuStrip();

            // File menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem(LocalizationManager.GetString("Menu_File"));
            fileMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_File_Export"), null, new EventHandler(BtnExportCsv_Click));
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_File_Exit"), null, (s, e) => this.Close());

            // Tools menu
            ToolStripMenuItem toolsMenu = new ToolStripMenuItem(LocalizationManager.GetString("Menu_Tools"));
            toolsMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_Tools_AdvancedSearch"), null, new EventHandler(BtnAdvancedSearch_Click));
            toolsMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_Tools_ViewLogs"), null, new EventHandler(ViewLogs_Click));

            // User menu
            ToolStripMenuItem userMenu = new ToolStripMenuItem(LocalizationManager.GetString("Menu_User"));
            userMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_User_ChangePassword"), null, new EventHandler(ChangePassword_Click));
            userMenu.DropDownItems.Add(new ToolStripSeparator());
            userMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_User_Logout"), null, new EventHandler(Logout_Click));

            // Language menu
            languageMenu = new ToolStripMenuItem(LocalizationManager.GetString("Menu_Language"));
            foreach (string language in LocalizationManager.GetAvailableLanguages())
            {
                ToolStripMenuItem languageItem = new ToolStripMenuItem(language);
                languageItem.Tag = language;
                languageItem.Click += new EventHandler(LanguageItem_Click);
                languageMenu.DropDownItems.Add(languageItem);
            }
            languageMenu.DropDownItems.Add(new ToolStripSeparator());
            languageMenu.DropDownItems.Add("More Languages...", null, new EventHandler(MoreLanguages_Click));

            // Help menu
            ToolStripMenuItem helpMenu = new ToolStripMenuItem(LocalizationManager.GetString("Menu_Help"));
            helpMenu.DropDownItems.Add(LocalizationManager.GetString("Menu_Help_About"), null, new EventHandler(About_Click));

            // Add all menus to the menu strip
            mainMenu.Items.Add(fileMenu);
            mainMenu.Items.Add(toolsMenu);
            mainMenu.Items.Add(userMenu);
            mainMenu.Items.Add(languageMenu);
            mainMenu.Items.Add(helpMenu);

            // Set the menu strip
            this.Controls.Add(mainMenu);
            this.MainMenuStrip = mainMenu;
        }

        private void InitializeServices()
        {
            try
            {                // Initialize email service (using placeholder values, these should be set in a configuration file)
                emailService = new EmailService(
                    "smtp.example.com",
                    587,
                    "username@example.com",
                    "password",
                    "noreply@yourcompany.com",
                    "Resume Screener"
                );

                // Initialize user authentication
                userAuth = new UserAuthentication(conn);

                Logger.Log(LogLevel.Info, "Services initialized");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Service initialization error: {ex.Message}");
            }
        }

        private void ShowLoginForm()
        {
            using (LoginForm loginForm = new LoginForm(conn))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    currentUser = loginForm.CurrentUser;
                    currentUserRole = loginForm.CurrentUserRole;
                    UpdateUIBasedOnAuthentication(true);
                    LoadResumesFromDatabase();
                    Logger.Log(LogLevel.Info, $"User logged in: {currentUser} with role {currentUserRole}");
                }
                else
                {
                    Logger.Log(LogLevel.Info, "Login cancelled by user");
                    Application.Exit();
                }
            }
        }

        private void UpdateUIBasedOnAuthentication(bool isAuthenticated)
        {
            // Enable/disable controls based on authentication
            btnLoadResumes.Enabled = isAuthenticated;
            btnSearch.Enabled = isAuthenticated;
            btnRankCandidates.Enabled = isAuthenticated;
            btnAdvancedSearch.Enabled = isAuthenticated;
            btnExportCsv.Enabled = isAuthenticated;
            btnSort.Enabled = isAuthenticated;
            cmbSortOptions.Enabled = isAuthenticated;
            txtSearch.Enabled = isAuthenticated;
            dataGridViewResumes.Enabled = isAuthenticated;

            // Update menu items based on authentication
            if (mainMenu != null && mainMenu.Items.Count > 0)
            {
                // File menu (1st item)
                if (mainMenu.Items[0] is ToolStripMenuItem fileMenu)
                {
                    fileMenu.Enabled = isAuthenticated;
                }

                // Tools menu (2nd item)
                if (mainMenu.Items[1] is ToolStripMenuItem toolsMenu)
                {
                    toolsMenu.Enabled = isAuthenticated;
                }

                // User menu (3rd item)
                if (mainMenu.Items[2] is ToolStripMenuItem userMenu)
                {
                    userMenu.Enabled = isAuthenticated;
                }
            }

            // Update status strip
            if (isAuthenticated)
            {
                statusStripLabel.Text = $"Logged in as: {currentUser} ({currentUserRole})";
            }
            else
            {
                statusStripLabel.Text = "Not logged in";
            }
        }

        private void LoadResumesFromDatabase()
        {
            try
            {
                string query = "SELECT * FROM Resumes";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                resumeDataTable = new DataTable();
                adapter.Fill(resumeDataTable);
                dataGridViewResumes.DataSource = resumeDataTable;
                AddActionButtons();

                Logger.Log(LogLevel.Info, $"Loaded {resumeDataTable.Rows.Count} resumes from database");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error loading resumes from database: {ex.Message}");
                MessageBox.Show($"Error loading resumes: {ex.Message}", LocalizationManager.GetString("Dialog_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddActionButtons()
        {

            if (dataGridViewResumes.Columns["Accept"] == null)
            {
                DataGridViewButtonColumn acceptBtn = new DataGridViewButtonColumn
                {
                    Name = "Accept",
                    HeaderText = LocalizationManager.GetString("Button_Accept"),
                    Text = LocalizationManager.GetString("Button_Accept"),
                    UseColumnTextForButtonValue = true
                };
                dataGridViewResumes.Columns.Add(acceptBtn);
            }

            if (dataGridViewResumes.Columns["Reject"] == null)
            {
                DataGridViewButtonColumn rejectBtn = new DataGridViewButtonColumn
                {
                    Name = "Reject",
                    HeaderText = LocalizationManager.GetString("Button_Reject"),
                    Text = LocalizationManager.GetString("Button_Reject"),
                    UseColumnTextForButtonValue = true
                };
                dataGridViewResumes.Columns.Add(rejectBtn);
            }
        }

        public void UpdateDataGridView(DataTable newData)
        {
            dataGridViewResumes.DataSource = newData;
            AddActionButtons();
        }

        private void BtnLoadResumes_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = LocalizationManager.GetString("Filter_CSVFiles");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LoadCsvData(ofd.FileName);
                        LoadResumesFromDatabase();
                        MessageBox.Show(
                            LocalizationManager.GetString("Message_LoadSuccess"),
                            LocalizationManager.GetString("Dialog_OperationSuccess"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(LogLevel.Error, $"Error loading CSV data: {ex.Message}");
                        MessageBox.Show(
                            $"Error loading CSV data: {ex.Message}",
                            LocalizationManager.GetString("Dialog_Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        private void LoadCsvData(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    reader.ReadLine(); // Skip header
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(',');
                        string query = "INSERT INTO Resumes (Name, Experience, Skill1, Skill2, Skill3, Course, Score, Email, Status) VALUES (@Name, @Experience, @Skill1, @Skill2, @Skill3, @Course, @Score, @Email, 'Pending')";

                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", fields[0]);
                        cmd.Parameters.AddWithValue("@Experience", int.Parse(fields[1]));
                        cmd.Parameters.AddWithValue("@Skill1", fields[2]);
                        cmd.Parameters.AddWithValue("@Skill2", fields[3]);
                        cmd.Parameters.AddWithValue("@Skill3", fields[4]);
                        cmd.Parameters.AddWithValue("@Course", fields[5]);
                        cmd.Parameters.AddWithValue("@Score", int.Parse(fields[6]));
                        cmd.Parameters.AddWithValue("@Email", fields[7]);
                        cmd.ExecuteNonQuery();
                    }
                }

                Logger.Log(LogLevel.Info, $"CSV data loaded from {filePath}");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error parsing CSV: {ex.Message}");
                throw;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadResumesFromDatabase();
                return;
            }

            try
            {
                string query = "SELECT * FROM Resumes WHERE LOWER(Name) LIKE @Keyword OR LOWER(Skill1) LIKE @Keyword OR LOWER(Skill2) LIKE @Keyword OR LOWER(Skill3) LIKE @Keyword";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                DataTable filteredData = new DataTable();
                adapter.Fill(filteredData);

                if (filteredData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        LocalizationManager.GetString("Message_NoSearchResults"),
                        LocalizationManager.GetString("Dialog_Info"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                dataGridViewResumes.DataSource = filteredData;
                AddActionButtons();

                Logger.Log(LogLevel.Info, $"Search performed for '{keyword}', found {filteredData.Rows.Count} results");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error searching: {ex.Message}");
                MessageBox.Show(
                    $"Error during search: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnRankCandidates_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Resumes ORDER BY Score DESC";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                DataTable rankedData = new DataTable();
                adapter.Fill(rankedData);
                dataGridViewResumes.DataSource = rankedData;
                AddActionButtons();

                Logger.Log(LogLevel.Info, "Candidates ranked by score");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error ranking candidates: {ex.Message}");
                MessageBox.Show(
                    $"Error ranking candidates: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            string sortBy = cmbSortOptions.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(sortBy))
            {
                MessageBox.Show(
                    "Select a sort option!",
                    LocalizationManager.GetString("Dialog_Warning"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                string query;
                switch (sortBy)
                {
                    case "Experience (Years)":
                        query = "SELECT * FROM Resumes ORDER BY Experience DESC";
                        break;
                    case "Name":
                        query = "SELECT * FROM Resumes ORDER BY Name";
                        break;
                    case "Score":
                        query = "SELECT * FROM Resumes ORDER BY Score DESC";
                        break;
                    default:
                        query = "SELECT * FROM Resumes";
                        break;
                }

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                DataTable sortedData = new DataTable();
                adapter.Fill(sortedData);
                dataGridViewResumes.DataSource = sortedData;
                AddActionButtons();

                Logger.Log(LogLevel.Info, $"Resumes sorted by {sortBy}");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error sorting data: {ex.Message}");
                MessageBox.Show(
                    $"Error sorting data: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void DataGridViewResumes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dataGridViewResumes.Columns[e.ColumnIndex].Name;
            if (colName != "Accept" && colName != "Reject") return;

            try
            {
                int id = Convert.ToInt32(dataGridViewResumes.Rows[e.RowIndex].Cells["ID"].Value);
                string candidateName = dataGridViewResumes.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                string candidateEmail = dataGridViewResumes.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                string status = colName == "Accept" ? "Accepted" : "Rejected";

                UpdateCandidateStatus(id, status);

                // Send email notification
                bool emailSent = emailService.SendCandidateStatusEmail(candidateName, candidateEmail, status);

                string message = status == "Accepted"
                    ? LocalizationManager.GetString("Message_CandidateAccepted")
                    : LocalizationManager.GetString("Message_CandidateRejected");

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

                LoadResumesFromDatabase();

                Logger.Log(LogLevel.Info, $"Candidate {candidateName} ({id}) status updated to {status}");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error updating candidate status: {ex.Message}");
                MessageBox.Show(
                    $"Error updating candidate status: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void UpdateCandidateStatus(int id, string status)
        {
            string query = "UPDATE Resumes SET Status = @Status WHERE ID = @ID";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
        }

        private void DataGridViewResumes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int id = Convert.ToInt32(dataGridViewResumes.Rows[e.RowIndex].Cells["ID"].Value);
                ResumeDetailsForm detailsForm = new ResumeDetailsForm(id, conn);
                detailsForm.ShowDialog();

                Logger.Log(LogLevel.Info, $"Viewing resume details for ID {id}");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error opening resume details: {ex.Message}");
                MessageBox.Show(
                    $"Error opening resume details: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

     
                    private void BtnAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                AdvancedSearchForm searchForm = new AdvancedSearchForm(conn, this);
                searchForm.ShowDialog();
                Logger.Log(LogLevel.Info, "Advanced search form opened");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error opening advanced search: {ex.Message}");
                MessageBox.Show(
                    $"Error opening advanced search: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current data displayed in the grid
                DataTable dataToExport = (DataTable)dataGridViewResumes.DataSource;

                if (dataToExport == null || dataToExport.Rows.Count == 0)
                {
                    MessageBox.Show(
                        LocalizationManager.GetString("Message_NoResumesSelected"),
                        LocalizationManager.GetString("Dialog_Warning"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = LocalizationManager.GetString("Filter_CSVFiles");
                    sfd.FileName = $"ResumeExport_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        bool success = CsvExporter.ExportDataTableToCsv(dataToExport, sfd.FileName);

                        if (success)
                        {
                            MessageBox.Show(
                                LocalizationManager.GetString("Message_ExportSuccess"),
                                LocalizationManager.GetString("Dialog_OperationSuccess"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            Logger.Log(LogLevel.Info, $"Exported {dataToExport.Rows.Count} resumes to {sfd.FileName}");
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to export data",
                                LocalizationManager.GetString("Dialog_OperationFailed"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error exporting to CSV: {ex.Message}");
                MessageBox.Show(
                    $"Error exporting to CSV: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ViewLogs_Click(object sender, EventArgs e)
        {
            try
            {
                string[] logs = Logger.GetRecentLogs();

                LogViewerForm logViewer = new LogViewerForm(logs);
                logViewer.ShowDialog();

                Logger.Log(LogLevel.Info, "Log viewer opened");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error viewing logs: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePasswordForm passwordForm = new ChangePasswordForm(conn, currentUser);
                if (passwordForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(
                        "Password changed successfully",
                        LocalizationManager.GetString("Dialog_OperationSuccess"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                Logger.Log(LogLevel.Info, "Password change form opened");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error changing password: {ex.Message}");
                MessageBox.Show(
                    $"Error changing password: {ex.Message}",
                    LocalizationManager.GetString("Dialog_Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to logout?",
                    LocalizationManager.GetString("Dialog_Confirmation"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Clear current user info
                    currentUser = null;
                    currentUserRole = null;

                    // Clear data grid
                    dataGridViewResumes.DataSource = null;

                    // Update UI
                    UpdateUIBasedOnAuthentication(false);

                    // Show login form
                    ShowLoginForm();

                    Logger.Log(LogLevel.Info, "User logged out");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error during logout: {ex.Message}");
            }
        }

        private void LanguageItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is ToolStripMenuItem menuItem && menuItem.Tag is string languageCode)
                {
                    LocalizationManager.SetLanguage(languageCode);

                    // Refresh the UI with the new language
                    InitializeUI();

                    Logger.Log(LogLevel.Info, $"Language changed to {languageCode}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error changing language: {ex.Message}");
            }
        }

        private void MoreLanguages_Click(object sender, EventArgs e)
        {
            try
            {
                LanguageSelectorForm langForm = new LanguageSelectorForm();
                if (langForm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(langForm.SelectedLanguage))
                {
                    LocalizationManager.SetLanguage(langForm.SelectedLanguage);

                    // Refresh the UI with the new language
                    InitializeUI();

                    Logger.Log(LogLevel.Info, $"Language changed to {langForm.SelectedLanguage}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error selecting language: {ex.Message}");
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            try
            {
                string aboutMessage =
                    "Resume Screener Application\n" +
                    "Version 1.0\n\n" +
                    "This application helps HR professionals screen and manage resumes.\n\n" +
                    "© 2025 Your Company Name";

                MessageBox.Show(
                    aboutMessage,
                    "About Resume Screener",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Logger.Log(LogLevel.Info, "About dialog displayed");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error showing about dialog: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // Close database connection
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                    Logger.Log(LogLevel.Info, "Database connection closed");
                }

                Logger.Log(LogLevel.Info, "Application closed");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error during application closing: {ex.Message}");
            }

            base.OnFormClosing(e);
        }
    }
}