using AfriLearnMobile.ViewModels.ChatViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace  AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumPage : ContentPage
    {
        ChatViewModel vm;
        public ForumPage()
        {
            InitializeComponent();
            BindingContext = vm = new ChatViewModel();
            var target = vm.Messages[vm.Messages.Count - 1];
            MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            vm.Messages.CollectionChanged += (sender, e) =>
            {
                target = vm.Messages[vm.Messages.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            };
        }
        void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }
      
    }
}