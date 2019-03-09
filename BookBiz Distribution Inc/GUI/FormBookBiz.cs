using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookBiz_Distribution_Inc.BLL;
using BookBiz_Distribution_Inc.DAL;
using BookBiz_Distribution_Inc.Validation;
using System.IO;


namespace BookBiz_Distribution_Inc.GUI
{
    public partial class FormBookBiz : Form
    {
        List<Authors> ListA = new List<Authors>();
        List<Books> ListB = new List<Books>();
        List<Clients> ListC = new List<Clients>();
        List<Employee> ListE = new List<Employee>();

        public FormBookBiz(int employeeLoged)
        {
            InitializeComponent();
            tabPage1.Enabled = false;
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;
            tabPage4.Enabled = false;
            tabPage5.Enabled = false;
            int employee = employeeLoged;
            textBoxOrdEmployee.Text = employeeLoged.ToString();
            UpdateComboBoxes();
        }

        public void AccessManager()
        {
            tabPage1.Enabled = true;
        }

        public void AccessSalesManager()
        {
            tabPage4.Enabled = true;
        }

        public void AccessInventoryController()
        {
            tabPage3.Enabled = true;
            tabPage5.Enabled = true;
        }

        public void AccessOrderClerk()
        {
            tabPage2.Enabled = true;
        }

        private void UpdateComboBoxes()
        {
            comboBoxOrdClient.Items.Clear();
            comboBoxOrdProduct.Items.Clear();
            comboBoxBooAuthor.Items.Clear();
            comboBoxOrdClient.Items.Clear();
            comboBoxOrdProduct.Items.Clear();
            ClientsDA.BoxClients(comboBoxOrdClient);
            BooksDA.BoxProducts(comboBoxOrdProduct);
            AuthorsDA.BoxAuthor(comboBoxBooAuthor);

            //OrderDA.bookSelection(comboBoxOrdISBN);
        }

        private void ClearAllEmp()
        {
            textBoxEmpID.Clear();
            textBoxEmpFName.Clear();
            textBoxEmpLName.Clear();
            comboBoxJobTitle.Text = "";
            textBoxEmpPassword.Clear();
            textBoxEmpID.Focus();
        }

        private void ClearAllAut()
        {
            textBoxAutID.Clear();
            textBoxAutFName.Clear();
            textBoxAutLName.Clear();
            textBoxAutEmail.Clear();
            textBoxAutID.Focus();
        }

        private void ClearAllCli()
        {
            textBoxCliName.Clear();
            maskedTextBoxCliPhone.Clear();
            textBoxCliCity.Clear();
            textBoxCliPostalCode.Clear();
            textBoxCliStreet.Clear();
            textBoxCliCredit.Clear();
        }

        private void ClearAllBoo()
        {
            textBoxBooISBN.Clear();
            textBoxBooTitle.Clear();
            comboBoxBooAuthor.Text = "";
            textBoxBooYPublished.Clear();
            textBoxBooPrice.Clear();
            textBoxBooQOH.Clear();
            comboBoxBooSuplier.Text = "";
        }

