/*
@title			Dulcet - Music Player
@author			Harish B
@createdOn		17-12-2024
@modifiedOn		-
@reviewedBy		-
@reviewedOn		-
*/

using System.Text.RegularExpressions;
using MusicPlayerSystem_v1_Database.Exceptions;
using MusicPlayerSystem_v1_Database.Logger;
using MusicPlayerSystem_v1_Database.Models;
using static System.Console;

namespace MusicPlayerSystem_v1_Database.Services
{
    class SongManager
    {
        private readonly LoggerService _loggerService;
        public SongManager(LoggerService loggerService)
        {
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService)); ;
        }
        public SongManager() { }
        public static readonly Dictionary<string, Song> songs = [];
        static bool isPlaying = false;
        static Song currentSong = null;
        // Fetching songs from Database and storing it in the Dictionary songs
        static SongManager()
        {
            songs = RetrieveSongs.FetchSongsFromDatabase();
        }
        // Validating the user choices with RegEx with the play, search, buy, exit.
        static void ValidateUserChoice(string userInput)
        {
            string patternPlay = @"^play\s+\d+$";
            string patternSearch = @"^search\s+[a-zA-Z0-9\s]+$";
            string patternBuy = @"^buy\s+\d+$";
            string patternExit = @"^exit$";

            if (!Regex.IsMatch(userInput, patternPlay) &&
            !Regex.IsMatch(userInput, patternBuy) &&
            !Regex.IsMatch(userInput, patternExit) &&
            !Regex.IsMatch(userInput, patternSearch))
            {
                throw new CustomInvalidArgumentException("Invalid command. Please enter one of the following: 'play <id>', 'search <name>', 'buy <id>', or 'exit'.");
            }
        }
        // Loading the songtrack page to the user in console like he wants to perform some operations in the application
        public void LoadSongTrack()
        {
            string userInput;
            do
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("\nEnter a command: 'play <id>', 'search <name>', 'buy <id>', or 'exit' to quit:");
                ForegroundColor = ConsoleColor.White;
                userInput = ReadLine();

                try
                {
                    ValidateUserChoice(userInput);
                }
                catch (CustomInvalidArgumentException customInvalidArgumentException)
                {
                    _loggerService.LogExceptionMessage(customInvalidArgumentException.Message);
                    WriteLine(customInvalidArgumentException.Message);
                }

                if (userInput.StartsWith("play "))
                {
                    string songId = userInput.Split(' ')[1];
                    PlaySong(songId);
                }
                else if (userInput.StartsWith("search "))
                {
                    // string searchTerm = userInput.Substring(7);
                    string searchTerm = userInput[7..];
                    SearchSongs(searchTerm);
                }
                else if (userInput.StartsWith("buy "))
                {
                    string songId = userInput.Split(' ')[1];
                    BuySong(songId);
                }
                else if (userInput.Contains("exit"))
                {
                    Environment.Exit(0);
                }
            } while (userInput.ToLower().ToString() != "exit");
        }

        // Displaying the songs from the database is stored in Dictionary songs and display to the user in as a Table
        public void DisplaySongs()
        {
            WriteLine("Available Songs:");
            WriteLine("{0,-10} {1,-30} {2,-20} {3,-20} {4,-20} {5,-15} {6,-10}",
                          "Song ID", "Song Name", "Artist Name", "Album Name", "Playlist Name", "Genre", "Duration");
            WriteLine(new string('-', 130));

            foreach (var songEntry in songs)
            {
                Thread.Sleep(120);
                var song = songEntry.Value;
                WriteLine("{0,-10} {1,-30} {2,-20} {3,-20} {4,-20} {5,-15} {6,-10}",
                              song.SongId, song.SongName, song.ArtistName, song.AlbumName, song.PlaylistName, song.Genre, song.Duration);
            }
        }
        // Play the song for the user input
        static void PlaySong(string songId)
        {
            var song = FindSongById(songId);
            if (song != null)
            {
                StopMusic();
                currentSong = song;
                isPlaying = true;
                WriteLine($"Now Playing: {song.SongName} by {song.ArtistName}");
                WriteLine("Press 'p' to pause");

                while (isPlaying)
                {
                    var command = ReadKey(true).KeyChar;
                    if (command == 'p')
                    {
                        PauseSong();
                    }
                    else if (command == 'r')
                    {
                        ResumeSong();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                WriteLine("Song not found.");
            }
        }

        // Pause the song which is currently running
        static void PauseSong()
        {
            isPlaying = false;
            WriteLine("Paused. Press 'r' to resume.");
            ResumeSong();
        }
        // Resume the song which is currently paused
        static void ResumeSong()
        {
            isPlaying = true;
            WriteLine($"Resumed: {currentSong.SongName} by {currentSong.ArtistName}");
        }
        // Stops the song which is currently running or else stop whole previous playback
        static void StopMusic()
        {
            isPlaying = false;
            currentSong = null;
            WriteLine("Stopped previous playback. You're get to go");
        }
        // Search the songs based on the user inputs
        static void SearchSongs(string searchTerm)
        {
            List<Song> results = [];
            foreach (var songEntry in songs)
            {
                var song = songEntry.Value;
                if (song.SongName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    song.ArtistName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(song);
                }
            }

            if (results.Count > 0)
            {
                WriteLine("Search Results:");
                foreach (var song in results)
                {
                    WriteLine($"{song.SongId}: {song.SongName} by {song.ArtistName} [{song.AlbumName}] - Duration: {song.Duration}");
                }
            }
            else
            {
                WriteLine("No results found.");
            }
        }
        // Display the song with an ID like songId
        static Song FindSongById(string songId)
        {
            foreach (var songEntry in songs)
            {
                var song = songEntry.Value;
                if (song.SongId == songId)
                {
                    return song;
                }
            }
            return null;
        }
        // User can buy the song by entering the songId
        static void BuySong(string songId)
        {
            var song = FindSongById(songId);
            if (song != null)
            {
                WriteLine($"Purchasing '{song.SongName}' by {song.ArtistName}...");
                Welcome.Loading();
                WriteLine($"Successfully purchased '{song.SongName}' for 299!");
            }
            else
            {
                WriteLine("Song not found.");
            }
        }
    }
}

