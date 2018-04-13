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
    public partial class Form1 : Form
    {
        public Logic logic;
        int employeeNum;

        public bool empCheck;
        public Form1()
        {
            InitializeComponent();
            logic = new Logic();
            employeeNameBox.Enabled = false;
            employeePayBox.Enabled = false;
            adminNoButton.Enabled = false;
            adminYesButton.Enabled = false;
            addEmployeeButton.Enabled = false;
            punchButton.Enabled = false;
            adminButton.Enabled = false;
            stubButton.Enabled = false;
            startPunch.Enabled = false;
            outPunch.Enabled = false;
            submitTimeButton.Enabled = false;
            addButton.Enabled = false;
            removeEmployeeButton.Enabled = false;
            removeEmpButton.Enabled = false;
            removeTextBox.Enabled = false;
            listEmpButton.Enabled = false;
            listBox.Enabled = false;
            logOutButton.Enabled = false;
        }

        public void employeeNumButton_Click(object sender, EventArgs e)
        {
            if (employeeNumTextBox.Text == "")
                return;
            employeeNum = Convert.ToInt32(employeeNumTextBox.Text);
            empCheck = logic.checkEmployeeStorage(employeeNum);
            if(empCheck == false)
            {
                logicSuccess.Text = "Invalid Number";
            }
            if(empCheck == true)
            {
                logicSuccess.Text = "Successful Login!";
                employeeNumTextBox.Enabled = false;
                employeeNumButton.Enabled = false;
                punchButton.Enabled = true;
                adminButton.Enabled = true;
                stubButton.Enabled = true;
                
                logOutButton.Enabled = true;
            }
            
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            bool adCheck;
            
            adCheck = logic.checkAdmin(employeeNum);
            if (adCheck == true)
            {
                adLabel.Text = "Welcome Admin";
                addEmployeeButton.Enabled = true;
                removeEmployeeButton.Enabled = true;
                listEmpButton.Enabled = true;
            }
            if(adCheck == false)
            {
                adLabel.Text = "ERROR: USER IS NOT ADMIN";
            }
            listBox.Clear();
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            employeeNameBox.Enabled = true;
            employeePayBox.Enabled = true;
            adminNoButton.Enabled = true;
            adminYesButton.Enabled = true;
            addButton.Enabled = true;
            addedSuccessLabel.Text = " ";
            listBox.Clear();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            bool admin;
            string employeeName = employeeNameBox.Text;
            int pay = Convert.ToInt32(employeePayBox.Text);

            if (adminNoButton.Checked == true)
            {
                admin = false;
            }
            else admin = true;

            logic.addEmployee(employeeName, pay, admin);
            addedSuccessLabel.Text = "Added!";
            employeeNameBox.Enabled = false;
            employeePayBox.Enabled = false;
            adminNoButton.Enabled = false;
            adminYesButton.Enabled = false;
            employeeNameBox.Clear();
            employeePayBox.Clear();
            listBox.Clear();
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            employeeNumTextBox.Enabled = true;
            employeeNumTextBox.Clear();
            employeeNumButton.Enabled = true;
            employeeNameBox.Enabled = false;
            employeePayBox.Enabled = false;
            adminNoButton.Enabled = false;
            adminYesButton.Enabled = false;
            addEmployeeButton.Enabled = false;
            punchButton.Enabled = false;
            adminButton.Enabled = false;
            stubButton.Enabled = false;
            removeEmployeeButton.Enabled = false;
            listEmpButton.Enabled = false;
            logOutButton.Enabled = false;
            adLabel.Text = " ";
            logicSuccess.Text = " ";
            removedLabel.Text = " ";
            listBox.Clear();
        }

        private void punchButton_Click(object sender, EventArgs e)
        {
            startPunch.Enabled = true;
            outPunch.Enabled = true;
            submitTimeButton.Enabled = true;
            listBox.Clear();
        }

        private void submitTimeButton_Click(object sender, EventArgs e)
        {
            if (startAMButton.Checked == true || startPMButton.Checked == true)
            {
                if (endAMButton.Checked == true || endPMButton.Checked == true)
                {
                    if(startPunch.Text != null)
                    {
                        if(outPunch.Text != null)
                        {
                            success2Label.Text = "Success!";
                            int inTime = Convert.ToInt32(startPunch.Text);
                            int outTime = Convert.ToInt32(outPunch.Text);
                            int totalTime = 0;
                            
                            if(startPMButton.Checked == true && endAMButton.Checked == true)
                            {
                                if (startPMButton.Checked == true)
                                {
                                    inTime = 24 - (inTime + 12);
                                }
                                totalTime = inTime + outTime;
                            }
                            if(startAMButton.Checked == true && endPMButton.Checked == true)
                            {
                                outTime = outTime + 12;
                                totalTime = outTime - inTime;
                            }
                            if(startAMButton.Checked ==true && endAMButton.Checked == true)
                            {
                                totalTime = outTime - inTime;
                            }
                            if (startPMButton.Checked == true && endPMButton.Checked == true)
                            {
                                totalTime = outTime - inTime;
                            }

                            totalTimeLabel.Text = Convert.ToString(totalTime);
                            logic.addHours(employeeNum, totalTime);
                            logic.addPay(employeeNum, totalTime);
                            listBox.Clear();
                        }
                    }
                }
            } 
        }

        private void stubButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            string name = logic.findEmpName(employeeNum);
            double pay = logic.findEmpPay(employeeNum);
            bool admin = logic.checkAdmin(employeeNum);
            double totPay = logic.findTotPay(employeeNum);

            form2.display(name, pay, admin, employeeNum, totPay);
            listBox.Clear();
        }

        private void removeEmployeeButton_Click(object sender, EventArgs e)
        {
            removeEmpButton.Enabled = true;
            removeTextBox.Enabled = true;
            listBox.Clear();
        }

        private void removeEmpButton_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(removeTextBox.Text);
            logic.removeEmployee(num);
            removedLabel.Text = "Removed!";
            removeTextBox.Clear();
        }

        private void listEmpButton_Click(object sender, EventArgs e)
        {
            listBox.Clear();
            listBox.Columns.Add("Name", 100);

            ListViewItem itm;
            Employee employee2 = new Employee();
            for(int i = 0; i < logic.employeeCount; i++)
            {
                employee2 = logic.returnEmployee(i);
                itm = new ListViewItem(employee2.employeeName);
                listBox.Items.Add(itm);
            }
        }
    }
}
