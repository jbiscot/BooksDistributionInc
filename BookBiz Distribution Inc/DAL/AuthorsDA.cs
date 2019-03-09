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
    public class AuthorsDA
    {

        public static string filePath = Application.StartupPath + @"\Authors.dat";
        public static string fileTemp = Application.StartupPath + @"\Temp.dat";

        public static void Save(Authors aAuthor)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(aAuthor.authorId + "," + aAuthor.authorFName + "," + aAuthor.authorLName + "," + aAuthor.authorEmail);
            sWriter.Close();
            MessageBox.Show("Author Data Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ListAuthors(ListView listViewAuthors)
        {
            StreamReader sReader = new StreamReader(filePath);
            listViewAuthors.Items.Clear();

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);
                listViewAuthors.Items.Add(item);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        public static List<Authors> ListAuthors()
        {
            List<Authors> listA = new List<Authors>();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                Authors Aut = new Authors();
                Aut.authorId = Convert.ToInt32(fields[0]);
                Aut.authorFName = fields[1];
                Aut.authorLName = fields[2];
                Aut.authorEmail = fields[3];
                listA.Add(Aut);
                line = sReader.ReadLine();
            }
            sReader.Close();
            return listA;
        }

        public static void Delete(int AutId)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((AutId) != (Convert.ToSingle(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();

            File.Delete(filePath);
            File.Move(fileTemp, filePath);

        }

        public static void Update(Authors Aut)
        {
            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((Convert.ToInt32(fields[0]) != (Aut.authorId)))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }

                line = sReader.ReadLine();
            }
            sWriter.WriteLine(Aut.authorId + "," + Aut.authorFName + "," + Aut.authorLName + "," + Aut.authorEmail);
            sReader.Close();
            sWriter.Close();
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }

        public static Authors Search(int txtInput)
        {
            Authors Aut = new Authors();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == Convert.ToInt32(fields[0]))
                {
                    Aut.authorId = Convert.ToInt32(fields[0]);
                    Aut.authorFName = fields[1];
                    Aut.authorLName = fields[2];
                    Aut.authorEmail = fields[3];
                    sReader.Close();
                    return Aut;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Authors SearchByFName(string txtInput)
        {
            Authors Aut = new Authors();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[1])
                {
                    Aut.authorId = Convert.ToInt32(fields[0]);
                    Aut.authorFName = fields[1];
                    Aut.authorLName = fields[2];
                    Aut.authorEmail = fields[3];
                    sReader.Close();
                    return Aut;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Authors SearchByLName(string txtInput)
        {
            Authors Aut = new Authors();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (txtInput == fields[2])
                {
                    Aut.authorId = Convert.ToInt32(fields[0]);
                    Aut.authorFName = fields[1];
                    Aut.authorLName = fields[2];
                    Aut.authorEmail = fields[3];
                    sReader.Close();
                    return Aut;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }
        public static void BoxAuthor(ComboBox comboBoxAuthor)
        {
            StreamReader sReader = new StreamReader(filePath);

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                comboBoxAuthor.Items.Add(fields[2] + "; " + fields[1]);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }
    }
}
