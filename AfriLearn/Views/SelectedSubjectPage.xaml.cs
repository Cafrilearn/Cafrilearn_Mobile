using Akavache;
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
        public SelectedSubjectPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allBooks = await BlobCache.LocalMachine.GetObject<List<string>>("allBookNames");
            var selectedSubjectName = await BlobCache.LocalMachine.GetObject<string>("subject");
            selectedSubjectPage.Title = selectedSubjectName;
            var selectedSubjectLongNames = allBooks.Where(s => s.Contains(selectedSubjectName)).ToList();
            var selectedSubjectShortNames = new List<string>();
            foreach (var selectedSubjectlongName in  selectedSubjectLongNames)
            {
                var index = selectedSubjectlongName.LastIndexOf('/');
                var  shortName = selectedSubjectlongName.Substring(index + 1);
                selectedSubjectShortNames.Add(shortName);
            }
            subjecNamesListView.ItemsSource = selectedSubjectShortNames;
        }
    }
}