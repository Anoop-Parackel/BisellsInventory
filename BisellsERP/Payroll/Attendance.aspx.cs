using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Payroll
{
    public partial class Attendance : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            var Employees = Entities.Payroll.Employee.GetDetails(CPublic.GetCompanyID()).Where(x=>x.Status!=0).ToList();
            for (int i = 0; i < Employees.Count; i++)
            {
                ltrEmpList.Text += @"<tr><td><div class='checkbox mark-employee'> <input type='checkbox'/> <label for=''></label> </div></td><td data-empid='"+Employees[i].ID+"'>"+Employees[i].FirstName+"&nbsp;"+Employees[i].LastName+"</td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td><td class='text-center attendance-box'> <span class='attendance' data-attendance='0'>-</span> </td></tr>";
            }
        }
    }
}