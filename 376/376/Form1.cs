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
        }

        public void employeeNumButton_Click(object sender, EventArgs e)
        {
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
            }
            
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            bool adCheck;
            Form2 form2 = new Form2();
            adCheck = logic.checkAdmin(employeeNum);
            if (adCheck == true)
            {
                adLabel.Text = "Welcome Admin";
                adminButton.Enabled = false;
                punchButton.Enabled = false;
                stubButton.Enabled = false;
                addEmployeeButton.Enabled = true;
                
                //form2.Show();
            }
            if(adCheck == false)
            {
                adLabel.Text = "ERROR: USER IS NOT ADMIN";
            }
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            employeeNameBox.Enabled = true;
            employeePayBox.Enabled = true;
            adminNoButton.Enabled = true;
            adminYesButton.Enabled = true; 
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
            adLabel.Text = " ";
            logicSuccess.Text = " ";
        }

        private void punchButton_Click(object sender, EventArgs e)
        {
            startPunch.Enabled = true;
            outPunch.Enabled = true;
            submitTimeButton.Enabled = true;
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

                           
                            //if(endPMButton.Checked == true)
                            //{
                            //    outTime = 24 - (outTime + 12);
                            //}
                            
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
                        }
                    }
                }
            }
            
        }
    }
}
