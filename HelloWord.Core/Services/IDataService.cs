using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWord.Core.Services
{
    public interface IDataService
    {
        Task<byte[]> GetCatAsync();
        Task<IEnumerable<string>> GetWordsAsync(int count);
    }
}