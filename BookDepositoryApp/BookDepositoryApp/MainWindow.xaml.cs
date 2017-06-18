using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static BookDepositoryApp.Login;
using static System.Net.Mime.MediaTypeNames;

namespace BookDepositoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Book> itemsFromDb;
        ObservableCollection<Customer> itemsFromDb2;

        public MainWindow()
        {
            InitializeComponent();

            itemsFromDb = new ObservableCollection<Book>(Database.GetBooks().Result);
            itemsFromDb2 = new ObservableCollection<Customer>(CDatabase.GetCustomers().Result);
            if (itemsFromDb.Count < 3)
            {
                Book book = new Book();
                book.Name = "Harry Potter 1";
                book.Author = "J. K. Rowling";
                book.Genre = "Fantasy";
                book.Page = 222;
                book.ISBN = "8119898198189";
                book.Price = 274;
                book.Done = 0;
                Database.SaveItemAsync(book);

                Book book1 = new Book();
                book1.Name = "Harry Potter 2";
                book1.Author = "J. K. Rowling";
                book1.Genre = "Fantasy";
                book1.Page = 258;
                book1.ISBN = "8119898198180";
                book1.Price = 282;
                book1.Done = 0;
                Database.SaveItemAsync(book1);

                Book book2 = new Book();
                book2.Name = "The Kite Runner";
                book2.Author = "Khaled Hosseini";
                book2.Genre = "Drama";
                book2.Page = 360;
                book2.ISBN = "9788085336399";
                book2.Price = 300;
                book2.Done = 0;
                Database.SaveItemAsync(book2);
            }

            ItemsCount.Content = "Items in Database " + itemsFromDb.Count;
            ToDoItemsListView.ItemsSource = itemsFromDb;
            CustomerView.ItemsSource = itemsFromDb2;
            //LoggedCustomer.Text = itemsFromDb2.ToString();

            if (MyStaticValues.login)
            {
                buttonLogin.Visibility = Visibility.Hidden;
                buttonLogout.Visibility = Visibility.Visible;
            }
            if (!MyStaticValues.login)
            {
                buttonLogin.Visibility = Visibility.Visible;
                buttonLogout.Visibility = Visibility.Hidden;
            }

        }

        private static BookDatabase _database;
        public static BookDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new BookDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }

        private static CustomerDatabase _Cdatabase;
        public static CustomerDatabase CDatabase
        {
            get
            {
                if (_Cdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _Cdatabase = new CustomerDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _Cdatabase;
            }
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Book book = (Book)ToDoItemsListView.SelectedItems[0];
            NameMain.Text = book.Name;
            AuthorMain.Text = book.Author;
            GenreMain.Text = book.Genre;
            PageMain.Text = book.Page.ToString();
            ISBNMain.Text = book.ISBN;
            BookDetail customization = new BookDetail(book);
            customization.Show();
            this.Close();
        }      
        

        private void buttonComedy_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Comedy";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonDrama_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Drama";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonFantasy_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Fantasy";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonHorror_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Horror";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonMythology_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Mythology";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonRomance_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Romance";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonTragedy_Click(object sender, RoutedEventArgs e)
        {
            string GetGenre = "Tragedy";
            var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }

        private void buttonComedyFav_Click(object sender, RoutedEventArgs e)
        {
            buttonComedy.Background = Brushes.Yellow;
        }
        private void buttonDramaFav_Click(object sender, RoutedEventArgs e)
        {
            buttonDrama.Background = Brushes.DarkOrange;
        }
        private void buttonFantasyFav_Click(object sender, RoutedEventArgs e)
        {
            buttonFantasy.Background = Brushes.DarkOrange;
        }
        private void buttonHorrorFav_Click(object sender, RoutedEventArgs e)
        {
            buttonHorror.Background = Brushes.DarkOrange;
        }
        private void buttonMythologyFav_Click(object sender, RoutedEventArgs e)
        {
            buttonMythology.Background = Brushes.DarkOrange;
        }
        private void buttonRomanceFav_Click(object sender, RoutedEventArgs e)
        {
            buttonRomance.Background = Brushes.DarkOrange;
        }
        private void buttonTragedyFav_Click(object sender, RoutedEventArgs e)
        {
            buttonTragedy.Background = Brushes.DarkOrange;
        }


        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login customization = new Login();
            customization.Show();
            this.Close();
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            MyStaticValues.login = false;
            CDatabase.LogoutCustomers();
            Login customization = new Login();
            customization.Show();
            this.Close();
        }

        private void buttonBasket_Click(object sender, RoutedEventArgs e)
        {
            Basket customization = new Basket();
            customization.Show();
            this.Close();
        }

    }
}
