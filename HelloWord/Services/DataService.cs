using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HelloWord.Extensions;
using Newtonsoft.Json.Linq;

namespace HelloWord.Services
{
    public class DataService : IDataService
    {
        public const string WordnikApiKey = "3364abe0c8fd3919b29790676fa05f4330d7b04045d3d5352";
        private readonly HttpClient httpClient;

        public DataService()
        {
            httpClient = new HttpClient();
        }

        public async Task<byte[]> GetCat()
        {
            return await httpClient.GetByteArrayAsync("http://edgecats.net");
        }

        public async Task<string> GetRandomWords(int wordCount)
        {
            string url = "http://api.wordnik.com/v4/words.json/randomWords?limit={0}&api_key={1}".FormatWith(wordCount, WordnikApiKey);
            string wordsJson = await httpClient.GetStringAsync(url);
            JArray wordArray = JArray.Parse(wordsJson);
            IEnumerable<JToken> words = wordArray.Select(w => w["word"]);
            return words.ToJoinedString(" ");
        }
    }
}