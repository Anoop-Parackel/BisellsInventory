<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Indent.aspx.cs" Inherits="BisellsERP.Purchase.Indent" ValidateRequest="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Purchase Indent</title>
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

        .l-font {
            font-size: 1.5em;
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

        .badge-danger {
            font-size: 12px;
        }

        .btn-mail-send {
            margin-top: 24px;
            margin-left: 24px;
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
        .overflow-content {
            height: 90vh;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="childContent" runat="server">
    <asp:HiddenField runat="server" Value="" ID="hdTandC" ClientIDMode="Static" />
    <input type="hidden" value="0" id="hdId" />
    <%--<button type="button" id="btnPrint" accesskey="p" class="btn btn-warning btn-lg waves-effect waves-light print-float"><i class="ion ion-printer"></i></button>--%>
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-3">
            <h3 class="page-title m-t-0">Purchase Indent</h3>
        </div>
        <div class="col-sm-9">
            <div class="btn-toolbar pull-right" role="group">
                <button id="btnFind" accesskey="v" type="button" data-toggle="tooltip" data-placement="bottom" title="View previous purchase indent" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                <button type="button" accesskey="n" id="btnNew" data-toggle="tooltip" data-placement="bottom" title="Start a new indent . Unsaved data will be lost " class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current indent" class="btn btn-default waves-effect waves-light"><i class="ion-archive"></i>&nbsp;Save</button>
                <button id="btnSavePrint" accesskey="a" type="button" data-toggle="tooltip" data-placement="bottom" title="Save & Mail the current indent" class="btn btn-default waves-effect waves-light"><i class="ion ion-email"></i>&nbsp;Save & Mail</button>
                <button type="button" accesskey="p" id="btnPrint" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light "><i class="ion ion-printer"></i></button>
                <button type="button" id="btnMail" data-toggle="tooltip" data-placement="bottom" title="Mail" class="btn btn-default waves-effect waves-light "><i class="ion ion-email"></i></button>
                <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion-trash-b"></i></button>
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
                                <input type="text" id="txtChooser" autocomplete="off" class="form-control" placeholder="Choose Item" />
                                <div id="descWrap" class="hide">
                                    <label>Description</label>
                                    <textarea id="txtDescription" cols="30" rows="4" class="form-control"></textarea>
                                    <p class="text-muted text-right m-t-10"><i>Press <kbd>ESC</kbd> after completion</i></p>
                                </div>
                                <div id="lookup"></div>
                            </div>
                            <div class="col-sm-3">
                                <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                                <input type="text" id="txtQuantity" autocomplete="off" class="form-control" placeholder="Qty" />
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
                                        <th style="text-align: right">MRP</th>
                                        <th style="text-align: right">Rate</th>
                                        <th style="text-align: right">Quantity</th>
                                        <th style="text-align: right">Gross</th>
                                        <th style="text-align: right">Tax%</th>
                                        <th style="text-align: right">Tax </th>
                                        <th style="text-align: right">Net</th>
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
                        <asp:TextBox ID="txtRoundOff" AutoComplete="off" CssClass="w-100 l-font" Style="border: none; text-align: center; background-color: transparent; font-size: 20px" ClientIDMode="Static" runat="server" placeholder="RoundOff">0.00</asp:TextBox>
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
                    <h4 class="modal-title">Previous Purchase Indents &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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
                                <th>Indent No</th>
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
    <%-- New mail Modal --%>
    <div id="mailModal" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">
            <!-- Modal content-->
            <div class="modal-content m-10">
                <div class="modal-body p-0">
                    <div class="row">
                        <div class="before-send">
                            <h2 class="text-center">Send Mail?</h2>
                            <p class="text-center">You can add multiple e-mail addresses here</p>
                            <label id="indentId" class="hidden">0</label>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="text-muted">To</label>
                                    <input id="txtToMail" type="email" placeholder="Enter email address..." class="form-control" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="text-muted">BCC</label>
                                    <input id="txtBCCMail" type="email" placeholder="Enter BCC email address..." class="form-control " />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="text-muted">CC</label>
                                    <input id="txtCCMail" type="email" placeholder="Enter CC email address..." class="form-control " />
                                </div>
                            </div>
                            <div class="col-sm-12 text-right m-t-20">
                                <button id="btnSendMail" type="button" class="btn btn-default m-t-5">Send&nbsp;<i class="md-done"></i></button>
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
    <%-- New mail modal ends here --%>
    <%-- ---- ADDITIONAL SETTINGS  ---- --%>
    <div class="additional-settings-overlay"></div>
    <div class="row additional-settings-wrap">
        <div class="additional-settings-button">
            <i class="fa fa-angle-left"></i>
            <span>Additional Details</span>
        </div>
        <div class="col-xs-12 additional-settings-block">
            <span class="additional-settings-close">
                <i class="fa fa-times-circle"></i>
            </span>
            <div class="col-md-12 overflow-content">
                <h5 class="sett-title p-t-0">General</h5>
                <div class="col-sm-12">
                    <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-sm-4">Cost Center</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCostCenter" class="searchDropdown" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Job</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlJob" class="searchDropdown" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <h5 class="sett-title p-t-0">Terms And Condition</h5>
                    <div class="summernote-editor1"></div>
                    <div id="dvTandC" class="summernote-editor"></div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <h5 class="sett-title p-t-0">Payment Terms</h5>
                    <div class="summernote-editor2"></div>
                    <div id="dvPaymentTerm" class="summernote-editor"></div>
                </div>
            </div>
        </div>
    </div>
    <script>

        //Find distance between Search-Panel and Summary-Panel
        $(window).resize(function () {
            var dataHeight = Math.abs($('.search-quote-panel').offset().top - $('.summary-panel').offset().top) - 105;
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

        //Functions for calculations
        function calculation() {
            var tr = $('#listTable>tbody').children('tr');
            for (var i = 0; i < tr.length; i++) {
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

        function calculateSummary() {
            var tr = $('#listTable > tbody').children('tr');
            var tax = 0.00;
            var gross = 0.00;
            var amount = 0.00;
            var qty = $('#txtQuantity').val();
            var net = 0.00;
            for (var i = 0; i < tr.length; i++) {
                qty = parseFloat($(tr[i]).children('td:nth-child(6)').text());
                tax += parseFloat($(tr[i]).children('td:nth-child(9)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(7)').text())
                net += parseFloat($(tr[i]).children('td:nth-child(10)').text())

            }
            gross = parseFloat(gross.toFixed(2));
            net = parseFloat(net.toFixed(2));
            tax = parseFloat(tax.toFixed(2));

            if (JSON.parse($('#hdSettings').val()).AutoRoundOff) {
                var roundoff = Math.round(net) - net;
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

        //Initialization of additional tab 
        $('.additional-settings-button').click(function () {
            $('#dvTandC').summernote({
                height: 450,
                focus: false,
            });
        });
        $('.additional-settings-button').click(function () {
            $('#dvPaymentTerm').summernote({
                height: 450,
                focus: false,
            });
        });
    </script>
  <%--  Another script section--%>
    <script type="text/javascript">
        //Document ready function
        $(document).ready(function ()
        {
            //Loading currency symbol
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            //Hide buttons
            $('#btnMail').hide();
            $('#btnPrint').hide();

            //URL functions
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

            //Loading default TandC
            LoadTandCSettings();

            //To load the Tand C in settings
            function LoadTandCSettings()
            {
                $('#dvTandC').summernote('code', $('#hdTandC').val());
                $('#dvTandC').summernote({
                    placeholder: '',
                    height: 450,
                    airMode: true,
                    popover: {
                        air: [
                          ['color', ['color']],
                          ['font', ['bold', 'underline', 'clear']]
                        ]
                    }
                });
            }

            //Find Filter Function
            $(function () {
                $('#txtChooser').popover({
                    placement: 'bottom',
                    trigger: 'manual',
                    html: true,
                    content: $('#descWrap').html()
                })
            });

            //Print register
            $('#btnPrint').click(function () {
                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/purchase/Print/" + type + "/" + number + "/Indent?id=" + id;
                    PopupCenter(url, 'Purchase Indent', 800, 700);
                }
            });

            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtEntryDate').datepicker('setDate', today);
            var due = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 7);
            $('#txtDuedate').datepicker('setDate', due);
            //If window size changes
            $(window).resize(function () {
                //offset class for Net Amount
                //$(window).width() > 1024 ? $('.net-test').removeClass('col-sm-offset-1') : $('.net-test').addClass('col-sm-offset-1')
            });

            //lookup initialization
            var companyId = $.cookie('bsl_1');
            lookup({
                textBoxId: 'txtChooser',
                url: $(hdApiUrl).val() + 'api/search/items?CompanyId=' + companyId + '&keyword=',
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
            $('#btnAdd').click(function () {
                addToList()
            });
            //Add to list ends here

            //Function for add to list
            function addToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var qty = parseFloat($('#txtQuantity').val());
                var rate = parseFloat(tempItem.CostPrice);
                var taxper = parseFloat(tempItem.TaxPercentage);
                var TaxAmount = parseFloat(((rate * (taxper / 100))).toFixed(2));
                var GrossAmount = parseFloat((qty * rate).toFixed(2));
                var NetAmount = parseFloat((GrossAmount + TaxAmount * qty).toFixed(2));
                var tempTax = (TaxAmount * qty).toFixed(2);
                var description = tempItem.Description.replace(/\n/g, '<br/>');
                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null & !isNaN(qty)) {
                    var Rows = $('#listTable > tbody').children('tr');
                    var itemExists = false;
                    var rowOfItem;
                    $(Rows).each(function () {
                        var itemId = $(this).children('td').eq(0).text();

                        var instanceId = $(this).children('td').eq(10).text();

                        if (tempItem.ItemID == itemId && tempItem.InstanceId == instanceId) {
                            itemExists = true;
                            rowOfItem = this;
                        }
                    });
                    if (itemExists) {
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
                    else {
                        html = '';
                        html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td><b>' + tempItem.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + description + '</i></td><td>' + tempItem.ItemCode + '</td><td style="text-align:right">' + tempItem.MRP + '</td><td style="text-align:right">' + tempItem.CostPrice + '</td><td style="text-align:right" contenteditable="true" class="numberonly">' + qty + '</td><td style="text-align:right">' + GrossAmount + '</td><td style="text-align:right">' + tempItem.TaxPercentage + '</td><td style="text-align:right">' + tempTax + '</td><td style="text-align:right">' + NetAmount + '</td><td style="display: none">' + tempItem.InstanceId + '</td><td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
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
            $('#txtQuantity').keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    addToList()
                }
            });
            //Add item with enter key ends here

            //Binding Event to Save button
            $('#btnSave').off().click(function () {
                save(false);
            });
            //Binding save event ends here

            //Printing the Request
            $('#btnMail').click(function () {
                var id = $('#hdId').val();
                if (id != 0) {
                    $('#mailModal').modal({ backdrop: 'static', keyboard: false, show: true })
                    $('#indentId').text(id);
                    //var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    //var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    //var url = "/purchase/Print/"+type+"/"+number+"/Request.aspx?id=" + id+"&location="+$.cookie('bsl_2');
                    //PopupCenter(url, 'purchaseRequest', 800, 700);
                }

            });

            //save and print Request
            $('#btnSavePrint').off().click(function () {
                save(true);
            });

            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#indentId').text('0');
                $('#hdId').val('');
                var date = new Date();
                LoadTandCSettings();
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#txtEntryDate').datepicker('setDate', today);
                var due = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 7);
                $('#txtDuedate').datepicker('setDate', due);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#btnMail').hide();
                $('#btnPrint').hide();
                $('#btnSavePrint').html('<i class=\"ion ion-printer"\></i>&nbsp;Save & Mail');
                $('#ddlCostCenter').select2('val', 0);
                $('#ddlJob').select2('val', 0);
            }
            //Reset ends here

            //Get single indentdetails
            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaseIndent/get/' + id,
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
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);
                            $('#txtNarration').val(register.Narration);
                            if (isClone) {
                                $('#hdId').val('0');
                                $('#btnPrint').hide();
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                            }
                            else {
                                $('#hdId').val(register.ID);
                                $('#btnPrint').show();
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                            }

                            var html = '';
                            $(register.Products).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td style="text-align:right">' + this.CostPrice + '</td>';
                                html += '<td style="text-align:right" contenteditable="true" class="numberonly">' + this.Quantity + '</td>';
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
                            //Additional data 
                            $('#dvTandC').summernote('code', register.TermsandConditon);
                            $('#dvTandC').summernote({
                                placeholder: '',
                                height: 450,
                                airMode: true,
                                popover: {
                                    air: [
                                      ['color', ['color']],
                                      ['font', ['bold', 'underline', 'clear']]
                                    ]
                                }
                            });

                            $('#dvPaymentTerm').summernote('code', register.Payment_Terms);
                            $('#dvPaymentTerm').summernote({
                                placeholder: '',
                                height: 450,
                                airMode: true,
                                popover: {
                                    air: [
                                      ['color', ['color']],
                                      ['font', ['bold', 'underline', 'clear']]
                                    ]
                                }
                            });
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
            function save(MailtoSupplier) {
                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save",
                    closeOnConfirm: true
                },
                function (isConfirm) {
                    if (isConfirm) {
                        var data = {};
                        var arr = [];
                        var tbody = $('#listTable > tbody');
                        var tr = tbody.children('tr');
                        var dueDate = $('#txtDuedate').val();
                        var entryDate = $('#txtEntryDate').val();
                        var narration = $('#txtNarration').val();
                        var costCenterId = $('#ddlCostCenter').val();
                        var jobId = $('#ddlJob').val();
                        var rOff = $('#txtRoundOff').val();
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var priority = $('#ChkPriority').is(':checked');
                        var TandC = $('#dvTandC').summernote('code');
                        var PaymentTerms = $('#dvPaymentTerm').summernote('code');

                        for (var i = 0; i < tr.length; i++) {
                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(6)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(11)').text();
                            var desc = $(tr[i]).children('td').eq(1).children('i').html();
                            var detail = { "Quantity": qty };
                            detail.InstanceId = instanceId;
                            detail.Description = desc;
                            detail.ItemID = id;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.DueDate = dueDate;
                        data.CostCenterId = costCenterId;
                        data.JobId = jobId;
                        data.RequestDate = entryDate;
                        data.RoundOff = rOff;
                        data.Priority = priority;
                        data.Products = arr;
                        data.Narration = narration;
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.TermsandConditon = TandC;
                        data.Payment_Terms = PaymentTerms;
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/purchaseindent/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    resetRegister();

                                    $('#lblOrderNo').text(response.Object.OrderNo);

                                    if (MailtoSupplier == true) {
                                        $('#mailModal').modal({ backdrop: 'static', keyboard: false, show: true })
                                        $('#indentId').text(response.Object.Id);

                                    }
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

                });


            }
            //Save Function Ends here

            //Mail modal configuration
            $("#mailModal").on('hidden.bs.modal', function () {
                $('.before-send').show();
                $('.after-send').hide();
                $('#txtToMail').val('');
                $('#txtCCMail').val('');
                $('#txtBCCMail').val('');

            });

            //Mail Modal send mail
            $('#mailModal').on('click', '#btnSendMail', function () {
                $('.before-send').fadeOut('slow', function () {
                    $('.after-send').fadeIn();
                    var toMailid = $('#txtToMail').val();
                    var CcMailid = $('#txtCCMail').val();
                    var BccMailid = $('#txtBCCMail').val();
                    var data = {};
                    data.SupplierMail = toMailid;
                    data.SupplierMailCC = CcMailid;
                    data.SupplierMailBCC = BccMailid;
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var indentId = $('#indentId').text();
                    var url = $(location).attr('protocol') + '//' + $(location).attr('host') + '/Purchase/Print/' + type + '/' + number + '/Indent?id=' + indentId + '&location=' + $.cookie('bsl_2');
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/purchaseindent/SendSupplierMail?indentId=' + indentId+'&url='+url,
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify(data),
                        success: function (response) {
                            if (response.Success) {
                                successAlert(response.Message);
                                $('#mailModal').modal('hide');
                                $('#txtToMail').val() !== '' ? $('#txtToMail')[0].selectize.destroy() : $('#txtToMail').val('');
                                $('#txtCCMail').val() !== '' ? $('#txtCCMail')[0].selectize.destroy() : $('#txtCCMail').val('');
                                $('#txtBCCMail').val() !== '' ? $('#txtBCCMail')[0].selectize.destroy() : $('#txtBCCMail').val('');
                            }
                        }
                    });
                });
            });
            //Mail modal ends here

            //To mail modal
            $('#mailModal').on('focus', '#txtToMail', function () {
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaseindent/GetSupplierMail',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie('bsl_1')),
                    success: function (response) {
                        var options = [];

                        for (var i = 0; i < response.length; i++) {
                            options.push({ id: response[i].Name + "<" + response[i].Email + ">", title: response[i].Name })
                        }
                        var $select = $('#txtToMail').selectize({
                            maxItems: null,
                            valueField: 'id',
                            labelField: 'id',
                            searchField: 'id',
                            options: options,
                            create: true,
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });
            //To mail modal ends here

            //CC mail modal
            $('#mailModal').on('focus', '#txtCCMail', function () {
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaseindent/GetSupplierMail',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie('bsl_1')),
                    success: function (response) {
                        var options = [];

                        for (var i = 0; i < response.length; i++) {
                            options.push({ id: response[i].Name + "<" + response[i].Email + ">", title: response[i].Name })
                        }
                        var $select = $('#txtCCMail').selectize({
                            maxItems: null,
                            valueField: 'id',
                            labelField: 'id',
                            searchField: 'id',
                            options: options,
                            create: true,
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });
            //CC mail modal ends here

            //BCC Mail modal
            $('#mailModal').on('focus', '#txtBCCMail', function () {
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaseindent/GetSupplierMail',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie('bsl_1')),
                    success: function (response) {
                        var options = [];

                        for (var i = 0; i < response.length; i++) {
                            options.push({ id: response[i].Name + "<" + response[i].Email + ">", title: response[i].Name })
                        }
                        var $select = $('#txtBCCMail').selectize({
                            maxItems: null,
                            valueField: 'id',
                            labelField: 'id',
                            searchField: 'id',
                            options: options,
                            create: true,
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });
            //BCC mail modal ends here

            //Find function start
            $('#btnFind').click(function () {
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

                            var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
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
                refreshTable(null, null);
                function refreshTable(fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/purchaseIndent/Get?&from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.IndentNo + '</td>';
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
                        },
                        //error: function (xhr) { alert(xhr.responseText); console.log(xhr); loading('stop'); },
                        //beforeSend: function () { loading('start') },
                        //complete: function () { loading('stop'); },
                    });
                }
            });
            // binding event to Edit row
            $('#tblRegister').off().on('click', '.edit-register', function () {
                var registerId = $(this).closest('tr').children('td').eq(0).text();
                getRegister(false, registerId);
            });

            $('#ddlCountry').change(function () {
                loadStates(0);
            })

            //BtnNew starts here 
            $('#btnNew').click(function () {
                resetRegister();
            });

            //Delete functionality
            $('#btnDelete').click(function () {
                if ($('#hdId').val() != 0) {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",

                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            var id = $('#hdId').val();
                            var modifiedBy = $.cookie('bsl_3');
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/purchaseIndent/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        $('#hdId').val('');
                                        resetRegister();
                                        //$('#lblOrderNo').text(response.Object.OrderNo);
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
                else {
                    errorAlert('Select an Entry to Delete');
                }

            });

            //Funtions to add Masters
            $('#addnewItem').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewItem(Url, User, Company, function (id, name) {

                });
            });

        });
        //Document ready ends here 
    </script>
    <%--summer note linking--%>
    <link href="../Theme/assets/summernote/summernote.css" rel="stylesheet" />
    <script src="../Theme/assets/summernote/summernote.min.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/Sections/Supplier.js"></script>
    <script src="../Theme/Sections/Items.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/selectize.js"></script>
    <script src="../Theme/assets/selectize.min.js"></script>
    <!-- Date Range Picker -->
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
    <link href="../Theme/css/selectize.bootstrap3.css" rel="stylesheet" />
</asp:Content>

