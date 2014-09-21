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
        Task<string> GetCatUrl();

        Task<byte[]> GetCat();
        Task<string> GetRandomWords(int wordCount);
    }

}