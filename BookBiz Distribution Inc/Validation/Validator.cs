using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookBiz_Distribution_Inc.BLL;

    

namespace BookBiz_Distribution_Inc.Validation
{
    public static class Validator
    {

  
        // Lenght and numeric value only validator
        public static void IsValidId(TextBox txtInput, int size)
        {
            int number = 0;
            if ((txtInput.TextLength != size) || (!int.TryParse(txtInput.Text, out number)))
            {
                MessageBox.Show("The Employee ID number must have 4 digits.", "Validation Error");
                txtInput.Clear();
                txtInput.Focus();
            }
        }
        public static bool IsValidId(string input, int size)
        {
            int tempId = 0;

            if ((input.Length) != size || (!int.TryParse(input, out tempId)))
            {
                return false;
            }
            return true;
        }


        public static bool IsValidQOH(string input)
        {
            int tempId = 0;
            if ((!int.TryParse(input, out tempId)))
            {
                return false;
            }
            return true;
        }
     

        //Empty field validator
        public static bool IsEmpty(TextBox txtInput)
        {
            if (txtInput.Text == "")
            {
                return false;
            }
            return true;
        }

        //Dulicate ID value validator
        public static bool IsDuplicate(List<Employee> listE, int txtInput)
        {
            foreach (Employee E in listE)
            {
                if (E.employeeId == txtInput)
                {
                    return false;
                }
            }
            return true;
        }

        //Name validator
        public static bool IsValidName(TextBox txtInput)
        {
            bool valid = true;
            for (int i = 0; i < txtInput.TextLength; i++)
            {
                if ((char.IsDigit(txtInput.Text, i)) || ((char.IsWhiteSpace(txtInput.Text, i))))
                {
                    valid = false;
                    break;
                }
            }
            return valid;
        }


        // Phone Number validator
        public static void IsValidPhone(MaskedTextBox txtInput, int size)
        {            
            if (txtInput.TextLength != size)
            {
                // tirar depois
                MessageBox.Show("The Phone Number number must have *number digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInput.Clear();
                txtInput.Focus();
            }
        }

        //Empty ComboBox validator
        public static bool IsEmptyComboBox(ComboBox txtInput)
        {
            if (txtInput.Text == "")
            {
                return false;
            }
            return true;
        }

    }
}
