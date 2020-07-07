using AfriLearn.Models;
using AfriLearnMobile.Models;
using Akavache;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AfriLearn.ViewModels
{
    class ForumViewModel : BaseViewModel
    {
        /// <summary>
        /// fields
        /// </summary>
        private ObservableCollection<Message> messages;

        /// <summary>
        /// constructors
        /// </summary>
        public ForumViewModel()
        {
            GetMessagesAsync();
        }


        /// <summary>
        /// properties
        /// </summary>
        public  string  MessageText { get; set; }

        public  ObservableCollection<Message>  Messages
        {
            get { return  messages; }
            set 
            {
                messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private async void GetMessagesAsync()
        {
            try
            {
                 var localMessages = await BlobCache.LocalMachine.GetObject<ObservableCollection<Message>>("messages");
                 Messages =  localMessages;
            }
            catch (Exception)
            {

                 
            }
           
        }

        public ICommand AskQuestionCommand => new Command(async () =>
        {
            var sender = await BlobCache.InMemory.GetObject<AppUser>("appUser");
            var localmessage = new Message
            {
                DateAndTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), 
                Text = MessageText,
                Sender = sender.UserName
            };
           
            try
            {
                var messages = await BlobCache.LocalMachine.GetObject<ObservableCollection<Message>>("messages");
                messages.Add(localmessage);
                Messages = messages;
                await BlobCache.LocalMachine.InsertObject<ObservableCollection<Message>>("messages", messages);
            }
            catch (Exception)
            {
                var messages = new ObservableCollection<Message>();
                messages.Add(localmessage);
                Messages = messages;
                await BlobCache.LocalMachine.InsertObject<ObservableCollection<Message>>("messages", messages);
            }
        });


    }
}
