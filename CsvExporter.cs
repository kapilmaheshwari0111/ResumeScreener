using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace ResumeScrenner
{
    public class CsvExporter
    {
        public static bool ExportDataTableToCsv(DataTable dt, string filePath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                // Column names
                string[] columnNames = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    // Skip the action button columns
                    if (dt.Columns[i].ColumnName != "Accept" && dt.Columns[i].ColumnName != "Reject")
                    {
                        columnNames[i] = dt.Columns[i].ColumnName;
                        sb.Append(dt.Columns[i].ColumnName + ",");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();

                // Rows
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // Skip the action button columns
                        if (dt.Columns[i].ColumnName != "Accept" && dt.Columns[i].ColumnName != "Reject")
                        {
                            string value = row[i].ToString();
                            // Escape commas in the value
                            if (value.Contains(","))
                            {
                                value = "\"" + value + "\"";
                            }
                            sb.Append(value + ",");
                        }
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine();
                }

                File.WriteAllText(filePath, sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error exporting to CSV: {ex.Message}");
                return false;
            }
        }
    }
}