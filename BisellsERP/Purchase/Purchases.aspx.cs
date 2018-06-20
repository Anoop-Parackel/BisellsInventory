﻿using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase
{
    public partial class Purchases : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            int companyId = Publics.CPublic.GetCompanyID();
            ddlSupplier.LoadSupplier(companyId);
            DataTable dt = new DataTable();
            Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
            dt = fin.GetAccountHeads(45);//The Parent group of Bank group is 45.
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ListItem items = new ListItem(item["Fah_Name"].ToString(), item["Fah_ID"].ToString());
                    ddlPaymentBank.Items.Add(items);
                }
            }
        }
    }
}