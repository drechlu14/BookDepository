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
using static System.Net.Mime.MediaTypeNames;

namespace BookDepositoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Book> itemsFromDb;

        public MainWindow()
        {
            InitializeComponent();

            itemsFromDb = new ObservableCollection<Book>(Database.GetBooks().Result);
            if (itemsFromDb.Count < 2)
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
                book.Price = 282;
                book1.Done = 0;
                Database.SaveItemAsync(book1);
            }

            ItemsCount.Content = "Items in Database " + itemsFromDb.Count;
            ToDoItemsListView.ItemsSource = itemsFromDb;
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
            /*var itemsFromDb = Database.GetBooksByGenre(GetGenre).Result;
            ToDoItemsListView.ItemsSource = itemsFromDb;*/


        }

        private void buttonDrama_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonFantasy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonHorror_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonMythology_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonRomance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonTragedy_Click(object sender, RoutedEventArgs e)
        {

        }


        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login customization = new Login();
            customization.Show();
            this.Close();
        }
    }
}
