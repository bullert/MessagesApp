using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prism.Commands;
using System.ComponentModel;
using System.Globalization;

namespace MessagesApp.ViewModels
{
    public class MultiValueConverter : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LoginViewModel2 : ValidatableBindableBase
    {
        private NavigationViewModel _navigationVM;

        public LoginViewModel2(NavigationViewModel navigationVM)
        {
            _navigationVM = navigationVM;
            //EmailLostFocusCmd = new DelegateCommand(OnEmailLostFocus, () => true).ObservesProperty(() => Email);
            //PasswordLostFocusCmd = new DelegateCommand(OnPasswordLostFocus, () => true).ObservesProperty(() => Password);
            //ValidateCmd = new DelegateCommand(OnValidate, CanValidate).ObservesProperty(() => Email).ObservesProperty(() => Password);
            //ValidateCmd = new DelegateCommand<object>(OnValidate);
            ValidateCmd = new DelegateCommand<object>(OnValidate).ObservesProperty(() => Email).ObservesProperty(() => IsValidate);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private bool isValidate = true;
        public bool IsValidate
        {
            get => isValidate;
            set => SetProperty(ref isValidate, value);
        }

        //private string password = "";
        //[Required(ErrorMessage = "This field cannot be empty.")]
        //public string Password
        //{
        //    get => password;
        //    set => SetProperty(ref password, value);
        //}

        public DelegateCommand<object> ValidateCmd { get; private set; }
        //public DelegateCommand EmailLostFocusCmd { get; private set; }
        //public DelegateCommand PasswordLostFocusCmd { get; private set; }

        //public void OnEmailLostFocus()
        //{
        //    //if (string.IsNullOrEmpty(Email)) Email = string.Empty;
        //    ValidateProperty("Email", Email);
        //}

        //public void OnPasswordLostFocus()
        //{
        //    if (string.IsNullOrEmpty(Password)) Password = string.Empty;
        //}

        //public void OnValidate()
        //{
        //    //OnEmailLostFocus();
        //    //OnPasswordLostFocus();

        //    if (!CanValidate()) return;
        //}

        public void OnValidate(object parameter)
        {
            var passwrodBox = parameter as System.Windows.Controls.PasswordBox;
            string passwrod = passwrodBox.Password;
            
            if (email == "bartek" && passwrod == " ")
            {
                IsValidate = true;
                //System.Windows.MessageBox.Show("OK");
                _navigationVM.Index = 2;
            }
            else
            {
                IsValidate = false;
                System.Windows.MessageBox.Show(email + " " + passwrod);
            }
        }

        //public void OnValidate(object parameters)
        //{
        //    var values = parameters as object[];
        //    var val0 = values[0] as System.Windows.Controls.TextBox;
        //    var val1 = values[1] as System.Windows.Controls.PasswordBox;

        //    string email = val0.Text;
        //    string passwrod = val1.Password;
        //    System.Windows.MessageBox.Show(email + " " + passwrod);

        //    //System.Windows.MessageBox.Show(parameters.ToString());
        //}

        //public bool CanValidate()
        //{
        //    return !HasErrors;
        //}
    }
}
