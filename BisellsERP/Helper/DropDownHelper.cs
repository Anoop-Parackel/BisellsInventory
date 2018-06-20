using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BisellsERP.Helper;
using System.Web.UI.WebControls;
using Entities;
using System.Data;
using Entities.Master;
using Entities.Payroll;
using Entities.Application;
using System.ComponentModel.Design;
using Entities.Finance;

namespace BisellsERP.Helper
{
    /// <summary>
    /// Extension Methods for using with System.Web.UI.WebControls.DropDownList
    /// </summary>
    public static class DropDownHelper
    {

        public static void LoadUnits(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = UOM.GetUnits(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["short_name"].ToString(), item["Unit_ID"].ToString()));
                }
            }
        }

        public static void LoadJobs(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Job.GetJobs(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Job_Name"].ToString(), item["Job_Id"].ToString()));
                }
            }
        }

        public static void LoadVehicles(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Vehicle.GetVehicle(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Vehicle_Id"].ToString()));
                }
            }
        }
        public static void LoadBrands(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Brand.GetBrand(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["name"].ToString(), item["brand_ID"].ToString()));
                }
            }
        }
        public static void LoadCategories(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Category.GetCategory(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["name"].ToString(), item["category_ID"].ToString()));
                }
            }
        }
        public static void LoadAccountHeads(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = AccountHeadMaster.GetAccountHeads(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["name"].ToString(), item["id"].ToString()));
                }
            }
        }
        public static void LoadTaxes(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Tax.GetTax(CompanyId);
            ddl.Items.Clear();

            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Percentage"].ToString(), item["tax_ID"].ToString()));
                }
            }
        }
        public static void LoadGroups(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Group.GetGroup(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Group_ID"].ToString()));
                }
            }
        }
        public static void LoadLocations(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Location.GetLocation(CompanyId);
            ddl.Items.Clear();
            if (dt != null)
            {
                if (dt.Rows.Count > 1)
                {

                    ddl.Items.Add(new ListItem("--select--", "0"));
                }
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Location_Id"].ToString()));
                }
            }
        }
        public static void LoadTypes(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = ProductType.GetProductType(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Type_Id"].ToString()));
                }
            }
        }
        public static void LoadCountry(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Country.GetCountry(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Country_Id"].ToString()));
                }
            }
        }
        public static void LoadCurrency(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Currency.GetCurrency(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Code"].ToString(), item["Currency_Id"].ToString()));
                }
            }
        }
        public static void LoadCustomer(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Customer.GetCustomer(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Customer_Id"].ToString()));
                }
            }
        }
        public static void LoadState(this DropDownList ddl, int CompanyId, int CountryId)
        {
            DataTable dt = State.GetState(CompanyId, CountryId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["State_Id"].ToString()));
                }
            }
        }
        public static void LoadSupplier(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Supplier.GetSupplier(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Supplier_Id"].ToString()));
                }
            }
        }
        public static void LoadDepartments(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Department.GetDepartment(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Department"].ToString(), item["Department_Id"].ToString()));
                }
            }
        }
        public static void LoadCompany(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Company.GetCompany(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Company_Id"].ToString()));
                }
            }
        }
        public static void LoadDesignation(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Designation.GetDesignation(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Designation"].ToString(), item["Designation_Id"].ToString()));
                }
            }
        }
        public static void LoadDespatch(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Despatch.GetDespatch(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Despatch_Id"].ToString()));
                }
            }
        }
        public static void LoadEmployee(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = Employee.GetEmployee(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["First_Name"].ToString(), item["Employee_Id"].ToString()));
                }
            }
        }
        public static void LoadUserGroups(this DropDownList ddl)
        {
            DataTable dt = UserGroup.GetUserGroup();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Group_Name"].ToString(), item["User_Group_Id"].ToString()));
                }
            }
        }
        public static void LoadUsers(this DropDownList ddl,int companyId)
        {
            DataTable dt = User.GetUserForRole(companyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["User_Name"].ToString(), item["User_Id"].ToString()));
                }
            }
        }
        public static void LoadUsers(this DropDownList ddl)
        {
            DataTable dt = User.GetUser();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["User_Name"].ToString(), item["User_Id"].ToString()));
                }
            }
        }
        public static void LoadBillNo(this DropDownList ddl, int LocationId)
        {
            DataTable dt = Entities.Register.SalesEntryRegister.GetRequestNo(LocationId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Bill_No"].ToString(), item["Sale_Entry_Id"].ToString()));
                }
            }
        }
        public static void LoadViews(this DropDownList ddl)
        {
            EnumerableRowCollection<DataRow> FilteredDt = Entities.Reporting.ReportingTool.GetViews().AsEnumerable().Where(x => x.Field<string>("Name").ToUpper().Contains("REPORT"));
            DataTable dt = FilteredDt != null ? FilteredDt.CopyToDataTable() : null;
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Name"].ToString(), item["Object_Id"].ToString()));
                }
            }
        }
        public static void LoadReports(this DropDownList ddl)
        {
            DataTable dt = Entities.Reporting.ReportingTool.GetReports();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Report_name"].ToString(), item["report_id"].ToString()));
                }
            }
        }
        public static void LoadAccountGroups(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = AccountGroup.GetAccountGroups(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["name"].ToString(), item["id"].ToString()));
                }
            }
        }
        public static void LoadVoucherTypes(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = VoucherType.GetVoucherTypes(CompanyId);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["name"].ToString(), item["id"].ToString()));
                }
            }
        }
        public static void LoadCost(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = CostCenter.LoadCostCenter();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["name"].ToString(), item["id"].ToString()));
                }
            }
        }
        public static void LoadVoucherTypesForEntry(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = VoucherEntry.getVoucherTypesForEntry();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Fvt_TypeName"].ToString(), item["Fvt_ID"].ToString()));
                }
            }
        }
        public static void LoadVoucherTypesFinancial(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = FinancialTransactions.LoadVoucherTypesForFin();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("All", "0"));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(item["Fvt_TypeName"].ToString(), item["Fvt_ID"].ToString()));
                }
            }
        }
        public static void LoadAccountHeadsVoucher(this DropDownList ddl, int CompanyId)
        {
            DataTable dt = VoucherEntry.GetAccountHeadsVoucher();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", ""));
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ListItem items = new ListItem(item["name"].ToString(), item["parent"].ToString() + "|" + item["ID"].ToString());
                    ddl.Items.Add(items);

                }
            }
        }

        //public static void LoadHeadsVoucher(this DropDownList ddl, int CompanyId,int Credit,int VoucherType)
        //{
        //    DataTable dt = AccountHeadMaster.GetAccountHeadsVoucher(VoucherType,Credit,CompanyId);
        //    ddl.Items.Clear();
        //    ddl.Items.Add(new ListItem("--Select--", ""));
        //    if (dt != null)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            ListItem items = new ListItem(item["name"].ToString(), item["parent"].ToString() + "|" + item["ID"].ToString());
        //            ddl.Items.Add(items);

        //        }
        //    }
        //}
    }
}