using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEurope
{
    public partial class NewUserSignUp : Form
    {
        NewEuropeDBEntities entities = new NewEuropeDBEntities();
        public NewUserSignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_NewName.Clear();
            txt_NewSurname.Clear();
            txt_NewPhone.Clear();
            txt_NewUsername.Clear();
            txt_NewPassword.Clear();

        }

        private void NewUserSignUp_Load(object sender, EventArgs e)
        {

        }

        private void btn_signUp_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            int PhoneNew = Convert.ToInt32(txt_NewPhone.Text);
            bll.AddCustomerControlling(txt_NewName.Text, txt_NewSurname.Text, PhoneNew);
            var query = (from db in entities.Tbl_Customer
                         orderby db.CustomerID
                         descending
                         select db.CustomerID).FirstOrDefault();
            int IDNumber = Convert.ToInt32(query);
            bll.AddUserControlling(txt_NewUsername.Text, txt_NewPassword.Text, IDNumber);
                    MessageBox.Show("You have been registered succesfully!\nNow sign in please!");
                    txt_NewName.Clear();
                    txt_NewSurname.Clear();
                    txt_NewPhone.Clear();
                    txt_NewUsername.Clear();
                    txt_NewPassword.Clear();
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();

  
        }
    }
}
