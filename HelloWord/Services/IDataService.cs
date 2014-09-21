using System.Threading.Tasks;

namespace HelloWord.Services
{
    public interface IDataService
    {
        Task<byte[]> GetCat();
        Task<string> GetRandomWords(int wordCount);
    }
}