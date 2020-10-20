namespace AfriLearnBackend.Models
{
    public class Setting : BaseModel
    {
        public  bool IsAppNotificationsOn { get; set; }
        public  bool IsMarkettingNotificationsOn { get; set; }
        public  bool IsNightModeOn { get; set; }
    }
}
