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

            //Store Procedure
            UserSP = "sp_CreateUser",
            BudgetSP = "sp_Budgets",


            //Views
            BudgetView= "v_Budgets",
            //Operations
            SpOperation = "sp_operation",
            Add = "ADD",
            Update = "UPDATE",
            Delete = "DELETE";
    }
}
