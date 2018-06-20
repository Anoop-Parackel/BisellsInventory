<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Build.aspx.cs" Inherits="BisellsERP.Purchase.Tools.Request.Build" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Purchase Quote Builder</title>
    <style>
        #wrapper {
            overflow: hidden;
        }
        .mini-stat.clearfix.bx-shadow {
            height: 70px !important;
        }

        .mini-stat {
            padding: 14px !important;
        }


        .summary-label-color {
            color: #607D8B;
            font-size: 12px;
        }

        .panel {
            margin-bottom: 10px;
        }

        .view-h {
            min-height: 65vh;
            max-height: 65vh;
        }

        .quote-date {
            border: none;
            color: #1e88e5;
            cursor: pointer;
            font-size: small;
            background-color: transparent;
            font-weight: bolder;
        }

        .order-num {
            width: 70%;
            font-size: 15px;
        }

        .pw-15 {
            padding-left: 15px;
            padding-right: 15px;
        }

        #btnAddToCart {
            padding-left: 20px;
            padding-right: 20px;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 30px;
        }

        #btnLoad {
            height: 30px;
            padding: 0 18px;
        }

        .btn-filter {
            background-color: whitesmoke;
            border: 1.2px solid rgba(239, 83, 80, .8);
            transition: all 300ms ease;
        }

            .btn-filter:hover, .btn-filter:active, .btn-filter:focus {
                box-shadow: none;
            }

            .btn-filter i {
                font-size: large;
                color: #ef5350;
            }

        .cart-wrap {
            position: relative;
            top: -5px;
            height: 35px;
            display: inline-block;
        }

            .cart-wrap img {
                height: 100%;
            }

            .cart-wrap .cart-no {
                position: absolute;
                top: -10px;
                left: 20px;
                font-size: smaller;
            }

            .cart-wrap .cart-name {
                position: relative;
                left: -23px;
                bottom: -11px;
                color: #5d5e60;
                font-weight: bold;
                font-size: small;
            }

        .mini-stat.clearfix.bx-shadow {
            height: 80px;
        }

        .qty-input {
            background-color: transparent;
            width: 80px;
            border: .4px solid rgba(167, 167, 167, .2);
            text-align: center;
            border-radius: 1em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">
        <%-- ---- Page Title, Button, Cart---- --%>
        <div class="row p-b-10 sub-page-title">
            <div class="col-sm-3 col-md-2">
                <h3 class="page-title m-t-0">Quote Builder</h3>
            </div>
            <div class="col-sm-7 col-md-8">
                <div id="cartsBar" style="z-index: 950"></div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="btn-toolbar pull-right" role="group" aria-label="...">
                    <button type="button" accesskey="s" id="btnSave"data-toggle="tooltip" data-placement="bottom" title="Save the current builder"class="btn btn-default waves-effect waves-light"><i class="ion-checkmark-circled"></i>&nbsp Save</button>
                    <button type="button" accesskey="n" id="btnNew"data-toggle="tooltip" data-placement="bottom" title="Start a new builder . Unsaved data will be lost"  class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp New</button>
                </div>
            </div>
        </div>
        <%-- ---- Search Quote Panel ---- --%>
        <div class="row search-quote-panel">
            <div class="col-sm-9">
                <div class="panel b-r-8">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-4 text-center">
                                <label class="title-label">Location</label>
                                <asp:DropDownList ID="ddlLocation" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3 text-center">
                                <label class="title-label">From Date</label>
                                <div class="input-inside-wrap">
                                    <asp:TextBox ID="txtFromDate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <a href="#" class="inside-btn-clear"><i class="fa fa-undo" onclick="$('#txtFromDate').val('')"></i></a>
                                </div>

                            </div>
                            <div class="col-sm-3 text-center">
                                <label class="title-label">To Date</label>
                                <div class="input-inside-wrap">
                                    <asp:TextBox ID="txtToDate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <a href="#" class="inside-btn-clear"><i class="fa fa-undo"></i></a>
                                </div>
                            </div>  
                            <div class="col-sm-2 text-center">
                                <button id="btnLoad" type="button" class="btn btn-rounded btn-filter"><i class="ion-social-buffer"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2 col-sm-offset-1">
                <div class="panel b-r-8">
                    <div class="panel-body" style="padding: 26px 10px">
                        <div class="col-sm-12">
                            <span>Date :</span>
                            <asp:TextBox ID="txtEntryDate" runat="server" ClientIDMode="Static" CssClass="date-info"></asp:TextBox>
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
                                <table id="listTable" class="table table-hover table-condensed table-striped">
                                    <thead>
                                        <tr>
                                            <th class="hidden">PurchaseRequestId</th>
                                            <th>Request No</th>
                                            <th>Date</th>
                                            <th>Due Date</th>
                                            <th style="text-align:right">No of Items</th>
                                            <th style="text-align:right" >Priority</th>
                                            <th style="text-align:right">Total Tax</th>
                                            <th style="text-align:right">Gross</th>
                                            <th style="text-align:right">Net Amount</th>
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
        <%-- ---- Modal for Quote Building ---- --%>
        <div id="myModal" class="modal animated fadeIn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-dialog-w-lg">
                <div class="modal-content modal-content-h-lg">
                    <div class="modal-header">
                        <div class="modal-title" id="myModalLabel">
                            <span>Request No:
                                <label id="mdlRequestId" class="badge badge-danger pw-15">32</label>
                            </span>
                            <span class="pull-right" style="padding-right: 5px">Request Date: 
                                <label id="mdlEntryDate" class="text-danger">xx</label>
                            </span>
                        </div>
                    </div>
                    <div class="modal-body modal-body-lg">
                        <table id="requestTable" class="table table-hover table-striped table-responsive">
                            <thead class="bg-blue-grey ">
                                <tr>
                                    <th class="hidden">PurchaseReqstDetailID</th>
                                    <th class="text-white">Item</th>
                                    <th class="text-white">Code</th>
                                    <th class="text-white">Mrp</th>
                                    <th class="text-white">Rate</th>
                                    <th class="text-white">Req Qty</th>
                                    <th class="text-white">Qty&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                    <th class="text-white">Tax</th>
                                    <th class="text-white">Gross</th>
                                    <th class="text-white">Net</th>
                                    <th class="hidden">TaxPerc</th>
                                    <th>
                                        <input type="checkbox" class="checkbox checkbox-primary chk-all" /></th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer modal-foo-lg">
                        <div class="row">
                            <div class="col-sm-3 col-md-5 text-left">
                                <h4 class="m-b-0">Total Items : &nbsp<label class="text-success" id="noOfItems"></label></h4>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:DropDownList runat="server" CssClass="form-control" ClientIDMode="Static" ID="ddlSupplier"></asp:DropDownList>
                            </div>
                            <div class="col-sm-5 col-md-3">
                                <div class="btn-toolbar pull-right">
                                    <button id="btnAddToCart" class="btn btn-primary waves-effect waves-light" aria-expanded="true" type="button"><i class="md md-add-shopping-cart"></i>Add</button>
                                    <button type="button" class="btn btn-inverse waves-effect waves-light" data-dismiss="modal" aria-hidden="true">x</button>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <%-- ---- Modal for Listing Selected Quotes ---- --%>
        <div id="quotesModal" class="modal animated fadeIn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog  modal-dialog-w-lg">
                <div class="modal-content  modal-content-h-lg">
                    <div class="modal-header">
                        <div class="modal-title">
                            <div class="row">
                                <div class="col-sm-4 col-md-3">
                                    <span>Location:
                                        <label id="lblLocation" class="badge badge-danger pw-15">xx</label>
                                    </span>
                                </div>
                                <div class="col-sm-5 col-md-6 text-center">
                                    <h4 id="lblSupplierName" class="text-danger m-0">Supplier Name</h4>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <div>
                                        <div class="row">
                                            <span class="" style="padding-right: 5px">Entry Date: 
                                           <label id="lblEntryDate" class="text-danger">xx</label>
                                            </span>
                                        </div>
                                        <div class="row">
                                            <span class="" style="padding-right: 5px">Due Date: 
                                    <asp:TextBox ID="txtMdlDueDate" runat="server" ForeColor="#f0615e" ClientIDMode="Static" Text="10/Dec/2017" CssClass="quote-date"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body modal-body-lg">
                        <table id="quotesList" class="table table-striped table-hover table-responsive">
                            <thead class="bg-blue-grey ">
                                <tr>
                                    <th class="hidden">PurchaseReqstDetailID</th>
                                    <th class="hidden">SupplierId</th>
                                    <th class="text-white">Item</th>
                                    <th class="text-white">Code</th>
                                    <th class="text-white">Mrp</th>
                                    <th class="text-white">Rate</th>
                                    <th class="text-white">Req Qty</th>
                                    <th class="text-white">Qty&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                    <th class="text-white">Tax</th>
                                    <th class="text-white">Gross</th>
                                    <th class="text-white">Net Amount</th>
                                    <th class="hidden">TaxPerc</th>
                                    <th class="text-white">#</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer modal-foo-lg">
                        <%-- ---- Summary Panel ---- --%>
                        <div class="row summary-panel">
                            <div class="col-sm-6 col-lg-6 ">
                                <div class="mini-stat clearfix bx-shadow b-r-10 m-0">
                                    <div class="row">
                                        <div class="col-sm-2 text-center">
                                            <label class="w-100 summary-label-color">Items</label>
                                            <%-- Total Items --%>
                                            <asp:Label ID="lblTotalItem" CssClass="l-font" ClientIDMode="Static" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 text-center">
                                            <label class="w-100 summary-label-color">Gross</label>
                                            <%-- Gross Amount --%>
                                            <asp:Label ID="lblGross" ClientIDMode="Static" CssClass="l-font" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 text-center">
                                            <label class="w-100 summary-label-color">Tax</label>
                                            <%-- Tax Amount --%>
                                            <asp:Label ID="lblTax" CssClass="l-font" runat="server" ClientIDMode="Static" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-sm-3 text-center">
                                            <label class="w-100 summary-label-color">Round Off</label>
                                            <%-- Round Off --%>
                                            <asp:Label ID="lblRound" CssClass="l-font" ClientIDMode="Static" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 text-center">
                                            <label class="w-100 summary-label-color">Narration</label>
                                            <%-- Narrartion --%>
                                            <a href="#" class="btn-narration"><i class="ion ion-ios7-paper-outline"></i></a>
                                            <div class="narration-box" style="display: none">
                                                <%-- ASP Textbox for narration --%>
                                                <asp:TextBox ID="txtNarration" CssClass="w-100" TextMode="MultiLine" Rows="3" ClientIDMode="Static" runat="server" placeholder="Enter narration here.."></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-lg-3">
                                <%-- NET AMOUNT --%>
                                <div class="mini-stat clearfix bx-shadow b-r-10 m-0">
                                    <h5 class="text-right text-primary m-0">
                                        <label>AED &nbsp</label><asp:Label ID="lblNet" ClientIDMode="Static" runat="server" CssClass="counter" Text="00"></asp:Label>
                                    </h5>
                                    <div class="mini-stat-info text-right text-muted">
                                        Net Amount
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-lg-3 ">
                                <div class="checkbox checkbox-danger checkbox-circle m-0 pull-right p-b-10">
                                    <asp:CheckBox ID="ChkPriority" ClientIDMode="Static" runat="server" />
                                    <label for="ChkPriority">
                                        High Priority
                                    </label>
                                </div>
                                <div class="btn-toolbar pull-right">
                                    <button id="btnUpdate" class="btn btn-primary waves-effect waves-light" aria-expanded="true" type="button"><i class="md md-shopping-cart"></i>Update</button>
                                    <button id="btnDelete" class="btn btn-danger waves-effect waves-light" type="button"><i class="md md-delete"></i></button>
                                    &nbsp
                                    <button class="btn btn-inverse waves-effect waves-light" data-dismiss="modal" aria-expanded="true" type="button">x</button>&nbsp
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        //CalculateSummary and calculation Function Call
        setInterval(calculateSummary, 1000);
        setInterval(calculation, 1000);
        // calculation function starts here
        function calculation()
        {
            var tr = $('#quotesList>tbody').children('tr');
            for (var i = 0; i < tr.length; i++)
            {
                var qty = !isNaN(parseFloat($(tr[i]).children('td:nth-child(8)').children('input').val())) ? parseFloat($(tr[i]).children('td:nth-child(8)').children('input').val()) : 0;
                var gross = qty * parseFloat($(tr[i]).children('td:nth-child(6)').text());
                gross = parseFloat(gross.toFixed(2));//Gross Amount

                var taxP = parseFloat($(tr[i]).children('td:nth-child(12)').text());
                var rate = parseFloat($(tr[i]).children('td:nth-child(6)').text());
                var tax = rate * (taxP / 100) * qty;
                tax = parseFloat(tax.toFixed(2));//Tax Amount

                var net = gross + (tax );
                net = parseFloat(net.toFixed(2));//Net amount

                $(tr[i]).children('td:nth-child(10)').text(gross); //gross amount
                $(tr[i]).children('td:nth-child(9)').text(tax);  //tax amount
                $(tr[i]).children('td:nth-child(11)').text(net);  //net amount
            }
        }
        //calculation summary function starts here
        function calculateSummary()
        {
            var tr = $('#quotesList > tbody').children('tr');
            var tax = 0.00;
            var gross = 0.00;
            var net = 0.00;
            for (var i = 0; i < tr.length; i++)
            {
                tax += parseFloat($(tr[i]).children('td:nth-child(9)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(10)').text())
                net += parseFloat($(tr[i]).children('td:nth-child(11)').text())
            }
            gross = parseFloat(gross.toFixed(2));
            net = parseFloat(net.toFixed(2));
            tax = parseFloat(tax.toFixed(2));
            var roundoff = Math.round(net) - net;
            net = Math.round(net);
            roundoff = parseFloat(roundoff.toFixed(2));
            $('#lblTotalItem').text(tr.length);
            $('#lblGross').text(gross.toString());
            $('#lblTax').text(tax);
            $('#lblNet').text(net);
            $('#lblRound').text(roundoff);
        }

    </script>
    <script>
        //documnet ready function starts here
        $(document).ready(function ()
        {
            //GLOBAL VARIABLES
            var deletedItems = [];

            //Reset on first load
            resetAll();
            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());          
            $('#txtEntryDate').datepicker('setDate', today);
            $('#txtEntryDate').datepicker({ format: 'dd/M/yyyy' });
            /* Date and Due Date */
            $('#txtFromDate, #txtToDate, #txtMdlDueDate').datepicker
                ({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true,
            });
            //BtnNew starts here 
            $('#btnNew').click(function ()
            {
                resetAll();
            });
            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
         
            $('#txtEntryDate').datepicker({ format: 'dd/M/yyyy', 'setDate': today });
            var due = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 7);
            $('#txtMdlDueDate').datepicker('setDate', due);

            //Loading Datatable
            $('#btnLoad').off().click(function ()
            {
                if ($('#ddlLocation').val() == '0')
                {
                    errorField('#ddlLocation');
                }
                $('#listTable tbody').children().remove();
                var locationid = $('#ddlLocation').val();
                var fromDate = $('#txtFromDate').val();
                var toDate = $('#txtToDate').val();
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/quotebuilder/get',
                    method: 'POST',
                    data: JSON.stringify({ 'LocationId': locationid, 'StartDate': fromDate, 'EndDate': toDate }),
                    dataType: 'JSON',
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        var html = '';
                        $('#listTable tbody').children().remove();
                        for (var i = 0; i < response.length; i++)
                        {
                            html += '<tr>';
                            html += '<td style="display:none">' + response[i].ID + '</td>';
                            html += '<td>' + response[i].RequestNo + '</td>';
                            html += '<td>' + response[i].EntryDateString + '</td>';
                            html += '<td>' + response[i].DueDateString + '</td>';
                            html += '<td style="text-align:right">' + response[i].Products.length + '</td>';
                            response[i].Priority ? html += '<td style="text-align:right"><label class="label label-danger">High</label></td>' : html += '<td style="text-align:right"><label class="label label-default">Normal</label></td>'
                            html += '<td style="text-align:right">' + response[i].TaxAmount + '</td>';
                            html += '<td style="text-align:right">' + response[i].Gross + '</td>';
                            html += '<td style="text-align:right">' + response[i].NetAmount + '</td>';
                            html += '<td  style="text-align:right;padding-right: 20px;"><a class="build-quote" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal" href="#"><i>Quote</i></td>';
                        }
                        $('#listTable tbody').append(html);

                        //binding event to qoute link
                        $('#listTable').off().on('click', '.build-quote', function ()
                        {
                            $('#requestTable tbody').children().remove();
                            var reqId = $(this).closest('tr').children('td').eq(0).text();
                            var locationid = $('#ddlLocation').val();
                            $.ajax({
                                url: $('#hdApiUrl').val() + '/api/QuoteBuilder/get/?RequestId=' + reqId,
                                method: 'POST',
                                data: JSON.stringify(locationid),
                                dataType: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                success: function (response)
                                {
                                    $('#mdlEntryDate').text(response.EntryDateString);
                                    $('#mdlRequestId').text(response.ID);
                                    let html = '';
                                    var selectedItemsId = sessionStorage.getItem('selectedItemsId') == null ? [] : JSON.parse(sessionStorage.getItem('selectedItemsId'));
                                    $(response.Products).each(function (index)
                                    {
                                        var detailsID = this.DetailsID;
                                        html += '<tr>';
                                        html += '<td style="display:none">' + this.DetailsID + '</td>';
                                        html += '<td>' + this.Name + '</td>';
                                        html += '<td>' + this.ItemCode + '</td>';
                                        html += '<td>' + this.MRP + '</td>';
                                        html += '<td>' + this.CostPrice + '</td>';
                                        html += '<td>' + this.Quantity + '</td>';
                                        html += '<td><input type="number" class="qty-input" value="' + this.Quantity + '"/>';
                                        html += '<td>' + this.TaxAmount + '</td>';
                                        html += '<td>' + this.Gross + '</td>';
                                        html += '<td>' + this.NetAmount + '</td>';
                                        html += '<td style="display:none">' + this.TaxPercentage + '</td>';
                                        var selected = false;
                                        $(selectedItemsId).each(function ()
                                        {
                                            if (this == detailsID)
                                            {
                                                selected = true;

                                            }
                                        });
                                        if (selected)
                                        {
                                            html += '<td class="text-success"><i class="md md-done"></i></td>';
                                        }
                                        else
                                        {
                                            html += '<td><input type="checkbox" class="checkbox checkbox-primary chk-single" /></td>';
                                        }
                                        html += '</tr>';
                                    });
                                    $('#requestTable tbody').append(html);

                                    // binding event to select All checkbox
                                    $('#requestTable').on('change', '.chk-all', function ()
                                    {

                                        if ($(this).is(':checked'))
                                        {
                                            var rows = $('#requestTable tbody').children('tr');
                                            for (var i = 0; i < rows.length; i++) {
                                                if ($(rows[i]).children('td:last-child').children('input').prop('checked') != undefined)
                                                {
                                                    $('.chk-single').prop('checked', true);
                                                    $(rows[i]).addClass('selected-row');
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var rows = $('#requestTable tbody').children('tr');
                                            for (var i = 0; i < rows.length; i++)
                                            {
                                                $('.chk-single').prop('checked', false);
                                                $(rows[i]).removeClass('selected-row');
                                            }
                                        }

                                    });

                                    //binding event to single checkbox
                                    $('#requestTable').on('click', '.chk-single', function ()
                                    {

                                        if ($(this).is(':checked'))
                                        {
                                            $(this).closest('tr').children('td').eq(6).children('input').focus().select();
                                            $(this).closest('tr').addClass('selected-row');
                                        }
                                        else
                                        {
                                            $(this).closest('tr').removeClass('selected-row');
                                        }
                                    });

                                    //function for calculation inside QouteBuilder
                                    setInterval(function ()
                                    {
                                        var rows = $('#requestTable tbody').children('tr');
                                        var noOfItems = 0.00;
                                        rows.each(function (index) {
                                            if ($(this).find('.chk-single').is(':checked'))
                                            {

                                                noOfItems++;
                                            }

                                        });
                                        $('#noOfItems').text(noOfItems);
                                    }, 500);
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


                            //Add to cart functionality
                            $('#myModal').off().on('click', '#btnAddToCart', function ()
                            {
                                var rows = $('#requestTable tbody').children('tr.selected-row');
                                var cartName = $('#ddlSupplier').children(':selected').text();
                                var locationId = $('#ddlLocation').val();
                                var locationName = $('#ddlLocation :selected').text();
                                var companyId = $.cookie('bsl_1');
                                var financialYear = $.cookie('bsl_4');
                                var cartId = $('#ddlSupplier').val();
                                if (rows.length != 0 & cartId != 0)
                                {
                                   var requestCarts = sessionStorage.getItem('requestCarts') == null ? { 'Qoutes': [], 'Suppliers': [], 'LocationId': locationId, 'CompanyId': companyId, 'FinancialYear': financialYear, 'LocationName': locationName, 'EntryDate': $('#txtEntryDate').val(), 'CreatedBy': $.cookie('bsl_3') } : JSON.parse(sessionStorage.getItem('requestCarts'));
                                   var selectedItemsId = sessionStorage.getItem('selectedItemsId') == null ? [] : JSON.parse(sessionStorage.getItem('selectedItemsId'));
                                   var items = [];
                                    $(rows).each(function (index) {
                                        var item = {};
                                        item.ID = $(this).children('td').eq(0).text();
                                        item.ItemName = $(this).children('td').eq(1).text();
                                        item.ItemCode = $(this).children('td').eq(2).text();
                                        item.Mrp = $(this).children('td').eq(3).text();
                                        item.Rate = $(this).children('td').eq(4).text();
                                        item.ReqQty = $(this).children('td').eq(5).text();
                                        item.Tax = $(this).children('td').eq(7).text();
                                        item.Gross = $(this).children('td').eq(8).text();
                                        item.NetAmount = $(this).children('td').eq(9).text();
                                        item.TaxPercentage = $(this).children('td').eq(10).text();
                                        selectedItemsId.push(item.ID);
                                        item.Qty = $(this).children('td').eq(6).children('input').val();
                                        if (requestCarts.Suppliers.indexOf(cartId) >= 0)
                                        {
                                            for (var i = 0; i < requestCarts.Qoutes.length; i++)
                                            {
                                                if (requestCarts.Qoutes[i].SupplierId == cartId)
                                                {
                                                    requestCarts.Qoutes[i].Products.push(item);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            items.push(item);
                                        }
                                    });
                                    if (items.length != 0)
                                    {
                                        requestCarts.Suppliers.push(cartId);
                                        requestCarts.Qoutes.push({ 'Products': items, 'Name': cartName, 'SupplierId': cartId });
                                    }
                                    sessionStorage.setItem('selectedItemsId', JSON.stringify(selectedItemsId));
                                    sessionStorage.setItem('requestCarts', JSON.stringify(requestCarts));
                                    refreshCartsBar();
                                    $('#myModal').modal('hide');

                                    //binding show qoutes function 
                                    $('body').off().on('click', '.show-quote', function ()
                                    {
                                        $('#quotesList tbody').children().remove();
                                        //var quote;
                                        if (sessionStorage.getItem('requestCarts') != null)
                                        {
                                            var requestCarts = JSON.parse(sessionStorage.getItem('requestCarts'));
                                            var supplierid = $(this).prop('id');
                                            for (var i = 0; i < requestCarts.Qoutes.length; i++)
                                            {

                                                if (requestCarts.Qoutes[i].SupplierId === supplierid)
                                                {
                                                    //quote = requestCarts.Qoutes[i];
                                                    var supplierName = requestCarts.Qoutes[i].Name;
                                                    var locationName = requestCarts.LocationName;
                                                    var entryDate = requestCarts.EntryDate;
                                                    $('#lblSupplierName').html(supplierName);
                                                    $('#lblLocation').html(locationName);
                                                    $('#lblEntryDate').html(entryDate);
                                                    $('#lblSupplierName').attr('value', supplierid);
                                                    var html = '';
                                                    var products = requestCarts.Qoutes[i].Products;
                                                    $(products).each(function (index)
                                                    {
                                                        html += '<tr>';
                                                        html += '<td style="display:none">' + this.ID + '</td>';
                                                        html += '<td style="display:none">' + supplierid + '</td>';
                                                        html += '<td>' + this.ItemName + '</td>';
                                                        html += '<td>' + this.ItemCode + '</td>';
                                                        html += '<td>' + this.Mrp + '</td>';
                                                        html += '<td>' + this.Rate + '</td>';
                                                        html += '<td>' + this.ReqQty + '</td>';
                                                        html += '<td><input type="number" class="qty-input" value="' + this.Qty + '"/></td>';
                                                        html += '<td>' + this.Tax + '</td>';
                                                        html += '<td>' + this.Gross + '</td>';
                                                        html += '<td>' + this.NetAmount + '</td>';
                                                        html += '<td style="display:none">' + this.TaxPercentage + '</td>';
                                                        html += '<td><a href="#"><i class="text-danger md md-remove-circle delete-entry"></i></a></td>';
                                                        html += '</tr>';
                                                    });

                                                    $('#quotesList tbody').append(html);
                                                    //binding event to delete entry
                                                    $('#quotesList').off().on('click', '.delete-entry', function ()
                                                    {

                                                        $(this).closest('tr').hide('slow', function ()
                                                        {
                                                            $(this).closest('tr').remove();
                                                        });
                                                        deletedItems.push($(this).closest('tr').children('td').eq(0).text());

                                                    });

                                                }
                                            }
                                        }

                                    });

                                }
                                else
                                {

                                    if ($('#ddlSupplier').val() == '0')
                                    {
                                        errorField('#ddlSupplier');
                                    }
                                    else
                                    {
                                        errorField('.chk-single');
                                        errorField('.chk-all');
                                    }
                                    //errorAlert("You must check atleast one product and a supplier to build this quote ");
                                }

                            });

                        });

                    },
                    error: function (xhr)
                    {
                        alert(xhr.responseText);;
                        console.log(xhr);
                    }
                });
            });//Loading Datatable ends here

            //Saving whole qoutes
            $('#btnSave').click(function ()
            {

                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                    
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                function (isConfirm)
                {
                    if (isConfirm)
                    {
                        if (sessionStorage.getItem('requestCarts') != null)
                        {
                            $.ajax({
                                method: 'POST',
                                url: $('#hdApiUrl').val() + 'api/QuoteBuilder/Save',
                                dataType: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(sessionStorage.getItem('requestCarts')),
                                success: function (response)
                                {
                                    if (response.Success)
                                    {
                                        successAlert(response.Message);
                                        resetAll();
                                        $('#listTable tbody').children().remove();
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
                                }
                            });
                        }
                    }
                });


            });
            //lock location once selected
            $('#ddlLocation').change(function ()
            {
                if ($(this).val() != 0)
                {
                    $(this).prop('disabled', true);
                }
            });

            //reset function
            function resetAll()
            {
                //reset();
                $('#txtFromDate').val('');
                $('#txtToDate').val('');
                sessionStorage.removeItem('requestCarts');
                sessionStorage.removeItem('selectedItemsId');
                $('#cartsBar').children().remove();
                $('#listTable tbody').children().remove();
                $('#txtEntryDate').datepicker({ format: 'dd/M/yyyy', 'setDate': today });
                $('#ddlLocation').prop('disabled', false);
            }
            //Refresh Carts Bar
            function refreshCartsBar()
            {
                var selectedItemsId = JSON.parse(sessionStorage.getItem('selectedItemsId'));
                var requestCarts = JSON.parse(sessionStorage.getItem('requestCarts'));
                $('#cartsBar').children().remove();
                if (selectedItemsId.length > 0)
                {

                    $(requestCarts.Qoutes).each(function ()
                    {
                        var SupplierName = this.Name;
                        var SupplierId = this.SupplierId;
                        var totalProducts = this.Products.length;
                        $('#cartsBar').append('<a href="#" id="' + SupplierId + '" class="show-quote cart-wrap" data-toggle="modal" data-target="#quotesModal" title="' + SupplierName + '"><img src="../../../../Theme/images/purchase_order_cart.png" /><span class="badge badge-danger cart-no"> ' + totalProducts + '</span><span class="cart-name">' + SupplierName.substring(0, 2).toUpperCase() + '</span></a>');
                    });
                }
            }

            //update cart
            $(document).on('click', '#btnUpdate', function ()
            {
                var supplierid = $('#lblSupplierName').attr('value');
                var products = [];
                var rows = $('#quotesList tbody').children('tr');
                for (var i = 0; i < rows.length; i++)
                {
                    var id = $(rows[i]).children('td').eq(0).text();
                    var itemname = $(rows[i]).children('td').eq(2).text();
                    var code = $(rows[i]).children('td').eq(3).text();
                    var mrp = $(rows[i]).children('td').eq(4).text();
                    var rate = $(rows[i]).children('td').eq(5).text();
                    var reqqty = $(rows[i]).children('td').eq(6).text();
                    var qty = $(rows[i]).children('td').eq(7).children('input').val();
                    var tax = $(rows[i]).children('td').eq(8).text();
                    var gross = $(rows[i]).children('td').eq(9).text();
                    var net = $(rows[i]).children('td').eq(10).text();
                    var taxp = $(rows[i]).children('td').eq(11).text();
                    var product = {};
                    product.ID = id;
                    product.ItemName = itemname;
                    product.ItemCode = code;
                    product.Mrp = mrp;
                    product.Rate = rate;
                    product.ReqQty = reqqty;
                    product.Tax = tax;
                    product.Gross = gross;
                    product.NetAmount = net;
                    product.TaxPercentage = taxp;
                    product.Qty = qty;
                    products.push(product);
                }

                var dueDate = $('#txtMdlDueDate').val();
                var narration = $('#txtNarration').val();
                var requestCarts = JSON.parse(sessionStorage.getItem('requestCarts'));

                for (var j = 0; j < requestCarts.Qoutes.length; j++)
                {
                    if (requestCarts.Qoutes[j].SupplierId == supplierid)
                    {
                        requestCarts.Qoutes[j].Products = products;
                        requestCarts.Qoutes[j].DueDate = dueDate;
                        requestCarts.Qoutes[j].Narration = narration;
                        sessionStorage.setItem('requestCarts', JSON.stringify(requestCarts));
                        $('#quotesModal').modal('hide');
                        refreshCartsBar();
                        //addToList();
                    }
                }
                var selectedItemsId = JSON.parse(sessionStorage.getItem('selectedItemsId'));
                $(deletedItems).each(function ()
                {
                    selectedItemsId.splice(selectedItemsId.indexOf(this.toString()), 1);

                });
                $('#txtNarration').val('');
                deletedItems = [];
                sessionStorage.setItem('selectedItemsId', JSON.stringify(selectedItemsId));
            });

            //Delete Cart
            $(document).on('click', '#btnDelete', function ()
            {
                swal({
                    title: "Delete?",
                    text: "Are you sure you want to delete this cart?",
                   
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Delete"
                },
                function (isConfirm)
                {
                    if (isConfirm)
                    {
                        var supplierid = $('#lblSupplierName').attr('value');
                        var rows = $('#quotesList tbody').children('tr');
                        var requestCarts = JSON.parse(sessionStorage.getItem('requestCarts'));
                        for (var j = 0; j < requestCarts.Qoutes.length; j++)
                        {
                            if (requestCarts.Qoutes[j].SupplierId == supplierid)
                            {
                                requestCarts.Qoutes.splice(j, 1);
                                requestCarts.Suppliers.splice(requestCarts.Suppliers.indexOf(supplierid), 1);
                                break;
                            }
                        }
                        var rows = $('#quotesList tbody').children('tr');
                        var selectedItemsId = JSON.parse(sessionStorage.getItem('selectedItemsId'));
                        for (var i = 0; i < rows.length; i++) {
                            var id = $(rows[i]).children('td').eq(0).text();
                            selectedItemsId.splice(selectedItemsId.indexOf(id), 1);
                        }
                        sessionStorage.setItem('requestCarts', JSON.stringify(requestCarts));
                        sessionStorage.setItem('selectedItemsId', JSON.stringify(selectedItemsId));
                        $('#quotesModal').modal('hide');
                        refreshCartsBar();
                    }
                });

            });
            //delete function ends here
        });//document ready ends here
    </script>
    <script src="../../../../Theme/Custom/Commons.js"></script>
    <link href="../../../../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../../../../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
