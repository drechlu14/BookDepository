using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BookDepositoryApp
{
    public class BookDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public BookDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Book>().Wait();
        }

        // Query using SQL query string
        public Task<List<Book>> GetBooks()
        {
            return database.QueryAsync<Book>("SELECT * FROM [Book] WHERE [Done] = 0");
        }
        public Task<List<Book>> DeleteBooks()
        {
            return database.QueryAsync<Book>("DELETE FROM [Book]");
        }
        public Task<List<Book>> DeleteBooksByID(int ID)
        {
            return database.QueryAsync<Book>("DELETE FROM [Book] WHERE [ID] =" + ID);
        }
        public Task<List<Book>> GetBooksByGenre(string GetGenre)
        {
            return database.QueryAsync<Book>("SELECT * FROM [Book] WHERE [Genre] ='" + GetGenre + "'");
        }

        public Task<Book> GetBook(int id)
        {
            return database.Table<Book>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteItemAsync(Book item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> SaveItemAsync(Book item)
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
