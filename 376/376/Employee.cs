using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _376
{
    public class Employee
    {
        public int employeeNumber { set; get; }
        public double hours { set; get; }
        public double payRate { set; get; }
        public bool isAdmin { set; get; } = false;
        public string employeeName { set; get; }
        public double totalPay {set; get;}
}
}
