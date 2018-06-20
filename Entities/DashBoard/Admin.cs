using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;
using System.Data;

namespace Entities.DashBoard
{
    public class Admin
    {
        public static dynamic SalesVsPurchase(DateTime? From, DateTime? To)
        {           
            DBManager db = new DBManager();
            try
            {
                if (From == null || To == null)
                {
                    To = Application.Localisation.GetLocalDate(DateTime.UtcNow);
                    From = To.Value.AddDays(-6);

                }
                string query = @"select CONVERT(varchar(20),A.Date,100)[Date],isnull(sum(Net_Amount),0) [Total] from TBL_CALENDER A left join  TBL_SALES_ENTRY_REGISTER B
                                ON CONVERT(DATE,A.Date) = CONVERT(DATE,B.Sales_Date)  where A.Date>='" + From.Value.ToString("yyyy/MM/dd") + "' and A.Date<='" + To.Value.ToString("yyyy/MM/dd") + "' group by A.Date order by A.Date;select CONVERT(varchar(20),A.Date,100)[Date],isnull(sum(Net_Amount),0) [Total] from TBL_CALENDER A left join  TBL_PURCHASE_ENTRY_REGISTER B ON CONVERT(DATE,A.Date) = CONVERT(DATE,B.Entry_Date) and  Is_Migrated=0  where A.Date>='" + From.Value.ToString("yyyy/MM/dd") + "' and A.Date<='" + To.Value.ToString("yyyy/MM/dd") + "' group by A.Date order by A.Date;";


                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                List<string> labels = new List<string>();
                List<object> datasets = new List<object>();

                if (ds.Tables[0] != null)
                {
                    object label = "Sale";
                    object borderColor = "#3e95cd";
                    List<Int64> data = new List<long>();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        labels.Add(Convert.ToDateTime(item["date"]).ToString("dd MMM"));
                        data.Add(Convert.ToInt64(item["Total"]));
                    }
                    datasets.Add(new { label = label, borderColor = borderColor, data = data, fill = false });
                }
                if (ds.Tables[1] != null)
                {
                    object label = "Purchase";
                    object borderColor = "#8E5EA2";
                    List<Int64> data = new List<long>();
                    bool NeedLabel = false;
                    if (labels.Count < ds.Tables[1].Rows.Count)
                    {
                        labels.Clear();
                        NeedLabel = true;
                    }
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        if (NeedLabel)
                        {
                            labels.Add(item["date"].ToString());
                        }
                        data.Add(Convert.ToInt64(item["Total"]));
                    }
                    datasets.Add(new { label = label, borderColor = borderColor, data = data, fill = false });
                }
                return new { labels = labels, datasets = datasets };
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Admin | SalesVsPurchase(DateTime? From, DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        public static dynamic CashVsCreditVsExpenses(DateTime? From, DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                if (From == null || To == null)
                {
                    To = Application.Localisation.GetLocalDate(DateTime.UtcNow);
                    From = To.Value.AddDays(-6);

                }
                string query = @" SELECT CONVERT(varchar(20),A.Date,100) [Date], sum(ISNULL(Net_Amount,0))[Total] FROM TBL_CALENDER A
		 LEFT JOIN TBL_SALES_ENTRY_REGISTER B ON CONVERT(DATE,A.Date) = CONVERT(DATE,B.Sales_Date) AND Mode_Of_Payment =0 
		 WHERE A.DATE >='" + From.Value.ToString("yyyy/MM/dd") + "' and A.DATE <='" + To.Value.ToString("yyyy/MM/dd") + "' GROUP BY A.Date,A.WkDName2 order by A.date;" +
         "SELECT CONVERT(varchar(20),A.Date,100) [Date], sum(ISNULL(Net_Amount,0))[Total] FROM TBL_CALENDER A LEFT JOIN TBL_SALES_ENTRY_REGISTER B ON CONVERT(DATE,A.Date) = CONVERT(DATE,B.Sales_Date) AND Mode_Of_Payment =1 WHERE A.DATE >='" + From.Value.ToString("yyyy/MM/dd") + "' and A.DATE <='" + To.Value.ToString("yyyy/MM/dd") + "' GROUP BY A.Date,A.WkDName2 order by A.date;" +
                  "SELECT CONVERT(varchar(20),A.Date,100) [Date], sum(ISNULL(Net_Amount,0))[Total] FROM TBL_CALENDER A LEFT JOIN TBL_PURCHASE_ENTRY_REGISTER B ON CONVERT(DATE,A.Date) = CONVERT(DATE,B.Entry_date)  and Is_Migrated=0 WHERE A.DATE >='" + From.Value.ToString("yyyy/MM/dd") + "' and A.DATE <='" + To.Value.ToString("yyyy/MM/dd") + "' GROUP BY A.Date,A.WkDName2 order by A.date;";


                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                List<string> labels = new List<string>();
                List<object> datasets = new List<object>();

                if (ds.Tables[0] != null)
                {
                    object label = "Cash Sales";
                    object backgroundColor = "#3e95cd";
                    List<Int64> data = new List<long>();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        labels.Add(Convert.ToDateTime(item["date"]).ToString("dd MMM"));
                        data.Add(Convert.ToInt64(item["Total"]));
                    }
                    datasets.Add(new { label = label, backgroundColor = backgroundColor, data = data, fill = false });
                }
                if (ds.Tables[1] != null)
                {
                    object label = "Credit Sales";
                    object backgroundColor = "#607D8B";
                    List<Int64> data = new List<long>();

                    foreach (DataRow item in ds.Tables[1].Rows)
                    {

                        data.Add(Convert.ToInt64(item["Total"]));
                    }
                    datasets.Add(new { label = label, backgroundColor = backgroundColor, data = data, fill = false });
                }
                if (ds.Tables[2] != null)
                {
                    object label = "Expense";
                    object backgroundColor = "#ccc";
                    List<Int64> data = new List<long>();

                    foreach (DataRow item in ds.Tables[2].Rows)
                    {

                        data.Add(Convert.ToInt64(item["Total"]));
                    }
                    datasets.Add(new { label = label, backgroundColor = backgroundColor, data = data, fill = false });
                }
                return new { labels = labels, datasets = datasets };
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Admin | CashVsCreditVsExpenses(DateTime? From, DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static dynamic PurchaseSupplierwiseData(DateTime? From, DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @" ;with cte as (
                               select top 4 sm.Supplier_Id,sm.Name[Supplier],isnull(sum(pr.[Net_Amount]),0)[Total] from TBL_PURCHASE_ENTRY_REGISTER pr 
                               join TBL_SUPPLIER_MST sm on pr.Supplier_Id=sm.Supplier_Id group by sm.Name,sm.Supplier_Id
                               order by sum(pr.[Net_Amount]) desc
                               )
                               (select * from cte
                               union 
                               select 0,'Others',isnull(sum(pr.[Net_Amount]),0)[Total] from TBL_PURCHASE_ENTRY_REGISTER pr 
                               join TBL_SUPPLIER_MST sm on pr.Supplier_Id=sm.Supplier_Id  where  sm.Supplier_Id not in (select Supplier_Id  from cte)) order by Total desc";


                //if (From == null || To == null)
                //{
                //    To = DateTime.UtcNow;
                //    From = To.Value.AddDays(-7);
                //    query += " and sales_date>='" + From.Value.ToString("yyyy/MM/dd") + "' and sales_date<='" + To.Value.ToString("yyyy/MM/dd") + "' group by Sales_date;";
                //}
                //else if (From != null && To != null)
                //{
                //    query += " and sales_date>='" + From.Value.ToString("yyyy/MM/dd") + "' and sales_date<='" + To.Value.ToString("yyyy/MM/dd") + "' group by Sales_date;";
                //}
                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                List<string> labels = new List<string>();
                List<object> datasets = new List<object>();

                if (ds.Tables[0] != null)
                {
                    string[] backgroundColor = { "#3e95cd", "#455A64", "#3cba9f", "#ccc", "#c45850", "#455A64" };
                    object label = "Purchase";
                    List<Int64> data = new List<long>();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        labels.Add(item["Supplier"].ToString());
                        data.Add(Convert.ToInt64(item["Total"]));
                    }
                    datasets.Add(new { label = label, backgroundColor = backgroundColor, data = data });
                }

                return new { labels = labels, datasets = datasets };
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Admin | PurchaseSupplierwiseData(DateTime? From, DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        public static dynamic CardsData()
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"declare @totalCustomersToday int;
declare @weekdate date = dateadd(day,-7,@date);
declare @monthdate date = dateadd(day,-30,@date);

select @totalCustomersToday= count(distinct(customer_id)) from TBL_SALES_ENTRY_REGISTER where  Sales_Date=@date;

;with cte as(

 select case @totalCustomersToday when 0 then 0 else convert(int,round(count(distinct sr.Customer_Id)*1.00/@totalCustomersToday *100,0)) end [New CustomerP],count(distinct sr.Customer_Id)[New Customer] from 
 TBL_SALES_ENTRY_REGISTER   sr   join    TBL_CUSTOMER_MST cs on cs.customer_id=sr.Customer_Id
 where  sr.Sales_Date=@date and CONVERT(date,cs.Created_Date)=@date

),
cte1 as(
select case @totalCustomersToday when 0 then 0 else convert(int,round(count(distinct sr.Customer_Id)*1.00/@totalCustomersToday *100,0)) end [Returning CustomerP],count(distinct sr.Customer_Id)[Returning Customer]     from 
TBL_SALES_ENTRY_REGISTER   sr join TBL_CUSTOMER_MST cs on cs.customer_id=sr.Customer_Id
 where  sr.Sales_Date=@date and CONVERT(date,cs.Created_Date)!=@date
),
cte2 as(
select * from 
(select isnull(sum(net_amount),0) [Total_Sales] from TBL_SALES_ENTRY_REGISTER where  Sales_Date=@date) a,
(select isnull(sum(net_amount),0) [Cash_Sales] from TBL_SALES_ENTRY_REGISTER where  Sales_Date=@date and Mode_Of_Payment in (0,2)) b,
(select isnull(sum(net_amount),0) [Credit_Sales] from TBL_SALES_ENTRY_REGISTER where  Sales_Date=@date and  Mode_Of_Payment=1) c
),
cte3 as(
select * from 
(select isnull(sum(net_amount),0) [Total_Sales_7days] from TBL_SALES_ENTRY_REGISTER where  Sales_Date<=@date and Sales_Date>=@weekdate) a,
(select isnull(sum(net_amount),0) [Cash_Sales_7days] from TBL_SALES_ENTRY_REGISTER where  Sales_Date<=@date and Sales_Date>=@weekdate and Mode_Of_Payment in (0,2)) b,
(select isnull(sum(net_amount),0) [Credit_Sales_7days] from TBL_SALES_ENTRY_REGISTER where  Sales_Date<=@date and Sales_Date>=@weekdate and  Mode_Of_Payment=1) c
),
cte4 as(
select * from 
(select isnull(sum(net_amount),0) [Total_Sales_30days] from TBL_SALES_ENTRY_REGISTER where  Sales_Date<=@date and Sales_Date>=@monthdate) a,
(select isnull(sum(net_amount),0) [Cash_Sales_30days] from TBL_SALES_ENTRY_REGISTER where  Sales_Date<=@date and Sales_Date>=@monthdate and Mode_Of_Payment in (0,2)) b,
(select isnull(sum(net_amount),0) [Credit_Sales_30days] from TBL_SALES_ENTRY_REGISTER where  Sales_Date<=@date and Sales_Date>=@monthdate and  Mode_Of_Payment=1) c
)
,cte5 as(
select isnull(sum([pending amount]),0)[Total_Receivable] from Report_PendingCustomerPayments
),
cte6 as (
select isnull(sum([pending amount]),0)[Total_Payable] from Report_PendingSupplierPayments

)
select* from  cte,cte1,cte2,cte3,cte4,cte5,cte6";
                #endregion
                db.CreateParameters(1);
                db.Open();
                db.AddParameters(0, "@date", Application.Localisation.GetLocalDate(DateTime.UtcNow).Date.ToString("yyyy/MM/dd"));
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null && dt.Rows.Count == 1)
                {
                    return new
                    {
                        TotalSales = dt.Rows[0]["Total_Sales"].ToString(),
                        CashSales = dt.Rows[0]["Cash_Sales"].ToString(),
                        CreditSales = dt.Rows[0]["Credit_Sales"].ToString(),
                        TotalSalesLast7Days = dt.Rows[0]["Total_Sales_7days"].ToString(),
                        CashSalesLastLast7Days = dt.Rows[0]["Cash_Sales_7days"].ToString(),
                        CreditSalesLast7Days = dt.Rows[0]["Credit_Sales_7days"].ToString(),
                        TotalSalesLast30Days = dt.Rows[0]["Total_Sales_30days"].ToString(),
                        CashSalesLast30Days = dt.Rows[0]["Cash_Sales_30days"].ToString(),
                        CreditSalesLast30Days = dt.Rows[0]["Credit_Sales_30days"].ToString(),
                        ReturningCustomer = dt.Rows[0]["Returning Customer"].ToString(),
                        NewCustomer = dt.Rows[0]["New Customer"].ToString(),
                        NewCustomerPercentage = dt.Rows[0]["New CustomerP"].ToString(),
                        ReturningCustomerPercentage = dt.Rows[0]["Returning CustomerP"].ToString(),
                        TotalReceivable= dt.Rows[0]["Total_Receivable"].ToString(),
                        TotalPayable= dt.Rows[0]["Total_Payable"].ToString()
                    };
                }

                return null;



            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Admin | CardsData()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static dynamic InitialiseAllCharts(DateTime? From, DateTime? To)
        {
            List<object> Charts = new List<object>();
            Charts.Add(SalesVsPurchase(From, To));
            Charts.Add(CashVsCreditVsExpenses(From, To));
            Charts.Add(PurchaseSupplierwiseData(From, To));
            Charts.Add(CardsData());
            return Charts;
        }

        /// <summary>
        /// Fetch Data for recent activity
        /// </summary>
        /// <returns></returns>
        public static dynamic PopulateActivityLog()
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @";with cte as (
                                 select top 5 sr.[Created_Date][Date],CONVERT(varchar(15),cast(sr.[Created_Date] as time),100)[Time],Request_No[Reference],Customer_Name[party],
                                 'SR'[Code],'#c45850'[CodeColor],'On behalf of '+Customer_Name+' new Sales Request arrived.'[Activity],us.Full_Name[User] 
                                 ,'/Sales/Request?MODE=edit&UID='+convert(varchar(20),sr.Sr_Id)[Url] from TBL_SALES_REQUEST_REGISTER sr left join TBL_USER_MST us on sr.created_by=us. [User_Id] order by sr.Created_Date desc
                                 ),
                                 cte1 as(
                                 select top 5 pr.Created_Date[Date],CONVERT(varchar(15),cast(pr.Created_Date as time),100)[Time],pr.Request_No[Reference],
                                 l.name[Name],'PR'[Code],'#455a64'[CodeColor],
                                  'Purchase request has been requested from '+l.Name+'.' [Activity],us.Full_Name[User],'/Purchase/Request?MODE=edit&UID='+convert(varchar(20),pr.Pr_Id)[Url]
                                  from TBL_PURCHASE_REQUEST_REGISTER pr left join TBL_LOCATION_MST l on pr.location_id=l.Location_Id  
                                  left join TBL_USER_MST us on pr.created_by=us.[User_id]  
                                 order by pr.Created_Date desc
                                 ),
                                 cte2 as(
                                 select top 5 pe.Created_Date[Date],CONVERT(varchar(15),cast(pe.Created_Date as time),100)[Time],pe.Entry_No[Reference],
                                 l.Name[Name],'PE'[Code],'#F08080'[CodeColor],'New items purchased from  ' +s.Name+'' [Activity],
                                 u.Full_Name[User],'/Purchase/Entry?MODE=edit&UID='+convert(varchar(20),pe.Pe_Id)[Url]
                                  from TBL_PURCHASE_ENTRY_REGISTER pe left join TBL_LOCATION_MST l on l.Location_Id=pe.Location_Id
                                  left join TBL_SUPPLIER_MST s on s.Supplier_Id=pe.Supplier_Id
                                  left join TBL_USER_MST u on u.User_Id=pe.Created_By  order by pe.Created_Date desc
                                  ),
                                 cte3 as (
                                 select top 5 pq.Created_Date[Date],CONVERT(varchar(15),cast(pq.Created_Date as time),100)[Time],pq.Quote_No[Reference],
                                 l.Name[Name],'PQ'[Code],'#808080'[CodeColor],'New purchase order has been drafted to '+s.Name+''[Activity],
                                 u.Full_Name[User],'/Purchase/Quote?MODE=edit&UID='+convert(varchar(20),pq.Pq_Id)[Url] from TBL_PURCHASE_QUOTE_REGISTER pq
                                 left join TBL_LOCATION_MST l on l.Location_Id=pq.Location_Id
                                 left join TBL_SUPPLIER_MST s on s.Supplier_Id=pq.Supplier_Id
                                 left join TBL_USER_MST u on u.[User_Id]=pq.Created_By order by pq.Created_Date desc
                                 ),
                                 cte4 as (
                                 select top 5 se.Created_Date[Date],CONVERT(varchar(15),cast(se.Created_Date as time),100)[Time],se.Sales_Bill_No[Reference],
                                 l.Name[Name],'SE'[Code],'#800000'[CodeColor],'New invoices recorded against ' +c.Name+''[Activity],
                                 u.Full_Name[User],'/Sales/Entry?MODE=edit&UID='+convert(varchar(20),se.Se_Id)[Url] from TBL_SALES_ENTRY_REGISTER se
                                  left join TBL_LOCATION_MST l on l.Location_Id=se.Location_Id
                                  left join TBL_USER_MST u on u.[User_Id]=se.Created_By
                                  left join TBL_CUSTOMER_MST c on c.Customer_Id=se.Customer_Id
                                 order by se.Created_Date desc
                                  ),
                                  cte5 as (
                                  select top 5 sq.Created_Date[Date],CONVERT(varchar(15),cast(sq.Created_Date as time),100)[Time],sq.Quote_No[Reference],
                                  l.Name[Name],'SQ'[Code],'#808000'[CodeColor],'New sales order has been drafted against '+c.Name+''[Activity],
                                  u.Full_Name[User],'/Sales/Quote?MODE=edit&UID='+convert(varchar(20),sq.Sq_Id)[Url] from TBL_SALES_QUOTE_REGISTER sq 
                                  left join TBL_LOCATION_MST l on l.Location_Id=sq.Location_Id
                                  left join TBL_USER_MST u on u.[User_Id]=sq.Created_By
                                  left join TBL_CUSTOMER_MST c on c.Customer_Id=sq.Customer_Id
                                   order by sq.Created_Date desc
                                  ),
                                 cte6 as(
                                 select * from cte 
                                 union all
                                 select * from cte1
                                 union all 
                                 select * from cte2
                                 union all
                                 select * from cte3
                                 union all
                                 select * from cte4
                                 union all
                                 select * from cte5
                                 )
                                 select * from cte6 order by [date] desc  ";
                #endregion query
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<object> Activities = new List<object>();
                if (dt != null)
                {
                    foreach (DataRow activity in dt.Rows)
                    {
                        Activities.Add(new
                        {
                            Date = Application.Localisation.GetLocalDate(Convert.ToDateTime(activity["date"])).ToString("dd MMM yyyy"),
                            Time = Application.Localisation.GetLocalDate(Convert.ToDateTime(activity["date"])).ToString("hh:mm tt"),
                            Reference = activity["Reference"].ToString(),
                            Party = activity["Party"].ToString(),
                            Code = activity["Code"].ToString(),
                            Activity = activity["Activity"].ToString(),
                            CodeColor = activity["CodeColor"].ToString(),
                            User = activity["User"].ToString(),
                            Url = activity["url"].ToString()
                        });
                    }
                }
                return Activities;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Admin | PopulateActivityLog()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
