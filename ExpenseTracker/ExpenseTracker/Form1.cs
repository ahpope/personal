using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class Form1 : Form
    {
        ExpenseTrackerDatabase database;
        SQLManager SQLMan;
        Form_TransactionViewer transactionViewerForm;

        private string[] months = new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };

        public Form1()
        {
            InitializeComponent();
            database = new ExpenseTrackerDatabase();
            SQLMan = new SQLManager();
            
            UpdateAllCategoryCmbx();
            UpdateAllAccountCmbx();
            
            transactionViewerForm = new Form_TransactionViewer();

            // Populate Month drop downs
            IncomeMonthCmbx.Items.AddRange(months);
            BudgetMonthCmbx.Items.AddRange(months);
        }

        #region Adding/Retrieving Data

        private void AddBudgetAmountBtn_Click(object sender, EventArgs e)
        {
            double budgetAmount = 0.00;

            var selectedCategory = CategoryCmbx.SelectedItem;
            Category asCategory = selectedCategory as Category;
            var selectedMonth = BudgetMonthCmbx.SelectedItem?.ToString();

            try
            {
                if (double.TryParse(BudgetAmountTxt.Text, out budgetAmount) && CategoryCmbx.SelectedItem != null
                    && BudgetMonthCmbx.SelectedItem != null)

                    if (!SQLMan.checkBudgetAlreadyCreated(database, asCategory, selectedMonth))
                    {
                        {
                            Budget newBudget = new Budget()
                            {
                                CategoryID = asCategory.CategoryID,
                                Month = selectedMonth,
                                Amount = (double?)budgetAmount
                            };

                            database.Budgets.Add(newBudget);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Budget has already been created. Consider updated it instead.");
                    }

                else
                {
                    MessageBox.Show("Please fill in all fields with valid data.", "Missing and or Invalid Input", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed in adding newBudget data." + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!", MessageBoxButtons.OK);
            }

            try
            {
                database.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save to database!" + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!");
                database.Dispose();
                database = new ExpenseTrackerDatabase();
            }

            UpdateAllCategoryCmbx();
            button5_Click(sender, e);
        }

        private void UpdateBudgetBtn_Click(object sender, EventArgs e)
        {
            double updatedBudgetValue = 0.00;

            var selectedCategory = UpdateBudgetCmbx.SelectedItem;
            Category asCategory = selectedCategory as Category;
            var selectedMonth = UpdateBudgetMonthCmbx.SelectedItem?.ToString();

            if (double.TryParse(UpdateBudgetAmountTxtbx.Text, out updatedBudgetValue) && UpdateBudgetCmbx.SelectedItem != null
                    && UpdateBudgetMonthCmbx.SelectedItem != null)
            {
                var budgetToUpdate = SQLMan.getBudget(database, asCategory, selectedMonth);

                budgetToUpdate.Amount = updatedBudgetValue;
                database.SaveChanges();
                UpdateAllCategoryCmbx();

            }

        }

        private void AddCatgoryBtn_Click(object sender, EventArgs e)
        {
            Category newCategory;

            // newCategory = new Category() and database.Categories.Add(newCategory) must be inside the if block 
            // or else you get blank categories and an error
            if (!CheckDuplicateCategory())
            {
                newCategory = new Category();
                newCategory.CategoryName = CategoryTxt.Text;
                database.Categories.Add(newCategory);
            }

            try
            {
                database.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save to database!" + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!");
                database.Dispose();
                database = new ExpenseTrackerDatabase();
            }

            UpdateAllCategoryCmbx();
            button6_Click(sender, e);
        }

        private void AccountTypeBtn_Click(object sender, EventArgs e)
        {
            double accountTotal;

            try
            {
                //var result = database.Accounts.FirstOrDefault(a => a.AccountName == AccountAmountTxt.Text);

                if (!CheckDuplicateAccount() && Double.TryParse(AccountAmountTxt.Text, out accountTotal))
                {
                    Account newAccount = new Account()
                    {
                        AccountName = AccountTypeTxt.Text,
                        AccountTotal = accountTotal,
                    };

                    database.Accounts.Add(newAccount);
                }
                else
                {
                    MessageBox.Show("Please fill in all account information with valid data.", "Missing and/or Invalid Input", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed in adding newAccount data." + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!", MessageBoxButtons.OK);
            }

            try
            {
                database.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save to database!" + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!");
                database.Dispose();
                database = new ExpenseTrackerDatabase();
            }

            UpdateAllAccountCmbx();
            button3_Click(sender, e);
        }

        private void AddIncomeBtn_Click(object sender, EventArgs e)
        {
            double incomeAmount = 0.00;
            try
            {
                var selectedMonth = IncomeMonthCmbx.SelectedItem?.ToString();
                var selectedAccount = IncomeAccountCmbx.SelectedItem;

                if (double.TryParse(IncomeAmountTxt.Text, out incomeAmount) && IncomeAccountCmbx.SelectedItem != null
                    && IncomeMonthCmbx.SelectedItem != null && incomeAmount > 0.00)
                {
                    var asAccount = selectedAccount as Account;

                    Income newIncome = new Income()
                    {
                        Amount = incomeAmount,
                        Month = selectedMonth,
                        AccountID = asAccount.AccountID
                    };

                    database.Incomes.Add(newIncome);
                    SQLMan.updateAccountTotal(database, asAccount.AccountID, incomeAmount);
                }
                else
                {
                    MessageBox.Show("Please fill in all fields with valid data.", "Missing and/or Invalid Input", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed in adding newIncome data." + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!", MessageBoxButtons.OK);
            }

            try
            {
                database.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save to database!" + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!");
                database.Dispose();
                database = new ExpenseTrackerDatabase();
            }

            UpdateAllAccountCmbx();
            button4_Click(sender, e);
        }

        private void AddTransactionBtn_Click(object sender, EventArgs e)
        {
            double transactionAmount = 0.00;
            var selectedCategory = TransactionCategoryCmbx.SelectedItem;
            var selectedAccount = TransactionAccountCmbx.SelectedItem;
            var asCategory = selectedCategory as Category;
            var asAccount = selectedAccount as Account;
            int categoryID = asCategory.CategoryID;
            int accountID = asAccount.AccountID;
            DateTime dateTime = TransactionDatePckr.Value;
            string month = getDateTimeMonth(dateTime);

            try
            {
                if (double.TryParse(TransactionAmountTxt.Text, out transactionAmount) && selectedCategory != null
                    && selectedAccount != null)
                {
                    if (SpendingRdb.Checked || ReceivingRdb.Checked)
                    {
                        if (SpendingRdb.Checked && transactionAmount > 0)
                        {
                            transactionAmount = 0 - transactionAmount;
                        }

                        if (ReceivingRdb.Checked && transactionAmount < 0)
                        {
                            throw new Exception("A receiving transaction cannot be negative.");
                        }

                        Transaction newTransaction = new Transaction()
                        {
                            Amount = transactionAmount,
                            Memo = TransactionMemoTxt.Text,
                            AccountID = accountID,
                            CategoryID = categoryID,
                            Date = dateTime
                        };

                        SQLMan.updateAccountTotal(database, accountID, transactionAmount);
                        database.Transactions.Add(newTransaction);
                    }
                    else
                    {
                        throw new Exception("Please select a transaction type below.");
                    }
                }
                else
                {
                    throw new Exception("Please fill in the category, account, and amount sections with valid data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Missing and / or Invalid Input", MessageBoxButtons.OK);
            }

            try
            {
                database.SaveChanges();
                CheckExceedBudget(database, asCategory, month);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save to database!" + Environment.NewLine + "Exception Message:" + Environment.NewLine + ex.ToString(), "ERROR!");
                database.Dispose();
                database = new ExpenseTrackerDatabase();
            }

            PrintGoalTotalEveryMonthEveryCategory();
            button2_Click(sender, e);
        }

        public string getDateTimeMonth(DateTime dateTime)
        {
            string month = dateTime.Month.ToString();

            if (month == "1")
            {
                month = "JANUARY";
            }
            else if (month == "2")
            {
                month = "FEBRUARY";
            }
            else if (month == "3")
            {
                month = "MARCH";
            }
            else if (month == "4")
            {
                month = "APRIL";
            }
            else if (month == "5")
            {
                month = "MAY";
            }
            else if (month == "6")
            {
                month = "JUNE";
            }
            else if (month == "7")
            {
                month = "JULY";
            }
            else if (month == "8")
            {
                month = "AUGUST";
            }
            else if (month == "9")
            {
                month = "SEPTEMBER";
            }
            else if (month == "10")
            {
                month = "OCTOBER";
            }
            else if (month == "11")
            {
                month = "NOVEMBER";
            }
            else if (month == "12")
            {
                month = "DECEMBER";
            }

            return month;
        }
        #endregion

        #region Removing Data

        private void RemoveIncomeAccountCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedAccount = RemoveIncomeAccountCmbx.SelectedItem;
            if (selectedAccount != null)
            {
                Account asAccount = selectedAccount as Account;
                var incomeList = SQLMan.getIncomeByAccount(database, asAccount.AccountID);

                UpdateRemoveIncomeMonthCmbx(incomeList);
            }
        }

        private void RemoveIncomeBtn_Click(object sender, EventArgs e)
        {
            var selectedItem = RemoveIncomeAccountCmbx.SelectedItem;

            if (selectedItem is Account && RemoveIncomeMonthCmbx.SelectedItem != null)
            {
                string selectedIncomeID = RemoveIncomeMonthCmbx.SelectedItem.ToString();
                selectedIncomeID = selectedIncomeID.Substring(0, 2).Trim();
                var selectedAccount = selectedItem as Account;
                var accountID = selectedAccount.AccountID;
                var incomes = database.Incomes.ToList();

                DialogResult result = MessageBox.Show($"Do you wish to continue?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var resultIncome = incomes.Where(i => incomes.Any(a => i.AccountID == accountID && i.IncomeID == Convert.ToInt32(selectedIncomeID))).FirstOrDefault();

                    try
                    {
                        database.Incomes.Remove(resultIncome);
                        database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        database.Dispose();
                        database = new ExpenseTrackerDatabase();
                    }

                    UpdateAllAccountCmbx();
                    button3_Click(sender, e);
                }
            }

        }

        private void RemoveBudgetCategoryCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCategory = RemoveBudgetCmbx.SelectedItem;
            if (selectedCategory != null)
            {
                Category asCategory = selectedCategory as Category;
                var months = SQLMan.getMonthsPerCategory(database, asCategory);

                UpdateRemoveBudgetMonthCmbx(months);
            }
        }

        private void RemoveCategoryButton_Click(object sender, EventArgs e)
        {
            // BUG: sometimes when you remove a Category, it can show some weird text
            var selectedItem = RemoveCategoryCmbx.SelectedItem;
            if (selectedItem is Category)
            {
                var selectedCategory = selectedItem as Category;
                string categoryName = selectedCategory.CategoryName;
                var categoryID = selectedCategory.CategoryID;

                var budgets = database.Budgets.ToList();
                var transactions = database.Transactions.ToList();

                DialogResult result = MessageBox.Show($"Warning! This will remove all Budgets and Transactions tied to Category '{categoryName}'. Do you wish to continue?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var resultBudgets = budgets.Where(b => budgets.Any(c => b.CategoryID == categoryID)).ToList();
                    var resultTransactions = transactions.Where(t => transactions.Any(c => t.CategoryID == categoryID)).ToList();

                    try
                    {
                        foreach (Transaction transaction in resultTransactions)
                        {
                            database.Transactions.Remove(transaction);
                        }

                        foreach (Budget budget in resultBudgets)
                        {
                            database.Budgets.Remove(budget);
                        }

                        database.Categories.Remove(selectedCategory);
                        database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        database.Dispose();
                        database = new ExpenseTrackerDatabase();
                    }

                    UpdateAllCategoryCmbx();
                    button6_Click(sender, e);
                }
            }
        }

        private void RemoveAccountTypesBtn_Click(object sender, EventArgs e)
        {
            var selectedItem = RemoveAccountTypesCmbx.SelectedItem;
            if (selectedItem is Account)
            {
                var selectedAccount = selectedItem as Account;
                string accountName = selectedAccount.AccountName;
                var accountID = selectedAccount.AccountID;

                // Scan all Incomes and Transactions
                // If none of them contain accountID, then allow the Account remove
                var incomes = database.Incomes.ToList();
                var transactions = database.Transactions.ToList();

                DialogResult result = MessageBox.Show($"Warning! This will remove all Incomes and Transactions tied to Account '{accountName}'. Do you wish to continue?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var resultIncomes = incomes.Where(i => incomes.Any(c => i.AccountID == accountID)).ToList();
                    var resultTransactions = transactions.Where(t => transactions.Any(c => t.AccountID == accountID)).ToList();

                    try
                    {
                        foreach (Transaction transaction in resultTransactions)
                        {
                            database.Transactions.Remove(transaction);
                        }

                        foreach (Income income in resultIncomes)
                        {
                            database.Incomes.Remove(income);
                        }

                        database.Accounts.Remove(selectedAccount);
                        database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        database.Dispose();
                        database = new ExpenseTrackerDatabase();
                    }

                    UpdateAllAccountCmbx();
                    button3_Click(sender, e);
                }
            }
        }

        private void RemoveBudgetBtn_Click(object sender, EventArgs e)
        {
            var selectedCategory = RemoveBudgetCmbx.SelectedItem;

            if (selectedCategory is Category && RemoveBudgetMonthCmbx.SelectedItem != null)
            {
                string selectedMonth = RemoveBudgetMonthCmbx.SelectedItem.ToString();
                Category asCategory = selectedCategory as Category;
                var budgetToRemove = SQLMan.getBudget(database, asCategory, selectedMonth);

                try
                {
                    database.Budgets.Remove(budgetToRemove);
                    database.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    database.Dispose();
                    database = new ExpenseTrackerDatabase();
                }
            }

            UpdateAllCategoryCmbx();
            RemoveBudgetCategoryCmbx_SelectedIndexChanged(sender, e);
            button5_Click(sender, e);
        }

        private void UpdateBudgetCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCategory = UpdateBudgetCmbx.SelectedItem;

            if (selectedCategory != null)
            {
                Category asCategory = selectedCategory as Category;
                var months = SQLMan.getMonthsPerCategory(database, asCategory);

                UpdateUpdateBudgetMonthCmbx(months);
                button5_Click(sender, e);
            }
        }
        #endregion

        #region Updating Labels

        private void UpdateAllCategoryCmbx()
        {
            CategoryCmbx.DataSource = null;
            CategoryCmbx.DataSource = database.Categories.ToList();
            CategoryCmbx.DisplayMember = "CategoryName";
            CategoryCmbx.ValueMember = "CategoryID";
            CategoryCmbx.Refresh();

            TransactionCategoryCmbx.DataSource = null;
            TransactionCategoryCmbx.DataSource = database.Categories.ToList();
            TransactionCategoryCmbx.DisplayMember = "CategoryName";
            TransactionCategoryCmbx.ValueMember = "CategoryID";
            TransactionCategoryCmbx.Refresh();

            RemoveCategoryCmbx.DataSource = null;
            RemoveCategoryCmbx.DataSource = database.Categories.ToList();
            RemoveCategoryCmbx.DisplayMember = "CategoryName";
            RemoveCategoryCmbx.ValueMember = "CategoryID";
            RemoveCategoryCmbx.Refresh();

            RemoveBudgetCmbx.DataSource = null;
            RemoveBudgetCmbx.DataSource = database.Categories.ToList();
            RemoveBudgetCmbx.DisplayMember = "CategoryName";
            RemoveBudgetCmbx.ValueMember = "CategoryID";
            RemoveBudgetCmbx.Refresh();

            ViewBudgetCategoryCmBx.DataSource = null;
            ViewBudgetCategoryCmBx.DataSource = database.Categories.ToList();
            ViewBudgetCategoryCmBx.DisplayMember = "CategoryName";
            ViewBudgetCategoryCmBx.ValueMember = "CategoryID";
            ViewBudgetCategoryCmBx.Refresh();

            UpdateBudgetCmbx.DataSource = null;
            UpdateBudgetCmbx.DataSource = database.Categories.ToList();
            UpdateBudgetCmbx.DisplayMember = "CategoryName";
            UpdateBudgetCmbx.ValueMember = "CategoryID";
            UpdateBudgetCmbx.Refresh();
          
        }

        private void UpdateAllAccountCmbx()
        {
            IncomeAccountCmbx.DataSource = null;
            IncomeAccountCmbx.DataSource = database.Accounts.ToList();
            IncomeAccountCmbx.DisplayMember = "AccountName";
            IncomeAccountCmbx.ValueMember = "AccountID";
            IncomeAccountCmbx.Refresh();

            TransactionAccountCmbx.DataSource = null;
            TransactionAccountCmbx.DataSource = database.Accounts.ToList();
            TransactionAccountCmbx.DisplayMember = "AccountName";
            TransactionAccountCmbx.ValueMember = "AccountID";
            TransactionAccountCmbx.Refresh();

            RemoveAccountTypesCmbx.DataSource = null;
            RemoveAccountTypesCmbx.DataSource = database.Accounts.ToList();
            RemoveAccountTypesCmbx.DisplayMember = "AccountName";
            RemoveAccountTypesCmbx.ValueMember = "AccountID";
            RemoveAccountTypesCmbx.Refresh();

            RemoveIncomeAccountCmbx.DataSource = null;
            RemoveIncomeAccountCmbx.DataSource = database.Accounts.ToList();
            RemoveIncomeAccountCmbx.DisplayMember = "AccountName";
            RemoveIncomeAccountCmbx.ValueMember = "AccountID";
            RemoveIncomeAccountCmbx.Refresh();
        }

        private void UpdateRemoveBudgetMonthCmbx(List<string> months)
        {

            //clear months from previously selected category
            RemoveBudgetMonthCmbx.Items.Clear();

            //populate months combobox
            for (int i = 0; i < months.Count; i++)
            {
                RemoveBudgetMonthCmbx.Items.Add(months[i]);
            }
        }

        private void UpdateRemoveIncomeMonthCmbx(List<Income> incomes)
        {
            RemoveIncomeMonthCmbx.Items.Clear();

            foreach (Income inc in incomes)
            {
                RemoveIncomeMonthCmbx.Items.Add(String.Format( "{0} - {1} - ${2}", inc.IncomeID, inc.Month, inc.Amount ));
            }
        }

        private void UpdateUpdateBudgetMonthCmbx(List<string> months)
        {
            UpdateBudgetMonthCmbx.Items.Clear();

            for (int i = 0; i < months.Count; i++)
            {
                UpdateBudgetMonthCmbx.Items.Add(months[i]);
            }
        }

        #endregion

        #region Checking Duplicates (and more)

        private bool CheckDuplicateCategory()
        {
            for (int i = 0; i < CategoryCmbx.Items.Count; i++)
            {
                if (CategoryTxt.Text == CategoryCmbx.GetItemText(CategoryCmbx.Items[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDuplicateAccount()
        {
            for (int i = 0; i < IncomeAccountCmbx.Items.Count; i++)
            {
                if (AccountTypeTxt.Text == IncomeAccountCmbx.GetItemText(IncomeAccountCmbx.Items[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckExceedBudget(ExpenseTrackerDatabase databse, Category category, string month)
        {
            // negate transaction amount to compare it with budget
            double? runningTotal = -SQLMan.getRunningTotalPerMonth(database, category, month);
            double? budget = SQLMan.getGoalPerMonth(database, category, month);
            var selectedCategory = TransactionCategoryCmbx.SelectedItem;
            var asCategory = selectedCategory as Category;
            int categoryID = asCategory.CategoryID;

            var hasABudget = database.Budgets.FirstOrDefault(b => b.Month == month && b.CategoryID == categoryID);

            // Expenses(Transactions that indicate you spent x amount of money) should be entered by the user as negative
            // need to fix this alert based on that, this condition assumes a postive number being entered is an expense
            if (runningTotal > budget && hasABudget != null)
            {
                MessageBox.Show(month + "'s " + category.CategoryName + " budget has been exceeded by $" + (runningTotal - budget));
            }
        }
        #endregion

        #region Print/View Information

        private void ViewBudgetCategoryCmBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrintGoalTotalEveryMonthEveryCategory();
        }

        private void ViewBudgetMonthTrackBar_Scroll(object sender, EventArgs e)
        {
            PrintGoalTotalEveryMonthEveryCategory();
        }

        private void ViewTransactionsBtn_Click(object sender, EventArgs e)
        {
            var transactions = database.Transactions.ToList();

            if (transactionViewerForm.IsDisposed)
            {
                transactionViewerForm = new Form_TransactionViewer();
            }
            
            transactionViewerForm.setDB(database);

            transactionViewerForm.Show();
        }

        private void PrintGoalTotalEveryMonthEveryCategory()
        {
            GoalTotalInfoLbl.Text = "";

            var categories = database.Categories.ToList();
            var category = ViewBudgetCategoryCmBx.SelectedItem;

            if (category != null)
            {
                var asCategory = category as Category;
                string month = months[ViewBudgetMonthTrackBar.Value];

                double? runningTotalInGoalInCategory = SQLMan.getGoalPerMonth(database, asCategory, month);
                double? runningTotalInActual = SQLMan.getRunningTotalPerMonth(database, asCategory, month);
                double? actualCompared = SQLMan.actualComparedToRunningTotal(database, asCategory, month);

                GoalTotalInfoLbl.Text += $"Budget for {asCategory.CategoryName} in {month} is {runningTotalInGoalInCategory}" + Environment.NewLine;
                GoalTotalInfoLbl.Text += $"Total spent for {asCategory.CategoryName} in {month} is {runningTotalInActual}" + Environment.NewLine;
                GoalTotalInfoLbl.Text += $"Compared value for {asCategory.CategoryName} in {month} is {actualCompared}" + Environment.NewLine;

                // find running goal total in each category in each month
                /*foreach (Category cat in categories)
                {
                    foreach (string month in months)
                    {
                        double? runningTotalInGoalInCategory = SQLMan.getGoalPerMonth(database, cat, month);
                        GoalTotalInfoLbl.Text += $"Budget for {cat.CategoryName} in {month} is {runningTotalInGoalInCategory}" + Environment.NewLine;
                    }
                }*/
            }
        }

        // deprecated
        private void PrintIncomesPerMonth()
        {
            /*IncomesInfoLbl.Text = "";

            var categories = database.Categories.ToList();

            foreach (Category cat in categories)
            {
                foreach (string month in months)
                {
                    double? runningTotalInActual = SQLMan.getRunningTotalPerMonth(database, cat, month);
                    IncomesInfoLbl.Text += $"Total spent for {cat.CategoryName} in {month} is {runningTotalInActual}" + Environment.NewLine;
                }
            }*/
        }

        // deprecated
        private void PrintActualComparedToRunningTotal()
        {
            /*ActualComparedInfoLbl.Text = "";

            var categories = database.Categories.ToList();

            foreach (Category cat in categories)
            {
                foreach (string month in months)
                {
                    double? actualCompared = SQLMan.actualComparedToRunningTotal(database, cat, month);
                    IncomesInfoLbl.Text += $"Compared value for {cat.CategoryName} in {month} is {actualCompared}" + Environment.NewLine;
                }
            }*/
        }

        private void PrintAccountBalance()
        {
            AccountBalanceInfoLbl.Text = "";

            var accounts = database.Accounts.ToList();

            foreach (Account account in accounts)
            {
                AccountBalanceInfoLbl.Text += $"Balance in {account.AccountName} Account is {account.AccountTotal}" + Environment.NewLine;
            }
        }

        private void CalculateQueryBtn_Click(object sender, EventArgs e)
        {
            PrintGoalTotalEveryMonthEveryCategory();
            PrintIncomesPerMonth();
            PrintActualComparedToRunningTotal();
            PrintAccountBalance();
        }
        
        private void ViewBudgetRefreshBtn_Click(object sender, EventArgs e)
        {
            PrintGoalTotalEveryMonthEveryCategory();
        }

        #endregion

        #region Gridview Logic
        private void button2_Click(object sender, EventArgs e)
        {
            var transactionTable = (from tran in database.Transactions.ToList()
                                    join acs in database.Accounts.ToList()
                                    on tran.AccountID equals acs.AccountID
                                    join cat in database.Categories.ToList()
                                    on tran.CategoryID equals cat.CategoryID
                                select new {tran.TransactionID, cat.CategoryName, tran.Memo , acs.AccountName, tran.Amount, tran.Date});

            dataGridView1.DataSource = transactionTable.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var accountTable = (from acs in database.Accounts.ToList()
                                select new { acs.AccountID, acs.AccountName, acs.AccountTotal});

            dataGridView1.DataSource = accountTable.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var incomeTable = (from inc in database.Incomes.ToList()
                               join acs in database.Accounts.ToList()
                               on inc.AccountID equals acs.AccountID
                                select new { inc.IncomeID, acs.AccountName, inc.Month, inc.Amount });

            dataGridView1.DataSource = incomeTable.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var categoryTable = (from cat in database.Categories.ToList()
                               select new { cat.CategoryID, cat.CategoryName });

            dataGridView1.DataSource = categoryTable.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var budgetTable = (from bud in database.Budgets.ToList()
                               join cat in database.Categories.ToList()
                               on bud.CategoryID equals cat.CategoryID
                               select new { bud.BudgetID, cat.CategoryName, bud.Month, bud.Amount});

            dataGridView1.DataSource = budgetTable.ToList();
        }


        #endregion
    }
}