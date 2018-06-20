<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Leads.aspx.cs" Inherits="BisellsERP.Party.Leads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Leads</title>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <input type="hidden" value="0" id="hdId" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Leads</h3>
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
                                All Leads &nbsp;<b id="countOfInvoices">(0)</b>&nbsp;<span class="caret"></span>
                            </button>
                        </div>
                        <input type="text" class="form-control listing-search-entity" placeholder="Search Name..." />
                           <a id="searchInvoice" href="#" class="pull-right"><i class="fa fa-search filter-list"></i></a>
                        <div class="pull-right t-y search-group">
                        </div>
                        <div class="pull-right t-y search-group ">
                             <span class="filter-span">
                                <label>Filter by Assigns</label>
                                 <input type="text" id="txtDate" name="daterange" value="01/01/2015 - 01/31/2015" />
                            </span>
                            <span class="filter-span">
                                <label>Filter by Assigns</label>
                                <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="searchDropdown" runat="server">
                                </asp:DropDownList>
                            </span>
                            <span class="filter-span">
                                <label>Filter by Status</label>
                               <asp:DropDownList ClientIDMode="Static" CssClass="searchDropdown" ID="ddlStat" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">New Lead</asp:ListItem>
                                <asp:ListItem Value="2">Pending</asp:ListItem>
                                <asp:ListItem Value="3">Followup</asp:ListItem>
                                <asp:ListItem Value="4">Process</asp:ListItem>
                                <asp:ListItem Value="5">Declined</asp:ListItem>
                            </asp:DropDownList>
                            </span>
                        </div>
                    </div>
                    <div class="left-side">
                        <div class="panel">
                            <div class="panel-body p-0" style="min-height: calc(100vh - 225px)">
                                <table id="invoiceList" class="table left-table invoice-list">
                                    <thead>
                                        <tr>
                                            <th style="display: none">Id</th>
                                            <th class="show-on-collapsed">Name</th>
                                            <th class="show-on-collapsed">Email</th>
                                            <th>Phone</th>
                                            <th>Lead Status</th>
                                            <th>Source</th>
                                            <th>Assigns</th>
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
                                        <a href="#" data-toggle="modal" data-target="#addModal" class="btn btn-default btn-xs btn-rounded waves-effect">Create New Lead</a>
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
                                <button type="button" data-toggle="modal" data-target="#addModal" id="btnEditLead" class="btn btn-default waves-effect waves-light" title="edit"><i class="md md-mode-edit"></i></button>
                                <button type="button" data-toggle="modal" data-target="#addModal" class="btn btn-default waves-effect waves-light" id="btnAddLead" title="New"><i class="fa fa-plus"></i></button>
                                <button type="button" id="btnDeleteLead" class="btn btn-default waves-effect waves-light" title="delete"><i class="fa fa-trash-o"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light  dropdown-toggle" data-toggle="dropdown" aria-expanded="false" id="btnMore">More&nbsp;<span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li><a href="#" class="change-status" id="btnChangeStatus">Change Status</a></li>
                                    <li><a id="btnChangeCust" href="#">Convert to customer</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="despatch-wrap" style="display: none;">
                            <label>Change Status</label>
                            <asp:DropDownList ClientIDMode="Static" CssClass="searchDropdown" ID="ddlStatus" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">New Lead</asp:ListItem>
                                <asp:ListItem Value="2">Pending</asp:ListItem>
                                <asp:ListItem Value="3">Followup</asp:ListItem>
                                <asp:ListItem Value="4">Process</asp:ListItem>
                                <asp:ListItem Value="5">Declined</asp:ListItem>
                            </asp:DropDownList>
                            <button type="button" class="btn btn-icon waves-effect waves-light btn-green status-update"><i class="fa fa-check"></i></button>
                            <button type="button" class="btn btn-icon waves-effect waves-light btn-inverse status-close"><i class="fa fa-close"></i></button>
                        </div>

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
                                <div class="">
                                    <div class="col-sm-7">
                                        <div class="customer-overview row">
                                            <div class="title-wrap row">
                                                <div class="col-sm-3">
                                                    <img id="imgPhoto" class="img-circle" src="../Theme/images/profile-pic.jpg" width="50" />
                                                </div>
                                                <div class="col-sm-9">
                                                    <h3 class="text-right" id="lblLeadName"></h3>
                                                    <label style="display: none" id="leadId">0</label>
                                                </div>
                                            </div>
                                            <h5><i class="fa fa-home">&nbsp;</i>Address</h5>
                                            <p>
                                                <label id="lblLeadAddress1">Address1,   </label>
                                            </p>
                                            <p class="m-t-0">
                                                <label id="lblLeadAddress2">Address2</label>
                                            </p>
                                            <h5><i class="fa fa-envelope">&nbsp;</i>Email</h5>
                                            <p><span class="text-primary" id="lblLeadEmail"></span></p>
                                            <h5><i class="fa fa-phone">&nbsp;</i>Contact</h5>
                                            <p>
                                                <label id="lblLeadPhone1">Phone 1   </label>
                                            </p>
                                            <p class="m-t-0">
                                                <label id="lblLeadPhone2">Phone 2</label>
                                            </p>
                                            <h5 class="m-t-20"><i class="fa fa-bookmark">&nbsp;</i>Other Details</h5>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <p>TIN/GST No : <span id="lblGstNo"></span></p>
                                                    <p class="m-t-0">CIN/Reg No : <span id="lblSinNo"></span></p>
                                                    <p class="m-t-0">Lead Status : <span id="lblLeadStatus"></span></p>
                                                    <p class="m-t-0">Lead Source : <span id="lblLeadSource"></span></p>
                                                    <p class="m-t-0">Details  : <span id="lblDetails"></span></p>
                                                </div>
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
    <%--find list modal ends here--%>

    <!-- Modal for updating salary-->
    <div id="addModal" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <div class="row">
                        <div class="col-xs-8">
                            <h4 class="modal-title" id="myModalLabel">Add new Lead</h4>
                        </div>

                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="row before-send p-t-25">
                        <input id="hdLeadId" type="hidden" value="0" />
                        <input id="hdLeadSaveId" type="hidden" value="0" />
                        <input id="hdStatus" type="hidden" value="0" />
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Source</label>
                                    <select class="form-control input-sm" id="ddlLeadSource">
                                        <option value="0">--Select--</option>
                                        <option value="1">Daddy Street</option>
                                        <option value="2">Facebook</option>
                                        <option value="3">Tele Sales</option>
                                        <option value="4">Twitter</option>
                                        <option value="5">Management</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Assigned</label>
                                    <asp:DropDownList ID="ddlAssign" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Name</label>
                                    <input type="text" id="txtLeadName" placeholder="Name" class="form-control input-sm" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label class="control-label">Contact</label>
                                    <select class="form-control" style="height: 30px;" id="ddlLeadSalutation">
                                        <option value="Mr">Mr.</option>
                                        <option value="Mrs">Mrs.</option>
                                        <option value="Ms">Ms.</option>
                                        <option value="Miss">Miss.</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="control-label"></label>
                                    <input type="text" class="form-control" id="txtLeadContactName" placeholder="Contact Name" />
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="control-label">Address 1</label>
                                    <input type="text" id="txtLeadAddr1" placeholder="Address Line 1" class="form-control input-sm" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Address 2</label>
                                    <input type="text" id="txtLeadAddr2" placeholder="Address Line 1" class="form-control input-sm" />
                                </div>
                            </div>
                        </div>

                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">City</label>
                                    <input type="text" id="txtLeadCity" placeholder="City" class="form-control input-sm" />

                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Zip Code</label>
                                    <input type="text" id="txtLeadZipCode" placeholder="ZipCode" class="form-control input-sm" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Phone 1</label>
                                    <input type="text" id="txtLeadPh1" placeholder="Phone Number 1" class="form-control input-sm" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Phone 2</label>
                                    <input type="text" id="txtLeadPh2" placeholder="Phone Number 1" class="form-control input-sm" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Country</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCountry" AutoPostBack="false" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">State/Province</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlState" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Email</label>
                                    <input type="email" id="txtLeadEmail" class="form-control input-sm" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">TIN/GST No</label>
                                    <input type="text" id="txtLeadTax1" placeholder="TIN/GST No" class="form-control input-sm" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">CIN/Reg No</label>
                                    <input type="text" id="txtLeadTax2" placeholder="CIN/Reg No" class="form-control input-sm" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Lead Status</label>
                                    <select class="form-control input-sm" id="ddlLeadPrimaryStatus">
                                        <option value="0">--Select--</option>
                                        <option value="1">New Lead</option>
                                        <option value="2">Pending</option>
                                        <option value="3">Followup</option>
                                        <option value="4">Process</option>
                                        <option value="5">Declined</option>
                                    </select>
                                </div>
                            </div>
                              <div class="col-sm-8">
                                <div class="form-group">
                                    <label class="control-label">Details</label>
                                      <textarea id="txtDetails" class="form-control" rows="3" placeholder="Details"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 m-t-15">
                            <div class="btn-toolbar pull-right">
                                <button id="btnAdd" type="button" class="btn btn-default m-t-5"><i class="md-done"></i>&nbsp;Save</button>
                                <button id="btnUpdate" type="button" class="btn btn-default m-t-5"><i class="md-done"></i>&nbsp;Update</button>
                                <button type="button" id="btnCancel" class="btn btn-inverse m-t-5" data-dismiss="modal">Close</button>
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
    <!-- Date Range Picker -->
 <%--   <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />--%>
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
    <script>
        $(document).ready(function () {

            $(".addon > ul").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px"
            });

            $(".left-side").niceScroll({
                cursorcolor: "#6d7993",
                cursorwidth: "8px"
            });

            //Show Change status
            $('.change-status').on('click', function () {
                $('.despatch-wrap').fadeIn('slow');
            });

            //Hide Change status
            $('.status-close').on('click', function () {
                $('.despatch-wrap').fadeOut('slow');
            });

            $(document).on('click', '.filter-list', function () {
                var empId = $('#ddlEmployee').val() == '0' ? null : $('#ddlEmployee').val();
                var status = $('#ddlStat').val() == '0' ? null : $('#ddlStat').val();
                var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                refreshTable(leadId, status, empId, fromDate, toDate);
            })


            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            refreshTable(null,null,null,null,null);
            $('#btnAdd').click(function () {
                saveLead();
            });
            function saveLead() {
                var Lead = {};
                Lead.Name = $('#txtLeadName').val();
                Lead.Address1 = $('#txtLeadAddr1').val();
                Lead.Address2 = $('#txtLeadAddr2').val();
                Lead.CountryId = $('#ddlCountry').val();
                Lead.StateId = $('#ddlState').val();
                Lead.Phone1 = $('#txtLeadPh1').val();
                Lead.Phone2 = $('#txtLeadPh2').val();
                Lead.Email = $('#txtLeadEmail').val();
                Lead.Taxno1 = $('#txtLeadTax1').val();
                Lead.Taxno2 = $('#txtLeadTax2').val();
                Lead.ID = $('#hdLeadSaveId').val();
                Lead.City = $('#txtLeadCity').val();
                Lead.ZipCode = $('#txtLeadZipCode').val();
                Lead.ContactName = $('#txtLeadContactName').val();
                Lead.Salutation = $('#ddlLeadSalutation').val();
                Lead.ModifiedBy = $.cookie('bsl_3');
                Lead.CompanyId = $.cookie("bsl_1");
                Lead.CreatedBy = $.cookie('bsl_3');
                Lead.Status = $('#ddlLeadPrimaryStatus').val();
                Lead.Source = $('#ddlLeadSource').val();
                Lead.AssignId = $('#ddlAssign').val();
                Lead.Details = $('#txtDetails').val();
                console.log($('#ddlLeadPrimaryStatus').val());
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Leads/SaveLeads',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Lead),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            refreshTable(null, null, null,null,null);
                            $('#hdLeadId').val("0");
                            $("#addModal").modal('hide');
                        }

                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            }

            //Date range initialization
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



            $('#btnUpdate').hide();
            $('#btnAdd').show();


            $('#btnAddLead').click(function () {
                reset();
                
                $('#btnUpdate').hide();
                $('#btnAdd').show();
            });

            $('#btnEditLead').click(function () {
                
                $('#btnUpdate').show();
                $('#btnAdd').hide();
            });

            $('#btnUpdate').click(function () {
                updateLead();
            });

            function updateLead() {
                var Lead = {};
                Lead.Name = $('#txtLeadName').val();
                Lead.Address1 = $('#txtLeadAddr1').val();
                Lead.Address2 = $('#txtLeadAddr2').val();
                Lead.CountryId = $('#ddlCountry').val();
                Lead.StateId = $('#ddlState').val();
                Lead.Phone1 = $('#txtLeadPh1').val();
                Lead.Phone2 = $('#txtLeadPh2').val();
                Lead.Email = $('#txtLeadEmail').val();
                Lead.Taxno1 = $('#txtLeadTax1').val();
                Lead.Taxno2 = $('#txtLeadTax2').val();
                Lead.ID = $('#hdLeadId').val();
                Lead.City = $('#txtLeadCity').val();
                Lead.ZipCode = $('#txtLeadZipCode').val();
                Lead.ContactName = $('#txtLeadContactName').val();
                Lead.Salutation = $('#ddlLeadSalutation').val();
                Lead.ModifiedBy = $.cookie('bsl_3');
                Lead.CompanyId = $.cookie("bsl_1");
                Lead.CreatedBy = $.cookie('bsl_3');
                Lead.Status = $('#ddlLeadPrimaryStatus').val();
                Lead.Source = $('#ddlLeadSource').val();
                Lead.AssignId = $('#ddlAssign').val();
                Lead.Details = $('#txtDetails').val();
                console.log($('#ddlLeadPrimaryStatus').val());
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Leads/SaveLeads',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Lead),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            refreshTable(null, null, null, null, null);
                            var id = $('#hdLeadId').val();
                            var trs= $('#invoiceList tbody').children('tr');
                            for (var i = 0; i < trs.length; i++) {
                                if ($(trs[i]).children('td').eq(0).text() == id) {
                                    $(trs[i]).addClass('active');
                                    $(trs[i]).trigger('click');
                                    break;
                                }
                            }
                            $('#hdLeadId').val("0");
                            $("#addModal").modal('hide');
                        }

                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            }

            $('#btnCancel').click(function () {
                $("#addModal").modal('hide');
            });

            $('#btnEditLead').click(function () {
                id = $('#hdLeadId').val();
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Leads/GetLeads/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        var countryId = response.CountryId;
                        $.ajax({
                            url: $('#hdApiUrl').val() + '/api/Customers/getStates/' + countryId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (data) {
                                console.log(data);
                                $('#ddlCountry').val(response.CountryId);
                                $('#ddlState').children('option').remove();
                                $('#ddlState').append('<option value="0">--select--</option>');
                                $(data).each(function () {
                                    $('#ddlState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                });
                                $('#ddlState').val(response.StateId);
                                $('#txtLeadName').val(response.Name);
                                $('#txtLeadAddr1').val(response.Address1);
                                $('#txtLeadAddr2').val(response.Address2);
                                $('#txtLeadPh1').val(response.Phone1);
                                $('#txtLeadPh2').val(response.Phone2);
                                $('#txtLeadTax1').val(response.Taxno1);
                                $('#txtLeadTax2').val(response.Taxno2);
                                $('#txtLeadEmail').val(response.Email);
                                $('#hdLeadId').val(response.ID);
                                $('#txtLeadCity').val(response.City);
                                $('#txtLeadZipCode').val(response.ZipCode);
                                $('#ddlLeadSalutation').val(response.Salutation);
                                $('#txtLeadContactName').val(response.ContactName);
                                $('#ddlLeadPrimaryStatus').val(response.Status);
                                $('#ddlLeadSource').val(response.Source);
                                $('#ddlAssign').select2('val', response.AssignId);
                                $('#txtDetails').val(response.Details);
                            },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                                complete: loading('stop', null);
                            },
                        });

                    }
                });
            });

            $('#ddlCountry').change(function () {
                var CompanyId = $.cookie('bsl_1');
                var FinancialYear = $.cookie('bsl_4');
                var countryId = $('#ddlCountry').val();

                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Customers/getStates/' + countryId,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (data) {
                        console.log(data);
                        $('#ddlCountry').val();
                        $('#ddlState').children('option').remove();
                        $('#ddlState').append('<option value="0">--select--</option>');
                        $(data).each(function () {
                            $('#ddlState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                        });

                    }
                });
            });

            function refreshTable(leadId, status, empId, fromDate, toDate) {

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Leads/GetLeads?Status=' + status + '&EmployeeId=' + empId+'&From='+fromDate+'&To='+toDate,
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
                            html += '<td style="display:none">' + this.ID + '</td>';
                            html += '<td class="show-on-collapsed">' + this.Name + '</td>';
                            html += '<td class="show-on-collapsed">' + this.Email + '</td>';
                            html += '<td>' + this.Phone1 + '</td>';
                            switch (this.Status) {
                                case 1:
                                    html += '<td><label class="label label-default">NEW LEAD</label></td>';
                                    break;
                                case 2:
                                    html += '<td><label class="label label-warning">PENDING</label></td>';
                                    break;
                                case 3:
                                    html += '<td><label class="label label-primary">FOLLOW UP</label></td>';
                                    break;
                                case 4:
                                    html += '<td><label class="label label-info">PROCESS</label></td>';
                                    break;
                                case 5:
                                    html += '<td><label class="label label-danger">DECLINED</label></td>';
                                    break;
                                case 6:
                                    html += '<td><label class="label label-success">CONVERTED TO CUSTOMER</label></td>';
                                    break;
                            }
                            switch (this.Source) {
                                case 1:
                                    html += '<td><label class="label label-info">DADDY STREET</label></td>';
                                    break;
                                case 2:
                                    html += '<td><label class="label label-danger">FACEBOOK</label></td>';
                                    break;
                                case 3:
                                    html += '<td><label class="label label-default">TELE SALES</label></td>';
                                    break;
                                case 4:
                                    html += '<td><label class="label label-primary">TWITTER</label></td>';
                                    break;
                                case 5:
                                    html += '<td><label class="label label-success">MANAGEMENT</label></td>';
                                    break;
                            }
                            html += '<td>' + this.Assign + '</td>';
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

            //Show right sidebar when invoice is clicked
            $(document).on('click', '.left-side table tbody tr', function () {
                $('.left-sidebar').find('[class="col-md-12"]').removeClass('col-md-12').addClass('col-md-4 collapsed');
                $('.left-side table tbody').find('.active').removeClass('active');
                $('.search-group').hide();
                $('.right-sidebar').removeClass('hidden');
                $(".left-side").getNiceScroll().resize();
                $(this).addClass('active');

                var id = $(this).children('td').eq(0).text();
                $('#leadId').text(id);
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Leads/GetLeads/' + id,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_1')),
                    success: function (data) {

                        $('#lblLeadName').text(data.Name);
                        $('#lblLeadAddress1').text(data.Address1);
                        $('#lblLeadAddress2').text(data.Address2);
                        $('#lblLeadPhone1').text(data.Phone1);
                        $('#lblLeadPhone2').text(data.Phone2);
                        $('#lblGstNo').text(data.Taxno1);
                        $('#lblDetails').text(data.Details);
                        $('#hdLeadId').val(data.ID);
                        $('#lblLeadEmail').text(data.Email);
                        $('#imgPhoto').attr('src', data.ProfileImagePath != '' ? data.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                        $('#hdStatus').text(data.Status);
                        switch (data.Status) {
                            case 1:
                                $('#lblLeadStatus').html('<label class="label label-default">NEW LEAD</label>');
                                break;
                            case 2:
                                $('#lblLeadStatus').html('<label class="label label-warning">PENDING</label>');
                                break;
                            case 3:
                                $('#lblLeadStatus').html('<label class="label label-primary">FOLLOW UP</label>');
                                break;
                            case 4:
                                $('#lblLeadStatus').html('<label class="label label-info">PROCESS</label>');
                                break;
                            case 5:
                                $('#lblLeadStatus').html('<label class="label label-danger">DECLINED</label>');
                                break;
                            case 6:
                                $('#lblLeadStatus').html('<label class="label label-success">CONVERTED TO CUSTOMER</label>');
                                break;
                        }
                        switch (data.Source) {
                            case 1:
                                $('#lblLeadSource').html('<label class="label label-info">DADDY STREET</label>');
                                break;
                            case 2:
                                $('#lblLeadSource').html('<label class="label label-danger">FACEBOOK</label>');
                                break;
                            case 3:
                                $('#lblLeadSource').html('<label class="label label-default">TELE SALES</label>');
                                break;
                            case 4:
                                $('#lblLeadSource').html('<label class="label label-primary">TWITTER</label>');
                                break;
                            case 5:
                                $('#lblLeadSource').html('<label class="label label-success">MANAGEMENT</label>');
                                break;
                        }
                        $('#lblSinNo').text(data.Taxno2);
                        $('#ddlStatus').select2('val', data.Status);
                        if (data.Status == 6) {
                            $('#btnMore').hide();
                        }
                        else {
                            $('#btnMore').show();
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },

                });


                $('.right-sidebar').fadeOut();
                $('.right-sidebar').fadeIn();
            });

            //convert to customer
            $('#btnChangeCust').click(function () {
                var Customer = {};
                Customer.Name = $('#lblLeadName').text();
                Customer.Address1 = $('#lblLeadAddress1').text();
                Customer.Address2 = $('#lblLeadAddress2').text();
                Customer.Phone1 = $('#lblLeadPhone1').text();
                Customer.Phone2 = $('#lblLeadPhone2').text();
                Customer.Email = $('#lblLeadEmail').text();
                Customer.Taxno1 = $('#lblGstNo ').text();
                Customer.Taxno2 = $('#lblSinNo').text();
                Customer.ID = $('#leadId').val();
                Customer.ModifiedBy = $.cookie('bsl_3');
                Customer.CompanyId = $.cookie("bsl_1");
                Customer.CreatedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Leads/ConvertToCustomer?LeadId=' + $('#hdLeadId').val(),
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Customer),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        // console.log(data.Object.Id)
                        if (data.Success) {
                            successAlert(data.Message);
                            window.open('../Masters/Inventories?ID=' + data.Object.Id + '&Mode=EDIT&Section=CUSTOMER', '_self');
                            reset();
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });

            })

            //Change status
            $('.status-update').on('click', function () {

                var id = $('#leadId').text();
                var status = $('#ddlStatus').val();
                var data = {};
                data.ID = id;
                data.Status = status;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Leads/UpdateStatus',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            successAlert(response.Message);
                            refreshTable(null, null, null,null,null);
                            $('.despatch-wrap').fadeOut('slow');

                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); }
                });

            });

            //delete lead
            $('#btnDeleteLead').click(function () {

                if ($('#leadId').text() != 0) {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    }, function (isConfirm) {

                        var id = $('#leadId').text();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/Leads/deleteleads/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        $('.md-cancel').trigger('click');
                                        refreshTable(null, null, null,null,null);
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

            $(".right-side, .left-side").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });
            $('.listing-search-entity').on('change keyup', function () {
                searchOnTable($('.listing-search-entity'), $('#invoiceList'), 1);
            });
        });

    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
   <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.debug.js"></script>--%>
    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>
     <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
</asp:Content>
