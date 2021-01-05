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

        private void BindableItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            var bookRelativePath = listView.SelectedItem.ToString();
            var absPathStart = bookRelativePath.LastIndexOf('/');
            var bookName = bookRelativePath.Substring(absPathStart + 1);
            var exploreVM = new ExploreBooksViewModel();
            exploreVM.BookSelected(bookName);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= BindableItemTapped;
        }
    }
}
