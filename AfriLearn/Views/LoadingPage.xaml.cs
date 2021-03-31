using AfriLearn.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public string SubjectName { get; set; }
        public LoadingPage()
        {
            InitializeComponent();
        }
        public  LoadingPage(string bookName)
        {
            SubjectName = bookName;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var exploreVM = new SubjectsViewModel();
            await exploreVM.GetBook(SubjectName);
        }
    }
}