using MessagesApp.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prism.Commands;

namespace MessagesApp.ViewModels
{
    public class RegisterViewModel : ValidatableBindableBase
    {
        static string defaultBrush;
        static string errorBrush;

        public RegisterViewModel()
        {
            defaultBrush = (System.Windows.Application.Current.FindResource("PrimaryHueMidBrush") as System.Windows.Media.SolidColorBrush).ToString();
            errorBrush = (System.Windows.Application.Current.FindResource("ValidationErrorBrush") as System.Windows.Media.SolidColorBrush).ToString();

            PasswordErrorBrush = defaultBrush;
            ConfirmPasswordErrorBrush = defaultBrush;

            ChangeSlideCmd = new DelegateCommand<object>(OnSlideChanged).ObservesProperty(() => SlideIndex);
            EmailLostFocusCmd = new DelegateCommand(OnEmailChanged).ObservesProperty(() => Email);
            //PasswordLostFocusCmd = new DelegateCommand<object>(OnPasswordChanged);
            //ConfirmPasswordLostFocusCmd = new DelegateCommand<object>(OnConfirmPasswordChanged);
            PasswordLostFocusCmd = new DelegateCommand<object>(OnPasswordChanged, CanPasswordExecute);
            ConfirmPasswordLostFocusCmd = new DelegateCommand<object>(OnConfirmPasswordChanged, CanConfirmPasswordExecute);

            ValidateCmd = new DelegateCommand<object>(OnValidate, CanValidate).ObservesProperty(() => Email);
        }

        public DelegateCommand<object> ChangeSlideCmd { get; set; }
        public DelegateCommand EmailLostFocusCmd { get; set; }
        public DelegateCommand<object> PasswordLostFocusCmd { get; set; }
        public DelegateCommand<object> ConfirmPasswordLostFocusCmd { get; set; }
        public DelegateCommand<object> ValidateCmd { get; set; }

        #region Properties

        public System.Windows.Media.Color a { get; set; } = System.Windows.Media.Color.FromArgb(222, 255, 0, 0);

        private int slideIndex = 0;
        public int SlideIndex
        {
            get => slideIndex;
            set => SetProperty(ref slideIndex, value);
        }

        private string email;
        [Required(ErrorMessage = "This field cannot be empty.")]
        [EmailAddress(ErrorMessage = "This doesn't look like an email address.")]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public int MinPasswordLength { get; set; } = 8;
        public int MaxPasswordLength { get; set; } = 30;

        private string passwordErrorContent;
        public string PasswordErrorContent
        {
            get => passwordErrorContent;
            set => SetProperty(ref passwordErrorContent, value);
        }

        private string passwordErrorBrush;
        public string PasswordErrorBrush
        {
            get => passwordErrorBrush;
            set => SetProperty(ref passwordErrorBrush, value);
        }

        private string confirmPasswordErrorContent;
        public string ConfirmPasswordErrorContent
        {
            get => confirmPasswordErrorContent;
            set => SetProperty(ref confirmPasswordErrorContent, value);
        }

        private string confirmPasswordErrorBrush;
        public string ConfirmPasswordErrorBrush
        {
            get => confirmPasswordErrorBrush;
            set => SetProperty(ref confirmPasswordErrorBrush, value);
        }

        #endregion

        private void OnSlideChanged(object parameter)
        {
            SlideIndex = int.Parse(parameter.ToString());
            //System.Windows.MessageBox.Show(SlideIndex.ToString());
        }

        private void OnEmailChanged()
        {
            ValidateProperty("Email", Email);
        }

        private void OnPasswordChanged(object parameter)
        {
            PasswordErrorContent = null;
            PasswordErrorBrush = defaultBrush;
        }

        private bool CanPasswordExecute(object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            string password = passwordBox.Password;

            PasswordErrorBrush = errorBrush;

            bool isValid = false;

            string smallLetters = "^[a-z]+$";
            string capitalLetters = "^[A-Z]+$";
            string digits = "^[0-9]+$";
            string specialSymbols = @"^[\s!@#$%^&*()`~\-_=+\[\{\]\}\\|;:'\"",<.>/?]+$";
            
            var smallLettersPattern = new System.Text.RegularExpressions.Regex(smallLetters);
            var capitalLettersPattern = new System.Text.RegularExpressions.Regex(capitalLetters);
            var digitsPattern = new System.Text.RegularExpressions.Regex(digits);
            var specialSymbolsPattern = new System.Text.RegularExpressions.Regex(specialSymbols);
            var mainPattern = new System.Text.RegularExpressions.Regex($"{smallLetters}|{capitalLetters}|{digits}|{specialSymbols}");
            
