using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        public int transaction_id { get; set; }
        public int user_id { get; set; }
        public string? user_name { get; set; }
        public int category_id { get; set; }
        public string? category_name { get; set; }
        public double amount { get; set; }
        public string? description { get; set; }
        public DateTime? transaction_date { get; set; }
    }
}
