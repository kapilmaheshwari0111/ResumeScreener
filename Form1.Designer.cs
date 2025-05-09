using System.Windows.Forms;

namespace ResumeScrenner
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewResumes;
        private System.Windows.Forms.Button btnLoadResumes;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRankCandidates;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbSortOptions;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnAdvancedSearch;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem languageMenu;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblAppTitle;

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
            this.dataGridViewResumes = new System.Windows.Forms.DataGridView();
            this.btnLoadResumes = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRankCandidates = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbSortOptions = new System.Windows.Forms.ComboBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnAdvancedSearch = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResumes)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewResumes
            // 
            this.dataGridViewResumes.AllowUserToAddRows = false;
            this.dataGridViewResumes.AllowUserToDeleteRows = false;
            this.dataGridViewResumes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewResumes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewResumes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResumes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResumes.Location = new System.Drawing.Point(240, 153);
            this.dataGridViewResumes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewResumes.MultiSelect = false;
            this.dataGridViewResumes.Name = "dataGridViewResumes";
            this.dataGridViewResumes.ReadOnly = true;
            this.dataGridViewResumes.RowHeadersWidth = 51;
            this.dataGridViewResumes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResumes.Size = new System.Drawing.Size(1093, 559);
            this.dataGridViewResumes.TabIndex = 0;
            this.dataGridViewResumes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewResumes_CellClick);
            this.dataGridViewResumes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewResumes_CellDoubleClick);
            // 
            // btnLoadResumes
            // 
            this.btnLoadResumes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnLoadResumes.FlatAppearance.BorderSize = 0;
            this.btnLoadResumes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadResumes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadResumes.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLoadResumes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadResumes.Location = new System.Drawing.Point(0, 25);
            this.btnLoadResumes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadResumes.Name = "btnLoadResumes";
            this.btnLoadResumes.Size = new System.Drawing.Size(240, 74);
            this.btnLoadResumes.TabIndex = 0;
            this.btnLoadResumes.Text = "Load Resumes";
            this.btnLoadResumes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadResumes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadResumes.UseVisualStyleBackColor = false;
            this.btnLoadResumes.Click += new System.EventHandler(this.BtnLoadResumes_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(293, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 31);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnRankCandidates
            // 
            this.btnRankCandidates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnRankCandidates.FlatAppearance.BorderSize = 0;
            this.btnRankCandidates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRankCandidates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRankCandidates.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRankCandidates.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRankCandidates.Location = new System.Drawing.Point(0, 98);
            this.btnRankCandidates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRankCandidates.Name = "btnRankCandidates";
            this.btnRankCandidates.Size = new System.Drawing.Size(240, 74);
            this.btnRankCandidates.TabIndex = 1;
            this.btnRankCandidates.Text = "Rank Candidates";
            this.btnRankCandidates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRankCandidates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRankCandidates.UseVisualStyleBackColor = false;
            this.btnRankCandidates.Click += new System.EventHandler(this.BtnRankCandidates_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(13, 12);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(266, 22);
            this.txtSearch.TabIndex = 0;
            // 
            // cmbSortOptions
            // 
            this.cmbSortOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortOptions.FormattingEnabled = true;
            this.cmbSortOptions.Location = new System.Drawing.Point(600, 12);
            this.cmbSortOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSortOptions.Name = "cmbSortOptions";
            this.cmbSortOptions.Size = new System.Drawing.Size(159, 24);
            this.cmbSortOptions.TabIndex = 2;
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSort.FlatAppearance.BorderSize = 0;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSort.ForeColor = System.Drawing.Color.White;
            this.btnSort.Location = new System.Drawing.Point(773, 10);
            this.btnSort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(107, 31);
            this.btnSort.TabIndex = 3;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // btnAdvancedSearch
            // 
            this.btnAdvancedSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnAdvancedSearch.FlatAppearance.BorderSize = 0;
            this.btnAdvancedSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvancedSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvancedSearch.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAdvancedSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdvancedSearch.Location = new System.Drawing.Point(0, 172);
            this.btnAdvancedSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdvancedSearch.Name = "btnAdvancedSearch";
            this.btnAdvancedSearch.Size = new System.Drawing.Size(240, 74);
            this.btnAdvancedSearch.TabIndex = 2;
            this.btnAdvancedSearch.Text = "Advanced Search";
            this.btnAdvancedSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdvancedSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdvancedSearch.UseVisualStyleBackColor = false;
            this.btnAdvancedSearch.Click += new System.EventHandler(this.BtnAdvancedSearch_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnExportCsv.FlatAppearance.BorderSize = 0;
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCsv.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnExportCsv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportCsv.Location = new System.Drawing.Point(0, 246);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(240, 74);
            this.btnExportCsv.TabIndex = 3;
            this.btnExportCsv.Text = "Export to CSV";
            this.btnExportCsv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportCsv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.BtnExportCsv_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.mainMenu.ForeColor = System.Drawing.Color.White;
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Location = new System.Drawing.Point(240, 74);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1093, 30);
            this.mainMenu.TabIndex = 2;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 712);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1333, 26);
            this.statusStrip.TabIndex = 4;
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(50, 20);
            this.statusStripLabel.Text = "Ready";
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.sidePanel.Controls.Add(this.btnLoadResumes);
            this.sidePanel.Controls.Add(this.btnRankCandidates);
            this.sidePanel.Controls.Add(this.btnAdvancedSearch);
            this.sidePanel.Controls.Add(this.btnExportCsv);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 74);
            this.sidePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(240, 638);
            this.sidePanel.TabIndex = 3;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.topPanel.Controls.Add(this.lblAppTitle);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1333, 74);
            this.topPanel.TabIndex = 5;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(16, 18);
            this.lblAppTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(217, 29);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "Resume Screener";
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Controls.Add(this.btnSearch);
            this.searchPanel.Controls.Add(this.cmbSortOptions);
            this.searchPanel.Controls.Add(this.btnSort);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(240, 104);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1093, 49);
            this.searchPanel.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 738);
            this.Controls.Add(this.dataGridViewResumes);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.topPanel);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1061, 728);
            this.Name = "Form1";
            this.Text = "Resume Screener";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResumes)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Panel searchPanel;
    }
}