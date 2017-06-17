using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDepositoryApp.Class
{
    public class Address
    {
        [PrimaryKey, AutoIncrement]
        public string PostNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
