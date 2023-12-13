using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using SoundBuddy.Views.UserControls;

namespace SoundBuddy.Services
{
    internal static class DbHelper
    {
        // TODO connection string fix
        private const string ConnectionString = "Data Source=SBData.db;Version=3;";

        private static DataTable ExecuteQuery(string query)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(query, connection);
            using var adapter = new SQLiteDataAdapter(command);

            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }

        private static bool ExecuteNonQuery(string query)
        {
            using var connection = new SQLiteConnection(ConnectionString);

            try
            {
                connection.Open();

                using var command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int? AddSongToDatabase(string path)
        {
            var addingSuccess = ExecuteNonQuery($"INSERT INTO Songs (Path) VALUES ('{path}')");

            if (!addingSuccess) 
                return null;

            var queryResult = ExecuteQuery($"SELECT Id FROM Songs WHERE Path = '{path}'");

            if (queryResult.Rows.Count > 0)
                return int.Parse(queryResult.Rows[0]["Id"].ToString() ?? string.Empty);

            return null;
        }

        public static List<(int, string)> GetAllPathsAndIds()
        {
            var pathsAndIds = new List<(int, string)>();
            var queryResult = ExecuteQuery("SELECT Id, Path FROM Songs;");

            foreach (DataRow row in queryResult.Rows)
                pathsAndIds.Add((int.Parse(row["Id"].ToString()), row["Path"].ToString()));

            return pathsAndIds;
        }

        public static List<(int, string?, string?, object?)> GetAllPlaylists()
        {
            var playlistData = new List<(int, string?, string?, object?)>();
            var queryResult = ExecuteQuery("SELECT Id, Name, Description, Picture FROM Playlists");

            foreach (DataRow row in queryResult.Rows)
                playlistData.Add((int.Parse(row["Id"].ToString()), row["Description"].ToString(), row["Description"].ToString(), row["Picture"]));

            return playlistData;
        }

        public static List<int> GetSongsOnPlaylistIds(int id)
        {
            var idList = new List<int>();
            var queryResult = ExecuteQuery($"SELECT SongId FROM SongsOnPlaylist WHERE PlaylistId = {id}");

            foreach (DataRow row in queryResult.Rows)
                idList.Add((int.Parse(row["SongId"].ToString())));

            return idList;
        }
    }
}
