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
    public partial class ManagerForm : Form
    {
        NewEuropeDBEntities entities = new NewEuropeDBEntities();

        public ManagerForm()
        {
            InitializeComponent();
            GetAll();
            GetAllEmp();
            GetAllEstate();

        }
        public void GetAll()
        {
            var query = entities.Tbl_Category.Select(I => I);
            dgw_Categ.DataSource = query.ToList();
        }
        public void GetAllEmp()
        {
            var query = entities.Tbl_Employee.Select(I => I);
            dgw_Employees.DataSource = query.ToList();
        }
        public void GetAllEstate()
        {
            var query = entities.Tbl_Property.Select(I => I);
            dgw_Estate.DataSource = query.ToList();
        }
        public void GetAllWithDep()
        {
            var GetData = (from t1 in entities.Tbl_Employee
                           where t1.DepartmentName == cmb_EmpDepartment.Text
                           select new { t1.EmployeeID, t1.EmployeeName, t1.EmployeeSurname, t1.DepartmentName, t1.Salary, t1.Phone }).Distinct();

            dgw_Employees.DataSource = GetData.ToList();
        }
        public void GetAllWithCategory()
        {
            var GetData = (from t1 in entities.Tbl_Property
                           join t3 in entities.Tbl_Category on t1.CategoryID equals t3.CategoryID
                           where t3.CategoryName == cmb_EstateCateg.Text
                           select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).Distinct();

            dgw_Estate.DataSource = GetData.ToList();
        }

        void style_dgw_categ()
        {
            dgw_Categ.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgw_Categ.DefaultCellStyle.SelectionBackColor = Color.Chocolate;
        }
        void style_dgw_emp()
        {
            dgw_Employees.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgw_Employees.DefaultCellStyle.SelectionBackColor = Color.Chocolate;
        }
        void style_dgw_estate()
        {
            dgw_Estate.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgw_Estate.DefaultCellStyle.SelectionBackColor = Color.Chocolate;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();

        }

        private void btn_Employees_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(60, 3);
            pnl_Employees.Show();
            pnl_Category.Hide();
            pnl_Estate.Hide();

        }

        private void btn_Categories_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(440, 3);
            pnl_Category.Show();
            pnl_Employees.Hide();
            pnl_Estate.Hide();


        }

        private void btn_Estates_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(800, 3);
            pnl_Estate.Show();
            pnl_Employees.Hide();
            pnl_Category.Hide();

        }

        private void pnl_Estate_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_Employees_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'newEuropeDBDataSet9.Tbl_Category' table. You can move, or remove it, as needed.
            this.tbl_CategoryTableAdapter1.Fill(this.newEuropeDBDataSet9.Tbl_Category);
            // TODO: This line of code loads data into the 'newEuropeDBDataSet8.Tbl_Property' table. You can move, or remove it, as needed.
            this.tbl_PropertyTableAdapter.Fill(this.newEuropeDBDataSet8.Tbl_Property);
            // TODO: This line of code loads data into the 'newEuropeDBDataSet7.Tbl_Employee' table. You can move, or remove it, as needed.
            this.tbl_EmployeeTableAdapter.Fill(this.newEuropeDBDataSet7.Tbl_Employee);
            // TODO: This line of code loads data into the 'newEuropeDBDataSet5.Tbl_Category' table. You can move, or remove it, as needed.
            this.tbl_CategoryTableAdapter.Fill(this.newEuropeDBDataSet5.Tbl_Category);
            GetAll();
            GetAllEmp();
            GetAllEstate();
            style_dgw_categ();
            style_dgw_emp();
            style_dgw_estate();
            cmb_EstateCateg.Text = "";

        }

        private void txt_SearchforCateg_Enter(object sender, EventArgs e)
        {
            if (txt_SearchforCateg.Text == "Search by name")
            {
                txt_SearchforCateg.Text = "";
                txt_SearchforCateg.ForeColor = Color.Black;
            }
        }

        private void txt_SearchforCateg_Leave(object sender, EventArgs e)
        {
            if (txt_SearchforCateg.Text == "")
            {
                txt_SearchforCateg.Text = "Search by name";
                txt_SearchforCateg.ForeColor = Color.Silver;
                GetAll();
            }
        }

        public int GetIndexCateg { get; set; }
        public int GetIndexEmp { get; set; }
        public void dgw_Categ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int getIndex = dgw_Categ.SelectedCells[0].RowIndex;
            GetIndexCateg = getIndex;
            txt_CategID.Text = dgw_Categ.Rows[getIndex].Cells[0].Value.ToString();
            txt_CategName.Text = dgw_Categ.Rows[getIndex].Cells[1].Value.ToString();
        }

        private void txt_SearchforCateg_TextChanged(object sender, EventArgs e)
        {
            string categNameSearch = txt_SearchforCateg.Text;
            var GetDataCateg = from t1 in entities.Tbl_Category
                               where t1.CategoryName.Contains(categNameSearch)
                               select t1;
            if (txt_SearchforCateg.Text == "")
            {
                GetAll();
            }
            dgw_Categ.DataSource = GetDataCateg.ToList();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_CategID.Text = "";
            txt_CategName.Text = "";
            GetAll();
        }

        private void btn_AddCateg_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int result = bll.AddCategControlling(txt_CategName.Text);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully added!");
                    txt_CategID.Text = "";
                    txt_CategName.Text = "";
                    GetAll();
                }
                else
                {
                    MessageBox.Show("Not added!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void btn_UpdateCateg_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int GetID = Convert.ToInt32(txt_CategID.Text);

                int result = bll.UpdateCategControlling(GetID, txt_CategName.Text);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully updated!");
                    GetAll();
                    txt_CategID.Text = "";
                    txt_CategName.Text = "";
                }
                else
                {
                    MessageBox.Show("Not updated!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void btn_DeleteCateg_Click(object sender, EventArgs e)
        {

        }

        private void btn_DeleteCateg_Click_1(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int GetID = Convert.ToInt32(txt_CategID.Text);

                int result = bll.DeleteCategControlling(GetID);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully deleted!");
                    txt_CategID.Text = "";
                    txt_CategName.Text = "";
                    GetAll();
                }
                else
                {
                    MessageBox.Show("Not deleted!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string EmpNameSearch = txt_Search.Text;
            var GetDataEmp = from t1 in entities.Tbl_Employee
                             where t1.EmployeeName.Contains(EmpNameSearch)
                             select t1;
            if (txt_Search.Text == "")
            {
                GetAllEmp();
            }
            dgw_Employees.DataSource = GetDataEmp.ToList();
        }

        private void dgw_Employees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int getIndex = dgw_Employees.SelectedCells[0].RowIndex;
            GetIndexEmp = getIndex;
            txt_EmpID.Text = dgw_Employees.Rows[getIndex].Cells[0].Value.ToString();
            txt_EmpName.Text = dgw_Employees.Rows[getIndex].Cells[1].Value.ToString();
            txt_EmpLastN.Text = dgw_Employees.Rows[getIndex].Cells[2].Value.ToString();
            cmb_EmpDepartment.Text = dgw_Employees.Rows[getIndex].Cells[3].Value.ToString();
            txt_EmpPhone.Text = dgw_Employees.Rows[getIndex].Cells[5].Value.ToString();
            txt_EmpSalary.Text = dgw_Employees.Rows[getIndex].Cells[4].Value.ToString();
        }

        private void cmb_EmpOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_EmpOrder.Text == "High salary")
            {
                var GetData = (from t1 in entities.Tbl_Employee
                               select new
                               {
                                   t1.EmployeeID,
                                   t1.EmployeeName,
                                   t1.EmployeeSurname,
                                   t1.DepartmentName,
                                   t1.Salary,
                                   t1.Phone
                               }).OrderByDescending(I => I.Salary);
                dgw_Employees.DataSource = GetData.ToList();

            }

            else if (cmb_EmpOrder.Text == "Low salary")
            {
                var GetData = (from t1 in entities.Tbl_Employee
                               select new
                               {
                                   t1.EmployeeID,
                                   t1.EmployeeName,
                                   t1.EmployeeSurname,
                                   t1.DepartmentName,
                                   t1.Salary,
                                   t1.Phone
                               }).OrderBy(I => I.Salary);
                dgw_Employees.DataSource = GetData.ToList();
            }
            else if (cmb_EmpOrder.Text == "By name")
            {
                var GetData = (from t1 in entities.Tbl_Employee
                               select new
                               {
                                   t1.EmployeeID,
                                   t1.EmployeeName,
                                   t1.EmployeeSurname,
                                   t1.DepartmentName,
                                   t1.Salary,
                                   t1.Phone
                               }).OrderBy(I => I.EmployeeName);
                dgw_Employees.DataSource = GetData.ToList();
            }
            else if (cmb_EmpOrder.Text == "By surname")
            {
                var GetData = (from t1 in entities.Tbl_Employee
                               select new
                               {
                                   t1.EmployeeID,
                                   t1.EmployeeName,
                                   t1.EmployeeSurname,
                                   t1.DepartmentName,
                                   t1.Salary,
                                   t1.Phone
                               }).OrderBy(I => I.EmployeeSurname);
                dgw_Employees.DataSource = GetData.ToList();
            }
            else
            {
                GetAllEmp();
            }
        }

        private void txt_Search_Leave(object sender, EventArgs e)
        {
            if (txt_Search.Text == "")
            {
                txt_Search.Text = "Search by Name";
                txt_Search.ForeColor = Color.Silver;
                GetAllEmp();
            }
        }

        private void txt_Search_Enter(object sender, EventArgs e)
        {
            if (txt_Search.Text == "Search by Name")
            {
                txt_Search.Text = "";
                txt_Search.ForeColor = Color.Black;
            }
        }

        private void btn_reset_emp_Click(object sender, EventArgs e)
        {
            txt_EmpID.Text = "";
            txt_EmpName.Text = "";
            cmb_EmpOrder.Text = "";
            txt_EmpLastN.Text = "";
            txt_EmpPhone.Text = "";
            cmb_EmpDepartment.Text = "";
            txt_EmpSalary.Text = "";
            GetAllEmp();

        }

        private void cmb_EmpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllWithDep();
        }

        private void btn_EmpAdd_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int salary = Convert.ToInt32(txt_EmpSalary.Text);
                int phone = Convert.ToInt32(txt_EmpPhone.Text);

                int result = bll.AddEmployee(txt_EmpName.Text, txt_EmpLastN.Text,
                    cmb_EmpDepartment.Text, salary, phone);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully added!");
                    txt_EmpID.Text = "";
                    txt_EmpName.Text = "";
                    txt_EmpLastN.Text = "";
                    txt_EmpPhone.Text = "";
                    cmb_EmpDepartment.Text = "";
                    txt_EmpSalary.Text = "";
                    GetAllEmp();
                }
                else
                {
                    MessageBox.Show("Not added!");
                    txt_EmpID.Text = "";
                    txt_EmpName.Text = "";
                    txt_EmpLastN.Text = "";
                    txt_EmpPhone.Text = "";
                    cmb_EmpDepartment.Text = "";
                    txt_EmpSalary.Text = "";
                    GetAllEmp();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void btn_EmpDelete_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int GetID = Convert.ToInt32(txt_EmpID.Text);

                int result = bll.DeleteEmpControlling(GetID);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully deleted!");
                    txt_EmpID.Text = "";
                    txt_EmpName.Text = "";
                    txt_EmpLastN.Text = "";
                    txt_EmpPhone.Text = "";
                    cmb_EmpDepartment.Text = "";
                    txt_EmpSalary.Text = "";
                    GetAllEmp();
                }
                else
                {
                    MessageBox.Show("Not deleted!");
                    txt_EmpID.Text = "";
                    txt_EmpName.Text = "";
                    txt_EmpLastN.Text = "";
                    txt_EmpPhone.Text = "";
                    cmb_EmpDepartment.Text = "";
                    txt_EmpSalary.Text = "";
                    GetAllEmp();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void btn_EmpUpdate_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int salary = Convert.ToInt32(txt_EmpSalary.Text);
                int phone = Convert.ToInt32(txt_EmpPhone.Text);
                int empID = Convert.ToInt32(txt_EmpID.Text);

                int result = bll.UpdateEmpControlling(empID, txt_EmpName.Text, txt_EmpLastN.Text,
                    cmb_EmpDepartment.Text, salary, phone);
                if (result > 0)
                {
                    GetAllEmp();
                    MessageBox.Show("Succesfully updated!");
                    txt_EmpID.Text = "";
                    txt_EmpName.Text = "";
                    txt_EmpLastN.Text = "";
                    txt_EmpPhone.Text = "";
                    cmb_EmpDepartment.Text = "";
                    txt_EmpSalary.Text = "";
                }
                else
                {
                    GetAllEmp();
                    MessageBox.Show("Not updated!");
                    txt_EmpID.Text = "";
                    txt_EmpName.Text = "";
                    txt_EmpLastN.Text = "";
                    txt_EmpPhone.Text = "";
                    cmb_EmpDepartment.Text = "";
                    txt_EmpSalary.Text = "";  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void txt_SearchEstate_Leave(object sender, EventArgs e)
        {
            if (txt_SearchEstate.Text == "")
            {
                txt_SearchEstate.Text = "Search by address";
                txt_SearchEstate.ForeColor = Color.Silver;
                GetAllEstate();
            }
        }

        private void txt_SearchEstate_Enter(object sender, EventArgs e)
        {
            if (txt_SearchEstate.Text == "Search by address")
            {
                txt_SearchEstate.Text = "";
                txt_SearchEstate.ForeColor = Color.Black;
            }
        }

        private void txt_SearchEstate_TextChanged(object sender, EventArgs e)
        {
            string AddressSearch = txt_SearchEstate.Text;
            var GetDataEmp = from t1 in entities.Tbl_Property
                             where t1.Address.Contains(AddressSearch)
                             select t1;
            if (txt_SearchEstate.Text == "")
            {
                GetAllEstate();
            }
            dgw_Estate.DataSource = GetDataEmp.ToList();
        }

        private void cmb_OrderforEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_OrderforEstate.Text == "Descending based on Area")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderByDescending(I => I.PropertyArea);

                dgw_Estate.DataSource = GetData.ToList();

            }
            else if (cmb_OrderforEstate.Text == "Ascending based on Area")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderBy(I => I.PropertyArea);

                dgw_Estate.DataSource = GetData.ToList();
            }
            else if (cmb_OrderforEstate.Text == "Descending based on Rent Price")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderByDescending(I => I.RentPrice);

                dgw_Estate.DataSource = GetData.ToList();
            }
            else if (cmb_OrderforEstate.Text == "Ascending based on Rent Price")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderBy(I => I.RentPrice);

                dgw_Estate.DataSource = GetData.ToList();
            }
            else if (cmb_OrderforEstate.Text == "Descending based on Sale Price")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderByDescending(I => I.SalePrice);

                dgw_Estate.DataSource = GetData.ToList();
            }
            else if (cmb_OrderforEstate.Text == "Ascending based on Sale Price")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderBy(I => I.SalePrice);

                dgw_Estate.DataSource = GetData.ToList();
            }
            else if (cmb_OrderforEstate.Text == "Address")
            {
                var GetData = (from t1 in entities.Tbl_Property
                               select new { t1.PropertyID, t1.CategoryID, t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired }).OrderBy(I => I.Address);

                dgw_Estate.DataSource = GetData.ToList();
            }
            else
            {
                GetAllEstate();
            }
        }

        private void cmb_EstateCateg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllWithCategory();
        }

        private void dgw_Estate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int getIndex = dgw_Estate.SelectedCells[0].RowIndex;
            GetIndexCateg = getIndex;
            txt_EstateID.Text = dgw_Estate.Rows[getIndex].Cells[0].Value.ToString();

            txt_Room.Text = dgw_Estate.Rows[getIndex].Cells[2].Value.ToString();
            txt_Area.Text = dgw_Estate.Rows[getIndex].Cells[3].Value.ToString();
            txt_RentPrice.Text = dgw_Estate.Rows[getIndex].Cells[4].Value.ToString();
            txt_SalePrice.Text = dgw_Estate.Rows[getIndex].Cells[5].Value.ToString();
            txt_Address.Text = dgw_Estate.Rows[getIndex].Cells[6].Value.ToString();
            string boolValue = dgw_Estate.Rows[getIndex].Cells[7].Value.ToString();
            bool ISrepaired = Convert.ToBoolean(boolValue);
            if (ISrepaired)
            {
                radio_Yes.Checked = true;
            }
            else
            {
                radio_No.Checked = true;

            }
            int idProperty = Convert.ToInt32(txt_EstateID.Text);
            var GetCateg = (from t1 in entities.Tbl_Property
                            join t2 in entities.Tbl_Category
                            on t1.CategoryID equals t2.CategoryID
                            where t1.PropertyID == idProperty
                            select t2.CategoryName).FirstOrDefault();

            cmb_EstateCateg.Text = GetCateg.ToString();

        }

        private void btn_resetEstate_Click(object sender, EventArgs e)
        {
            cmb_EstateCateg.Text = "";
            txt_EstateID.Text = "";
            cmb_OrderforEstate.Text = "";
            txt_Area.Text = "";
            txt_Address.Text = "";
            txt_Room.Text = "";
            txt_RentPrice.Text = "";
            txt_SalePrice.Text = "";
            GetAllEstate();
        }

        private void btn_AddEstate_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                string categName = (cmb_EstateCateg.Text).ToString();
                int GetCateg =Convert.ToInt32((from t1 in entities.Tbl_Property
                                join t2 in entities.Tbl_Category
                                on t1.CategoryID equals t2.CategoryID
                                where t2.CategoryName == categName
                                select t2.CategoryID).FirstOrDefault());
                int rooms = Convert.ToInt32(txt_Room.Text);
                double area = Convert.ToDouble(txt_Area.Text);
                double rentP = Convert.ToDouble(txt_RentPrice.Text);
                double saleP = Convert.ToDouble(txt_SalePrice.Text);
                bool isRepaired;
                if (radio_Yes.Checked)
                {
                    isRepaired = true;
                }
                else
                {
                    isRepaired = false;
                }

                int result = bll.AddEstate(GetCateg, rooms, area, rentP, saleP, txt_Address.Text, isRepaired);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully added!");
                    cmb_EstateCateg.Text = "";
                    txt_EstateID.Text = "";
                    txt_Area.Text = "";
                    txt_Address.Text = "";
                    txt_Room.Text = "";
                    txt_RentPrice.Text = "";
                    txt_SalePrice.Text = "";
                    GetAllEstate();
                }
                else
                {
                    MessageBox.Show("Not added!");
                    cmb_EstateCateg.Text = "";
                    txt_EstateID.Text = "";
                    txt_Area.Text = "";
                    txt_Address.Text = "";
                    txt_Room.Text = "";
                    txt_RentPrice.Text = "";
                    txt_SalePrice.Text = "";
                    GetAllEstate();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void btn_DeleteEstate_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int GetID = Convert.ToInt32(txt_EstateID.Text);

                int result = bll.DeleteEstateControlling(GetID);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully deleted!");
                    cmb_EstateCateg.Text = "";
                    txt_EstateID.Text = "";
                    txt_Area.Text = "";
                    txt_Address.Text = "";
                    txt_Room.Text = "";
                    txt_RentPrice.Text = "";
                    txt_SalePrice.Text = "";
                    GetAllEstate();
                }
                else
                {
                    MessageBox.Show("Not deleted!");
                    cmb_EstateCateg.Text = "";
                    txt_EstateID.Text = "";
                    txt_Area.Text = "";
                    txt_Address.Text = "";
                    txt_Room.Text = "";
                    txt_RentPrice.Text = "";
                    txt_SalePrice.Text = "";
                    GetAllEstate();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void btn_UpdateEstate_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int estateID = Convert.ToInt32(txt_EstateID.Text);
                string categName = (cmb_EstateCateg.Text).ToString();
                int GetCateg = Convert.ToInt32((from t1 in entities.Tbl_Property
                                                join t2 in entities.Tbl_Category
                                                on t1.CategoryID equals t2.CategoryID
                                                where t2.CategoryName == categName
                                                select t2.CategoryID).FirstOrDefault());
                int rooms = Convert.ToInt32(txt_Room.Text);
                double area = Convert.ToDouble(txt_Area.Text);
                double rentP = Convert.ToDouble(txt_RentPrice.Text);
                double saleP = Convert.ToDouble(txt_SalePrice.Text);
                bool isRepaired;
                if (radio_Yes.Checked)
                {
                    isRepaired = true;
                }
                else
                {
                    isRepaired = false;
                }

                int result = bll.UpdateEstateControlling(estateID,GetCateg, rooms, area, rentP, saleP, txt_Address.Text, isRepaired);
                if (result > 0)
                {
                    MessageBox.Show("Succesfully updated!");
                    cmb_EstateCateg.Text = "";
                    txt_EstateID.Text = "";
                    txt_Area.Text = "";
                    txt_Address.Text = "";
                    txt_Room.Text = "";
                    txt_RentPrice.Text = "";
                    txt_SalePrice.Text = "";
                    GetAllEstate();
                }
                else
                {
                    MessageBox.Show("Not updated!");
                    cmb_EstateCateg.Text = "";
                    txt_EstateID.Text = "";
                    txt_Area.Text = "";
                    txt_Address.Text = "";
                    txt_Room.Text = "";
                    txt_RentPrice.Text = "";
                    txt_SalePrice.Text = "";
                    GetAllEstate();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }
        //public void GetForYes()
        //{
        //    var GetByEstateCmb = from t1 in entities.Tbl_Property
        //                         join t3 in entities.Tbl_Category on t1.CategoryID equals t3.CategoryID
        //                         where t3.CategoryName == cmb_EstateCateg.Text && t1.IsRepaired == radio_Yes.Checked
        //                         select t1;
        //    dgw_Estate.DataSource = GetByEstateCmb.ToList();
        //}
        //public void GetForNo()
        //{
        //    var GetByEstateCmb = from t1 in entities.Tbl_Property
        //                         join t3 in entities.Tbl_Category on t1.CategoryID equals t3.CategoryID
        //                         where t3.CategoryName == cmb_EstateCateg.Text && t1.IsRepaired != radio_No.Checked
        //                         select t1;
        //    dgw_Estate.DataSource = GetByEstateCmb.ToList();
        //}

    }
}