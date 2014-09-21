using System.Windows.Input;
using Acr.MvvmCross.Plugins.UserDialogs;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.Services;

namespace HelloWord.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IDataService dataService;
        private readonly IUserDialogService userDialogService;
        private readonly IUserInteraction userInteraction;
        public string CatUrl { get; set; }

        public ICommand FetchCatCommand
        {
            get { return new MvxCommand(FetchCat); }
        }

        public ICommand FetchWordsCommand
        {
            get { return new MvxCommand(FetchWords); }
        }

        public byte[] CatBytes { get; set; }
        public string RandomWords { get; set; }

        public MainViewModel(IDataService dataService, IUserDialogService userDialogService, IUserInteraction userInteraction)
        {
            this.dataService = dataService;
            this.userDialogService = userDialogService;
            this.userInteraction = userInteraction;
        }

        private async void FetchWords()
        {
            int wordCount = 0;
            while (wordCount == 0)
            {
                InputResponse response = await userInteraction.InputAsync("How many random words?", "e.g. 10");
                if (!response.Ok)
                    return;
                if (!int.TryParse(response.Text, out wordCount))
                    userDialogService.Toast("Please enter a number greater than 0");
            }

            using (userDialogService.Loading())
            {
                RandomWords = await dataService.GetRandomWords(wordCount);
            }
        }

        private async void FetchCat()
        {
            using (userDialogService.Loading())
            {
                CatBytes = await dataService.GetCat();
            }
        }
    }
}