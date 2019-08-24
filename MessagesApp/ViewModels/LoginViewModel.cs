//using MessagesApp.Commands;
using MessagesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Collections;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using Prism.Commands;
using System.Net.Mail;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using Microsoft.Win32;
using System.IO;

namespace MessagesApp.ViewModels
{
    public class Occupied : ValidationAttribute
    {
        public Occupied()
        { }

        public override bool IsValid(object value)
        {
            string strValue = value as string;
            if (strValue == "bartek")
            {
                return false;
            }
            return true;
        }
    }

    public class LoginViewModel : ValidatableBindableBase
    {
        private User user;

        public LoginViewModel()
        {
            user = new User
            {
                FirstName = "Bartek",
                LastName = "Bullert",
                Email = "b.bullert@gmail.com"
            };

            //ValidationCheckCommand = new RelayCommand(ValidationCheck2);
            ValidationCheckCommand = new DelegateCommand(onSave, canSave)
                .ObservesProperty(() => FirstName)
                .ObservesProperty(() => LastName)
                .ObservesProperty(() => Email);
        }

        public DelegateCommand ValidationCheckCommand { get; set; }
        //public RelayCommand ValidationCheckCommand { get; set; }

        private string firstName = "Barte";
        [RegularExpression("^[a-zA-Z0-9/#%&*\\- ]*$", ErrorMessage = "sfsfsdfs")]
        [MinLength(3, ErrorMessage = "min 1")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(5, ErrorMessage = "max 5")]
        [Occupied(ErrorMessage = "This name is already occupied.")]
        public string FirstName
        {
            get => firstName;
            set
            {
                SetProperty(ref firstName, value);
                //ValidateFirstName();
            }
        }

        private string lastName;
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string email;
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        
        public void ValidateFirstName()
        {
            var results = new List<string>();

            if (firstName.Length == 0) results.Add("cannot be empty");
            else if (firstName.Length < 2) results.Add("cannot be less than 2");

            errors["FirstName"] = results;
        }

        public void onSave()
        {
            //FirstName = FirstName;
            //LastName = LastName;
            //Email = Email + "";
            Email = Email + "";
            //if (canSave()) System.Windows.MessageBox.Show("dasfaf");
            //SendMail(Email);
        }

        public bool canSave()
        {
            //FirstName = FirstName;
            //LastName = LastName;
            //Email = Email;
            return !this.HasErrors;
        }

        private static void SendMail(string emailAddres)
        {
            var from = new MailAddress("messagesapp2019@gmail.com", "MessagesApp");
            var to = new MailAddress(emailAddres);
            const string subject = "MessagesApp - Sign up confirmation";
            const string body = "Welcome, this is a test email.";
            SecureString password = new SecureString();

            foreach (var c in "zaq1@WSX")
            {
                password.AppendChar(c);
            }
            
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(from.Address, password)
            };

            using (var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body
            })
            {
                client.Send(message);
            }
        }

        //public bool canSave()
        //{
        //    return !HasErrors;
        //}

        //private bool isValid;
        //public bool IsValid
        //{
        //    get => isValid;
        //    set => SetProperty(ref isValid, value);
        //}

        //event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        //{
        //    add
        //    {
        //        throw new NotImplementedException();
        //    }

        //    remove
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //bool INotifyDataErrorInfo.HasErrors => throw new NotImplementedException();

        //IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        //{
        //    throw new NotImplementedException();
        //}

        //#region IDataErrorInfo Members

        //public string Error
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        return GetValidationError(columnName);
        //    }
        //}

        //#endregion

        #region Validation

        //static readonly string[] ValidatedProperites =
        //{
        //    "FirstName"
        //};

        //public void ValidationCheck(object param)
        //{
        //    bool hasError = false;
        //    foreach (var property in ValidatedProperites)
        //    {
        //        if (GetValidationError(property) != null)
        //        {
        //            hasError = true;
        //            break;
        //        }
        //    }

        //    //IsValid = !hasError;
        //}

        //public string GetValidationError(string columnName)
        //{
        //    string error = null;

        //    switch (columnName)
        //    {
        //        case "FirstName":
        //            error = ValidateUserName();
        //            break;
        //    }

        //    return error;
        //}

        //public string ValidateUserName()
        //{
        //    if (string.IsNullOrWhiteSpace(FirstName))
        //    {
        //        return "User name cannot be empty.";
        //    }

        //    return null;
        //}

        #endregion
    }
}
