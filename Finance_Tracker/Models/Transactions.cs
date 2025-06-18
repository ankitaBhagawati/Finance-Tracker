using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Models
{
    public class Transactions
    {
        public int transaction_id { get; set; }
        public int user_id { get; set; }
        public int user_name { get; set; }
        public int category_id { get; set; }
        public string? category_name { get; set; }
        public double amount { get; set; }
        public double description { get; set; }
        public DateTime? transaction_date { get; set; }
    }
}
