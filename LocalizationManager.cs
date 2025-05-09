using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Xml;

namespace ResumeScrenner
{
	public class LocalizationManager
	{
		private static readonly string ResourcesFolder = "Resources";
		private static Dictionary<string, Dictionary<string, string>> _translations;
		private static string _currentLanguage = "en"; // Default language

		static LocalizationManager()
		{
			Initialize();
		}

		public static void Initialize()
		{
			_translations = new Dictionary<string, Dictionary<string, string>>();

			if (!Directory.Exists(ResourcesFolder))
			{
				Directory.CreateDirectory(ResourcesFolder);
				CreateDefaultLanguageFiles();
			}

			LoadAllLanguages();
		}

		private static void CreateDefaultLanguageFiles()
		{
			// Create English language file
			Dictionary<string, string> englishTranslations = new Dictionary<string, string>
			{
				{ "MainForm_Title", "Resume Screener" },
				{ "Button_LoadResumes", "Load Resumes" },
				{ "Button_Search", "Search" },
				{ "Button_RankCandidates", "Rank Candidates" },
				{ "Button_Sort", "Sort" },
				{ "Button_AdvancedSearch", "Advanced Search" },
				{ "Button_ExportCSV", "Export to CSV" },
				{ "Button_Accept", "Accept" },
				{ "Button_Reject", "Reject" },
				{ "Status_Accepted", "Accepted" },
				{ "Status_Rejected", "Rejected" },
				{ "Status_Pending", "Pending" },
				{ "Menu_File", "File" },
				{ "Menu_File_Export", "Export to CSV..." },
				{ "Menu_File_Exit", "Exit" },
				{ "Menu_Tools", "Tools" },
				{ "Menu_Tools_AdvancedSearch", "Advanced Search..." },
				{ "Menu_Tools_ViewLogs", "View Logs" },
				{ "Menu_User", "User" },
				{ "Menu_User_ChangePassword", "Change Password..." },
				{ "Menu_User_Logout", "Logout" },
				{ "Menu_Language", "Language" },
				{ "Menu_Help", "Help" },
				{ "Menu_Help_About", "About" },
				{ "Dialog_OperationSuccess", "Operation Completed Successfully" },
				{ "Dialog_OperationFailed", "Operation Failed" },
				{ "Dialog_Confirmation", "Confirmation" },
				{ "Dialog_Warning", "Warning" },
				{ "Dialog_Error", "Error" },
				{ "Dialog_Info", "Information" },
				{ "Filter_CSVFiles", "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*" },
				{ "Message_LoadSuccess", "Resumes loaded successfully." },
				{ "Message_ExportSuccess", "Resumes exported successfully." },
				{ "Message_CandidateAccepted", "Candidate accepted successfully." },
				{ "Message_CandidateRejected", "Candidate rejected successfully." },
				{ "Message_NoResumesSelected", "No resumes selected." },
				{ "Message_LoginRequired", "Please login to continue." },
				{ "Message_InvalidCredentials", "Invalid username or password." },
				{ "Message_NoSearchResults", "No matching resumes found." }
			};

			SaveLanguageFile("en", englishTranslations);

			// Create Spanish language file
			Dictionary<string, string> spanishTranslations = new Dictionary<string, string>
			{
				{ "MainForm_Title", "Filtro de Currículums" },
				{ "Button_LoadResumes", "Cargar Currículums" },
				{ "Button_Search", "Buscar" },
				{ "Button_RankCandidates", "Clasificar Candidatos" },
				{ "Button_Sort", "Ordenar" },
				{ "Button_AdvancedSearch", "Búsqueda Avanzada" },
				{ "Button_ExportCSV", "Exportar a CSV" },
				{ "Button_Accept", "Aceptar" },
				{ "Button_Reject", "Rechazar" },
				{ "Status_Accepted", "Aceptado" },
				{ "Status_Rejected", "Rechazado" },
				{ "Status_Pending", "Pendiente" },
				{ "Menu_File", "Archivo" },
				{ "Menu_File_Export", "Exportar a CSV..." },
				{ "Menu_File_Exit", "Salir" },
				{ "Menu_Tools", "Herramientas" },
				{ "Menu_Tools_AdvancedSearch", "Búsqueda Avanzada..." },
				{ "Menu_Tools_ViewLogs", "Ver Registros" },
				{ "Menu_User", "Usuario" },
				{ "Menu_User_ChangePassword", "Cambiar Contraseña..." },
				{ "Menu_User_Logout", "Cerrar Sesión" },
				{ "Menu_Language", "Idioma" },
				{ "Menu_Help", "Ayuda" },
				{ "Menu_Help_About", "Acerca de" },
				{ "Dialog_OperationSuccess", "Operación Completada Exitosamente" },
				{ "Dialog_OperationFailed", "La Operación Falló" },
				{ "Dialog_Confirmation", "Confirmación" },
				{ "Dialog_Warning", "Advertencia" },
				{ "Dialog_Error", "Error" },
				{ "Dialog_Info", "Información" },
				{ "Filter_CSVFiles", "Archivos CSV (*.csv)|*.csv|Todos los Archivos (*.*)|*.*" },
				{ "Message_LoadSuccess", "Currículums cargados exitosamente." },
				{ "Message_ExportSuccess", "Currículums exportados exitosamente." },
				{ "Message_CandidateAccepted", "Candidato aceptado exitosamente." },
				{ "Message_CandidateRejected", "Candidato rechazado exitosamente." },
				{ "Message_NoResumesSelected", "No hay currículums seleccionados." },
				{ "Message_LoginRequired", "Por favor inicie sesión para continuar." },
				{ "Message_InvalidCredentials", "Nombre de usuario o contraseña inválidos." },
				{ "Message_NoSearchResults", "No se encontraron currículums coincidentes." }
			};

			SaveLanguageFile("es", spanishTranslations);
		}

