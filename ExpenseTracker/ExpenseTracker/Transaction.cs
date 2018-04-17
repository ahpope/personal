namespace ExpenseTracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        public int TransactionID { get; set; }

        public int? AccountID { get; set; }

        public int? CategoryID { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Memo { get; set; }

        public double? Amount { get; set; }

        public virtual Account Account { get; set; }

        public virtual Category Category { get; set; }
    }
}
