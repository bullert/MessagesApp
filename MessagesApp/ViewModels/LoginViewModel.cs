using MessagesApp.Commands;
using MessagesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MessagesApp.ViewModels
{
    public class LoginViewModel : BindableBase, IDataErrorInfo
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

            //ValidationCheckCommand = new RelayCommand(ValidationCheck);
        }

        public RelayCommand ValidationCheckCommand { get; set; }


        public string FirstName
        {
            get => user.FirstName;
            set
            {
                user.FirstName = value;
                RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get => user.LastName;
            set
            {
                user.LastName = value;
                RaisePropertyChanged();
            }
        }
        
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                RaisePropertyChanged();
            }
        }

        private bool isValid;
        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return GetValidationError(columnName);
            }
        }

        #endregion

        #region Validation

        static readonly string[] ValidatedProperites =
        {
            "FirstName"
        };

        public void ValidationCheck(object param)
        {
            //System.Windows.Application.Current.Shutdown();
            //System.Windows.MessageBox.Show("fsfsdafs");
            bool hasError = false;
            foreach (var property in ValidatedProperites)
            {
                if (GetValidationError(property) != null)
                {
                    hasError = true;
                    break;
                }
            }

            IsValid = !hasError;
            OnPropertyChanged("IsValid");
        }

        public string GetValidationError(string columnName)
        {
            string error = null;

            switch (columnName)
            {
                case "FirstName":
                    error = ValidateUserName();
                    break;
            }

            return error;
        }

        public string ValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return "User name cannot be empty.";
            }

            return null;
        }

        #endregion
    }
}
