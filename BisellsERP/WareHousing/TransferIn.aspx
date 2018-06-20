<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferIn.aspx.cs" Inherits="BisellsERP.WareHousing.TransferIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ware Housing Transfer In</title>
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
        /*.table-scroll tbody {
            height: 54vh;
        }*/

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
    <div class="row p-b-5">
        <div class="col-sm-4">
            <h3 class="page-title">Transfer In</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" accesskey="v" id="btnFind" data-toggle="tooltip" data-placement="bottom" title="View previous transfer in" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new transfer in . Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current transfer in" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                <button type="button" accesskey="p" id="btnPrint" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light "><i class="ion ion-printer"></i></button>
                <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion ion-trash-b"></i></button>
            </div>
        </div>

    </div>
    <div class="row search-quote-panel">
        <div class="col-sm-12">
            <div class="row">

                <div class="col-sm-4">
                    <div class="panel b-r-8">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label class="title-label">Transfer Date</label>
                                    <label id="lblTransferDate">xx</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel b-r-8">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label class="title-label">From Location</label>
                                    <label id="lblFromLocation">xx</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="btn-group" data-toggle="tooltip" title="Link Transfer Out">
                                <button type="button" id="btnLink" data-toggle="modal" data-target="#myModal" class="btn btn-icon waves-effect waves-light btn-warning m-t-10">
                                    <i class="ion-pull-request text-bold"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="panel b-r-8">
                        <div class="panel-body" style="padding-top: 9.5px; padding-bottom: 9.5px">
                            <div class="row">
                                <div class="col-sm-12">
                                    <span>Date : </span>
                                    <input type="text" id="txtEntryDate" style="width: 60%;" class="date-info" value="01/Oct/2017" />
                                </div>
                                <div class="col-sm-12">
                                    <span>Order No :</span>
                                    <asp:Label ID="lblOrderNo" ClientIDMode="Static" runat="server" CssClass="badge badge-danger pull-right" Text="856542"></asp:Label>
                                    <div class="clearfix"></div>
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
                                        <th class="hidden">item_id</th>
                                        <th class="hidden">TodId</th>
                                        <th>Item Name</th>
                                        <th>Item Code</th>
                                        <th style="text-align: right">Tax%</th>
                                        <th style="text-align: right">MRP</th>
                                        <th style="text-align: right">Qty</th>
                                        <th style="display: none">instanceId</th>
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
                    <h4 class="modal-title">Previous Transfer Ins &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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
                                <th class="hidden">TransferInId</th>
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

    <%-- ---- Modal for Quote Building ---- --%>
    <div id="myModal" class="modal animated fadeIn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-w-lg">
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                    <div class="modal-title" id="myModalLabel">
                    </div>
                </div>
                <div class="modal-body modal-body-lg">
                    <table id="transferOutTable" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey ">
                            <tr>
                                <th class="hidden">TransferOutId</th>
                                <th class="tect-white">Location</th>
                                <th class="text-white">Order No</th>
                                <th class="text-white">Date</th>
                                <th class="text-white">Tax</th>
                                <th class="text-white">Gross</th>
                                <th class="text-white">Net</th>
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
                            <h4 class="m-b-0">Total TOs : &nbsp<label class="text-success" id="noOfItems"></label></h4>
                        </div>

                        <div class="col-sm-5 col-md-7">
                            <div class="btn-toolbar pull-right">
                                <button id="btnMerge" class="btn btn-primary waves-effect waves-light" aria-expanded="true" type="button"><i class="ion ion-steam"></i>Merge</button>
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
                <div class="col-sm-12">
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
        //documnet ready function starts here
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
            $('#txtDueDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });
            $('#txtEntryDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });

            $('#btnPrint').click(function () {
                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/Warehousing/Print/" + type + "/" + number + "/TransferIn?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'TransferIn', 800, 700);
                }

            });

            $('#txtEntryDate').datepicker('setDate', today);
            $('#txtDueDate').datepicker('setDate', today);

            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtEntryDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtEntryDate').datepicker('hide');
           });
            // binding event to select All checkbox
            $('#transferOutTable').on('change', '.chk-all', function () {

                if ($(this).is(':checked')) {
                    var rows = $('#transferOutTable tbody').children('tr');
                    for (var i = 0; i < rows.length; i++) {
                        if ($(rows[i]).children('td:last-child').children('input').prop('checked') != undefined) {
                            $('.chk-single').prop('checked', true);
                            $(rows[i]).addClass('selected-row');
                        }
                    }
                }
                else {
                    var rows = $('#transferOutTable tbody').children('tr');
                    for (var i = 0; i < rows.length; i++) {
                        $('.chk-single').prop('checked', false);
                        $(rows[i]).removeClass('selected-row');
                    }
                }

            });

            //binding event to single checkbox
            $('#transferOutTable').on('click', '.chk-single', function () {

                if ($(this).is(':checked')) {
                    $(this).closest('tr').children('td').eq(6).children('input').focus().select();
                    $(this).closest('tr').addClass('selected-row');
                }
                else {
                    $(this).closest('tr').removeClass('selected-row');
                }
            });


            setInterval(function () {
                var rows = $('#transferOutTable tbody').children('tr');
                var noOfItems = 0.00;
                rows.each(function (index) {
                    if ($(this).find('.chk-single').is(':checked')) {

                        noOfItems++;
                    }

                });
                $('#noOfItems').text(noOfItems);
            }, 500);

            //Save function call
            $('#btnSave').off().click(function () {
                save();
            });

            //link with TransferOut function starts here
            $('#btnLink').click(function () {

                $('#transferOutTable tbody').children().remove();

                var locationId = $.cookie('bsl_2');
                var companyId = $.cookie('bsl_1');
                var financialYear = $.cookie('bsl_4');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/TransferIn/GetTransferOut/',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: (locationId),
                    success: function (response) {
                        var html = '';
                        $(response).each(function () {
                            html += '<tr>';
                            html += '<td style="display:none">' + this.ID + '</td>';
                            html += '<td>' + this.Location + '</td>';
                            html += '<td>' + this.EntryNo + '</td>';
                            html += '<td>' + this.EntryDateString + '</td>';
                            html += '<td>' + this.TaxAmount + '</td>';
                            html += '<td>' + this.Gross + '</td>';
                            html += '<td>' + this.NetAmount + '</td>';
                            html += '<td><input type="checkbox" class="checkbox chk-single"></td>';
                            html += '</tr>';
                        });

                        $('#transferOutTable tbody').append(html);
                        //Merge TransferOut
                        $('#btnMerge').click(function () {

                            rows = $('#transferOutTable tbody').children('tr');
                            $('#lblFromLocation').text(response[0].Location);
                            for (var i = 0; i < response.length; i++) {
                                $('#lblTransferDate').text(response[i].EntryDateString);
                            }

                            html = '';
                            $(rows).each(function () {

                                if ($(this).children('td').eq(7).children('input').is(':checked')) {
                                    var toId = $(this).children('td').eq(0).text();

                                    for (var i = 0; i < response.length; i++) {
                                        if (response[i].ID == toId) {
                                            $(response[i].Products).each(function () {
                                                html += '<tr>';
                                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                                html += '<td style="display:none">' + this.DetailsID + '</td>';
                                                html += '<td>' + this.Name + '</td>';
                                                html += '<td>' + this.ItemCode + '</td>';
                                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                                html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                                html += '<td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                                html += '</tr>'
                                            });
                                            $('#listTable tbody').children().remove();
                                            $('#listTable tbody').append(html);
                                            $('#transferOutTable tbody').children().remove();
                                            $('#myModal').modal('hide');
                                            break;
                                        }
                                    }
                                }
                            });
                            //Binding Event to remove button
                            $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                        });//Merge Transfer out end here
                    },
                    error: function (err) {
                        alert(err.responseText)
                    },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });

            });
            //link with Transfer Out ends here

            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/TransferIn/get/' + id,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        try {
                            var fromLocation = $('#ddlLocation').val();
                            var html = '';
                            var register = response;
                            $('#txtEntryDate').val(register.EntryDateString);
                            $('#txtNarration').val(register.Narration);
                            $('#ddlLocation').val(register.FromLocation);
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
                                html += '<td style="display:none">' + this.TodId + '</td>';
                                html += '<td>' + this.Name + '</td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td style="text-align:right">' + this.Quantity + '</td>';
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

                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);
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
                    error: function (xhr) { alert(xhr.responseText); },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });
            }

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
                        var fromLocation = $('#ddlLocation').val();
                        var LocationId = $.cookie('bsl_2');
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var createdBy = $.cookie('bsl_3');
                        //Additional Details tab data
                        var TandC = $('#dvTandC').summernote('code');
                        for (var i = 0; i < tr.length; i++) {

                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var todId = $(tr[i]).children('td:nth-child(2)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(8)').text();
                            var detail = {};
                            detail.InstanceId = instanceId;
                            detail.ItemID = id;
                            detail.TodId = todId;

                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.TermsAndConditions = TandC;
                        data.fromLocation = $('#ddlLocation').val();
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.EntryDate = entryDate;
                        data.EntryDateString = entryDate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.FromLocation = $.cookie('bsl_2');
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.UserId = $.cookie('bsl_3');
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/TransferIn/Save',
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

            //Find function start
            $('#btnFind').click(function () {
                if ($('#ddlLocation').val() != 0) {
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
                            url: $('#hdApiUrl').val() + 'api/TransferIn/Get?from=' + fromDate + '&to=' + toDate,
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify($.cookie('bsl_2')),
                            contentType: 'application/json;charset=utf-8',
                            success: function (response) {
                                var fromLocation = $('#ddlLocation').val();

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
                            //error: function (xhr) { alert(xhr.responseText); },
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
                }
                else {
                    errorField('#ddlLocation');
                }
            });
            //find function ends here
            //BtnNew starts here 
            $('#btnNew').click(function () {
                resetRegister();
            });

            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#txtEntryDate').datepicker('setDate', today);
                $('#lblTransferDate').text('');
                $('#lblFromLocation').text('');
                $('#btnPrint').hide();
                LoadTandCSettings();
                $('#btnSave').html('<i class=\"ion-archive"\></i>Save');
                $('#ddlJob').select2('val', '0');
                $('#ddlCostCenter').select2('val', '0');

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
                                url: $('#hdApiUrl').val() + 'api/TransferIn/delete/' + id,
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
                                    alert(xhr.responseText); console.log(xhr);
                                    miniLoading('stop');
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
        //documnet ready function ends here
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
