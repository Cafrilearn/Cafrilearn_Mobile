using AfriLearn.Constants;
using AfriLearn.Services;
using AfriLearn.ViewModels;
using Akavache;
using System;
using System.Collections.Generic;
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

            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            subjectNamesListView.IsVisible = false;

            var httpClientService = new HttpClientService();

            List<string> selectedBooks;

            var selecteBookName = SubjectName.Replace(' ', '/').ToLower();

            try
            {
                selectedBooks = await BlobCache.LocalMachine.GetObject<List<string>>(selecteBookName);
            }
            catch (Exception)
            {
                if (!InternetService.Internet())
                {
                   await InternetService.NoInternet();
                    return;
                }

                // instead of getting all books and saving them, lets get the selected books,
                // there might be thousands of books and its memory inefficient pulling them all by name
                var selecteBookNameResponse = await httpClientService.Get(HttpClientServiceConstants.RootUri + "courses/books/" + selecteBookName);
                string[] selectedBookString = selecteBookNameResponse.Split(new char[] { ':' }, 2);
                selectedBooks = new List<string>(selectedBookString[1]
                                .Replace("[",string.Empty)
                                .Replace("]","")
                                .Replace("}","")
                                .Replace("\"","")
                                .Split(','));
                await BlobCache.LocalMachine.InsertObject(selecteBookName, selectedBooks);
            }


            // these pieces of code will be removed once the names for different subjects are changed
            // and become unique
            var selectedSubjectShortNames = new List<string>();
            foreach (var selectedSubjectlongName in selectedBooks)
            {
                var index = selectedSubjectlongName.LastIndexOf('/');
                var  shortName = selectedSubjectlongName.Substring(index + 1);
                selectedSubjectShortNames.Add(shortName);
            }

            subjectNamesListView.ItemsSource = selectedSubjectShortNames;

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
            subjectNamesListView.IsVisible = true;
        }
        private async void BookNameLabel_Tapped(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            subjectNamesListView.IsVisible = false;

            var label = sender as Label;
            var exploreVM = new SubjectsViewModel();
            await exploreVM.GetBook(label.Text);

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
            subjectNamesListView.IsVisible = true;
        }
    }
}