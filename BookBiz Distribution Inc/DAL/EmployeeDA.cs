using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookBiz_Distribution_Inc.BLL;
using System.IO;

namespace BookBiz_Distribution_Inc.DAL
{
    public class EmployeeDA
    {
        public static string filePath = Application.StartupPath + @"\Employees.dat";
        public static string fileTemp = Application.StartupPath + @"\Temp.dat";

        public static void Save (Employee Emp)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(Emp.employeeId + "," + Emp.firstName + "," + Emp.lastName + "," +
                               Emp.jobTitle + "," + Emp.password);
            sWriter.Close();
            MessageBox.Show("Employee Data Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ListEmployees(ListView listViewEmployee)
        {       
            StreamReader sReader = new StreamReader(filePath);
            listViewEmployee.Items.Clear();       

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);
                item.SubItems.Add(fields[4]);
                listViewEmployee.Items.Add(item);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        public static List<Employee> ListEmployees()
        {
            List<Employee> listE = new List<Employee>();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                Employee Emp = new Employee();
                Emp.employeeId = Convert.ToInt32(fields[0]);
                Emp.firstName = fields[1];
                Emp.lastName = fields[2];
                Emp.password = fields[3];
                Emp.jobTitle = fields[4];
                listE.Add(Emp);
                line = sReader.ReadLine();
            }
            sReader.Close(); 
            return listE;
        }


        public static void Delete(int EmpId)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((EmpId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4]);
                }
                line = sReader.ReadLine(); 
            }
            sReader.Close();
            sWriter.Close();
            
            File.Delete(filePath); 
            File.Move(fileTemp, filePath);

        }

        public static void Update(Employee Emp)
        {
            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((Convert.ToInt32(fields[0]) != (Emp.employeeId)))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4]);
                }

                line = sReader.ReadLine();   
            }
            sWriter.WriteLine(Emp.employeeId + "," + Emp.firstName + "," + Emp.lastName + "," + Emp.jobTitle + "," + Emp.password);
            sReader.Close();
            sWriter.Close();
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }

        public static Employee Search(int txtInput)
        {
            Employee Emp = new Employee();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == Convert.ToInt32(fields[0]))
                {
                    Emp.employeeId = Convert.ToInt32(fields[0]);
                    Emp.firstName = fields[1];
                    Emp.lastName = fields[2];
                    Emp.jobTitle = fields[3];
                    Emp.password = fields[4];
                    sReader.Close();
                    return Emp;
                }
                line = sReader.ReadLine(); 
            }
            sReader.Close();
            return null;
        }

        public static Employee SearchByFName(string txtInput)
        {
            Employee Emp = new Employee();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[1])
                {
                    Emp.employeeId = Convert.ToInt32(fields[0]);
                    Emp.firstName = fields[1];
                    Emp.lastName = fields[2];
                    Emp.jobTitle = fields[3];
                    Emp.password = fields[4];
                    sReader.Close();
                    return Emp;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Employee SearchByLName(string txtInput)
        {
            Employee Emp = new Employee();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[2])
                {
                    Emp.employeeId = Convert.ToInt32(fields[0]);
                    Emp.firstName = fields[1];
                    Emp.lastName = fields[2];
                    Emp.jobTitle = fields[3];
                    Emp.password = fields[4];
                    sReader.Close();
                    return Emp;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }



    }
}
