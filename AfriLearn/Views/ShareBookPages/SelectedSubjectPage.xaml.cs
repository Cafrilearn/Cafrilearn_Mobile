using AfriLearn.Services;
using AfriLearn.ViewModels;
using AfriLearnMobile.Models;
using Akavache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedSubjectPage : ContentPage
    {
        public  string  SubjectName { get; set; }
        public SelectedSubjectPage()
        {
            InitializeComponent();
        }
        public SelectedSubjectPage(string subJect)
        {
            InitializeComponent();
            SubjectName = subJect;
            selectedSubjectPage.Title = subJect;
        }    
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
            var httpClientService = new HttpClientService(appUser.AuthKey);
            List<string> allBooks;

            try
            {
                 allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
            }
            catch (Exception)
            {
                var allBooksResponse = await httpClientService.Get("Books/getallbooknames");
                allBooks = JsonConvert.DeserializeObject<List<string>>(allBooksResponse);
                await BlobCache.LocalMachine.InsertObject("allBookNames", allBooks);
            }
            
            var selectedSubjectLongNames = allBooks.Where(s => s.Contains(SubjectName)).ToList();
           
            // these pieces of code will be removed once the names for different subjects are changed
            // and become unique
            var selectedSubjectShortNames = new List<string>();
            foreach (var selectedSubjectlongName in  selectedSubjectLongNames)
            {
                var index = selectedSubjectlongName.LastIndexOf('/');
                var  shortName = selectedSubjectlongName.Substring(index + 1);
                selectedSubjectShortNames.Add(shortName);
            }

            subjectNamesListView.ItemsSource = selectedSubjectShortNames;

            var vm = new BaseViewModel();
            subjectNamesListView.IsVisible = vm.MainContentVisibility;
            loadingActivityIndicator.IsVisible = vm.IsBusy;
        }
    }
}