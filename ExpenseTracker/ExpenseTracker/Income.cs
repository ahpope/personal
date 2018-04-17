namespace ExpenseTracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Income")]
    public partial class Income
    {
        public int IncomeID { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        public double? Amount { get; set; }

        public int? AccountID { get; set; }

        public virtual Account Account { get; set; }
    }
}
