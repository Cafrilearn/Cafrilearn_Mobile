using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Xamarin.Forms.TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>()
                .SetToolbarPlacement(ToolbarPlacement.Bottom)
                .SetOffscreenPageLimit(4)
                .SetIsSwipePagingEnabled(true)
                .EnableSmoothScroll();         
        }
    }
}