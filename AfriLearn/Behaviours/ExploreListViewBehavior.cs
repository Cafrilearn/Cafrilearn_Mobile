using AfriLearn.Models;
using AfriLearn.ViewModels;
using Xamarin.Forms;

namespace AfriLearn.Behaviours
{
    class ExploreListViewBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemTapped += BindableItemTapped;
        }

        private void BindableItemTapped(object sender, ItemTappedEventArgs e)
        {
           var listView = (ListView)sender;
            var book = (Book)listView.SelectedItem;
           ExploreViewModel.BookSelected(book.BookTitle);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= BindableItemTapped;
        }
    }
}
