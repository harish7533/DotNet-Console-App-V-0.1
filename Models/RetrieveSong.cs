using Microsoft.Data.SqlClient;

namespace MusicPlayerSystem_v1_Database.Models
{
    public class RetrieveSongs
    {
        static readonly Dictionary<string, Song> songs = [];
        public static Dictionary<string, Song> FetchSongsFromDatabase()
        {
            using SqlCommand command = new("SELECT songId, songName, artistName, albumName, playlistName, genre, duration FROM Songs", DataBaseConnectionManager.dataBaseConnectionManager.Connection);
            using SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string songId = dataReader.GetString(0);
                string songName = dataReader.GetString(1);
                string artistName = dataReader.GetString(2);
                string albumName = dataReader.GetString(3);
                string playlistName = dataReader.GetString(4);
                string genre = dataReader.GetString(5);
                TimeSpan durationUnVerified = dataReader.GetTimeSpan(6);

                double minutes = durationUnVerified.Minutes;
                double seconds = durationUnVerified.Seconds;

                TimeSpan duration = TimeSpan.FromMinutes(minutes).Add(TimeSpan.FromSeconds(seconds));

                Song newSong = new(songId, songName, artistName, albumName, playlistName, genre, duration);
                songs[songId] = newSong;
            }
            return songs;
        }
    }
}