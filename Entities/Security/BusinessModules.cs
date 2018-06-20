using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Security
{
    public enum BusinessModules
    {
        PurchaseRequest = 2,
        PurchaseQuote = 4,
        PurchaseQuoteBuilder = 3,
        PurchaseEntry = 35,
        PurchaseReturn = 36,
        SalesRequest = 6,
        SalesEntry = 8,
        SalesReturn = 37,
        Brand = 17,
        Category = 18,
        Customer = 22,
        Group = 24,
        Item = 25,
        Location = 26,
        Suppliers = 28,
        Tax = 29,
        Type = 30,
        Unit = 31,
        Vehicle = 34,
        Department = 13,
        Designation = 12,
        Employee = 14,
        SalaryComponents = 15,
        SalesQuote = 7,
        Scheme = 40,
        Despatch=42,
        VoucherType=53,
        TransferOut=56,
        RegisterOut=59,   
        CostCenter=54,
        TransferIn=58,
        RegisterIn=60,
        AccountGroup=61,
        AccountHeadMaster=62,
        Admin=64,
        ReportSettings=65,
        VoucherSettings=67,
        VoucherEntry=69,
        OpeningBalance=73,
        FinancialTransactions=74,
        VoucherEdit=75,
        Damage=76,
        HourlyTemplate=79,
        PayRollTemplate=80,
        OfficeShift=81,
        StockAdjustment=85,
        Preferences=88,
        company= 19,
        AdvanceSalary=108,
        Jobs = 112,
        Leads=136,
        SalesEstimate=128,
        SalesDeliveryNote=130,
            SalesCreditNote=132,
            GRN= 109,
            PurchaseDebitNote=134,
            PurchaseIndent=107
    }

}
