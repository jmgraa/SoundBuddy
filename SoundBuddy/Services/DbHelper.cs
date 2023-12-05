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
        private static string _connectionString = "Data Source=C:\\Users\\jmgra\\Documents\\Projects\\C#\\SoundBuddy\\SoundBuddy\\Services\\SBData.db;Version=3;";

        private static DataTable ExecuteQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
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

        private static bool ExecuteNonQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Operacja wykonana poprawnie.");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd podczas operacji: " + ex.Message);
                    return false;
                }
            }
        }

        public static int? AddSongToDatabase(string path)
        {
            var addingSuccess = ExecuteNonQuery($"INSERT INTO Songs (Path) VALUES ('{path}')");

            if (addingSuccess)
            {
                var queryResult = ExecuteQuery($"SELECT Id FROM Songs WHERE Path = '{path}'");

                foreach (DataRow row in queryResult.Rows)
                {
                    return int.Parse(row["Id"].ToString());
                }
            }

            return null;
        }

        public static List<(int, String)> GetAllPathsAndIds()
        {
            List<(int, String)> pathsAndIds = new List<(int, String)>();
            var queryResult = ExecuteQuery("SELECT Id, Path FROM Songs;");

            foreach (DataRow row in queryResult.Rows)
            {
                pathsAndIds.Add((int.Parse(row["Id"].ToString()), row["Path"].ToString()));
            }

            return pathsAndIds;
        }
    }
}
