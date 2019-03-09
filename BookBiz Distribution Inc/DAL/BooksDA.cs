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
    public class BooksDA
    {
        public static string filePath = Application.StartupPath + @"\Books.dat";
        public static string fileTemp = Application.StartupPath + @"\Temp.dat";

        public static void Save(Books aBooks)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(aBooks.ISBN + "," + aBooks.title + "," + 
                              aBooks.author + "," + aBooks.yearPublished + "," +
                              aBooks.unitPrice + "," + aBooks.QOH + "," +
                              aBooks.publisher);

            sWriter.Close();
            MessageBox.Show("Book Data Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ListBooks(ListView listViewBooks)
        {
            StreamReader sReader = new StreamReader(filePath);
            listViewBooks.Items.Clear();

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
                listViewBooks.Items.Add(item);
                line = sReader.ReadLine();

            }
            sReader.Close();
        }

        public static List<Books> ListBooks()
        {
            List<Books> listB = new List<Books>();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                Books Boo = new Books();
                Boo.ISBN = Convert.ToInt32(fields[0]);
                Boo.title = fields[1];
                Boo.author = fields[2];
                Boo.yearPublished = fields[3];
                Boo.unitPrice = Convert.ToDecimal(fields[4]);                
                Boo.QOH = Convert.ToInt32(fields[5]);
                Boo.publisher = fields[6];                
                listB.Add(Boo);
                line = sReader.ReadLine();

            }
            sReader.Close();
            return listB;
        }

        public static void Delete(int BooISBN)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((BooISBN) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();

            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }

        public static void Update(Books Boo)
        {
            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((Convert.ToInt32(fields[0]) != (Boo.ISBN)))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                }

                line = sReader.ReadLine();
            }
            sWriter.WriteLine(Boo.ISBN + "," + Boo.title + "," + Boo.author + "," + Boo.yearPublished + "," + Boo.unitPrice + "," + Boo.QOH + "," + Boo.publisher);
            sReader.Close();
            sWriter.Close();
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }

        public static Books Search(int txtInput)
        {
            Books Boo = new Books();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == Convert.ToInt32(fields[0]))
                {
                    Boo.ISBN = Convert.ToInt32(fields[0]);
                    Boo.title = fields[1];
                    Boo.author = fields[2];
                    Boo.yearPublished = fields[3];
                    Boo.unitPrice = Convert.ToDecimal(fields[4]);
                    Boo.QOH = Convert.ToInt32(fields[5]);
                    Boo.publisher = fields[6];
                    sReader.Close();
                    return Boo;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Books SearchByTitle(string txtInput)
        {
            Books Boo = new Books();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[1])
                {
                    Boo.ISBN = Convert.ToInt32(fields[0]);
                    Boo.title = fields[1];
                    Boo.author = fields[2];
                    Boo.yearPublished = fields[3];
                    Boo.unitPrice = Convert.ToDecimal(fields[4]);
                    Boo.QOH = Convert.ToInt32(fields[5]);
                    Boo.publisher = fields[6];
                    sReader.Close();
                    return Boo;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Books SearchByAuthor(string txtInput)
        {
            Books Boo = new Books();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[2])
                {
                    Boo.ISBN = Convert.ToInt32(fields[0]);
                    Boo.title = fields[1];
                    Boo.author = fields[2];
                    Boo.yearPublished = fields[3];
                    Boo.unitPrice = Convert.ToDecimal(fields[4]);
                    Boo.QOH = Convert.ToInt32(fields[5]);
                    Boo.publisher = fields[6];
                    sReader.Close();
                    return Boo;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static void BoxProducts(ComboBox comboBoxProducts)
        {
            StreamReader sReader = new StreamReader(filePath);

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                comboBoxProducts.Items.Add(fields[1]);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

    }
}
