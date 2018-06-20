<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="BisellsERP.Purchase.request" %>

 <asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <title>Purchase Request</title>
    <style>

        #wrapper {
            overflow: hidden;
        }
        .mini-stat.clearfix.bx-shadow {
            height: 80px;
            padding: 15px 5px;
        }
            .mini-stat.clearfix.bx-shadow .fa {
                font-size: 1.5em;
                margin-top: 5px;
            }

        .l-font
         {
            font-size: 1.5em;
         }
            .combo-dropdown {
            display: inline-block;
            position: relative;
            margin-right: 10px;
        }
            .combo-dropdown p {
                position: absolute;
                top: -13px;
                font-size: 10px;
                left: 2px;
                color: #607D8B;
                text-transform: uppercase;
            }
            .combo-dropdown .select2-container {
                width: 140px !important;
                border: 1px solid #ccc;
                border-radius: 3px;
            }
        .light-font-color {
            color: #607D8B;
        }
        .order-num {
            font-size: 15px;
            padding: 4px 20px;
        }

        .col-sm-4.text-center:not(:last-child) {
            border-right: 1px solid rgba(119, 136, 153, .1);
        }

        .panel {
            margin-bottom: 10px;
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


    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="childContent" runat="server">
    <input type="hidden" id="hdId" value="0" />
   <%--<button type="button" id="btnPrint" accesskey="p" class="btn btn-warning btn-lg waves-effect waves-light print-float"><i class="ion ion-printer"></i></button>--%>
 <%-- ---- Page Title ---- --%>
        <div class="row p-b-10">
            <div class="col-sm-3">
                <h3 class="page-title m-t-0">Purchase Request</h3>
            </div>
            <div class="col-sm-9">
                <div class="btn-toolbar pull-right" role="group">
                    <button id="btnFind" accesskey="v"  type="button"  data-toggle="tooltip" data-placement="bottom" title="View previous purchase request" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                    <button type="button" accesskey="n" id="btnNew" data-toggle="tooltip" data-placement="bottom" title="Start a new request . Unsaved data will be lost " class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                    <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current request" class="btn btn-default waves-effect waves-light"><i class="ion-archive"></i>&nbsp;Save</button>
                     <button id="btnSavePrint" accesskey="a" type="button" data-toggle="tooltip" data-placement="bottom" title="Save & Print the current request"  class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i>&nbsp;Save & Print</button>
                    <button type="button" id="btnPrint" accesskey="p" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light "><i class="ion ion-printer"></i></button>
                    <button type="button" id="btnDelete"data-toggle="tooltip" data-placement="bottom" title="Delete"  class="btn btn-default waves-effect waves-light text-danger"><i class="ion-trash-b"></i></button>
                </div>
                <div class="pull-right form-inline">
                    <div class="combo-dropdown">
                        <div class="col-xs-6 p-l-0">
                            <p>Cost Center</p>
                            <asp:DropDownList ID="ddlCostCenter" class="searchDropdown" runat="server" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                        <div class="col-xs-6 p-l-0">
                            <p>Job</p>
                            <asp:DropDownList ID="ddlJob" class="searchDropdown" runat="server" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
 <%-- ---- Search Quote Panel OLD ---- --%>
        <div class="row search-quote-panel">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="panel b-r-8">
                            <div class="panel-body">
                                <div class="col-sm-7">
                                    <label class="title-label">Add  to list from here..<a class="add-master" id="addnewItem" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="right" title="Add new Item"></i></a></label>
                                    <input type="text" autocomplete="off" id="txtChooser" class="form-control" placeholder="Choose Item" />    
                                     <div id="descWrap" class="hide">
                                        <label>Description</label>
                                        <textarea id="txtDescription" cols="30" rows="4" class="form-control"></textarea>
                                        <p class="text-muted text-right m-t-10"><i>Press <kbd>ESC</kbd> after completion</i></p>
                                    </div>
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
             <div class="col-sm-4">
                        <div class="panel b-r-8">
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <label class="title-label">Set Priority</label>
                                    <div class="checkbox m-b-0">
                                        <div class="checkbox checkbox-inline checkbox-danger">
                                            <asp:CheckBox ID="ChkPriority" ClientIDMode="Static" Text="Higher" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3">
                       <div class="panel b-r-8">
                            <div class="panel-body p-t-5 p-b-5">
                                <div class="col-sm-12">
                                 <span>Order No :</span>
                                 <asp:Label ID="lblOrderNo" runat="server" ClientIDMode="Static" class="badge badge-danger pull-right"><b>KU1368B</b></asp:Label>
                                 <div class="clearfix"></div>
                                </div>
                                <div class="col-sm-12">
                                  <span>Date :</span>
                                    <input type="text" id="txtEntryDate" value="10/Dec/2017" class="date-info" />
                                </div>
                                <div class="col-sm-12">
                                   <span>Due Date :</span>
                                    <input type="text" id="txtDuedate" value="21/Dec/2017" class="date-info" />
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
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-small-font table-hover table-striped table-responsive">
                                    <thead>
                                        <tr>
                                            <th style="display: none">ItemID</th>
                                            <th>Item </th>
                                            <th>Code</th>
                                            <th style="text-align:right;">MRP</th>
                                            <th style="text-align:right;">Rate</th>
                                            <th style="text-align:right;">Quantity</th>
                                            <th style="text-align:right;">Gross</th>
                                            <th style="text-align:right;">Tax%</th>
                                            <th style="text-align:right;">Tax </th>
                                            <th style="text-align:right;">Net</th>
                                            <th style="display: none">InstanceID</th>
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
                            <label class="w-100 light-font-color">Gross </label>
                            <%-- Gross Amount --%>
                            <span id="lblGross" class="l-font"></span>
                        </div>
                        <div class="col-sm-4 text-center">
                            <label class="w-100 light-font-color">Total </label>
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
                            <label class="w-100 light-font-color">Tax </label>
                            <%-- Tax Amount --%>
                            <span id="lblTaxAmount" class="l-font"></span>
                        </div>
                        <div class="col-sm-3 text-center">
                            <label class="w-100 light-font-color">Round Off</label>
                            <%-- Round Off --%>
                    <asp:TextBox ID="txtRoundOff" AutoComplete="off" CssClass="w-100 l-font" style="border:none;text-align:center;background-color:transparent;font-size:20px"  ClientIDMode="Static" runat="server" placeholder="RoundOff">0.00</asp:TextBox>
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
                            <span id="lblNetAmount"></span>
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
                    <h4 class="modal-title">Previous Purchase Requests &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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
                                    <th>Request No</th>
                                    <th>Date</th>
                                    <th>Tax</th>
                                    <th>Gross</th>
                                    <th>Net </th>
                                    <th>Priority</th>
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
            if (dataHeight) {
                $('.view-h').css('min-height', dataHeight);
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
                tax = parseFloat(tax);//Tax Amount

                var net = gross + (tax * qty);
                net = parseFloat(net.toFixed(2));//Net amount
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax.toFixed(2));
                $(tr[i]).children('td:nth-child(7)').text(gross); //gross amount
                $(tr[i]).children('td:nth-child(9)').text(tempTax.toFixed(2));  //tax amount
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
         
            if (JSON.parse($('#hdSettings').val()).AutoRoundOff)
            {
                var roundoff =Math.round(net) - net;
                net = Math.round(net);
                roundoff = parseFloat(roundoff.toFixed(2));
                $('#txtRoundOff').val(roundoff);
            }
            else {
                var roundoff = parseFloat($('#txtRoundOff').val());
                net = net + parseFloat($('#txtRoundOff').val());
                net = net.toFixed(2);
            }
           
            
            $('#lblTotalItems').text(tr.length);
            $('#lblGross').text(gross);
            $('#lblTotalAmount').text(gross);
            $('#lblTaxAmount').text(tax);
            $('#lblNetAmount').text(net);
            

          }
    </script>

    <script type="text/javascript">
        $(document).ready(function ()
        {
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            $('#btnPrint').hide();

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
            else {
                if (Params.JOB != undefined && !isNaN(Params.JOB)) {
                    var job = getUrlVars()["JOB"];
                    var jobOpts = $('#ddlJob').children('option');
                    for (var i = 0; i < jobOpts.length; i++) {
                        if ($(jobOpts[i]).val() == job) {
                            $('#ddlJob').select2('val', job);
                            break;
                        }
                        else {
                            $('#ddlJob').select2('val', '0');
                        }
                    }
                    $('#hdId').val('0');
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                    $('#btnPrint').hide();
                    $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                }

                if (Params.COSTCENTER != undefined && !isNaN(Params.COSTCENTER)) {
                    var costCenter = getUrlVars()["COSTCENTER"];
                    var costOptn = $('#ddlCostCenter').children('option');
                    for (var i = 0; i < costOptn; i++) {
                        if ($(costOptn[i]).val() == costCenter) {
                            $('#ddlCostCenter').select2('val', costCenter);
                            break;
                        }
                        else {
                            $('#ddlCostCenter').select2('val', '0');
                        }
                    }
                    $('#ddlCostCenter').select2('val', costCenter);
                    $('#hdId').val('0');
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                    $('#btnPrint').hide();
                    $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                }
            }

         /* Date and Due Date */
            $('#txtEntryDate, #txtDuedate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });


            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtEntryDate').datepicker('setDate', today);
            var due = new Date(date.getFullYear(), date.getMonth(), date.getDate()+7);
            $('#txtDuedate').datepicker('setDate', due);
            //If window size changes
            $(window).resize(function ()
            {
                //offset class for Net Amount
                //$(window).width() > 1024 ? $('.net-test').removeClass('col-sm-offset-1') : $('.net-test').addClass('col-sm-offset-1')
            });

            //lookup initialization
            var companyId = $.cookie('bsl_1');
            lookup({
                textBoxId: 'txtChooser',
                url: $(hdApiUrl).val() + 'api/search/items?CompanyId='+companyId+'&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'CostPrice'],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate'],
                visibility: [false, false, true, true, true, true, true],
                key: 'ItemID',
                dataToShow: 'Name',
                OnLoading: function () { miniLoading('start') },
                OnComplete: function () { miniLoading('stop') },
                OnSelection: function (data) {
                    var isDescriptionEnabled = JSON.parse($('#hdSettings').val()).IsSalesDescriptionEnabled;

                    if (isDescriptionEnabled) {
                        var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                        $('#txtChooser').popover('show');
                        $('#txtDescription').focus();
                        $('#txtDescription').val(tempItem.Description);
                        $('#txtDescription').off().keydown(function (e) {
                            if (e.which == 27 || e.keycode == 27 || e.which == 9 || e.keycode == 9) {
                                e.preventDefault();
                                tempItem.Description = $('<div/>').text($(this).val()).html();
                                sessionStorage.setItem('tempItem', JSON.stringify(tempItem));
                                $('#txtChooser').popover('hide');
                                $('#txtQuantity').focus();
                            }
                        });
                    }
                    else {
                        var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                        tempItem.Description = '';
                        sessionStorage.setItem('tempItem', JSON.stringify(tempItem));
                    }

                }
            });
            //lookup initialization ends here

            //Add Item to list
            $('#btnAdd').click(function ()
            {
                console.log($('#hdSettings').val());
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
                var description = tempItem.Description.replace(/\n/g, '<br/>');
                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null & !isNaN(qty))
                {
                    var Rows = $('#listTable > tbody').children('tr');
                    var itemExists = false;
                    var rowOfItem;
                    $(Rows).each(function () {
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
                        $(rowOfItem).children('td').eq(5).replaceWith('<td style="text-align:right" contenteditable="true" class="numberonly">' + newQty + '</td>');
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
                        html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td>' + tempItem.Name + '<br/><i contenteditable="true" spellcheck="false">' + description + '</i></td><td>' + tempItem.ItemCode + '</td><td style="text-align:right;">' + tempItem.MRP + '</td><td contenteditable="true" class="numberonly" style="text-align:right;">' + tempItem.CostPrice + '</td><td style="text-align:right;" contenteditable="true" class="numberonly">' + qty + '</td><td style="text-align:right;">' + GrossAmount + '</td><td style="text-align:right;">' + tempItem.TaxPercentage + '</td><td style="text-align:right;">' + tempTax + '</td><td style="text-align:right;">' + NetAmount + '</td><td style="display: none">' + tempItem.InstanceId + '</td><td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
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
                save(false);
            });
            //Binding save event ends here

            //Printing the Request
            $('#btnPrint').click(function ()
            {
                var id = $('#hdId').val();
                if (id != 0)
                {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/purchase/Print/"+type+"/"+number+"/Request?id=" + id+"&location="+$.cookie('bsl_2');
                    PopupCenter(url, 'purchaseRequest', 800, 700);
                }
            
            });

            //save and print Request
            $('#btnSavePrint').off().click(function ()
            {
                save(true);
            });

            $(function () {
                $('#txtChooser').popover({
                    placement: 'bottom',
                    trigger: 'manual',
                    html: true,
                    content: $('#descWrap').html()
                })
            });
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
                var due = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 7);
                $('#txtDuedate').datepicker('setDate', due);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#btnPrint').hide();
                $('#btnSavePrint').html('<i class=\"ion ion-printer"\></i>&nbsp;Save & Print');
                $('#ddlCostCenter').select2('val', 0);
                $('#ddlJob').select2('val', 0);
            }//Reset ends here

            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaserequest/get/' + id,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        try {
                            console.log(response);
                            var register = response;
                            var html = '';
                            $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                            $('#txtDuedate').datepicker("update", new Date(register.DueDateString));
                            $('#ChkPriority').prop('checked', register.Priority);
                            $('#txtNarration').val(register.Narration);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);
                            if (isClone)
                            {
                                $('#hdId').val('0');
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                                $('#btnPrint').hide();
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                            }
                            else {
                                $('#hdId').val(register.ID);
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                $('#btnPrint').show();
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                            }
                            
                            html = '';
                            $(register.Products).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td>' + this.MRP + '</td>';
                                html += '<td contenteditable="true" class="numberonly">' + this.CostPrice + '</td>';
                                html += '<td contenteditable="true" class="numberonly">' + this.Quantity + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.TaxPercentage + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
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

            //Function for Saving the register
            function save(PrintSave)
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
                        var dueDate = $('#txtDuedate').val();
                        var entryDate = $('#txtEntryDate').val();
                        var narration = $('#txtNarration').val();                       
                        var rOff = $('#txtRoundOff').val();
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var priority = $('#ChkPriority').is(':checked');

                        for (var i = 0; i < tr.length; i++)
                        {
                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(6)').text();
                            var rate = $(tr[i]).children('td:nth-child(5)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(11)').text();
                            var desc = $(tr[i]).children('td').eq(1).children('i').html();
                            var detail = { "Quantity": qty };
                            detail.CostPrice = rate;
                            detail.InstanceId = instanceId;
                            detail.ItemID = id;
                            detail.Description = desc;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.DueDate = dueDate;
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.EntryDate = entryDate;
                        data.RoundOff = rOff;
                        data.Priority = priority;
                        data.Products = arr;
                        data.Narration = narration;
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.ModifiedBy = $.cookie('bsl_3');
                        console.log(data);
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/purchaserequest/Save',
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
                                    if (PrintSave == true)
                                    {
                                        var type = JSON.parse($('#hdSettings').val()).TemplateType;
                                        var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                                       var url = "/purchase/Print/"+type+"/"+number+"/Request?id=" + response.Object.Id+"&location="+$.cookie('bsl_2');
                                        PopupCenter(url, 'purchaseRequest', 800, 700);

                                    }
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


            }//Save Function Ends here

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
                       

                        // Apply Filter Click
                        $('#applyFilter').click(function () {
                            //Filter Logic Here
                            
                            var fromDate = $('#txtDate').dataa('daterangepicker').startDate.format('YYYY-MMM-DD');
                            var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                            refreshTable(fromDate, toDate);
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
                refreshTable(null, null, null);
                function refreshTable(supplierID, fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/purchaseRequest/Get?SupplierId=' + supplierID + '&from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.RequestNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += this.Priority ? '<td><span class="label label-danger">High</span></td>' : '<td><span class="label label-default">Normal</span></td>';
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
                        error: function (xhr) { alert(xhr.responseText); console.log(xhr); loading('stop', null); },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); },
                    });
                }
            });
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
                    swal({
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
                        if (isConfirm) {
                            var id = $('#hdId').val();
                            var modifiedBy = $.cookie('bsl_3');
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/purchaserequest/delete/' + id,
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
                                error: function (xhr) { alert(xhr.responseText); console.log(xhr);}
                            });
                        }
                    
                    });
                }
                else
                {
                    errorAlert('Select an Entry to Delete');
                }
          
            });
            //Funtions to add Masters
            $('#addnewItem').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewItem(Url, User, Company, function (id, name) {
                    console.log(id + ':' + name);
                });
            });
        }) //document ready ends here 
 </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/Sections/Supplier.js"></script>
    <script src="../Theme/Sections/Items.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
        <!-- Date Range Picker -->
     <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
</asp:Content>

