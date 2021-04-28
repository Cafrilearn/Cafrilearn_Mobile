using AfriLearn.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        private async void BookNameLabel_Tapped(object sender, System.EventArgs e)
        {
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            booksListViw.IsVisible = false;

            var label = sender as Label;
            var vm = new SubjectsViewModel();
            await vm.GetBook(label.Text);

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            booksListViw.IsVisible = true;
        }
    }
}