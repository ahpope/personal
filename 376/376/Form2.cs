using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _376
{
    public partial class Form2 : Form
    {
       
        public Logic logic;
        
        public Form2()
        {
            InitializeComponent();
        }

        public void display(string name, double pay, bool admin, int empNum, double totPay)
        {
            nameLabelF2.Text = name;
            payLabelF2.Text = Convert.ToString(pay);
            empNumberF2.Text = Convert.ToString(empNum);
            totPayLabelF2.Text = Convert.ToString(totPay);
            

            if (admin == true)
            {
                adminLabelF2.Text = "Is Admin";
            }
            else adminLabelF2.Text = "Not Admin";
        }

     
    }
}
