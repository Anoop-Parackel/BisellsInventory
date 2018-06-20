<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferOut.aspx.cs" Inherits="BisellsERP.WareHousing.TransferOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ware Housing Transfer Out</title>
    <style>
        #wrapper {
            overflow: hidden;
        }

        .panel {
            margin-bottom: 10px;
        }

        .view-h {
            min-height: 65vh;
            max-height: 65vh;
        }

        tbody tr td {
            padding: 5px !important;
            font-size: smaller;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 30px;
        }

        .daterangepicker.dropdown-menu.ltr.opensleft.show-calendar {
            right: auto !important;
        }

        .overflow-content {
            height: 90vh;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%--hidden fileds--%>
    <asp:HiddenField runat="server" Value="" ID="hdTandC" ClientIDMode="Static" />
    <input type="hidden" id="hdId" value="0" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Transfer Out</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" accesskey="v" id="btnFind" data-toggle="tooltip" data-placement="bottom" title="View previous transfer out" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                <button id="btnNew" type="button" accesskey="n" data-toggle="tooltip" data-placement="bottom" title="Start a new transfer out . Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" id="btnSave" accesskey="s" data-toggle="tooltip" data-placement="bottom" title="Save the current transfer out" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                <button type="button" id="btnPrint" accesskey="p" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light "><i class="ion ion-printer"></i></button>
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
                                <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
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
                                <label class="title-label">To Location</label>
                                <asp:DropDownList ID="ddlLocation" ClientIDMode="Static" CssClass="form-control round-no-border" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3">
                    <div class="panel b-r-8">
                        <div class="panel-body" style="padding-top: 17.5px; padding-bottom: 17.5px;">
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
                                        <th class="hidden">ItemId</th>
                                        <th>Item Name</th>
                                        <th>Item Code</th>
                                        <th style="text-align: right">Tax%</th>
                                        <th style="text-align: right">MRP</th>
                                        <th style="text-align: right">Qty</th>
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

    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">
            <!-- Modal content-->
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Transfer Outs &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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
                    <table id="tblRegister" class="table table-hover table-striped table-responsive table-scroll">
                        <thead>
                            <tr>
                                <th class="hidden">TransferOutId</th>
                                <th>Order No</th>
                                <th>Date</th>
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
                <div class="col-md-12">
                    <h5 class="sett-title p-t-0">General</h5>

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
                <div class="col-md-12">
                    <h5 class="sett-title p-t-0">Terms & Conditions</h5>
                    <div class="summernote-editor1"></div>
                    <div id="dvTandC" class="summernote-editor"></div>
                </div>

            </div>
        </div>

    </div>
    <script>

        $(document).ready(function () {

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
                    $('#btnPrint').hide();
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
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
                    $('#btnPrint').hide();
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                }
            }

            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('[data-toggle="popover"]').popover({
                content: "<textarea placeholder=\" Enter Narration Here\"></textarea>"
            });

            $('#txtEntryDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });

            $('#txtEntryDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtEntryDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtEntryDate').datepicker('hide');
           });

            //lookup initialization
            var locationId = $.cookie('bsl_2');
            lookup({
                textBoxId: 'txtChooser',
                url: $('#hdApiUrl').val() + 'api/Search/ItemsFromPurchaseWithStock?LocationId=' + locationId + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'Stock'],
                visibility: [false, false, true, true, true, true, true],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Stock'],
                key: 'ItemID',
                dataToShow: 'Name',
                OnLoading: function () { miniLoading('start') },
                OnComplete: function () { miniLoading('stop') }
            });
            //lookup initialization ends here

            //Add Item to list
            $('#btnAdd').click(function () {
                AddToList();
            });
            function AddToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var taxper = parseFloat(tempItem.TaxPercentage);
                //var rate = parseFloat(tempItem.CostPrice);
                var qty = parseFloat($('#txtQuantity').val());
                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null) {

                    if (parseFloat(tempItem.Stock) < qty) {
                        $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                    }
                    else {
                        var Rows = $('#listTable > tbody').children('tr');
                        var itemExists = false;
                        var rowOfItem;
                        $(Rows).each(function () {
                            var itemId = $(this).children('td').eq(0).text();
                            var instanceId = $(this).children('td').eq(7).text();
                            if (tempItem.ItemID == itemId && tempItem.InstanceId == instanceId) {
                                itemExists = true;
                                rowOfItem = this;
                            }

                        });

                        if (itemExists) {
                            var existingQty = parseFloat($(rowOfItem).children('td').eq(5).text());
                            var newQty = existingQty + qty;
                            if (parseFloat(tempItem.Stock) < newQty) {
                                $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                            }
                            else {
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

                        }
                        else {
                            html = '';
                            html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td>' + tempItem.Name + '</td><td>' + tempItem.ItemCode + '</td><td style="text-align:right">' + tempItem.TaxPercentage + '</td><td style="text-align:right">' + tempItem.MRP + '</td><td style="text-align:right">' + qty + ' </td><td style="display:none">0</td><td style="display:none">' + tempItem.InstanceId + '</td><td>  <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
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
            $('#txtQuantity').keypress(function (e) {

                if (e.which == 13) {
                    e.preventDefault();
                    AddToList();
                }

            });

            //Save functionality
            $('#btnSave').off().click(function () {
                save();
            });
            //Function for Saving the register
            function save() {

                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",

                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,

                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        var data = {};
                        var arr = [];
                        var tbody = $('#listTable > tbody');
                        var tr = tbody.children('tr');
                        var entryDate = $('#txtEntryDate').val();
                        var narration = $('#txtNarration').val();
                        var toLocation = $('#ddlLocation').val();
                        var LocationId = $.cookie('bsl_2');
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var createdBy = $.cookie('bsl_3');
                        var TandC = $('#dvTandC').summernote('code');
                        for (var i = 0; i < tr.length; i++) {

                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(6)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(8)').text();
                            var detail = { "Quantity": qty };
                            detail.InstanceId = instanceId;
                            detail.ItemID = id;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.TermsAndConditions = TandC;
                        data.toLocation = $('#ddlLocation').val();
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.EntryDate = entryDate;
                        data.EntryDateString = entryDate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.UserId = $.cookie('bsl_3');
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/TransferOut/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    resetRegister();
                                    $('#lblOrderNo').text(response.Object.OrderNo);

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
                            beforeSend: function () { miniLoading('start') },
                            complete: function () { miniLoading('stop') }
                        });
                    }

                });
            }//Save Function Ends here

            $('#btnPrint').click(function () {
                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/Warehousing/Print/" + type + "/" + number + "/TransferOut?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'TransferOut', 800, 700);
                }

            });

            //Find function start
            $('#btnFind').click(function () {      //Find Filter Function
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
                resetRegister();
                refreshTable(null, null);
                function refreshTable(fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/TransferOut/Get?from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var toLocation = $('#ddlLocation').val();
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.EntryNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += this.status == 0 ? '<td><span class="label label-danger">In Active</span></td>' : '<td><span class="label label-default">Active</span></td>';
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
            //find function ends here
            //BtnNew starts here 
            $('#btnNew').click(function () {
                resetRegister();
            });

            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/TransferOut/get/' + id,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        try {
                            var toLocation = $('#ddlLocation').val();
                            var html = '';
                            var register = response;
                            $('#txtEntryDate').val(register.EntryDateString);
                            $('#txtNarration').val(register.Narration);
                            $('#ddlLocation').val(register.ToLocation);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);
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
                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                html += '<td style="display:none">0</td>';
                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                html += '</tr>';
                            });
                            $('#listTable tbody').append(html);
                            $('#lblOrderNo').text(register.EntryNo);
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
            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#btnPrint').hide();
                LoadTandCSettings();
                $('#txtEntryDate').datepicker('setDate', today);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#ddlCostCenter').select2('val', '0');
                $('#ddlJob').select2('val', '0');
            }//Reset ends here

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
                    }, function (isConfirm) {
                        var id = $('#hdId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/TransferOut/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        resetRegister();
                                    }
                                    else {
                                        errorAlert(response.Message);
                                    }
                                },
                                error: function (xhr) {
                                    alert(xhr.responseText); console.log(xhr); miniLoading
                                },
                                beforeSend: function () { miniLoading('start') },
                                complete: function () { miniLoading('stop') }

                            });
                        }

                    });
                }

            });
            //delete function ends here

            //Init HTML EDITOR(Summer note Editor Initilization)
            $('.additional-settings-button').click(function () {
                $('#dvTandC').summernote({
                    height: 450,
                    focus: false,
                });
                $('#dvPaymentTerm').summernote({
                    height: 450,
                    focus: false,
                });
            });



            LoadTandCSettings();

            //To load the Tand C in settings
            function LoadTandCSettings() {

                $('#dvTandC').summernote('code', $('#hdTandC').val());
            }



        });
        //document ready function ends here
    </script>
    <%--Linking of Summernote--%>
    <link href="../Theme/assets/summernote/summernote.css" rel="stylesheet" />
    <script src="../Theme/assets/summernote/summernote.min.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <!-- Date Range Picker -->
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
</asp:Content>
