using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace ResumeScrenner
{
    public class AdvancedSearch
    {
        private SQLiteConnection _conn;

        public AdvancedSearch(SQLiteConnection connection)
        {
            _conn = connection;
        }

        public DataTable Search(Dictionary<string, object> filters)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM Resumes WHERE 1=1");
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            int paramCount = 0;

            foreach (KeyValuePair<string, object> filter in filters)
            {
                switch (filter.Key)
                {
                    case "Name":
                        queryBuilder.Append(" AND Name LIKE @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, "%" + filter.Value + "%"));
                        paramCount++;
                        break;

                    case "ExperienceMin":
                        queryBuilder.Append(" AND Experience >= @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, filter.Value));
                        paramCount++;
                        break;

                    case "ExperienceMax":
                        queryBuilder.Append(" AND Experience <= @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, filter.Value));
                        paramCount++;
                        break;

                    case "ScoreMin":
                        queryBuilder.Append(" AND Score >= @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, filter.Value));
                        paramCount++;
                        break;

                    case "ScoreMax":
                        queryBuilder.Append(" AND Score <= @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, filter.Value));
                        paramCount++;
                        break;

                    case "Skills":
                        queryBuilder.Append(" AND (Skill1 LIKE @p" + paramCount + " OR Skill2 LIKE @p" + paramCount + " OR Skill3 LIKE @p" + paramCount + ")");
                        parameters.Add(new SQLiteParameter("@p" + paramCount, "%" + filter.Value + "%"));
                        paramCount++;
                        break;

                    case "Course":
                        queryBuilder.Append(" AND Course LIKE @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, "%" + filter.Value + "%"));
                        paramCount++;
                        break;

                    case "Status":
                        queryBuilder.Append(" AND Status = @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, filter.Value));
                        paramCount++;
                        break;

                    case "Email":
                        queryBuilder.Append(" AND Email LIKE @p" + paramCount);
                        parameters.Add(new SQLiteParameter("@p" + paramCount, "%" + filter.Value + "%"));
                        paramCount++;
                        break;
                }
            }

            try
            {
                Logger.Log(LogLevel.Info, "Executing advanced search query: " + queryBuilder.ToString());
                SQLiteCommand cmd = new SQLiteCommand(queryBuilder.ToString(), _conn);
                foreach (SQLiteParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                DataTable result = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(result);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error in advanced search: {ex.Message}");
                return new DataTable();
            }
        }
    }
}