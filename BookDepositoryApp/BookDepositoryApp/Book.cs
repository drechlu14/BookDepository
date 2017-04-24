using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BookDepositoryApp
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Page { get; set; }
        public string ISBN { get; set; }

        public DateTime Date { get; set; }
        public int Done { get; set; }
    }
}
