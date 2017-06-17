using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BookDepositoryApp
{
    public class CustomerDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public CustomerDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Customer>().Wait();
        }

        // Query using SQL query string
        public Task<List<Customer>> GetCustomers()
        {
            return database.QueryAsync<Customer>("SELECT * FROM [Customer]");
        }
        public Task<List<Customer>> DeleteBooks()
        {
            return database.QueryAsync<Customer>("DELETE FROM [Customer]");
        }
        public Task<List<Customer>> DeleteBooksByID(int ID)
        {
            return database.QueryAsync<Customer>("DELETE FROM [Customer] WHERE [ID] =" + ID);
        }
        public Task<List<Customer>> GetBooksByGenre(string GetGenre)
        {
            return database.QueryAsync<Customer>("SELECT * FROM [Customer] WHERE [Genre] ='" + GetGenre + "'");
        }

        public Task<Customer> GetBook(int id)
        {
            return database.Table<Customer>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteItemAsync(Customer item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> SaveItemAsync(Customer item)
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
