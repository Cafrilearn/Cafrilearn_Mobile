using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassroomPage : ContentPage
    {
        public ClassroomPage()
        {
            InitializeComponent();
        }

        //[Obsolete]
        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //   // Navigation.PushAsync(new  ReadPageTest3());
        //    Uri uri= new Uri("https://afrilearn.blob.core.windows.net/books/SCIENCE%20-%20PLANTS.pdf?sv=2019-10-10&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-06-16T13:54:23Z&sig=L8U56BajpVZOdkQy0tZVz2MfWVJVue8BKtFdAonHv3E%3D&_=1592287045127");
        //    Device.OpenUri(uri);
        //}
    }
}