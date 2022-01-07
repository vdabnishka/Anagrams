namespace Anagrams
{
    public class AnagramService
    {
        private readonly WordRepository _repository;

        public AnagramService(WordRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<string> GetAnagrams(string input)
        {
            Console.WriteLine("SERVICE: Preparing...");

            var inputSorted = new string(input.ToCharArray().OrderBy(c => c).ToArray());
            var anagrams = new List<string>();

            var matches = _repository.GetWords(input.Length);

            Console.WriteLine("SERVICE: Analysing matches");

            foreach (var word in matches)
            {
                var sorted = new string(word.ToCharArray().OrderBy(c => c).ToArray());

                if (inputSorted == sorted) anagrams.Add(word);
            }

            Console.WriteLine("SERVICE: Matches analysed");

            return anagrams;
        }

        public Task<List<string>> GetAnagramsNaiveAsync(string input)
        {
            var results = GetAnagrams(input);

            return Task.FromResult(results);
        }

        public async Task<List<string>> GetAnagramsAsync(string input)
        {
            Console.WriteLine("SERVICE: Preparing...");

            var inputSorted = new string(input.ToCharArray().OrderBy(c => c).ToArray());
            var anagrams = new List<string>();

            var task = _repository.GetWordsAsync(input.Length);

            Console.WriteLine("SERVICE: Analysing matches");

            var matches = await task;

            foreach (var word in matches)
            {
                var sorted = new string(word.ToCharArray().OrderBy(c => c).ToArray());

                if (inputSorted == sorted) anagrams.Add(word);
            }

            Console.WriteLine("SERVICE: Matches analysed");

            return anagrams;
        }
    }
}