            if (string.IsNullOrEmpty(password))
            {
                PasswordErrorContent = "This field cannot be empty.";
            }
            //else if (password.Length < MinPasswordLength)
            //{
            //    PasswordErrorContent = $"Password must be at least {MinPasswordLength} characters.";
            //}
            //else if (password.Length > MaxPasswordLength)
            //{
            //    PasswordErrorContent = $"Password must be maximum {MaxPasswordLength} characters.";
            //}
            //else if (!mainPattern.Match(password).Success)
            //{
            //    PasswordErrorContent = $"Use only letters, numbers, spaces and allowed special characters.";
            //}
            else
            {
                short complexity = 0;
                bool hasSmallLetters = false;
                bool hasCapitalLetters = false;
                bool hasNumbers = false;
                bool hasSymbols = false;

                foreach (var character in password)
                {
                    string strCharacter = character.ToString();
                    if (smallLettersPattern.Match(strCharacter).Success) hasSmallLetters = true;
                    if (capitalLettersPattern.Match(strCharacter).Success) hasCapitalLetters = true;
                    if (digitsPattern.Match(strCharacter).Success) hasNumbers = true;
                    if (specialSymbolsPattern.Match(strCharacter).Success) hasSymbols = true;
                }

                if (hasSmallLetters == true) complexity++;
                if (hasCapitalLetters == true) complexity++;
                if (hasNumbers == true) complexity++;
                if (hasSymbols == true) complexity++;

                //ConfirmPasswordErrorContent = complexity.ToString();

                if (complexity < 3)
                {
                    PasswordErrorContent = $"Create a stronger password - use a combination of letters, numbers and symbols.";
                }
                else
                {
                    isValid = true;
                }
            }
            
            return isValid;
        }

        private void OnConfirmPasswordChanged(object parameters)
        {
            ConfirmPasswordErrorContent = null;
            ConfirmPasswordErrorBrush = defaultBrush;
        }

        private bool CanConfirmPasswordExecute(object parameters)
        {
            //var a = (parameters as object[])[1];
            //var b = a as System.Windows.Controls.PasswordBox;

            var values = parameters as object[];
            var PasswdBox = values[0] as System.Windows.Controls.PasswordBox;
            var ConfirmPasswdBox = values[1] as System.Windows.Controls.PasswordBox;
            string password = PasswdBox.Password;
            string confirmation = ConfirmPasswdBox.Password;

            ConfirmPasswordErrorBrush = errorBrush;

            if (PasswordErrorContent != null) return true;

            if (!password.Equals(confirmation))
            {
                ConfirmPasswordErrorContent = "The passwords are not identical.";
                return false;
            }

            return true;
        }

        private void OnValidate(object parameters)
        {
            var values = parameters as object[];
            var PasswdBox = values[0] as System.Windows.Controls.PasswordBox;
            var ConfirmPasswdBox = values[1] as System.Windows.Controls.PasswordBox;

            EmailLostFocusCmd.Execute();
            PasswordLostFocusCmd.CanExecute(PasswdBox);
            ConfirmPasswordLostFocusCmd.CanExecute(values);

            string passwrod = PasswdBox.Password;
            string confirmation = ConfirmPasswdBox.Password;
            
            if (!HasErrors)
            {
                System.Windows.MessageBox.Show("ok");
            }
        }

        private bool CanValidate(object parameters)
        {
            return true;
        }

        public new bool HasErrors
        {
            get => (base.HasErrors || PasswordErrorContent != null || ConfirmPasswordErrorContent != null);
        }

        //public string Password
        //{
        //    get
        //    {
        //        return password;
        //    }
        //    set
        //    {
        //        password = value;
        //        OnPropertyChanged("Password");
        //    }
        //}

        //public string PasswdCrypt
        //{
        //    get
        //    {
        //        return passwdCrypt;
        //    }
        //    set
        //    {
        //        passwdCrypt = value;
        //        OnPropertyChanged("PasswdCrypt");
        //    }
        //}

        //public string Hash
        //{
        //    get
        //    {
        //        return hash;
        //    }
        //    set
        //    {
        //        hash = value;
        //        OnPropertyChanged("Hash");
        //    }
        //}

        //public string Length
        //{
        //    get
        //    {
        //        return length;
        //    }
        //    set
        //    {
        //        length = value;
        //        OnPropertyChanged("Length");
        //    }
        //}

        //private string password;
        //public string Password
        //{
        //    get => password;
        //    set => SetProperty(ref password, value);
        //}

        //private string passwdCrypt;
        //public string PasswdCrypt
        //{
        //    get => passwdCrypt;
        //    set => SetProperty(ref passwdCrypt, value);
        //}

        //private string hash;
        //public string Hash
        //{
        //    get => hash;
        //    set => SetProperty(ref hash, value);
        //}

        //private string length;
        //public string Length
        //{
        //    get => length;
        //    set => SetProperty(ref length, value);
        //}

        //private void Calc(object param)
        //{
        //    //PasswdCrypt = Password;

        //    string password = Password;

        //    string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //    string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        //    PasswdCrypt = salt;
        //    Hash = hash;

        //    bool match = BCrypt.Net.BCrypt.Verify(password, hash);

        //    Length = match.ToString();
        //}

        //private void Calc(object param)
        //{
        //    //PasswdCrypt = Password;

        //    Console.Write("Enter a password: ");
        //    //string password = Console.ReadLine();
        //    string password = Password;

        //    // generate a 128-bit salt using a secure PRNG
        //    byte[] salt = new byte[128 / 8];

        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(salt);
        //    }

        //    PasswdCrypt = $"Salt: {Convert.ToBase64String(salt)}";

        //    // derive a 256-bit subkey (use HMACSHA512 with 10,000 iterations)
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA512,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));
        //    //Console.WriteLine($"Hashed: {hashed}");
        //    Hash = $"Hashed: {hashed}";
        //    Length = PasswdCrypt.Length + " " + Hash.Length + " ";
        //}
    }
}
