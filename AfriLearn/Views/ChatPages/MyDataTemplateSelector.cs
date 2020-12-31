using AfriLearn.Models;
using Xamarin.Forms;

namespace CMapp_Mobile.Views.ChatPages
{
    class MyDataTemplateSelector : DataTemplateSelector
    {
        #region fields
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        #endregion
        public MyDataTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;
            return messageVm.IsMine ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }
    }
}
