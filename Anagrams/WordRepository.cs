using System.Reflection;

namespace Anagrams
{
    public class WordRepository
    {
        private const string WordResourceName = "Anagrams.words.txt";

        public List<string> GetWords(int lenth)
        { 
            var words = new List<string>();
            Console.WriteLine("REPOSITORY: Looking for possible matches");

            using (var stream = GetWordStream())
                using (var reader = new StreamReader(stream))
            {
                while(reader.Peek() != -1)
                {
                    var word = reader.ReadLine();
                    if (word?.Length == lenth)
                    {
                        words.Add(word);
                    }
                }
            }

            Task.Delay(1000).Wait();

            Console.WriteLine("REPOSITORY: Matches found");

            return words;
        }

        public async Task<List<string>> GetWordsAsync(int lenth)
        {
            var words = new List<string>();
            Console.WriteLine("REPOSITORY: Looking for possible matches");

            using (var stream = GetWordStream())
            using (var reader = new StreamReader(stream))
            {
                while (reader.Peek() != -1)
                {
                    var word = await reader.ReadLineAsync();
                    if (word?.Length == lenth)
                    {
                        words.Add(word);
                    }
                }
            }

            await Task.Delay(1000);

            Console.WriteLine("REPOSITORY: Matches found");

            return words;
        }

        private Stream GetWordStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(WordResourceName);

            return stream;
        }
    }
}
