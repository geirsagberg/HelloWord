using System.Threading.Tasks;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.UserDialogs;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.Services;

namespace HelloWord.ViewModels
{
    public class MainViewModel
        : MvxViewModel
    {
        public string CatUrl {
            get;
            set;
        }

        private readonly ICatService catService;
        private readonly IUserDialogService userDialogService;
        private readonly IUserInteraction userInteraction;

        public ICommand FetchCatCommand
        {
            get { return new AsyncCommand(FetchCat); }
        }

        public byte[] CatBytes { get; set; }
        public string RandomWords { get; set; }

        public MainViewModel(ICatService catService, IUserDialogService userDialogService, IUserInteraction userInteraction)
        {
            this.catService = catService;
            this.userDialogService = userDialogService;
            this.userInteraction = userInteraction;
        }

        private async Task FetchCat()
        {
            using (userDialogService.Loading())
            {
                // Start downloading cat while prompting for wordcount
                Task<byte[]> catTask = catService.GetCat();
//                Task<string> catTask = catService.GetCatUrl();


                int wordCount = 0;
                while (wordCount == 0)
                {
                    InputResponse response = await userInteraction.InputAsync("How many random words?", "e.g. 10");
                    if (!response.Ok)
                        return;
                    if (!int.TryParse(response.Text, out wordCount))
                        userDialogService.Toast("Please enter a number greater than 0");
                }

                Task<string> wordTask = catService.GetRandomWords(wordCount);
                await Task.WhenAll(catTask, wordTask);
                CatBytes = catTask.Result;
                RandomWords = wordTask.Result;
            }
        }
    }
}