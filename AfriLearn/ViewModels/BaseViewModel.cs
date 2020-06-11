using AfriLearn;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace  AfriLearn.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // fields
        private bool isBusy;
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructors
        public BaseViewModel()
        {
             
           
        }
        
       //Properties
        public  bool  IsBusy
        {
            get { return  isBusy; }
            set 
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }


        //Methods
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task<bool> DisplayAlert(string title, string message, string agree, string deny)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, agree, deny);
        }
        public async Task  DisplayAlert(string title, string message, string okay)
        {
            await App.Current.MainPage.DisplayAlert(title, message, okay);
        }
        public async Task PopAsync()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public  bool Internet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }
        public async Task NoInternet()
        {    
           await  DisplayAlert("Error", "No internet connection, please connect to internet and try again", "Okay");
        }

               
      
    }
}