        private void FormBookBiz_Load(object sender, EventArgs e)
        {
            listViewEmp.FullRowSelect = true;
            listViewAut.FullRowSelect = true;
            listViewBoo.FullRowSelect = true;
            listViewOrd.FullRowSelect = true;
            listViewCli.FullRowSelect = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            //Employee ID Validators
            if (!Validator.IsEmpty(textBoxEmpID))
            {
                MessageBox.Show("Employee ID field must have 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmpID.Clear();
                textBoxEmpID.Focus();
                return;
            }

            if (!Validator.IsValidId(textBoxEmpID.Text, 4))
            {
                MessageBox.Show("Employee ID field must have 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmpID.Clear();
                textBoxEmpID.Focus();
                return;
            }


            //if (Validator.IsDuplicate(ListE, Convert.ToInt32(textBoxEmpID.Text)))
            //{
            //    MessageBox.Show("Employee Id already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpID.Clear();
            //    textBoxEmpID.Focus();
            //    return;
            //}


            //Employee First and Last Name Validators
            //if (Validator.IsValidName(textBoxEmpFName))
            //{
            //    MessageBox.Show("Employee First Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpFName.Clear();
            //    textBoxEmpFName.Focus();
            //    return;
            //}

            //if (Validator.IsValidName(textBoxEmpLName))
            //{
            //    MessageBox.Show("Employee Last Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpLName.Clear();
            //    textBoxEmpLName.Focus();
            //    return;
            //}

            if (!Validator.IsEmptyComboBox(comboBoxJobTitle))
            {
                MessageBox.Show("Employee must have a Job Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxJobTitle.Focus();
                return;
            }

            //Saving the data into the text file
            Employee aEmployee = new Employee();
            aEmployee.employeeId = Convert.ToInt32(textBoxEmpID.Text);
            aEmployee.firstName = textBoxEmpFName.Text;
            aEmployee.lastName = textBoxEmpLName.Text;
            aEmployee.jobTitle = comboBoxJobTitle.Text;
            aEmployee.password = textBoxEmpPassword.Text;
            EmployeeDA.Save(aEmployee);
            ClearAllEmp();
            buttonEmpList.PerformClick();
            UpdateComboBoxes();
        }

        private void buttonEmpList_Click(object sender, EventArgs e)
        {
            listViewEmp.Items.Clear();
            EmployeeDA.ListEmployees(listViewEmp);
        }

        private void buttonEmpDelete_Click(object sender, EventArgs e)
        {

            int itemselected = Convert.ToInt32(textBoxEmpID.Text);
            EmployeeDA.Delete(itemselected);
            MessageBox.Show("Employee record has been deleted successfully", "Confirmation");
            ClearAllEmp();
            UpdateComboBoxes();
            buttonEmpList.PerformClick();
        }

        private void buttonEmpUpdate_Click(object sender, EventArgs e)
        {
            Employee Emp = new Employee();
            Emp.employeeId = Convert.ToInt32(textBoxEmpID.Text);
            Emp.firstName = textBoxEmpFName.Text;
            Emp.lastName = textBoxEmpLName.Text;
            Emp.jobTitle = comboBoxJobTitle.Text;
            Emp.password = textBoxEmpPassword.Text;
            DialogResult ans = MessageBox.Show("Do you really want to update this Employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                EmployeeDA.Update(Emp);
                MessageBox.Show("Customer record has been updated successfully", "Confirmation");
                ClearAllEmp();
                UpdateComboBoxes();
                buttonEmpList.PerformClick();
            }
        }

        private void buttonEmpSearch_Click(object sender, EventArgs e)
        {
            int choice = comboBoxEmpSearch.SelectedIndex;
            switch (choice)
            {
                case -1:
                    MessageBox.Show("Please select the search option");
                    break;
                case 0: //Search by ID
                    Employee Emp = EmployeeDA.Search(Convert.ToInt32(textBoxEmpSearch.Text));
                    if (Emp != null)
                    {
                        textBoxEmpID.Text = (Emp.employeeId).ToString();
                        textBoxEmpFName.Text = Emp.firstName;
                        textBoxEmpLName.Text = Emp.lastName;
                        textBoxEmpPassword.Text = Emp.password;
                        comboBoxJobTitle.Text = Emp.jobTitle;

                        buttonEmpDelete.Enabled = true;
                        buttonEmpUpdate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Customer not Found!");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                    }
                    break;

                case 1: //Search by First Name
                    Employee EmpFName = EmployeeDA.SearchByFName(textBoxEmpSearch.Text);
                    if (EmpFName != null)
                    {
                        textBoxEmpID.Text = (EmpFName.employeeId).ToString();
                        textBoxEmpFName.Text = EmpFName.firstName;
                        textBoxEmpLName.Text = EmpFName.lastName;
                        textBoxEmpPassword.Text = EmpFName.password;
                        comboBoxJobTitle.Text = EmpFName.jobTitle;

                        buttonEmpDelete.Enabled = true;
                        buttonEmpUpdate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Customer not Found!");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                    }
                    break;


                case 2: //Search by Last Name
                    Employee EmpLName = EmployeeDA.SearchByLName(textBoxEmpSearch.Text);
                    if (EmpLName != null)
                    {
                        textBoxEmpID.Text = (EmpLName.employeeId).ToString();
                        textBoxEmpFName.Text = EmpLName.firstName;
                        textBoxEmpLName.Text = EmpLName.lastName;
                        textBoxEmpPassword.Text = EmpLName.password;
                        comboBoxJobTitle.Text = EmpLName.jobTitle;

                        buttonEmpDelete.Enabled = true;
                        buttonEmpUpdate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Customer not Found!");
                        textBoxEmpSearch.Clear();
                        textBoxEmpSearch.Focus();
                    }
                    break;

                default:
                    break;
            }
        }

        private void listViewEmp_MouseClick(object sender, MouseEventArgs e)
        {
            string empID = listViewEmp.SelectedItems[0].SubItems[0].Text;
            string empFirstName = listViewEmp.SelectedItems[0].SubItems[1].Text;
            string empLastName = listViewEmp.SelectedItems[0].SubItems[2].Text;
            string empJobtitle = listViewEmp.SelectedItems[0].SubItems[3].Text;
            string empPassword = listViewEmp.SelectedItems[0].SubItems[4].Text;

            textBoxEmpID.Text = empID;
            textBoxEmpLName.Text = empFirstName;
            textBoxEmpFName.Text = empLastName;
            comboBoxJobTitle.Text = empJobtitle;
            textBoxEmpPassword.Text = empPassword;


            buttonEmpDelete.Enabled = true;
            buttonEmpUpdate.Enabled = true;
        }

        private void buttonAutSave_Click(object sender, EventArgs e)
        {
            //Author ID Validators
            if (!Validator.IsEmpty(textBoxAutID))
            {
                MessageBox.Show("Author ID field must have 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAutID.Clear();
                textBoxAutID.Focus();
                return;
            }

            if (!Validator.IsValidId(textBoxAutID.Text, 4))
            {
                MessageBox.Show("Author ID field must have 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAutID.Clear();
                textBoxAutID.Focus();
                return;
            }


            //if (Validator.IsDuplicate(ListE, Convert.ToInt32(textBoxEmpID.Text)))
            //{
            //    MessageBox.Show("Employee Id already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpID.Clear();
            //    textBoxEmpID.Focus();
            //    return;
            //}


            //Employee First and Last Name Validators
            //if (Validator.IsValidName(textBoxEmpFName))
            //{
            //    MessageBox.Show("Employee First Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpFName.Clear();
            //    textBoxEmpFName.Focus();
            //    return;
            //}

            //if (Validator.IsValidName(textBoxEmpLName))
            //{
            //    MessageBox.Show("Employee Last Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpLName.Clear();
            //    textBoxEmpLName.Focus();
            //    return;
            //}


            //Saving the data into the text file
            Authors aAuthor = new Authors();
            aAuthor.authorId = Convert.ToInt32(textBoxAutID.Text);
            aAuthor.authorFName = textBoxAutFName.Text;
            aAuthor.authorLName = textBoxAutLName.Text;
            aAuthor.authorEmail = textBoxAutEmail.Text;
            AuthorsDA.Save(aAuthor);
            ClearAllAut();
            buttonAutList.PerformClick();
            UpdateComboBoxes();
        }

        private void buttonAutList_Click(object sender, EventArgs e)
        {
            listViewEmp.Items.Clear();
            AuthorsDA.ListAuthors(listViewAut);
        }

        private void buttonAutUpdate_Click(object sender, EventArgs e)
        {
            Authors Aut = new Authors();
            Aut.authorId = Convert.ToInt32(textBoxAutID.Text);
            Aut.authorFName = textBoxAutFName.Text;
            Aut.authorLName = textBoxAutLName.Text;
            Aut.authorEmail = textBoxAutEmail.Text;
            DialogResult ans = MessageBox.Show("Do you really want to update this Author?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                AuthorsDA.Update(Aut);
                MessageBox.Show("Author record has been updated successfully", "Confirmation");
                ClearAllAut();
                UpdateComboBoxes();
                buttonAutList.PerformClick();
            }
        }

        private void buttonAutDelete_Click(object sender, EventArgs e)
        {
            int itemselected = Convert.ToInt32(textBoxAutID.Text);
            AuthorsDA.Delete(itemselected);
            MessageBox.Show("Author record has been deleted successfully", "Confirmation");
            ClearAllAut();
            UpdateComboBoxes();
            buttonAutList.PerformClick();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int choice = comboBoxAutSearch.SelectedIndex;
            switch (choice)
            {
                case -1:
                    MessageBox.Show("Please select the search option");
                    break;
                case 0: //Search by ID
                    Authors Aut = AuthorsDA.Search(Convert.ToInt32(textBoxAutSearch.Text));
                    if (Aut != null)
                    {
                        textBoxAutID.Text = (Aut.authorId).ToString();
                        textBoxAutFName.Text = Aut.authorLName;
                        textBoxAutLName.Text = Aut.authorLName;
                        textBoxAutEmail.Text = Aut.authorEmail;

                        buttonAutDelete.Enabled = true;
                        buttonAutUpdate.Enabled = true;
                    }

                    else
                    {
                        MessageBox.Show("Author not Found!");
                        textBoxAutSearch.Clear();
                        textBoxAutSearch.Focus();
                    }
                    break;

                case 1: //Search by First Name
                    Authors AutFName = AuthorsDA.SearchByFName(textBoxAutSearch.Text);
                    if (AutFName != null)
                    {
                        textBoxAutID.Text = (AutFName.authorId).ToString();
                        textBoxAutFName.Text = AutFName.authorFName;
                        textBoxAutLName.Text = AutFName.authorLName;
                        textBoxAutEmail.Text = AutFName.authorEmail;

                        buttonAutDelete.Enabled = true;
                        buttonAutUpdate.Enabled = true;
                    }

                    else
                    {
                        MessageBox.Show("Author not Found!");
                        textBoxAutSearch.Clear();
                        textBoxAutSearch.Focus();
                    }
                    break;


                case 2: //Search by Last Name
                    Authors AutLName = AuthorsDA.SearchByLName(textBoxAutSearch.Text);
                    if (AutLName != null)
                    {
                        textBoxAutID.Text = (AutLName.authorId).ToString();
                        textBoxAutFName.Text = AutLName.authorFName;
                        textBoxAutLName.Text = AutLName.authorLName;
                        textBoxAutEmail.Text = AutLName.authorEmail;

                        buttonAutDelete.Enabled = true;
                        buttonAutUpdate.Enabled = true;
                    }

                    else
                    {
                        MessageBox.Show("Author not Found!");
                        textBoxAutSearch.Clear();
                        textBoxAutSearch.Focus();
                    }
                    break;



                default:
                    break;
            }
        }

        private void listViewAut_MouseClick(object sender, MouseEventArgs e)
        {
            string AutID = listViewAut.SelectedItems[0].SubItems[0].Text;
            string AutFirstName = listViewAut.SelectedItems[0].SubItems[1].Text;
            string AutLastName = listViewAut.SelectedItems[0].SubItems[2].Text;
            string AutEmail = listViewAut.SelectedItems[0].SubItems[3].Text;

            textBoxAutID.Text = AutID;
            textBoxAutLName.Text = AutFirstName;
            textBoxAutFName.Text = AutLastName;
            textBoxAutEmail.Text = AutEmail;

            buttonAutDelete.Enabled = true;
            buttonAutUpdate.Enabled = true;
        }

        private void buttonCliSave_Click(object sender, EventArgs e)
        {
            //Client Validators

            //if (!Validator.IsEmpty(textBoxCliName))
            //{
            //    MessageBox.Show("Author ID field must have 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxCliName.Clear();
            //    textBoxCliName.Focus();
            //    return;
            //}

            //if (!Validator.IsValidId(textBoxCliName.Text, 4))
            //{
            //    MessageBox.Show("Author ID field must have 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBoxCliName.Clear();
            //    textBoxCliName.Focus();
            //    return;
            //}


            //if (Validator.IsDuplicate(ListE, Convert.ToInt32(textBoxEmpID.Text)))
            //{
            //    MessageBox.Show("Employee Id already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpID.Clear();
            //    textBoxEmpID.Focus();
            //    return;
            //}


            //Employee First and Last Name Validators
            //if (Validator.IsValidName(textBoxEmpFName))
            //{
            //    MessageBox.Show("Employee First Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpFName.Clear();
            //    textBoxEmpFName.Focus();
            //    return;
            //}

            //if (Validator.IsValidName(textBoxEmpLName))
            //{
            //    MessageBox.Show("Employee Last Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpLName.Clear();
            //    textBoxEmpLName.Focus();
            //    return;
            //}

            if (string.IsNullOrEmpty(textBoxCliCredit.Text))
            {
                textBoxCliCredit.Text = "0";
            }

            //Saving the data into the text file
            Clients aClient = new Clients();
            aClient.clientName = textBoxCliName.Text;
            aClient.phoneNumber = maskedTextBoxCliPhone.Text;
            aClient.city = textBoxCliCity.Text;
            aClient.street = textBoxCliStreet.Text;
            aClient.postalCode = textBoxCliPostalCode.Text;
            aClient.creditLimit = Convert.ToDecimal(textBoxCliCredit.Text);
            ClientsDA.Save(aClient);
            ClearAllCli();
            UpdateComboBoxes();
            buttonCliList.PerformClick();
        }

        private void textBoxBooQOH_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCliUpdate_Click(object sender, EventArgs e)
        {
            Clients aClient = new Clients();
            aClient.clientName = textBoxCliName.Text;
            aClient.phoneNumber = maskedTextBoxCliPhone.Text;
            aClient.city = textBoxCliCity.Text;
            aClient.street = textBoxCliStreet.Text;
            aClient.postalCode = textBoxCliPostalCode.Text;
            aClient.creditLimit = Convert.ToDecimal(textBoxCliCredit.Text);

            DialogResult ans = MessageBox.Show("Do you really want to update this Client?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                ClientsDA.Update(aClient);
                MessageBox.Show("Client record has been updated successfully", "Confirmation");
                ClearAllCli();
                UpdateComboBoxes();
                buttonCliList.PerformClick();
            }
        }

        private void buttonCliDelete_Click(object sender, EventArgs e)
        {
            string itemselected = textBoxCliName.Text;
            ClientsDA.Delete(itemselected);
            MessageBox.Show("Client record has been deleted successfully", "Confirmation");
            ClearAllCli();
            UpdateComboBoxes();
            buttonCliList.PerformClick();

        }

        private void buttonCliList_Click(object sender, EventArgs e)
        {
            listViewCli.Items.Clear();
            ClientsDA.ListClients(listViewCli);
        }

        private void listViewCli_MouseClick(object sender, MouseEventArgs e)
        {
            string CliName = listViewCli.SelectedItems[0].SubItems[0].Text;
            string CliPhone = listViewCli.SelectedItems[0].SubItems[1].Text;
            string CliCity = listViewCli.SelectedItems[0].SubItems[2].Text;
            string CliStreet = listViewCli.SelectedItems[0].SubItems[3].Text;
            string CliPostalCode = listViewCli.SelectedItems[0].SubItems[4].Text;
            string CliCreditLimit = listViewCli.SelectedItems[0].SubItems[5].Text;

            textBoxCliName.Text = CliName;
            maskedTextBoxCliPhone.Text = CliPhone;
            textBoxCliCity.Text = CliCity;
            textBoxCliStreet.Text = CliStreet;
            textBoxCliPostalCode.Text = CliPostalCode;
            textBoxCliCredit.Text = CliCreditLimit;

            buttonCliDelete.Enabled = true;
            buttonCliUpdate.Enabled = true;
        }

        private void buttonCliSearch_Click(object sender, EventArgs e)
        {
            int choice = comboBoxCliSearch.SelectedIndex;
            switch (choice)
            {
                case -1:
                    MessageBox.Show("Please select the search option");
                    break;
                case 0: //Search by Client Name
                    Clients Cli = ClientsDA.Search(textBoxCliSearch.Text);
                    if (Cli != null)
                    {
                        textBoxCliName.Text = Cli.clientName;
                        maskedTextBoxCliPhone.Text = Cli.phoneNumber;
                        textBoxCliCity.Text = Cli.city;
                        textBoxCliPostalCode.Text = Cli.postalCode;
                        textBoxCliStreet.Text = Cli.street;
                        textBoxCliCredit.Text = Cli.creditLimit.ToString();

                        buttonCliDelete.Enabled = true;
                        buttonCliUpdate.Enabled = true;
                    }

                    else
                    {
                        MessageBox.Show("Client not Found!");
                        textBoxCliSearch.Clear();
                        textBoxCliSearch.Focus();
                    }
                    break;

                case 1: //Search by Client City
                    Clients CliCity = ClientsDA.SearchByCity(textBoxCliSearch.Text);
                    if (CliCity != null)
                    {
                        textBoxCliName.Text = CliCity.clientName;
                        maskedTextBoxCliPhone.Text = CliCity.phoneNumber;
                        textBoxCliCity.Text = CliCity.city;
                        textBoxCliPostalCode.Text = CliCity.postalCode;
                        textBoxCliStreet.Text = CliCity.street;
                        textBoxCliCredit.Text = CliCity.creditLimit.ToString();

                        buttonCliDelete.Enabled = true;
                        buttonCliUpdate.Enabled = true;
                    }

                    else
                    {
                        MessageBox.Show("Client not Found!");
                        textBoxCliSearch.Clear();
                        textBoxCliSearch.Focus();
                    }
                    break;

                default:
                    break;
            }


        }

        private void buttonBooSave_Click(object sender, EventArgs e)
        {
            //BOOK ISBN Validators
            if (!Validator.IsEmpty(textBoxBooISBN))
            {
                MessageBox.Show("The Book have to have a ISBN code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBooISBN.Clear();
                textBoxBooISBN.Focus();
                return;
            }

            if (!Validator.IsEmpty(textBoxBooQOH))
            {
                MessageBox.Show("The Book must have a quantity!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBooQOH.Clear();
                textBoxBooQOH.Focus();
                return;
            }


            if (!Validator.IsValidId(textBoxBooISBN.Text, 4))
            {
                MessageBox.Show("Book must have a 4 digits ISBN Code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBooISBN.Clear();
                textBoxBooISBN.Focus();
                return;
            }

            if (!Validator.IsValidQOH(textBoxBooQOH.Text))
            {
                MessageBox.Show("Quantity must be a integer number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBooQOH.Clear();
                textBoxBooQOH.Focus();
                return;
            }



            //if (Validator.IsDuplicate(ListE, Convert.ToInt32(textBoxEmpID.Text)))
            //{
            //    MessageBox.Show("Employee Id already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpID.Clear();
            //    textBoxEmpID.Focus();
            //    return;
            //}


            //Employee First and Last Name Validators
            //if (Validator.IsValidName(textBoxEmpFName))
            //{
            //    MessageBox.Show("Employee First Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpFName.Clear();
            //    textBoxEmpFName.Focus();
            //    return;
            //}

            //if (Validator.IsValidName(textBoxEmpLName))
            //{
            //    MessageBox.Show("Employee Last Name cannot have digits or white spaces!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    textBoxEmpLName.Clear();
            //    textBoxEmpLName.Focus();
            //    return;
            //}

            //Saving the data into the text file
            Books aBook = new Books();
            aBook.ISBN = Convert.ToInt32(textBoxBooISBN.Text);
            aBook.title = textBoxBooTitle.Text;
            aBook.author = comboBoxBooAuthor.Text;
            aBook.yearPublished = textBoxBooYPublished.Text;
            aBook.unitPrice = Convert.ToDecimal(textBoxBooPrice.Text);
            aBook.QOH = Convert.ToInt32(textBoxBooQOH.Text);
            aBook.publisher = comboBoxBooSuplier.Text;
            BooksDA.Save(aBook);
            ClearAllBoo();
            UpdateComboBoxes();
            buttonBooList.PerformClick();
        }

        private void buttonBooUpdate_Click(object sender, EventArgs e)
        {
            Books aBook = new Books();
            aBook.ISBN = Convert.ToInt32(textBoxBooISBN.Text);
            aBook.title = textBoxBooTitle.Text;
            aBook.author = comboBoxBooAuthor.Text;
            aBook.yearPublished = textBoxBooYPublished.Text;
            aBook.unitPrice = Convert.ToDecimal(textBoxBooPrice.Text);
            aBook.QOH = Convert.ToInt32(textBoxBooQOH.Text);
            aBook.publisher = comboBoxBooSuplier.Text;
            DialogResult ans = MessageBox.Show("Do you really want to update this Book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                BooksDA.Update(aBook);
                MessageBox.Show("Book record has been updated successfully", "Confirmation");
                ClearAllBoo();
                UpdateComboBoxes();
                buttonBooList.PerformClick();
            }
        }

        private void buttonBooDelete_Click(object sender, EventArgs e)
        {
            string itemselected = textBoxBooISBN.Text;
            BooksDA.Delete(Convert.ToInt32(itemselected));
            MessageBox.Show("Book record has been deleted successfully", "Confirmation");
            ClearAllBoo();
            UpdateComboBoxes();
            buttonBooList.PerformClick();
        }

        private void buttonBooList_Click(object sender, EventArgs e)
        {
            listViewBoo.Items.Clear();
            BooksDA.ListBooks(listViewBoo);
        }

        private void listViewBoo_MouseClick(object sender, MouseEventArgs e)
        {
            string BooISBN = listViewBoo.SelectedItems[0].SubItems[0].Text;
            string BooTitle = listViewBoo.SelectedItems[0].SubItems[1].Text;
            string BooAuthor = listViewBoo.SelectedItems[0].SubItems[2].Text;
            string BooYPublished = listViewBoo.SelectedItems[0].SubItems[3].Text;
            string BooPrice = listViewBoo.SelectedItems[0].SubItems[4].Text;
            string BooQOH = listViewBoo.SelectedItems[0].SubItems[5].Text;
            string BooSuplier = listViewBoo.SelectedItems[0].SubItems[6].Text;

            textBoxBooISBN.Text = BooISBN;
            textBoxBooTitle.Text = BooTitle;
            comboBoxBooAuthor.Text = BooAuthor;
            textBoxBooYPublished.Text = BooYPublished;
            textBoxBooPrice.Text = BooPrice;
            textBoxBooQOH.Text = BooQOH;
            comboBoxBooSuplier.Text = BooSuplier;

            buttonBooDelete.Enabled = true;
            buttonBooUpdate.Enabled = true;
        }

        private void buttonBooSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxBooSearch.Text))
            {

                int choice = comboBoxBooSearch.SelectedIndex;
                switch (choice)
                {
                    case -1:
                        MessageBox.Show("Please select the search option");
                        break;

                    case 0: //Search by ISBN Number
                        Books Boo = BooksDA.Search(Convert.ToInt32(textBoxBooSearch.Text));
                        if (Boo != null)
                        {
                            textBoxBooISBN.Text = Boo.ISBN.ToString();
                            textBoxBooTitle.Text = Boo.title;
                            comboBoxBooAuthor.Text = Boo.author;
                            textBoxBooYPublished.Text = Boo.yearPublished;
                            textBoxBooPrice.Text = Boo.unitPrice.ToString();
                            textBoxBooQOH.Text = Boo.QOH.ToString();
                            comboBoxBooSuplier.Text = Boo.publisher;

                            buttonBooDelete.Enabled = true;
                            buttonBooUpdate.Enabled = true;
                        }

                        else
                        {
                            MessageBox.Show("Client not Found!");
                            textBoxBooSearch.Clear();
                            textBoxBooSearch.Focus();
                        }
                        break;

                    case 1: //Search by Book Title
                        Books BooTitle = BooksDA.SearchByTitle(textBoxBooSearch.Text);
                        if (BooTitle != null)
                        {
                            textBoxBooISBN.Text = BooTitle.ISBN.ToString();
                            textBoxBooTitle.Text = BooTitle.title;
                            comboBoxBooAuthor.Text = BooTitle.author;
                            textBoxBooYPublished.Text = BooTitle.yearPublished;
                            textBoxBooPrice.Text = BooTitle.unitPrice.ToString();
                            textBoxBooQOH.Text = BooTitle.QOH.ToString();
                            comboBoxBooSuplier.Text = BooTitle.publisher;

                            buttonBooDelete.Enabled = true;
                            buttonBooUpdate.Enabled = true;
                        }

                        else
                        {
                            MessageBox.Show("Client not Found!");
                            textBoxBooSearch.Clear();
                            textBoxBooSearch.Focus();
                        }
                        break;


                    case 2: //Search by Book Author
                        Books BooAuthor = BooksDA.SearchByAuthor(textBoxBooSearch.Text);
                        if (BooAuthor != null)
                        {
                            textBoxBooISBN.Text = BooAuthor.ISBN.ToString();
                            textBoxBooTitle.Text = BooAuthor.title;
                            comboBoxBooAuthor.Text = BooAuthor.author;
                            textBoxBooYPublished.Text = BooAuthor.yearPublished;
                            textBoxBooPrice.Text = BooAuthor.unitPrice.ToString();
                            textBoxBooQOH.Text = BooAuthor.QOH.ToString();
                            comboBoxBooSuplier.Text = BooAuthor.publisher;

                            buttonBooDelete.Enabled = true;
                            buttonBooUpdate.Enabled = true;
                        }

                        else
                        {
                            MessageBox.Show("Client not Found!");
                            textBoxBooSearch.Clear();
                            textBoxBooSearch.Focus();
                        }
                        break;

                    default:
                        break;
                }

            }
            else
            {
                MessageBox.Show("Please, enter a valid information to search!", "Serach Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxBooSearch.Clear();
                textBoxBooSearch.Focus();
            }

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }


        //________________ORDER TAB_______________________________________________________________________

        private Order createOrder()
        {
            Order aOrder = new Order();
            Clients aClients = new Clients();
            Books aBook = new Books();
            Employee aEmployee = new Employee();

            aBook = BooksDA.SearchByTitle(comboBoxOrdProduct.Text);
            aClients = ClientsDA.Search(comboBoxOrdClient.Text);
            aEmployee = EmployeeDA.Search(Convert.ToInt32(textBoxOrdEmployee.Text));

            aOrder.OrdNumber = OrderDA.OrderID() + 1;
            aOrder.OrdEmployee = null;
            aOrder.OrdEmployee = aEmployee;
            aOrder.OrdClient = aClients;
            aOrder.OrdProduct = aBook;
            aOrder.OrdQuantity = Convert.ToInt32(textBoxOrdQuantity.Text);
            aOrder.OrdTotal = aOrder.OrdQuantity * BooksDA.SearchByTitle(comboBoxOrdProduct.Text).unitPrice;
            aOrder.OrdDate = Convert.ToDateTime(dateTimePicker1.Value);
            return aOrder;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clients result = ClientsDA.Search(comboBoxOrdClient.Text);
            if (result != null)
            {
                textBoxOrdClient.Text = result.clientName.ToString();
                textBoxOrdPhoneNumber.Text = result.phoneNumber.ToString();
                textBoxOrdCreditLimit.Text = ("$ " + result.creditLimit.ToString());
                textBoxOrdAdress.Text = (result.postalCode + ", " + result.city + " - " + result.street);

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Books result = BooksDA.SearchByTitle(comboBoxOrdProduct.Text);
            if (result != null)
            {
                textBoxOrdUnitPrice.Text = ("$ " + result.unitPrice.ToString());
            }
        }

        private void buttonOrdPlaceOrder_Click(object sender, EventArgs e)
        {

            //Order Validators
            if (!Validator.IsEmptyComboBox(comboBoxOrdClient))
            {
                MessageBox.Show("Order must have a valid Client.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAutID.Clear();
                textBoxAutID.Focus();
                return;
            }

            if (!Validator.IsEmptyComboBox(comboBoxOrdProduct))
            {
                MessageBox.Show("Order must have a valid Product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAutID.Clear();
                textBoxAutID.Focus();
                return;
            }

            if (!Validator.IsEmpty(textBoxOrdQuantity))
            {
                MessageBox.Show("Orders must have a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAutID.Clear();
                textBoxAutID.Focus();
                return;
            }

            int temp = 0;
            if ((!int.TryParse((textBoxOrdQuantity.Text), out temp)))
            {
                MessageBox.Show("Quantity of Products must be a integer number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxOrdQuantity.Clear();
                textBoxOrdQuantity.Focus();
                return;
            }

            Order aOrder = createOrder();
            Books aBook = BooksDA.SearchByTitle(comboBoxOrdProduct.Text);
            Clients aClient = ClientsDA.Search(comboBoxOrdClient.Text);

            aBook.QOH = aBook.QOH - aOrder.OrdQuantity;
            BooksDA.Update(aBook);

            aClient.creditLimit = aClient.creditLimit - aOrder.OrdTotal;
            ClientsDA.Update(aClient);

            OrderDA.SaveOrder(aOrder);
            buttonOrdList.PerformClick();
            UpdateComboBoxes();

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxOrdQuantity_TextChanged(object sender, EventArgs e)
        {
            Multiply();
        }

        private void Multiply()
        {
            if (!Validator.IsEmpty(textBoxOrdQuantity))
            {
                textBoxOrdTotal.Text = "$ ";
                return;
            }

            if (!Validator.IsValidQOH(textBoxOrdQuantity.Text))
            {
                MessageBox.Show("Quantity of Products must be a integer number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxOrdQuantity.Clear();
                textBoxOrdQuantity.Focus();
                textBoxOrdTotal.Text = "$ ";
                return;
            }

            else
            {
                textBoxOrdTotal.Text = "$ " + (Convert.ToInt32(textBoxOrdQuantity.Text) * BooksDA.SearchByTitle(comboBoxOrdProduct.Text).unitPrice).ToString();
            }
        }

        private void buttonOrdList_Click(object sender, EventArgs e)
        {
            listViewOrd.Items.Clear();
            OrderDA.ListOrder(listViewOrd);
        }

        private void buttonOrdDelete_Click(object sender, EventArgs e)
        {

            DialogResult answer = MessageBox.Show("Do you want to delete this Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (answer == DialogResult.Yes)
            {

                OrderDA.Delete(Convert.ToInt32((textBoxOrdNumber.Text)));

                //int BookQuantity = OrderDA.SearchByNumber(Convert.ToInt32(textBoxOrdNumber.Text)).OrdQuantity;
                //decimal orderCost = OrderDA.SearchByNumber(Convert.ToInt32(textBoxOrdNumber.Text)).OrdTotal;

                //Books abook = new Books();
                //Clients aclient = new Clients();


                //abook = BooksDA.SearchByTitle(comboBoxOrdProduct.Text);
                //aclient = ClientsDA.Search(comboBoxOrdClient.Text);

                //abook.QOH = abook.QOH + BookQuantity;
                //aclient.creditLimit = aclient.creditLimit + orderCost;

                OrderDA.ListOrder(listViewOrd);
                buttonOrdList.PerformClick();

                UpdateComboBoxes();
            }

        }

        private void listViewOrd_MouseClick(object sender, MouseEventArgs e)
        {
          
            string OrdNumber = listViewOrd.SelectedItems[0].SubItems[0].Text;
            string OrdEmployee = listViewOrd.SelectedItems[0].SubItems[1].Text;
            string OrdClient = ClientsDA.Search(listViewOrd.SelectedItems[0].SubItems[2].Text).clientName;
            string OrdProduct = BooksDA.SearchByTitle(listViewOrd.SelectedItems[0].SubItems[3].Text).title;
            string OrdQuantity = listViewOrd.SelectedItems[0].SubItems[4].Text;
            string OrdTotal = listViewOrd.SelectedItems[0].SubItems[5].Text;
            string OrdDate = listViewOrd.SelectedItems[0].SubItems[6].Text;

            string OrdPhoneNumber = ClientsDA.Search(listViewOrd.SelectedItems[0].SubItems[2].Text).phoneNumber;
            string OrdCreditLimit = ClientsDA.Search(listViewOrd.SelectedItems[0].SubItems[2].Text).creditLimit.ToString();
            string OrdAddress = (ClientsDA.Search(listViewOrd.SelectedItems[0].SubItems[2].Text).postalCode) + ", " + (ClientsDA.Search(listViewOrd.SelectedItems[0].SubItems[2].Text).street);
            string OrdUnitPrice = BooksDA.SearchByTitle(listViewOrd.SelectedItems[0].SubItems[3].Text).unitPrice.ToString();


            textBoxOrdNumber.Text = OrdNumber;            
            textBoxOrdClient.Text = OrdClient;
            comboBoxOrdProduct.Text = OrdProduct;
            textBoxOrdQuantity.Text = OrdQuantity;
            textBoxOrdTotal.Text = OrdTotal;
            dateTimePicker1.Text = OrdDate;
            textBoxOrdPhoneNumber.Text = OrdPhoneNumber;
            textBoxOrdCreditLimit.Text = OrdCreditLimit;
            textBoxOrdAdress.Text = OrdAddress;
            textBoxOrdUnitPrice.Text = OrdUnitPrice;


            buttonOrdDelete.Enabled = true;
            buttonOrdUpdate.Enabled = true;
        }

        private void buttonOrdUpdate_Click(object sender, EventArgs e)
        {

            if (!Validator.IsEmpty(textBoxOrdQuantity))
            {
                MessageBox.Show("Orders must have a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAutID.Clear();
                textBoxAutID.Focus();
                return;
            }

            Order aorder = createOrder();
            Order xorder = OrderDA.SearchByNumber(Convert.ToInt32(textBoxOrdNumber.Text));

            Books abook = BooksDA.SearchByTitle(comboBoxOrdProduct.Text);
            Clients aclient = ClientsDA.Search(comboBoxOrdClient.Text);

            abook.QOH = (abook.QOH + xorder.OrdQuantity) - aorder.OrdQuantity;
            BooksDA.Update(abook);

            aclient.creditLimit = (aclient.creditLimit + aorder.OrdTotal) - aorder.OrdTotal;
            ClientsDA.Update(aclient);

            OrderDA.Update(aorder);
            OrderDA.ListOrder(listViewOrd);

            buttonOrdList.PerformClick();
            UpdateComboBoxes();
        }

        private void textBoxOrdAdress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


    
    

