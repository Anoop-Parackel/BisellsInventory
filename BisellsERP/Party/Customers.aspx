<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="BisellsERP.Party.Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customers</title>
    <style>
        #wrapper {
            overflow: hidden;
        }

        .left-side {
            height: calc(100vh - 198px);
            overflow-y: auto;
            overflow-x: hidden;
            margin-top: 13px;
            box-shadow: 0 2px 1px 0 #dcdcdc;
        }

            .left-side table tbody td {
                padding: 15px 11px;
                cursor: pointer;
            }

            .left-side table thead td {
                padding: 4px 10px;
            }


            .left-side table tbody tr.active td, .left-side table tbody tr.active th {
                background-color: rgba(30, 136, 229, 0.1);
            }

            .left-side table tbody tr.active, .left-side table tbody tr.active {
                border-left: 5px solid #1E88E5;
            }

        .right-side {
            /*height: calc(100vh - 216px);*/
            height: calc(100vh - 198px);
        }

        .print-wrapper {
            padding: 0 4em;
            border: 1px dashed #ccc;
        }

        .btn-group.open .dropdown-toggle {
            box-shadow: none;
        }

        input[name="daterange"] {
            border: none;
            color: #607d8b;
            font-size: 14px;
            /*background-color: #CFD8DC;*/
            background-color: white;
            padding: 6px 6px;
            border-radius: 3px;
        }

        .select2-choice {
            background-color: #CFD8DC;
            border-color: #CFD8DC;
        }

        .searchDropdown.cust .select2-container .select2-choice {
            line-height: 16px;
            font-size: 12px;
            background-color: #cfd8dc !important;
            margin-top: 2px;
        }

        .filter-span:nth-of-type(1) {
            margin-right: 15px;
        }

        .cust-input {
            border-radius: 4px;
            border: 1px solid #ccc;
            padding: 3px 6px;
        }

        .searchDropdown {
            width: 160px !important;
        }

        .select2-container .select2-choice {
            background-color: white;
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
            line-height: 41px;
        }

        .nav-inner.nav-tabs > li.active {
            border-bottom: 2px solid #1e88e5;
        }

        .overview-bg {
            background-color: whitesmoke;
        }

        /*.right-tab-activity {
            padding-left: 20px;
        }*/

        /*timeline chart*/
        .activities {
            margin: 10px 0 50px 85px;
        }

        .tree-structure {
            border-left: 1px solid #277ad8;
            margin-left: 133px !important;
        }

            .tree-structure .tree-node:first-child {
                padding-top: 0;
            }


            .tree-structure .tree-node {
                padding: 10px 0;
                position: relative;
                margin-left: 15px;
            }

                .tree-structure .tree-node .tree-node-leaf {
                    padding: 10px 20px;
                    position: relative;
                    margin-bottom: 10px;
                    margin-left: 25px;
                    border: 1px solid #efefef;
                    background-color: #fff;
                    border-radius: 6px;
                }

        .activities .time {
            left: -120px;
        }

        .activities .time, .comments .time {
            position: absolute;
            top: 25px;
            width: 90px;
            text-align: right;
        }

        .tree-structure {
            border-left: 1px solid rgba(60, 186, 159, 0.23);
            margin-left: 25px;
        }

        .activities .tree-node:before {
            left: -20px;
        }

        .tree-structure .tree-node:before {
            content: ' ';
            position: absolute;
            display: block;
            width: 10px;
            height: 10px;
            border-radius: 50%;
            top: 30px;
            left: -20px;
            background-color: #3cba9f;
        }

        .tree-structure .tree-node .tree-node-leaf .arrow:after {
            position: absolute;
            display: block;
            width: 0;
            height: 0;
            border-color: transparent #f3f3f3 transparent transparent;
            border-style: solid;
            border-width: 10px 10px 10px 0;
            content: " ";
            left: -11px;
            bottom: 31px;
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

        .customer-overview {
            background-color: #FAFAFA;
            padding: 10px 15px 5px;
            margin-top: -10px;
            margin-left: -20px;
            margin-bottom: -10px;
            border-right: 1px solid #eee;
            position: relative;
        }


            .customer-overview .title-wrap img {
                border: 1px solid #ddd;
            }

            .customer-overview .title-wrap {
                padding: 0 0 10px;
                margin: 0 2px 4px;
                border-bottom: 1px dashed #ccc;
            }

        .address-wrap {
            min-height: calc(100vh - 241px - 89px);
            max-height: calc(100vh - 241px - 89px);
        }

            .address-wrap .title-div {
                padding: 3px 5px;
                background-color: #F5F5F5;
            }

        .customer-lists li {
            padding: 5px 10px;
            background-color: #fff;
            border: 1px solid #efefef;
            border-radius: 3px;
            margin-top: 3px;
        }

            .customer-lists li .address-edit {
                opacity: 0;
                transition: opacity .3s ease-in;
            }

            .customer-lists li:hover .address-edit {
                opacity: 1;
                color: #3cba9f;
            }

            .customer-lists li:hover .address-edit:hover {
                opacity: 1;
                color: black;
            }

            .customer-lists li .set-primary {
                opacity: 0;
                transition: opacity .3s ease-in;
            }

            .customer-lists li:hover .set-primary {
                opacity: 1;
                color: #3cba9f;
            }

            .customer-lists li:hover .set-primary:hover {
                opacity: 1;
                color: black;
            }

            .customer-lists li .delete-address {
                opacity: 0;
                transition: opacity .3s ease-in;
            }

            .customer-lists li:hover .delete-address {
                opacity: 1;
                color: red;
            }

            .customer-lists li:hover .delete-address:hover {
                opacity: 1;
                color: black;
            }

            .customer-lists li > p {
                margin: 0 !important;
                font-size: 12px;
            }

            .customer-lists li > label {
                margin: 0;
            }

            .customer-lists li > p:first-child {
                font-weight: 700;
            }


        .customer-widget .widget {
            margin: 0;
            display: block;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            box-shadow: 0 2px 1px 0 #eee;
        }

            .customer-widget .widget .widget-heading {
                padding: 5px 4px;
                -webkit-border-radius: 2px 2px 0 0;
                -moz-border-radius: 2px 2px 0 0;
                border-radius: 2px 2px 0 0;
                text-align: center;
                background: #EEE;
                color: #607D8B;
                font-size: 12px;
            }

            .customer-widget .widget .widget-body {
                padding: 10px 15px;
                font-size: 14px;
                font-weight: 300;
                background: #FAFAFA;
                border: 1px solid #eee;
            }


        .timeline-wrap {
            max-height: calc(100vh - 327px);
            margin-top: 5px;
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

        #invoiceList thead > tr > th {
            padding: 10px !important;
            border-bottom: 3px solid #ddd !important;
            color: #6f726f;
        }

        #addlist {
            height: calc(100vh - 271px);
        }

        #transactions > div {
            height: calc(100vh - 263px);
        }

        /* Styling Address Popover Only */
        .popover {
            min-width: 400px;
        }

            .popover .form-group {
                margin-bottom: 5px;
            }

            .popover .control-label {
                margin-bottom: 0;
                font-weight: 100;
                font-size: 12px;
                color: #5b7886;
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
            <h3 class="page-title m-t-0">Customers</h3>
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
                    <div class="list-group-item active m-b-5">
                        <div class="btn-group">
                            <button type="button" class="trans-btn dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                All Customers&nbsp;<b id="countOfInvoices">(0)</b>&nbsp;<span class="caret"></span>
                            </button>
                            <%--   <ul class="dropdown-menu">
                                <li><a href="#">Draft</a></li>
                                <li><a href="#">Client Viewed</a></li>
                                <li><a href="#">Partially Paid</a></li>
                            </ul>--%>
                        </div>
                        <input type="text" class="form-control listing-search-entity" placeholder="Search Customer..."/>
                        <%--<a id="searchInvoice" href="#" class="pull-right"><i class="fa fa-search filter-list"></i></a>--%>
                        <div class="pull-right t-y search-group">
                        </div>
                    </div>
                    <div class="left-side">
                        <div class="panel m-b-0">
                            <div class="panel-body p-0" style="min-height: calc(100vh - 225px)">
                                <table id="invoiceList" class="table left-table invoice-list">
                                    <thead>
                                        <tr>
                                            <th style="display: none">Id</th>
                                            <th class="show-on-collapsed">Customer</th>
                                            <%--<th>Company</th>--%>
                                            <th class="show-on-collapsed">Email</th>
                                            <th>Phone</th>
                                            <th class="show-on-collapsed">Receivable</th>
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
                                        <a href="../Masters/Inventories?Mode=NEW&Section=CUSTOMER" class="btn btn-default btn-xs btn-rounded waves-effect">Create New Customer</a>
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
                    <div class="col-sm-8">
                        <div class="btn-toolbar m-b-10" role="toolbar">
                            <div class="btn-group">
                                <button type="button" id="btnEditCustomer" class="btn btn-default waves-effect waves-light" title="edit"><i class="md md-mode-edit"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light" id="btnAddCustomer" title="New"><i class="fa fa-plus"></i></button>
                                <button type="button" id="btnDeleteCustomer" class="btn btn-default waves-effect waves-light" title="delete"><i class="fa fa-trash-o"></i></button>
                            </div>
                            <div class="btn-group m-l-20">
                                <button type="button" id="btnAddJob" class="btn btn-default waves-effect waves-light"><i class="md md-add-circle-outline"></i>&nbsp;Add Job</button>
                            </div>

                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="btn-toolbar m-t-0" role="toolbar">
                            <div class="btn-group pull-right">
                                <a href="#" class="close-sidebar"><i class="md md-cancel"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End row -->

                <div class="panel panel-default m-t-10 p-b-0 right-side">
                    <div class="panel-body p-0">
                        <ul class="nav nav-inner nav-tabs">
                            <li class="active">
                                <a href="#overview" data-toggle="tab" aria-expanded="false">
                                    <span>Overview</span>
                                </a>
                            </li>
                            <%--<li class="">
                                <a href="#transactions" id="hrefTransaction" data-toggle="tab" aria-expanded="true">
                                    <span>Transactions</span>
                                </a>
                            </li>--%>
                            <li class="">
                                <a href="#jobs" id="hrefJobs" data-toggle="tab" aria-expanded="true">
                                    <span>Jobs</span>
                                </a>
                            </li>
                            <li class="">
                                <a href="#Invoices" id="hrefInvoices" data-toggle="tab" aria-expanded="true">
                                    <span>Invoices</span>
                                </a>
                            </li>
                            <li class="">
                                <a href="#Payments" id="hrefPayments" data-toggle="tab" aria-expanded="true">
                                    <span>Payments</span>
                                </a>
                            </li>
                        </ul>

                        <div class="tab-content row customer-list">
                            <%-- tab for overview --%>
                            <div class="tab-pane active" id="overview">
                                <div class="">
                                    <div class="col-sm-4">
                                        <div class="customer-overview row">
                                            <div class="title-wrap row">
                                                <div class="col-sm-3">
                                                    <img id="imgPhoto" class="img-circle" src="../Theme/images/profile-pic.jpg" width="60" />
                                                </div>
                                                <div class="col-sm-9">
                                                    <h4 class="text-right m-0" id="lblCustomerName">Tony Stark</h4>
                                                    <p class="text-right"><small class="text-muted" id="lblCustomerEmail">arun@gmail.com</small></p>
                                                    <p class="text-right m-0"><a href="#" class="edit-cust"><small>Edit</small></a>&nbsp;|&nbsp;<a href="#" class="text-danger delete-cust"><small><i class="ion-trash-a"></i></small></a></p>
                                                    <label style="display: none" id="cusId">0</label>
                                                </div>
                                            </div>
                                            <div class="address-wrap">
                                                <div class="title-div">
                                                    <span><i class="ion-map"></i>&nbsp;Contact Address</span>
                                                    <a href="#" class="pull-right text-green"><small class="add-address">Add New</small></a>
                                                </div>
                                                <div class="address-tab"></div>
                                                <div class="hide editAddressPop">
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <div class="form-group">
                                                                <label class="control-label">Salutation</label>
                                                                <select id="ddlSalutation" class="form-control input-sm">
                                                                    <option value="Mr">Mr.</option>
                                                                    <option value="Mrs">Mrs.</option>
                                                                    <option value="Ms">Ms.</option>
                                                                    <option value="Miss">Miss.</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-9">
                                                            <div class="form-group">
                                                                <label class="control-label">Contact Person</label>
                                                                <input type="text" id="txtCustName" class="form-control input-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Address Line 1</label>
                                                                <textarea rows="2" id="txtCustAddress1" class="form-control input-sm"></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Address Line 2</label>
                                                                <textarea rows="2" id="txtCustAddress2" class="form-control input-sm"></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">City</label>
                                                                <input type="text" id="txtCustCity" class="form-control input-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Zip</label>
                                                                <input type="number" class="form-control input-sm" id="txtZipNumber" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Country</label>
                                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">State</label>
                                                                <select id="ddlState" class="form-control input-sm">
                                                                    <option value="0">--Select--</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Phone 1</label>
                                                                <input type="number" class="form-control input-sm" id="txtPhone1" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Phone 2</label>
                                                                <input type="number" class="form-control input-sm" id="txtPhone2" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-12">
                                                            <div class="form-group">
                                                                <label class="control-label">Email</label>
                                                                <input type="text" id="txtEmail" class="form-control input-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-12">
                                                            <div class="btn-toolbar pull-right m-t-15">
                                                                <button type="button" class="btn btn-default btn-sm update-address-pop">Save</button>
                                                                <button type="button" class="btn btn-inverse btn-sm close-popover">x</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <input type="hidden" id="hdnCustomerAddressID" value="0" />
                                                    <input type="hidden" id="hdnPrimaryAddress" value="0" />
                                                </div>

                                                <div class="title-div">
                                                    <span><i class="ion-briefcase"></i>&nbsp;Statutory Details</span>
                                                </div>

                                                <ul class="customer-lists list-unstyled">
                                                    <li>
                                                        <p></p>
                                                        <p>
                                                            TRN/GST No :
                                                            <label id="lblGstNo">12345678951256123</label>
                                                        </p>
                                                        <p>
                                                            CIN/Reg No :
                                                            <label id="lblSinNo">123123123</label>
                                                        </p>
                                                    </li>
                                                </ul>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 right-tab-activity">

                                        <div class="customer-widget">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="widget">
                                                        <div class="widget-heading text-center">
                                                            Total Orders
                                                        </div>
                                                        <div class="widget-body clearfix">
                                                            <div class="pull-left">
                                                                <i class="ion-star"></i>
                                                            </div>
                                                            <div class="pull-right number"><span class="total-orders">$ 5000</span></div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="widget">
                                                        <div class="widget-heading text-center">
                                                            Total Invoices
                                                        </div>
                                                        <div class="widget-body clearfix">
                                                            <div class="pull-left">
                                                                <i class="ion-flag"></i>
                                                            </div>
                                                            <div class="pull-right number"><span class="no-of-invoices">$ 35000</span></div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="widget">
                                                        <div class="widget-heading text-center">
                                                            Payment Due
                                                        </div>
                                                        <div class="widget-body clearfix">
                                                            <div class="pull-left">
                                                                <i class="ion-heart"></i>
                                                            </div>
                                                            <div class="pull-right number"><span class="payment-due">$ 4582</span></div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="timeline-wrap">
                                            <div class="activities tree-structure">
                                                <div class="tree-node right">
                                                    <div class="time">
                                                        <small>xxxxx</small>
                                                    </div>
                                                    <div class="tree-node-leaf">
                                                        <div class="arrow"></div>
                                                        <p>
                                                            xxxxxxx
                                                        </p>
                                                        <div class="text-muted">
                                                            xxxxxxx
                                                                 <strong>xxxx</strong>
                                                            - <a data-ember-action="" data-ember-action-3261="3261"><small>View Details</small></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tree-node right">
                                                    <div class="time">
                                                        <small>xxxxxxx</small>
                                                    </div>
                                                    <div class="tree-node-leaf">
                                                        <div class="arrow"></div>
                                                        <p>
                                                            xxxxxxxx
                                                        </p>
                                                        <div class="text-muted">
                                                            xxxxxx
                                                       <strong>xxxx</strong>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-5 overview-bg hidden">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <i class="md-2x md-person fa-color pull-left"></i>
                                            </div>
                                            <div class="col-md-6">
                                                <%--<h3 class="pull-left" id="lblCustomerName">Arun Kumar Anil</h3>--%>
                                                <%--<label style="display: none" id="cusId">0</label>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 m-t-30 ">
                                        <h4 class="h4-color">Address &nbsp;<i class="fa fa-newspaper-o fa-color"></i></h4>
                                    </div>
                                    <div class="col-md-12 bd-bottom">
                                        <br />
                                        <%--<label id="lblCusAddress1">Address1   </label>--%>
                                    </div>
                                    <div class="col-md-12">
                                        <%--<label id="lblCusAddress2">Address2</label>--%>
                                    </div>
                                    <br />
                                    <div class="col-md-12 m-t-30 ">
                                        <h4 class="h4-color">Contact Details&nbsp;<i class="fa fa-phone fa-color"></i></h4>
                                    </div>
                                    <div class="col-md-12 bd-bottom">
                                        <br />
                                        <%--<label id="lblCusPhone1">Phone 1   </label>--%>
                                    </div>
                                    <div class="col-md-12">
                                        <%--<label id="lblCusPhone2">Phone 2</label>--%>
                                    </div>
                                    <br />
                                    <div class="col-md-12 m-t-30 ">
                                        <h4 class="h4-color">Other Details &nbsp;<i class="fa fa-cog fa-color"></i></h4>
                                    </div>
                                    <div class="col-md-12 bd-bottom">
                                        <br />
                                        <div class="col-sm-5">
                                            <label>TRN/GST No : </label>
                                        </div>
                                        <div class="col-sm-7">
                                            <%--<span class="text-success text-left" id="lblGstNo">12345678951256123</span>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                        <div class="col-sm-5">
                                            <label>Email-Id :  </label>
                                        </div>
                                        <div class="col-sm-7 left">
                                            <%--<span class="text-primary" id="lblCustomerEmail">arun@gmail.com</span>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <br />
                                        <div class="col-sm-5">
                                            <label>CIN/Reg No :  </label>
                                        </div>
                                        <div class="col-sm-7 left">
                                            <%--<span class="text-primary" id="lblSinNo">123123123</span>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7 right-tab-activity  hidden">
                                </div>
                            </div>



                            <%-- tab for jobs --%>
                            <div class="tab-pane" id="jobs">
                                <div class="col-md-12">
                                    <div class="row panel-list">
                                        <div class="col-sm-12">
                                            <div id="noJobSection" class="empty-state-wrap" style="">
                                                    <img class="empty-state-icon" src="../Theme/images/empty_state.png" />
                                                    <h4 class="empty-state-title">Nothing to show</h4>
                                                    <p class="empty-state-text">Oooh oh, there are no jobs created for this customer</p>
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
                            <%-- tab for invoices --%>
                            <div class="tab-pane" id="Invoices">
                                <div class="col-md-12">
                                    <div class="row panel-list list-scroll">
                                        <div class="col-md-12">
                                            <table id="tblInvoices" class="table  table-striped table-hover">
                                                <thead>
                                                    <tr>
                                                        <th style="display: none">ID</th>
                                                        <th>Invoice Number</th>
                                                        <th>Date</th>
                                                        <th>Tax</th>
                                                        <th>Gross</th>
                                                        <th>Net</th>
                                                        <th>#</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <%-- tab for payments --%>
                            <div class="tab-pane" id="Payments">
                                <div class="col-md-12">
                                    <div class="row panel-list list-scroll">
                                        <div class="col-sm-12">
                                            <table id="tblPayments" class="table  table-striped table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Date</th>
                                                        <th>Particulars</th>
                                                        <th>Amount</th>
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
                </div>
                <!-- panel body -->
            </div>
            <!-- panel -->
        </div>
        <!-- End Right sidebar -->
    </div>

    <%--Add job modal--%>
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
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Estimated End Date</label>
                                <input type="text" id="txtEndDate" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Start Date</label>
                                <input type="text" id="txtStartDate" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Estimate Amount</label>
                                <input type="text" id="txtEstAmt" class="form-control" />
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
                                        <select id="ddlJobSalutation" class="form-control input-sm">
                                            <option value="Mr">Mr.</option>
                                            <option value="Mrs">Mrs.</option>
                                            <option value="Ms">Ms.</option>
                                            <option value="Miss">Miss.</option>
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
                                        <asp:DropDownList ID="ddlJobCountry" CssClass="form-control input-sm" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">State</label>
                                        <select class="form-control input-sm" id="ddlJobState">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 1</label>
                                        <input type="number" id="txtJobPhone1" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 2</label>
                                        <input type="number" id="txtJobPhone2" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input type="text" id="txtJobEmail" class="form-control input-sm" />
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
                                            <option value="Mr">Mr.</option>
                                            <option value="Mrs">Mrs.</option>
                                            <option value="Ms">Ms.</option>
                                            <option value="Miss">Miss.</option>
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

    <%--find list modal ends here--%>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>

    <script>



        $(document).ready(function () {
            $(document).on('click', '.address-edit', function () {
                //if ($(this).closest('li').children('.id').text() != "0") {

                //}
                //else {
                //    //alert($('#cusId').text());
                //}
                var isPrimary = $(this).parent('p').children('.primary').text();
                var id = $('#cusId').text()
                var addressid = $(this).closest('li').children('.id').text();
                var Salutation = $(this).closest('li').find('.salutation').text();
                var Name = $(this).closest('li').find('.cust-name').text();
                var address1 = $(this).closest('li').find('.address1').text();
                var address2 = $(this).closest('li').find('.address2').text();
                var City = $(this).closest('li').find('.city').text();
                var ZipCode = $(this).closest('li').find('.zipcode').text();
                var Phone1 = $(this).closest('li').find('.phone1').text();
                var Phone2 = $(this).closest('li').find('.phone2').text();
                var countryID = $(this).closest('li').find('.country-id').text();
                var Email = $(this).closest('li').find('.email').text();
                if (countryID == undefined || countryID == '') {
                    countryID = 0;
                }
                var StateID = $(this).closest('li').find('.state-id').text();
                if (StateID == undefined || StateID == '') {
                    StateID = '0';
                }
                $('#ddlSalutation').val(Salutation);
                $('#txtCustName').val(Name);
                $('#txtCustAddress1').val(address1);
                $('#txtCustAddress2').val(address2);
                $('#txtCustCity').val(City);
                $('#txtZipNumber').val(ZipCode);
                $('#ddlCountry').val(countryID);
                loadStates(StateID);
                $('#txtPhone1').val(Phone1);
                $('#txtPhone2').val(Phone2);
                $('#hdnCustomerAddressID').val(addressid);
                $('#hdnPrimaryAddress').val(isPrimary);
                $('#txtEmail').val(Email);
                $('.address-edit').popover({
                    placement: 'right',
                    html: true,
                    content: $('.editAddressPop').html()
                }).on('click', function () {
                    // Apply Filter Click
                    $('.update-address-pop').click(function () {
                        //Update Address Logic Here
                        var Data = {};
                        Data.ID = $('#cusId').text();
                        Data.Salutation = $('#ddlSalutation').val();
                        Data.Name = $('#txtCustName').val();
                        Data.Address1 = $('#txtCustAddress1').val();
                        Data.Address2 = $('#txtCustAddress2').val();
                        Data.City = $('#txtCustCity').val();
                        Data.StateId = $('#ddlState').val();
                        Data.CountryId = $('#ddlCountry').val();
                        Data.ZipCode = $('#txtZipNumber').val();
                        Data.Phone1 = $('#txtPhone1').val();
                        Data.Phone2 = $('#txtPhone2').val();
                        Data.ModifiedBy = $.cookie('bsl_3');
                        Data.CustomerAddressID = $('#hdnCustomerAddressID').val();
                        Data.Email = $('#txtEmail').val();
                        //If it is primary address then updates the address in the master table.Else updates only the address table
                        if ($('#hdnPrimaryAddress').val() == 1) {
                            Data.IsPrimary = true;
                        }
                        else {
                            Data.IsPrimary = false;
                        }
                        console.log(Data);
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/Customers/SaveAddress',
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify(Data),
                            contentType: 'application/json;charset=utf-8',
                            success: function (data) {
                                if (data.Success) {
                                    LoadAddress($('#cusId').text());
                                    successAlert(data.Message);
                                    $('.popover').popover('hide');
                                }
                                else {
                                    errorAlert(data.Message);
                                }
                            },
                            error: function (xhr) {
                                errorAlert(data.Message);
                            }
                        });
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                    })
                    // Cancel Filter Click
                    $('.close-popover').click(function () {
                        $('.popover').popover('hide');
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                    })
                })
            });



            //Added for Adding new Customer address
            $(function () {
                $('.add-address').popover({
                    placement: 'right',
                    html: true,
                    content: $('.editAddressPop').html()
                }).on('click', function () {
                    // Apply Filter Click
                    $('.update-address-pop').click(function () {
                        //Update Address Logic Here
                        var Data = {};
                        Data.ID = $('#cusId').text();
                        Data.Salutation = $('#ddlSalutation').val();
                        Data.Name = $('#txtCustName').val();
                        Data.Address1 = $('#txtCustAddress1').val();
                        Data.Address2 = $('#txtCustAddress2').val();
                        Data.City = $('#txtCustCity').val();
                        Data.StateId = $('#ddlState').val();
                        Data.CountryId = $('#ddlCountry').val();
                        Data.ZipCode = $('#txtZipNumber').val();
                        Data.Phone1 = $('#txtPhone1').val();
                        Data.Phone2 = $('#txtPhone2').val();
                        Data.CreatedBy = $.cookie('bsl_3');
                        Data.Email = $('#txtEmail').val();
                        console.log(Data);
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/Customers/SaveAddress',
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify(Data),
                            contentType: 'application/json;charset=utf-8',
                            success: function (data) {
                                if (data.Success) {
                                    LoadAddress($('#cusId').text());
                                    $('.popover').popover('hide');
                                }
                                else {
                                    errorAlert(data.Message);
                                }
                            },
                            error: function (xhr) {
                                errorAlert(data.Message);
                            }
                        });

                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                    })
                    // Cancel Filter Click
                    $('.close-popover').click(function () {
                        $('.popover').popover('hide');
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                    })
                })
            });

            //Event to set an address a primary.It will update the current address isprimary to true and updates the data in the primary table
            $(document).on('click', '.set-primary', function () {
                var id = $('#cusId').text()
                var addressid = $(this).closest('li').children('.id').text();
                var data = {};
                data.ID = id;
                data.CustomerAddressID = addressid;
                data.ModifiedBy = $.cookie('bsl_3');
                console.log(data);
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Customers/SetPrimaryAddress',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    contentType: 'application/json;charset=utf-8',
                    success: function (Data) {
                        if (Data.Success) {
                            LoadAddress($('#cusId').text());
                        }
                        else {
                            errorAlert(Data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(Data.Message);
                    }
                });
            });


            //Event that deletes the selected address
            $(document).on('click', '.delete-address', function () {
                var id = $(this).closest('li').children('.id').text();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Customers/DeleteAddress?id=' + id,
                    method: 'Delete',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_3')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (Data) {
                        if (Data.Success) {
                            LoadAddress($('#cusId').text());
                        }
                        else {
                            errorAlert(Data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(Data.Message);
                    }
                });
            })

            //$(".addon > ul").niceScroll({
            //    cursorcolor: "#90A4AE",
            //    cursorwidth: "8px"
            //});

            //$(".left-side").niceScroll({
            //    cursorcolor: "#6d7993",
            //    cursorwidth: "8px",
            //    horizrailenabled: false
            //});
            //$(".right-side, .left-side").niceScroll({
            //    cursorcolor: "#90A4AE",
            //    cursorwidth: "8px",
            //    horizrailenabled: false
            //});

            //$(".timeline-wrap").niceScroll({
            //    cursorcolor: "#6d7993",
            //    cursorwidth: "8px"
            //});

            $(".address-wrap").niceScroll({
                cursorcolor: "#6d7993",
                cursorwidth: "5px"
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

            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);
            $(document).on('click', '.filter-list', function () {
                var customerId = $('#ddlCustomer').val() == '0' ? null : $('#ddlCustomer').val();
                refreshTable(customerId);
            })
            //get list of invoices
            refreshTable(null);

            function refreshTable(customerId) {

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/customers/GetCustomerList/?Customer_id=' + customerId,
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
                        var html = '';
                        var count = 0;
                        var currencySymbol = JSON.parse($('#hdSettings').val()).CurrencySymbol;
                        $(data).each(function (index) {
                            count++;
                            html += '<tr>';
                            html += '<td style="display:none">' + this.ID + '</td>';
                            html += '<td class="show-on-collapsed">' + this.Name + '</td>';
                            //     html += '<td>' + this.Company + '</td>';
                            html += '<td class="show-on-collapsed">' + this.Email + '</td>';
                            html += '<td>' + this.Phone1 + '</td>';
                            html += '<td class="show-on-collapsed">' + currencySymbol + '&nbsp;' + this.TotalReceivable + '</td>';
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
                    complete: function () {
                        loading('stop', null);
                        $(".left-side").niceScroll({
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
                var amount = $(this).children('td').eq(4).text();
                $('#cusId').text(id);
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/customers/get/' + id,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_1')),
                    success: function (data) {
                        LoadAddress(id);
                        $('#lblCustomerName').text(data.Name);
                        $('#lblCusAddress1').text(data.Address1);
                        $('#lblCusAddress2').text(data.Address2);
                        $('#lblCusPhone1').text(data.Phone1);
                        $('#lblCusPhone2').text(data.Phone2);
                        $('#lblGstNo').text(data.Taxno1);
                        $('#lblCustomerEmail').text(data.Email);
                        $('#imgPhoto').attr('src', data.ProfileImagePath != '' ? data.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                        $('#lblSinNo').text(data.Taxno2);
                        //loading time line of customer
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/customers/GetCustomerActivity/?id=' + id,
                            method: 'POST',
                            contentType: 'application/json;charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify($.cookie('bsl_1')),
                            success: function (response) {
                                var html = '';
                                $(response).each(function (index) {
                                    html += '<div class="tree-node right"><div class="time"><small>' + this.Date + '&nbsp;' + this.Time + '</small></div><div class="tree-node-leaf"><div class="arrow"></div><div class="text-muted">' + this.Activity + '<small><br/><a href="' + this.Url + '">#' + this.Reference + '</a></small>&nbsp;|&nbsp;<small>' + this.EntryDate + '</small></div></div></div>';

                                });
                                $('.activities').children().remove();
                                $('.activities').append(html);
                                //LoadTransactions();
                                LoadJobs();
                                LoadPayments();
                                if (response.length != 0) {
                                    console.log(response);
                                    $('.no-of-invoices').text(response[0].NoOfBills);
                                    $('.total-orders').text(response[0].NoOfOrders);
                                    $('.payment-due').text(amount);
                                }
                                else {
                                    $('.no-of-invoices').text("0");
                                    $('.total-orders').text("0");
                                    $('.payment-due').text(amount);
                                }

                            },
                            error: function (err) {
                                alert(err.responseText);
                                loading('stop', null);
                            },
                            beforeSend: function () { miniLoading('start', null) },
                            complete: function () {
                                miniLoading('stop', null);
                                $(".timeline-wrap").niceScroll({
                                    cursorcolor: "#6d7993",
                                    cursorwidth: "8px"
                                });
                                $(".timeline-wrap").getNiceScroll().resize();
                            },
                        });
                        //loading time line of customer ends here
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

            //Loading the table for transactions of customer
            function LoadTransactions() {
                var cusId = $('#cusId').text();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/FinancialTransactions/Getdetails/?Customer_id=' + cusId,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    success: function (response) {
                        //console.log(response);
                        var html = '';
                        $('#tblTransactions').DataTable().destroy();
                        $('#tblTransactions').children('tbody').children('tr').remove();

                        $(response).each(function (index) {
                            html += '<tr>';
                            html += '<td>' + this.TransactionDateString + '</td>';
                            html += '<td>' + this.Description + '</td>';
                            html += '<td>' + this.DebitAmount + '</td>';
                            html += '<td>' + this.CreditAmount + '</td>';
                            html += '</tr>';

                        });
                        $('#tblTransactions').children('tbody').append(html);

                        $('#tblTransactions').DataTable({ destroy: true, dom: 'Blfrtip', buttons: ['copy', 'excel', 'csv', 'print'] });

                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () {

                        $("#transactions > div").niceScroll({
                            cursorcolor: "#6d7993",
                            cursorwidth: "8px",
                            horizrailenabled: false
                        });
                        miniLoading('stop', null);
                    },
                });
            }

            function LoadPayments() {
                var cusId = $('#cusId').text();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/FinancialTransactions/GetDetailsForCustomerPayment/?Customer_id=' + cusId,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    success: function (response) {
                        //console.log(response);
                        var html = '';
                        $('#tblPayments').DataTable().destroy();
                        $('#tblPayments').children('tbody').children('tr').remove();

                        $(response).each(function (index) {
                            html += '<tr>';
                            html += '<td>' + this.TransactionDateString + '</td>';
                            html += '<td>' + this.Description + '</td>';
                            html += '<td>' + this.DebitAmount + '</td>';
                            //html += '<td>' + this.CreditAmount + '</td>';
                            html += '</tr>';

                        });
                        $('#tblPayments').children('tbody').append(html);

                        $('#tblPayments').DataTable({ destroy: true, dom: 'Blfrtip', buttons: ['copy', 'excel', 'csv', 'print'] });

                    },
                    error: function (err) {
                        alert(err.responseText);
                        loading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () {

                        $("#Payments > div").niceScroll({
                            cursorcolor: "#6d7993",
                            cursorwidth: "8px",
                            horizrailenabled: false
                        });
                        miniLoading('stop', null);
                    },
                });
            }

            //Click in transaction tab
            $('#hrefTransaction').click(function () {
                // LoadTransactions();
            })
            //end of click in transaction tab

            $('#hrefPayments').click(function () {
                LoadPayments();
            })

            //click in invoice tab
            $('#hrefInvoices').click(function () {
                LoadInvoices();
            })

            //popover
            $('#addlist').on('click', '.select-job', function () {
                $('.select-job').popover({
                    placement: 'down',
                    html: true,
                    content: $('#jobWrap').html()
                }).on('click', function () {

                });
            })

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

            $('#ddlJobCountry').change(function () {
                LoadStates(0, $(this), $('#ddlJobState'));
            });

            $('#ddlSiteCountry').change(function () {
                LoadStates(0, $(this), $('#ddlSiteState'));
            });

            //save click of job
            $('#btnSaveJob').click(function () {
                var data = {};
                var CustomerId = $('#cusId').text();
                var jobName = $('#txtJob').val();
                var salutation = $('#ddlJobSalutation').val();
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
                var countryId = $('#ddlJobCountry').val();
                var stateId = $('#ddlJobState').val();
                var siteCountryId = $('#ddlSiteCountry').val();
                var siteStateId = $('#ddlSiteState').val();
                var ph1 = $('#txtJobPhone1').val();
                var ph2 = $('#txtJobPhone2').val();
                var Siteph1 = $('#txtSitePh1').val();
                var Siteph2 = $('#txtSitePh2').val();
                var email = $('#txtJobEmail').val();
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
                data.StartDate = startDate;
                data.StartDateString = startDate;
                data.EstimatedAmount = EstimatedAmount;

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/save',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                            $('#txtJob').val('');
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
                            $('#txtEstAmt').val('');
                            $('#txtStartDate').val('');
                            $('#jobModal').modal('hide');
                            $('#btnSaveJob').html('<i class="fa fa-plus"></i>&nbsp;Save ');
                            $('#hdId').val(0);
                            LoadJobs();
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start') },
                    complete: function () { miniLoading('stop') },
                });

            })
            //click for modal
            $('#btnAddJob').click(function () {
                $('#txtJob').val('');
                $('#txtEndDate').val('');
                $('#ddlJobSalutation').val('0');
                $('#txtContactPers').val('');
                $('#ddlSiteSalutation').val('0');
                $('#txtSiteContPer').val('');
                $('#txtContAddr1').val('');
                $('#txtContAddr2').val('');
                $('#txtSiteAddr1').val('');
                $('#txtSiteAddr2').val('');
                $('#txtCity').val('');
                $('#txtZip').val('');
                $('#txtSiteCity').val('');
                $('#txtSiteZip').val('');
                $('#ddlJobCountry').val('0');
                $('#ddlJobState').val('0');
                $('#ddlSiteCountry').val('0');
                $('#ddlSiteState').val('0');
                $('#txtJobPhone1').val('');
                $('#txtJobPhone2').val('');
                $('#txtSitePh1').val('');
                $('#txtSitePh2').val('');
                $('#txtJobEmail').val('');
                $('#txtSiteEmail').val('');
                $('#txtEstAmt').val('');
                $('#txtStartDate').val('');
                $('#jobModal').modal({ backdrop: 'static', keyboard: false, show: true });

            });
            $('#btnCreateJob').click(function () {
                $('#btnAddJob').trigger('click');
            });
            $('#hrefJobs').click(function () {
                LoadJobs();
            })

            //Load job of customer
            function LoadJobs() {
                var cusId = $('#cusId').text();
                var currencySymbol = JSON.parse($('#hdSettings').val()).CurrencySymbol;
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/Get/?customer_id=' + cusId,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    success: function (response) {
                        var html = '';
                        //console.log(response);
                        if (response != null && response.length != 0) {
                            $('#noJobSection').hide();
                            $(response).each(function () {
                                html += '<li class="card row" data-jobId="' + this.ID + '"><div class="col-sm-6"><h4 class="text-muted m-t-0"><a href="/Party/Job?UID=' + this.ID + '">' + this.JobName + '</a><small class="pull-right m-t-5"><strong>Start Date:</strong>' + this.StartDateString + '</small></h4><div class="row m-t-30"><div class="col-sm-6"><p><b>Contact Name</b></p><p class="m-t-0">' + this.ContactName + '</p></div><div class="col-sm-6 text-right"><p><b>Estimate</b></p><p class="m-t-0">' + currencySymbol + '&nbsp;' + this.EstimatedAmount + '</p></div></div><div class="row m-t-10"><div class="col-sm-6"><p><b>Contact Address</b></p><p class="m-t-0">' + this.ContactAddress + '</p></div><div class="col-sm-6 text-right"><p><b>Site Address</b></p><p class="m-t-0">' + this.SiteAddress + '</p></div></div></div><div class="col-sm-6 text-right"><div class="row "><div class="btn-toolbar"><button type="button" value="edit" class="btn btn-default btn-sm waves-effect waves-ripple edit-job"><i class="ion-edit text-info"></i>&nbsp;Edit</button><button type="button" value="edit" class="btn btn-default btn-sm waves-effect waves-ripple delete-job"><i class="fa fa-trash text-danger"></i>&nbsp;Delete</button><div class="dropdown pull-right"><button class="btn btn-default btn-sm waves-effect waves-ripple dropdown-toggle select-job m-l-5" type="button" data-toggle="dropdown">More<span class="caret"></span></button><ul class="dropdown-menu pull-left"><li><a href="../Purchase/Quote?JOB=' + this.ID + '">Purchase Order</a></li><li><a href="../Purchase/Entry?JOB=' + this.ID + '">New Bill</a></li><li><a href="../Sales/Quote?JOB=' + this.ID + '">Estimate</a></li><li><a href="../Sales/Entry?JOB=' + this.ID + '">Sales Invoice</a></li></ul></div></div></div><div class="row m-t-40"><div class="col-md-3"><label class="label-chkbx">Created</label><div class="checkbox checkbox-success">';

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

            //click in edit of invoice list
            $('#tblInvoices').children('tbody').on('click', '.edit-invoice', function () {
                var ID = $(this).closest('tr').children('td:nth-child(1)').text();
                window.open('/Sales/Entry?MODE=edit&UID=' + ID, '_self');

            })
            //Load job of customer

            function LoadInvoices() {
                var cusId = $('#cusId').text();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/customers/GetCustomerInvoice?Customer_id=' + cusId + '&FromDate=' + '1 / 1 / 1753' + '&ToDate=' + '12 / 31 / 9999',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (response) {
                        var html = '';
                        //console.log(response);
                        var html = '';
                        $('#tblInvoices').DataTable().destroy();
                        $('#tblInvoices').children('tbody').children('tr').remove();
                        $(response).each(function () {
                            html += '<tr>';
                            html += '<td  style="display:none">' + this.ID + '</td>';
                            html += '<td>' + this.SalesBillNo + '</td>';
                            html += '<td>' + this.EntryDateString + '</td>';
                            html += '<td>' + this.TaxAmount + '</td>';
                            html += '<td>' + this.Gross + '</td>';
                            html += '<td>' + this.NetAmount + '</td>';
                            html += '<td><a class="edit-invoice" href="#"><i class="fa fa-edit "></i></a></td>';
                            html += '</tr>';

                        });
                        $('#tblInvoices').append(html);

                        $('#tblInvoices').DataTable({ destroy: true, dom: 'Blfrtip', buttons: ['copy', 'excel', 'csv', 'print'] });
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
                        miniLoading('stop', null);
                    },
                });
            }

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
                        $('#txtEndDate').val(response.CompletedDateString);
                        $('#ddlJobSalutation').val(response.Salutation);
                        $('#ddlSiteSalutation').val(response.Salutation);
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
                        $('#ddlJobCountry').val(response.CountryId);
                        $('#ddlSiteCountry').val(response.SiteCountryId);
                        LoadStates(response.SiteStateId, $('#ddlSiteCountry'), $('#ddlSiteState'));
                        LoadStates(response.StateId, $('#ddlJobCountry'), $('#ddlJobState'));
                        $('#txtJobPhone1').val(response.ContactPhone1);
                        $('#txtJobPhone2').val(response.ContactPhone2);
                        $('#txtSitePh1').val(response.SiteContactPhone1);
                        $('#txtSitePh2').val(response.SiteContactPhone2);
                        $('#txtJobEmail').val(response.Email);
                        $('#txtSiteEmail').val(response.SiteEmail);
                        $('#txtStartDate').val(response.StartDateString);
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
                                    LoadJobs();
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
                            LoadJobs();
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
                            LoadJobs();
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
                            LoadJobs();
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
                            LoadJobs();
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

            //Edit customer
            $('#btnEditCustomer').click(function () {
                window.open('../Masters/Inventories?ID=' + $('#cusId').text() + '&Mode=EDIT&Section=CUSTOMER', '_self');
            });

            $('.edit-cust').click(function () {
                window.open('../Masters/Inventories?ID=' + $('#cusId').text() + '&Mode=EDIT&Section=CUSTOMER', '_self');
            });

            $('#btnAddCustomer').click(function () {
                window.open('../Masters/Inventories?Mode=NEW&Section=CUSTOMER', '_self')
            });

            $('.delete-cust').click(function () {
                $('#btnDeleteCustomer').trigger('click');
            });

            $('#btnDeleteCustomer').click(function () {
                if ($('#cusId').text() != 0) {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    }, function (isConfirm) {

                        var id = $('#cusId').text();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/Customers/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        $('.md-cancel').trigger('click');
                                        refreshTable(null);

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

            //Hide/Show columns of table
            //function hideTableColumn() {
            //    $('#invoiceList.table>tbody>tr').each(function () { $(this).find('td').eq(4).hide() }); $('#invoiceList.table>thead>tr').find('th').eq(4).hide()
            //    $('#invoiceList.table>tbody>tr').each(function () { $(this).find('td').eq(5).hide() }); $('#invoiceList.table>thead>tr').find('th').eq(5).hide()
            //}

            $(document).on('change', '#ddlCountry', function () {
                loadStates(0);
            });

            //Function to load Addresses
            function LoadAddress(id) {
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Customers/GetCustomerAddress?CustomerID=' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        $('.address-tab').empty();
                        html = '';
                        $(response).each(function (index) {
                            html += '<ul class="customer-lists multiple list-unstyled">';
                            html += '<li><span class="hidden id">' + this.CustomerAddressID + '</span>';
                            html += '<p><b><span class="salutation">' + this.Salutation + '</span> <span class="cust-name">' + this.Name + '</span></b><a href="#" class="pull-right address-edit"><i class="ion-edit"></i></a>';
                            //To set the primary address tag.
                            if (this.IsPrimary) {
                                html += '<span class="hidden primary">1</span><small class="text-green"> ( Primary )</small></p>';
                            }
                            else {
                                html += '<span class="hidden primary">0</span><a href="#" class="pull-right m-r-10 set-primary">Set as Primary</a><span class="text-danger"><a href="#" class="pull-right m-r-10 delete-address"><i class="fa fa-trash"></i></a></span></p>'
                            }
                            if (this.Address1 != "") {
                                html += '<p><span class="address1">' + this.Address1 + '</span>';
                            }
                            if (this.Address2 != "") {
                                html += ',</p><p><span class="address2">' + this.Address2 + '</span></p>';
                            }
                            else {
                                html += '</p>';
                            }
                            if (this.City != "") {
                                html += '<p><span class="city">' + this.City + '</span> ';
                            }
                            if (this.StateId != 0) {
                                html += ',<span class="state-id hidden">' + this.StateId + '</span><span class="state">' + this.State + '</span></p>';
                            }
                            else        
                            {
                                html+='</p>';
                            }
                            html += '<p><span class="country-id hidden">' + this.CountryId + '</span><span class="country">' + this.Country + '</span>';
                            if (this.ZipCode != '') {
                                html += '| <b>Zip:</b><span class="zipcode">' + this.ZipCode + '</span></p>';
                            }
                            else {
                                html += '</p>';
                            }
                            if (this.Phone1 != "") {
                                html += '<p><b>Ph: </b><span class="phone1">' + this.Phone1 + '</span> ';
                            }
                            if (this.Phone2 != "") {
                                html += ',<span class="phone2">' + this.Phone2 + '</span></p>';
                            }
                            if (this.Email!="") {
                                html += '<p><b>Email:</b><span class="email">' + this.Email + '</span></p>';
                            }
                            html += '</li>';
                            html += '</ul>';
                        });
                        $('.address-tab').append(html);

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            function loadStates(state) {
                var selectedstate = state | 0;
                var selected = $('#ddlCountry').val();
                if (selected == "0") {
                    $('#ddlState').empty();
                    $('#ddlState').append('<option value="0">--Select--</option>');
                }
                else {
                    var company = $.cookie("bsl_1");
                    $.ajax({
                        type: "POST",
                        url: $('#hdApiUrl').val() + "api/customers/GetStates?id=" + selected,
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($.cookie("bsl_1")),
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            $('#ddlState').empty();
                            $('#ddlState').append('<option value="0">--Select--</option>');
                            $.each(data, function () {
                                $("#ddlState").append($("<option/>").val(this.StateId).text(this.State));
                            });
                            $("#ddlState").val(selectedstate);
                        },
                        failure: function () {
                            console.log("Error")
                        }
                    });
                }
            }
            $('.listing-search-entity').on('change keyup', function () {
                searchOnTable($('.listing-search-entity'), $('#invoiceList'), 1);
            });
        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.debug.js"></script>
    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>
</asp:Content>
