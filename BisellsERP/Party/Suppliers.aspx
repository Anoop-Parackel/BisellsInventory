<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="BisellsERP.Party.Suppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Suppliers</title>
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
            height: calc(100vh - 197px);
            margin-top: 2px;
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

        .m-b-9 {
            margin-bottom: 9px;
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
            line-height: 40px;
        }

        .nav-inner.nav-tabs > li.active {
            border-bottom: 2px solid #1e88e5;
        }

        .overview-bg {
            background-color: whitesmoke;
        }

        .bd-bottom {
            border-top-style: outset;
        }


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

        .ul-lookup > .card {
            background-color: #fff;
            min-height: 170px;
            padding: 10px;
            /*box-shadow: 0 2px 1px 0 #ccc;*/
            margin: 10px auto;
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
        .supplier-lists li {
            padding: 5px 10px;
            background-color: #fff;
            border: 1px solid #efefef;
            border-radius: 3px;
            margin-top: 3px;
        }

        .supplier-lists li .address-edit {
            opacity: 0;
            transition: opacity .3s ease-in;
        }

        .supplier-lists li:hover .address-edit {
            opacity: 1;
            color: #3cba9f;
        }

        .supplier-lists li:hover .address-edit:hover {
            opacity: 1;
            color: black;
        }

            .supplier-lists li > p {
                margin: 0 !important;
                font-size: 12px;
            }
            .supplier-lists li > label {
                margin: 0;
            }

                .supplier-lists li > p:first-child {
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
                padding: 10px 10px 0;
                margin: 0 0 30px;
                box-shadow: none;
            }

        thead > tr > th {
            padding: 10px !important;
            border-bottom: 1px solid #ddd !important;
            color: #6f726f;
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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <input type="hidden" id="hdId" value="0" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Suppliers</h3>
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
                                All Suppliers &nbsp;<b id="countOfInvoices">(0)</b>&nbsp;<span class="caret"></span>
                            </button>
                        </div>
                        <input type="text" class="form-control listing-search-entity" placeholder="Search Supplier..."/>
                         <div class="pull-right t-y search-group">
                           
                        </div>
                    </div>
                    <div class="left-side">
                        <div class="panel">
                            <div class="panel-body p-0" style="min-height: calc(100vh - 225px)" >
                                <table id="invoiceList" class="table left-table invoice-list">
                                    <thead>
                                        <tr>
                                            <th style="display: none">Id</th>
                                            <th class="show-on-collapsed">Supplier</th>
                                            <%--<th >Company</th>--%>
                                            <th class="show-on-collapsed">Email</th>
                                            <th>Phone</th>
                                            <th class="show-on-collapsed">Payable</th>
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
                                        <a href="../Masters/Inventories?Mode=NEW&Section=SUPPLIER" class="btn btn-default btn-xs btn-rounded waves-effect">Create New Supplier</a>
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
                        <div class="btn-toolbar m-b-15" role="toolbar">
                            <div class="btn-group">
                                <button type="button" id="btnEditSupplier" class="btn btn-default waves-effect waves-light" title="edit"><i class="md md-mode-edit"></i></button>
                                <button type="button" class="btn btn-default waves-effect waves-light" id="btnAddSupplier" title="New"><i class="fa fa-plus"></i></button>
                                <button type="button" id="btnDeleteSupplier" class="btn btn-default waves-effect waves-light" title="delete"><i class="fa fa-trash-o"></i></button>
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

                <div class="panel panel-default right-side">
                    <div class="panel-body p-0">

                        <ul class="nav nav-inner nav-tabs">
                            <li class="active">
                                <a href="#overview" data-toggle="tab" aria-expanded="false">
                                    <span>Overview</span>
                                </a>
                            </li>
                            <li class="">
                                <a href="#transactions" id="hrefTransaction" data-toggle="tab" aria-expanded="true">
                                    <span>Transactions</span>
                                </a>
                            </li>
                       <%--     <li class="">
                                <a href="#jobs" id="hrefJobs" data-toggle="tab" aria-expanded="true">
                                    <span>Jobs</span>
                                </a>
                            </li>--%>
                        </ul>

                        <div class="tab-content row supplier-list">
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
                                                    <h4 class="text-right m-0" id="lblSupName">Tony Stark</h4>
                                                    <p class="text-right"><small id="lblSupEmail" class="text-muted">suppliermail@gmail.com</small></p>
                                                    <p class="text-right m-0"><a href="#" class="edit-sup"><small>Edit</small></a>&nbsp;|&nbsp;<a href="#" class="text-danger delete-sup"><small><i class="ion-trash-a"></i></small></a></p>
                                                    <label style="display: none" id="supId">0</label>
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
                                                                <input type="text" id="txtSupName" class="form-control input-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Address Line 1</label>
                                                                <textarea rows="2" id="txtSupAddress1" class="form-control input-sm"></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">Address Line 2</label>
                                                                <textarea rows="2" id="txtSupAddress2" class="form-control input-sm"></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="form-group">
                                                                <label class="control-label">City</label>
                                                                <input type="text" id="txtSupCity" class="form-control input-sm" />
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
                                                                <input type="email" class="form-control input-sm" id="txtEmail" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-12">
                                                            <div class="btn-toolbar pull-right m-t-15">
                                                                <button type="button" class="btn btn-default btn-sm update-address-pop">Save</button>
                                                                <button type="button" class="btn btn-inverse btn-sm close-popover">x</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <input type="hidden" id="hdnSupplierAddressID" value="0" />
                                                    <input type="hidden" id="hdnPrimaryAddress" value="0" />
                                                </div>

                                                <div class="title-div">
                                                    <span><i class="ion-briefcase"></i>&nbsp;Statutory Details</span>
                                                </div>

                                                <ul class="customer-lists list-unstyled">
                                                    <li>
                                                        <p></p>
                                                        <p>TRN/GST No : <label id="lblGstNo">12345678951256123</label></p>
                                                        <p>CIN/Reg No : <label id="lblSinNo">123123123</label></p>
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
                                                            <div class="pull-right number"><span class="total-orders">$ 35000</span></div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="widget">
                                                        <div class="widget-heading text-center">
                                                            Total Bills
                                                        </div>
                                                        <div class="widget-body clearfix">
                                                            <div class="pull-left">
                                                                <i class="ion-flag"></i>
                                                            </div>
                                                            <div class="pull-right number"><span class="no-of-bills">$ 35000</span></div>
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
                                                        <small>29/01/2018 10:08 AM</small>
                                                    </div>
                                                    <div class="tree-node-leaf">
                                                        <div class="arrow"></div>
                                                        <p>
                                                            Invoice added
                                                        </p>
                                                        <div class="text-muted">
                                                            Invoice INV-000001 of amount Rs.234.00 created
                                                                 <strong>by Akhil</strong>
                                                            - <a data-ember-action="" data-ember-action-3261="3261"><small>View Details</small></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tree-node right">
                                                    <div class="time">
                                                        <small>29/01/2018 10:06 AM</small>
                                                    </div>
                                                    <div class="tree-node-leaf">
                                                        <div class="arrow"></div>
                                                        <p>
                                                            Contact added
                                                        </p>
                                                        <div class="text-muted">
                                                            Contact created
                                                       <strong>by Akhil</strong>
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

                            <%-- tab for payments --%>
                            <div class="tab-pane" id="transactions">
                                <div class="col-md-12 table-pos list-scroll">
                                    <table id="tblTransactions" class="table  table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th>Date</th>
                                                <th>Particulars</th>
                                                <th>Debit</th>
                                                <th>Credit</th>
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
                <!-- panel body -->
            </div>
            <!-- panel -->
        </div>
        <!-- End Right sidebar -->
    </div>

     <%--find list modal ends here--%>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>

    <!-- Date Range Picker -->
<%--    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />--%>
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
   <script>
       $(document).ready(function () {


               $(document).on('click', '.address-edit', function (e) {
                   //if ($(this).closest('li').children('.id').text() != "0") {

                   //}
                   //else {
                   //    //alert($('#cusId').text());
                   //}
                   var isPrimary = $(this).parent('p').children('.primary').text();
                   var id = $('#supId').text();
                   var addressid = $(this).closest('li').children('.id').text();
                   var countryID = $(this).closest('li').find('.country-id').text();
                   if(countryID==undefined||countryID=='')
                   {
                      countryID=0;
                   }
                   var StateID = $(this).closest('li').find('.state-id').text();
                   if (StateID==undefined||StateID=='') {
                       StateID = '0';
                   }
                   var Salutation = $(this).closest('li').find('.salutation').text();
                   var Name=$(this).closest('li').find('.sup-name').text();
                   var address1=$(this).closest('li').find('.address1').text();
                   var address2=$(this).closest('li').find('.address2').text();
                   var City=$(this).closest('li').find('.city').text();
                   var ZipCode=$(this).closest('li').find('.zipcode').text();
                   var Phone1=$(this).closest('li').find('.phone1').text();
                   var Phone2 = $(this).closest('li').find('.phone2').text();
                   var Email = $(this).closest('li').find('.email').text();
                   $('#ddlSalutation').val(Salutation);
                   $('#txtSupName').val(Name);
                   $('#txtSupAddress1').val(address1);
                   $('#txtSupAddress2').val(address2);
                   $('#txtSupCity').val(City);
                   $('#txtZipNumber').val(ZipCode);
                   $('#ddlCountry').val(countryID);
                   loadStates(StateID);
                   //$('#ddlState').val(StateID).trigger('change');
                   $('#txtPhone1').val(Phone1);
                   $('#txtPhone2').val(Phone2);
                   $('#hdnSupplierAddressID').val(addressid);
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
                           Data.ID = $('#supId').text();
                           Data.Salutation = $('#ddlSalutation').val();
                           Data.Name = $('#txtSupName').val();
                           Data.Address1 = $('#txtSupAddress1').val();
                           Data.Address2 = $('#txtSupAddress2').val();
                           Data.City = $('#txtSupCity').val();
                           Data.StateId = $('#ddlState').val();
                           Data.CountryId = $('#ddlCountry').val();
                           Data.ZipCode = $('#txtZipNumber').val();
                           Data.Phone1 = $('#txtPhone1').val();
                           Data.Phone2 = $('#txtPhone2').val();
                           Data.ModifiedBy = $.cookie('bsl_3');
                           Data.SupplierAddressID = $('#hdnSupplierAddressID').val();
                           Data.Email = $('#txtEmail').val();
                           //If it is primary address then updates the address in the master table.Else updates only the address table
                           if ($('#hdnPrimaryAddress').val()==1) {
                               Data.IsPrimary = true;
                           }
                           else {
                               Data.IsPrimary = false;
                           }
                           console.log(Data);
                           $.ajax({
                               url: $('#hdApiUrl').val() + 'api/Suppliers/SaveAddress',
                               method: 'POST',
                               dataType: 'JSON',
                               data: JSON.stringify(Data),
                               contentType: 'application/json;charset=utf-8',
                               success: function (data) {
                                   console.log(data);
                                   if (data.Success) {
                                       LoadAddress($('#supId').text());
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
                        Data.ID = $('#supId').text();
                        Data.Salutation = $('#ddlSalutation').val();
                        Data.Name = $('#txtSupName').val();
                        Data.Address1 = $('#txtSupAddress1').val();
                        Data.Address2 = $('#txtSupAddress2').val();
                        Data.City = $('#txtSupCity').val();
                        Data.StateId = $('#ddlState').val();
                        Data.CountryId = $('#ddlCountry').val();
                        Data.ZipCode = $('#txtZipNumber').val();
                        Data.Phone1 = $('#txtPhone1').val();
                        Data.Phone2 = $('#txtPhone2').val();
                        Data.CreatedBy = $.cookie('bsl_3');
                        Data.Email = $('#txtEmail').val();
                        console.log(Data);
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/Suppliers/SaveAddress',
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify(Data),
                            contentType: 'application/json;charset=utf-8',
                            success: function (data) {
                                if (data.Success) {
                                    LoadAddress($('#supId').text());
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

           $(".address-wrap").niceScroll({
               cursorcolor: "#6d7993",
               cursorwidth: "5px"
           });

             $(".addon > ul").niceScroll({
                 cursorcolor: "#90A4AE",
                 cursorwidth: "8px"
             });

             $(".left-side").niceScroll({
                 cursorcolor: "#6d7993",
                 cursorwidth: "8px"
             });

             $(".timeline-wrap").niceScroll({
                 cursorcolor: "#6d7993",
                 cursorwidth: "8px"
             });

             $('#txtStartDate').datepicker({
                 autoClose: true,
                 format: 'dd/M/yyyy',

                 todayHighlight: true
             });

           //Event to set an address a primary.It will update the current address isprimary to true and updates the data in the primary table
             $(document).on('click', '.set-primary', function () {
                 var id = $('#supId').text();
                 var addressid = $(this).closest('li').children('.id').text();
                 var data = {};
                 data.ID = id;
                 data.SupplierAddressID = addressid;
                 data.ModifiedBy = $.cookie('bsl_3');
                 console.log(data);
                 $.ajax({
                     url: $('#hdApiUrl').val() + 'api/Suppliers/SetPrimaryAddress',
                     method: 'POST',
                     dataType: 'JSON',
                     data: JSON.stringify(data),
                     contentType: 'application/json;charset=utf-8',
                     success: function (Data) {
                         if (Data.Success) {
                             LoadAddress($('#supId').text());
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
                     url: $('#hdApiUrl').val() + 'api/Suppliers/DeleteAddress?id=' + id,
                     method: 'Delete',
                     dataType: 'JSON',
                     data: JSON.stringify($.cookie('bsl_3')),
                     contentType: 'application/json;charset=utf-8',
                     success: function (Data) {
                         if (Data.Success) {
                             LoadAddress($('#supId').text());
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


             //Set Request Date to current date
             var date = new Date();
             var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
             $('#txtStartDate').datepicker('setDate', today);
             // Below script used for to close the date picker (auto close is not working properly)
             $('#txtStartDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtStartDate').datepicker('hide');
           });

             var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
             $('.currency').html(currency);

             $(document).on('click', '.filter-list', function () {
                 var customerId = $('#ddlSupplier').val() == '0' ? null : $('#ddlSupplier').val();
                 refreshTable(supplierId);
             })

             //get list of invoices
             refreshTable(null);

             function refreshTable(supplierId) {

                 $.ajax({
                     url: $('#hdApiUrl').val() + 'api/Suppliers/GetSupplierList/?supplierId=' + supplierId,
                     method: 'POST',
                     contentType: 'application/json;charset=utf-8',
                     dataType: 'JSON',
                     data: JSON.stringify($.cookie('bsl_1')),
                     success: function (data) {
                         console.log(data);
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
                             //html += '<td >' + this.Company + '</td>';
                             html += '<td class="show-on-collapsed">' + this.Email + '</td>';
                             html += '<td>' + this.Phone1 + '</td>';
                             html += '<td class="show-on-collapsed">' + currencySymbol + '&nbsp;' + this.TotalPayables + '</td>';
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

             //Show right sidebar when invoice is clicked
             $(document).on('click', '.left-side table tbody tr', function () {
                 $('.left-sidebar').find('[class="col-md-12"]').removeClass('col-md-12').addClass('col-md-4 collapsed');
                 $('.left-side table tbody').find('.active').removeClass('active');
                 $('.search-group').hide();
                 $('.right-sidebar').removeClass('hidden');
                 $(".left-side").getNiceScroll().resize();
                 $(this).addClass('active');

                 var id = $(this).children('td').eq(0).text();
                 var amount=$(this).children('td').eq(4).text();
                 $('#supId').text(id);
                 $.ajax({
                     url: $('#hdApiUrl').val() + 'api/Suppliers/get/' + id,
                     method: 'POST',
                     contentType: 'application/json;charset=utf-8',
                     dataType: 'JSON',
                     data: JSON.stringify($.cookie('bsl_1')),
                     success: function (data) {

                         $('#lblSupName').text(data.Name);
                         $('#lblSupAddress1').text(data.Address1);
                         $('#lblSupAddress2').text(data.Address2);
                         $('#lblSupPhone1').text(data.Phone1);
                         $('#lblSupPhone2').text(data.Phone2);
                         $('#lblGstNo').text(data.Taxno1);
                         $('#lblSupplierEmail').text(data.Email);
                         $('#lblSinNo').text(data.Taxno2);
                         $('#lblSupEmail').text(data.Email);
                         $('#imgPhoto').attr('src', data.ProfileImagePath != '' ? data.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                         //loading time line of customer
                         LoadAddress(id);
                         $.ajax({
                             url: $('#hdApiUrl').val() + 'api/Suppliers/GetSupplierActivity/?id=' + id,
                             method: 'POST',
                             contentType: 'application/json;charset=utf-8',
                             dataType: 'JSON',
                             data: JSON.stringify($.cookie('bsl_1')),
                             success: function (response) {
                                 var html = '';
                                 console.log(response);
                                 $(response).each(function (index) {
                                     html += '<div class="tree-node right"><div class="time"><small>' + this.Date + '&nbsp;' + this.Time + '</small></div><div class="tree-node-leaf"><div class="arrow"></div><div class="text-muted">' + this.Activity + '<small><br/><a href="' + this.Url + '">#' + this.Reference + '</a></small>&nbsp;|&nbsp;<small>' + this.EntryDate + '</small></div></div></div>';

                                 });
                                 $('.activities').children().remove();
                                 $('.activities').append(html);
                                 LoadTransactions();
                                 if(response.length!=0){
                                     console.log(response);
                                    $('.no-of-bills').text(response[0].NoOfBills);
                                    $('.total-orders').text(response[0].NoOfOrders); 
                                     $('.payment-due').text(amount);
                                 }
                                 else       
                                 {
                                     $('.no-of-bills').text("0");
                                     $('.total-orders').text("0"); 
                                     $('.payment-due').text(amount);
                                 }
                                
                             },
                             error: function (err) {
                                 alert(err.responseText);
                                 loading('stop', null);
                             },
                             beforeSend: function () { miniLoading('start', null) },
                             complete: function () { miniLoading('stop', null); },
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

             //Loading the table for transactions of supplier
             function LoadTransactions() {
                 var supId = $('#supId').text();
                 $.ajax({
                     url: $('#hdApiUrl').val() + 'api/FinancialTransactions/GetdetailsSupplierwise/?SupplierId=' + supId,
                     method: 'POST',
                     contentType: 'application/json;charset=utf-8',
                     dataType: 'JSON',
                     success: function (response) {
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
                     complete: function () { miniLoading('stop', null); },
                 });
             }
             //Click in transaction tab
             $('#hrefTransaction').click(function () {
                 LoadTransactions();
             })
             //end of click in transaction tab

            
             //Edit Supplier
             $('#btnEditSupplier').click(function () {
                 window.open('../Masters/Inventories?ID=' + $('#supId').text() + '&Mode=EDIT&Section=SUPPLIER', '_self');
             });

             $('.edit-sup').click(function () {
                 window.open('../Masters/Inventories?ID=' + $('#supId').text() + '&Mode=EDIT&Section=SUPPLIER', '_self');
             });

             $('.delete-sup').click(function () {
                    $('#btnDeleteSupplier').trigger('click');
             });

             //Add new Supplier
             $('#btnAddSupplier').click(function () {
                 window.open('../Masters/Inventories?Mode=NEW&Section=SUPPLIER', '_self')
             });
             //delete supplier
             $('#btnDeleteSupplier').click(function() {
                 if ($('#supId').text() != 0) {
                     swal({
                         title: "Delete?",
                         text: "Are you sure you want to delete?",
                         showConfirmButton: true, closeOnConfirm: true,
                         showCancelButton: true,
                         cancelButtonText: "Back to Entry",
                         confirmButtonClass: "btn-danger",
                         confirmButtonText: "Delete"
                     }, function (isConfirm) {

                         var id = $('#supId').text();
                         var modifiedBy = $.cookie('bsl_3');
                         if (isConfirm) {
                             $.ajax({
                                 url: $('#hdApiUrl').val() + 'api/Suppliers/delete/' + id,
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

             //$(".right-side, .left-side").niceScroll({
             //    cursorcolor: "#90A4AE",
             //    cursorwidth: "8px",
             //    horizrailenabled: false
           //});

           //Function to load Addresses
             function LoadAddress(id) {
                 $.ajax({
                     url: $('#hdApiUrl').val() + '/api/Suppliers/GetSupplierAddress?SupplierID=' + id,
                     method: 'POST',
                     contentType: 'application/json; charset=utf-8',
                     dataType: 'Json',
                     data: JSON.stringify($.cookie("bsl_1")),
                     success: function (response) {
                         console.log(response);
                         $('.address-tab').empty();
                         html = '';
                         $(response).each(function (index) {
                             html += '<ul class="supplier-lists multiple list-unstyled">';
                             html += '<li><span class="hidden id">' + this.SupplierAddressID + '</span>';
                             html += '<p><b><span class="salutation">' + this.Salutation + '</span> <span class="sup-name">' + this.Name + '</span></b><a href="#" class="pull-right address-edit"><i class="ion-edit"></i></a>';
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
                                 html += '<p><span class="city">' + this.City + '</span>';
                             }
                             if (this.StateId != 0) {
                                 html += ',<p><span class="state-id hidden">'+this.StateId+'</span><span class="state">' + this.State + '</span></p>';
                             }
                             else {
                                 html += '</p>';
                             }
                             html += '<p><span class="country-id hidden">' + this.CountryId + '</span><span class="country">' + this.Country + '</span>';
                             if (this.ZipCode!="") {
                                 html += '|<b>Zip:</b><span class="zipcode">' + this.ZipCode + '</span></p>';
                             }
                             else {
                                 html += '</p>';
                             }
                             html += '<p><b>Ph: </b>';
                             if (this.Phone1 != "") {
                                 html += '<span class="phone1">' + this.Phone1 + '</span>,';
                             }
                             else {
                                 html += '<span class="phone1"></span>';
                             }
                             if (this.Phone2 != "") {
                                 html += '<span class="phone2">' + this.Phone2 + '</span></p>';
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

             $(document).on('change', '#ddlCountry', function (e) {
                 loadStates(0);
             });
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
<%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.debug.js"></script>--%>
    <script src="../Theme/assets/jspdf/jspdf.debug.js"></script>

</asp:Content>
