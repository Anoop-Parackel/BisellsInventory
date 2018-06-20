<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="BisellsERP.Sales.Invoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sales Invoices</title>
    <style>
        #wrapper {
            overflow: hidden;
        }
        .searchDropdown {
            width: 200px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Invoices</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Left sidebar -->
        <div class="left-sidebar">
            <div class="col-md-12">
                <div class="list-group m-b-0">
                    <div class="list-group-item active m-b-10">
                        <div class="btn-group">
                            <button type="button" class="trans-btn dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                All Sales Invoices &nbsp;<b id="countOfInvoices">(0)</b>&nbsp;<span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu">
                                <li><a href="#">Draft</a></li>
                                <li><a href="#">Client Viewed</a></li>
                                <li><a href="#">Partially Paid</a></li>
                            </ul>
                        </div>
                        <input type="text" class="form-control listing-search-entity" placeholder="Seach invoice number..."/>

                        <a id="searchInvoice" href="#" class="pull-right"><i class="fa fa-search filter-list"></i></a>
                        <div class="pull-right t-y search-group">
                            <span class="filter-span">
                                <label>Filter by Date</label>
                                <input type="text" id="txtDate" name="daterange" value="01/01/2015 - 01/31/2015" />
                            </span>
                            <span class="filter-span">
                                <label>Filter by Customer</label>
                                <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown cust" runat="server"></asp:DropDownList>
                            </span>
                        </div>
                    </div>
                    <div class="left-side">
                        <div class="panel m-b-0">
                            <div class="panel-body">

                                <table id="invoiceList" class="table left-table invoice-list">
                                    <thead>
                                        <tr>
                                            <th style="display: none">Id</th>
                                            <th class="show-on-collapsed">Invoice Number</th>
                                            <th class="show-on-collapsed">Date</th>
                                            <th >Customer</th>
                                            <th style="text-align:right">Tax</th>
                                            <th style="text-align:right">Gross</th>
                                            <th class="show-on-collapsed" style="text-align:right">Net</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                                <div class="empty-state-wrap" style="display: none">
                                    <img class="empty-state-icon" src="../Theme/images/empty_state.png" />
                                    <h4 class="empty-state-title">Nothing to show</h4>
                                    <p class="empty-state-text">Oooh oh, there are no results that match your filters. Refactor filters</p>
                                    <div class="btn-group empty-state-buttons">
                                        <a href="/Sales/Entry?MODE=new" class="btn btn-default btn-xs btn-rounded waves-effect">Create New Invoice</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Left sidebar -->
        </div>

        <!-- Right Sidebar -->
        <div class="right-sidebar" style="display: none">
            <div class="col-md-8">
                <div class="row">
                    <div class="col-sm-7">
                        <div class="btn-toolbar m-t-5" role="toolbar">
                            <div class="btn-group">
                                <button type="button" class="btn btn-default waves-effect waves-light edit-register" title="edit"><i class="md md-mode-edit"></i></button>
                                <button type="button" accesskey="p" class="btn btn-default waves-effect waves-light print-register" title="print"><i class="md  md-print"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light email-register" title="mail" data-toggle="modal" data-target="#modalMail"><i class="md  md-mail"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light delete-register" title="delete"><i class="fa fa-trash-o"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light dropdown-toggle" data-toggle="dropdown" aria-expanded="false">More&nbsp;<span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li class="clone-register"><a href="#">Clone Bill</a></li>
                                    <li><a href="#" class="change-despatch">Alter Despatch</a></li>
                                    <li><a href="#" class="print-delivery">Print Delivery Note</a></li>
                                </ul>
                            </div>

                            <div class="btn-group hidden">
                                <button type="button" class="btn btn-default waves-effect waves-light dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="md-cloud-download"></i>&nbsp;Export&nbsp;<span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li class="clone-register"><a href="#">PDF</a></li>
                                    <li><a href="#">Image</a></li>
                                    <li><a href="#">Excel</a></li>
                                    <li><a href="#">Word</a></li>
                                </ul>
                            </div>

                            <div class="btn-group">
                                <button type="button" class="btn btn-default waves-effect waves-light new-register"><i class="md md-add-circle-outline"></i>&nbsp;New</button>
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-default waves-effect waves-light recordPayment-register" data-toggle="modal" data-target="#paymentContent">Receipt</button>
                            </div>
                             <div class="btn-group">

                                <button type="button" class="btn btn-default waves-effect waves-light view-receipts-register">View All Receipts</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="despatch-wrap" style="display: none;">
                            <label>Change Despatch</label>
                            <asp:DropDownList ID="ddlDespatch" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                            <button type="button" class="btn btn-icon waves-effect waves-light btn-green despatch-update"><i class="fa fa-check"></i></button>
                            <button type="button" class="btn btn-icon waves-effect waves-light btn-inverse despatch-close"><i class="fa fa-close"></i></button>
                        </div>

                    </div>
                    <div class="col-sm-1">
                        <div class="btn-toolbar m-t-0" role="toolbar">
                            <div class="btn-group pull-right">
                                <a href="#" class="close-sidebar"><i class="md md-cancel"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End row -->

                <div class="panel panel-default m-t-15 right-side">
                    <div class="ribbon success" style="display:none" id="paymentStatus"><i class="md md-notifications"></i>&nbsp;<span id="paymentStatusText"></span></div>
                    <div class="panel-body">
                        <div class="print-wrapper">
                            <div class="row">
                                <div class="">
                                    <%-- INVOICE DETAILS --%>
                                    <div class="row" style="margin-top: 15px;">
                                        <input id="hdRegisterId" type="hidden" value="0" />
                                        <input id="hdEmail" type="hidden" value="0" />
                                        <div class="col-xs-4">
                                            <address>
                                                <b>
                                                    <small class="text-muted">Received by </small><small id="lblCustName" class="underline"></small>
                                                </b>
                                                <br />
                                                <small id="lblCustAddr1"></small>
                                                <br />
                                                <small id="lblCustAddr2"></small>
                                                <br />
                                                <small id="lblCustState"></small>
                                                <br />
                                                <small id="lblCustCountry"></small>
                                                <br />
                                                ☎ :<small id="lblCustphone"></small><br />
                                                <b>TRN No :  </b>
                                                <small id="lblCustTaxNo"></small>
                                            </address>
                                        </div>
                                        <div class="col-xs-4">
                                            <address>
                                                <b>
                                                    <small class="text-muted">Delivered from </small><small id="lblLocName" class="underline"></small>
                                                </b>
                                                <br />
                                                <small id="lblLocAddr1"></small>
                                                <br />
                                                <small id="lblLocAddr2"></small>
                                                <br />
                                                ☎ :<small id="lblLocPhone"></small><br />
                                                <b>TRN No : </b>
                                                <small id="lblLocRegId"></small>

                                            </address>
                                        </div>
                                        <div class="col-xs-4 text-right invoice-info">
                                            <p>
                                                <span>Dated<span class="pull-right">:</span></span>
                                                <b id="lblDate"></b>
                                            </p>
                                            <p class="m-0">
                                                <span>Invoice No<span class="pull-right">:</span></span>
                                                <b id="lblInvoiceNo"></b>
                                            </p>

                                            <p class="m-0">
                                                <span>Invoice Total<span class="pull-right">:</span></span>
                                                <b class="currency-only"></b><b class="label-net"></b>
                                            </p>
                        
                                        </div>
                                    </div>
                                    <%-- INVOICE ITEMS --%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <table id="listTable" class="table table-bordered m-t-20">
                                                    <thead>
                                                        <tr>
                                                            <th>Item Code</th>
                                                            <th>Item Name</th>
                                                            <th style="text-align:right;">Mrp</th>
                                                            <th style="text-align:right;">Rate</th>
                                                            <th style="text-align:right;">Qty</th>
                                                            <th style="text-align:right">Gross</th>
                                                            <th style="text-align:right;">Tax %</th>
                                                            <th style="text-align:right;">Tax</th>
                                                            <th style="text-align:right;">Net</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- INVOICE SUMMARY --%>
                                    <div class="row">
                                        <div class="col-xs-4 col-xs-offset-8 text-right invoice-summary">
                                            <p class="m-b-0">
                                                <span>Sub-total<span class="pull-right">:</span></span>
                                                <b id="lblTotal"></b>
                                            </p>
                                            <p class="m-0">
                                                <span>Tax Amount<span class="pull-right">:</span></span>
                                                <b id="lblTax"></b>
                                            </p>
                                            <p class="m-0">
                                                <span>Round Off<span class="pull-right">:</span></span>
                                                <b id="lblroundOff"></b>
                                            </p>
                                               <p class="m-0">
                                                <span>Discount<span class="pull-right">:</span></span>
                                                <b id="lblDiscount"></b>
                                            </p>
                                              <p class="m-0">
                                                <span>Freight Amount<span class="pull-right">:</span></span>
                                                <b id="lblFreight"></b>
                                            </p>
                                            <h4 style="border-top: 1px solid #ccc;">
                                                <span class="currency currency-only">$</span>&nbsp;
                                                <b id="lblNet"></b>
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- panel body -->
                </div>
                <!-- panel -->
            </div>
            <!-- End Right sidebar -->
        </div>
    </div>
    <div id="paymentContent" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">

            <!-- Modal content-->
            <div class="modal-content">

                <div class="modal-header">
                    <div class="row">
                        <div class="col-xs-6">
                            <h4 class="modal-title" id="myModalLabel">Reciept for <b>
                                <asp:Label ID="lblCustomer" ClientIDMode="Static" runat="server"></asp:Label></b></h4>
                        </div>
                        <div class="col-xs-6 text-right">
                            <div class="form-horizontal">
                                <label class="col-xs-6 p-t-5 control-label">Payment Mode</label>
                                <div class="col-xs-6 p-r-0">
                                    <asp:DropDownList ID="ddlPayment" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                        <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-body p-0">
                    <div class="row before-send p-t-25">
                        <input id="hdCustomerId" type="hidden" value="0" />
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Date</label>
                                    <input type="text" class="form-control" id="txtPayDate" value="01/01/2015 - 01/31/2015" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Amount</label>
                                    <asp:TextBox ID="txtAmount" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="" id="hiddenDiv">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Bank</label>
                                    <asp:DropDownList ID="ddlPaymentBank" runat="server" ClientIDMode="Static" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Cheque Date</label>
                                    <input type="text" class="form-control" id="txtChDate" value="01/01/2015 - 01/31/2015" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Cheque No</label>
                                    <asp:TextBox ID="txtChequeNo" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Draw On</label>
                                    <asp:TextBox ID="txtDraw" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-12">
                                <label class="control-label">Note : </label>
                                <textarea id="txtNote" class="form-control"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-12 m-t-15">
                            <div class="btn-toolbar pull-right">
                                <button id="btnSave" type="button" class="btn btn-default m-t-5"><i class="md-done"></i>&nbsp;Save</button>
                                <button type="button" id="btnCancel" class="btn btn-inverse m-t-5" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div id="modalMail" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">

            <!-- Modal content-->
            <div class="modal-content m-10">
                <div class="modal-body p-0">
                    <div class="row">
                        <div class="before-send">
                            <h2 class="text-center">Send Mail?</h2>
                            <p class="text-center">Are you sure you want to send mail?</p>
                            <div class="col-sm-6 col-sm-offset-3">
                                <input id="txtMailAddress" type="email" placeholder="Enter email address..." class="form-control m-t-10" />
                            </div>
                            <div class="col-sm-12 text-center m-t-20">
                                <button id="btnSend" type="button" class="btn btn-default m-t-5">Send&nbsp;<i class="md-done"></i></button>
                                <button type="button" class="btn btn-inverse m-t-5" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                        <div class="after-send" style="display: none;">
                            <div class="col-sm-1 m-t-15">
                                <i class="fa fa-warning fa-2x text-muted"></i>
                            </div>
                            <div class="col-sm-7">
                                <p class="text-muted">This may take a while. You can run this in the background too. You will be notified once the mail is sent.</p>
                            </div>
                            <div class="col-sm-4">
                                <button type="button" class="btn btn-blue btn-block m-t-5" data-dismiss="modal">Run in Background</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
     <%--Modal for showing all receipts--%>
    <div id="modalReceipts" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">

            <!-- Modal content-->
            <div class="modal-content m-10">
                <div class="modal-body p-0">
                    <div class="row">
                        <div class="before-send">
                            <h2 class="text-center">xx</h2>
                            <div class="col-sm-12">
                                <table id="tblAllReceipts" class="table table-bordered table-condensed table-hover table-striped">
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Net Amount</th>
                                            <th>Paid</th>
                                            <th style="text-align:center"><i class="md md-print"></i></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>30 dec 2017</td>
                                            <td>1250</td>
                                            <td>1000</td>
                                            <td><a>print</a></td>
                                        </tr>
                                        <tr>
                                            <td>30 dec 2017</td>
                                            <td>1250</td>
                                            <td>250</td>
                                            <td><a href="#">print</a></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-sm-12 text-center m-t-20">
                                <button type="button" class="btn btn-inverse m-t-5" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>

    <!-- Date Range Picker -->

    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
    <script>
        $(document).ready(function () {

            $("#hiddenDiv").hide();

            $('#ddlPayment').on('change', function () {
                if (this.value == 'Bank') {
                    $("#hiddenDiv").show();
                }
                else {
                    $("#hiddenDiv").hide();
                }
            });

            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtPayDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });
            $('#txtPayDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtPayDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtPayDate').datepicker('hide');
           });

            $('#txtChDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });
            $('#txtChDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtChDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtChDate').datepicker('hide');
           });
            $('#txtIssueDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });
            $('#txtIssueDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtIssueDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtIssueDate').datepicker('hide');
           });

            $.ajax({
                url: $('#hdApiUrl').val() + 'api/SalesEntry/GetCompanyDetails/',
                method: 'POST',
                contentType: 'application/json;charset=utf-8',
                dataType: 'JSON',
                data: JSON.stringify($.cookie('bsl_2')),
                success: function (data) {
                    console.log(data);
                    $('#lblCompRegId').text(data.RegId1);
                    $('#lblComp').text(data.Name);
                    $('#lblCompAddr1').text(data.Address1);
                    $('#lblCompAddr2').text(data.Address2);
                    $('#lblCompPh').text(data.MobileNo1);
                    $('#lblCompCity').text(data.City);
                    $('#lblCompCountry').text(data.Country);
                    $('#lblCompState').text(data.State);
                    $("#imgLogo").prop('src', 'data:image/jpeg;base64,' + data.LogoBase64)
                },
                error: function (err) {
                    alert(err.responseText);
                }
            });

            $('#btnSave').click(function () {
                    Save();
            });
            $('#btnCancel').click(function () {
                ResetModalPayment();
            });
            $('.recordPayment-register').click(function () {
                ResetModalPayment();
            });
            function ResetModalPayment() {

                $('#ddlPayment').val('Cash').trigger('change');
                var date = new Date();
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#txtPayDate').datepicker('setDate', today);
                $('#txtChDate').datepicker('setDate', today);
                $('#txtChequeNo').val('');
                $('#txtNote').val('');
                $('#txtDraw').val('');
                $('#paymentContent').modal("hide");

            }

            function Save() {
                var SelectedBank = $('#ddlPaymentBank').val();
                var modeofPay = $('#ddlPayment').val();
                if (SelectedBank == 0 && modeofPay == "Bank") {
                    errorAlert('Please select a bank');
                }
                else {
                    var data = {};
                    var date = $('#txtPayDate').val();
                    var mode = $('#ddlPayment').val();
                    var chequeDate = $('#txtChDate').val();
                    var chequeNo = $('#txtChequeNo').val();
                    var CompanyId = $.cookie('bsl_1');
                    var FinancialYear = $.cookie('bsl_4');
                    var amount = $('#txtAmount').val();
                    var note = $('#txtNote').val();
                    var draw = $('#txtDraw').val();
                    var party = $('#hdCustomerId').val();
                    data.PayBank = $('#ddlPaymentBank').val();
                    data.Date = date;
                    data.ChequeDate = chequeDate;
                    data.ChequeNumber = chequeNo;
                    data.DrawOn = draw;
                    data.Narration = note;
                    data.PayingAmount = amount;
                    data.PartyId = party;
                    data.PayBy = mode;
                    data.UserId = $.cookie('bsl_3');
                    var Bills = [];
                    var bill = {};
                    bill.BillNo = $('#hdRegisterId').val();
                    bill.Vouchers = [];
                    var voucher = {};
                    voucher.Amount = amount;
                    voucher.Id = 0;
                    bill.Vouchers.push(voucher);

                    Bills.push(bill);
                    data.Bills = Bills;

                    console.log(data);
                    $.ajax({
                        url: $(hdApiUrl).val() + 'api/SettleBills/SettleBillsCustomer',
                        method: 'POST',
                        data: JSON.stringify(data),
                        contentType: 'application/json',
                        dataType: 'JSON',
                        success: function (response) {
                            if (response.Success) {
                                successAlert(response.Message);
                                ResetModalPayment();
                                $('#hdCustomerId').val('');
                                $('#txtAmount').val('');
                                $('.left-side table tbody tr.active').trigger("click");

                            }
                            else {
                                errorAlert(response.Message);
                            }
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }
            }

            //Show Change Despatch
            $('.change-despatch').on('click', function () {
                $('.despatch-wrap').fadeIn('slow');
            });
            //Hide Change Despatch
            $('.despatch-close').on('click', function () {
                $('.despatch-wrap').fadeOut('slow');
            });


            $(".left-side").niceScroll({
                cursorcolor: "#6d7993",
                cursorwidth: "8px"
            });
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            //$('.currency').html(currency);
            $('.currency-only').html(currency);

            $(document).on('click', '.filter-list', function () {
                var customerId = $('#ddlCustomer').val() == '0' ? null : $('#ddlCustomer').val();
                var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                refreshTable(customerId, fromDate, toDate);
            })
            //get list of invoices
            refreshTable(null, null, null);

            function refreshTable(customerId, fromDate, toDate) {

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/SalesEntry/Get?CustomerId=' + customerId + '&from=' + fromDate + '&to=' + toDate,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (data) {
                        if (data == null || data.length <= 0) {
                            $('#invoiceList tbody').children().remove();
                            $('.empty-state-wrap').show();
                            return;

                        }
                        else {
                            $('.empty-state-wrap').hide();
                        }
                        var html = '';
                        var count = 0;
                        $(data).each(function (index) {
                            count++;
                            html += '<tr>';
                            html += '<td style="display:none">' + this.ID + '</td>';
                            html += '<td class="show-on-collapsed">' + this.SalesBillNo + '</td>';
                            html += '<td class="show-on-collapsed">' + this.EntryDateString + '</td>';
                            html += '<td>' + this.Customer + '</td>';
                            html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                            html += '<td style="text-align:right">' + this.Gross + '</td>';
                            html += '<td class="show-on-collapsed" style="text-align:right">' + this.NetAmount + '</td>';
                            html += '</tr>';
                        });
                        $('#invoiceList tbody').children().remove();
                        $('#countOfInvoices').text('(' + count + ')');
                        $('#invoiceList tbody').append(html);
                        //$('#invoiceList').dataTable({
                        //    responsive: true,
                        //    paging: false,
                        //    ordering: false,
                        //    searching: false,
                        //    bInfo: false
                        //});
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); }
                });

            }

            $('input[name="daterange"]').daterangepicker({
                "opens": "left",
                "startDate": moment().startOf('month'),
                "endDate": moment().endOf('month'),
                "alwaysShowCalendars": false,
                locale: {
                    format: 'DD MMM YYYY'
                },
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            });

            //Show right sidebar when invoice is clicked
            $(document).on('click', '.left-side table tbody tr', function () {
                $('.left-sidebar').find('[class="col-md-12"]').removeClass('col-md-12').addClass('col-md-4 collapsed');
                $('.left-side table tbody').find('.active').removeClass('active');
                $('.filter-list').removeClass('fa-search').addClass('fa-angle-right');
                $('.search-group').hide();
                $(".left-side").getNiceScroll().resize();
                $(this).addClass('active');

                var id = $(this).children('td').eq(0).text();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/SalesEntry/get/' + id,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (data) {
                        $('#hdRegisterId').val(data.ID);
                        $('#hdEmail').val(data.BillingAddress[0].Email);
                        $('#lblCustName').text(data.Customer);
                        $('#lblCustAddr1').text(data.BillingAddress[0].Address1);
                        $('#lblCustAddr2').text(data.BillingAddress[0].Address2);
                        $('#lblCustphone').text(data.BillingAddress[0].Phone1);
                        switch (data.PaymentStatus) {
                            case 0:
                                $('#paymentStatus').removeClass('success warning danger');
                                $('#paymentStatus').addClass('success');
                                $('#paymentStatus').show();
                                $('#paymentStatusText').text("Paid");
                                $('.recordPayment-register').addClass('hidden');
                                $('.view-receipts-register').removeClass('hidden');
                                break;
                            case 1:
                                $('#paymentStatus').removeClass('success warning danger');
                                $('#paymentStatus').addClass('warning');
                                $('#paymentStatus').show();
                                $('#paymentStatusText').text("Partially Paid");
                                $('.recordPayment-register').removeClass('hidden');
                                $('.view-receipts-register').removeClass('hidden');
                                break;
                            case 2:
                                $('#paymentStatus').removeClass('success warning danger');
                                $('#paymentStatus').addClass('danger');
                                $('#paymentStatus').show();
                                $('#paymentStatusText').text("Due");
                                $('.recordPayment-register').removeClass('hidden');
                                $('.view-receipts-register').addClass('hidden');
                                break;
                            case 3:
                                $('#paymentStatus').removeClass('success warning danger');
                                $('#paymentStatus').addClass('danger');
                                $('#paymentStatus').show();
                                $('#paymentStatusText').text("Over Due");
                                $('.recordPayment-register').removeClass('hidden');
                                $('.view-receipts-register').addClass('hidden');
                                break;
                            default:
                                $('#paymentStatus').hide();
                                $('#paymentStatusText').text("");
                                $('.recordPayment-register').addClass('hidden');
                        }
                        $('#lblLocName').text(data.Location);
                        $('#lblCustState').text(data.BillingAddress[0].State);
                        $('#lblCustCountry').text(data.BillingAddress[0].Country);
                        $('#lblLocRegId').text(data.LocationRegId);
                        $('#lblCustTaxNo').text(data.CustomerTaxNo);
                        $('#ddlDespatch').val(data.DespatchId);
                        $('#ddlDespatch').trigger('change');
                        $('#lblLocAddr1').text(data.LocationAddress1);
                        $('#lblDiscount').text(data.Discount);
                        $('#lblFreight').text(data.FreightAmount);
                        $('#lblLocAddr2').text(data.LocationAddress2);
                        $('#lblLocPhone').text(data.LocationPhone);
                        $('#lblDate').text(data.EntryDateString);
                        $('#lblInvoiceNo').text(data.SalesBillNo);
                        $('#lblCustomer').text(data.Customer);
                        $('#txtAmount').val(data.BalanceAmount);
                        $('#hdCustomerId').val(data.CustomerId);
                        var html = '';

                        $(data.Products).each(function (index) {

                            html += '<tr>';
                            html += '<td>' + this.ItemCode + '</td>';
                            html += '<td><b>' + this.Name + '</b><br/><i>' + this.Description + '</i></td>';
                            html += '<td style="text-align:right;">' + this.MRP + '</td>';
                            html += '<td style="text-align:right;">' + this.SellingPrice + '</td>';
                            html += '<td style="text-align:right;">' + this.Quantity + '</td>';
                            html += '<td style="text-align:right;">' + this.Gross + '</td>';
                            html += '<td style="text-align:right;">' + this.TaxPercentage + '</td>';
                            html += '<td style="text-align:right;">' + this.TaxAmount + '</td>';
                            html += '<td style="text-align:right;">' + this.NetAmount + '</td>';
                            html += '</tr>';

                        });
                        $('#listTable tbody').children().remove();
                        $('#listTable tbody').append(html);
                        $('#lblTotal').text(data.Gross);
                        $('#lblTax').text(data.TaxAmount);
                        $('#lblroundOff').text(data.RoundOff);
                        $('#lblNet').text(data.NetAmount);
                        $('.label-net').text(data.NetAmount);
                        console.log(data);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },

                });


                $('.right-sidebar').fadeOut();
                $('.right-sidebar').fadeIn();
            });

            $(document).on('click', '.edit-register', function () {
                window.open('/Sales/Entry?MODE=edit&UID=' + $('#hdRegisterId').val(), '_self');
            });
            $(document).on('click', '.print-register', function () {
                var type = JSON.parse($('#hdSettings').val()).TemplateType;
                var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                PopupCenter('/Sales/Prints/' + type + '/' + number + '/Entry?id=' + $('#hdRegisterId').val() + '&location=' + $.cookie('bsl_2'), 'salesEntry', 800, 700);
            });
            $(document).on('click', '.clone-register', function () {
                window.open('/Sales/Entry?MODE=clone&UID=' + $('#hdRegisterId').val(), '_self');
            });
            $(document).on('click', '.new-register', function () {
                window.open('/Sales/Entry?MODE=new', '_self');
            });

            $(document).on('click', '.print-delivery', function () {
                var type = JSON.parse($('#hdSettings').val()).TemplateType;
                var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                var url = "/sales/Prints/" + type + "/" + number + "/Entry?id=" + $('#hdRegisterId').val() + "&location=" + $.cookie('bsl_2') + "&invoicetype=delivery";
                PopupCenter(url, 'salesEntry', 800, 700);
            });

            $("#modalMail").on('hidden.bs.modal', function () {
                $('.before-send').show();
                $('.after-send').hide();
                $('#txtMailAddress').val('');
            });

            //Update Change Despatch
            $('.despatch-update').on('click', function () {

                var id = $('#hdRegisterId').val();
                var despatchId = $('#ddlDespatch').val();
                var data = {};
                data.ID = id;
                data.DespatchId = despatchId;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/SalesEntry/updateDespatch',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            successAlert(response.Message);
                            $('.despatch-wrap').fadeOut('slow');

                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        complete: loading('stop', null);
                    },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });

            });


            $(document).on('click', '.email-register', function (e) {
                $('#txtMailAddress').val($('#hdEmail').val());

            });

            $(document).on('click', '.delete-register', function () {
                if ($('#hdRegisterId').val() != 0) {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    }, function (isConfirm) {
                        var customerId = $('#ddlCustomer').val() == '0' ? null : $('#ddlCustomer').val();
                        var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                        var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                        var id = $('#hdRegisterId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/SalesEntry/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        $('.md-cancel').trigger('click');
                                        refreshTable(customerId, fromDate, toDate);
                                    }
                                    else {
                                        errorAlert(response.Message);
                                    }
                                },
                                error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                            });
                        }
                    });
                }
            });

            $(document).on('click', '#btnSend', function (e) {
                $('.before-send').fadeOut('slow', function () {
                    $('.after-send').fadeIn();
                    var toaddr = $('#txtMailAddress').val();
                    var id = $('#hdRegisterId').val();
                    var modifiedBy = $.cookie('bsl_3');
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SalesEntry/SendMail?salesid=' + id + '&toaddress=' + toaddr + '&userid=' + $.cookie('bsl_3'),
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify($(location).attr('protocol') + '//' + $(location).attr('host') + '/sales/Prints/' + type + '/' + number + '/entry?id=' + id + '&location=' + $.cookie('bsl_2')),
                        success: function (response) {
                            if (response.Success) {
                                successAlert(response.Message);
                                $("#modalMail").modal('hide');
                            }
                            else {
                                errorAlert(response.Message);
                                $("#modalMail").modal('hide');
                            }
                        },
                        error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                    });
                });
            });

            //Hide right sidebar when search or close is clicked
            $('#searchInvoice, .close-sidebar').click(function () {
                var sidebar = $('.left-sidebar').children().hasClass('col-md-4 collapsed');
                $('.filter-list').removeClass('fa-angle-right').addClass('fa-search');
                if (sidebar) {
                    $('.left-sidebar').find('[class="col-md-4 collapsed"]').removeClass('col-md-4 collapsed').addClass('col-md-12');
                    $('.search-group').show();
                    $('.right-sidebar').fadeOut();
                    $(".left-side").getNiceScroll().resize();
                }

                else {
                    //Write Search function here
                }
            });

            $(".right-side, .left-side").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });

            $('.listing-search-entity').on('change keyup',function () {
                searchOnTable($('.listing-search-entity'), $('#invoiceList'), 1);
            });


        
        //handler for view all receipts
        $('.view-receipts-register').click(function () {
            let id = Number($('#hdRegisterId').val());
            $.ajax({
                url: $('#hdApiUrl').val() + 'api/SalesEntry/GetReceipts?salesId=' + id,
                method: 'GET',
                dataType: 'JSON',
                success: function (data) {
                    console.log(data);
                    if (data.length) {
                        $('#tblAllReceipts > tbody').children().remove();
                        $('#modalReceipts').find('h2').html(data[0].Invoice);
                        let html = '';
                        $(data).each(function () {
                            html += '<tr>';
                            html += '<td>' + this.VoucherDate + '</td>';
                            html += '<td>' + this.NetAmount + '</td>';
                            html += '<td>' + this.PaidAmount + '</td>';
                            html += '<td style="text-align: center;"><a class="print-receipt" href="#" data-url="/finance/Print/Voucher.aspx?id=' + this.VoucherGroupId + '">print</a></td>';
                            html+='</tr>'
                        });
                        $('#tblAllReceipts > tbody').append(html);
                    } else {

                    }
                    $('#modalReceipts').modal('show');
                }
            });
        });

        //handler for print receipt button
        $('body').on('click', '.print-receipt', function () {
            let url = $(this).attr('data-url');
            PopupCenter(url, "Print Receipt", 800, 700);
        });
        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>

    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>
</asp:Content>
