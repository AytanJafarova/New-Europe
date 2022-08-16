using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NewEurope
{
    class DataAccessLayer:DbSet
    {
        NewEuropeDBEntities entities = new NewEuropeDBEntities();
        public int LoginControl(UserLogin login)
        {
            try
            {
                var LoginControlling = from db in entities.Tbl_Users
                                       where db.Username == login.Username && db.Password == login.Password
                                       select db;
                return LoginControlling.Count();
                
              
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int AddCustomer(NewUser newUser)
        {
            try
            {
                Tbl_Customer customer = new Tbl_Customer();
                customer.CustomerName = newUser.NewName;
                customer.CustomerSurname = newUser.NewSurname;
                customer.CustomerPhone = newUser.NewPhone;
                var ControlCustomers = entities.Tbl_Customer.Add(customer);
                entities.SaveChanges();
                int result = Convert.ToInt32(ControlCustomers);

                return result;

            }
            catch (Exception)
            {
                return 0;
            }

        }
        public int AddUser(NewUser newUser)
        {
            try
            {
                Tbl_Users users = new Tbl_Users();
                users.Username = newUser.NewUsername;
                users.Password = newUser.NewPassword;
                users.CustomerID = newUser.NewID;
                var ControlUsers = entities.Tbl_Users.Add(users);
                entities.SaveChanges();

                int result = Convert.ToInt32(ControlUsers);

                return result;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int AddOperation(NewOperation operation)
        {
            try
            {
                Tbl_Operation _Operation = new Tbl_Operation();
                _Operation.PropertyID = operation.PropertyID;
                _Operation.Cost = operation.Cost;
                _Operation.CustomerID = operation.CustomerID;
                _Operation.OperationType = operation.OperationType;
                _Operation.OperationDate = operation.dateNow;
                var ControlOperation = entities.Tbl_Operation.Add(_Operation);
                entities.SaveChanges();
                int result = Convert.ToInt32(ControlOperation);
                return result;
            }
            catch (Exception)
            {

                return 0;
               
            }
        }
        public int AddCateg(CategoryClass categoryClass)
        {
            try
            {
                Tbl_Category tbl_Category = new Tbl_Category();
                tbl_Category.CategoryName = categoryClass.CategName;
                entities.Tbl_Category.Add(tbl_Category);
                int result = entities.SaveChanges();
                return result;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int SelectedCategID { get; set; }
        public int UpdateCateg(CategoryClass categoryClass)
        {
            try
            {
                Tbl_Category category = (from db in entities.Tbl_Category
                                         where db.CategoryID == categoryClass.CategId
                                         select db).FirstOrDefault();
                category.CategoryName = categoryClass.CategName;
                int result = entities.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int DeleteCateg(CategoryClass categoryClass)
        {
            try
            {
                Tbl_Category category = (from db in entities.Tbl_Category
                                         where db.CategoryID == categoryClass.CategId
                                         select db).FirstOrDefault();
                entities.Tbl_Category.Remove(category);
                int result = entities.SaveChanges();
                return result;
        
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int AddEmp(EmployeeInfo emp)
        {
            try
            {
                Tbl_Employee employee = new Tbl_Employee();
                employee.EmployeeName = emp.EmpName;
                employee.EmployeeSurname = emp.EmpSurname;
                employee.DepartmentName = emp.DepName;
                employee.Salary = emp.Salary;
                employee.Phone = emp.Phone;
                entities.Tbl_Employee.Add(employee);
                int result = entities.SaveChanges();
                return result;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int DeleteEmp(EmployeeInfo emp)
        {
            try
            {
                Tbl_Employee employee = (from db in entities.Tbl_Employee
                                         where db.EmployeeID == emp.EmployeeID
                                         select db).FirstOrDefault();
 
                entities.Tbl_Employee.Remove(employee);
                int result = entities.SaveChanges();
                return result;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int UpdateEmp(EmployeeInfo emp)
        {
            try
            {
                Tbl_Employee employee = (from db in entities.Tbl_Employee
                                         where db.EmployeeID == emp.EmployeeID
                                         select db).FirstOrDefault();
                employee.EmployeeName = emp.EmpName;
                employee.EmployeeSurname = emp.EmpSurname;
                employee.DepartmentName = emp.DepName;
                employee.Phone = emp.Phone;
                employee.Salary = emp.Salary;
                int result = entities.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int AddEstate(EstateInfo estate)
        {
            try
            {
                Tbl_Property property = new Tbl_Property();
                property.CategoryID = estate.CategoryID;
                property.RoomNumbers = estate.Rooms;
                property.PropertyArea = estate.Area;
                property.RentPrice = estate.RentPrice;
                property.SalePrice = estate.SalePrice;
                property.Address = estate.Address;
                property.IsRepaired = estate.IsRepaired;
                entities.Tbl_Property.Add(property);
                int result = entities.SaveChanges();
                return result;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int DeleteEstate(EstateInfo estate)
        {
            try
            {
                Tbl_Property property = (from db in entities.Tbl_Property
                                         where db.PropertyID == estate.PropertyID
                                         select db).FirstOrDefault();

                entities.Tbl_Property.Remove(property);
                int result = entities.SaveChanges();
                return result;

            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int UpdateEstate(EstateInfo estate)
        {
            try
            {
                Tbl_Property property = (from db in entities.Tbl_Property
                                         where db.PropertyID == estate.PropertyID
                                         select db).FirstOrDefault();
                property.CategoryID = estate.CategoryID;
                property.RoomNumbers = estate.Rooms;
                property.PropertyArea = estate.Area;
                property.RentPrice = estate.RentPrice;
                property.SalePrice = estate.SalePrice;
                property.Address = estate.Address;
                property.IsRepaired = estate.IsRepaired;
                int result = entities.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
