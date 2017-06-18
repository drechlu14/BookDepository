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
    public partial class Login : Window
    {
        ObservableCollection<Customer> users;

        public Login()
        {
            InitializeComponent();
        }

        public void LoginMethode()
        {
            string name = UsernameLogin.Text;
            string password = PasswordLogin.Text;
            string passDat = "";
            int ID = 0;
            bool test = false;
            //int decision = 0;

            if (name != "" & password != "")
            {
                users = new ObservableCollection<Customer>(Database.GetCustomers().Result);
                foreach (var myUser in users)
                {
                    if (myUser.Name == name)
                    {
                        ID = myUser.ID;
                        test = true;
                        passDat = myUser.Password;
                        //decision = house.Choice;
                    }
                }
                if (test)
                {
                    string passHash = GetStringSha256Hash(password);
                    if (passHash == passDat)
                    {
                        MyStaticValues.login = true;
                        MainWindow Page = new MainWindow();
                        Page.Show();
                        this.Close();
                    }
                    else
                    {
                        Error.Content = "Wrong password.";
                    }
                }
                else
                {
                    Error.Content = "Check your data.";
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

        private static CustomerDatabase _database;
        public static CustomerDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new CustomerDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow customization = new MainWindow();
            customization.Show();
            this.Close();
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            Registration customization = new Registration();
            customization.Show();
            this.Close();
        }

        public static class MyStaticValues
        {
            public static bool login { get; set; }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginMethode();
            MyStaticValues.login = true;
        }
    }
}
