using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDepositoryApp
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public string Items { get; set; }
    }
}
