using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDepositoryApp.Class
{
    class Author
    {
        [PrimaryKey, AutoIncrement]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Book Book { get; set; }
        public string Country { get; set; }
    }
}
