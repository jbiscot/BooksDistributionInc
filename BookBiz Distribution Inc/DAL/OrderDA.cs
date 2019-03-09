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
    public class OrderDA
    {
        public static string ordFilePath = Application.StartupPath + @"\Order.dat";
        public static string ordTempFilePath = Application.StartupPath + @"\tempOrder.dat";

        public static void SaveOrder(Order aOrder)
        {
            StreamWriter sWriter = new StreamWriter(ordFilePath, true);
            sWriter.WriteLine(aOrder.OrdNumber + "," + aOrder.OrdEmployee.firstName + "," + aOrder.OrdClient.clientName + "," +
                               aOrder.OrdProduct.title + "," + aOrder.OrdQuantity + "," + aOrder.OrdTotal + "," + aOrder.OrdDate);
            sWriter.Close();
            MessageBox.Show("Order saved successfully.");
        }

        public static int OrderID()
        {
            int orderId = 0;
            Order aOrder = new Order();
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                line = sReader.ReadLine();
                orderId = Convert.ToInt32(fields[0]);
            }
            sReader.Close();
            return orderId;
        }

        public static void ListOrder(ListView listViewOrder)
        {
            StreamReader sReader = new StreamReader(ordFilePath);

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);
                item.SubItems.Add(fields[4]);
                item.SubItems.Add(fields[5]);
                item.SubItems.Add(fields[6]);
                listViewOrder.Items.Add(item);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        public static void Delete(int ordId)
        {
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(ordTempFilePath, true);

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((ordId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();
            File.Delete(ordFilePath);
            File.Move(ordTempFilePath, ordFilePath);
            MessageBox.Show("Order Sucessfully deleted!");
        }

        public static Order SearchByNumber(int searchOrder)
        {
            Order aorder = new Order();
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (searchOrder == Convert.ToInt32(fields[0]))
                {
                    aorder.OrdNumber = Convert.ToInt32(fields[0]);
                    aorder.OrdEmployee = EmployeeDA.SearchByFName((fields[1]));
                    aorder.OrdClient = ClientsDA.Search(fields[2]);
                    aorder.OrdProduct = BooksDA.SearchByTitle(fields[3]);
                    aorder.OrdQuantity = Convert.ToInt32(fields[4]);
                    aorder.OrdTotal = Convert.ToDecimal(fields[5]);
                    aorder.OrdDate = Convert.ToDateTime(fields[6]);
                    sReader.Close();
                    return aorder;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static void Update(Order aOrder)
        {
            DialogResult answer = MessageBox.Show("Doe you want to update the order information?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (answer == DialogResult.Yes)
            {
                StreamReader sReader = new StreamReader(ordFilePath);
                string line = sReader.ReadLine();
                StreamWriter sWriter = new StreamWriter(ordTempFilePath, true);

                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if ((aOrder.OrdNumber) != (Convert.ToInt32(fields[0])))
                    {
                        sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                    }
                    line = sReader.ReadLine();
                }
                sWriter.WriteLine(aOrder.OrdNumber + "," + aOrder.OrdEmployee.firstName + "," + aOrder.OrdClient.clientName + "," +
                   aOrder.OrdProduct.title + "," + aOrder.OrdQuantity + "," + aOrder.OrdTotal + "," + aOrder.OrdDate);

                sReader.Close();
                sWriter.Close();
                File.Delete(ordFilePath);
                File.Move(ordTempFilePath, ordFilePath);
                MessageBox.Show("Sucesso demais!");
            }

        }
    }
}
