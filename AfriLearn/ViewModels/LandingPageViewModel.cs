using AfriLearn.Services;
using AfriLearn.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class LandingPageViewModel : BaseViewModel
    {
        public ICommand GoToClassRoom => new Command(() => NavigationService.PushAsync(new ExploreBooksPage()));
        public ICommand GoToReviewsPage => new Command(() => NavigationService.PushAsync(new ReviewsPage()));
        public ICommand GoToDiscussionPage => new Command(() => NavigationService.PushAsync(new ForumPage()));
        
        // change this last one to a page to explore non curriculum books
        public ICommand GoToExploreBooksPage => new Command(() => NavigationService.PushAsync(new ExploreBooksPage()));
    }
}
