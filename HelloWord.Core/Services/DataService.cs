using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HelloWord.Core.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient httpClient;
        public const string WordnikApiKey = "3364abe0c8fd3919b29790676fa05f4330d7b04045d3d5352";
        public const string WordnikUrl = "http://api.wordnik.com/v4/words.json/randomWords?limit={0}&api_key={1}";
        public const string EdgeCatsUrl = "http://edgecats.net?random=";

        public DataService()
        {
            httpClient = new HttpClient();
        }

        public async Task<byte[]> GetCatAsync()
        {
            return await httpClient.GetByteArrayAsync(EdgeCatsUrl + new Random().Next());
        }

        public async Task<IEnumerable<string>> GetWordsAsync(int count)
        {
            var url = string.Format(WordnikUrl, count, WordnikApiKey);
            var wordsJson = await httpClient.GetStringAsync(url);
            var wordArray = JArray.Parse(wordsJson);
            return wordArray.Select(w => w["word"].ToString());
        } 
    }
}
