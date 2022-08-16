using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEurope
{
    

    public partial class Form2 : Form
    {
        UserEnteredForm enteredForm = new UserEnteredForm();
        NewEuropeDBEntities entities = new NewEuropeDBEntities();
        public Form2()
        {
           
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pnl_login.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            pnl_customer.Show();
            pnl_manager.Hide();
        }

        private void btn_manager_Click(object sender, EventArgs e)
        {
            pnl_manager.Show();
            pnl_customer.Hide();
        }
       
        private void btn_user_enter_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            var control = bll.ControlLogin(txt_username.Text, txt_password.Text);
            if (control>0)
            {
                UserEnteredForm userEnteredForm = new UserEnteredForm();
                userEnteredForm.CustName = txt_username.Text;
                userEnteredForm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Please, enter correctly!");
                txt_password.Text = "";

            }

        }
        NewUserSignUp signUp = new NewUserSignUp();
        private void label8_Click(object sender, EventArgs e)
        {
            signUp.Show();
            this.Hide();

        }
        ManagerForm managerForm = new ManagerForm();
        private void btn_enter_system_Click(object sender, EventArgs e)
        {
            if (txt_systemName.Text == "booleanteach" && txt_system_password.Text == "bool123")
            {
                managerForm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Please enter correctly!");
                txt_system_password.Text = "";
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_password.PasswordChar = '\0';
            }
            else
            {
                txt_password.PasswordChar = '*';
            }
        }

        private void txt_systemName_Leave(object sender, EventArgs e)
        {
            if (txt_systemName.Text == "")
            {
                txt_systemName.Text = "Username";
                txt_systemName.ForeColor = Color.Silver;

            }
        }

        private void txt_systemName_Enter(object sender, EventArgs e)
        {
            if (txt_systemName.Text == "Username")
            {
                txt_systemName.Text = "";
                txt_systemName.ForeColor = Color.Black;

            }
        }

        private void txt_system_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_system_password_Leave(object sender, EventArgs e)
        {
            if (txt_system_password.Text == "")
            {
                txt_system_password.Text = "Password";
                txt_system_password.ForeColor = Color.Silver;

            }
        }

        private void txt_system_password_Enter(object sender, EventArgs e)
        {
            if (txt_system_password.Text == "Password")
            {
                txt_system_password.Text = "";
                txt_system_password.ForeColor = Color.Black;

            }
        }

        private void txt_username_Enter(object sender, EventArgs e)
        {
            if (txt_username.Text == "Username")
            {
                txt_username.Text = "";
                txt_username.ForeColor = Color.Black;

            }
        }

        private void txt_username_Leave(object sender, EventArgs e)
        {
            if (txt_username.Text == "")
            {
                txt_username.Text = "Username";
                txt_username.ForeColor = Color.Silver;

            }
        }

        private void txt_password_Enter(object sender, EventArgs e)
        {

            if (txt_password.Text == "Password")
            {
                txt_password.Text = "";
                txt_password.ForeColor = Color.Black;

            }
        }

        private void txt_password_Leave(object sender, EventArgs e)
        {
            if (txt_password.Text == "")
            {
                txt_password.Text = "Password";
                txt_password.ForeColor = Color.Silver;

            }
        }
    }
}
