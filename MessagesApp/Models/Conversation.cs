using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using MessagesApp.Models;
using System.Collections.ObjectModel;

namespace MessagesApp.Models
{
    public class Conversation : BindableBase
    {
        //public ObservableCollection<Message> messagesList;
        public User user;

        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages
        {
            get => messages;
            //set => SetProperty(ref messages, value);
        }

        public Conversation()
        {
            messages = new ObservableCollection<Message>();
            //messagesList = new ObservableCollection<Message>();
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            //messagesList.Add(message);
            UpdateStyle(Messages.Count - 1);
            UpdateStyle(Messages.Count - 2);
            //Messages = messagesList;
        }

        public void PrependMessage(Message message)
        {
            Messages.Insert(0, message);
            UpdateStyle(0);
            UpdateStyle(1);
        }

        private void UpdateStyle(int index)
        {
            if (IsOutOfRange(index)) return;

            string author = Messages[index].Author.FirstName;
            string style = (author != user.FirstName) ? "Left" : "Right";

            if (IsFirst(index, author) && IsLast(index, author)) style += "SingleMessageStyle";
            else if (IsFirst(index, author)) style += "TopMessageStyle";
            else if (IsLast(index, author)) style += "BottomMessageStyle";
            else style += "MiddleMessageStyle";

            Messages[index].Style = style;
        }

        //private void UpdateStyle(int index, int step = 2)
        //{
        //    if (IsOutOfRange(index) || step < 1) return;

        //    string author = messagesList[index].Author.FirstName;
        //    string style = (author != user.FirstName) ? "Left" : "Right";

        //    if (IsFirst(index, author) && IsLast(index, author)) style += "SingleMessageStyle";
        //    else if (IsFirst(index, author)) style += "TopMessageStyle";
        //    else if (IsLast(index, author)) style += "BottomMessageStyle";
        //    else style += "MiddleMessageStyle";

        //    messagesList[index].Style = style;

        //    UpdateStyle(index - 1, step--);
        //}

        private bool IsOutOfRange(int index)
        {
            return (index < 0 || index >= Messages.Count) ? true : false;
        }

        private bool IsFirst(int index, string author)
        {
            if (index - 1 < 0) return true;
            if (Messages[index - 1].Author.FirstName != author) return true;
            return false;
        }

        private bool IsLast(int index, string author)
        {
            if (index + 1 >= Messages.Count) return true;
            if (Messages[index + 1].Author.FirstName != author) return true;
            return false;
        }
    }
}
