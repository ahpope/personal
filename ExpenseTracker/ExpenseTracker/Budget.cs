namespace ExpenseTracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Budget")]
    public partial class Budget
    {
        public int BudgetID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public double? Amount { get; set; }

        public virtual Category Category { get; set; }
    }
}
