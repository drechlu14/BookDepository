using BookDepositoryApp.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookDepositoryApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Registration : Window
    {
        ObservableCollection<User> users;

        public Registration()
        {
            InitializeComponent();
        }

        public void RegistrationMethode()
        {
            string name = UsernameRegistration.Text;
            string password = PasswordRegistration.Text;
            string passwordCheck = PasswordCheckRegistration.Text;
            bool test = false;

            if (name != "" & password != "" & passwordCheck != "")
            {
                users = new ObservableCollection<User>(DatabaseUser.GetUsers().Result);
                //var userCount = App.UserDatabase.GetUsers().Result;
                foreach (var myUser in users)
                {
                    if (myUser.Name == name)
                    {
                        test = true;
                    }

                }
                if (!test)
                {
                    if (password == passwordCheck)
                    {
                        string passwordHash = GetStringSha256Hash(password);
                        User user = new User();
                        user.Done = 0;
                        user.Name = name;
                        user.Password = passwordHash;                      
                        DatabaseUser.SaveItemAsync(user);
                        Error.Content = "Account was successfully created!";
                        UsernameRegistration.Text = "";
                        PasswordRegistration.Text = "";
                        PasswordCheckRegistration.Text = "";
                    }
                    else
                    {
                        Error.Content = "Passwords doesn't match!";
                    }
                }
                else
                {
                    Error.Content = "Account already exists!";
                }

            }
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        private static UserDatabase _userDatabase;
        public static UserDatabase DatabaseUser
        {
            get
            {
                if (_userDatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _userDatabase = new UserDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _userDatabase;
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Login customization = new Login();
            customization.Show();
            this.Close();
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            Error.Content = "";
            RegistrationMethode();
        }
    }
}
