using System;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.Extensions;
using HelloWord.Services;

namespace HelloWord.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IDataService dataService;
        private readonly IUserDialogService userDialogService;
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

        public string CatImageBase64
        {
            get
            {
                var base64Image = Convert.ToBase64String(CatBytes);
                return "<img style='width: 100%' src='data:image/gif;base64,{0}' />".FormatWith(base64Image);
            }
        }

        public MainViewModel(IDataService dataService, IUserDialogService userDialogService)
        {
            this.dataService = dataService;
            this.userDialogService = userDialogService;
        }

        private async void FetchWords()
        {
            var response = await userDialogService.PromptAsync("How many random words?", placeholder: "e.g. 10");
            if (!response.Ok)
                return;
            int wordCount;
            if (!int.TryParse(response.Text, out wordCount)) {
                userDialogService.Toast("Please enter a number greater than 0");
                return;
            }

            using (userDialogService.Loading()) {
                RandomWords = await dataService.GetRandomWords(wordCount);
            }
        }

        private async void FetchCat()
        {
            using (userDialogService.Loading()) {
                CatBytes = await dataService.GetCat();
            }
        }
    }
}