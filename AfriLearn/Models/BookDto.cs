namespace AfriLearn.Models
{
    class BookDto : BaseModel
    {
       public string BookName { get; set; }
        public string BookUri { get; set; }
        public string ContainerType { get; set; }
        public string BookTitle { get; set; }
        public string CoverImagePhotoUri { get; set; }
        public string Author { get; set; }
    }
}
