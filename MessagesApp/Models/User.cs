using MessagesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MessagesApp.Models
{
    public class User : BindableBase
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string Note { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string MessageProfilePictureUrl { get; set; }

        private bool isOnline = false;
        public bool IsOnline
        {
            get => isOnline;
            set => SetProperty(ref isOnline, value);
        }

        //private string firstName;
        //public string FirstName
        //{
        //    get => firstName;
        //    set
        //    {
        //        firstName = value;
        //        OnPropertyChanged("FirstName");
        //    }
        //}

        //private string lastName;
        //public string LastName
        //{
        //    get => lastName;
        //    set
        //    {
        //        lastName = value;
        //        OnPropertyChanged("LastName");
        //    }
        //}

        //private string email;
        //public string Email
        //{
        //    get => email;
        //    set => SetProperty(ref email, value);
        //}
    }
}
