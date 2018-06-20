<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jobs.aspx.cs" Inherits="BisellsERP.Party.Jobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Jobs</title>
    <style>
        #wrapper {
            overflow: hidden;
        }

        .left-side {
            height: calc(100vh - 198px);
            overflow-y: auto;
            overflow-x: hidden;
        }

            .left-side table tbody td {
                padding: 15px 14px;
                cursor: pointer;
            }

            .left-side table thead td {
                padding: 4px 10px;
            }


            .left-side table tbody tr.active td, .left-side table tbody tr.active th {
                background-color: rgba(30, 136, 229, 0.1);
            }

            .left-side table tbody tr.active, .left-side table tbody tr.active {
                border-left: 10px solid #1E88E5;
            }

        .right-side {
            height: calc(100vh - 216px);
        }

        .m-b-9 {
            margin-bottom: 9px;
        }

        .searchDropdown {
            width: 160px !important;
            height: 30px;
        }

        .despatch-wrap {
            position: relative;
            float: right;
        }

            .despatch-wrap label {
                position: absolute;
                left: 2px;
                top: -20px;
                font-weight: 500;
                color: #90A4AE;
            }

            .despatch-wrap button {
                padding: 5px 8px;
            }
        /*inner nav*/
        .nav-inner.nav-tabs > li > a, .nav.tabs-vertical > li > a {
            line-height: 43px;
        }
        
        .nav-inner.nav-tabs > li.active {
            border-bottom: 2px solid #1e88e5;
        }

        .h4-color {
            color: #78a1d0;
        }

        .fa-color {
            color: #b4b2c6;
        }

        .md-2x {
            font-size: 7em;
        }

        .table-pos {
            margin-top: 20px;
        }

        .customer-overview {
            background-color: #fbfbfb;
            padding: 10px 20px 30px;
            margin-top: 10px;
            margin-left: 5px;
            border-radius: 5px;
            box-shadow: 0 2px 1px 0 #eee;
            border: 1px solid #eee;
            position: relative;
        }

            .customer-overview .title-wrap img {
                border: 1px solid #ddd;
            }

            .customer-overview .title-wrap {
                margin-bottom: 10px;
                padding: 20px 10px 10px;
                border-bottom: 1px dashed #ccc;
            }

            .customer-overview p {
                color: #616161;
                padding-left: 35px;
            }

                .customer-overview p label {
                    font-weight: 100;
                }

            .customer-overview i {
                color: #b3b3b3;
                margin-right: 10px;
            }

            .customer-overview h5 {
                font-weight: bold;
                margin-bottom: 5px;
                color: #616161;
            }

        .nav.nav-tabs {
            border-bottom: 1px solid #eee;
        }

        #addModal .control-label {
            color: #546E7A;
            font-weight: 100;
        }

        .nav.nav-tabs + .tab-content, .tabs-vertical-env .tab-content {
            background: #FFF;
            padding: 10px;
            margin: 0 0 30px;
            box-shadow: none;
        }

        thead > tr > th {
            padding: 10px !important;
            border-bottom: 1px solid #ddd !important;
            color: #6f726f;
        }
         .ul-lookup > .card {
            background-color: #fff;
            min-height: 170px;
            padding: 9px 10px;
            /*box-shadow: 0 2px 1px 0 #ccc;*/
            margin: 5px auto;
            border-radius: 5px;
            border: 1px dashed #ccc;
        }

            .ul-lookup > .card > div:nth-child(2) > div {
                display: inline-block;
                margin-left: 20px;
            }

            .ul-lookup > .card > div:first-child input {
                display: inline-block;
                width: 85%;
            }

        .ul-lookup input[type=number] {
            width: 100%;
            border-radius: 3px;
            border: 1px solid #cfd8dc;
            height: 34px;
            padding: 4px;
            font-size: 16px;
            color: #607D8B;
        }

        .ul-lookup .control-label {
            color: #546E7A;
        }

        .btn-toolbar-list {
            margin-left: -17px;
        }

        #jobs .control-label {
            font-weight: 100;
        }

        #jobs .checkbox label::before {
            border-radius: 50%;
        }

        #jobs .checkbox label::after {
            margin-left: -22px;
        }

        #jobs b {
            color: #78909C;
            font-size: 12px;
        }

        .label-chkbx {
            font-size: 12px;
        }
       #jobModal label.control-label {
            color: #607D8B;
            font-weight: 100;
        }

        #jobModal .form-group {
            margin-bottom: 5px;
        }

        #jobModal .modal-dialog .modal-content {
            padding: 15px;
        }

        .nav.nav-tabs {
            border-bottom: 1px solid #eee;
        }

            .nav.nav-tabs + .tab-content, .tabs-vertical-env .tab-content {
                background: #FFF;
                padding: 10px;
                margin: 0 0 30px;
                box-shadow: none;
            }
             .list-scroll {
            max-height: calc(100vh - 260px);
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <input type="hidden" value="0" id="hdId" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Jobs</h3>
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
                    <div class="list-group-item active m-b-9">
                        <div class="btn-group">
                            <button type="button" class="trans-btn dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                All Jobs &nbsp;<b id="countOfInvoices">(0)</b>&nbsp;<span class="caret"></span>
                            </button>
                        </div>
                        <input type="text" class="form-control listing-search-entity" placeholder="Search Name..." />
                        <a id="searchInvoice" href="#" class="pull-right"><i class="fa fa-search filter-list"></i></a>
                        <div class="pull-right t-y search-group">
                        </div>
                        <div class="pull-right t-y search-group ">
                        </div>
                    </div>
                    <div class="left-side">
                        <div class="panel">
                            <div class="panel-body p-0" style="min-height: calc(100vh - 225px)">
                                <table id="invoiceList" class="table left-table invoice-list">
                                    <thead>
                                        <tr>
                                            <th class="show-on-collapsed">#</th>
                                            <th class="show-on-collapsed">Job</th>
                                            <th class="show-on-collapsed">Customer</th>
                                            <th>Estimated Amount</th>
                                            <th>Start Date</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                                <div class="empty-state-wrap" style="display: none">
                                    <img class="empty-state-icon" src="../Theme/images/empty_state.png" />
                                    <h4 class="empty-state-title">Nothing to show</h4>
                                    <p class="empty-state-text">Oooh oh, there are no results that match your filters</p>
                                    <div class="btn-group empty-state-buttons">
                                        <a href="#" data-toggle="modal" data-target="#addModal" class="btn btn-default btn-xs btn-rounded waves-effect">Create New Job</a>
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
        <div class="right-sidebar hidden" style="">
            <div class="col-md-8">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="btn-toolbar m-b-5 m-t-5" role="toolbar">
                            <div class="btn-group">
                                <button type="button" id="btnAdd" class="btn btn-default waves-effect waves-light" data-toggle="modal" data-target="#addModal"><i class="md md-add-circle-outline"></i>&nbsp;Add Job</button>
                                <%--<button type="button" data-toggle="modal" data-target="#addModal" class="btn btn-default waves-effect waves-light" id="btnAdd" title="New"><i class="fa fa-plus"></i></button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
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

                <div class="panel panel-default m-t-10 right-side">
                    <div class="panel-body p-0">
                        <ul class="nav nav-inner nav-tabs">
                            <li class="active">
                                <a href="#overview" data-toggle="tab" aria-expanded="false">
                                    <span>Overview</span>
                                </a>
                            </li>
                        </ul>

                        <div class="tab-content row lead-list">
                            <%-- tab for overview --%>
                            <div class="tab-pane active" id="overview">
                                <div class="tab-pane" id="jobs">
                                    <div class="col-md-12">
                                        <div class="row panel-list">
                                            <div class="col-sm-12">
                                                <div id="noJobSection" class="empty-state-wrap" style="">
                                                    <img class="empty-state-icon" src="../Theme/images/empty_state.png" />
                                                    <h4 class="empty-state-title">Nothing to show</h4>
                                                    <p class="empty-state-text">Oooh oh, there are no jobs</p>
                                                    <div class="btn-group empty-state-buttons">
                                                        <a href="#" data-toggle="modal" data-target="#addModal" id="btnCreateJob" class="btn btn-default btn-xs btn-rounded waves-effect">Create Job</a>
                                                    </div>
                                                </div>
                                                <ul id="addlist" class="list-unstyled ul-lookup">
                                                </ul>
                                            </div>
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

    <%-- Job creation modal--%>
    <div id="jobModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Job Creation&nbsp;</h4>
                </div>
                <div class="modal-body p-b-0 p-t-5">
                    <div class="row cust-form">

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Job</label>
                                <input type="text" id="txtJob" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Start Date</label>
                                <input type="text" id="txtStartDate" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Estimated End Date</label>
                                <input type="text" id="txtEndDate" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Estimate Amount</label>
                                <input type="text" id="txtEstAmt" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Customer</label>
                                <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group hidden">
                                <label class="control-label">Contact Address</label>
                                <textarea id="txtContactAddress" class="form-control"></textarea>
                            </div>

                            <div class="row dashed-b-r m-t-15">
                                <div class="col-xs-12">
                                    <h4 class="m-t-0"><i class="md md-home text-muted"></i>&nbsp;Contact Address</h4>
                                </div>
                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label class="control-label">Salutation</label>
                                        <select id="ddlSalutation" class="form-control input-sm">
                                            <option value="Mr">Mr </option>
                                            <option value="Mrs">Mrs </option>
                                            <option value="Ms">Ms </option>
                                            <option value="Miss">Miss </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-9">
                                    <div class="form-group">
                                        <label class="control-label">Contact Person</label>
                                        <input type="text" id="txtContactPers" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 1</label>
                                        <textarea rows="2" id="txtContAddr1" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 2</label>
                                        <textarea rows="2" id="txtContAddr2" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                        <input type="text" id="txtCity" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Zip</label>
                                        <input type="number" id="txtZip" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Country</label>
                                        <asp:DropDownList ID="ddlCountry" CssClass="form-control input-sm" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">State</label>
                                        <select class="form-control input-sm" id="ddlState">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 1</label>
                                        <input type="number" id="txtPhone1" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 2</label>
                                        <input type="number" id="txtPhone2" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input type="text" id="txtEmail" class="form-control input-sm" />
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="col-sm-6">
                            <div class="form-group hidden">
                                <label class="control-label">Site Address</label>
                                <textarea id="txtSiteAddress" class="form-control"></textarea>
                            </div>


                            <div class="row m-t-15">
                                <div class="col-xs-12">
                                    <h4 class="m-t-0"><i class="md md-store text-muted"></i>&nbsp;Site Address</h4>
                                </div>
                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label class="control-label">Salutation</label>
                                        <select id="ddlSiteSalutation" class="form-control input-sm">
                                            <option value="Mr">Mr </option>
                                            <option value="Mrs">Mrs </option>
                                            <option value="Ms">Ms </option>
                                            <option value="Miss">Miss </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-9">
                                    <div class="form-group">
                                        <label class="control-label">Contact Person</label>
                                        <input type="text" id="txtSiteContPer" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 1</label>
                                        <textarea rows="2" id="txtSiteAddr1" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 2</label>
                                        <textarea rows="2" id="txtSiteAddr2" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                        <input type="text" id="txtSiteCity" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Zip</label>
                                        <input type="number" id="txtSiteZip" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Country</label>
                                        <asp:DropDownList ID="ddlSiteCountry" CssClass="form-control input-sm" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">State</label>
                                        <select class="form-control input-sm" id="ddlSiteState">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 1</label>
                                        <input type="number" id="txtSitePh1" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 2</label>
                                        <input type="number" id="txtSitePh2" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input type="text" id="txtSiteEmail" class="form-control input-sm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 pull-right">
                            <div class="btn-toolbar pull-right m-t-15">
                                <button id="btnSaveJob" type="button" class="btn btn-default btn-sm btn-job">Create</button>
                                <button type="button" class="btn btn-inverse btn-sm" data-dismiss="modal" aria-hidden="true">x</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>

    <script>
        //Document ready starts here
        $(document).ready(function () {

            $(".addon > ul").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px"
            });

            $(".left-side").niceScroll({
                cursorcolor: "#6d7993",
                cursorwidth: "8px"
            });
            $('#txtStartDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });

            $('#txtEndDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });

            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtStartDate').datepicker('setDate', today);
            $('#txtEndDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtStartDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtStartDate').datepicker('hide');
           });
            $('#txtEndDate').datepicker()
                  .on('changeDate', function (ev) {
                      $('#txtEndDate').datepicker('hide');
                  });
            //load currency symbol
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            //load data into table function call
            refreshTable();

            //load State
            function LoadStates(stateId, country, state) {
                var selected = country.val();
                if (selected == 0) {
                    state.empty();
                    state.append("<option value='0'>--Select States--</option>")
                }
                else {
                    state.empty();
                    state.append("<option value='0'>--Select States--</option>")
                    $.ajax({
                        type: "POST",
                        url: $('#hdApiUrl').val() + "api/customers/GetStates?id=" + selected,
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($.cookie("bsl_1")),
                        dataType: "json",
                        success: function (data) {
                            state.empty;
                            var html = '';
                            $.each(data, function () {
                                html += '<option value="' + this.StateId + '">' + this.State + '</option>';
                            });
                            state.append(html);
                            state.val(stateId);
                        },
                        failure: function () {
                            console.log("Error");
                        }
                    });
                }
            }

            //Load state function call when contact address country changed
            $('#ddlCountry').change(function () {
                LoadStates(0, $(this), $('#ddlState'));
            });

            //Load state function call when site address country changed
            $('#ddlSiteCountry').change(function () {
                LoadStates(0, $(this), $('#ddlSiteState'));
            });

            //reset modal
            $('#btnAdd').click(function () {
                $('#txtJob').val('');
                $('#txtEstAmt').val('');
                $('#txtStartDate').val('');
                $('#txtEndDate').val('');
                $('#ddlSalutation').val('0');
                $('#txtContactPers').val('');
                $('#ddlSiteSalutation').val('0');
                $('#ddlCustomer').select2('val', '0');
                $('#txtSiteContPer').val('');
                $('#txtContAddr1').val('');
                $('#txtContAddr2').val('');
                $('#txtSiteAddr1').val('');
                $('#txtSiteAddr2').val('');
                $('#txtCity').val('');
                $('#txtZip').val('');
                $('#txtSiteCity').val('');
                $('#txtSiteZip').val('');
                $('#ddlCountry').val('0');
                $('#ddlState').val('0');
                $('#ddlSiteCountry').val('0');
                $('#ddlSiteState').val('0');
                $('#txtPhone1').val('');
                $('#txtPhone2').val('');
                $('#txtSitePh1').val('');
                $('#txtSitePh2').val('');
                $('#txtEmail').val('');
                $('#txtSiteEmail').val('');
                $('#jobModal').modal({ backdrop: 'static', keyboard: false, show: true });
            });

            //function for loading data into the table
            function refreshTable() {
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Jobs/GetJobs',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_1')),
                    success: function (data) {

                        if (data == null || data.length <= 0) {
                            $('#invoiceList tbody').children().remove();
                            $('.empty-state-wrap').show();
                            return;
                        }
                        else {
                            $('.empty-state-wrap').hide();
                        }
                        console.log(data)
                        var html = '';
                        var count = 0;
                        var currencySymbol = JSON.parse($('#hdSettings').val()).CurrencySymbol;
                        $(data).each(function (index) {
                            count++;
                            html += '<tr>';
                            html += '<td class="show-on-collapsed">' + this.ID + '</td>';
                            html += '<td class="show-on-collapsed">' + this.JobName + '</td>';
                            html += '<td class="show-on-collapsed">' + this.Customer + '</td>';
                            html += '<td>' + this.EstimatedAmount + '</td>';
                            html += '<td>' + this.StartDateString + '</td>';
                            if (this.Status == 0) {
                                html += '<td><label class="label label-default">Create</label></td>';
                            }
                            if (this.Status == 1) {
                                html += '<td><label class="label label-success">In Progress</label></td>';
                            }
                            if (this.Status == 2) {
                                html += '<td><label class="label label-info">Completed</label></td>';
                            }
                            if (this.Status == 3) {
                                html += '<td><label class="label label-danger">Closed</label></td>';
                            }
                            html += '</tr>';
                        });
                        $('#invoiceList tbody').children().remove();
                        $('#countOfInvoices').text('(' + count + ')');
                        $('#invoiceList tbody').append(html);
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });

            }

            //Delete job
            $('#addlist').on('click', '.delete-job', function () {
                var data = {};
                var jobId = $(this).closest('li').attr('data-jobid');
                data.ID = jobId;
                data.ModifiedBy = $.cookie('bsl_3');
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
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/jobs/Delete',
                            method: 'DELETE',
                            contentType: 'application/json;charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify(data),
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    $('.md-cancel').trigger('click');
                                    refreshTable();
                                }
                                else {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (err) {
                                alert(err.responseText);
                                loading('stop', null);
                            },
                            beforeSend: function () { miniLoading('start', null) },
                            complete: function () { miniLoading('stop', null); },
                        });
                    }
                });
            });

            //Load single job details
            function LoadJobs(id) {
                var currencySymbol = JSON.parse($('#hdSettings').val()).CurrencySymbol;
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Jobs/GetJob?job_id=' + id,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    success: function (response) {
                        var html = '';
                        if (response != null && response.length != 0) {
                            $('#noJobSection').hide();
                            $(response).each(function () {
                                html += '<li class="card row" data-jobId="' + this.ID + '"><div class="col-sm-6"><h4 class="text-muted m-t-0"><a href="/Party/Job?UID=' + this.ID + '">' + this.JobName + '</a><small class="pull-right m-t-5"><strong>Start Date:</strong>' + this.StartDateString + '</small></h4><div class="row m-t-30"><div class="col-sm-6"><p><b>Contact Name</b></p><p class="m-t-0">' + this.ContactName + '</p></div><div class="col-sm-6 text-right"><p><b>Estimate</b></p><p class="m-t-0">' + currencySymbol + '&nbsp;' + this.EstimatedAmount + '</p></div></div><div class="row m-t-10"><div class="col-sm-6"><p><b>Contact Address</b></p><p class="m-t-0">' + this.ContactAddress + '</p></div><div class="col-sm-6 text-right"><p><b>Site Address</b></p><p class="m-t-0">' + this.SiteAddress + '</p></div></div></div><div class="col-sm-6 text-right"><div class="row "><div class="btn-toolbar"><button type="button" value="add" class="btn btn-default btn-sm waves-effect waves-ripple add-task"><i class="ion-plus text-success"></i>&nbsp;Add Task</button><button type="button" value="edit" class="btn btn-default btn-sm waves-effect waves-ripple edit-job"><i class="ion-edit text-info"></i>&nbsp;Edit</button><button type="button" value="edit" class="btn btn-default btn-sm waves-effect waves-ripple delete-job"><i class="fa fa-trash text-danger"></i>&nbsp;Delete</button><div class="dropdown pull-right"><button class="btn btn-default btn-sm waves-effect waves-ripple dropdown-toggle select-job m-l-5" type="button" data-toggle="dropdown">More<span class="caret"></span></button><ul class="dropdown-menu pull-left"><li><a href="../Purchase/Quote?JOB=' + this.ID + '">Purchase Order</a></li><li><a href="../Purchase/Entry?JOB=' + this.ID + '">New Bill</a></li><li><a href="../Sales/Quote?JOB=' + this.ID + '">Estimate</a></li><li><a href="../Sales/Entry?JOB=' + this.ID + '">Sales Invoice</a></li></ul></div></div></div><div class="row m-t-40"><div class="col-md-3"><label class="label-chkbx">Created</label><div class="checkbox checkbox-success">';

                                if (this.Status == '0') {
                                    html += '<input class="chk-status-create" type="checkbox" checked="true" /><label for="chk-status-create"></label></div></div><div class="col-md-3"><label class="label-chkbx">Inprogress</label><div class="checkbox checkbox-success"><input class="chk-status-inprogress" type="checkbox"  /><label for="chk-status-inprogress"></label></div></div><div class="col-md-3"><label class="label-chkbx">Completed</label><div class="checkbox checkbox-success"><input class="chk-status-complete" type="checkbox" /><label for="chk-status-complete"></label></div></div><div class="col-md-3"><label class="label-chkbx">Closed</label><div class="checkbox checkbox-success"><input class="chk-status-close" type="checkbox" /><label for="chk-status-close"></label></div></div></div></div></li>';
                                }
                                else if (this.Status == '1') {
                                    html += '<input class="chk-status-create" type="checkbox" checked="true" /><label for="chk-status-create"></label></div></div><div class="col-md-3"><label class="label-chkbx">Inprogress</label><div class="checkbox checkbox-success"><input class="chk-status-inprogress" type="checkbox" checked="true" /><label for="chk-status-inprogress"></label></div></div><div class="col-md-3"><label class="label-chkbx">Completed</label><div class="checkbox checkbox-success"><input class="chk-status-complete" type="checkbox" /><label for="chk-status-complete"></label></div></div><div class="col-md-3"><label class="label-chkbx">Closed</label><div class="checkbox checkbox-success"><input class="chk-status-close" type="checkbox" /><label for="chk-status-close"></label></div></div></div></div></li>';
                                }
                                else if (this.Status == '2') {
                                    html += '<input class="chk-status-create" type="checkbox" checked="true" /><label for="chk-status-create"></label></div></div><div class="col-md-3"><label class="label-chkbx">Inprogress</label><div class="checkbox checkbox-success"><input class="chk-status-inprogress" type="checkbox" checked="true" /><label for="chk-status-inprogress"></label></div></div><div class="col-md-3"><label class="label-chkbx">Completed</label><div class="checkbox checkbox-success"><input class="chk-status-complete" type="checkbox"  checked="true" /><label for="chk-status-complete"></label></div></div><div class="col-md-3"><label class="label-chkbx">Closed</label><div class="checkbox checkbox-success"><input class="chk-status-close" type="checkbox" /><label for="chk-status-close"></label></div></div></div></div></li>';
                                }
                                else if (this.Status == '3') {
                                    html += '<input class="chk-status-create" type="checkbox" checked="true" /><label for="chk-status-create"></label></div></div><div class="col-md-3"><label class="label-chkbx">Inprogress</label><div class="checkbox checkbox-success"><input class="chk-status-inprogress" type="checkbox" checked="true" /><label for="chk-status-inprogress"></label></div></div><div class="col-md-3"><label class="label-chkbx">Completed</label><div class="checkbox checkbox-success"><input class="chk-status-complete" type="checkbox"  checked="true" /><label for="chk-status-complete"></label></div></div><div class="col-md-3"><label class="label-chkbx">Closed</label><div class="checkbox checkbox-success"><input class="chk-status-close" type="checkbox"  checked="true" /><label for="chk-status-close"></label></div></div></div></div></li>';
                                }
                                else {
                                    html += '<input class="chk-status-create" type="checkbox"/><label for="chk-status-create"></label></div></div><div class="col-md-3"><label class="label-chkbx">Inprogress</label><div class="checkbox checkbox-success"><input class="chk-status-inprogress" type="checkbox"  /><label for="chk-status-inprogress"></label></div></div><div class="col-md-3"><label class="label-chkbx">Completed</label><div class="checkbox checkbox-success"><input class="chk-status-complete" type="checkbox" /><label for="chk-status-complete"></label></div></div><div class="col-md-3"><label class="label-chkbx">Closed</label><div class="checkbox checkbox-success"><input class="chk-status-close" type="checkbox" /><label for="chk-status-close"></label></div></div></div></div></li>';
                                }

                            });
                            $('#addlist').children().remove();
                            $('#addlist').append(html);
                        }
                        else {
                            $('#addlist').children().remove();
                            $('#noJobSection').show();
                        }
                        miniLoading('stop', null);
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () {
                        $("#addlist").niceScroll({
                            cursorcolor: "#6d7993",
                            cursorwidth: "8px",
                            horizrailenabled: false
                        });

                    },
                });
            }

            //Show right sidebar when invoice is clicked
            $(document).on('click', '.left-side table tbody tr', function () {
                $('.left-sidebar').find('[class="col-md-12"]').removeClass('col-md-12').addClass('col-md-4 collapsed');
                $('.left-side table tbody').find('.active').removeClass('active');
                $('.search-group').hide();
                $('.right-sidebar').removeClass('hidden');
                $(".left-side").getNiceScroll().resize();
                $(this).addClass('active');
                var id = $(this).children('td').eq(0).text();
                LoadJobs(id);
                $('.right-sidebar').fadeOut();
                $('.right-sidebar').fadeIn();
            });

            //Popover
            $('#addlist').on('click', '.select-job', function () {
                $('.select-job').popover({
                    placement: 'down',
                    html: true,
                    content: $('#jobWrap').html()
                }).on('click', function () {

                });
            })

            //Save jobs
            $('#btnSaveJob').click(function () {
                var data = {};
                var CustomerId = $('#ddlCustomer').val();
                var jobName = $('#txtJob').val();
                var salutation = $('#ddlSalutation').val();
                var completedDate = $('#txtEndDate').val();
                var contPer = $('#txtContactPers').val();
                var siteSalut = $('#ddlSiteSalutation').val();
                var siteContPer = $('#txtSiteContPer').val();
                var addr1 = $('#txtContAddr1').val();
                var addr2 = $('#txtContAddr2').val();
                var siteAddr1 = $('#txtSiteAddr1').val();
                var siteAddr2 = $('#txtSiteAddr2').val();
                var city = $('#txtCity').val();
                var zip = $('#txtZip').val();
                var siteCity = $('#txtSiteCity').val();
                var siteZip = $('#txtSiteZip').val();
                var countryId = $('#ddlCountry').val();
                var stateId = $('#ddlState').val();
                var siteCountryId = $('#ddlSiteCountry').val();
                var siteStateId = $('#ddlSiteState').val();
                var ph1 = $('#txtPhone1').val();
                var ph2 = $('#txtPhone2').val();
                var Siteph1 = $('#txtSitePh1').val();
                var Siteph2 = $('#txtSitePh2').val();
                var email = $('#txtEmail').val();
                var siteEmail = $('#txtSiteEmail').val();
                var startDate = $('#txtStartDate').val();
                var EstimatedAmount = $('#txtEstAmt').val();
                var Id = $('#hdId').val();
                data.ID = Id;
                data.JobName = jobName;
                data.CustomerId = CustomerId;
                data.Status = 0;
                data.CompanyId = $.cookie("bsl_1");
                data.CreatedBy = $.cookie('bsl_3');
                data.ModifiedBy = $.cookie('bsl_3');
                data.StartDate = startDate;
                data.StartDateString = startDate;
                data.EstimatedAmount = EstimatedAmount;
                data.CompletedDate = completedDate;
                data.ContactName = contPer;
                data.SiteContactName = siteContPer;
                data.SiteContactPhone1 = Siteph1;
                data.ContactPhone1 = ph1;
                data.ContactPhone2 = ph2;
                data.SiteContactPhone2 = Siteph2;
                data.ContactAddress = addr1;
                data.ContactAddress2 = addr2;
                data.SiteAddress = siteAddr1;
                data.SiteContactAddress2 = siteAddr2;
                data.ContactCity = city;
                data.SiteContactCity = siteCity;
                data.StateId = stateId;
                data.SiteStateId = siteStateId;
                data.CountryId = countryId;
                data.SiteCountryId = siteCountryId;
                data.ZipCode = zip;
                data.SiteZipCode = siteZip;
                data.Email = email;
                data.SiteEmail = siteEmail;
                data.Salutation = salutation;
                data.SiteSalutation = siteSalut;
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/save',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        console.log(response)
                        if (response.Success) {
                            successAlert(response.Message);
                            refreshTable();
                            $('#txtJob').val('');
                            $('#txtEstAmt').val('');
                            $('#txtStartDate').val('');
                            $('#txtEndDate').val('');
                            $('#ddlSalutation').val('0');
                            $('#ddlSiteSalutation').val('0');
                            $('#txtContactPers').val('');
                            $('#txtSiteContPer').val('');
                            $('#txtContAddr1').val('');
                            $('#txtContAddr2').val('');
                            $('#txtSiteAddr1').val('');
                            $('#txtSiteAddr2').val('');
                            $('#txtCity').val('');
                            $('#txtZip').val('');
                            $('#txtSiteCity').val('');
                            $('#txtSiteZip').val('');
                            $('#ddlCustomer').select2('val', '0');
                            $('#ddlCountry').val('0');
                            $('#ddlState').val('0');
                            $('#ddlSiteCountry').val('0');
                            $('#ddlSiteState').val('0');
                            $('#txtPhone1').val('');
                            $('#txtPhone2').val('');
                            $('#txtSitePh1').val('');
                            $('#txtSitePh2').val('');
                            $('#txtEmail').val('');
                            $('#txtSiteEmail').val('');
                            $('#jobModal').modal('hide');
                            $('#btnSaveJob').html('<i class="fa fa-plus"></i>&nbsp;Save ');
                            $('#hdId').val(0);
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Job status
            //job Status create
            $('#addlist').on('click', '.chk-status-create', function () {
                var data = {};
                var jobId = $(this).closest('li').attr('data-jobid');
                data.ID = jobId;
                data.Status = 0;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/UpdateStatus',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            LoadJobs(jobId);
                            refreshTable();
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () { miniLoading('stop', null); },
                });
            });

            //job Status Inprogress
            $('#addlist').on('click', '.chk-status-inprogress', function () {
                var data = {};
                var jobId = $(this).closest('li').attr('data-jobid');
                data.ID = jobId;
                data.Status = 1;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/UpdateStatus',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            LoadJobs(jobId);
                            refreshTable();
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () { miniLoading('stop', null); },
                });
            });

            //job Status Completed
            $('#addlist').on('click', '.chk-status-complete', function () {
                var data = {};
                var jobId = $(this).closest('li').attr('data-jobid');
                data.ID = jobId;
                data.Status = 2;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/UpdateStatus',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            LoadJobs(jobId);
                            refreshTable();
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () { miniLoading('stop', null); },
                });
            });

            //job Status closed
            $('#addlist').on('click', '.chk-status-close', function () {
                var data = {};
                var jobId = $(this).closest('li').attr('data-jobid');
                data.ID = jobId;
                data.Status = 3;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/UpdateStatus',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            LoadJobs(jobId);
                            refreshTable();
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () { miniLoading('stop', null); },
                });
            });
            //Job status ends here

            //Edit of job
            $('#addlist').on('click', '.edit-job', function () {
                var jobId = $(this).closest('li').attr('data-jobid');
                var cusId = $('#cusId').text();
                $('#hdId').val(jobId);
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/GetJob/?job_id=' + jobId,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    success: function (response) {
                        //console.log(response);
                        $('#jobModal').modal({ backdrop: 'static', keyboard: false, show: true });
                        $('#txtJob').val(response.JobName);
                        $('#txtStartDate').val(response.StartDateString);
                        $('#txtEndDate').val(response.CompletedDateString);
                        $('#ddlSalutation').val(response.Salutation);
                        $('#ddlCustomer').select2('val', response.CustomerId);
                        $('#ddlSiteSalutation').val(response.SiteSalutation);
                        $('#txtContactPers').val(response.ContactName);
                        $('#txtSiteContPer').val(response.SiteContactName);
                        $('#txtContAddr1').val(response.ContactAddress);
                        $('#txtContAddr2').val(response.ContactAddress2);
                        $('#txtSiteAddr1').val(response.SiteAddress);
                        $('#txtSiteAddr2').val(response.SiteContactAddress2);
                        $('#txtCity').val(response.ContactCity);
                        $('#txtZip').val(response.ZipCode);
                        $('#txtSiteCity').val(response.SiteContactCity);
                        $('#txtSiteZip').val(response.SiteZipCode);
                        $('#ddlCountry').val(response.CountryId);
                        $('#ddlSiteCountry').val(response.SiteCountryId);
                        LoadStates(response.SiteStateId, $('#ddlSiteCountry'), $('#ddlSiteState'));
                        LoadStates(response.SiteStateId, $('#ddlSiteCountry'), $('#ddlSiteState'));
                        LoadStates(response.StateId, $('#ddlCountry'), $('#ddlState'));
                        $('#txtPhone1').val(response.ContactPhone1);
                        $('#txtPhone2').val(response.ContactPhone2);
                        $('#txtSitePh1').val(response.SiteContactPhone1);
                        $('#txtSitePh2').val(response.SiteContactPhone2);
                        $('#txtEmail').val(response.Email);
                        $('#txtSiteEmail').val(response.SiteEmail);
                        $('#txtEstAmt').val(response.EstimatedAmount);
                        $('#btnSaveJob').html('Update');
                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () { miniLoading('stop', null); },
                });
            })

            //Add task
            $('#addlist').on('click', '.add-task', function () {
                var jobId = $(this).closest('li').attr('data-jobid');
                window.open('/Party/Job?UID=' + jobId, '_self');
            });

            //Search job 
            $('.listing-search-entity').on('change keyup', function () {
                searchOnTable($('.listing-search-entity'), $('#invoiceList'), 1);
            });

            //Hide right sidebar when search is clicked
            $('#searchInvoice, .close-sidebar').click(function () {
                var sidebar = $('.left-sidebar').children().hasClass('col-md-4 collapsed');
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

        });//Document ready ends here

    </script>

    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.debug.js"></script>
    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>
</asp:Content>
