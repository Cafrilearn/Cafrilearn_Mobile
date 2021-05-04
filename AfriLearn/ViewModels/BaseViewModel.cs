using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    public class BaseViewModel : ContentPage
    {
        private bool mainContentVisibility = true;
        private bool isRefreshing;
        public BaseViewModel()
        {            
        }      

        public bool MainContentVisibility
        {
            get { return  mainContentVisibility; }
            set 
            {
                mainContentVisibility = value;
                OnPropertyChanged(nameof(MainContentVisibility));
            }
        }

        public bool  IsRefreshing
        {
            get { return  isRefreshing; }
            set 
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

    }
}
