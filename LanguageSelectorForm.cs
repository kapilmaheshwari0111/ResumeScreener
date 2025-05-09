using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ResumeScrenner
{
    public partial class LanguageSelectorForm : Form
    {
        private Dictionary<string, string> _languageNames = new Dictionary<string, string>
        {
            { "en", "English" },
            { "es", "Español (Spanish)" },
            { "fr", "Français (French)" },
            { "de", "Deutsch (German)" },
            { "zh", "中文 (Chinese)" },
            { "ja", "日本語 (Japanese)" },
            { "ru", "Русский (Russian)" },
            { "ar", "العربية (Arabic)" }
        };

        public string SelectedLanguage { get; private set; }

        public LanguageSelectorForm()
        {
            InitializeComponent();
            LoadLanguages();
        }

        private void LoadLanguages()
        {
            string[] availableLanguages = LocalizationManager.GetAvailableLanguages();
            string currentLanguage = LocalizationManager.GetCurrentLanguage();

            foreach (string langCode in availableLanguages)
            {
                string displayName = _languageNames.ContainsKey(langCode)
                    ? _languageNames[langCode]
                    : langCode;

                cmbLanguages.Items.Add(new LanguageItem(langCode, displayName));

                if (langCode == currentLanguage)
                {
                    cmbLanguages.SelectedIndex = cmbLanguages.Items.Count - 1;
                }
            }

            // If no language was selected, select the first one
            if (cmbLanguages.SelectedIndex == -1 && cmbLanguages.Items.Count > 0)
            {
                cmbLanguages.SelectedIndex = 0;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (cmbLanguages.SelectedItem is LanguageItem selectedItem)
            {
                SelectedLanguage = selectedItem.LanguageCode;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Class to represent a language in the combobox
        private class LanguageItem
        {
            public string LanguageCode { get; }
            public string DisplayName { get; }

            public LanguageItem(string languageCode, string displayName)
            {
                LanguageCode = languageCode;
                DisplayName = displayName;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}