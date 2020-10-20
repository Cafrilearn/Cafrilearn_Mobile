using CMapp.Models;

namespace AfriLearnBackend.Models
{
    public class Help : BaseModel
    {
        public string HelpName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string HelpLevel { get; set; }
        public  string  UserId { get; set; }
    }
}
