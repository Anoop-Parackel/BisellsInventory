using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace Entities.Reporting
{
    public class ReportingTool
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string ReportView { get; set; }
        public int? DefaultRows { get; set; }
        public string FilterAPI { get; set; }
        public string DetailKeyColumn { get; set; }
        public int? DetailKeyValue { get; set; }
        public int DetailReportId { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public long ReportViewObjectId { get; set; }
        /// <summary>
        /// SUMMARISED	0
        /// NESTABLE	1
        /// DETAILED    2
        /// </summary>
        public int Type { get; set; }
        public string TypeString
        {
            get
            {
                switch (this.Type)
                {
                    case 0:
                        return "SUMMARISED";
                    case 1:
                        return "NESTABLE";
                    case 2:
                        return "DETAILED";
                    default:
                        return string.Empty;
                }
            }
        }
        public int Status { get; set; }
        public List<ReportFilter> Filters { get; set; }
        public List<ReportSchema> Columns { get; set; }
        public int CreatedBy { get; set; }
        public Application.User CreatedUser { get; set; }
        public Application.User ModifiedUser { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ReportingTool(int ReportId, int KeyValue)
        {
            this.ReportId = ReportId;
            this.DetailKeyValue = KeyValue;
        }
        public ReportingTool(int ReportId)
        {
            this.ReportId = ReportId;
        }
        public ReportingTool(int ReportId, List<ReportFilter> Filters, int? DetailKeyValue)
        {
            this.ReportId = ReportId;
            this.Filters = Filters;
            this.DetailKeyValue = DetailKeyValue;
        }
        public ReportingTool()
        {

        }
        #region Css
        const string CSS = "<style>.adhoc-panel{width:100%;display:inline-block;background-color:#fff;box-shadow:0 2px 1px 0 rgba(0,0,0,.1);position:relative;border-radius:5px}.adhoc-filter-panel{position:absolute;background-color:#fff;z-index:99;top:-999px;right:0;border:1px solid #ccc;box-shadow:0 2px 8px 0 rgba(0,0,0,.3);display:none;transition:all .12s ease;border-radius:0 0 3px 3px;opacity:0}.adhoc-active{display:block;opacity:1;top:40px}.adhoc-label{margin-bottom:0;margin-top:5px;color:#004566}.adhoc-input{background-color:transparent;border:2px solid #ececec;padding:0 4px;border-radius:4px;height:30px;line-height:30px}.adhoc-range-left{padding-left:0}.adhoc-range-right{padding-right:0}.adhoc-btn-group{text-align:right;margin:10px 0}.adhoc-btn-group a{padding:3px 12px}.adhoc-btn-group a:nth-of-type(1),.adhoc-btn-group a:nth-of-type(2){margin-right:5px}.adhoc-heading{border-radius:3px 3px 0 0;box-shadow:0 2px 1px 0 rgba(0,0,0,.2);margin-bottom:15px;background-color: #607D8B}.adhoc-heading h3{color:#F5F5F5;text-transform:capitalize;text-shadow:1px 1px 1px rgba(0,0,0,.15);position:relative;margin:5px 0;font-size:20px}.adhoc-heading a{padding:0 10px;border-radius:3px;color:#F5F5F5;opacity:.9;font-size:14px;letter-spacing:.5px;position:absolute;right:0}.adhoc-heading a:hover{opacity:1}.adhoc-heading a i{margin-right:7px;border:1px solid #F5F5F5;padding:2px;border-radius:50%;font-weight:100}}.dataTables_filter,.dataTables_length,.dt-buttons .btn-group{display:inline-block}.dataTables_filter{float:right}a.btn.btn-default{padding:3px 15px;border:1px solid #ccc;color:#263238;box-shadow:0 1px 2px 0 rgba(0,0,0,.1)}a.btn.btn-default:active,a.btn.btn-default:hover,a.btn.btn-default:visited{color:#263238}.dataTables_length{float:left;width:120px;color:transparent;overflow:hidden;margin-left:-40px}.adhoc-table-panel .input-sm{height:27px;line-height:27px}.adhoc-table-panel .form-control{padding:2px 6px}.dataTables_filter>label>input{background-color:transparent;width:120px!important;border: 1px solid #7cb4d8;transition:width .3s ease-in-out}.dataTables_filter>label>input:focus{background-color:transparent;border: 1px solid #7cb4d8;width:170px!important}div.dt-button-info h2{display:none}div.dt-button-info{border:2px solid #ccc}table.dataTable.dtr-inline.collapsed>tbody>tr[role=row]>td:first-child:before,table.dataTable.dtr-inline.collapsed>tbody>tr[role=row]>th:first-child:before{top:5px !important;background-color:#6b8693 !important}table.dataTable.dtr-inline.collapsed>tbody>tr.parent>td:first-child:before,table.dataTable.dtr-inline.collapsed>tbody>tr.parent>th:first-child:before{background-color:#E57373 !important}.dataTables_info{display:inline;float:left}#tblReport{width:100% !important}table tfoot th.summable{border-bottom: 3px double;border-color: #004566;padding-bottom: 0px;color: cadetblue;}table >tbody >tr>td{padding:10px !important;} .dataTables_paginate.paging_simple_numbers{padding-top: 50px!important;padding-bottom:10px}.dataTables_info{padding-top: 50px!important;}  </style>";
        #endregion Css

        public string GenerateReport()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select rpt.*,usr.[User_Id],usr.Profile_Image_Path,usr.Full_Name,mdf.[User_Id][mdf_UserId],mdf.Profile_Image_Path[mdf_ProfileImage],mdf.Full_Name[mdf_Fullname] from [dbo].[TBL_REPORT_MST] rpt join TBL_USER_MST usr on rpt.Created_By=usr.[User_Id] 
                  left join TBL_USER_MST mdf on rpt.Modified_By=mdf.[User_Id] 
                  where Report_Id=@Report_Id;
                               select * from [dbo].TBL_REPORT_FILTER_RELATION where Report_Id =@Report_Id ;
                               select * from [dbo].TBL_REPORT_SCHEMA_RELATION where Report_Id =@Report_Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Report_Id", ReportId);
                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    throw new ReportingNotCreatedException(ReportId);

                }
                this.Filters = new List<ReportFilter>();
                this.ReportName = Convert.ToString(ds.Tables[0].Rows[0]["report_name"]);
                this.ReportView = Convert.ToString(ds.Tables[0].Rows[0]["report_view"]);
                this.FilterAPI = Convert.ToString(ds.Tables[0].Rows[0]["filterapi"]);
                this.PrimaryKeyColumn = Convert.ToString(ds.Tables[0].Rows[0]["primary_key_column"]);
                this.DetailKeyColumn = Convert.ToString(ds.Tables[0].Rows[0]["detail_key_column"]);
                this.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["report_type"]);
                this.DetailReportId = ds.Tables[0].Rows[0]["detail_report_id"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["detail_report_id"]) : 0;
                this.DefaultRows = ds.Tables[0].Rows[0]["default_rows"] != DBNull.Value ? (int?)Convert.ToInt32(ds.Tables[0].Rows[0]["default_rows"]) : null;
                this.Status = Convert.ToInt32(ds.Tables[0].Rows[0]["status"]);
                this.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["created_date"]);
                this.ModifiedDate = ds.Tables[0].Rows[0]["modified_date"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["modified_date"]) : (DateTime?)null;
                this.CreatedUser = new Application.User()
                {
                    ID = Convert.ToInt32(ds.Tables[0].Rows[0]["User_Id"]),
                    FullName = Convert.ToString(ds.Tables[0].Rows[0]["Full_Name"]),
                    ProfileImagePath = Convert.ToString(ds.Tables[0].Rows[0]["Profile_Image_Path"]),
                };
                if (ds.Tables[0].Rows[0]["mdf_UserId"] != DBNull.Value)
                {
                    this.ModifiedUser = new Application.User()
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[0]["mdf_UserId"]),
                        FullName = Convert.ToString(ds.Tables[0].Rows[0]["mdf_Fullname"]),
                        ProfileImagePath = Convert.ToString(ds.Tables[0].Rows[0]["mdf_ProfileImage"])
                    };
                }

                //Fetching filters for the current report
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    ReportFilter rf = new ReportFilter();
                    rf.ReportId = this.ReportId;
                    rf.ReportFilterId = Convert.ToInt32(item["report_filter_id"]);
                    rf.FilterName = Convert.ToString(item["filter_name"]);
                    rf.Label = Convert.ToString(item["label"]);
                    rf.ColumnIndex = item["column_index"] != DBNull.Value ? Convert.ToInt32(item["column_index"]) : 0;
                    rf.Type = Convert.ToInt32(item["type"]);
                    rf.ControlType = Convert.ToInt32(item["control_type"]);
                    rf.DataTextField = item["data_text_field"] != DBNull.Value ? Convert.ToString(item["data_text_field"]) : string.Empty;
                    rf.DataTextFieldId = item["data_text_field_id"] != DBNull.Value ? Convert.ToInt32(item["data_text_field_id"]) : 0;
                    rf.DataValueField = item["data_value_field"] != DBNull.Value ? Convert.ToString(item["data_value_field"]) : string.Empty;
                    rf.DataValueFieldId = item["data_value_field_id"] != DBNull.Value ? Convert.ToInt32(item["data_value_field_id"]) : 0;
                    this.Filters.Add(rf);
                }
                this.Columns = new List<ReportSchema>();
                //Fetching columns for the current report
                foreach (DataRow item in ds.Tables[2].Rows)
                {
                    ReportSchema rs = new ReportSchema();
                    rs.ReportId = this.ReportId;
                    rs.ReportSchemaId = Convert.ToInt32(item["report_schema_id"]);
                    rs.ColumnIndex = Convert.ToInt32(item["column_index"]);
                    rs.ColumnName = Convert.ToString(item["column_name"]);
                    rs.DisplayName = Convert.ToString(item["display_name"]);
                    rs.IsSummable = Convert.ToBoolean(item["IsSummable"]);
                    rs.Render = Convert.ToString(item["render"]);
                    rs.IsSummable = item["IsSummable"] != DBNull.Value ? Convert.ToBoolean(item["IsSummable"]) : false;
                    this.Columns.Add(rs);
                }
                //HTML rendering
                StringBuilder script = new StringBuilder("<script type='text/javascript'>$(document).ready(function () { $('head').append('');});function applyFilter(){ var filterArray=[];");
                Panel mainPanel = new Panel() { CssClass = "adhoc-panel" };
                LiteralControl styles = new LiteralControl();
                styles.Text = CSS;
                mainPanel.Controls.Add(styles);
                //mainPanel.Style.Add("display", "none");
                Panel tableHeading = new Panel() { CssClass = "col-sm-12 adhoc-heading" };
                LiteralControl tableHead = new LiteralControl() { Text = "<h3>" + this.ReportName + "&nbsp;<a href='#' onclick='$(\".adhoc-filter-panel\").toggleClass(\"adhoc-active\");'><i class='fa fa-chevron-down'></i>Filter</a></h3>" };
                tableHeading.Controls.Add(tableHead);
                mainPanel.Controls.Add(tableHeading);
                #region RenderingFilterControl
                //creating filter section
                if (this.Filters.Count > 0)
                {
                    Panel pnlFilter = new Panel();
                    pnlFilter.CssClass = "col-sm-3 adhoc-filter-panel";
                    for (int i = 0; i < this.Filters.Count; i++)
                    {
                        Panel pnlCurrentFilter = new Panel();
                        pnlCurrentFilter.CssClass = "col-sm-12";
                        //Type=Exact Value or Contains Search
                        if (this.Filters[i].Type == 2 || this.Filters[i].Type == 1)
                        {

                            if (this.Filters[i].ControlType == 1)
                            {
                                pnlCurrentFilter.Controls.Add(new Literal() { Text = "<label class='adhoc-label'>" + this.Filters[i].FilterName + "</label>" });
                                TextBox txt = new TextBox();
                                txt.CssClass = "adhoc-input form-control ";
                                txt.Attributes.Add("placeholder", this.Filters[i].FilterName);
                                txt.ID = "txt" + i;
                                txt.ClientIDMode = ClientIDMode.Static;
                                pnlCurrentFilter.Controls.Add(txt);
                                script.Append("filterArray.push({ReportFilterId:'" + this.Filters[i].ReportFilterId + "',Value1:$('#txt" + i + "').val()});");
                            }
                            else if (this.Filters[i].ControlType == 0)
                            {
                                pnlCurrentFilter.Controls.Add(new Literal() { Text = "<label class='adhoc-label'>" + this.Filters[i].FilterName + "</label>" });
                                DataTable dtOptions = db.ExecuteQuery(CommandType.Text, "select distinct [" + this.Filters[i].DataValueField + "] [value], [" + this.Filters[i].DataTextField + "] [text] from [" + this.ReportView + "]");
                                DropDownList ddl = new DropDownList();
                                ddl.CssClass = "adhoc-input form-control ";
                                ddl.ID = "ddl" + i;
                                ddl.ClientIDMode = ClientIDMode.Static;
                                ddl.Items.Clear();
                                ddl.Items.Add(new ListItem("select", ""));
                                foreach (DataRow item in dtOptions.Rows)
                                {
                                    ddl.Items.Add(new ListItem(item["text"].ToString(), item["value"].ToString()));
                                }
                                pnlCurrentFilter.Controls.Add(ddl);
                                script.Append("filterArray.push({ReportFilterId:'" + this.Filters[i].ReportFilterId + "',Value1:$('#ddl" + i + "').val()});");
                            }
                            else if (this.Filters[i].ControlType == 2)
                            {
                                pnlCurrentFilter.Controls.Add(new Literal() { Text = "<label class='adhoc-label'>" + this.Filters[i].FilterName + "</label>" });
                                TextBox txt = new TextBox();
                                txt.CssClass = "adhoc-input form-control ";
                                txt.ID = "txtDate" + i;
                                txt.ClientIDMode = ClientIDMode.Static;
                                Literal datepicker = new Literal() { Text = "<script>$(document).ready(function(){$('#txtDate" + i + "').datepicker({autoclose: true,format: 'dd/M/yyyy',todayHighlight: true});});</script>" };
                                script.Append("filterArray.push({ReportFilterId:'" + this.Filters[i].ReportFilterId + "',Value1:$('#txtDate" + i + "').val()});");
                                pnlCurrentFilter.Controls.Add(txt);
                                pnlCurrentFilter.Controls.Add(datepicker);
                            }

                            pnlFilter.Controls.Add(pnlCurrentFilter);
                        }
                        //Type=Range
                        else if (this.Filters[i].Type == 0)
                        {
                            if (this.Filters[i].ControlType == 1)
                            {
                                pnlCurrentFilter.Controls.Add(new Literal() { Text = "<div><label class='adhoc-label'>" + this.Filters[i].FilterName + "</label></div>" });
                                TextBox txt1 = new TextBox();
                                txt1.CssClass = "adhoc-input form-control ";
                                txt1.Attributes.Add("placeholder", "From:");
                                txt1.ID = "txt" + i + "A";
                                txt1.ClientIDMode = ClientIDMode.Static;
                                Panel txtPnl = new Panel() { CssClass = "col-sm-6 adhoc-range-left" };
                                txtPnl.Controls.Add(txt1);
                                pnlCurrentFilter.Controls.Add(txtPnl);
                                TextBox txt2 = new TextBox();
                                txt2.CssClass = "adhoc-input form-control ";
                                txt2.Attributes.Add("placeholder", "To:");
                                txt2.ID = "txt" + i + "B";
                                txt2.ClientIDMode = ClientIDMode.Static;
                                Panel txtPnl2 = new Panel() { CssClass = "col-sm-6 adhoc-range-right" };
                                txtPnl2.Controls.Add(txt2);
                                pnlCurrentFilter.Controls.Add(txtPnl2);
                                script.Append("filterArray.push({ReportFilterId:'" + this.Filters[i].ReportFilterId + "',Value1:$('#txt" + i + "A').val(),Value2:$('#txt" + i + "B').val()});");
                            }
                            else if (this.Filters[i].ControlType == 0)
                            {
                                pnlCurrentFilter.Controls.Add(new Literal() { Text = "<div><label class='adhoc-label'>" + this.Filters[i].FilterName + "</label></div>" });
                                DataTable dtOptions = db.ExecuteQuery(CommandType.Text, "select distinct [" + this.Filters[i].DataValueField + "] [value], [" + this.Filters[i].DataTextField + "] [text] from [" + this.ReportView + "]");
                                DropDownList ddl = new DropDownList();
                                ddl.CssClass = "adhoc-input form-control ";
                                ddl.ID = "ddl" + i + "A";
                                ddl.ClientIDMode = ClientIDMode.Static;
                                DropDownList ddl2 = new DropDownList();
                                ddl2.CssClass = "adhoc-input form-control ";
                                ddl2.ID = "ddl" + i + "B";
                                ddl2.ClientIDMode = ClientIDMode.Static;
                                ddl.Items.Clear();
                                ddl.Items.Add(new ListItem("select", ""));
                                ddl2.Items.Clear();
                                ddl2.Items.Add(new ListItem("select", ""));
                                foreach (DataRow item in dtOptions.Rows)
                                {
                                    ddl.Items.Add(new ListItem(item["text"].ToString(), item["value"].ToString()));
                                    ddl2.Items.Add(new ListItem(item["text"].ToString(), item["value"].ToString()));

                                }
                                Panel ddlPnl = new Panel() { CssClass = "col-sm-6 adhoc-range-left" };
                                ddlPnl.Controls.Add(ddl);
                                Panel ddlPnl2 = new Panel() { CssClass = "col-sm-6 adhoc-range-right" };
                                ddlPnl2.Controls.Add(ddl2);
                                pnlCurrentFilter.Controls.Add(ddlPnl);
                                pnlCurrentFilter.Controls.Add(ddlPnl2);
                                script.Append("filterArray.push({ReportFilterId:'" + this.Filters[i].ReportFilterId + "',Value1:$('#ddl" + i + "A').val(),Value2:$('#ddl" + i + "B').val()});");
                            }
                            else if (this.Filters[i].ControlType == 2)
                            {

                                pnlCurrentFilter.Controls.Add(new Literal() { Text = "<div><label class='adhoc-label'>" + this.Filters[i].FilterName + "</label></div>" });
                                TextBox txt = new TextBox();
                                txt.CssClass = "adhoc-input form-control ";
                                txt.ID = "txtDate" + i + "A";
                                txt.Attributes.Add("placeholder", "From:");
                                txt.ClientIDMode = ClientIDMode.Static;
                                Literal datepicker = new Literal() { Text = "<script>$(document).ready(function(){$('#txtDate" + i + "A').datepicker({autoclose: true,format: 'dd/M/yyyy',todayHighlight: true});});</script>" };
                                Panel txtPnl = new Panel() { CssClass = "col-sm-6 adhoc-range-left" };
                                txtPnl.Controls.Add(txt);
                                pnlCurrentFilter.Controls.Add(txtPnl);
                                pnlCurrentFilter.Controls.Add(datepicker);
                                TextBox txt2 = new TextBox();
                                txt2.CssClass = "adhoc-input form-control ";
                                txt2.ID = "txtDate" + i + "B";
                                txt2.Attributes.Add("placeholder", "To:");
                                txt2.ClientIDMode = ClientIDMode.Static;
                                Literal datepicker2 = new Literal() { Text = "<script>$(document).ready(function(){$('#txtDate" + i + "B').datepicker({autoclose: true,format: 'dd/M/yyyy',todayHighlight: true});});</script>" };
                                script.Append("filterArray.push({ReportFilterId:'" + this.Filters[i].ReportFilterId + "',Value1:$('#txtDate" + i + "A').val(),Value2:$('#txtDate" + i + "B').val()});");
                                Panel txtPnl2 = new Panel() { CssClass = "col-sm-6 adhoc-range-right" };
                                txtPnl2.Controls.Add(txt2);
                                pnlCurrentFilter.Controls.Add(txtPnl2);
                                pnlCurrentFilter.Controls.Add(datepicker2);
                            }
                            pnlFilter.Controls.Add(pnlCurrentFilter);
                        }

                    }
                    Panel pnlButton = new Panel();
                    pnlButton.CssClass = "col-sm-12 adhoc-btn-group";
                    LinkButton btnApplyFilter = new LinkButton() { CssClass = "btn btn-danger", Text = "<i class='fa fa-play'/> </i> Apply", OnClientClick = "applyFilter()" };
                    pnlButton.Controls.Add(btnApplyFilter);
                    LinkButton btnResetFilter = new LinkButton() { CssClass = "btn btn-default", Text = "Reset", OnClientClick = "$('form')[0].reset()" };
                    LinkButton btnCloseFilter = new LinkButton() { CssClass = "btn btn-default", Text = "Close", OnClientClick = "$('.adhoc-filter-panel').removeClass('adhoc-active')" };
                    pnlButton.Controls.Add(btnResetFilter);
                    pnlButton.Controls.Add(btnCloseFilter);
                    pnlFilter.Controls.Add(pnlButton);
                    mainPanel.Controls.Add(pnlFilter);

                }
                script.Append("var KeyValue = getUrlVars()['KeyValue'] != undefined ? getUrlVars()['KeyValue'] : null;$.ajax({ url: '" + this.FilterAPI + "?id=" + this.ReportId + "&KeyValue='+KeyValue, method: 'POST', contentType: 'application/json;charset=utf-8', data: JSON.stringify(filterArray), success: function (data) { var response = data;  $('#tableContainer').children().remove(); $('#tableContainer').append($.parseHTML(response));$('#tblReport').dataTable({ responsive: true, dom: 'Blfrtip', buttons: ['copy', 'csv', 'excel', 'print'], footerCallback: function (row, data, start, end, isplay) { var api = this.api(); api.columns('.summable').every(function () { var sum = api.cells(null, this.index()).render('display').reduce(function (a, b) { var x = parseFloat(a) || 0; var y = parseFloat(b) || 0; return x + y; }, 0); console.log(this.index() + ' ' + sum); $(this.footer()).html('<i><small>Total</small></i><p>' + sum.toFixed(2) + '</p>'); }); } });$('.adhoc-filter-panel').toggleClass('adhoc-active'); },beforeSend: function () { $('.adhoc-btn-group').children('a:nth-child(1)').children('i').removeClass('fa-play').addClass('fa-spinner fa-spin')},complete: function () { $('.adhoc-btn-group').children('a:nth-child(1)').children('i').addClass('fa-play').removeClass('fa-spinner fa-spin');}, error: function (xhr, status) { alert(xhr.responseText); } }); ");
                script.Append("}");
                script.Append("</script>");
                script.Append("<script>function getUrlVars() { var vars = [], hash;var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');for (var i = 0; i < hashes.length; i++) { hash = hashes[i].split('=');vars.push(hash[0]);vars[hash[0]] = hash[1];}return vars;}</script>");
                script.Append("<script>function PopupCenter(url, title, w, h) { var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left; var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top; var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width; var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height; var left = ((width / 2) - (w / 2)) + dualScreenLeft; var top = ((height / 2) - (h / 2)) + dualScreenTop; var newWindow = window.open(url, title, 'scrollbars=yes,toolbar=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left); if (window.focus) { newWindow.focus(); } };</script>");
                #endregion RenderingFilterControl

                string sql = this.GetInitialQuery(this.DefaultRows);
                DataTable dt = db.ExecuteQuery(CommandType.Text, sql);
                Panel pnlTable = new Panel() { CssClass = "col-sm-12 adhoc-table-panel", ID = "tableContainer", ClientIDMode = ClientIDMode.Static };
                Table table = new Table() { CssClass = "table table-hover table-striped", ID = "tblReport", ClientIDMode = ClientIDMode.Static };
                TableHeaderRow thead = new TableHeaderRow() { TableSection = TableRowSection.TableHeader };
                TableHeaderCell thKey = null;

                TableFooterRow tfoot = new TableFooterRow() { TableSection = TableRowSection.TableFooter };

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0 && this.Type == 1)
                    {
                        thKey = new TableHeaderCell();
                        thKey.Text = dt.Columns[i].ColumnName;
                        thKey.Style.Add("display", "none");
                        thKey.CssClass = "never hidden-key-value";

                    }
                    else
                    {
                        TableHeaderCell th = new TableHeaderCell() { Text = dt.Columns[i].ColumnName };
                        TableHeaderCell fth = new TableHeaderCell();
                        if (this.Type == 1)
                        {
                            if (this.Columns[i - 1].IsSummable)
                            {
                                th.CssClass = "summable";
                                fth.CssClass = "summable";
                            }
                        }
                        else
                        {
                            if (this.Columns[i].IsSummable)
                            {
                                th.CssClass = "summable";
                                fth.CssClass = "summable";
                            }
                        }
                        thead.Cells.Add(th);
                        tfoot.Cells.Add(fth);
                    }

                }
                if (thKey != null)
                {
                    thead.Cells.Add(new TableHeaderCell() { Text = "", HorizontalAlign = HorizontalAlign.Center });
                    thead.Cells.Add(thKey);
                    tfoot.Cells.Add(new TableHeaderCell());
                    tfoot.Cells.Add(new TableHeaderCell());
                }
                table.Rows.Add(thead);


                foreach (DataRow row in dt.Rows)
                {
                    TableCell tdKey = null;
                    TableRow tr = new TableRow() { TableSection = TableRowSection.TableBody };
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (i == 0 && this.Type == 1)
                        {
                            tdKey = new TableCell();
                            tdKey.Text = row[i].ToString();
                            tdKey.Style.Add("display", "none");
                            tdKey.CssClass = "never hidden-key-value";
                        }
                        else
                        {
                            TableCell td = new TableCell();
                            td.Text = row[i].ToString();
                            tr.Cells.Add(td);
                        }

                    }
                    if (tdKey != null)
                    {
                        tr.Cells.Add(new TableCell() { Text = "<a href='#' onclick=\"PopupCenter('/Reports/Details?ReportUID=" + this.DetailReportId + "&KeyValue=" + tdKey.Text + "','Details','1200','700')\"><button type=\"button\" class=\"btn btn-sm btn-default\"><i class=\"md md-visibility\"></i></button></a>", HorizontalAlign = HorizontalAlign.Center });
                        tr.Cells.Add(tdKey);
                    }
                    table.Rows.Add(tr);
                }
                table.Rows.Add(tfoot);
                pnlTable.Controls.Add(table);
                mainPanel.Controls.Add(pnlTable);
                LiteralControl ltr = new LiteralControl();
                ltr.Text = script.ToString();
                LiteralControl ltrForDataTable = new LiteralControl() { Text = "<script>$(document).ready(function () { $('#tblReport').dataTable( {responsive:true, dom: 'Blfrtip', buttons: ['copy', 'csv', 'excel', 'print'], footerCallback: function(row, data, start, end, isplay) {var api = this.api();api.columns('.summable').every(function() {var sum = api.cells(null, this.index()).render('display').reduce(function(a, b) {var x = parseFloat(a) || 0;var y = parseFloat(b) || 0;return x + y;}, 0);console.log(this.index() + ' ' + sum);$(this.footer()).html('<i><small>Total</small></i><p>' + sum.toFixed(2) + '</p>');});}});});</script>" };
                mainPanel.Controls.Add(ltr);
                mainPanel.Controls.Add(ltrForDataTable);
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                System.Web.UI.HtmlTextWriter html = new HtmlTextWriter(sw);
                mainPanel.RenderControl(html);
                return sb.ToString();
            }
            catch (ReportingNotCreatedException ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |  GenerateReport()");
                return "<h2>No Report Found!</h2><p> Contact Administrator</p>";
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |  GenerateReport()");
                return "<h2>Bad Report Configuration!</h2><p> Contact Administrator</p>";
            }
            finally
            {
                db.Close();
            }

        }

        public string ApplyFilters()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select * from [dbo].[TBL_REPORT_MST] where Report_Id =@Report_Id ;
                               select * from [dbo].TBL_REPORT_FILTER_RELATION where Report_Id =@Report_Id;
                               select * from [dbo].TBL_REPORT_SCHEMA_RELATION where Report_Id =@Report_Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Report_Id", ReportId);
                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                this.ReportName = Convert.ToString(ds.Tables[0].Rows[0]["report_name"]);
                this.ReportView = Convert.ToString(ds.Tables[0].Rows[0]["report_view"]);
                this.FilterAPI = Convert.ToString(ds.Tables[0].Rows[0]["filterapi"]);
                this.PrimaryKeyColumn = Convert.ToString(ds.Tables[0].Rows[0]["primary_key_column"]);
                this.DetailKeyColumn = Convert.ToString(ds.Tables[0].Rows[0]["detail_key_column"]);
                this.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["report_type"]);
                this.DetailReportId = ds.Tables[0].Rows[0]["detail_report_id"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["detail_report_id"]) : 0;
                this.DefaultRows = ds.Tables[0].Rows[0]["default_rows"] != DBNull.Value ? (int?)Convert.ToInt32(ds.Tables[0].Rows[0]["default_rows"]) : null;
                this.Status = Convert.ToInt32(ds.Tables[0].Rows[0]["status"]);
                //Fetching filters for the current report
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    for (int i = 0; i < this.Filters.Count; i++)
                    {
                        if (Convert.ToInt32(item["report_filter_id"]) == this.Filters[i].ReportFilterId)
                        {
                            this.Filters[i].ReportId = this.ReportId;
                            this.Filters[i].FilterName = Convert.ToString(item["filter_name"]);
                            this.Filters[i].Label = Convert.ToString(item["label"]);
                            this.Filters[i].ColumnIndex = item["column_index"] != DBNull.Value ? Convert.ToInt32(item["column_index"]) : 0;
                            this.Filters[i].ColumnName = Convert.ToString(item["column_name"]);
                            this.Filters[i].Type = Convert.ToInt32(item["type"]);
                            this.Filters[i].ControlType = Convert.ToInt32(item["control_type"]);
                            this.Filters[i].ColumnDataType = Convert.ToString(item["column_data_type"]);
                            break;
                        }
                    }
                }
                this.Columns = new List<ReportSchema>();
                //Fetching columns for the current report
                foreach (DataRow item in ds.Tables[2].Rows)
                {
                    ReportSchema rs = new ReportSchema();
                    rs.ReportId = this.ReportId;
                    rs.ReportSchemaId = item["report_schema_id"] != DBNull.Value ? Convert.ToInt32(item["report_schema_id"]) : 0;
                    rs.ColumnIndex = item["column_index"] != DBNull.Value ? Convert.ToInt32(item["column_index"]) : 0;
                    rs.ColumnName = Convert.ToString(item["column_name"]);
                    rs.DisplayName = Convert.ToString(item["display_name"]);
                    rs.Render = Convert.ToString(item["render"]);
                    rs.IsSummable = Convert.ToBoolean(item["IsSummable"]);
                    this.Columns.Add(rs);
                }
                //HTML rendering
                StringBuilder sql = new StringBuilder(this.GetInitialQuery(null));
                for (int i = 0; i < this.Filters.Count; i++)
                {
                    if (this.Filters[i].Type == 1)
                    {
                        if (this.Filters[i].Value1 != null && !string.IsNullOrEmpty(this.Filters[i].Value1))
                        {
                            if (this.Filters[i].ColumnDataType == "int")
                            {
                                sql.Append(" and [" + this.Filters[i].ColumnName + "] =" + this.Filters[i].Value1);
                            }
                            else
                            {
                                sql.Append(" and [" + this.Filters[i].ColumnName + "] ='" + this.Filters[i].Value1 + "'");
                            }
                        }
                    }
                    else if (this.Filters[i].Type == 2)
                    {
                        sql.Append(" and [" + this.Filters[i].ColumnName + "] like '%" + this.Filters[i].Value1 + "%'");
                    }
                    else if (this.Filters[i].Type == 0 && this.Filters[i].Value1 != null && !string.IsNullOrEmpty(this.Filters[i].Value1) && this.Filters[i].Value2 != null && !string.IsNullOrEmpty(this.Filters[i].Value2))
                    {
                        if (this.Filters[i].ColumnDataType == "int")
                        {
                            sql.Append(" and ([" + this.Filters[i].ColumnName + "] between " + this.Filters[i].Value1 + " and " + this.Filters[i].Value2 + ")");
                        }
                        else
                        {
                            sql.Append(" and ([" + this.Filters[i].ColumnName + "] between '" + this.Filters[i].Value1 + "' and '" + this.Filters[i].Value2 + "')");
                        }
                    }
                }

                DataTable dt = db.ExecuteQuery(CommandType.Text, sql.ToString());
                Table table = new Table() { CssClass = "table table-hover table-striped", ID = "tblReport", ClientIDMode = ClientIDMode.Static };
                TableHeaderRow thead = new TableHeaderRow() { TableSection = TableRowSection.TableHeader };
                TableFooterRow tfoot = new TableFooterRow() { TableSection = TableRowSection.TableFooter };
                TableHeaderCell thKey = null;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0 && this.Type == 1)
                    {
                        thKey = new TableHeaderCell();
                        thKey.Text = dt.Columns[i].ColumnName;
                        thKey.Style.Add("display", "none");
                        thKey.CssClass = "never hidden-key-value";
                    }
                    else
                    {
                        TableHeaderCell th = new TableHeaderCell() { Text = dt.Columns[i].ColumnName };
                        TableHeaderCell fth = new TableHeaderCell();
                        if (this.Type == 1)
                        {
                            if (this.Columns[i - 1].IsSummable)
                            {
                                th.CssClass = "summable";
                                fth.CssClass = "summable";
                            }
                        }
                        else
                        {
                            if (this.Columns[i].IsSummable)
                            {
                                th.CssClass = "summable";
                                fth.CssClass = "summable";
                            }
                        }
                        thead.Cells.Add(th);
                        tfoot.Cells.Add(fth);

                    }

                }
                if (thKey != null)
                {
                    thead.Cells.Add(new TableHeaderCell() { Text = "", HorizontalAlign = HorizontalAlign.Center });
                    thead.Cells.Add(thKey);
                    tfoot.Cells.Add(new TableHeaderCell());
                    tfoot.Cells.Add(new TableHeaderCell());
                }
                table.Rows.Add(thead);

                foreach (DataRow row in dt.Rows)
                {
                    TableCell tdKey = null;
                    TableRow tr = new TableRow() { TableSection = TableRowSection.TableBody };
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (i == 0 && this.Type == 1)
                        {
                            tdKey = new TableCell();
                            tdKey.Text = row[i].ToString();
                            tdKey.Style.Add("display", "none");
                            tdKey.CssClass = "never hidden-key-value";
                        }
                        else
                        {
                            TableCell td = new TableCell();
                            td.Text = row[i].ToString();
                            tr.Cells.Add(td);
                        }

                    }
                    if (tdKey != null)
                    {
                        tr.Cells.Add(new TableCell() { Text = "<a href='#' onclick=\"PopupCenter('/Reports/Details?ReportUID=" + this.DetailReportId + "&KeyValue=" + tdKey.Text + "','Details','1200','700')\"><button type=\"button\" class=\"btn btn-sm btn-default\"><i class=\"md md-visibility\"></i></button></a>", HorizontalAlign = HorizontalAlign.Center });
                        tr.Cells.Add(tdKey);
                    }
                    table.Rows.Add(tr);
                }
                table.Rows.Add(tfoot);
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                System.Web.UI.HtmlTextWriter html = new HtmlTextWriter(sw);
                table.RenderControl(html);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |  ApplyFilters()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public OutputMessage Save()
        {
            DBManager db = new DBManager();
            if (string.IsNullOrEmpty(this.ReportName))
            {
                return new OutputMessage("Report Name must not be empty", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrEmpty(this.ReportView) || this.ReportView == "--select--")
            {
                return new OutputMessage("Select a View for reporting", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.Columns.Count <= 0)
            {
                return new OutputMessage("Select some columns for creating report", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    string query = @"insert into [dbo].[TBL_REPORT_MST] ([Report_Name],[Report_View],[Report_View_Object_Id],[Default_Rows],[Detail_Report_Id],[Primary_Key_Column],[Detail_Key_Column],[FilterAPI],[Report_Type],[Status],[created_by],[created_date],Description) values (@Report_Name,@Report_View,(select [object_id] from sys.views where name =@Report_View),@Default_Rows,@Detail_Report_Id,@Primary_Key_Column,@Detail_Key_Column,@FilterAPI,@Report_Type,1,@created_by,getutcdate(),@Description);select @@identity;";
                    db.CreateParameters(10);
                    db.AddParameters(0, "@Report_Name", this.ReportName);
                    db.AddParameters(1, "@Default_Rows", this.DefaultRows);
                    db.AddParameters(2, "@Detail_Report_Id", this.DetailReportId);
                    db.AddParameters(3, "@Primary_Key_Column", this.PrimaryKeyColumn);
                    db.AddParameters(4, "@Detail_Key_Column", this.DetailKeyColumn);
                    db.AddParameters(5, "@FilterAPI", this.FilterAPI);
                    db.AddParameters(6, "@Report_Type", this.Type);
                    db.AddParameters(7, "@Report_View", this.ReportView);
                    db.AddParameters(8, "@created_by", this.CreatedBy);
                    db.AddParameters(9, "@Description", this.Description);
                    db.Open();
                    int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    db.CleanupParameters();
                    foreach (ReportFilter filter in this.Filters)
                    {
                        query = @"insert into [dbo].[TBL_REPORT_FILTER_RELATION]  ([Filter_Name],[Label],[Report_Id],[Column_Name],[Column_Data_Type],[Type],[Control_Type],[Data_Text_Field],[Data_Value_Field])  values (@Filter_Name,@Label,@Report_Id,@Column_Name,@Column_Data_Type,@Type,@Control_Type,@Data_Text_Field,@Data_Value_Field)";
                        db.CreateParameters(9);
                        db.AddParameters(0, "@Filter_Name", filter.FilterName);
                        db.AddParameters(1, "@Label", filter.Label);
                        db.AddParameters(2, "@Report_Id", identity);
                        db.AddParameters(3, "@Column_Name", filter.ColumnName);
                        db.AddParameters(4, "@Type", filter.Type);
                        db.AddParameters(5, "@Control_Type", filter.ControlType);
                        db.AddParameters(6, "@Data_Text_Field", filter.DataTextField);
                        db.AddParameters(7, "@Data_Value_Field", filter.DataValueField);
                        db.AddParameters(8, "@Column_Data_Type", filter.ColumnDataType);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    db.CleanupParameters();
                    foreach (ReportSchema column in this.Columns)
                    {
                        query = @"insert into [dbo].[TBL_REPORT_SCHEMA_RELATION] ([Report_Id],[Column_Index],[Column_Name],[Display_Name],[Render],IsSummable)
                            values (@Report_Id,@Column_Index,@Column_Name,@Display_Name,@Render,@IsSummable)";
                        db.CreateParameters(6);
                        db.AddParameters(0, "@Report_Id", identity);
                        db.AddParameters(1, "@Column_Index", column.ColumnIndex);
                        db.AddParameters(2, "@Column_Name", column.ColumnName);
                        db.AddParameters(3, "@Display_Name", column.DisplayName);
                        db.AddParameters(4, "@Render", column.Render);
                        db.AddParameters(5, "@IsSummable", column.IsSummable);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    db.CommitTransaction();
                    return new OutputMessage("New report has been created as number " + identity, true, Entities.Type.NoError, "Settings | Reporting | Adhoc | Save", System.Net.HttpStatusCode.OK, identity);
                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. Could not create report", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Save", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public OutputMessage Update()
        {
            DBManager db = new DBManager();
            if (string.IsNullOrEmpty(this.ReportName))
            {
                return new OutputMessage("Report Name must not be empty", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrEmpty(this.ReportView) || this.ReportView == "--select--")
            {
                return new OutputMessage("Select a View for reporting", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.Columns.Count <= 0)
            {
                return new OutputMessage("Select some columns for creating report", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    string query = @"  update [dbo].[TBL_REPORT_MST] set [Report_Name]=@Report_Name,[Report_View]=@Report_View,[Report_View_Object_Id]=(select [object_id] from sys.views where name =@Report_View),[Default_Rows]=@Default_Rows,
                                        [Detail_Report_Id]=@Detail_Report_Id,[Primary_Key_Column]=@Primary_Key_Column,[Detail_Key_Column]=@Detail_Key_Column,[FilterAPI]=@FilterAPI,
                                        [Report_Type]=@Report_Type,[Status]=1,Description=@Description,modified_by=@modified_by,modified_date=getutcdate() where [Report_Id]=@report_id;
                                        delete from [TBL_REPORT_SCHEMA_RELATION] where [Report_Id]=@report_id;
                                        delete from [TBL_REPORT_FILTER_RELATION] where [Report_Id]=@report_id;";
                    db.CreateParameters(11);
                    db.AddParameters(0, "@Report_Name", this.ReportName);
                    db.AddParameters(1, "@Default_Rows", this.DefaultRows);
                    db.AddParameters(2, "@Detail_Report_Id", this.DetailReportId);
                    db.AddParameters(3, "@Primary_Key_Column", this.PrimaryKeyColumn);
                    db.AddParameters(4, "@Detail_Key_Column", this.DetailKeyColumn);
                    db.AddParameters(5, "@FilterAPI", this.FilterAPI);
                    db.AddParameters(6, "@Report_Type", this.Type);
                    db.AddParameters(7, "@Report_View", this.ReportView);
                    db.AddParameters(8, "@report_id", this.ReportId);
                    db.AddParameters(9, "@modified_by", this.ModifiedBy);
                    db.AddParameters(10, "@Description", this.Description);
                    db.Open();
                    int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    db.CleanupParameters();
                    foreach (ReportFilter filter in this.Filters)
                    {
                        query = @"insert into [dbo].[TBL_REPORT_FILTER_RELATION]  ([Filter_Name],[Label],[Report_Id],[Column_Name],[Column_Data_Type],[Type],[Control_Type],[Data_Text_Field],[Data_Value_Field])  values (@Filter_Name,@Label,@Report_Id,@Column_Name,@Column_Data_Type,@Type,@Control_Type,@Data_Text_Field,@Data_Value_Field)";
                        db.CreateParameters(9);
                        db.AddParameters(0, "@Filter_Name", filter.FilterName);
                        db.AddParameters(1, "@Label", filter.Label);
                        db.AddParameters(2, "@Report_Id", this.ReportId);
                        db.AddParameters(3, "@Column_Name", filter.ColumnName);
                        db.AddParameters(4, "@Type", filter.Type);
                        db.AddParameters(5, "@Control_Type", filter.ControlType);
                        db.AddParameters(6, "@Data_Text_Field", filter.DataTextField);
                        db.AddParameters(7, "@Data_Value_Field", filter.DataValueField);
                        db.AddParameters(8, "@Column_Data_Type", filter.ColumnDataType);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    db.CleanupParameters();
                    foreach (ReportSchema column in this.Columns)
                    {
                        query = @"insert into [dbo].[TBL_REPORT_SCHEMA_RELATION] ([Report_Id],[Column_Index],[Column_Name],[Display_Name],[Render],IsSummable)
                            values (@Report_Id,@Column_Index,@Column_Name,@Display_Name,@Render,@IsSummable)";
                        db.CreateParameters(6);
                        db.AddParameters(0, "@Report_Id", this.ReportId);
                        db.AddParameters(1, "@Column_Index", column.ColumnIndex);
                        db.AddParameters(2, "@Column_Name", column.ColumnName);
                        db.AddParameters(3, "@Display_Name", column.DisplayName);
                        db.AddParameters(4, "@Render", column.Render);
                        db.AddParameters(5, "@IsSummable", column.IsSummable);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    db.CommitTransaction();
                    return new OutputMessage("Report number " + this.ReportId + " has been updated ", true, Entities.Type.NoError, "Settings | Reporting | Adhoc | Update", System.Net.HttpStatusCode.OK, identity);
                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. Could not create report", false, Entities.Type.Others, "Settings | Reporting | Adhoc | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }

                finally
                {
                    db.Close();
                }
            }
        }


        public OutputMessage Delete(int id)
        {
            DBManager db = new DBManager();
            {

                try
                {
                    string query = "select ISNULL(is_system_defined,0) from TBL_REPORT_SCHEMA_RELATION where [Report_Id]=@report_id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@report_id", id);
                    db.Open();
                    bool isSystemDefined = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                    if (isSystemDefined)
                    {
                        return new OutputMessage("You cannot delete this Entry because it is system defined ", false, Entities.Type.ForeignKeyViolation, "reportingtool | Delete", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        query = "delete from [dbo].[TBL_REPORT_MST] where [Report_Id]=@report_id";
                        db.BeginTransaction();
                        db.ExecuteNonQuery(CommandType.Text, query);
                        query = "delete from TBL_REPORT_SCHEMA_RELATION where Report_Id=@report_id";
                        db.ExecuteNonQuery(CommandType.Text, query);

                        query = "delete from TBL_REPORT_FILTER_RELATION where Report_Id=@report_id";
                        db.ExecuteNonQuery(CommandType.Text, query);
                        db.CommitTransaction();
                        return new OutputMessage("Report has been deleted", true, Entities.Type.NoError, "Settings | Reporting | Adhoc  | Delete", System.Net.HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;

                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "PurchaseEntry | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Entities.Type.RequiredFields, "Settings | Reporting | Adhoc  | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }

        private string GetInitialQuery(int? DefaultRows)
        {
            StringBuilder sb = new StringBuilder("select ");
            if (DefaultRows != null)
            {
                sb.Append("top " + DefaultRows + " ");
            }
            if (this.Type == 1)
            {
                sb.Append("[" + this.DetailKeyColumn + "][hidden_detail_key], ");
            }
            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (i != this.Columns.Count - 1)
                {
                    sb.Append("[" + this.Columns[i].ColumnName + "][" + this.Columns[i].DisplayName + "], ");
                }
                else
                {
                    sb.Append("[" + this.Columns[i].ColumnName + "][" + this.Columns[i].DisplayName + "] ");
                }
            }
            sb.Append(" from [" + this.ReportView + "] where 1=1 ");
            if (DetailKeyValue != null && DetailKeyValue != 0)
            {
                sb.Append(" and [" + this.PrimaryKeyColumn + "] = " + this.DetailKeyValue + " ");
            }
            return sb.ToString();
        }

        public static DataTable GetViews()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                return db.ExecuteQuery(CommandType.Text, "  select name,[object_id] from sys.views");
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |  GetViews()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<object> GetFields(int ViewObjectId)
        {
            DBManager db = new DBManager();
            try
            {
                db.CreateParameters(1);
                db.AddParameters(0, "@objectid", ViewObjectId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select name,column_id from sys.columns where [object_id] =@objectid");
                List<object> result = new List<object>();
                foreach (DataRow item in dt.Rows)
                {
                    object field = new
                    {
                        Field = item["name"].ToString(),
                        ColumnId = Convert.ToInt32(item["column_id"])
                    };
                    result.Add(field);
                }
                return result;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |  GetFields(int ViewObjectId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static DataTable GetReports()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                return db.ExecuteQuery(CommandType.Text, "select Report_Id,Report_Name from TBL_REPORT_MST");
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |  GetReports()");
                return null;
            }
        }

        public static List<ReportingTool> GetDetails()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                List<ReportingTool> result = new List<ReportingTool>();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select * from [TBL_REPORT_MST]").Tables[0];
                if (dt != null)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        DataRow row = dt.Rows[i];
                        ReportingTool register = new ReportingTool();
                        register.ReportId = row["Report_Id"] != DBNull.Value ? Convert.ToInt32(row["Report_Id"]) : 0;
                        register.ReportName = Convert.ToString(row["Report_Name"]);
                        register.ReportView = Convert.ToString(row["Report_View"]);
                        register.ReportViewObjectId = row["Report_View_Object_Id"] != DBNull.Value ? Convert.ToInt32(row["Report_View_Object_Id"]) : 0;
                        register.DefaultRows = row["Default_Rows"] != DBNull.Value ? Convert.ToInt32(row["Default_Rows"]) : 0;
                        register.DetailReportId = row["Detail_Report_Id"] != DBNull.Value ? Convert.ToInt32(row["Detail_Report_Id"]) : 0;
                        register.PrimaryKeyColumn = Convert.ToString(row["Primary_Key_Column"]);
                        register.DetailKeyColumn = Convert.ToString(row["Detail_Key_Column"]);
                        register.Type = row["Report_Type"] != DBNull.Value ? Convert.ToInt32(row["Report_Type"]) : 0;
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        result.Add(register);
                    }
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ReportingTool |   GetDetails()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public ReportingTool GetDetails(int id)
        {
            using (DBManager db = new DBManager())
            {


                try
                {

                    db.Open();
                    string query = @"select * from [TBL_REPORT_MST] where report_id=@id;
                                   select * from TBL_REPORT_SCHEMA_RELATION where report_id=@id ; 
                                   select * from [TBL_REPORT_FILTER_RELATION] where report_id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", id);
                    ReportingTool register = new ReportingTool();
                    DataSet dt = db.ExecuteDataSet(System.Data.CommandType.Text, query);
                    if (dt != null)
                    {
                        DataRow row = dt.Tables[0].Rows[0];
                        register.ReportId = row["Report_Id"] != DBNull.Value ? Convert.ToInt32(row["Report_Id"]) : 0;
                        register.ReportName = Convert.ToString(row["Report_Name"]);
                        register.Description = Convert.ToString(row["Description"]);
                        register.ReportView = Convert.ToString(row["Report_View"]);
                        register.ReportViewObjectId = row["Report_View_Object_Id"] != DBNull.Value ? Convert.ToInt32(row["Report_View_Object_Id"]) : 0;
                        register.DefaultRows = row["Default_Rows"] != DBNull.Value ? Convert.ToInt32(row["Default_Rows"]) : 0;
                        register.DetailReportId = row["Detail_Report_Id"] != DBNull.Value ? Convert.ToInt32(row["Detail_Report_Id"]) : 0;
                        register.PrimaryKeyColumn = Convert.ToString(row["Primary_Key_Column"]);
                        register.DetailKeyColumn = Convert.ToString(row["Detail_Key_Column"]);
                        register.Type = row["Report_Type"] != DBNull.Value ? Convert.ToInt32(row["Report_Type"]) : 0;
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        List<ReportSchema> Schema = new List<ReportSchema>();
                        for (int j = 0; j < dt.Tables[1].Rows.Count; j++)
                        {
                            DataRow rowItem = dt.Tables[1].Rows[j];
                            ReportSchema schema = new ReportSchema();
                            schema.ReportSchemaId = rowItem["Report_Schema_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Report_Schema_Id"]) : 0;
                            schema.ColumnIndex = rowItem["Column_Index"] != DBNull.Value ? Convert.ToInt32(rowItem["Column_Index"]) : 0;
                            schema.ColumnName = Convert.ToString(rowItem["Column_Name"]);
                            schema.DisplayName = Convert.ToString(rowItem["Display_Name"]);
                            schema.Render = Convert.ToString(rowItem["Render"]);
                            schema.IsSummable = Convert.ToBoolean(rowItem["IsSummable"]);
                            Schema.Add(schema);

                        }
                        List<ReportFilter> Filter = new List<ReportFilter>();
                        for (int k = 0; k < dt.Tables[2].Rows.Count; k++)
                        {
                            DataRow rowItem1 = dt.Tables[2].Rows[k];
                            ReportFilter filter = new ReportFilter();
                            filter.ReportFilterId = rowItem1["Report_Filter_Id"] != DBNull.Value ? Convert.ToInt32(rowItem1["Report_Filter_Id"]) : 0;
                            filter.FilterName = Convert.ToString(rowItem1["Filter_Name"]);
                            filter.Label = Convert.ToString(rowItem1["Label"]);
                            filter.ColumnIndex = rowItem1["Column_Index"] != DBNull.Value ? Convert.ToInt32(rowItem1["Column_Index"]) : 0;
                            filter.ColumnName = Convert.ToString(rowItem1["Column_Name"]);
                            filter.ColumnDataType = Convert.ToString(rowItem1["Column_Data_Type"]);
                            filter.Type = rowItem1["Type"] != DBNull.Value ? Convert.ToInt32(rowItem1["Type"]) : 0;
                            filter.ControlType = rowItem1["Control_Type"] != DBNull.Value ? Convert.ToInt32(rowItem1["Control_Type"]) : 0;
                            filter.DataTextFieldId = rowItem1["Data_Text_Field_Id"] != DBNull.Value ? Convert.ToInt32(rowItem1["Data_Text_Field_Id"]) : 0;
                            filter.DataTextField = Convert.ToString(rowItem1["Data_Text_Field"]);
                            filter.DataValueFieldId = rowItem1["Data_Value_Field_Id"] != DBNull.Value ? Convert.ToInt32(rowItem1["Data_Value_Field_Id"]) : 0;
                            filter.DataValueField = Convert.ToString(rowItem1["Data_Value_Field"]);
                            Filter.Add(filter);

                        }


                        register.Filters = Filter;
                        register.Columns = Schema;
                        return register;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "ReportingTool | GetDetails(int id)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }

    }
}
