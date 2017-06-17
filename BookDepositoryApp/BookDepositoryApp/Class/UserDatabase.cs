using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BookDepositoryApp.Class;

namespace BookDepositoryApp
{
    public class UserDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public UserDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        // Query using SQL query string
        public Task<List<User>> GetUsers()
        {
            return database.QueryAsync<User>("SELECT * FROM [UserDatabase] WHERE [Done] = 0");
        }
        public Task<List<User>> DeleteUsers()
        {
            return database.QueryAsync<User>("DELETE FROM [UserDatabase]");
        }
        public Task<List<User>> DeleteUsersByID(int ID)
        {
            return database.QueryAsync<User>("DELETE FROM [UserDatabase] WHERE [ID] =" + ID);
        }
        public Task<List<User>> GetUsersByGenre(string GetGenre)
        {
            return database.QueryAsync<User>("SELECT * FROM [UserDatabase] WHERE [Genre] = '" + GetGenre + "'");
        }

        public Task<User> GetUser(int id)
        {
            return database.Table<User>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteItemAsync(User item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> SaveItemAsync(User item)
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
