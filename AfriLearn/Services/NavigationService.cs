using System.Threading.Tasks;

namespace AfriLearn.Services
{
    static class NavigationService
    {
        public static async void PushAsync(Xamarin.Forms.Page page)
        {
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        public static async void PopAsync()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public static async void DisplayAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        public static async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
           return await App.Current.MainPage.DisplayAlert(title, message, ok, cancel);
            
        }
    }
}
