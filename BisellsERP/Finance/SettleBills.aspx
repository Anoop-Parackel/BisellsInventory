<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SettleBills.aspx.cs" Inherits="BisellsERP.Finance.Receipts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Settle Bills</title>
    <style>
        .content-page {
            overflow: hidden;
        }

        .panel .panel-body {
            padding: 10px;
        }

        .customer-div .panel-body > div:not(:last-child), .supplier-div .panel-body > div:not(:last-child) {
            border-right: 1px dashed #ccc;
        }

        ul.voucher-list {
            width: 19%;
            margin-left: 2%;
            padding-left: 0;
            list-style: none;
            display: inline-block;
            position: absolute;
            top: 0;
            right: -20%;
        }

            ul.voucher-list li {
                height: 60px;
                background-color: #fff;
                padding: 10px;
                margin-bottom: 10px;
                position: relative;
            }

                ul.voucher-list li:first-child {
                    height: 20px;
                    padding: 0 10px;
                    margin-bottom: 4px;
                    background-color: #607d8b;
                    color: #ECEFF1;
                    border-radius: 3px 3px 0 0;
                }

        .voucher-close {
            cursor: pointer;
        }

        ul.voucher-list li .voucherNo {
            position: absolute;
            bottom: 10px;
            left: 10px;
            font-size: smaller;
        }

        ul.voucher-list .voucherAmt {
            margin: 0;
            position: absolute;
            top: 10px;
            right: 10px;
        }

        ul.voucher-list .voucherSelect {
            margin: 0;
            position: absolute;
            bottom: 10px;
            right: 10px;
            font-size: smaller;
            border: 1px solid #607d8b;
            border-radius: 20px;
            padding: 0 10px;
            color: #607d8b;
            cursor: pointer;
        }

            ul.voucher-list .voucherSelect:disabled {
                cursor: not-allowed;
            }

        ul.voucher-list > li > span:first-child {
            font-size: 12px;
        }

        .with-voucher {
            margin: 0 2% !important;
        }

        #salesBills, #purchaseBills {
            width: 80%;
            margin: 0 10%;
            position: relative;
            transition: all .3s ease-in-out;
        }

        .dummy-data {
                margin-left: calc(50% - 50vw);
                margin-right: calc(50% - 50vw);
                margin-top: calc(45% - 35vh);
                margin-bottom: calc(45% - 50vh);
                position: relative;

        }


        @media only screen and (max-width: 1024px) {
            #salesBills {
                width: 90%;
            }
        }

        @media only screen and (max-width: 768px) {
            #salesBills {
                width: 100%;
            }
        }

        .invo-card {
            position: relative;
            background-color: #FFF;
            background: url('../Theme/images/cream_pixels.png');
            padding: 5px;
            box-shadow: 0 2px 1px 0 #ccc;
            margin: 10px 0;
            border-radius: 3px;
        }

            .invo-card h3 {
                font-size: 20px;
            }

            .invo-card > div:first-child i {
                color: #e4e4e4;
            }

            .invo-card > div:nth-child(2) span {
                width: 100%;
                border-radius: 4px;
                padding: 5px 0;
                font-size: medium;
                border: 2px solid #e4e4e4;
                background-color: transparent;
                color: #ef5350;
            }

            .invo-card > div:nth-child(4) input {
                width: 100%;
                border-radius: 5px;
                border: 1px solid #ccc;
                text-align: center;
                padding: 6px;
                height: 33px;
            }

            .invo-card > div:nth-child(4) label {
                color: #9e9e9e;
            }

        .pay-checkbox {
            cursor: pointer;
            margin-top: 10px;
            font-size: 3em;
        }

        i.ion-ios7-checkmark-outline.pay-checkbox {
            color: #33b86c;
            transition: color .2s ease-in-out;
        }

        .voucher-group {
            padding: 5px 0;
        }

            .voucher-group > .badge {
                background-color: #607D8B;
                cursor: pointer;
                margin-right: 3px;
            }

                .voucher-group > .badge:after {
                    content: 'x';
                    color: #ECEFF1;
                    background-color: #607D8B;
                    width: 100%;
                    height: 100%;
                    position: absolute;
                    top: 0;
                    left: 0;
                    border-radius: 10px;
                    padding: 3px;
                    display: none;
                }

                .voucher-group > .badge:hover:after {
                    display: block;
                }

        .btn-settle, .btn-settle:hover, .btn-settle:active {
            padding: 1px 3px;
            background-color: #1976D2;
            color: #fff;
            box-shadow: none;
            display: block;
            margin: 0 auto;
        }

            .btn-settle:disabled, .btn-settle:disabled:hover {
                background-color: #9e9e9e;
            }

        .date-picker {
            width: 100%;
            border: none;
            text-align: center;
            cursor: pointer;
        }

        .combo-dropdown {
            display: inline-block;
            position: relative;
            margin-right: 10px;
        }

            .combo-dropdown select {
                margin: 0;
            }

        .pay-all div {
            display: inline-block;
            width: 50%;
            float: left;
        }

        .pay-all input[type=number] {
            border: none;
            text-align: center;
        }

        .pull-right.form-inline {
            margin-right: 40px;
        }

        .search-icon {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 2px 8px;
            font-size: 20px;
            cursor: pointer;
            transition: all .2s ease-in-out;
        }

            .search-icon:hover {
                color: #4ac2f8;
                border: 1px solid #4ac2f8;
            }

        .invoice-wrapper {
            height: 68vh;
            overflow-y: scroll;
            overflow-x: hidden;
        }

        #paymentModal .form-group {
            margin-bottom: 8px;
        }

        #voucherType {
            font-size: 20px;
        }

        #billType, #customerDropdown, #supplierDropdown {
            width: 200px;
        }

        .Goto-link {
            margin-left: 350px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%------ Page Title ------%>
    <div class="row m-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0" id="MainTitle">Settle Bills</h3>
        </div>
        <div></div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right m-t-0" role="group">
                <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Proceed to Payment" class="btn btn-default waves-effect waves-light"><i class="ion-archive"></i>&nbsp;Proceed to Pay</button>
                <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger hidden"><i class="ion ion-trash-b"></i></button>
            </div>
            <div class="pull-right form-inline">
                <select id="billType" class="form-control m-r-10 drpTest">
                    <option value="0">Customer Receipts</option>
                    <option value="1">Supplier Payments</option>
                </select>
                <div class="combo-dropdown">
                    <asp:DropDownList ID="customerDropdown" class="form-control m-r-10" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                    <asp:DropDownList ID="supplierDropdown" class="form-control m-r-10 hidden" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
                <i id="searchCustomer" class="md md-search search-icon"></i>
                <i id="searchSupplier" class="md md-search search-icon hidden"></i>
            </div>
        </div>
    </div>



    <%------ CUSTOMER DIVISION ------%>
    <div class="row customer-div">
        <div class="panel b-r-8">
            <div class="panel-body">
                <div class="col-sm-2 text-center">
                    <h4>
                        <input type="text" class="date-picker pay-date" /></h4>
                    <span class="text-muted">Date</span>
                </div>
                <div class="col-sm-2 text-center">
                    <h4><span id="selectedBills">0</span> of <span id="totalBills">0</span></h4>
                    <span class="text-muted">Total Bills Selected</span>
                </div>
                <div class="col-sm-2 text-center">
                    <h4 id="pendingAmount">0.00</h4>
                    <span class="text-muted">Pending Amount</span>
                </div>
                <div class="col-sm-2 text-center">
                    <h4 id="cashInAccount">0.00</h4>
                    <span class="text-muted">Cash in Account</span>
                </div>
                <div class="col-sm-4  text-center pay-all">
                    <div>
                        <h4>
                            <input id="txtPayAmount" type="number" value="0.00" /></h4>
                        <span class="text-muted">Paying/Payable</span>
                    </div>
                    <div>
                        <button id="payAllCust" type="button" class="btn btn-info btn-custom m-t-20">Pay All</button>
                        <button id="allocateCust" type="button" class="btn btn-info btn-custom m-t-20">Allocate All</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="invoice-wrapper">
            <ul id="salesBills" class="list-unstyled sales-bill-list">
                <%--                <li class="invo-card row">
                    <div class="col-xs-1 text-center">
                        <i class="fa ion-android-note fa-3x m-t-30"></i>
                    </div>
                    <div class="col-xs-3 text-center">
                        <span class="badge m-t-5">B12YF456</span>
                        <div class="text-muted m-t-5">Dated : 12 Nov 2017</div>
                        <h4 class="m-0">Net Amount :<label>16588</label></h4>
                    </div>
                    <div class="col-xs-4 text-center">
                        <h3 class="m-t-30">Balance :
                        <label>42588</label></h3>
                    </div>
                    <div class="col-xs-3 text-center">
                        <label class="control-label">Amount</label>
                        <input type="number" />
                        <div class="voucher-group">
                            <span class="badge badge-sm" title="Voucher #125">12455</span>
                            <span class="badge badge-sm" title="Voucher #15">1255</span>
                            <span class="badge badge-sm" title="Voucher #3">12455</span>
                            <span class="badge badge-sm" title="Voucher #48">12455</span>
                        </div>
                    </div>
                    <div class="col-xs-1 text-center">
                        <i class="ion-ios7-checkmark-outline pay-checkbox"></i>
                        <input type="checkbox" class=" hidden" />
                        <button type="button" class="btn btn-sm btn-settle">SETTLE</button>
                    </div>
                </li>
                <li class="invo-card row">
                    <div class="col-xs-1 text-center">
                        <i class="fa ion-android-note fa-3x m-t-30"></i>
                    </div>
                    <div class="col-xs-3 text-center">
                        <span class="badge m-t-5">B12YF456</span>
                        <div class="text-muted m-t-5">Dated : 12 Nov 2017</div>
                        <h4 class="m-0">Net Amount :<label>16588</label></h4>
                    </div>
                    <div class="col-xs-4 text-center">
                        <h3 class="m-t-30">Balance :
                        <label>42588</label></h3>
                    </div>
                    <div class="col-xs-3 text-center">
                        <label class="control-label">Amount</label>
                        <input type="number" />
                        <div class="voucher-group">
                        </div>
                    </div>
                    <div class="col-xs-1 text-center">
                        <i class="ion-ios7-circle-outline pay-checkbox"></i>
                        <input type="checkbox" class=" hidden" />
                        <button type="button" class="btn btn-sm btn-settle">SETTLE</button>
                    </div>
                </li>
                <li class="invo-card row">
                    <div class="col-xs-1 text-center">
                        <i class="fa ion-android-note fa-3x m-t-30"></i>
                    </div>
                    <div class="col-xs-3 text-center">
                        <span class="badge m-t-5">B12YF456</span>
                        <div class="text-muted m-t-5">Dated : 12 Nov 2017</div>
                        <h4 class="m-0">Net Amount :<label>16588</label></h4>
                    </div>
                    <div class="col-xs-4 text-center">
                        <h3 class="m-t-30">Balance :
                        <label>42588</label></h3>
                    </div>
                    <div class="col-xs-3 text-center">
                        <label class="control-label">Amount</label>
                        <input type="number" />
                        <div class="voucher-group">
                        </div>
                    </div>
                    <div class="col-xs-1 text-center">
                        <i class="ion-ios7-circle-outline pay-checkbox"></i>
                        <input type="checkbox" class=" hidden" />
                        <button type="button" class="btn btn-sm btn-settle" disabled="disabled">SETTLE</button>
                    </div>
                </li>
                
                                    <ul class="voucher-list hidden">
                                        
                        <li>
                            <label>Vouchers</label><span class="voucher-close pull-right"><i class="ion ion-close-circled"></i></span>
                            <input id="hdCurrentBill" type="hidden" value=""/>
                        </li>
                        <li>
                            <span class="text-muted">12/11/2017</span>
                            <span class="voucherNo text-info">#1255</span>
                            <h4 class="voucherAmt">12500</h4>
                            <span class="voucherSelect">Use</span>
                        </li>
                        <li>
                            <span class="text-muted">12/11/2017</span>
                            <span class="voucherNo text-info">#1255</span>
                            <h4 class="voucherAmt">12500</h4>
                            <span class="voucherSelect">Use</span>
                        </li>
                        <li>
                            <span class="text-muted">12/11/2017</span>
                            <span class="voucherNo text-info">#1255</span>
                            <h4 class="voucherAmt">12500</h4>
                            <span class="voucherSelect">Use</span>
                        </li>
                    </ul>--%>
            </ul>

        </div>
    </div>

    <%------ SUPPLIER DIVISION ------%>
    <div class="row supplier-div hidden">
        <div class="panel b-r-8">
            <div class="panel-body">
                <div class="col-sm-2 text-center">
                    <h4>
                        <input type="text" class="date-picker pay-date" /></h4>
                    <span class="text-muted">Date</span>
                </div>
                <div class="col-sm-2 text-center">
                    <h4><span id="selectedSupplierBills">0</span> of <span id="totalSupplierBills">0</span></h4>
                    <span class="text-muted">Total Bills Selected</span>
                </div>
                <div class="col-sm-2 text-center">
                    <h4 id="supplierPendingAmount">0.00</h4>
                    <span class="text-muted">Pending Amount</span>
                </div>
                <div class="col-sm-2 text-center">
                    <h4 id="SupplirecashInAccount">0.00</h4>
                    <span class="text-muted">Cash in Account</span>
                </div>
                <div class="col-sm-4  text-center pay-all">
                    <div>
                        <h4>
                            <input id="txtSupplierPayAmount" type="number" value="0.00" /></h4>
                        <span class="text-muted">Paying/Payable</span>
                    </div>
                    <div>
                        <button id="payAllSupp" type="button" class="btn btn-info btn-custom m-t-20">Pay All</button>
                        <button id="allocateSupp" type="button" class="btn btn-info btn-custom m-t-20">Allocate All</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="invoice-wrapper">
            <ul id="purchaseBills" class="list-unstyled sales-bill-list">
            </ul>
        </div>

    </div>


    <%-- Payment Modal --%>
    <div id="paymentModal" class="modal animated fadeIn">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <span id="voucherType" class="text-primary">Customer Payment</span>
                    <div class="form-group pull-right">
                        <div class="radio radio-info radio-inline">
                            <input type="radio" id="inlineRadio1" value="cash" name="radioInline" checked="checked" />
                            <label for="inlineRadio1">Cash </label>
                        </div>
                        <div class="radio radio-info radio-inline">
                            <input type="radio" id="inlineRadio2" value="cheque" name="radioInline" />
                            <label for="inlineRadio2">Cheque </label>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <div id="chequeDiv" class="row" style="display: none">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Bank</label>
                                <asp:DropDownList ID="ddlPaymentBank" runat="server" ClientIDMode="Static" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <input id="txtBank" type="text" class="form-control hidden" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Cheque Number</label>
                                <input id="txtChequeNumber" type="text" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Cheque Date</label>
                                <input id="txtChequeDate" placeholder="provide date.." type="text" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Draw On</label>
                                <input id="txtDrawOn" placeholder="Draw On" type="text" class="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label">Paying Amount</label>
                        <input type="text" id="amounttoPay" disabled="disabled" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label class="control-label">Narration</label>
                        <%--<input type="text" class="form-control" />--%>
                        <textarea id="txtNarration" class="form-control"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <span class="pull-left text-muted m-t-5">You need to pay <span id="paymentCash" class="text-success">AED 2580</span> for this settlement </span>
                    <button type="button" class="btn btn-default waves-effect" data-dismiss="modal">Close</button>
                    <button type="button" id="btnSaveConfirmed" class="btn btn-primary waves-effect waves-light">Save</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <%-- Customer Scripts --%>
    <script>
        $(function () {
            //Show Voucher List
            $('body').on('click', '.customer-div .btn-settle', function () {
                var liPos = $(this).closest('li').position().top + 10;
                $('.sales-bill-list').addClass('with-voucher');
                $(this).closest('ul').find('ul.voucher-list').css('top', '' + liPos + 'px').removeClass('hidden');
                $('#hdCurrentBill').val($(this).closest('li').val());

            });
            //Hide Voucher List
            $('body').on('click', '.customer-div .voucher-close', function () {
                $('.sales-bill-list').removeClass('with-voucher');
                $(this).closest('ul').addClass('hidden');
            });


            //Allocate Voucher to Invoice
            $('body').on('click', '.customer-div .voucherSelect', function () {

                var vouchAmt = parseFloat($(this).closest('li').find('.voucherAmt').text());
                var vouchNo = $(this).closest('li').find('.voucherNo').text();
                var id = $(this).closest('li').val();
                if (isNaN(vouchAmt) || vouchAmt == '0') {

                }
                else {
                    var currentBill = $('#salesBills').children('li[value=' + $('#hdCurrentBill').val() + ']');
                    var originalBal = parseFloat(currentBill.find('.li-balance').text());
                    var currentVoucherAmount = 0;
                    var requiredBal = 0;
                    currentBill.find('.voucher-group > span.badge').each(function () {
                        currentVoucherAmount += parseFloat($(this).text());
                    });
                    requiredBal = originalBal - currentVoucherAmount;
                    if (requiredBal == 0) {
                        //do nothing
                    }
                    else if (requiredBal < vouchAmt) {
                        $('#salesBills').children('li[value=' + $('#hdCurrentBill').val() + ']').find('.voucher-group').append('<span id="' + id + '" class="badge badge-sm" title="Voucher ' + vouchNo + '">' + requiredBal + '</span>');
                        $(this).closest('li').find('.voucherAmt').text(vouchAmt - requiredBal);
                        currentBill.find('.li-ind-amount').val(0);
                    }
                    else {
                        $('#salesBills').children('li[value=' + $('#hdCurrentBill').val() + ']').find('.voucher-group').append('<span id="' + id + '" class="badge badge-sm" title="Voucher ' + vouchNo + '">' + vouchAmt + '</span>');
                        $(this).closest('li').find('.voucherAmt').text('0');
                        //finding current voucher after adding the new one
                        currentVoucherAmount = 0;
                        currentBill.find('.voucher-group > span.badge').each(function () {
                            currentVoucherAmount += parseFloat($(this).text());
                        });
                        currentBill.find('.li-ind-amount').val(originalBal - currentVoucherAmount);
                    }
                    currentBill.find('.li-checkbox').prop('checked', true).trigger('change');
                    resetBal();
                    $('#txtPayAmount').val(findPayable());
                    $('#selectedBills').html(findSelectedBills());
                    $(this).text('Used');
                }
            })
            //Remove Allocated Voucher from Invoice
            $('body').on('click', '.customer-div .voucher-group > .badge', function () {
                $(this).remove();
                var vouchId = $(this).attr('id');
                var amt = (parseFloat($(this).text()));
                var bal = parseFloat($('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text());
                $('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text(bal + amt);
                resetBal();
                $('#txtPayAmount').val(findPayable());
                $('#selectedBills').html(findSelectedBills());
            });

            //Get Customer wise sales data
            $('#searchCustomer').click(function () {
                if ($('#customerDropdown').val() != '0') {
                    resetMainPanel();
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SettleBills/GetSalesData?CustomerId=' + $('#customerDropdown').val(),
                        method: 'POST',
                        dataType: 'JSON',
                        success: function (response) {
                            if (response == null) {
                                var html = '';
                                $('#totalBills').html('0');
                                $('#cashInAccount').html('0');
                                $('#pendingAmount').html('0');
                                html += '<div class="dummy-data">';
                                html += '<h1 style="text-align:center">No pending invoices to settle</h1>'
                                html += '<div style="text-align:center"><a href="/Sales/Entry?CUSTOMER=' + $('#customerDropdown').val() + '">Create a new Invoice</a></div>'
                                $('#salesBills').children().remove();
                                $('#salesBills').append(html);
                                html = '';
                            }
                            else {
                                $('#totalBills').html(response.Sales.length);
                                $('#cashInAccount').html((response.AvailableAmount).toFixed(2));
                                $('#pendingAmount').html((response.PendingAmount).toFixed(2));
                                var html = '';
                                $('#salesBills').children().remove();
                                $(response.Sales).each(function () {
                                    html += '<li value="' + this.SalesId + '" class="invo-card row"><div class="col-xs-1 text-center"><i class="fa ion-android-note fa-3x m-t-20"></i></div><div class="col-xs-3 text-center">';
                                    html += '<a href="/Sales/Entry?UID=' + this.SalesId + '&MODE=edit"><span class="badge m-t-5">' + this.BillNo + '</span></a>';
                                    html += '<div class="text-muted m-t-5">Dated : ' + this.SalesDate + '</div>';
                                    html += ' <h4 class="m-0">Net Amount :<label>' + this.NetAmount + '</label></h4></div>';
                                    html += ' <div class="col-xs-4 text-center"><h3 class="m-t-30">Balance :<label class="li-balance hidden">' + this.BalanceAmount + '</label><label class="li-new-balance">' + this.BalanceAmount + '</label></h3></div>';
                                    html += '<div class="col-xs-3 text-center"><label class="control-label">Amount</label><input class="li-ind-amount" type="number" /><div class="voucher-group"></div></div>'
                                    html += '<div class="col-xs-1 text-center"><i class="ion-ios7-circle-outline pay-checkbox"></i><input type="checkbox" class="li-checkbox hidden" /><button type="button" class="btn btn-sm btn-settle" >SETTLE</button></div>';
                                    html += '</li>';

                                });
                                html += '<ul class="voucher-list hidden"><li><label>Vouchers</label><span class="voucher-close pull-right"><i class="ion ion-close-circled"></i></span><input id="hdCurrentBill" type="hidden" value=""/></li>';
                                if (response.Vouchers.length > 0) {
                                    $(response.Vouchers).each(function () {
                                        html += '<li value="' + this.Id + '"><span class="text-muted">' + this.Date + '</span><span class="voucherNo text-info">#' + this.Type + '</span><h4 class="voucherAmt">' + this.Amount + '</h4><span class="voucherSelect">Use</span></li>';
                                    });
                                }
                                else {
                                    html += '<li><p>Sorry. No available voucher found</p></li>';
                                }
                                html += '</ul>';
                                //$('#salesBills').children().remove();
                                $('#salesBills').append(html);
                                html = '';
                            }
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); },
                    });
                }
                else {
                    errorField('#customerDropdown');
                }
            });
            //Date picker init
            $('.customer-div .date-picker').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            //reset main panel
            resetMainPanel();
            //Invoice Pay Chechbox Toggle
            $(document).on('click', '.customer-div .pay-checkbox', function () {
                if ($(this).hasClass('ion-ios7-circle-outline')) {
                    $(this).removeClass('ion-ios7-circle-outline').addClass('ion-ios7-checkmark-outline').next('input[type=checkbox]').prop('checked', true);
                    $(this).closest('li').find('.li-ind-amount').val($(this).closest('li').find('.li-balance').html());
                    resetBal();
                    $('#txtPayAmount').val(findPayable());
                    $('#selectedBills').html(findSelectedBills());
                }
                else if ($(this).hasClass('ion-ios7-checkmark-outline')) {
                    $(this).removeClass('ion-ios7-checkmark-outline').addClass('ion-ios7-circle-outline').next('input[type=checkbox]').prop('checked', false);
                    $(this).closest('li').find('.li-ind-amount').val('');
                    resetBal();
                    $('#txtPayAmount').val(findPayable());
                    $('#selectedBills').html(findSelectedBills());
                }
            });

            $('body').on('change', '.li-checkbox', function () {
                var checkbox = $(this).closest('li').find('.pay-checkbox');
                if ($(this).prop('checked')) {
                    checkbox.removeClass('ion-ios7-circle-outline').addClass('ion-ios7-checkmark-outline');
                }
                else {
                    checkbox.addClass('ion-ios7-circle-outline').removeClass('ion-ios7-checkmark-outline');
                }
            });

            $('.drpTest').change(function () {
                //val - 0 (Customer Receipts)
                //val - 1 (Supplier Payments)
                if ($(this).val() == 0) {
                    $('#MainTitle').text("Customer Receipts");
                    $('.supplier-div').fadeOut("slow", function () {
                        $('.customer-div').fadeIn("slow");
                        $('#supplierDropdown, #searchSupplier').addClass('hidden');
                        $('#customerDropdown, #searchCustomer').removeClass('hidden');
                    });
                }
                else if ($(this).val() == 1) {
                    $('#MainTitle').text("Supplier Payments");
                    $('.customer-div').fadeOut("slow", function () {
                        $('.supplier-div').removeClass('hidden').fadeIn("slow");
                        $('#supplierDropdown, #searchSupplier').removeClass('hidden');
                        $('#customerDropdown, #searchCustomer').addClass('hidden');
                    });
                }
            });

            //Pay All
            $('#payAllCust').off().click(function () {
                $('.li-ind-amount').val('');
                resetAllcheckBoxes();
                checkAllcheckboxes();
                var bills = $('#salesBills').children('li');
                var payingAmount = 0;
                for (var i = 0; i < bills.length; i++) {
                    var voucher = $(bills[i]).find('.voucher-group > span.badge');
                    var vouchId = voucher.attr('id');
                    var voucherAmt = parseFloat(voucher.text());
                    var voucherBal = parseFloat($('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text());
                    $('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text(voucherBal + voucherAmt);
                    voucher.remove();
                    var bal = parseFloat($(bills[i]).find('.li-balance').html()).toFixed(2);
                    $(bills[i]).find('.li-ind-amount').val(bal);
                    payingAmount += parseFloat(bal);


                }
                resetBal();
                $('#txtPayAmount').val(payingAmount);
                $('#selectedBills').html(findSelectedBills());
            });

            $('body').on('keyup', '.li-ind-amount', function () {

                if ($(this).val() == '' || $(this).val() == null || isNaN($(this).val())) {
                    $(this).closest('li').find('.pay-checkbox').removeClass('ion-ios7-checkmark-outline').addClass('ion-ios7-circle-outline').next('input[type=checkbox]').prop('checked', false);
                }
                else {
                    $(this).closest('li').find('.pay-checkbox').removeClass('ion-ios7-circle-outline').addClass('ion-ios7-checkmark-outline').next('input[type=checkbox]').prop('checked', true);
                }
                resetBal();
                $('#txtPayAmount').val(findPayable());
                $('#selectedBills').html(findSelectedBills());
            });

            //Allocate All
            $('#allocateCust').off().click(function () {
                $('.li-ind-amount').val('');
                resetBal();
                resetAllcheckBoxes();
                var bills = $('#salesBills').children('li');
                var amountToPay = 0;
                var payingAmount = parseFloat($('#txtPayAmount').val());
                if (payingAmount > 0) {
                    for (var i = 0; i < bills.length; i++) {
                        var voucher = $(bills[i]).find('.voucher-group > span.badge');
                        var vouchId = voucher.attr('id');
                        var voucherAmt = parseFloat(voucher.text());
                        var voucherBal = parseFloat($('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text());
                        $('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text(voucherBal + voucherAmt);
                        voucher.remove();
                        var bal = parseFloat($(bills[i]).find('.li-balance').html()).toFixed(2);
                        if (bal <= payingAmount) {
                            $(bills[i]).find('.li-ind-amount').val(bal);
                            $(bills[i]).find('.li-checkbox ').prop('checked', true).trigger('change');
                            $(bills[i]).find('.li-new-balance').html('0.00');
                            payingAmount -= bal;
                        }
                        else {
                            $(bills[i]).find('.li-ind-amount').val(payingAmount.toFixed(2));
                            $(bills[i]).find('.li-checkbox ').prop('checked', true).trigger('change');
                            $(bills[i]).find('.li-new-balance').html(bal - payingAmount);
                            payingAmount = 0.00;
                            break;
                        }
                    }
                    $('#selectedBills').html(findSelectedBills());
                }
                else {
                    $('#selectedBills').html(findSelectedBills());
                }
            });
        });

        //findSelected Bills
        function findSelectedBills() {
            var count = 0;
            $('.li-checkbox').each(function () {
                if ($(this).prop('checked')) {
                    count++;
                }
            });
            return count;
        }

        //uncheck all checkboxes
        function resetAllcheckBoxes() {
            $('.li-checkbox').each(function () {
                $(this).prop('checked', false).trigger('change');
            });
        }

        //check all checkboxes
        function checkAllcheckboxes() {
            $('.li-checkbox').each(function () {
                $(this).prop('checked', true).trigger('change');
            });
        }

        //find payable amount
        function findPayable() {
            var payable = 0;
            $('.li-ind-amount').each(function (index) {
                var amount = $(this).val() != null && $(this).val() != '' && !isNaN($(this).val()) ? parseFloat($(this).val()) : 0;
                payable += amount;
            });
            return payable;
        }

        //reset balances
        function resetBal() {
            $('.li-ind-amount').each(function () {
                var amount = $(this).val() != null && $(this).val() != '' && !isNaN($(this).val()) ? parseFloat($(this).val()) : 0;
                var originalBal = parseFloat($(this).closest('li').find('.li-balance').html());
                var currentVoucherAmount = 0;
                $(this).closest('li').find('.voucher-group > span.badge').each(function () {
                    currentVoucherAmount += parseFloat($(this).text());
                });

                $(this).closest('li').find('.li-new-balance').html((originalBal - (amount + currentVoucherAmount)).toFixed(2));
            });
        }
        //reset main panel
        function resetMainPanel() {
            $('#selectedBills').text('0');
            $('#totalBills').text('0');
            $('#pendingAmount').text('0.00');
            $('#cashInAccount').text('0.00');
            $('#txtPayAmount').val('0.00');
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('.customer-div input.date-picker').datepicker('setDate', today);
        }
    </script>

    <%-- Supplier Script --%>
    <script>
        $(function (e) {
            //Get Supplier wise Purchase data
            $('#searchSupplier').click(function () {
                if ($('#supplierDropdown').val() != '0') {
                    resetMainPanel();
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SettleBills/GetPurchaseData?SuplierId=' + $('#supplierDropdown').val(),
                        method: 'POST',
                        dataType: 'JSON',
                        success: function (response) {
                            console.log(response);
                            if (response == null) {
                                var html = '';
                                $('#totalSupplierBills').html('0');
                                $('#SupplirecashInAccount').html('0');
                                $('#supplierPendingAmount').html('0');
                                console.log($('#supplierDropdown').val());
                                html += '<div class="dummy-data">';
                                html += '<h1 style="text-align:center">No pending invoices to settle</h1>'
                                html += '<div style="text-align:center"><a href="/Purchase/Entry?SUPPLIER=' + $('#supplierDropdown').val() + '">Create new Purchase</a></div>'
                                $('#purchaseBills').children().remove();
                                $('#purchaseBills').append(html);
                                html = '';
                            }
                            else {
                                $('#totalSupplierBills').html(response.Purchase.length);
                                $('#SupplirecashInAccount').html((response.AvailableAmount).toFixed(2));
                                $('#supplierPendingAmount').html((response.PendingAmount).toFixed(2));
                                var html = '';
                                $('#purchaseBills').children().remove();
                                $(response.Purchase).each(function () {
                                    html += '<li value="' + this.PurchaseId + '" class="invo-card row"><div class="col-xs-1 text-center"><i class="fa ion-android-note fa-3x m-t-20"></i></div><div class="col-xs-3 text-center">';
                                    html += '<a href="/Purchase/Entry?UID=' + this.PurchaseId + '"><span class="badge m-t-5">' + this.InvoicNo + '</span></a>';
                                    html += '<div class="text-muted m-t-5">Dated : ' + this.PurchaseDate + '</div>';
                                    html += ' <h4 class="m-0">Net Amount :<label>' + this.NetAmount + '</label></h4></div>';
                                    html += ' <div class="col-xs-4 text-center"><h3 class="m-t-30">Balance :<label class="li-balance hidden">' + this.BalanceAmount + '</label><label class="li-new-balance">' + this.BalanceAmount + '</label></h3></div>';
                                    html += '<div class="col-xs-3 text-center"><label class="control-label">Amount</label><input class="li-ind-amount" type="number" /><div class="voucher-group"></div></div>'
                                    html += '<div class="col-xs-1 text-center"><i class="ion-ios7-circle-outline pay-checkbox"></i><input type="checkbox" class="li-checkbox hidden" /><button type="button" class="btn btn-sm btn-settle" >SETTLE</button></div>';
                                    html += '</li>';

                                });
                                html += '<ul class="voucher-list hidden"><li><label>Vouchers</label><span class="voucher-close pull-right"><i class="ion ion-close-circled"></i></span><input id="hdCurrentBillPurchase" type="hidden" value=""/></li>';
                                if (response.Vouchers.length > 0) {
                                    $(response.Vouchers).each(function () {
                                        html += '<li value="' + this.Id + '"><span class="text-muted">' + this.Date + '</span><span class="voucherNo text-info">#' + this.Type + '</span><h4 class="voucherAmt">' + this.Amount + '</h4><span class="voucherSelect">Use</span></li>';
                                    });
                                }
                                else {
                                    html += '<li><p>Sorry. No available voucher found</p></li>';
                                }

                                html += '</ul>';

                                //$('#salesBills').children().remove();
                                $('#purchaseBills').append(html);
                                html = '';
                            }
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); },
                    });
                }
                else {
                    errorField('#supplierDropdown');
                }
            });
            //Show Voucher List
            $('body').on('click', '.supplier-div .btn-settle', function () {
                var liPos = $(this).closest('li').position().top + 10;
                $('.supplier-div #purchaseBills').addClass('with-voucher');
                $(this).closest('ul').find(' ul.voucher-list').css('top', '' + liPos + 'px').removeClass('hidden');
                $('#hdCurrentBillPurchase').val($(this).closest('li').val());
            });
            //Hide Voucher List
            $('body').on('click', '.supplier-div .voucher-close', function () {
                $('.supplier-div #purchaseBills').removeClass('with-voucher');
                $(this).closest('ul').addClass('hidden');
            });

            //Added to load Page according to querystring 
            var Option = getUrlVars();
            if (Option.type != undefined && !isNaN(Option.type)) {
                $('.drpTest').val(Option.type);
                $('.drpTest').trigger('change');
                if(Option.type==0){
                    if (Option.custid != undefined && !isNaN(Option.custid)) {
                        //If Customer ID is Specified   
                        $('#customerDropdown').val(Option.custid);
                        $('#searchCustomer').trigger("click");
                    }
                }
                else if (Option.type == 1) {
                    if (Option.supid != undefined && !isNaN(Option.supid)) {
                        $('#supplierDropdown').val(Option.supid);
                        $('#searchSupplier').trigger('click');
                    }
                }
            }

            //Allocate Voucher to Invoice
            $('body').on('click', '.supplier-div .voucherSelect', function () {

                var vouchAmt = parseFloat($(this).closest('li').find(' .voucherAmt').text());
                var vouchNo = $(this).closest('li').find('.voucherNo').text();
                var id = $(this).closest('li').val();
                if (isNaN(vouchAmt) || vouchAmt == '0') {

                }
                else {
                    var currentBill = $('#purchaseBills').children('li[value=' + $('#hdCurrentBillPurchase').val() + ']');
                    var originalBal = parseFloat(currentBill.find('.li-balance').text());
                    var currentVoucherAmount = 0;
                    var requiredBal = 0;
                    currentBill.find('.voucher-group > span.badge').each(function () {
                        currentVoucherAmount += parseFloat($(this).text());
                    });
                    requiredBal = originalBal - currentVoucherAmount;
                    if (requiredBal == 0) {
                        //do nothing
                    }
                    else if (requiredBal < vouchAmt) {
                        $('#purchaseBills').children('li[value=' + $('#hdCurrentBillPurchase').val() + ']').find(' .voucher-group').append('<span id="' + id + '" class="badge badge-sm" title="Voucher ' + vouchNo + '">' + requiredBal + '</span>');
                        $(this).closest('li').find(' .voucherAmt').text(vouchAmt - requiredBal);
                        currentBill.find(' .li-ind-amount').val(0);
                    }
                    else {
                        $('#purchaseBills').children('li[value=' + $('#hdCurrentBillPurchase').val() + ']').find('.voucher-group').append('<span id="' + id + '" class="badge badge-sm" title="Voucher ' + vouchNo + '">' + vouchAmt + '</span>');
                        $(this).closest('li').find(' .voucherAmt').text('0');
                        //finding current voucher after adding the new one
                        currentVoucherAmount = 0;
                        currentBill.find('.voucher-group > span.badge').each(function () {
                            currentVoucherAmount += parseFloat($(this).text());
                        });
                        currentBill.find(' .li-ind-amount').val(originalBal - currentVoucherAmount);
                    }
                    currentBill.find(' .li-checkbox').prop('checked', true).trigger('change');
                    resetBal2();
                    $('#txtSupplierPayAmount').val(findPayable2());
                    $('#selectedSupplierBills').html(findSelectedBills2());
                    $(this).text('Used');
                }
            })
            //Remove Allocated Voucher from Invoice
            $('body').on('click', '.supplier-div .voucher-group > .badge', function () {
                $(this).remove();
                var vouchId = $(this).attr('id');
                var amt = (parseFloat($(this).text()));
                var bal = parseFloat($('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text());
                $('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text(bal + amt);
                resetBal2();
                $('#txtSupplierPayAmount').val(findPayable2());
                $('#selectedSupplierBills').html(findSelectedBills2());
            });


            //Date picker init
            $('supplier-div .date-picker').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            //reset main panel
            resetMainPanel2();
            //Invoice Pay Chechbox Toggle
            $(document).on('click', '.supplier-div .pay-checkbox', function () {
                console.log($(this).hasClass('ion-ios7-circle-outline'));
                if ($(this).hasClass('ion-ios7-circle-outline')) {
                    $(this).removeClass('ion-ios7-circle-outline').addClass('ion-ios7-checkmark-outline').next('input[type=checkbox]').prop('checked', true);
                    $(this).closest('li').find('.li-ind-amount').val($(this).closest('li').find('.li-balance').html());
                    resetBal2();
                    $('#txtSupplierPayAmount').val(findPayable2());
                    $('#selectedSupplierBills').html(findSelectedBills2());
                }
                else if ($(this).hasClass('ion-ios7-checkmark-outline')) {

                    $(this).removeClass('ion-ios7-checkmark-outline').addClass('ion-ios7-circle-outline').next('input[type=checkbox]').prop('checked', false);
                    $(this).closest('li').find('.li-ind-amount').val('');
                    resetBal2();
                    $('#txtSupplierPayAmount').val(findPayable2());
                    $('#selectedSupplierBills').html(findSelectedBills2());
                }
            });

            $('body').on('change', '.supplier-div .li-checkbox', function () {
                var checkbox = $(this).closest('li').find('.pay-checkbox');
                if ($(this).prop('checked')) {
                    checkbox.removeClass('ion-ios7-circle-outline').addClass('ion-ios7-checkmark-outline');
                }
                else {
                    checkbox.addClass('ion-ios7-circle-outline').removeClass('ion-ios7-checkmark-outline');
                }
            });

            //Pay All
            $('#payAllSupp').off().click(function () {

                $('.supplier-div .li-ind-amount').val('');
                resetAllcheckBoxes2();
                checkAllcheckboxes2();
                var bills = $('#purchaseBills').children('li');
                var payingAmount = 0;
                for (var i = 0; i < bills.length; i++) {
                    var voucher = $(bills[i]).find('.voucher-group > span.badge');
                    var vouchId = voucher.attr('id');
                    var voucherAmt = parseFloat(voucher.text());
                    var voucherBal = parseFloat($(' .voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text());
                    $('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text(voucherBal + voucherAmt);
                    voucher.remove();
                    var bal = parseFloat($(bills[i]).find('.li-balance').html()).toFixed(2);
                    $(bills[i]).find('.li-ind-amount').val(bal);
                    payingAmount += parseFloat(bal);
                }
                resetBal2();
                $('#txtSupplierPayAmount').val(payingAmount);
                $('#selectedSupplierBills').html(findSelectedBills2());
            });


            $('body').on('keyup', '.supplier-div .li-ind-amount', function () {

                if ($(this).val() == '' || $(this).val() == null || isNaN($(this).val())) {
                    $(this).closest('li').find('.pay-checkbox').removeClass('ion-ios7-checkmark-outline').addClass('ion-ios7-circle-outline').next('input[type=checkbox]').prop('checked', false);
                }
                else {
                    $(this).closest('li').find('.pay-checkbox').removeClass('ion-ios7-circle-outline').addClass('ion-ios7-checkmark-outline').next('input[type=checkbox]').prop('checked', true);
                }
                resetBal2();
                $('#txtSupplierPayAmount').val(findPayable2());
                $('#selectedSupplierBills').html(findSelectedBills2());
            });

            //Allocate All
            $('#allocateSupp').off().click(function () {
                $('.supplier-div .li-ind-amount').val('');
                resetBal2();
                resetAllcheckBoxes2();
                var bills = $('#purchaseBills').children('li');
                var amountToPay = 0;
                var payingAmount = parseFloat($('#txtSupplierPayAmount').val());
                if (payingAmount > 0) {
                    for (var i = 0; i < bills.length; i++) {
                        var voucher = $(bills[i]).find(' .voucher-group > span.badge');
                        var vouchId = voucher.attr('id');
                        var voucherAmt = parseFloat(voucher.text());
                        var voucherBal = parseFloat($(' .voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text());
                        $('.voucher-list').find('li[value=' + vouchId + ']').find('.voucherAmt').text(voucherBal + voucherAmt);
                        voucher.remove();
                        var bal = parseFloat($(bills[i]).find('.li-balance').html()).toFixed(2);
                        if (bal <= payingAmount) {
                            $(bills[i]).find(' .li-ind-amount').val(bal);
                            $(bills[i]).find('.li-checkbox ').prop('checked', true).trigger('change');
                            $(bills[i]).find(' .li-new-balance').html('0.00');
                            payingAmount -= bal;
                        }
                        else {
                            $(bills[i]).find(' .li-ind-amount').val(payingAmount.toFixed(2));
                            $(bills[i]).find('.li-checkbox ').prop('checked', true).trigger('change');
                            $(bills[i]).find(' .li-new-balance').html(bal - payingAmount);
                            payingAmount = 0.00;
                            break;
                        }
                    }
                    $('#selectedSupplierBills').html(findSelectedBills2());
                }
                else {
                    $('#selectedSupplierBills').html(findSelectedBills2());
                }
            });
        });



        //findSelected Bills
        function findSelectedBills2() {
            var count = 0;
            $('.supplier-div .li-checkbox').each(function () {
                if ($(this).prop('checked')) {
                    count++;
                }
            });
            return count;
        }

        //uncheck all checkboxes
        function resetAllcheckBoxes2() {
            $('.supplier-div .li-checkbox').each(function () {
                $(this).prop('checked', false).trigger('change');
            });
        }

        //check all checkboxes
        function checkAllcheckboxes2() {
            $('.supplier-div .li-checkbox').each(function () {
                $(this).prop('checked', true).trigger('change');
            });
        }

        //find payable amount
        function findPayable2() {
            var payable = 0;
            $('.supplier-div .li-ind-amount').each(function (index) {
                var amount = $(this).val() != null && $(this).val() != '' && !isNaN($(this).val()) ? parseFloat($(this).val()) : 0;
                payable += amount;
            });
            return payable;
        }

        //reset balances
        function resetBal2() {
            $('.supplier-div .li-ind-amount').each(function () {
                var amount = $(this).val() != null && $(this).val() != '' && !isNaN($(this).val()) ? parseFloat($(this).val()) : 0;
                var originalBal = parseFloat($(this).closest('li').find('.li-balance').html());
                var currentVoucherAmount = 0;
                $(this).closest('li').find('.voucher-group > span.badge').each(function () {
                    currentVoucherAmount += parseFloat($(this).text());
                });

                $(this).closest('li').find('.li-new-balance').html((originalBal - (amount + currentVoucherAmount)).toFixed(2));
            });
        }
        //reset main panel
        function resetMainPanel2() {
            $('#selectedSupplierBills').text('0');
            $('#totalSupplierBills').text('0');
            $('#supplierPendingAmount').text('0.00');
            $('#SupplirecashInAccount').text('0.00');
            $('#txtSupplierPayAmount').val('0.00');
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('.supplier-div input.date-picker').datepicker('setDate', today);
        }
    </script>

    <%--Save--%>
    <script>
        $(document).ready(function () {
            $('#txtChequeDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            $('#btnSave').click(function () {
                var payAmount = 0;
                var Type = '';
                if ($('.drpTest').val() == '0') {
                    payAmount = $('#txtPayAmount').val();
                    Type = 'Customer Receipt';
                }
                else {
                    Type = 'Supplier Payment';
                    payAmount = $('#txtSupplierPayAmount').val();
                }
                $('#paymentCash').text('AED ' + payAmount);
                $('#amounttoPay').val(payAmount);
                $('#voucherType').text(Type);
                $('#paymentModal').modal('show');


            });


            $('input[name=radioInline]').change(function () {
                if ($('input[name=radioInline]:checked').val() == 'cheque') {
                    $('#chequeDiv').slideDown('slow');
                }
                else {
                    $('#txtBank,#txtChequeNumber,#txtChequeDate,#txtDrawOn').val('');
                    $('#chequeDiv').slideUp('slow');
                }
            });

            $('#btnSaveConfirmed').click(function () {
                var partyId;
                var payingAmount = 0;
                var type;
                var bills = [];
                if ($('#billType').val() == '0') {
                    type = 'Customer Receipt';
                    partyId = $('#customerDropdown').val();
                    var lis = $('#salesBills').children('li');
                    $(lis).each(function () {
                        if ($(this).find('.li-checkbox').prop('checked')) {
                            var bill = {};
                            bill.BillNo = $(this).val();
                            bill.Vouchers = [];
                            var voucher = {};
                            voucher.Id = 0;
                            voucher.Amount = parseFloat($(this).find('.li-ind-amount').val());
                            payingAmount += parseFloat($(this).find('.li-ind-amount').val());
                            bill.Vouchers.push(voucher);
                            //getting attached vouchers
                            $($(this).find('.voucher-group span.badge')).each(function () {
                                voucher = {};
                                voucher.Id = parseInt($(this).attr('id'));
                                voucher.Amount = parseFloat($(this).text());
                                bill.Vouchers.push(voucher);
                            });
                            bills.push(bill);
                        }

                    });
                }
                else {
                    partyId = $('#supplierDropdown').val();
                    type = 'Supplier Payment';
                    var lis = $('#purchaseBills').children('li');
                    $(lis).each(function () {
                        if ($(this).find('.li-checkbox').prop('checked')) {
                            var bill = {};
                            bill.BillNo = $(this).val();
                            bill.Vouchers = [];
                            var voucher = {};
                            voucher.Id = 0;
                            voucher.Amount = parseFloat($(this).find('.li-ind-amount').val());
                            payingAmount += parseFloat($(this).find('.li-ind-amount').val());
                            bill.Vouchers.push(voucher);
                            //getting attached vouchers
                            $($(this).find('.voucher-group span.badge')).each(function () {
                                voucher = {};
                                voucher.Id = parseInt($(this).attr('id'));
                                voucher.Amount = parseFloat($(this).text());
                                bill.Vouchers.push(voucher);
                            });
                            bills.push(bill);
                        }

                    });
                }
                var Data = { Type: type, PartyId: partyId, Bills: bills };
                Data.Narration = $('#txtNarration').val();
                Data.Bank = $('#txtBank').val();
                Data.ChequeNumber = $('#txtChequeNumber').val();
                Data.ChequeDate = $('#txtChequeDate').val();
                Data.DrawOn = $('#txtDrawOn').val();
                Data.PayBank = $('#ddlPaymentBank').val();
                Data.PayingAmount = payingAmount;
                Data.UserId = $.cookie('bsl_3');
                Data.Date = $('.pay-date').val();
                if ($('input[name=radioInline]:checked').val() == 'cheque') {
                    Data.PayBy = "Cheque";
                }
                else {
                    Data.PayBy = "Cash";
                }
                var AllocateURL = ''
                if (Data.Type == "Customer Receipt") {
                    AllocateURL = 'api/SettleBills/SettleBillsCustomer';
                }
                else {
                    AllocateURL = 'api/SettleBills/SettleBillsSupplier';
                }
                console.log(Data);
                console.log(AllocateURL);
                if ($('input[name=radioInline]:checked').val() == 'cheque' && ($('#txtChequeNumber').val() == "" || $('#txtChequeDate').val() == "" || $('#txtDrawOn').val() == "" || $('#ddlPaymentBank').val() == "0")) {
                    errorAlert('Please Fill The required Details');
                }
                else {
                    $.ajax({
                        url: $('#hdApiUrl').val() + AllocateURL,
                        method: 'POST',
                        dataType: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify(Data),
                        success: function (data) {

                            //resetAll();
                            successAlert('Payment Done Successfully');
                            resetAll();
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }


            });
        });

        function resetAll() {
            $('#paymentModal').modal('hide');
            reset();
            resetMainPanel();
            resetAllcheckBoxes2();
            $('#salesBills').children().remove();
            $('#purchaseBills').children().remove();
        }
    </script>

    <script src="/Theme/Custom/Commons.js"></script>
    <script src="../Theme/Sections/Supplier.js"></script>
    <script src="../Theme/Sections/Customer.js"></script>
</asp:Content>
