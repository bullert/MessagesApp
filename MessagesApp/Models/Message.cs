using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MessagesApp.Models
{
    public class Message : BindableBase
    {
        public Message()
        {
            Date = new DateTime();
        }

        private User author;
        public User Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        private string style;
        public string Style
        {
            get => style;
            set => SetProperty(ref style, value);
        }
    }
}
