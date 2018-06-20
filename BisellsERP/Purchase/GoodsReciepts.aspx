<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GoodsReciepts.aspx.cs" Inherits="BisellsERP.Purchase.GoodReciepts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Goods Receipts</title>
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
            <h3 class="page-title m-t-0">Goods Receipts</h3>
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
                                All GRN&nbsp;<b id="countOfInvoices">(0)</b>&nbsp;<span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu">
                                <li><a href="#">Draft</a></li>
                                <li><a href="#">Client Viewed</a></li>
                                <li><a href="#">Partially Paid</a></li>
                            </ul>
                        </div>
                        <input type="text" class="form-control listing-search-entity" placeholder="Search GRN Number..."/>
                        <a id="searchInvoice" href="#" class="pull-right"><i class="fa fa-search filter-list"></i></a>
                        <div class="pull-right t-y search-group">
                            <span class="filter-span">
                                <label>Filter by Date</label>
                                <input type="text" id="txtDate" name="daterange" value="01/01/2015 - 01/31/2015" />
                            </span>
                            <span class="filter-span">
                                <label>Filter by Supplier</label>
                                <asp:DropDownList ID="ddlSupplier" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                            </span>
                        </div>
                    </div>
                    <div class="left-side">
                        <div class="panel">
                            <div class="panel-body">

                                <table id="invoiceList" class="table left-table invoice-list">
                                    <thead>
                                        <tr>
                                            <th style="display: none">Id</th>
                                            <th class="show-on-collapsed">GRN Number</th>
                                            <th class="show-on-collapsed">Date</th>
                                            <th>Supplier</th>
                                            <th style="text-align:right">Tax Amount</th>
                                            <th class="show-on-collapsed" style="text-align:right">Net Amount</th>
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
                                        <a href="/Purchase/GRN?MODE=new" class="btn btn-default btn-xs btn-rounded waves-effect">Create New Purchase Goods Receipt</a>
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
                    <div class="col-sm-8">
                        <div class="btn-toolbar m-t-5" role="toolbar">
                            <div class="btn-group">
                                <button type="button" class="btn btn-default waves-effect waves-light edit-register" title="edit"><i class="md md-mode-edit"></i></button>
                                <button type="button" accesskey="p" class="btn btn-default waves-effect waves-light print-register" title="print"><i class="md  md-print"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light email-register" title="mail" data-toggle="modal" data-target="#modalMail"><i class="md  md-mail"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light delete-register" title="delete"><i class="fa fa-trash-o"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light  dropdown-toggle" data-toggle="dropdown" aria-expanded="false">More&nbsp;<span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li class="clone-register"><a href="#">Clone Bill</a></li>
                                    <li class="convert-to-bill"><a href="#">Convert to Bill</a></li>
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
                        </div>
                    </div>
                    <div class="col-sm-4">
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
                                                <small class="text-muted">Received by </small><small id="lblSupName" class="underline"></small>
                                                </b>
                                                <br />
                                                <small id="lblSupAddress1"></small>
                                                <br />
                                                <small id="lblSupAddress2"></small>
                                                <br />
                                                <small id="lblSupState"></small>
                                                <br />
                                                <small id="lblSupCountry"></small>
                                                <br />
                                                ☎ :<small id="lblCompPh"></small><br />
                                                <b>TRN No :  </b>
                                                <small id="lblSupTaxNo"></small>
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
                                                <span>Order No<span class="pull-right">:</span></span>
                                                <b id="lblInvoiceNo"></b>
                                            </p>
                                            <p class="m-0">
                                                <span>GSTIN<span class="pull-right">:</span></span>
                                                <b id="lblCompGst">
                                                <b class="currency-only"></b> </b><b class="label-net"></b>
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
                                                            <th style="text-align:right;">Rate</th>
                                                            <th style="text-align:right;">Qty</th>
                                                            <th style="text-align:right;">Received Qty</th>
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
                                            <p class="m-0">
                                                <span>Tax Amount<span class="pull-right">:</span></span>
                                                <b id="lblTax"></b>
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
                </div>
                <!-- panel -->
            </div>
            <!-- End Right sidebar -->
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
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <!-- Date Range Picker -->
   <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
     <script>

        //Document ready function starts here
         $(document).ready(function ()
         {
            //Retrieve company details
            $.ajax({
                url: $('#hdApiUrl').val() + 'api/GRN/GetCompanyDetails/',
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

             //Scroll
           $(".left-side").niceScroll({
                cursorcolor: "#6d7993",
                cursorwidth: "8px"
           });

            //load currency symbol from database
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            //Filter data according to daterange or supplier wise
            $(document).on('click', '.filter-list', function () {
                var supplierId = $('#ddlSupplier').val() == '0' ? null : $('#ddlSupplier').val();
                var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                refreshTable(supplierId, fromDate, toDate);
            });

            //Get list of invoices function call
            refreshTable(null, null, null);
            
             //Get list of invoices
            function refreshTable(supplierID, fromDate, toDate)
            {
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/GRN/Get?SupplierId=' + supplierID + '&FromDate=' + fromDate + '&ToDate=' + toDate,
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
                        $(data).each(function () {
                            count++;
                            html += '<tr>';
                            html += '<td style="display:none">' + this.GRNID + '</td>';
                            html += '<td class="show-on-collapsed">' + this.InvoiceNo + '</td>';
                            html += '<td class="show-on-collapsed">' + this.EntryDateString + '</td>';
                            html += '<td>' + this.Supplier + '</td>';
                            html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                            html += '<td class="show-on-collapsed" style="text-align:right">' + this.Net + '</td>';
                            html += '</tr>';
                        });
                        $('#invoiceList tbody').children().remove();
                        $('#countOfInvoices').text('(' + count + ')');
                        $('#invoiceList tbody').append(html);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); }
                });
            }

            //datetime initialization
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
                    url: $('#hdApiUrl').val() + 'api/GRN/Get/' + id,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (data) {
                        $('#hdRegisterId').val(data.GRNID);
                        $('#hdEmail').val(data.BillingAddress[0].Email);
                        $('#lblSupName').text(data.Supplier);
                        $('#lblSupAddress1').text(data.BillingAddress[0].Address1);
                        $('#lblSupAddress2').text(data.BillingAddress[0].Address2);
                        $('#lblSupPhone').text(data.BillingAddress[0].Phone1);
                        $('#lblLocName').text(data.Location);
                        $('#lblSupState').text(data.BillingAddress[0].State);
                        $('#lblSupCountry').text(data.BillingAddress[0].Country);
                        $('#lblLocAddr1').text(data.LocationAddress1);
                        $('#lblLocAddr2').text(data.LocationAddress2);
                        $('#lblLocPhone').text(data.LocationPhone);
                        $('#lblLocRegId').text(data.LocationRegId);
                        $('#lblSupTaxNo').text(data.SupplierTaxNo);
                        $('#lblDate').text(data.EntryDateString);
                        $('#lblInvoiceNo').text(data.InvoiceNo);

                        var html = '';
                        $(data.Products).each(function (index) {
                            html += '<tr>';
                            html += '<td>' + this.ItemCode + '</td>';
                            html += '<td><b>' + this.Name + '</b><br/><i>' + this.Description + '</i></td>';
                            html += '<td style="text-align:right;">' + this.CostPrice + '</td>';
                            html += '<td style="text-align:right;">' + this.Quantity + '</td>';
                            html += '<td style="text-align:right;">' + this.ModifiedQuantity + '</td>';
                            html += '<td style="text-align:right;">' + this.TaxPercentage + '</td>';
                            html += '<td style="text-align:right;">' + this.TaxAmount + '</td>';
                            html += '<td style="text-align:right;">' + this.NetAmount + '</td>';
                            html += '</tr>';
                        });
                        $('#listTable tbody').children().remove();
                        $('#listTable tbody').append(html);
                        $('#lblTax').text(data.TaxAmount);
                        $('#lblNet').text(data.Net);
                        console.log(data);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); }
                });
                $('.right-sidebar').fadeOut();
                $('.right-sidebar').fadeIn();
            });
            
            //Edit functionality
            $(document).on('click', '.edit-register', function () {
                window.open('/Purchase/GRN?MODE=edit&UID=' + $('#hdRegisterId').val(), '_self');
            });

            //Print Entry
            $(document).on('click', '.print-register', function () {
                var type = JSON.parse($('#hdSettings').val()).TemplateType;
                var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                PopupCenter('/Purchase/print/' + type + '/' + number + '/GRN?id=' + $('#hdRegisterId').val() + "&location=" + $.cookie('bsl_2'), 'GRN', 1000, 1000);
            });

            //Clone bill
            $(document).on('click', '.clone-register', function () {
                window.open('/Purchase/GRN?MODE=clone&UID=' + $('#hdRegisterId').val(), '_self');
            });

            $(document).on('click', '.convert-to-bill', function () {
                window.open('/Purchase/Entry?MODE=convert&TYPE=GRN&UID=' + $('#hdRegisterId').val(), '_self');
            });

            

            //Create a new GRN 
            $(document).on('click', '.new-register', function () {
                window.open('/Purchase/GRN?MODE=new', '_self');
            });

            //Delete an entry
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
                        var supplierId = $('#ddlSupplier').val() == '0' ? null : $('#ddlSupplier').val();
                        var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                        var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                        var id = $('#hdRegisterId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/GRN/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        $('.md-cancel').trigger('click');
                                        refreshTable(supplierId, fromDate, toDate);
                                    }
                                    else {
                                        errorAlert(response.Message);
                                    }
                                },
                                error: function (xhr) {
                                    alert(xhr.responseText);
                                    console.log(xhr);
                                    miniLoading('stop');
                                },
                                beforeSend: function () { miniLoading('start'); },
                                complete: function () { miniLoading('stop'); }
                            });
                        }
                    });
                }
            });

            //mail modal initialization
            $("#modalMail").on('hidden.bs.modal', function () {
                $('.before-send').show();
                $('.after-send').hide();
                $('#txtMailAddress').val('');
            });

            //place supplier email id to mail 
            $(document).on('click', '.email-register', function (e) {
                $('#txtMailAddress').val($('#hdEmail').val());
            });

            //Mail sending function starts here
            $(document).on('click', '#btnSend', function (e) {
                $('.before-send').fadeOut('slow', function () {
                    $('.after-send').fadeIn();
                    var toaddr = $('#txtMailAddress').val();
                    var id = $('#hdRegisterId').val();
                    var modifiedBy = $.cookie('bsl_3');
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/GRN/SendMail?grnId=' + id + '&toaddress=' + toaddr + '&userid=' + $.cookie('bsl_3'),
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify($(location).attr('protocol') + '//' + $(location).attr('host') + '/Purchase/Print/' + type + '/' + number + '/GRN?id=' + id + '&location=' + $.cookie('bsl_2')),
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
            //Mail sending function ends here

            //Hide right sidebar when search is clicked
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
            $('.listing-search-entity').on('change keyup', function () {
                searchOnTable($('.listing-search-entity'), $('#invoiceList'), 1);
            });
        }); //Document ready function ends here
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>
</asp:Content>
