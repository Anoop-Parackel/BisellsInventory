<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Damage.aspx.cs" Inherits="BisellsERP.WareHousing.Damage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Ware Housing Damage</title>
      <style>
          #wrapper {
              overflow: hidden;
          }

        .mini-stat.clearfix.bx-shadow {
            height: 90px;
        }

        .order-num {
            font-size: 15px;
            padding: 4px 20px;
        }
        .panel .panel-body {
            padding: 10px;
            padding-top: 30px;
        }
           tbody tr td {
            padding: 5px !important;
            font-size: smaller;
        }
        .badge-danger  {
            font-size: 12px;
        }
            .daterangepicker.dropdown-menu.ltr.opensleft.show-calendar {
            right: auto !important;
        }
  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <input type="hidden" id="hdId" value="0">
  <%-- <button type="button" id="btnPrint" accesskey="p" class="btn btn-warning btn-lg waves-effect waves-light print-float"><i class="ion ion-printer"></i></button>--%>
 <%-- ---- Page Title ---- --%>
        <div class="row p-b-10">
            <div class="col-sm-4">
                <h3 class="page-title m-t-0">Damage<%--<asp:Label ID="Label1" runat="server" CssClass="small" Text=" | UAE"></asp:Label>--%></h3>
            </div>
            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group">
                    <button id="btnFind" accesskey="v"  type="button" data-toggle="tooltip" data-placement="bottom" title="View previous damage" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                    <button type="button" accesskey="n" id="btnNew" data-toggle="tooltip" data-placement="bottom" title="Start a new damage . Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                    <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save" class="btn btn-default waves-effect waves-light"><i class="ion-archive"></i>&nbsp;Save</button>
                    <button type="button" id="btnPrint" accesskey="p"data-toggle="tooltip" data-placement="bottom" title="Print" style="display:none"  class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i></button>
                    <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion-trash-b"></i></button>
                </div>
            
            </div>
        </div>
 <%-- ---- Search Quote Panel OLD ---- --%>
        <div class="row search-quote-panel">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="panel b-r-8 m-b-10">
                            <div class="panel-body">
                                <div class="col-sm-7">
                                    <label class="title-label">Add  to list from here..</label>
                                    <input type="text" id="txtChooser" autocomplete="off" class="form-control" placeholder="Choose Item" />
                                    <div id="lookup"></div>
                                </div>
                                <div class="col-sm-3">
                                     <label id="lblquantityError" style="display:none;color:indianred" class="title-label">..</label>
                                    <input type="text" id="txtQuantity" autocomplete="off" class="form-control " placeholder="Qty" />
                                </div>
                                <div class="col-sm-2">
                                    <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List" class="btn btn-icon btn-primary"><i class="ion-plus"></i></button>
                                </div>
                            </div>
                        </div>
                    </div>
                  <div class="col-sm-3 col-sm-offset-4">
                       <div class="panel b-r-8 m-b-10">
                           <div class="panel-body" style="padding-top: 17.5px; padding-bottom: 17.5px">
                               <div class="col-sm-12">
                                   <span>Order No :</span>
                                   <asp:Label ID="lblOrderNo" runat="server" ClientIDMode="Static" class="badge badge-danger pull-right"><b>KU1368B</b></asp:Label>
                                   <div class="clearfix"></div>
                               </div>

                               <div class="col-sm-12">
                                   <span>Date :</span>
                                   <input type="text"id="txtEntryDate" class="date-info" value="10/Dec/2017" />
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
                            <div class="col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-small-font table-hover table-striped table-responsive">
                                    <thead>
                                        <tr>
                                            <th style="display: none">ItemId</th>
                                            <th>Item Name</th>
                                            <th>Item Code</th>
                                            <th style="text-align:right">MRP</th>
                                            <th style="text-align:right">Rate</th>
                                            <th style="text-align:right">Quantity</th>
                                            <th style="text-align:right">Gross</th>
                                            <th style="text-align:right">Tax Percent</th>
                                            <th style="text-align:right">Tax Amount</th>
                                            <th style="text-align:right">NetAmount</th>
                                            <th style="display: none">InstanceId</th>
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
        <div class="row summary-panel summary-fixed">
            <div class="col-sm-5 col-lg-5">
                <div class="mini-stat clearfix bx-shadow b-r-8">
                    <div class="row">
                        <div class="col-sm-4 text-center">
                            <label class="w-100 light-font-color">Items</label>
                            <%-- Total Items --%>
                            <span id="lblTotalItems" class="l-font"></span>
                        </div>
                        <div class="col-sm-4 text-center">
                            <label class="w-100 light-font-color">Gross Amt</label>
                            <%-- Gross Amount --%>
                            <span id="lblGross" class="l-font"></span>
                        </div>
                        <div class="col-sm-4 text-center">
                            <label class="w-100 light-font-color">Total Amt</label>
                            <%-- Total Amount --%>
                            <span id="lblTotalAmount" class="l-font"></span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-4 col-lg-5">
                <div class="mini-stat clearfix bx-shadow b-r-8">
                    <div class="row">
                        <div class="col-sm-4 text-center">
                            <label class="w-100 light-font-color">Tax Amt</label>
                            <%-- Tax Amount --%>
                            <span id="lblTaxAmount" class="l-font"></span>
                        </div>
                        <div class="col-sm-4 text-center">
                            <label class="w-100 light-font-color">Round Off</label>
                            <%-- Round Off --%>
                            <asp:Label ID="lblRoundoff" CssClass="l-font" runat="server" ClientIDMode="Static" Text="0.00"></asp:Label>
                        </div>
                        <div class="col-sm-4 text-center">
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

            <div class="net-test col-sm-3 col-lg-2">
                <%-- NET AMOUNT --%>
                <div class="mini-stat clearfix bx-shadow b-r-8">
                    <div class="col-sm-2"><span class="currency">$</span></div>
                    <div class="col-sm-10">
                        <h3 class="text-right text-primary m-0">
                            <span id="lblNetAmount" class="l-font"></span>
                        </h3>
                        <div class="mini-stat-info text-right text-muted">
                            Net Amount
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-lg">
                
            <!-- Modal content-->
    <div class="modal-content">
                <div class="modal-header">
                          <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Damages &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
                    <div id="filterWrap" class="hide">
                      
                        <label>Filter by Date Range :</label>
                        <input type="text" name="daterange" class="form-control" id="txtDate" value="01/01/2015 - 01/31/2015" />
                        <div class="btn-toolbar pull-right m-t-10 m-b-10">
                            <button id="applyFilter" type="button" class="btn btn-default btn-sm">Apply Filter</button>
                            <button id="filterCancel" type="button" class="btn btn-inverse btn-sm">x</button>
                        </div>
                    </div>           
                </div>
                <div class="modal-body p-b-0">
                        <table id="tblRegister" class="table  table-condensed table-hover table-striped">
                            <thead>
                                <tr>
                                    <th class="hidden">ID</th>
                                    <th>Damage No</th>
                                    <th>Date</th>
                                    <th>Tax</th>
                                    <th>Gross</th>
                                    <th>Net Amount</th>
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

        //Find distance between Search-Panel and Summary-Panel
        $(window).resize(function () {
            var dataHeight = Math.abs($('.search-quote-panel').offset().top - $('.summary-panel').offset().top) - 105;
            console.log('resize' + dataHeight);
            if (dataHeight) {
                $('.view-h').css({
                    'max-height': dataHeight,
                    'min-height': dataHeight
                });
            }
        });

        //Calculate Function Call
        setInterval(calculateSummary, 1000);
        setInterval(calculation, 1000);
        function calculation()
        {
            var tr = $('#listTable>tbody').children('tr');
            for (var i = 0; i < tr.length; i++)
            {
                var tempTax = 0;
                var qty = parseFloat($(tr[i]).children('td:nth-child(6)').text());

                var gross = parseFloat($(tr[i]).children('td:nth-child(6)').text()) * parseFloat($(tr[i]).children('td:nth-child(5)').text());
                gross = parseFloat(gross.toFixed(2));//Gross Amount

                var taxper = parseFloat($(tr[i]).children('td:nth-child(8)').text());
                var tax = parseFloat($(tr[i]).children('td:nth-child(5)').text()) * (taxper / 100);
                tax = parseFloat(tax.toFixed(2));//Tax Amount

                var net = gross + (tax * qty);
                net = parseFloat(net.toFixed(2));//Net amount
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax.toFixed(2));
                $(tr[i]).children('td:nth-child(7)').text(gross); //gross amount
                $(tr[i]).children('td:nth-child(9)').text(tempTax);  //tax amount
                $(tr[i]).children('td:nth-child(10)').text(net);  //net amount
                qty = 0;
            }
        }
        function calculateSummary()
        {
            var tr = $('#listTable > tbody').children('tr');
            var tax = 0.00;
            var gross = 0.00;
            var amount = 0.00;
            var qty = $('#txtQuantity').val();
            var net = 0.00;
            for (var i = 0; i < tr.length; i++)
            {
                qty = parseFloat($(tr[i]).children('td:nth-child(6)').text());
                tax +=parseFloat($(tr[i]).children('td:nth-child(9)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(7)').text())
                net += parseFloat($(tr[i]).children('td:nth-child(10)').text())

            }
            gross = parseFloat(gross.toFixed(2));
            net = parseFloat(net.toFixed(2));
            tax = parseFloat(tax.toFixed(2));
            var roundoff = Math.round(net) - net;
            net = Math.round(net);
            roundoff = parseFloat(roundoff.toFixed(2));
            $('#lblTotalItems').text(tr.length);
            $('#lblGross').text(gross);
            $('#lblTotalAmount').text(gross);
            $('#lblTaxAmount').text(tax);
            $('#lblNetAmount').text(net);
            $('#lblRoundoff').text(roundoff);

        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            var dataHeight = Math.abs($('.search-quote-panel').offset().top - $('.summary-panel').offset().top) - 115;
            console.log('ready' + dataHeight);
            if (dataHeight) {
                $('.view-h').css({
                    'max-height': dataHeight,
                    'min-height': dataHeight
                });
            }

            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);
            $('#btnPrint').hide();
            //Getting Entry for edit purpose if user asked for it
            var Params = getUrlVars();
            if (Params.UID != undefined && !isNaN(Params.UID)) {
                if (Params.MODE == 'clone') {
                    getRegister(true, Params.UID);
                } else if (Params.MODE == 'edit') {

                    getRegister(false, Params.UID);
                } else {
                    resetRegister();
                }
            }
          
            $('#btnPrint').click(function () {
                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/Warehousing/Print/" + type + "/" + number + "/Damage?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'Damage', 800, 700);
                }

            });

         /* Date and Due Date */
            $('#txtEntryDate, #txtDuedate').datepicker
            ({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
           //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtEntryDate').datepicker('setDate', today);


            //lookup initialization
            var locationId = $.cookie('bsl_2');
            lookup({
                textBoxId: 'txtChooser',
                url: $('#hdApiUrl').val() + 'api/search/ItemsFromPurchaseWithStock?locationid=' + locationId + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'SellingPrice', 'Stock'],
                visibility: [false, false, true, true, true, true, true,true],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate','Stock'],
                key: 'ItemID',
                dataToShow: 'Name',
                OnLoading: function () { miniLoading('start') },
                OnComplete: function () { miniLoading('stop') }
            });
            //lookup initialization ends here

            //Add Item to list
            $('#btnAdd').click(function ()
            {
                addToList()
            });//Add to list ends here

            //Function for add to list
            function addToList()
            {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var qty = parseFloat($('#txtQuantity').val());
                var rate = parseFloat(tempItem.CostPrice);
                var taxper = parseFloat(tempItem.TaxPercentage);
                var TaxAmount = parseFloat(((rate * (taxper / 100))).toFixed(2));
                var GrossAmount = parseFloat((qty * rate).toFixed(2));
                var NetAmount = parseFloat((GrossAmount + TaxAmount * qty).toFixed(2));
                var tempTax = (TaxAmount * qty).toFixed(2);

                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null & !isNaN(qty))
                {
                    if (parseFloat(tempItem.Stock) < qty)
                    {
                        $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                    }
                    else
                    {
                    var Rows = $('#listTable > tbody').children('tr');
                    var itemExists = false;
                    var rowOfItem;
                    $(Rows).each(function ()
                    {
                        var itemId = $(this).children('td').eq(0).text();
                        var instanceId = $(this).children('td').eq(10).text();
                        if (tempItem.ItemID == itemId && tempItem.InstanceId == instanceId)
                        {
                            itemExists = true;
                            rowOfItem = this;
                        }
                    });
                    if (itemExists)
                    {
                        var existingQty = parseFloat($(rowOfItem).children('td').eq(5).text());
                        var newQty = existingQty + qty;
                        $(rowOfItem).children('td').eq(5).replaceWith('<td style="text-align:right">' + newQty + '</td>');
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
                        html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td>' + tempItem.Name + '</td><td>' + tempItem.ItemCode + '</td><td style="text-align:right">' + tempItem.MRP + '</td><td style="text-align:right">' + tempItem.SellingPrice + '</td><td style="text-align:right">' + qty + '</td><td style="text-align:right">' + GrossAmount + '</td><td style="text-align:right">' + tempItem.TaxPercentage + '</td><td style="text-align:right">' + tempTax + '</td><td style="text-align:right">' + NetAmount + '</td><td style="display: none">' + tempItem.InstanceId + '</td><td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
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
            }


             //Add Item to list with enter key
            $('#txtQuantity').keypress(function (e)
            {
                if (e.which == 13)
                {
                    e.preventDefault();
                    addToList()
                }
            });//Add item with enter key ends here

            //Binding Event to Save button
            $('#btnSave').off().click(function ()
            {
             save();
            });
            //Binding save event ends here

          
            //Reset This Register
            function resetRegister()
            {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                var date = new Date();
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#txtEntryDate').datepicker('setDate', today);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#ddlCostCenter').select2('val', '0');
                $('#ddlJob').select2('val', '0');
                $('#btnPrint').hide();
            }
            //Reset ends here

            //Function for Saving the register
            function save()
            {

                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                   
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save",
                    closeOnConfirm: true
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
                        var narration = $('#txtNarration').val();                       
                        var rOff = $('#lblRoundoff').text();
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        for (var i = 0; i < tr.length; i++)
                        {
                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(6)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(11)').text();
                            var detail = { "Quantity": qty };
                            detail.InstanceId = instanceId;
                            detail.ItemID = id;
                           arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.EntryDate = entryDate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.ModifiedBy = $.cookie('bsl_3');
                        $.ajax
                            ({
                            url: $(hdApiUrl).val() + 'api/Damage/Save',
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
                            }
                        });
                    }

                });


            }
            //Save Function Ends here

        //Find function start
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
                            var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                            var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                            refreshTable( fromDate, toDate);
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
                $('#findModal').modal('show');
                refreshTable(null, null);
                function refreshTable(fromDate, toDate) {
                    $.ajax
                        ({
                            url: $('#hdApiUrl').val() + 'api/Damage/Get?from=' + fromDate + '&to=' + toDate,
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify($.cookie('bsl_2')),
                            contentType: 'application/json;charset=utf-8',
                            success: function (response) {

                                var html = '';
                                $(response).each(function (index) {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ID + '</td>';
                                    html += '<td>' + this.DamageNo + '</td>';
                                    html += '<td>' + this.EntryDateString + '</td>';
                                    html += '<td>' + this.TaxAmount + '</td>';
                                    html += '<td>' + this.Gross + '</td>';
                                    html += '<td>' + this.NetAmount + '</td>';
                                    html += '<td><a class="edit-register" title="edit" href="#"><i class="fa fa-edit"></i></a></td>'
                                    html += '</tr>';
                                });
                                $('#tblRegister').DataTable().destroy();
                                $('#tblRegister tbody').children().remove();
                                $('#tblRegister tbody').append(html);
                                $('#tblRegister').dataTable({ destroy: true, aaSorting: [], "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]] });
                                //binding event to row
                                $('#tblRegister').off().on('click', '.edit-register', function () {
                                    var registerId = $(this).closest('tr').children('td').eq(0).text();
                                    getRegister(false, registerId);
                                });
                            },
                            //error: function (xhr) { alert(xhr.responseText); console.log(xhr); },
                            //beforeSend: function () { loading('start', null) },
                            //complete: function () { loading('stop', null); },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                                miniLoading('stop');
                            },
                            beforeSend: function () { miniLoading('start') },
                            complete: function () { miniLoading('stop'); },
                        });
                }
            });

            function getRegister(isClone, id) {
                resetRegister();
                $.ajax
                   ({
                       url: $('#hdApiUrl').val() + 'api/Damage/get/' + id,
                       method: 'POST',
                       dataType: 'JSON',
                       data: JSON.stringify($.cookie('bsl_2')),
                       contentType: 'application/json;charset=utf-8',
                       success: function (response) {
                           try {
                               var html = '';
                               var register = response;
                               $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                               $('#txtNarration').val(register.Narration);
                               if (isClone) {
                                   $('#hdId').val('0');
                                   $('#btnPrint').hide();
                                   $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                               }
                               else {
                                   $('#hdId').val(register.ID);
                                   $('#btnPrint').show();
                                   $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                               }
                               
                               html = '';
                               $(register.Products).each(function () {
                                   html += '<tr>';
                                   html += '<td style="display:none">' + this.ItemID + '</td>';
                                   html += '<td>' + this.Name + '</td>';
                                   html += '<td>' + this.ItemCode + '</td>';
                                   html += '<td style="text-align:right">' + this.MRP + '</td>';
                                   html += '<td style="text-align:right">' + this.CostPrice + '</td>';
                                   html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                   html += '<td style="text-align:right">' + this.Gross + '</td>';
                                   html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                   html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                   html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                   html += '<td style="display:none">' + this.InstanceId + '</td>';
                                   html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                   html += '</tr>';
                               });
                               $('#lblGross').val(register.Gross);
                               $('#lblOrderNo').text(register.RequestNo);
                               $('#lblTotalAmount').val(register.Gross);
                               $('#lblTaxAmount').val(register.Gross);
                               $('#lblNetAmount').val(register.NetAmount);
                               $('#listTable tbody').append(html);
                               $('#findModal').modal('hide');
                               //binding delete
                               $('.delete-row').click(function () {
                                   $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                               });
                           }
                           catch (err) {
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

            //BtnNew starts here 
            $('#btnNew').click(function ()
            {
                resetRegister();
            });
            //Delete functionality
            $('#btnDelete').click(function ()
            {
                if ($('#hdId').val() != 0)
                {
                    swal
                        ({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",
                      
                        showConfirmButton: true,closeOnConfirm:true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    },
                    function (isConfirm)
                    {
                        if (isConfirm)
                        {
                            var id = $('#hdId').val();
                            var modifiedBy = $.cookie('bsl_3');
                            $.ajax
                                ({
                                url: $('#hdApiUrl').val() + 'api/Damage/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response)
                                {
                                    
                                    if (response.Success)
                                    {
                                        successAlert(response.Message);
                                        $('#hdId').val('');
                                        resetRegister();
                                    }
                                    else
                                    {
                                        errorAlert(response.Message);
                                    }
                                },
                                error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                            });
                        }
                    
                    });
                }
                else
                {
                    errorAlert('Select an Entry to Delete');
                }
          
            });
        }) //document ready ends here
 </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <!-- Date Range Picker -->
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
</asp:Content>
