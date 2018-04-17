namespace ExpenseTracker
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExpenseTrackerDatabase : DbContext
    {
        public ExpenseTrackerDatabase()
            : base("name=ExpenseTrackerDatabase")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Budget>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<Budget>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Income>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Memo)
                .IsUnicode(false);
        }
    }
}
