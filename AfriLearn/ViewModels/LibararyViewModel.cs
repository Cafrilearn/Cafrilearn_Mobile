namespace AfriLearn.ViewModels
{
    class LibararyViewModel : BaseViewModel
    {
        private bool headerTextVisibility = true;
        public LibararyViewModel()
        {

        }
        
        // change its value to false once the collection view is populated with data
        public  bool HeaderTextVisibility
        {
            get { return headerTextVisibility; }
            set 
            {
                headerTextVisibility = value;
                OnPropertyChanged(nameof(HeaderTextVisibility));
            }
        }


    }
}
