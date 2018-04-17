using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _376
{
    public class Logic
    {
        public static List<Employee> list = new List<Employee>();
        public int employeeCount = 4;
              
        public void defaultCharacters()
        {
            Employee employee = new Employee();
            employee.isAdmin = true;
            employee.employeeNumber = 1;
            employee.employeeName = "Steve Rogers";
            employee.payRate = 5;
            list.Add(employee);

            Employee employee2 = new Employee();
            employee2.employeeNumber = 2;
            employee2.employeeName = "Megan Petersen";
            employee2.payRate = 15;
            list.Add(employee2);

            Employee employee3 = new Employee();
            employee3.employeeNumber = 3;
            employee3.employeeName = "Lily James";
            employee3.payRate = 7;
            list.Add(employee3);

            Employee employee4 = new Employee();
            employee4.employeeNumber = 4;
            employee4.employeeName = "Peter Parker";
            employee4.payRate = 10;
            list.Add(employee4);
        }

        public bool checkEmployeeStorage(int empNum)
        {
            defaultCharacters();
            for(int i = 0; i < employeeCount; i++)
            {
                if (list[i].employeeNumber == empNum)
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkAdmin(int empNum)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == empNum)
                {
                    if (list[i].isAdmin == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void addEmployee(string employeeName, int pay, bool admin)
        {
            employeeCount++;
            Employee newEmployee = new Employee();
            newEmployee.employeeNumber = employeeCount;
            newEmployee.employeeName = employeeName;
            newEmployee.payRate = pay;
            newEmployee.isAdmin = admin;
            list.Add(newEmployee);
        }
        
        public void addHours(int employeeNum, int num)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == employeeNum)
                {
                    list[i].hours += num;  
                }
            }
        }

        public void addPay(int employeeNum, int num)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == employeeNum)
                {
                    list[i].totalPay = list[i].hours * list[i].payRate;
                }
            }
        }

        public string findEmpName(int employeeNum)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == employeeNum)
                {
                    return list[i].employeeName;
                }
            }
            return " ";
        }

        public double findEmpPay(int employeeNum)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == employeeNum)
                {
                    return list[i].payRate;
                }
            }
            return 0;
        }

        public double findTotPay(int employeeNum)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == employeeNum)
                {
                    return list[i].totalPay;
                }
            }
            return 0;
        }

        public void removeEmployee(int employeeNum)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].employeeNumber == employeeNum)
                {
                    employeeCount--;
                    list.Remove(list[i]);
                }
            }
        }
        public Employee returnEmployee(int count)
        {
            return list[count];
        }
    }
}
