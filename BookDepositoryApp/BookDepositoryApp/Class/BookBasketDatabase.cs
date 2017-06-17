using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BookDepositoryApp
{
    public class BookBasketDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public BookBasketDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<BookBasket>().Wait();
        }

        // Query using SQL query string
        public Task<List<BookBasket>> GetBooks()
        {
            return database.QueryAsync<BookBasket>("SELECT * FROM [BookBasket] WHERE [Done] = 0");
        }
        public Task<List<BookBasket>> DeleteBooks()
        {
            return database.QueryAsync<BookBasket>("DELETE FROM [BookBasket]");
        }
        public Task<List<BookBasket>> DeleteBooksByID(int ID)
        {
            return database.QueryAsync<BookBasket>("DELETE FROM [BookBasket] WHERE [ID] =" + ID);
        }
        public Task<List<BookBasket>> GetBooksByGenre(string GetGenre)
        {
            return database.QueryAsync<BookBasket>("SELECT * FROM [BookBasket] WHERE [Genre] = '" + GetGenre + "'");
        }

        public Task<BookBasket> GetBook(int id)
        {
            return database.Table<BookBasket>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteItemAsync(BookBasket item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> SaveItemAsync(BookBasket item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
    }
}
