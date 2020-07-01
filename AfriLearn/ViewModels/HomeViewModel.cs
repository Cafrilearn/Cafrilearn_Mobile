using AfriLearn.Services;
using AfriLearn.Views;
using Akavache;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
		public HomeViewModel()
		{
			
		}
				
		public ICommand NavigateToSettingsPageCommand => new Command(() =>
		{
			NavigationService.PushAsync(new SettingPage());
		});

		public ICommand NavigateToProfilePageCommand => new Command(() =>
		{
			NavigationService.PushAsync(new ProfilePage());
		});

		public ICommand LogOutCommand => new Command(() =>
		{
			SignInSignOutService.SignOut();
		});
		public async void GetAllBookNames()
        {
			// get book names from the containers
			var scienceBooks = await AzureBlobStorageService.GetFilesListAsync("science");
			var englishBooks = await AzureBlobStorageService.GetFilesListAsync("english");
			var mathBooks = await AzureBlobStorageService.GetFilesListAsync("mathematics");
			var kiswahiliBooks = await AzureBlobStorageService.GetFilesListAsync("kiswahili");
			var peBooks = await AzureBlobStorageService.GetFilesListAsync("physical-education");
			var socialStudiesBooks = await AzureBlobStorageService.GetFilesListAsync("social-studies");
			
			// store all the names in one list
			List<string> allBooks = null;
			allBooks.AddRange(scienceBooks);
			allBooks.AddRange(englishBooks);
			allBooks.AddRange(mathBooks);
			allBooks.AddRange(kiswahiliBooks);
			allBooks.AddRange(peBooks);
			allBooks.AddRange(socialStudiesBooks);

			BlobCache.LocalMachine.InsertObject<List<string>>("allBookNames", allBooks);

		}


	}
}
