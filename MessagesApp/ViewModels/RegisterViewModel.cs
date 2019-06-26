using MessagesApp.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessagesApp.ViewModels
{
    class RegisterViewModel : BindableBase
    {
        public RegisterViewModel()
        {
            Update = new RelayCommand(Calc);
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

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string passwdCrypt;
        public string PasswdCrypt
        {
            get => passwdCrypt;
            set => SetProperty(ref passwdCrypt, value);
        }

        private string hash;
        public string Hash
        {
            get => hash;
            set => SetProperty(ref hash, value);
        }

        private string length;
        public string Length
        {
            get => length;
            set => SetProperty(ref length, value);
        }

        public RelayCommand Update { get; set; }

        private void Calc(object param)
        {
            //PasswdCrypt = Password;
            
            string password = Password;

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            PasswdCrypt = salt;
            Hash = hash;

            bool match = BCrypt.Net.BCrypt.Verify(password, hash);

            Length = match.ToString();
        }

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
