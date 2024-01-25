using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Media.Imaging;
using SoundBuddy.Models;
using SoundBuddy.ViewModels;

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
	        try
	        {
		        using (var connection = new SQLiteConnection(ConnectionString))
		        {
			        connection.Open();

			        using (var transaction = connection.BeginTransaction())
			        {
				        try
				        {
					        using (var command = new SQLiteCommand(query, connection, transaction))
					        {
						        command.ExecuteNonQuery();
					        }

					        // Commit the transaction if everything is successful
					        transaction.Commit();

					        return true;
				        }
				        catch (Exception ex)
				        {
					        // Rollback the transaction in case of an exception
					        transaction.Rollback();
					        MessageBox.Show(ex.ToString());
					        return false;
				        }
			        }
		        }
	        }
	        catch (Exception ex)
	        {
		        // Handle connection-related exceptions
		        MessageBox.Show(ex.ToString());
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

        public static int? AddPlaylistToDatabase(string name, string? description, BitmapImage? cover)
        {
            var addingSuccess = ExecuteNonQuery($"INSERT INTO Playlists (Name, Description, Picture) VALUES ('{name}', '{description}', null);");

            if (!addingSuccess) 
                return null;

            var queryResult = ExecuteQuery($"SELECT Id FROM Playlists WHERE Name = '{name}'");

            if (queryResult.Rows.Count <= 0) 
                return null;

            var id = int.Parse(queryResult.Rows[0]["Id"].ToString() ?? string.Empty);

            if (cover != null)
                InsertBitmapImageToDatabase(cover, id);

            return id;

        }

        // TODO fix method - shouldn't be a separate method
        private static void InsertBitmapImageToDatabase(BitmapImage bitmapImage, int id)
        {
            var imageBytes = Tools.ConvertBitmapImageToByteArray(bitmapImage);

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"UPDATE Playlists SET Picture = @image WHERE Id = {id};";
                    command.Parameters.Add("@image", DbType.Binary).Value = imageBytes;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<(int, string)> GetAllPathsAndIds()
        {
            var pathsAndIds = new List<(int, string)>();
            var queryResult = ExecuteQuery("SELECT Id, Path FROM Songs;");

            foreach (DataRow row in queryResult.Rows)
                pathsAndIds.Add((int.Parse(row["Id"].ToString()), row["Path"].ToString()));

            return pathsAndIds;
        }

        public static List<(int, string, string?, BitmapImage?)> GetAllPlaylists()
        {
            var playlistData = new List<(int, string, string?, BitmapImage?)>();
            var queryResult = ExecuteQuery("SELECT Id, Name, Description, Picture FROM Playlists");

            foreach (DataRow row in queryResult.Rows)
            {
                var cover = row["Picture"] != DBNull.Value ? Tools.LoadImageFromBlob((byte[])row["Picture"]) : null;
                playlistData.Add((int.Parse(row["Id"].ToString()), row["Description"].ToString(), row["Description"].ToString(), cover));
            }

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

        public static void DeleteSongFromDatabase(int id)
        {
            ExecuteNonQuery($"DELETE FROM Songs WHERE Id = {id};");
        }

        public static void AddSongToPlaylist(Playlist playlist, int songId)
        {
            var succes = ExecuteNonQuery($"INSERT INTO SongsOnPlaylist (SongId, PlaylistId) VALUES ({songId}, {playlist.Id});");
            //var succes = ExecuteNonQuery($"INSERT INTO SongsOnPlaylist (SongId, PlaylistId) VALUES (5, 10);");
            //var dede = ExecuteNonQuery($"INSERT INTO Songs VALUES (69, 'xddd');");
        }
    }
}
