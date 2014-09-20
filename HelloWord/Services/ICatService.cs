using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HelloWord.Extensions;
using Newtonsoft.Json.Linq;

namespace HelloWord.Services
{
    public interface ICatService
    {
        Task<byte[]> GetCat();
        Task<string> GetRandomWords(int wordCount);
    }

    public class CatService : ICatService
    {
        public const string WordnikApiKey = "3364abe0c8fd3919b29790676fa05f4330d7b04045d3d5352";
        private readonly HttpClient httpClient;

        public CatService()
        {
            httpClient = new HttpClient();
        }

        public async Task<byte[]> GetCat()
        {
            return await httpClient.GetByteArrayAsync("http://edgecats.net");
        }

        public async Task<string> GetRandomWords(int wordCount)
        {
            string wordsJson =
                await httpClient.GetStringAsync("http://api.wordnik.com/v4/words.json/randomWords?limit={0}&api_key={1}".FormatWith(wordCount, WordnikApiKey));
            JArray wordArray = JArray.Parse(wordsJson);
            IEnumerable<JToken> words = wordArray.Select(w => w["word"]);
            return words.ToJoinedString(" ");
        }
    }
}