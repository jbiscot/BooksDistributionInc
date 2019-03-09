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
    public class ClientsDA
    {
        public static string filePath = Application.StartupPath + @"\Clients.dat";
        public static string fileTemp = Application.StartupPath + @"\Temp.dat";

        public static void Save(Clients aClients)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(aClients.clientName + "," + aClients.phoneNumber + "," +
                              aClients.city + "," + aClients.street + "," +
                              aClients.postalCode + "," + aClients.creditLimit);
            sWriter.Close();
            MessageBox.Show("Client Data Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

    }

        public static void ListClients(ListView listViewClients)
        {
            StreamReader sReader = new StreamReader(filePath);
            listViewClients.Items.Clear();

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
                listViewClients.Items.Add(item);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        public static void BoxClients(ComboBox comboBoxClient)
        {
            StreamReader sReader = new StreamReader(filePath);
            
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                comboBoxClient.Items.Add(fields[0]);                
                line = sReader.ReadLine();
            }
            sReader.Close();
        }
   
        public static List<Clients> ListClients()
        {
            List<Clients> listC = new List<Clients>();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                Clients Cli = new Clients();
                Cli.clientName = fields[0];
                Cli.phoneNumber = fields[1];
                Cli.city = fields[2];
                Cli.street = fields[3];
                Cli.postalCode = fields[4];
                Cli.creditLimit = Convert.ToInt32(fields[5]);
                listC.Add(Cli);
                line = sReader.ReadLine();
            }
            sReader.Close();
            return listC;
        }

        public static void Delete(string CliId)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((CliId) != ((fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();

            File.Delete(filePath);
            File.Move(fileTemp, filePath);

        }

        public static void Update(Clients Cli)
        {
            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (((fields[0]) != (Cli.clientName)))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5]);
                }

                line = sReader.ReadLine();
            }
            sWriter.WriteLine(Cli.clientName + "," + Cli.phoneNumber + "," + Cli.city + "," + Cli.street + "," + Cli.postalCode + "," + Cli.creditLimit);
            sReader.Close();
            sWriter.Close();
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }

        public static Clients Search(string txtInput)
        {
            Clients Cli = new Clients();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[0])
                {
                    Cli.clientName = fields[0];
                    Cli.phoneNumber = fields[1];
                    Cli.city = fields[2];
                    Cli.postalCode = fields[3];
                    Cli.street = fields[4];
                    Cli.creditLimit = Convert.ToDecimal(fields[5]);
                    sReader.Close();
                    return Cli;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Clients SearchByCity(string txtInput) 
        {
            Clients Cli = new Clients();

        StreamReader sReader = new StreamReader(filePath);
        string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[2])
                {
                    Cli.clientName = fields[0];
                    Cli.phoneNumber = fields[1];
                    Cli.city = fields[2];
                    Cli.postalCode = fields[3];
                    Cli.street = fields[4];
                    Cli.creditLimit = Convert.ToDecimal(fields[5]);
                    sReader.Close();
                    return Cli;
                }
                 line = sReader.ReadLine();
            }
                 sReader.Close();
            return null;
        }
    }
}
