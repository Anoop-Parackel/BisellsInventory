<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpeningStock.aspx.cs" Inherits="BisellsERP.Settings.Inventory.OpeningStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mini-stat.clearfix.bx-shadow {
            height: 90px;
        }

        .print-float {
            position: fixed;
            bottom: 15px;
            right: 35px;
            height: 60px;
            width: 60px;
            border-radius: 50%;
        }

            .print-float i {
                font-size: 1.6em;
                color: #263238;
            }

        .l-font {
            font-size: 1.5em;
        }

        .light-font-color {
            color: #607D8B;
        }

        .panel {
            margin-bottom: 10px;
        }

        .view-h {
            max-height: 50vh;
            overflow: auto;
        }

        tbody tr td {
            padding: 5px !important;
            font-size: smaller;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 30px;
        }

        .edit-value {
            background-color: transparent;
            width: 40px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="container-fluid">
        <%--hidden fileds--%>
        <input type="hidden" id="hdId" value="0" />
        <%-- ---- Page Title ---- --%>
        <div class="row p-b-5">
            <div class="col-sm-4">
                <h3 class="page-title">Set Opening Stock</h3>
            </div>
            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group">
                    <button type="button" accesskey="f"  id="btnFind" data-toggle="tooltip" data-placement="bottom" title="Find Previous Opening Stock" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                    <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new stock . Unsaved data will be lost." class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                    <button type="button" accesskey="s" id="btnSave" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                    <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion ion-trash-b"></i></button>
                </div>
            </div>
        </div>
        <%-- ---- Search Quote Panel ---- --%>
        <div class="row search-quote-panel">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="panel b-r-8">
                            <div class="panel-body">
                                <div class="col-sm-7">
                                    <label class="title-label">Add to list from here..</label>
                                    <input type="text" id="txtChooser" autocomplete="off" class="form-control" placeholder="Choose Item" />
                                    <div id="lookup">
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                     <input type="text" id="txtQuantity" autocomplete="off" class="form-control" placeholder="Qty" />
                                </div>
                                <div class="col-sm-2 text-center">
                                    <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List" class="btn btn-icon btn-primary"><i class="ion-plus"></i></button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="panel b-r-8">
                            <div class="panel-body">
                                <div class="col-sm-6">
                                    <label class="title-label">Supplier</label>
                                    <asp:DropDownList ID="ddlSupplier" ClientIDMode="Static" CssClass="form-control round-no-border" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="panel b-r-8">
                            <div class="panel-body" style="padding-top: 7px; padding-bottom: 7px;">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <span>Order No :</span>
                                        <asp:Label ID="lblOrderNo" ClientIDMode="Static" runat="server" CssClass="badge badge-danger pull-right" Text="856542"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div>
                                        <div class="col-sm-12">
                                            <span>Date : </span>
                                            <input type="text" id="txtEntryDate" style="width: 60%;" class="date-info" value="01/Oct/2017" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- ---- Data Table ---- --%>
        <div class="row data-table">
            <div class="col-sm-12">
                <div class="panel panel-default view-h b-r-8">
                    <div class="panel-body p-t-10">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th class="hidden">Item ID</th>
                                            <th class="hidden">Po Id</th>
                                            <th>Item Name</th>
                                            <th>Item Code</th>
                                            <th>Tax%</th>
                                            <th>MRP</th>
                                            <th>Rate</th>
                                            <th>Order Qty</th>
                                            <th>Qty</th>
                                            <th>SP </th>
                                            <th>Tax</th>
                                            <th>Gross</th>
                                            <th>Net</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- ---- Summary Panel ---- --%>
        <div class="row summary-panel">
            <div class="col-sm-9 col-lg-10">
                <div class="mini-stat clearfix bx-shadow b-r-8">
                    <div class="row">
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Total Items</label>
                            <%-- Total Items --%>
                            <label id="lblTotalItem" class="l-font"></label>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Gross Amt</label>
                            <%-- Gross Amount --%>
                              <label id="lblGross" class="l-font"></label>
                        </div>
                        <div class="col-sm-2 text-center">

                            <label class="w-100 light-font-color">Total Amt</label>
                            <%-- Total Amount --%>
                              <label id="lblTotal" class="l-font"></label>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Tax Amt</label>
                            <%-- Tax Amount --%>
                             <label id="lblTax" class="l-font"></label>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Round Off</label>
                            <%-- Round Off --%>
                              <asp:TextBox ID="txtRoundOff" AutoComplete="off" CssClass="w-100 l-font" style="border:none;text-align:center;background-color:transparent;font-size:20px"  ClientIDMode="Static" runat="server" placeholder="RoundOff">0.00</asp:TextBox>
                         </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Narration</label>
                            <%-- Narrartion --%>
                            <a href="#" class="btn-narration"><i class="ion ion-ios7-paper-outline"></i></a>
                            <div class="narration-box" style="display: none">
                                <%-- ASP Textbox for narration --%>
                                <textarea id="txtNarration" class="w-100" rows="3" placeholder="Enter narration here.."></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-3 col-lg-2">
                <%-- NET AMOUNT --%>
                <div class="mini-stat clearfix bx-shadow b-r-8">
                    <div class="col-sm-2"><span class="currency">$</span></div>
                    <div class="col-sm-10">
                        <h3 class="text-right text-primary m-0">
                            <label id="lblNet" class="counter"></label>
                        </h3>
                        <div class="mini-stat-info text-right text-muted">Net Amount</div>
                    </div>

                </div>
            </div>
        </div>

    </div>
    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">
            <!-- Modal content-->
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                     <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Opening Stock &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
                    <div id="filterWrap" class="hide">
                        <label>Filter by Supplier :</label>
                        <asp:DropDownList ID="ddlSupplierFilter" ClientIDMode="Static" CssClass="" runat="server"></asp:DropDownList>
                        <label>Filter by Date Range :</label>
                        <input type="text" name="daterange" class="form-control" id="txtDate" value="01/01/2015 - 01/31/2015" />
                        <div class="btn-toolbar pull-right m-t-10 m-b-10">
                            <button id="applyFilter" type="button" class="btn btn-default btn-sm">Apply Filter</button>
                            <button id="filterCancel" type="button" class="btn btn-inverse btn-sm">x</button>
                        </div>
                    </div>
                </div>
                <div class="modal-body p-b-0">
                    <table id="tblRegister" class="table table-hover table-striped table-responsive table-scroll">
                        <thead>
                            <tr>
                                <th class="hidden">Po Id</th>
                                <th>Entry No</th>
                                <th>Date</th>
                                <th>Supplier</th>
                                <th>Tax</th>
                                <th>Gross</th>
                                <th>Net Amount</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <%--find list modal ends here--%>

<script>
        //calculate function and calculate summary function call
        setInterval(calculateSummary, 1000);
        setInterval(calculation, 1000);

    //function calculation starts here
        function calculation()
        {
            var tr = $('#listTable>tbody').children('tr');
            for (var i = 0; i < tr.length; i++) {
                var tempTax = 0;
                var qty = parseFloat($(tr[i]).children('td:nth-child(9)').children('input').val());

                var gross = parseFloat($(tr[i]).children('td:nth-child(9)').children('input').val()) * parseFloat($(tr[i]).children('td:nth-child(7)').children('input').val());
                gross = parseFloat(gross.toFixed(2));//Gross Amount

                var taxper = parseFloat($(tr[i]).children('td:nth-child(5)').text());
                var tax = parseFloat($(tr[i]).children('td:nth-child(7)').children('input').val()) * (taxper / 100);
                tax = parseFloat(tax.toFixed(2));//Tax Amount

                var net = gross + (tax * parseFloat($(tr[i]).children('td:nth-child(9)').children('input').val()));
                net = parseFloat(net.toFixed(2));//Net amount
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax.toFixed(2));
                $(tr[i]).children('td:nth-child(12)').text(gross); //gross amount
                $(tr[i]).children('td:nth-child(11)').text(tempTax);  //tax amount
                $(tr[i]).children('td:nth-child(13)').text(net);  //net amount
                qty = 0;
            }
        }
        //calculation function ends here

        //calculateSummary function starts here
        function calculateSummary()
        {

            var tr = $('#listTable > tbody').children('tr');
            var tax = 0;
            var gross = 0;
            var net = 0;
            var round = 0.0;
            var temp = 0;
            for (var i = 0; i < tr.length; i++)
            {
                tax += parseFloat($(tr[i]).children('td:nth-child(11)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(12)').text());
                net += parseFloat($(tr[i]).children('td:nth-child(13)').text());
            }
            temp = net;
            if (JSON.parse($('#hdSettings').val()).AutoRoundOff)
            {
                var roundoff = Math.round(net) - net;
                net = Math.round(net);
                roundoff = parseFloat(roundoff.toFixed(2));
                $('#txtRoundOff').val(roundoff);
            }
            else
            {
                var roundoff = parseFloat($('#txtRoundOff').val());
                net = net + parseFloat($('#txtRoundOff').val());
                net = net.toFixed(2);
            }
            var le = tr.length;
            $('#lblTotalItem').text(le);
            tax = parseFloat(tax.toFixed(2));
            gross = parseFloat(gross.toFixed(2));
            $('#lblGross').text(gross);
            $('#lblNet').text(net);
            $('#lblTax').text(tax);
            $('#lblTotal').text(gross);
        }
        //calculate summary function ends here
    </script>

    <script>
        //document ready function starts here
        $(document).ready(function ()
        {
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            //Getting Entry for edit purpose if user asked for it
            var Params = getUrlVars();
            if (Params.UID != undefined || !isNaN(Params.UID)) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/SetOpeningMenu/get/' + Params.UID,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        try
                        {
                            var supplierId = $('#ddlSupplier').val();
                            var html = '';
                            var register = response;
                            $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                            $('#txtDueDate').datepicker("update", new Date(register.InvoiceDateString));
                            $('#txtNarration').val(register.Narration);
                            $('#ddlSupplier').val(register.SupplierId);
                            $('#hdId').val(register.ID);
                            html = '';
                            $(register.Products).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td style="display:none">' + this.QuoteDetailId + '</td>';
                                html += '<td>' + this.Name + '</td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td>' + this.TaxPercentage + '</td>';
                                html += '<td><input type="number" class="edit-value" value="' + this.MRP + '"/></td>';
                                html += '<td><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                html += '<td>' + this.Quantity + '</td>';
                                html += '<td><input type="number" class="edit-value" value="' + this.Quantity + '"/>';
                                html += '<td><input type="number" class="edit-value" value="' + this.SellingPrice + '"/></td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';

                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                html += '</tr>';
                            });
                            $('#lblGross').val(register.Gross);
                            $('#lblTotalAmount').val(register.Gross);
                            $('#lblTaxAmount').val(register.Gross);
                            $('#lblNetAmount').val(register.NetAmount);
                            $('#txtRoundOff').val(register.RoundOff);
                            $('#listTable tbody').append(html);
                            $('#lblOrderNo').text(register.EntryNo);
                            $('#btnSave').html('<i class="ion-archive"></i>Update');
                            $('#findModal').modal('hide');
                            //binding delete
                            $('.delete-row').click(function () {
                                $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                            });
                        }
                        catch(err)
                        {
                            console.log(err);
                            errorAlert('Entry not found');
                        }
                        finally {
                            loading('stop', null);
                        }
                     
                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr); },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });
            }

            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('[data-toggle="popover"]').popover
            ({
                content: "<textarea placeholder=\" Enter Narration Here\"></textarea>"
            });
            $('#txtDueDate').datepicker
            ({
                autoClose: true,
                format: 'dd/M/yyyy',
                startDate: today,
                todayHighlight: true
            });
            $('#txtEntryDate').datepicker
            ({
                autoClose: true,
                format: 'dd/M/yyyy',
                startDate: today,
                todayHighlight: true
            });

            $('#txtEntryDate').datepicker('setDate', today);
            $('#txtDueDate').datepicker('setDate', today);

            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtEntryDate').datepicker()
           .on('changeDate', function (ev)
           {
               $('#txtEntryDate').datepicker('hide');
           });

            //lookup initialization
            var companyId = $.cookie('bsl_1');
            lookup({
                textBoxId: 'txtChooser',
                url: $('#hdApiUrl').val() + 'api/Search/Items?CompanyId=' + companyId + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'SellingPrice'],
                visibility: [false, false, true, true, true, true, true],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate'],
                key: 'ItemID',
                dataToShow: 'Name',
                OnLoading: function () { miniLoading('start') },
                OnComplete: function () { miniLoading('stop') }
               });
            //lookup initialization ends here

            //function for add products to the table 
            function AddToList()
            {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var taxper = parseFloat(tempItem.TaxPercentage);
                var rate = $('#listTable > tbody').children('td').eq(6).children('input').val();
                var qty = parseFloat($('#txtQuantity').val());
                var TaxAmount = parseFloat(((rate * (taxper / 100)) * qty).toFixed(2));
                var GrossAmount = parseFloat((qty * rate).toFixed(2));
                var NetAmount = parseFloat((GrossAmount + TaxAmount).toFixed(2));
                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null)
                {

                    var Rows = $('#listTable > tbody').children('tr');
                    var itemExists = false;
                    var rowOfItem;
                    $(Rows).each(function ()
                    {
                        var pedId = $(this).children('td').eq(0).text();
                        if (tempItem.ItemID == pedId)
                        {
                            itemExists = true;
                            rowOfItem = this;
                        }

                    });

                    if (itemExists)
                    {
                        var existingQty = parseFloat($(rowOfItem).children('td').eq(8).children('input').val());
                        var newQty = existingQty + qty;
                        $(rowOfItem).children('td').eq(8).replaceWith('<td><input type="number" class="edit-value" value="' + newQty + '"/></td>');
                        sessionStorage.removeItem('tempItem');
                        $('#txtQuantity').val('');
                        $('#txtChooser').val('');
                        $('#txtChooser').focus();
                        highlightRow(rowOfItem, '#ffda4d');
                        //Binding Event to remove button
                        $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                        $('#lblquantityError').text('Quantity Updated').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                    }
                    else
                    {
                        html = '';
                        html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td style="display:none">0</td><td>' + tempItem.Name + '</td><td>' + tempItem.ItemCode + '</td><td>' + tempItem.TaxPercentage + '</td><td><input type="number" class="edit-value" value="' + tempItem.MRP + '"/></td><td><input type="number" class="edit-value" value="' + tempItem.SellingPrice + '"/></td><td>0</td><td>  <input type="number" class="edit-value" value="' + qty + '"/> </td><td> <input type="number" class="edit-value" value="0"/></td><td>' + TaxAmount + '</td><td>' + GrossAmount + '</td><td>' + NetAmount + '</td><td style="display:none">0</td><td>  <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
                        $('#listTable > tbody').append(html);
                        sessionStorage.removeItem('tempItem');
                        $('#txtQuantity').val('');
                        $('#txtChooser').val('');
                        $('#txtChooser').focus();
                        //Binding Event to remove button
                        $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                    }
                }

            }
            //addtolist function ends here

            //AddToList function call
            $('#btnAdd').click(function ()
            {
                AddToList();
            });

            //Add Item to list with enter key           
            $('#txtQuantity').keypress(function (e)
            {

                if (e.which == 13)
                {
                    e.preventDefault();
                    AddToList();
                }

            });

            //Save function call
            $('#btnSave').off().click(function ()
            {
                save();
            });

            //Function for Saving the register
            function save()
            {

                swal
                    ({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                    
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                function (isConfirm)
                {
                    if (isConfirm)
                    {
                        var data = {};
                        var arr = [];
                        var tbody = $('#listTable > tbody');
                        var tr = tbody.children('tr');
                        var entryDate = $('#txtEntryDate').val();
                        var invoicedate = $('#txtDueDate').val();
                        var narration = $('#txtNarration').val();
                        var rOff = $('#txtRoundOff').val();
                        var LocationId = $.cookie('bsl_2');
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var createdBy = $.cookie('bsl_3');

                        for (var i = 0; i < tr.length; i++)
                        {
                            var itemId = $(tr[i]).children('td').eq(0).text();
                            var pqdId = $(tr[i]).children('td').eq(13).text();
                            var poId = $(tr[i]).children('td').eq(1).text();
                            var mrp = $(tr[i]).children('td').eq(5).children('input').val();
                            var rate = $(tr[i]).children('td').eq(6).children('input').val();
                            var qty = $(tr[i]).children('td').eq(8).children('input').val();
                            var IsMigrated;
                            var sp = $(tr[i]).children('td').eq(9).children('input').val();
                            var detail = { "Quantity": qty };
                            detail.ItemID = itemId;
                            detail.QuoteDetailId = pqdId;
                            detail.MRP = mrp;
                            detail.CostPrice = rate;
                            detail.SellingPrice = sp;
                            detail.QuoteDetailId = poId;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.SupplierId = $('#ddlSupplier').val();
                        data.EntryDate = entryDate;
                        data.RoundOff = rOff;
                        data.IsMigrated = true;
                        data.EntryDateString = entryDate;
                        data.EntryDate = entryDate;
                        data.InvoiceDateString = invoicedate;
                        data.InvoiceDate = invoicedate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.UserId = $.cookie('bsl_3');
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/SetOpeningMenu/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response)
                            {
                                if (response.Success)
                                {
                                    successAlert(response.Message);
                                    resetRegister();
                                    $('#lblOrderNo').text(response.Object.OrderNo);

                                }
                                else
                                {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (xhr)
                            {
                                alert(xhr.responseText);
                                console.log(xhr);
                                miniLoading('stop')

                            },
                            beforeSend: function () { miniLoading('start');},
                            complete: function () { miniLoading('stop'); },
                        });
                    }

                });
            }
            //Save Function Ends here

            //Find function starts here
            $('#btnFind').click(function ()
            {
                //Find Filter Function
                $(function () {
                    $('#findFilter').popover({
                        placement: 'bottom',
                        html: true,
                        content: $('#filterWrap').html()
                    }).on('click', function () {
                        //inititalize date range picker
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
                        //inititalize select 2 ddl
                        $("#ddlSupplierFilter").select2({
                            width: '100%'
                        });

                        // Apply Filter Click
                        $('#applyFilter').click(function () {
                            //Filter Logic Here
                            var supplierId = $('#ddlSupplierFilter').val() == '0' ? null : $('#ddlSupplierFilter').val();
                            var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                            var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                            refreshTable(supplierId, fromDate, toDate);
                            $('#findFilter').popover('hide');
                            $('body').on('hidden.bs.popover', function (e) {
                                $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                            });
                        })
                        // Cancel Filter Click
                        $('#filterCancel').click(function () {
                            $('#findFilter').popover('hide');
                            $('body').on('hidden.bs.popover', function (e) {
                                $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                            });
                        })
                    })
                });
                resetRegister();
                $('#findModal').modal('show');
                refreshTable(null, null, null);
                function refreshTable(supplierID, fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SetOpeningMenu/Get?SupplierId=' + supplierID + '&from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var supplierId = $('#ddlSupplier').val();
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.EntryNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.Supplier + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += this.status == 0 ? '<td><span class="label label-danger">In Active</span></td>' : '<td><span class="label label-default">Active</span></td>';
                                html += '<td><a class="edit-register" title="edit" href="#"><i class="fa fa-edit"></i></a></td>'
                                html += '</tr>';
                            });
                            //$('#tblRegister tbody').children().remove();
                            //$('#tblRegister tbody').append(html);
                            //$('#tblRegister').dataTable({ destroy: true });
                            //binding event to row
                            $('#tblRegister').DataTable().destroy();
                            $('#tblRegister tbody').children().remove();
                            $('#tblRegister tbody').append(html);
                            $('#tblRegister').dataTable({ destroy: true, aaSorting: [], "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]] });
                            $('#tblRegister').off().on('click', '.edit-register', function () {
                                resetRegister();
                                var registerId = $(this).closest('tr').children('td').eq(0).text();
                                var register = {};
                                $(response).each(function () {
                                    if (this.ID == registerId) {
                                        register = this;
                                    }
                                });

                                $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                                $('#txtDueDate').datepicker("update", new Date(register.InvoiceDateString));
                                $('#txtNarration').val(register.Narration);
                                $('#ddlSupplier').val(register.SupplierId);
                                $('#hdId').val(register.ID);
                                html = '';
                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td style="display:none">' + this.QuoteDetailId + '</td>';
                                    html += '<td>' + this.Name + '</td>';
                                    html += '<td>' + this.ItemCode + '</td>';
                                    html += '<td>' + this.TaxPercentage + '</td>';
                                    html += '<td><input type="number" class="edit-value" value="' + this.MRP + '"/></td>';
                                    html += '<td><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                    html += '<td>' + this.Quantity + '</td>';
                                    html += '<td><input type="number" class="edit-value" value="' + this.Quantity + '"/>';
                                    html += '<td><input type="number" class="edit-value" value="' + this.SellingPrice + '"/></td>';
                                    html += '<td>' + this.TaxAmount + '</td>';
                                    html += '<td>' + this.Gross + '</td>';
                                    html += '<td>' + this.NetAmount + '</td>';

                                    html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '</tr>';
                                });
                                $('#lblGross').val(register.Gross);
                                $('#lblTotalAmount').val(register.Gross);
                                $('#lblTaxAmount').val(register.Gross);
                                $('#lblNetAmount').val(register.NetAmount);
                                $('#txtRoundOff').val(register.RoundOff);
                                $('#listTable tbody').append(html);
                                $('#lblOrderNo').text(register.EntryNo);
                                $('#btnSave').html('<i class="ion-archive"></i>Update');
                                $('#findModal').modal('hide');
                                //binding delete
                                $('.delete-row').click(function () {
                                    $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                                });
                            });
                        },
                        error: function (xhr)
                        {
                            alert(xhr.responseText);
                            console.log(xhr);
                            loading('stop', null);
                        },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); },
                    });
                }
            });
            //find function ends here


            //BtnNew for reset the page
            $('#btnNew').click(function ()
            {
                resetRegister();
            });

            //Reset This Register
            function resetRegister()
            {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#ddlSupplier').prop('disabled', false);
                $('#txtEntryDate').datepicker('setDate', today);
                $('#txtDueDate').datepicker('setDate', today);
                $('#btnSave').html('<i class=\"ion-archive"\></i>Save');

            }
            //Reset ends here


            //Delete functionality
            $('#btnDelete').click(function ()
            {
                if ($('#hdId').val() != 0)
                {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",
                       
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,

                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    },
                    function (isConfirm)
                    {
                        var id = $('#hdId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/SetOpeningMenu/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response)
                                {
                                    if (response.Success)
                                    {
                                        successAlert(response.Message);
                                        resetRegister();
                                    }
                                    else
                                    {
                                        errorAlert(response.Message);
                                    }
                                },
                                error: function (xhr)
                                {
                                    alert(xhr.responseText);
                                    console.log(xhr);
                                    miniLoading('stop');
                                },
                                beforeSend: function () { miniLoading('start'); },
                                complete: function () { miniLoading('stop'); },
                            });
                        }

                    });
                }

            });
            //delete function ends here
        });

        //document ready function ends here
    </script>
    <link href="/Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="/Theme/assets/datatables/datatables.min.js"></script>
    <script src="/Theme/Custom/Commons.js"></script>
    <link href="/Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="/Theme/assets/sweet-alert/sweet-alert.min.js"></script>
            <!-- Date Range Picker -->
<%--    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />--%>
    
<script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>

</asp:Content>
