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
    public partial class Form_TransactionViewer : Form
    {
        ExpenseTrackerDatabase database;

        private int selectedIndex;

        public Form_TransactionViewer()
        {
            InitializeComponent();
        }

        public void setDB(ExpenseTrackerDatabase database)
        {
            this.database = database;
        }

        private void UpdateLabels()
        {
            var transactions = database.Transactions.ToList();

            if (selectedIndex >= 0 && selectedIndex < transactions.Count)
            {
                Transaction activeTransaction = transactions[selectedIndex];
                TransactionIDLbl.Text = $"Transaction {activeTransaction.TransactionID}";
                TransactionInfoLbl.Text = $"Category: {activeTransaction.Category.CategoryName}{Environment.NewLine}Account: {activeTransaction.Account.AccountName}{Environment.NewLine}Date: {activeTransaction.Date}{Environment.NewLine}Memo: {activeTransaction.Memo}";
            }
        }

        private void PrevTransactionBtn_Click(object sender, EventArgs e)
        {
            decrementID();
        }

        private void NextTransactionBtn_Click(object sender, EventArgs e)
        {
            incrementID();
        }

        private void incrementID()
        {
            var transactions = database.Transactions.ToList();

            if (transactions.Count > 0)
            {
                selectedIndex = (selectedIndex + 1) % transactions.Count;
                UpdateLabels();
            }
        }

        private void decrementID()
        {
            var transactions = database.Transactions.ToList();

            if (transactions.Count > 0)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = transactions.Count - 1;
                }
                UpdateLabels();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var transactions = database.Transactions.ToList();

            if (selectedIndex >= 0 && selectedIndex < transactions.Count)
            {
                var transaction = transactions[selectedIndex];

                database.Transactions.Remove(transaction);

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

                //transactions[selectedIndex] = null;
                //transactions.Remove(transactions[selectedIndex]);
                UpdateLabels();
            }
        }
    }
}
