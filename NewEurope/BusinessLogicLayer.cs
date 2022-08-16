using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEurope
{
    class BusinessLogicLayer
    {
        DataAccessLayer dal;
        public BusinessLogicLayer()
        {
            dal = new DataAccessLayer();
        }
        public int ControlLogin(string Username,string Password)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                return dal.LoginControl(new UserLogin { Username = Username, Password = Password });

            }
            else
            {
                return 0;
            }
        }
        public int AddCustomerControlling(string Name,string Surname,int PhoneNumber)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(PhoneNumber.ToString()))
            {
                return dal.AddCustomer(new NewUser {
                    NewName=Name,
                    NewSurname=Surname,
                    NewPhone=PhoneNumber
                });
            }
            else
            {
                return 0;
            }
        }
        public int AddUserControlling(string Username, string Password,int ID)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ID.ToString()))
            {
                return dal.AddUser(new NewUser
                {
                    NewUsername = Username,
                    NewPassword = Password,
                    NewID = ID
                });

            }
            else
            {
                return 0;
            }
        }
        public int AddOperation(int PropertyID,double Cost,int CustomerID,string OperationType,DateTime OperationDate)
        {
            if (!string.IsNullOrEmpty((PropertyID).ToString()) && !string.IsNullOrEmpty(Cost.ToString()) 
                && !string.IsNullOrEmpty(CustomerID.ToString()) && !string.IsNullOrEmpty(OperationType)
                && !string.IsNullOrEmpty(OperationDate.ToString()))
            {
                return dal.AddOperation(new NewOperation
                {
                    PropertyID=PropertyID,
                    Cost=Cost,
                    CustomerID=CustomerID,
                    OperationType=OperationType,
                    dateNow=OperationDate
                });
            }
            else
            {
                return 0;
            }
        }
        public int AddCategControlling(string CategoryName)
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                return dal.AddCateg(new CategoryClass
                {
                    CategName = CategoryName,
                });

            }
            else
            {
                return 0;
            }
        }
        public int UpdateCategControlling(int CategoryId, string CategoryName)
        {
            if ((!string.IsNullOrEmpty(CategoryId.ToString()) && !string.IsNullOrEmpty(CategoryName)))
            {
                return dal.UpdateCateg(new CategoryClass
                {
                    CategId = CategoryId,
                    CategName = CategoryName,
                });

            }
            else
            {
                return 0;
            }
        }
        public int DeleteCategControlling(int CategoryId)
        {
            if (!string.IsNullOrEmpty(CategoryId.ToString()))
            {
                return dal.DeleteCateg(new CategoryClass
                {
                    CategId = CategoryId,
                });

            }
            else
            {
                return 0;
            }
        }
        public int AddEmployee(string EmployeeName, string EmployeeSurname,
            string DepartName,int Salary,int Phone)
        {
            if (!string.IsNullOrEmpty(EmployeeName)
                && !string.IsNullOrEmpty(EmployeeSurname) && !string.IsNullOrEmpty(DepartName)
                && !string.IsNullOrEmpty(Salary.ToString()) && !string.IsNullOrEmpty(Phone.ToString()))
            {
                return dal.AddEmp(new EmployeeInfo
                {
                    EmpName = EmployeeName,
                    EmpSurname = EmployeeSurname,
                    DepName = DepartName,
                    Salary = Salary,
                    Phone=Phone
                });
            }
            else
            {
                return 0;
            }
        }
        public int DeleteEmpControlling(int EmployeeID)
        {
            if (!string.IsNullOrEmpty(EmployeeID.ToString()))
            {
                return dal.DeleteEmp(new EmployeeInfo
                {
                    EmployeeID = EmployeeID,
                });

            }
            else
            {
                return 0;
            }
        }
        public int UpdateEmpControlling(int EmployeeID, string EmployeeName, string EmployeeSurname,
            string DepartName, int Salary, int Phone)
        {
            if (!string.IsNullOrEmpty((EmployeeID).ToString()) && !string.IsNullOrEmpty(EmployeeName)
                && !string.IsNullOrEmpty(EmployeeSurname) && !string.IsNullOrEmpty(DepartName)
                && !string.IsNullOrEmpty(Salary.ToString()) && !string.IsNullOrEmpty(Phone.ToString()))
            {
                return dal.UpdateEmp(new EmployeeInfo
                {
                    EmployeeID = EmployeeID,
                    EmpName = EmployeeName,
                    EmpSurname = EmployeeSurname,
                    DepName = DepartName,
                    Salary = Salary,
                    Phone = Phone
                });

            }
            else
            {
                return 0;
            }
        }
        public int AddEstate(int CategoryID,int Rooms,double Area, double RentP, double SaleP, string Address,
            bool isRepaired)
        {
            if (!string.IsNullOrEmpty(CategoryID.ToString()) && !string.IsNullOrEmpty(Rooms.ToString())
                && !string.IsNullOrEmpty(Area.ToString()) && !string.IsNullOrEmpty(RentP.ToString())
                && !string.IsNullOrEmpty(SaleP.ToString())
                && !string.IsNullOrEmpty(Address)
                && !string.IsNullOrEmpty(isRepaired.ToString()))
            {
                return dal.AddEstate(new EstateInfo
                {
                    CategoryID=CategoryID,
                    Rooms=Rooms,
                    Area=Area,
                    RentPrice=RentP,
                    SalePrice=SaleP,
                    Address=Address,
                    IsRepaired=isRepaired
                });
            }
            else
            {
                return 0;
            }
        }
        public int DeleteEstateControlling(int EstateID)
        {
            if (!string.IsNullOrEmpty(EstateID.ToString()))
            {
                return dal.DeleteEstate(new EstateInfo
                {
                    PropertyID = EstateID,
                });

            }
            else
            {
                return 0;
            }
        }
        public int UpdateEstateControlling(int EstateID,int CategoryID, int Rooms, double Area, double RentP, double SaleP, string Address,
            bool isRepaired)
        {
            if (!string.IsNullOrEmpty(EstateID.ToString())
                && !string.IsNullOrEmpty(CategoryID.ToString()) 
                && !string.IsNullOrEmpty(Rooms.ToString())
                && !string.IsNullOrEmpty(Area.ToString()) 
                && !string.IsNullOrEmpty(RentP.ToString())
                && !string.IsNullOrEmpty(SaleP.ToString())
                && !string.IsNullOrEmpty(Address)
                && !string.IsNullOrEmpty(isRepaired.ToString()))
            {
                return dal.UpdateEstate(new EstateInfo
                {
                    PropertyID=EstateID,
                    CategoryID = CategoryID,
                    Rooms = Rooms,
                    Area = Area,
                    RentPrice = RentP,
                    SalePrice = SaleP,
                    Address = Address,
                    IsRepaired = isRepaired
                });
            }
            else
            {
                return 0;
            }
        }

    }

}
