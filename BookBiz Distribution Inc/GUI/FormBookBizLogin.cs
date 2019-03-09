using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BookBiz_Distribution_Inc.BLL;
using BookBiz_Distribution_Inc.DAL;
using BookBiz_Distribution_Inc.Validation;
using BookBiz_Distribution_Inc.GUI;


namespace BookBiz_Distribution_Inc.GUI
{
    public partial class FormBookBizLogin : Form
    {
        public FormBookBizLogin()
        {
            InitializeComponent();
        }        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FormBookBizLogin_Load(object sender, EventArgs e)
        {
            textBoxLoginUser. Focus();
        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {

            FormBookBiz form = new FormBookBiz(Convert.ToInt32(textBoxLoginUser.Text));
        
            if (!Validator.IsEmpty(textBoxLoginUser))
            {
                MessageBox.Show("Please enter a User ID!", "Sorry...");
                textBoxLoginUser.Clear();
                textBoxLoginUser.Focus();
                return;
            }

            if (EmployeeDA.Search(Convert.ToInt32(textBoxLoginUser.Text)) == null)
            {
                MessageBox.Show("User not found!", "Sorry...");
                textBoxLoginUser.Clear();
                textBoxPassword.Clear();
                textBoxLoginUser.Focus();
                return;
            }

            if (EmployeeDA.Search(Convert.ToInt32(textBoxLoginUser.Text)).password != textBoxPassword.Text)
            {
                MessageBox.Show("Incorrect Password, please try again!", "Sorry...");
                textBoxPassword.Clear();
                textBoxPassword.Focus();
                return;
            }

            
            //Access Level_______________________________________________________________________________

            string txtInput = EmployeeDA.Search(Convert.ToInt32(textBoxLoginUser.Text)).jobTitle;

            if (txtInput == "Inventory Controller")
            {
                this.Hide();
                form.AccessInventoryController();                         
                form.Show();               
            }
            if (txtInput == "Manager")
            {
                this.Hide();
                form.AccessManager();
                form.Show();
            }
            if (txtInput == "Sales Manager")
            {
                this.Hide();
                form.AccessSalesManager();
                form.Show();
            }
            if (txtInput == "Order Clerk")
            {
                this.Hide();
                form.AccessOrderClerk();
                form.Show();
            }
            else
            {
                return;
            }        
        }            
        
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