		private static void LoadAllLanguages()
		{
			_translations.Clear();

			string[] languageFiles = Directory.GetFiles(ResourcesFolder, "*.xml");
			foreach (string file in languageFiles)
			{
				string languageCode = Path.GetFileNameWithoutExtension(file);
				Dictionary<string, string> translations = LoadLanguageFile(languageCode);
				_translations[languageCode] = translations;
			}
		}

		private static Dictionary<string, string> LoadLanguageFile(string languageCode)
		{
			Dictionary<string, string> translations = new Dictionary<string, string>();
			string filePath = Path.Combine(ResourcesFolder, $"{languageCode}.xml");

			if (!File.Exists(filePath))
			{
				Logger.Log(LogLevel.Warning, $"Language file not found: {filePath}");
				return translations;
			}

			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(filePath);

				XmlNodeList nodes = doc.SelectNodes("//string");
				if (nodes != null)
				{
					foreach (XmlNode node in nodes)
					{
						string key = node.Attributes["name"]?.Value;
						string value = node.InnerText;

						if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
						{
							translations[key] = value;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(LogLevel.Error, $"Error loading language file {filePath}: {ex.Message}");
			}
			return translations;
		}

		private static void SaveLanguageFile(string languageCode, Dictionary<string, string> translations)
		{
			string filePath = Path.Combine(ResourcesFolder, $"{languageCode}.xml");

			try
			{
				XmlDocument doc = new XmlDocument();
				XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
				doc.AppendChild(xmlDeclaration);

				XmlElement root = doc.CreateElement("resources");
				doc.AppendChild(root);

				foreach (KeyValuePair<string, string> translation in translations)
				{
					XmlElement stringElement = doc.CreateElement("string");
					XmlAttribute nameAttribute = doc.CreateAttribute("name");
					nameAttribute.Value = translation.Key;
					stringElement.Attributes.Append(nameAttribute);
					stringElement.InnerText = translation.Value;
					root.AppendChild(stringElement);
				}

				doc.Save(filePath);
				Logger.Log(LogLevel.Info, $"Language file saved: {filePath}");
			}
			catch (Exception ex)
			{
				Logger.Log(LogLevel.Error, $"Error saving language file {filePath}: {ex.Message}");
			}
		}

		public static string GetString(string key)
		{
			if (_translations.TryGetValue(_currentLanguage, out Dictionary<string, string> currentTranslations))
			{
				if (currentTranslations.TryGetValue(key, out string value))
				{
					return value;
				}
			}

			// Fall back to English if translation is not found
			if (_currentLanguage != "en" && _translations.TryGetValue("en", out Dictionary<string, string> englishTranslations))
			{
				if (englishTranslations.TryGetValue(key, out string value))
				{
					return value;
				}
			}

			// Return the key itself if no translation is found
			return key;
		}

		public static void SetLanguage(string languageCode)
		{
			if (_translations.ContainsKey(languageCode))
			{
				_currentLanguage = languageCode;

				// Set the current UI culture
				CultureInfo culture = new CultureInfo(languageCode);
				Thread.CurrentThread.CurrentUICulture = culture;

				Logger.Log(LogLevel.Info, $"Language set to: {languageCode}");
			}
			else
			{
				Logger.Log(LogLevel.Warning, $"Language not found: {languageCode}, defaulting to English");
				_currentLanguage = "en";
			}
		}

		public static string[] GetAvailableLanguages()
		{
			return new List<string>(_translations.Keys).ToArray();
		}

		public static string GetCurrentLanguage()
		{
			return _currentLanguage;
		}
	}
}

