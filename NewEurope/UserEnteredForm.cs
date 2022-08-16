using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEurope
{
    public partial class UserEnteredForm : Form
    {
        NewEuropeDBEntities entities = new NewEuropeDBEntities();
        public string  CustName { get; set; }
        public void GetAll()
        {
            dgw_forCustomer.Columns[6].Visible = false;
            var GetDataAll= (from t1 in entities.Tbl_Property join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                         where t2.OperationType!="Sale"
                         select new { t1.RoomNumbers, t1.PropertyArea,t1.RentPrice,t1.SalePrice,t1.Address,t1.IsRepaired,t1.PropertyID }).Distinct();

            dgw_forCustomer.DataSource = GetDataAll.ToList();     
        }
        public void GetAllWithCategory()
        {
            dgw_forCustomer.Columns[6].Visible = false;
            var GetData = (from t1 in entities.Tbl_Property
                          join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                          join t3 in entities.Tbl_Category on t1.CategoryID equals t3.CategoryID
                          where t2.OperationType != "Sale" && t3.CategoryName == cmb_chooseEstate.Text
                          select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct();

            dgw_forCustomer.DataSource = GetData.ToList();
        }

        public UserEnteredForm()
        {
            InitializeComponent();
            GetAll();
            wb_Map.ScriptErrorsSuppressed = true;
            int House = getRate("Rent", 1)+ getRate("Sale", 1);
            int Apartment = getRate("Rent", 2) + getRate("Sale", 2);
            int Country = getRate("Rent", 3) + getRate("Sale", 3);
            int Office = getRate("Rent", 4) + getRate("Sale", 4);
            int Garage = getRate("Rent", 5) + getRate("Sale", 5);
            int Land = getRate("Rent", 6) + getRate("Sale", 6);
            int Object = getRate("Rent", 7) + getRate("Sale", 7);
            chart_Total.Series["Categories"].IsValueShownAsLabel = true;
            chart_Total.Series["Categories"].Points.AddXY("House", $"{House}");
            chart_Total.Series["Categories"].Points.AddXY("Apartment", $"{Apartment}");
            chart_Total.Series["Categories"].Points.AddXY("Country House", $"{Country}");
            chart_Total.Series["Categories"].Points.AddXY("Office", $"{Office}");
            chart_Total.Series["Categories"].Points.AddXY("Garage", $"{Garage}");
            chart_Total.Series["Categories"].Points.AddXY("Land", $"{Land}");
            chart_Total.Series["Categories"].Points.AddXY("Object", $"{Object}");
          
        }
        private void UserEnteredForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'newEuropeDBDataSet2.Tbl_Category' table. You can move, or remove it, as needed.
            this.tbl_CategoryTableAdapter1.Fill(this.newEuropeDBDataSet2.Tbl_Category);
            // TODO: This line of code loads data into the 'newEuropeDBDataSet6.Tbl_Property' table. You can move, or remove it, as needed.
            this.tbl_PropertyTableAdapter.Fill(this.newEuropeDBDataSet6.Tbl_Property);
            // TODO: This line of code loads data into the 'newEuropeDBDataSet3.Tbl_Category' table. You can move, or remove it, as needed.
            this.tbl_CategoryTableAdapter.Fill(this.newEuropeDBDataSet3.Tbl_Category);
            // TODO: This line of code loads data into the 'newEuropeDBDataSet.Tbl_Operation' table. You can move, or remove it, as needed.
            this.tbl_OperationTableAdapter.Fill(this.newEuropeDBDataSet.Tbl_Operation);
            style_dgw_history();
            style_dgw_forCUstomer();
            cmb_chooseEstate.Text = "";
            cmb_chooseRoom.Text = "";
            cmb_serviceType.Text="";
            GetAll();
        }
        void style_dgw_history()
        {
            dgw_History.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgw_History.DefaultCellStyle.SelectionBackColor = Color.Chocolate;
        }
        void style_dgw_forCUstomer()
        {
            dgw_forCustomer.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgw_forCustomer.DefaultCellStyle.SelectionBackColor = Color.Chocolate;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void btn_Estates_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(570, 3);

            pnl_about_us.Show();
            pnl_myHistory.Hide();
            StringBuilder builder = new StringBuilder();
            builder.Append("https://www.google.com/maps/place/NEW+EUROPE+REAL+ESTATE/@40.3805361,49.827476,17z/data=!4m5!3m4!1s0x40307d9790ddd021:0xd2ab87ccbc9e4bb3!8m2!3d40.3805618!4d49.827469");
            wb_Map.Navigate(builder.ToString());
            pnl_services.Hide();
            pnl_Rate.Hide();
        }

        private void btn_Categories_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(290, 3);

            var GetCustId = (from db in entities.Tbl_Users where db.Username == CustName select db.CustomerID).FirstOrDefault();
            var GetName = (from db in entities.Tbl_Customer where db.CustomerID == GetCustId select db.CustomerName).FirstOrDefault();
            lbl_NameGained.Text = "Dear "+GetName.ToString()+"!";
            var query = (from db in entities.Tbl_Operation where db.CustomerID == GetCustId select db);
            dgw_History.DataSource = query.ToList();

            pnl_myHistory.Show();
            pnl_about_us.Hide();
            pnl_Rate.Hide();
            pnl_services.Hide();
        }

        private void btn_Employees_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(17, 3);

            GetAll();
            pnl_services.Show();
            pnl_myHistory.Hide();
            pnl_Rate.Hide();
            pnl_about_us.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnl_line.Location = new Point(820, 3);
            
            int l1 = getRate("Rent", 1);
            lbl_rentHouse.Text = l1.ToString();

            int l2 = getRate("Sale", 1);
            lbl_soldHouse.Text = l2.ToString();

            int l3 = getRate("Rent", 2);
            lbl_rentApart.Text = l3.ToString();

            int l4 = getRate("Sale", 2);
            lbl_soldApart.Text = l4.ToString();

            int l5 = getRate("Rent", 3);
            lbl_rentCountry.Text = l5.ToString();

            int l6 = getRate("Sale", 3);
            lbl_soldCountry.Text = l6.ToString();

            int l7 = getRate("Rent", 4);
            lbl_rentOffice.Text = l7.ToString();

            int l8 = getRate("Sale", 4);
            lbl_soldOffice.Text = l8.ToString();

            int l9 = getRate("Rent", 5);
            lbl_rentGarage.Text = l9.ToString();

            int l10 = getRate("Sale", 5);
            lbl_soldGarage.Text = l10.ToString();

            int l11 = getRate("Rent", 6);
            lbl_rentLand.Text = l11.ToString();

            int l12 = getRate("Sale", 6);
            lbl_soldLand.Text = l12.ToString();

            int l13 = getRate("Rent", 7);
            lbl_rentObject.Text = l13.ToString();

            int l14 = getRate("Sale", 7);
            lbl_soldObject.Text = l14.ToString();


            pnl_Rate.Show();
            pnl_about_us.Hide();
            pnl_myHistory.Hide();
            pnl_services.Hide();


        }
        public int getRate(string Type,int CategID)
        {
            int query =Convert.ToInt32((from t1 in entities.Tbl_Operation
                          join t2 in entities.Tbl_Property
                          on t1.PropertyID equals t2.PropertyID
                          join t3 in entities.Tbl_Category
                          on t2.CategoryID equals t3.CategoryID
                          where t1.OperationType == Type && t3.CategoryID == CategID
                          select t1).Count());
            return query;
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            GetAll();
            cmb_chooseEstate.Text = "";
            txt_Address.Text = "";
            cmb_chooseRoom.Text = "";
            cmb_EmpOrder.Text = "";
            cmb_serviceType.Text = "";
            txt_cost.Text = "";

        }

        private void pnl_services_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgw_forCustomer_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgw_forCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int INdex;
        private void dgw_forCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                int getIndex = dgw_forCustomer.SelectedCells[0].RowIndex;
                INdex = getIndex;
                cmb_chooseRoom.Text = dgw_forCustomer.Rows[getIndex].Cells[0].Value.ToString();
                string boolValue = dgw_forCustomer.Rows[getIndex].Cells[5].Value.ToString();

                bool ISrepaired = Convert.ToBoolean(boolValue);
                txt_Address.Text = dgw_forCustomer.Rows[getIndex].Cells[4].Value.ToString();
               
                int idProperty = Convert.ToInt32(dgw_forCustomer.Rows[getIndex].Cells[6].Value.ToString());
                int queryC = Convert.ToInt32((from db in entities.Tbl_Property
                                              where db.PropertyID == idProperty
                                              select db.CategoryID).FirstOrDefault());
                if (ISrepaired)
                {
                    radio_Yes.Checked = true;
                }
                else
                {
                    radio_No.Checked = true;
                }
            var GetCateg = (from t1 in entities.Tbl_Property join t2 in entities.Tbl_Category
                           on t1.CategoryID equals t2.CategoryID
                            where t1.PropertyID == idProperty
                            select t2.CategoryName).FirstOrDefault();
            cmb_chooseEstate.Text = GetCateg.ToString();
        }

        private void cmb_chooseEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllWithCategory();

        }
        //public void GetForYes()
        //{
        //    dgw_forCustomer.Columns[6].Visible = false;
        //    int GetID = Convert.ToInt32((from db in entities.Tbl_Operation
        //                                 where db.OperationType == "Sale"
        //                                 select db.PropertyID).FirstOrDefault());
        //    var GetByEstateCmb = from t1 in entities.Tbl_Property
        //                         join t3 in entities.Tbl_Category on t1.CategoryID equals t3.CategoryID
        //                         where t3.CategoryName == cmb_chooseEstate.Text &&  t1.IsRepaired==radio_Yes.Checked
        //                         select t1;
        //    dgw_forCustomer.DataSource = GetByEstateCmb.ToList();
        //}
        //public void GetForNo()
        //{
        //    dgw_forCustomer.Columns[6].Visible = false;
        //    int GetID = Convert.ToInt32((from db in entities.Tbl_Operation
        //                                 where db.OperationType == "Sale"
        //                                 select db.PropertyID).FirstOrDefault());
        //    var GetByEstateCmb = from t1 in entities.Tbl_Property
        //                         join t3 in entities.Tbl_Category on t1.CategoryID equals t3.CategoryID
        //                         where t3.CategoryName == cmb_chooseEstate.Text &&  t1.IsRepaired != radio_No.Checked
        //                         select t1;
        //    dgw_forCustomer.DataSource = GetByEstateCmb.ToList();
        //}
   

        private void radio_Yes_CheckedChanged(object sender, EventArgs e)
        {
            //GetForYes();

        }

        private void radio_No_CheckedChanged(object sender, EventArgs e)
        {
            //GetForNo();
        }

        private void cmb_chooseRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void txt_Search_Enter(object sender, EventArgs e)
        {
            if (txt_Search.Text == "Search for address")
            {
                txt_Search.Text = "";
                txt_Search.ForeColor = Color.Black;
            }
            GetAll();
        }

        private void txt_Search_Leave(object sender, EventArgs e)
        {
            if (txt_Search.Text == "")
            {
                txt_Search.Text = "Search for address";
                txt_Search.ForeColor = Color.Silver;

            }
            GetAll();
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string AddressSearch =txt_Search.Text;
            dgw_forCustomer.Columns[6].Visible = false;
            var GetData = (from t1 in entities.Tbl_Property
                          join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                          where t2.OperationType != "Sale" && t1.Address.Contains(AddressSearch)
                          select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct();
            dgw_forCustomer.DataSource = GetData.ToList();

        }

        private void cmb_EmpOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_EmpOrder.Text== "Descending based on Area")
            {
                dgw_forCustomer.Columns[6].Visible = false;
                var GetData = (from t1 in entities.Tbl_Property
                               join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                               where t2.OperationType != "Sale"
                               select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct().OrderByDescending(I => I.PropertyArea);


                dgw_forCustomer.DataSource = GetData.ToList();

            }
            else if (cmb_EmpOrder.Text == "Ascending based on Area")
            {
                dgw_forCustomer.Columns[6].Visible = false;
                var GetData = (from t1 in entities.Tbl_Property
                              join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                              where t2.OperationType != "Sale"
                              select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct().OrderBy(I => I.PropertyArea);

                dgw_forCustomer.DataSource = GetData.ToList();
            }
            else if (cmb_EmpOrder.Text== "Descending based on Rent Price")
            {
                dgw_forCustomer.Columns[6].Visible = false;
                var GetData = (from t1 in entities.Tbl_Property
                              join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                              where t2.OperationType != "Sale"
                              select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct().OrderByDescending(I => I.RentPrice);

                dgw_forCustomer.DataSource = GetData.ToList();
            }
            else if (cmb_EmpOrder.Text == "Ascending based on Rent Price")
            {
                dgw_forCustomer.Columns[6].Visible = false;
                var GetData = (from t1 in entities.Tbl_Property
                              join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                              where t2.OperationType != "Sale"
                              select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct().OrderBy(I => I.RentPrice);

                dgw_forCustomer.DataSource = GetData.ToList();
            }
            else if (cmb_EmpOrder.Text == "Descending based on Sale Price")
            {
                dgw_forCustomer.Columns[6].Visible = false;
                var GetData = (from t1 in entities.Tbl_Property
                              join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                              where t2.OperationType != "Sale"
                              select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct().OrderByDescending(I => I.SalePrice);

                dgw_forCustomer.DataSource = GetData.ToList();
            }
            else if (cmb_EmpOrder.Text == "Ascending based on Sale Price")
            {
                dgw_forCustomer.Columns[6].Visible = false;
                var GetData = (from t1 in entities.Tbl_Property
                              join t2 in entities.Tbl_Operation on t1.PropertyID equals t2.PropertyID
                              where t2.OperationType != "Sale"
                              select new { t1.RoomNumbers, t1.PropertyArea, t1.RentPrice, t1.SalePrice, t1.Address, t1.IsRepaired, t1.PropertyID }).Distinct().OrderBy(I => I.SalePrice);

                dgw_forCustomer.DataSource = GetData.ToList();
            }
            else
            {
                GetAll();
            }
        }

        private void btn_choose_Click(object sender, EventArgs e)
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            try
            {
                int roomNumbers = Convert.ToInt32(cmb_chooseRoom.Text);

                int GetID = Convert.ToInt32(dgw_forCustomer.Rows[INdex].Cells[6].Value);
                int GetCustId = Convert.ToInt32((from db in entities.Tbl_Users where db.Username == CustName select db.CustomerID).FirstOrDefault());
                DateTime dateTime = new DateTime();
                double GetCost = Convert.ToDouble(txt_cost.Text);

                int result = bll.AddOperation(GetID, GetCost, GetCustId, cmb_serviceType.Text, dateTime);
                int phoneQuery = Convert.ToInt32((from db in entities.Tbl_Customer
                                                  where db.CustomerID == GetCustId
                                                  select db.CustomerPhone).FirstOrDefault()); 
                MessageBox.Show($"Succesfully done!\nWe will call you via {phoneQuery} ...");
                txt_Address.Text = "";
                txt_cost.Text = "";
                cmb_chooseEstate.Text = "";
                cmb_chooseRoom.Text = "";
                cmb_serviceType.Text = "";
                GetAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Please fill correctly!");
            }
        }

        private void txt_Address_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_serviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idProperty = Convert.ToInt32(dgw_forCustomer.Rows[INdex].Cells[6].Value.ToString());
                if (cmb_serviceType.Text == "Sale")
                {
                    txt_cost.Text = (from db in entities.Tbl_Property
                                     where db.PropertyID == idProperty
                                     select db.SalePrice).FirstOrDefault().ToString();
                }
                else if (cmb_serviceType.Text == "Rent")
                {
                    txt_cost.Text = (from db in entities.Tbl_Property
                                     where db.PropertyID == idProperty
                                     select db.RentPrice).FirstOrDefault().ToString();
                }
                else
                {
                    txt_cost.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
    }
}