// AddSong(songs, new Song(1, "Nenjukkul Peidhidum", "Harris Jayaraj", "Vaaranam Aayiram", "Romantic Melodies", "Melody", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(25))));
// AddSong(songs, new Song(2, "Rowdy Baby", "Dhanush, Dhee", "Maari 2", "Dance Hits", "Folk", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(42))));
// AddSong(songs, new Song(3, "Why This Kolaveri Di", "Dhanush", "3", "Trending Hits", "Pop", TimeSpan.FromMinutes(4)));
// AddSong(songs, new Song(4, "Mersal Arasan", "A.R. Rahman", "Mersal", "Mass Hits", "Folk", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(12))));
// AddSong(songs, new Song(5, "Vaseegara", "Bombay Jayashri", "Minnale", "Evergreen Classics", "Melody", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(2))));
// AddSong(songs, new Song(6, "Anbe Sivam", "S.P. Balasubrahmanyam", "Anbe Sivam", "Inspirational Songs", "Devotional", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(18))));
// AddSong(songs, new Song(7, "Vaathi Coming", "Anirudh Ravichander", "Master", "Party Hits", "Folk", TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(50))));
// AddSong(songs, new Song(8, "Aalaporan Thamizhan", "A.R. Rahman", "Mersal", "Patriotic Songs", "Folk", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(47))));
// AddSong(songs, new Song(9, "Munbe Vaa", "A.R. Rahman", "Sillunu Oru Kaadhal", "Romantic Melodies", "Melody", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(20))));
// AddSong(songs, new Song(10, "Kannaana Kanney", "D. Imman", "Viswasam", "Heartfelt Tunes", "Melody", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(39))));
// AddSong(songs, new Song(11, "Oru Naalil", "Yuvan Shankar Raja", "Pudhupettai", "Melancholy Tunes", "Melody", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(12))));
// AddSong(songs, new Song(12, "Kadhalan Kangal", "Sid Sriram", "Unnale Unnale", "Romantic Vibes", "Melody", TimeSpan.FromMinutes(5)));
// AddSong(songs, new Song(13, "Thaniye Thananthaniye", "Shankar Mahadevan", "Rhythm", "Soulful Tunes", "Melody", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(10))));
// AddSong(songs, new Song(14, "En Uchimandai", "Santhosh Narayanan", "Jigarthanda", "Mass Hits", "Folk", TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(52))));
// AddSong(songs, new Song(15, "Maduraikku Pogadhadi", "Rahul Nambiar", "Azhagiya Tamil Magan", "Festive Beats", "Folk", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(30))));
// AddSong(songs, new Song(16, "Kanave Kanave", "Anirudh Ravichander", "David", "Emotional Tunes", "Melody", TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(47))));
// AddSong(songs, new Song(17, "Rakkamma Kaiya Thattu", "SPB, Swarnalatha", "Thalapathi", "Retro Classics", "Pop", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(43))));
// AddSong(songs, new Song(18, "Vizhigalil Oru Vaanavil", "Karthik", "Deiva Thirumagal", "Heartfelt Tunes", "Melody", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(3))));
// AddSong(songs, new Song(19, "Naaka Mukka", "Chinmayi", "Kadhalil Vizhunthen", "Party Anthems", "Pop", TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(32))));
// AddSong(songs, new Song(20, "Vaseegara Remix", "Bombay Jayashri", "Minnale", "Club Remixes", "Pop", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(50))));
// AddSong(songs, new Song(21, "Poove Sempoove", "K.J. Yesudas", "Solla Thudikkudhu Manasu", "Evergreen Hits", "Classical", TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(22))));
// AddSong(songs, new Song(22, "Manmadhan Theme", "Yuvan Shankar Raja", "Manmadhan", "Instrumental Beats", "Instrumental", TimeSpan.FromMinutes(2).Add(TimeSpan.FromSeconds(48))));
// AddSong(songs, new Song(23, "Nenjinile Nenjinile", "A.R. Rahman", "Uyire", "Romantic Melodies", "Rock", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(15))));
// AddSong(songs, new Song(24, "Vaarthai Thavari", "Haricharan", "Ayudha Ezhuthu", "Life Lessons", "Classical", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(25))));
// AddSong(songs, new Song(25, "Petta Paraak", "Anirudh Ravichander", "Petta", "Mass Moments", "Rock", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(35))));
// AddSong(songs, new Song(26, "Kannamma Kannamma", "Ilaiyaraaja", "Kaala", "Soulful Melodies", "Folk", TimeSpan.FromMinutes(4).Add(TimeSpan.FromSeconds(52))));
// AddSong(songs, new Song(27, "Maalai Neram", "Chinmayi, Aalaap Raju", "Anegan", "Love Ballads", "Melody", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(8))));
// AddSong(songs, new Song(28, "Aathichudi", "Jassie Gift", "TN 07 AL 4777", "Cultural Beats", "Folk", TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(55))));
// AddSong(songs, new Song(29, "Narumugaiye", "Unnikrishnan", "Iruvar", "Carnatic Specials", "Classical", TimeSpan.FromMinutes(6)));
// AddSong(songs, new Song(30, "Kaatre En Vaasal", "Hariharan", "Rhythm", "Calm and Peaceful", "Melody", TimeSpan.FromMinutes(5).Add(TimeSpan.FromSeconds(38))));

// static void AddSong(Dictionary<int, Song> songs, Song song)
// {
//     if (!songs.ContainsKey(song.SongId))
//         songs.Add(song.SongId, song);
// }   

// {
//     if (songs.TryGetValue(1, out Song fetchedSong))
//     {
//         WriteLine($"Fetched: {fetchedSong.SongName} by {fetchedSong.ArtistName}");
//     }

//     UpdateSong(songs, 1, "Updated Song Name");

//     DeleteSong(songs, 2);


// static void UpdateSong(Dictionary<int, Song> songs, int id, string newName)
// {
//     if (songs.ContainsKey(id))
//         songs[id].SongName = newName;
// }

// static void DeleteSong(Dictionary<int, Song> songs, int id)
// {
//     if (songs.ContainsKey(id))
//         songs.Remove(id);
// }