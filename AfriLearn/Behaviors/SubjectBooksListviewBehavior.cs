using AfriLearn.Services;
using AfriLearn.ViewModels;
using AfriLearn.Views;
using Xamarin.Forms;

namespace AfriLearn.Behaviors
{
    class SubjectBooksListviewBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemTapped += BindableItemTapped;
        }

        private void BindableItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = new BaseViewModel();
            vm.IsBusy = true;
            var listView = (ListView)sender;
            var bookName = listView.SelectedItem.ToString();
            NavigationService.PushAsync(new LoadingPage(bookName));
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= BindableItemTapped;
        }
    }
}
