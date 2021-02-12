using AfriLearn.ViewModels;
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

        private async void BindableItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = new BaseViewModel();
            vm.IsBusy = true;
            vm.MainContentVisibility = false;
            var listView = (ListView)sender;
            var  bookName = listView.SelectedItem.ToString();
            var exploreVM = new SubjectsViewModel();
            await exploreVM.GetBook(bookName);
            vm.IsBusy = false;
            vm.MainContentVisibility = true;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= BindableItemTapped;
        }
    }
}
