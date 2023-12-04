using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace SoundBuddy.Services
{
    internal static class DbHelper
    {
        private string connectionString;

        public DbHelper(string dbPath)
        {
            connectionString = $"Data Source={dbPath};Version=3;";
        }

        public static DataTable ExecuteQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public static void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<String> GetAllPaths()
        {
            List<String> paths = new List<String>();
            var querryResult = ExecuteQuery("SELECT Path FROM Songs;");

            foreach (DataRow row in querryResult.Rows)
            {
                paths.Append(row["Path"].ToString());
            }

            return paths;
        }
    }
}
