using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagesApp.ViewModels;
using Prism.Mvvm;

namespace MessagesApp.Models
{
    public class Conversation2 : BindableBase
    {
        private User loggedUser = new User { Id = 1, FirstName = "ja" };

        public Conversation2()
        {
            Users = new ObservableCollection<User>();
            //SeenBy = new ObservableCollection<User>();
            Messages = new ObservableCollection<MessageViewModel>();
        }

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get => users;
            private set => SetProperty(ref users, value);
        }

        //private ObservableCollection<User> seenBy;
        //public ObservableCollection<User> SeenBy
        //{
        //    get => seenBy;
        //    private set => SetProperty(ref seenBy, value);
        //}

        private ObservableCollection<MessageViewModel> messages;
        public ObservableCollection<MessageViewModel> Messages
        {
            get => messages;
            private set => SetProperty(ref messages, value);
        }

        private DateTime lastDateUpdate;
        public DateTime LastDateUpdate
        {
            get => lastDateUpdate;
            set => SetProperty(ref lastDateUpdate, value);
        }


        public void AppendMessage(MessageViewModel mvm)
        {
            if ((mvm.Date - LastDateUpdate).TotalSeconds > 300)
            {
                AppendMessage2(new MessageViewModel { CreatedDate = mvm.CreatedDate });
                //LastDateUpdate = mvm.Date;
            }
            LastDateUpdate = mvm.Date;
            int index = Messages.Count;
            Messages.Add(mvm);
            UpdateTemplate(index - 1);
            UpdateTemplate(index);

            foreach (var x in Users)
                mvm.SeenBy.Add(new SeenByItem { User = x });
        }

        public void PrependMessages(MessageViewModel mvm)
        {
            Messages.Insert(0, mvm);
            UpdateTemplate(0);
            UpdateTemplate(1);
            if ((mvm.Date - LastDateUpdate).TotalSeconds > 300)
            {
                PrependMessages2(new MessageViewModel { CreatedDate = mvm.CreatedDate });
            }
            LastDateUpdate = mvm.Date;
        }

        private void AppendMessage2(MessageViewModel mvm)
        {
            int index = Messages.Count;
            Messages.Add(mvm);
            UpdateTemplate(index - 1);
            UpdateTemplate(index);
        }

        private void PrependMessages2(MessageViewModel mvm)
        {
            Messages.Insert(0, mvm);
            UpdateTemplate(0);
            UpdateTemplate(1);
        }

        private void UpdateTemplate(int index)
        {
            if (!IsExist(index)) return;
            
            short templateIndex = 0;
            string template;
            string side = null;

            var message = Messages[index];
            //if (message.MessageTemplate == "DateHeadlineTemplate") return;

            if (message.Author != null)
            {
                side = (message.Author.Id == loggedUser.Id) ? "Right" : "Left";
                templateIndex++;
            }

            if (IsExist(index + 1))
            {
                var nextMessage = Messages[index + 1];
                if (message.Author != null && nextMessage.Author != null)
                {
                    if (nextMessage.Author.Id == message.Author.Id) templateIndex++;
                }
                message.IsLast = false;
            }
            else message.IsLast = true;

            if (IsExist(index - 1))
            {
                var prevMessage = Messages[index - 1];
                if (message.Author != null && prevMessage.Author != null)
                {
                    if (prevMessage.Author.Id == message.Author.Id) templateIndex += 2;
                }
                message.IsFirst = false;
            }
            else message.IsFirst = true;

            //if (message.Author == null) templateIndex = 0;

            switch (templateIndex)
            {
                case 1: template = "MessageTemplate"; break;
                case 2: template = "TopMessageTemplate"; break;
                case 3: template = "BottomMessageTemplate"; break;
                case 4: template = "MiddleMessageTemplate"; break;
                default: template = "DateHeadlineTemplate"; break;
            }

            message.MessageTemplate = side + template;
        }

        private bool IsExist(int index)
        {
            return (index >= 0 && index < Messages.Count) ? true : false;
        }
    }
}
