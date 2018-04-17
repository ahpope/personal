using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    class SQLManager
    {
        public double? getSumOfIncomesInMonth(ExpenseTrackerDatabase database, string month)
        {
            var incomes = database.Incomes.ToList();
            var sumInMonth = (from inc in incomes
                                where inc.Month == month.ToUpper()
                                select inc.Amount).Sum();

            return sumInMonth;
        }

        public int getMonthAsInt(string month)
        {
            string[] months = new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };

            int monthAsInt = 0;

            for (int index = 0; index < 12; index++)
            {
                if (month == months[index])
                    monthAsInt = index;
            }

            return monthAsInt + 1;
        }

        public double? getRunningTotalPerMonth(ExpenseTrackerDatabase database, Category category, string month)
        {
            int monthAsInt = getMonthAsInt(month);

            var transactions = database.Transactions.ToList();
            var transactionSumInMonth = (from transaction in transactions
                                         where transaction.Category.CategoryID == category.CategoryID 
                                            && ((DateTime)transaction.Date).Month == monthAsInt
                                         select transaction.Amount).Sum();

            return transactionSumInMonth;
        }

        public double? getGoalPerMonth(ExpenseTrackerDatabase database, Category category, string month)
        {
            var expenses = database.Budgets.ToList();

            var expenseSumInMonth = (from exp in expenses
                                  where exp.Category.CategoryID == category.CategoryID
                                     && exp.Month == month
                                  select exp.Amount).Sum();

            return expenseSumInMonth;
        }

        public double? actualComparedToRunningTotal(ExpenseTrackerDatabase database, Category category, string month)
        {
            return getRunningTotalPerMonth(database, category, month) - getGoalPerMonth(database, category, month);
        }

        //public double? getAccountBalance(ExpenseTrackerDatabase database, string accountName)
        //{
        //    var transactions = database.Transactions.ToList();
        //    var accounts = database.Accounts.ToList();
        //    double? balance = 0.00;

        //    var accountCurrentBalance = (from acs in accounts
        //                                 where acs.AccountName == accountName
        //                                 select acs.AccountTotal).SingleOrDefault();

        //    var accountTransTotal = (from trans in transactions
        //                             join acs in accounts
        //                             on trans.AccountID equals acs.AccountID
        //                             where acs.AccountName == accountName
        //                             select trans.Amount).Sum();

        //    double currentBalance = 0.00; 
        //    double transactionTotal = 0.00;

        //    if (double.TryParse(accountCurrentBalance.ToString(), out currentBalance) && double.TryParse(accountTransTotal.ToString(), out transactionTotal))
        //    {
        //        balance = currentBalance - transactionTotal;
        //    }

        //    return balance;
        //}

        public void updateAccountTotal(ExpenseTrackerDatabase database, int accountID, double amount)
        {
            var accounts = database.Accounts.ToList();

            var result = (from acs in accounts
                          where acs.AccountID == accountID
                          select acs).SingleOrDefault();

            result.AccountTotal += amount;
        }

        public List<string> getMonthsPerCategory(ExpenseTrackerDatabase database, Category category)
        {
            var budgets = database.Budgets.ToList();

            var months = (from bud in budgets
                          where bud.CategoryID == category.CategoryID
                          select bud.Month).ToList();

            return months;
        }

        public bool checkBudgetAlreadyCreated(ExpenseTrackerDatabase database, Category category, string month)
        {
            var budgets = database.Budgets.ToList();

            var alreadyCreated = (from bud in budgets
                                  where bud.CategoryID == category.CategoryID
                                  where bud.Month == month
                                  select bud);

            if(alreadyCreated.Any())
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public List<Income> getIncomeByAccount(ExpenseTrackerDatabase database, int accountID)
        {
            var incomes = database.Incomes.ToList();

            var incomeList = (from inc in incomes
                                     where inc.AccountID == accountID
                                     select inc).ToList();

            return incomeList;
        }

        public Budget getBudget(ExpenseTrackerDatabase database, Category category, string month)
        {
            var budgets = database.Budgets.ToList();

            Budget toRemove = (from bud in budgets
                               where bud.CategoryID == category.CategoryID
                               where bud.Month == month
                               select bud).SingleOrDefault();

            return toRemove;
        }
    }
}
