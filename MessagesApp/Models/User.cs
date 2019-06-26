//using DevExpress.Mvvm;
using MessagesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagesApp.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

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
