/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		17-12-2024
@modifiedOn		26-12-2024
@reviewedBy		-
@reviewedOn		-
*/

namespace MusicPlayerSystem_v1_Database.Models
{
    public class Song
    {
        public string SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string PlaylistName { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }

        public Song(string songId, string songName, string artistName, string albumName, string playlistName, string genre, TimeSpan duration)
        {
            SongId = songId;
            SongName = songName;
            ArtistName = artistName;
            AlbumName = albumName;
            PlaylistName = playlistName;
            Genre = genre;
            Duration = duration;
        }
        public override string ToString()
        {
            return $"{SongName} by {ArtistName} from the album '{AlbumName}' [{Genre}] - Duration: {Duration}";
        }
    }
}