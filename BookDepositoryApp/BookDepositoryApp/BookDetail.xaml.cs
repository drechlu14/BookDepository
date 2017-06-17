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
    /// Interaction logic for BookDetail.xaml
    /// </summary>
    public partial class BookDetail : Window
    {
        ObservableCollection<BookBasket> itemsFromDb;
        private Book _user;
        public BookDetail(Book book)
        {
            InitializeComponent();

            NameDetail.Content = "Book: " + book.Name;
            AuthorDetail.Content = "Author: " + book.Author;
            GenreDetail.Content = "Genres: " + book.Genre;
            PageDetail.Content = "Pages: " + book.Page;
            ISBNDetail.Content = "ISBN: " + book.ISBN;
            BookCreationDate.Content = "Timestamp: " + book.Date;
            this._user = book;
        }

        private static BookBasketDatabase _database;
        public static BookBasketDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new BookBasketDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow customization = new MainWindow();
            customization.Show();
            this.Close();
        }
        private void buttonBuy_Click(object sender, RoutedEventArgs e)
        {
            itemsFromDb = new ObservableCollection<BookBasket>(Database.GetBooks().Result);

            string nameForBasket = Convert.ToString(NameDetail.Content);
            string authorForBasket = Convert.ToString(AuthorDetail.Content);
            BookBasket book = new BookBasket();
            book.Name = nameForBasket;
            book.Author = authorForBasket;
            book.Price = 300;
            book.Done = 0;
            Database.SaveItemAsync(book);

            Basket customization = new Basket();
            customization.Show();
            this.Close();
        }
    }
}
