<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ledger.aspx.cs" Inherits="BisellsERP.Finance.Ledger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Ledger</title>
    <style>
        .rcorners1 {
            border-radius: 30px;
            padding: 20px;
            height: 150px;
        }
    </style>
    <script>
        $(document).ready(function () {

            var apiurl = $('#hdApiUrl').val();


            //Initilizing Select2
            $('#ddlTransHead').select2({
                width:"100%"
            });



            //Date picker Initilization
            $('#txtToDate,#txtFromDate').datepicker({
                todayHighlight: true,
                autoclose: true,
                format: 'dd-M-yyyy',
                todayBtn: "linked"
            });

            //button event to show the filter div
            $('.show-filter').click(function () {
                $('#dvfilters').fadeIn(500);
                $('#dvfilters').removeClass('hidden')
                $('.hide-filter').removeClass('hidden')
                $('.show-filter').addClass('hidden');
            });

            //Button event to hide filter div
            $('body').on('click', '.hide-filter', function () {
                $('#dvfilters').addClass('hidden')
                $('.show-filter').removeClass('hidden');
                $('.hide-filter').addClass('hidden');
            });

            //Main account head drop down change event
            $('#ddlTransHead').change(function () {
                var Company = $.cookie("bsl_1");
                var Head = $('#ddlTransHead').val();
                var FromDate = new Date($('#txtFromDate').val());
                var ToDate = new Date($('#txtToDate').val());
                console.log(FromDate + ',' + ToDate);
                if (FromDate <= ToDate) {
                    $.ajax({
                        url: apiurl + '/api/FinancialTransactions/GetAccountHeadsChild?HeadID=' + Head,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify(Company),
                        success: function (response) {
                            console.log(response);
                            if (response.length > 0) {
                                //If main account has child heads then enables child-dropdown list and loads the value to dropdown
                                $('#childDiv').fadeIn('slow');
                                $('#childDiv').removeClass('hidden');
                                $('#ddlChildHead').children('option').remove();
                                $('#ddlChildHead').append('<option value="0">--Select--</option>');
                                for (var i = 0; i < response.length; i++) {
                                    $('#ddlChildHead').append('<option value=' + response[i].parent + '>' + response[i].name + '</option>');
                                }
                                $('#hiddenTableName').val(response[0].Fah_SQLTable);
                                $('#ddlChildHead').select2({
                                    width:"100%"
                                });
                            }
                            else {
                                $('#childDiv').fadeOut('slow');
                                $('#childDiv').addClass('hidden');
                                $('#ddlChildHead').children('option').remove();
                                $('#ddlChildHead').append('<option value="0">--Select--</option>');
                                $('#hiddenTableName').val("");
                            }

                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }
                else {
                    errorAlert('To Date is Greater than From Date');
                }
            });

            //Filters the report with new cretirea. 
            $('#cmdSearch').click(function () {
                var child = "0";
                var mainHead = "0";
                var FromDate = "";
                var ToDate = "";
                var CostCenter = "";
                child = $('#ddlChildHead').val();
                mainHead = $('#ddlTransHead').val();
                FromDate = $('#txtFromDate').val();
                ToDate = $('#txtToDate').val();
                CostCenter = $('#ddlCostCenter').val();
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/FinancialTransactions/GetLedgerReport?ChildHead=' + child + '&MainHead=' + mainHead + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '&CostCenter=' + CostCenter,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(Company),
                    success: function (response) {
                        console.log(response);
                        $('#tblLedger').children().remove();
                        $('#tblLedger').append(response);
                        $('#lblDate').text('From ' + FromDate + ' to ' + ToDate);
                        if (child != "0") {
                            if (mainHead != "0") {
                                $('#lblLedgerHead').text($('#ddlChildHead :selected').text());
                            }
                        }
                        else {
                            if (mainHead != "0") {
                                $('#lblLedgerHead').text($('#ddlTransHead :selected').text());
                            }
                            else {
                                $('#lblLedgerHead').text("Cash");
                            }

                        }

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Export button Event
            $("#btnExport").click(function (e) {
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel;charset=utf-8';

                var table_html = $('#dvData')[0].outerHTML;
                table_html = table_html.replace(/<tfoot[\s\S.]*tfoot>/gmi, '');

                var css_html = '<style>td {border: 0.5pt solid #c0c0c0} .tRight { text-align:right} .tLeft { text-align:left} </style>';

                a.href = data_type + ',' + encodeURIComponent('<html><head>' + css_html + '</' + 'head><body>' + table_html + '</body></html>');

                //setting the file name
                a.download = $('#lblLedgerHead').text()+' ' + $('#lblDate').text() + '.xls';
                a.click();
                e.preventDefault();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="col-sm-8">
                <h3 class="pull-left page-title text-default m-b-0" id="lblMainhead" style="margin-top: -10px;">Ledgers&nbsp;</h3>
            </div>
            <div class="col-sm-4">
                <button type="button" class="btn btn-default waves-effect waves-light show-filter pull-right">Show Filter</button>
                <button type="button" class="btn btn-default waves-effect waves-light hide-filter hidden pull-right">Hide Filter</button>
                <button type="button" class="btn btn-default waves-effect waves-light pull-right m-r-5" clientidmode="Static" id="btnExport" title="Export to Excel"><i class="fa fa-file-excel-o"></i>&nbsp;Export to excel</button>
            </div>
        </div>
    </div>
    <div class="row m-t-10">
        <div class="panel panel-default hidden rcorners1" id="dvfilters">
            <div class="panel-body">
                <div class="col-md-2">
                    <label class="control-label">Head</label>
                    <asp:DropDownList ID="ddlTransHead" runat="server"  ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 hidden" id="childDiv">
                    <label class="control-label">Child Head</label>
                    <asp:DropDownList ID="ddlChildHead" runat="server" ClientIDMode="Static">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="control-label">Cost Center</label>
                    <asp:DropDownList ID="ddlCostCenter" runat="server" ClientIDMode="Static" CssClass="form-control">
                        <asp:ListItem Value="0">--select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="control-label">From</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtFromDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    </div>
                </div>
                <div class="col-md-2">
                    <label class="control-label">To</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtToDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    </div>
                </div>
                <div class="col-md-2 pull-right m-t-25">
                    <div class="col-sm-12">
                        <%--<asp:LinkButton ID="cmdSearch" runat="server" CssClass="btn btn-info" Text="Search"
                            ToolTip="Search" ClientIDMode="Static" />--%>
                        <button type="button" id="cmdSearch" class="btn btn-info pull-right" title="Search" clientidmode="Static">Apply Filters</button>
                    </div>
                    <%--<div class="col-sm-6">
                        <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-info hidden" Text="Export"
                            ToolTip="Export" ClientIDMode="Static" />
                    </div>--%>
                </div>
                <div class="col-lg-12 hidden" style="text-align: center">
                    <asp:Button ID="cmdPrint" runat="server" CssClass="btn btn-primary" Text="Print"
                        ToolTip="Print" OnClick="cmdPrint_Click" Visible="False" />
                    <asp:Button ID="cmdBack" runat="server" CssClass="btn btn-primary"
                        Visible="false" Text="Back" ToolTip="Back" OnClick="cmdBack_Click" />
                    <asp:Button ID="cmdConsoleLedger" runat="server" CssClass="btn btn-primary" OnClick="cmdConsoleLedger_Click"
                        Text="View Console Ledger" Visible="False" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body" id="dvData">
                <div class="col-sm-12" style="text-align: center">
                    <h4>
                        <label id="lblLedgerHead" class="page-title text-default" runat="server" clientidmode="static"></label>
                    </h4>
                    <label id="lblDate" runat="server" class="m-b-10" clientidmode="static"></label>
                </div>
                <table class="table table-hover table-bordered no-footer" id="tblLedger">
                    <asp:Literal ID="literalGrid" runat="server" ClientIDMode="Static"></asp:Literal>
                </table>
            </div>
        </div>
    </div>
    <div class="hidden">
        <asp:TextBox ID="rowCostHead" runat="server"></asp:TextBox>
        <input id="hiddenEmpId" name="hiddenEmpId" style="width: 25px" type="hidden" runat="server" />
        <input id="hiddenObjId" name="hiddenObjId" style="width: 25px" type="hidden" runat="server" /><input
            id="hiddenHeadID" name="hiddenHeadID" style="width: 25px" type="hidden" runat="server" />
        <asp:HiddenField ID="hiddenChildId" Value="0" runat="server" ClientIDMode="Static" />
        <input id="hiddenDate" name="hiddenDate" style="width: 25px" type="hidden" runat="server" /><input
            id="hiddenDataSQL" runat="server" style="width: 25px" type="hidden" /><input id="hiddenDataSQLName"
                runat="server" style="width: 25px" type="hidden" /><input id="hiddenDataSQLId" runat="server"
                    style="width: 25px" type="hidden" /><input id="hiddenDataSQLTable" runat="server"
                        style="width: 25px" type="hidden" />
        <input id="hiddenCompanyId" runat="server" style="width: 25px" type="hidden" />
        <asp:HiddenField ID="hiddenTableName" runat="server" Value="0" ClientIDMode="Static" />
        <asp:Label ID="messageLabel" runat="server" CssClass="operationFailed" ClientIDMode="Static"></asp:Label>
        <asp:Button ID="Button1" runat="server" Visible="False" ClientIDMode="Static" />
    </div>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
  
        <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
