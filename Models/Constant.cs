using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class Constant
    {
        public const string
            //Table
            UserTable = "Users",
            BudgetTable = "Budgets",
            TransactionTable= "Transactions",
            CategoryTable= "Categories",

            //Store Procedure
            UserSP = "sp_CreateUser",
            BudgetSP = "sp_Budgets",
            TransactionSP = "sp_Transactions",
            CategorySP = "sp_Category",


            //Views
            BudgetView= "v_Budgets",
            TransactionView= "v_Transactions",


            //Operations
            SpOperation = "sp_operation",
            Add = "ADD",
            Update = "UPDATE",
            Delete = "DELETE";
    }
}
