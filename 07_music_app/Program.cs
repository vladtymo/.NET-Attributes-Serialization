using System.Text.Json;

namespace _07_music_app
{
    public class Song
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Style { get; set; }

        public void InputData()
        {
            // input song information
            Console.Write("Enter song name: ");
            this.Name = Console.ReadLine();

            Console.Write("Enter song style: ");
            this.Style = Console.ReadLine();

            Console.Write("Enter duration: ");
            this.Duration = TimeSpan.Parse(Console.ReadLine());
        }

        public override string ToString()
        {
            return $"Song: {Name} - {Duration} - {Style}";
        }
    }
    public class Album
    {
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public int PublishYear { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();

        public void Show()
        {
            Console.WriteLine($"--------- {Name} : {PublishYear} ---------\n" +
                $"Artist: {ArtistName}\n" +
                $"Duration: {Duration}");
            Console.WriteLine("\tSongs");
            foreach (var song in this.Songs)
            {
                Console.WriteLine(song);
            }
        }
        public void InputData()
        {
            // input album information
            Console.Write("Enter album name: ");
            this.Name = Console.ReadLine();

            Console.Write("Enter artist name: ");
            this.ArtistName = Console.ReadLine();

            Console.Write("Enter publish year: ");
            this.PublishYear = int.Parse(Console.ReadLine());

            Console.Write("Enter duration: ");
            this.Duration = TimeSpan.Parse(Console.ReadLine());

            // input songs information
            Console.Write("Enter song count: ");
            var songs = int.Parse(Console.ReadLine());

            for (int i = 0; i < songs; i++)
            {
                Song newSong = new Song();
                newSong.InputData();
                this.Songs.Add(newSong);
            }
        }
        public void Save()
        {
            // Serialize the object to a JSON file
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText("album_data.json", json);
        }
        public void Load()
        {
            // Deserialize the object from a JSON file
            string json = File.ReadAllText("album_data.json");
            Album? loaded = JsonSerializer.Deserialize<Album>(json);

            this.Name = loaded.Name;
            this.ArtistName = loaded.ArtistName;
            this.PublishYear = loaded.PublishYear;
            this.Duration = loaded.Duration;
            this.Songs = loaded.Songs;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Album album = new Album();

            album.InputData();
            album.Show();

            album.Save();

            album.Name = "";
            album.PublishYear = 0;
            album.Songs.Clear();

            album.Load();

            album.Show();
        }
    }
}