using System;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;
using HelloWord.Core.Services;
using PropertyChanged;

namespace HelloWord.Core.ViewModels
{
    [ImplementPropertyChanged]
    public class FirstViewModel : MvxViewModel
    {
        private readonly IDataService dataService;
        private readonly IUserDialogService userDialogService;
        private IMvxCommand _fetchCatCommand;

        public IMvxCommand FetchCatCommand
        {
            get { return _fetchCatCommand ?? (_fetchCatCommand = new MvxCommand(FetchCat)); }
        }

        private IMvxCommand _fetchWordsCommand;

        public IMvxCommand FetchWordsCommand
        {
            get { return _fetchWordsCommand ?? (_fetchWordsCommand = new MvxCommand(FetchWords)); }
        }

        private async void FetchWords()
        {
            var result = await userDialogService.PromptAsync("How many words would you like?", placeholder: "e.g. 10");
            if (!result.Ok)
                return;
            int count;
            if (!int.TryParse(result.Text, out count) || count <= 0) {
                userDialogService.Toast("Please enter a positive number.");
                return;
            }
            using (userDialogService.Loading("Fetching words...")) {
                Words = string.Join(" ", await dataService.GetWordsAsync(count));
            }
        }

        public string Words { get; set; }

        public byte[] CatBytes { get; set; }

        public string CatImageBase64
        {
            get
            {
                var base64Image = Convert.ToBase64String(CatBytes);
                return string.Format("<img style='width: 100%' src='data:image/gif;base64,{0}' />", base64Image);
            }
        }

        public FirstViewModel(IDataService dataService, IUserDialogService userDialogService)
        {
            this.dataService = dataService;
            this.userDialogService = userDialogService;
        }

        private async void FetchCat()
        {
            using (userDialogService.Loading("Chasing down a cat...")) {
                CatBytes = await dataService.GetCatAsync();
            }
        }
    }
}