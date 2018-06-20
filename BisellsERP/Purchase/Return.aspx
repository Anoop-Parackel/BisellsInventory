<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Return.aspx.cs" Inherits="BisellsERP.Purchase.Return" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Purchase New Return</title>
    <style>
        #wrapper {
            overflow: hidden;
        }

        .mini-stat.clearfix.bx-shadow {
            height: 80px;
            padding: 15px 5px;
        }

        .edit-value {
            background-color: transparent;
            width: 40px;
            text-align: right !important;
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

        .round-no-border {
            border-radius: 4px;
            border: none;
            background-color: #ECEFF1;
        }

        .panel {
            margin-bottom: 10px;
        }

        tbody tr td {
            padding: 5px !important;
            font-size: smaller;
        }

        .quote-date {
            text-align: right;
            border: none;
            color: #014d6f;
            cursor: pointer;
            font-size: smaller;
            width: 70%;
            float: right;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 25px;
        }

        .add-dd-btn {
            background-color: #607D8B;
            color: #fff;
            box-shadow: none;
            border-radius: 5px;
        }


        .item-list {
            height: calc(90vh - 42px);
            display: inline-block;
            width: 100%;
            margin-bottom: 0;
            overflow-y: auto;
        }

        .item-list-active {
            border-left: 3px solid #29b6f6;
        }

        .item-list > .card {
            background-color: #fff;
            margin: 5px 0;
            box-shadow: 0 2px 1px 0 #ccc;
            padding: 5px 10px;
            height: 85px;
        }

            .item-list > .card .col-xs-5, .item-list > .card .col-xs-7 {
                height: 75px;
            }

            .item-list > .card .col-xs-5 {
                border-right: 1px dashed #ccc;
            }

                .item-list > .card .col-xs-5 > div:last-child {
                    position: absolute;
                    bottom: 0;
                    width: calc(100% - 20px);
                    color: #616161;
                    font-size: small;
                }

                .item-list > .card .col-xs-5 > div:first-child label {
                    display: inline-block;
                    font-size: 14px;
                    font-weight: 500;
                    letter-spacing: .5px;
                    padding-top: 5px;
                    background-color: #546E7A;
                }

                .item-list > .card .col-xs-5 > div:first-child i {
                    font-size: 1.5em;
                    margin-bottom: 5px;
                    color: #455A64;
                }


            .item-list > .card .col-xs-7 > div:last-child {
                position: absolute;
                bottom: 8px;
                width: calc(100% - 20px);
                text-align: left;
                color: #616161;
            }

                .item-list > .card .col-xs-7 > div:last-child > label {
                    position: absolute;
                    right: -3px;
                    z-index: 2;
                    top: -10px;
                }

            .item-list > .card .col-xs-7 > div:not(:last-child) {
                font-size: 14px;
                margin-top: 8px;
                font-weight: 700;
                color: cadetblue;
            }

            .item-list > .card .col-xs-7 > div:last-child a {
                float: right;
                padding: 1px 12px;
                background-color: transparent;
                border: 1px solid #37474F;
                color: #37474F;
                margin-right: 5px;
            }

            .item-list > .card .col-xs-7 small {
                color: #757575;
            }

        .stock-invoice {
            position: absolute;
            right: 10px;
            top: 0;
        }

        .item-list > .card .col-xs-7.stock-list > div:not(:last-child) {
            margin-top: 0;
            font-weight: 700;
            color: cadetblue;
        }

        .list-table > div:first-child {
            height: calc(90vh - 75px);
            width: 100%;
            margin-bottom: 5px;
            border-bottom: 1px dotted #ccc;
            overflow-y: auto;
        }


        .list-table input {
            height: 28px;
            padding: 4px;
            border: 1px solid #ccc;
        }

        .list-table button {
            padding: 3px 12px;
        }

        #viewModal .modal-header {
            padding: 4px 12px !important;
        }

        .inv-search-input {
            border: 1px solid #ccc;
            border-radius: 20px;
            padding: 3px 12px;
            font-size: 12px;
            width: 125px;
            margin-left: 10px;
            transition: width .3s ease-in-out;
        }

            .inv-search-input:focus {
                width: 185px;
            }

        /* For Scroll Bar */
        .list-table > div:first-child::-webkit-scrollbar, .item-list::-webkit-scrollbar {
            width: 8px;
            background-color: #F5F5F5;
        }

        .list-table > div:first-child::-webkit-scrollbar-thumb, .item-list::-webkit-scrollbar-thumb {
            border-radius: 8px;
            background: linear-gradient(left, #fff, #e4e4e4);
            border: 1px solid #aaa;
        }

            .list-table > div:first-child::-webkit-scrollbar-thumb:hover, .item-list::-webkit-scrollbar-thumb:hover {
                background: #fff;
            }

            .list-table > div:first-child::-webkit-scrollbar-thumb:active, .item-list::-webkit-scrollbar-thumb:active {
                background: linear-gradient(left, #22ADD4, #1E98BA);
                ;
            }

        .daterangepicker.dropdown-menu.ltr.opensleft.show-calendar {
            right: auto !important;
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

        .list-unstyled {
            padding-left: 0;
            list-style: none;
            cursor: pointer;
        }

            .list-unstyled li:hover {
                background-color: #f6fbf9;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">
        <%--hidden fileds--%>
        <asp:HiddenField runat="server" Value="" ID="hdTandC" ClientIDMode="Static" />
        <input type="hidden" value="0" id="hdId" />
        <input id="hdEmail" type="hidden" value="0" />
        <%-- ---- Page Title ---- --%>
        <div class="row p-b-5">
            <div class="col-sm-4">
                <h3 class="page-title m-t-0">Debit Note</h3>
            </div>
            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group">
                    <button type="button" accesskey="v" data-toggle="tooltip" data-placement="bottom" title="View previous debit note" id="btnFind" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                    <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new debit note . Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                    <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the curren return" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                    <button type="button" accesskey="p" id="btnPrint" data-toggle="tooltip" data-placement="bottom" title="Print the current debit note" class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i></button>
                    <button id="btnMail" type="button" class="btn btn-default waves-effect waves-light" data-toggle="modal" data-target="#modalMail"><i class="icon ion-chatbox"></i>&nbsp;Mail</button>
                    <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion ion-trash-b"></i></button>
                </div>

            </div>
        </div>

        <%--<a href="#" id="testClick" data-toggle="modal" data-target="#viewModal">Testt</a>--%>

        <%-- ---- Search Quote Panel ---- --%>
        <div class="row search-quote-panel">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="panel b-r-5">
                            <div class="panel-body">
                                <div class="col-sm-8">
                                    <label class="title-label">Add  to list from here..</label>
                                    <input type="text" autocomplete="off" id="txtChooser" class="form-control" placeholder="Choose Item" />
                                    <div id="lookup">
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                                    <input type="text" id="txtQuantity" autocomplete="off" class="form-control" placeholder="Qty" />
                                </div>
                                <div class="col-sm-2">
                                    <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List"
                                        class="btn btn-icon btn-primary">
                                        <i class="ion-plus"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-5">
                        <div class="panel b-r-5">
                            <div class="panel-body">
                                <div class="col-sm-4">
                                    <label class="title-label">Return From</label>
                                    <select id="ddlItem" class="form-control">
                                        <option value="0">Stock</option>
                                        <option value="1">Damage</option>
                                    </select>
                                </div>
                                <div class="col-sm-4">
                                    <label class="title-label">Supplier</label>
                                    <asp:DropDownList ID="ddlSupplier" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label class="title-label">Reasons</label>
                                    <select id="ddlReason" class="form-control">
                                        <option value="0">Damage</option>
                                        <option value="1">Wrong Supply</option>
                                        <option value="2">Item Shortage</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="panel b-r-8">
                            <div class="panel-body p-t-15">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <span>Order No : <b></b></span>
                                        <asp:Label ID="lblOrderNumber" runat="server" ClientIDMode="Static" class="badge badge-danger pull-right"><b>KU1368B</b></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <span>Date : </span>
                                    <input type="text" id="txtEntryDate" class="quote-date" value="21/Dec/2017" />
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
                <div class="panel panel-default view-h b-r-10">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th class="hidden">InstanceID</th>
                                            <th>Item </th>
                                            <th>Code</th>
                                            <th style="text-align: right">Tax%</th>
                                            <th style="text-align: right">MRP</th>
                                            <th style="text-align: right">Rate&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                            <th style="text-align: right">Quantity</th>
                                            <th style="text-align: right">Tax</th>
                                            <th style="text-align: right">Gross</th>
                                            <th style="text-align: right">Net</th>
                                            <th class="hidden">ReturnFrom</th>
                                            <th style="text-align: right">Return From</th>
                                            <th class="hidden">ItemId</th>
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
            <div class="col-sm-9 col-lg-10">
                <div class="mini-stat clearfix bx-shadow b-r-10">
                    <div class="row">
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Total Items</label>
                            <%-- Total Items --%>
                            <span id="lblTotalItem" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Gross </label>
                            <%-- Gross Amount --%>
                            <span id="lblGross" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">

                            <label class="w-100 light-font-color">Total </label>
                            <%-- Total Amount --%>
                            <span id="lblTotal" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Tax </label>
                            <%-- Tax Amount --%>
                            <span id="lblTax" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Round Off</label>
                            <%-- Round Off --%>
                            <asp:TextBox ID="txtRoundOff" AutoComplete="off" CssClass="w-100 l-font" Style="border: none; text-align: center; background-color: transparent; font-size: 20px" ClientIDMode="Static" runat="server" placeholder="RoundOff">0.00</asp:TextBox>
                        </div>
                        <div class="col-sm-2 text-center">
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

            <div class="col-sm-3 col-lg-2">
                <%-- NET AMOUNT --%>
                <div class="mini-stat clearfix bx-shadow b-r-10">
                    <div class="col-sm-2"><span class="currency">$</span></div>
                    <div class="col-sm-10">
                        <h3 class="text-right text-primary m-0">
                            <span id="lblNet" class="counter"></span>
                        </h3>
                        <div class="mini-stat-info text-right text-muted">
                            Net Amount
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Debit Note &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
                    <div id="filterWrap" class="hide">
                        <label>Filter by Supplier :</label>
                        <asp:DropDownList ID="ddlSupplierFilter" ClientIDMode="Static" CssClass="" runat="server"></asp:DropDownList>
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
                                <th>Order No</th>
                                <th>Date</th>
                                <th>Supplier</th>
                                <th>Tax</th>
                                <th>Gross</th>
                                <th>Net </th>
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

    <!-- Modal -->
    <div id="modalMail" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">

            <!-- Modal content-->
            <div class="modal-content m-10">
                <div class="modal-body p-0">
                    <div class="row">
                        <div class="before-send">
                            <h2 class="text-center">Send Mail?</h2>
                            <p class="text-center">Are you sure you want to send mail?</p>
                            <div class="col-sm-6 col-sm-offset-3">
                                <input id="txtMailAddress" type="email" placeholder="Enter email address..." class="form-control m-t-10" />
                            </div>
                            <div class="col-sm-12 text-center m-t-20">
                                <button id="btnSend" type="button" class="btn btn-default m-t-5">Send&nbsp;<i class="md-done"></i></button>
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


    <%-- View modal--%>
    <div id="viewModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content modal-content-h-lg p-0">
                <div class="modal-header">
                    <h4 class="modal-title"><span id="titleModal">Item Name</span>
                        <input type="text" placeholder="Search Invoice No..." class="inv-search-input" />
                    </h4>
                </div>
                <div class="modal-body p-0">
                    <div class="">
                        <div class="col-sm-5" style="background-color: #eaeaea; overflow-y: auto;">
                            <ul id="listItems" class="list-unstyled item-list">
                                <li class="card">
                                    <div class="row">
                                        <div class="col-xs-5">
                                            <div class="text-center m-t-10">
                                                <label class="label">BS/Y/00063</label>
                                            </div>
                                            <div class="text-center text-muted">
                                                Dated :
                                        <label class="text-muted m-b-0">12 Nov 17</label>
                                            </div>
                                        </div>
                                        <div class="col-xs-7">
                                            <small>Tax :<label>123456</label></small>
                                            <small class="pull-right">Gross :<label>123456</label></small>
                                            <div>
                                                Net Amount
                                                <br />
                                                <label class="m-b-0">1234567</label>
                                            </div>
                                            <div>
                                                <label class="badge badge-info">4</label>
                                                <a href="#" class="btn btn-sm">View Bill</a>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li class="card">
                                    <div class="row">
                                        <div class="col-xs-5">
                                            <div class="text-center m-t-10">
                                                <label class="label">BS/Y/00063</label>
                                            </div>
                                            <div class="text-center text-muted">
                                                Dated :
                                        <label class="text-muted m-b-0">12 Nov 17</label>
                                            </div>
                                        </div>
                                        <div class="col-xs-7">
                                            <small>Tax :<label>123456</label></small>
                                            <small class="pull-right">Gross :<label>123456</label></small>
                                            <div>
                                                Net Amount
                                                <br />
                                                <label class="m-b-0">1234567</label>
                                            </div>
                                            <div>
                                                <label class="badge badge-info">4</label>
                                                <a href="#" class="btn btn-sm">View Bill</a>
                                            </div>
                                        </div>
                                    </div>
                                </li>

                            </ul>
                        </div>
                        <div class="col-sm-7 list-table">
                            <div>
                                <table id="listDetailTable" class="table table-condensed table-hover">
                                    <thead>
                                        <tr>
                                            <th class="hidden">ID</th>
                                            <th style="display: none">Item Id</th>
                                            <th>Name</th>
                                            <th>Code</th>
                                            <th style="text-align: right">Rate</th>
                                            <th style="text-align: right">Qty</th>
                                            <th style="text-align: right">Tax</th>
                                            <th style="text-align: right">Gross</th>
                                            <th style="text-align: right">Net </th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                            <div class="col-xs-4 col-xs-offset-5">
                           <input type="text" autocomplete="off" id="txtViewQuantity" class="form-control" placeholder="Qty" />
                                 </div>
                            <div class="col-xs-3">
                                <div class="btn-toolbar pull-right" role="group">
                                    <button type="button" id="btnSelect" class="btn btn-success waves-effect waves-light">Add</button>
                                    <button type="button" id="modalClose" class="btn btn-default text-inverse" data-dismiss="modal" aria-hidden="true">x</button>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <%-- View modal ends here--%>
    <%-- ---- ADDITIONAL SETTINGS  ---- --%>
    <div class="additional-settings-overlay"></div>
    <div class="row additional-settings-wrap">
        <div class="additional-settings-button">
            <i class="fa fa-angle-left"></i>
            <span>Additional Details</span>
        </div>
        <div class="col-xs-12 additional-settings-block">

            <ul class="nav nav-tabs">
                <li class="active">
                    <a href="#additionalOthersTab" data-toggle="tab" aria-expanded="true">
                        <span class="">General</span>
                    </a>
                </li>
                <li class="">
                    <a href="#additionalAddressTab" data-toggle="tab" aria-expanded="false">
                        <span class="">Billing Address</span>
                    </a>
                </li>
            </ul>

            <span class="additional-settings-close">
                <i class="fa fa-times-circle"></i>
            </span>

            <div class="tab-content overflow-content">
                <div class="tab-pane active" id="additionalOthersTab">
                    <div class="row">
                        <div class="col-md-12">
                            <%-- Job Cost Center --%>
                            <h5 class="sett-title p-t-0">Cost Center / Job</h5>
                            <div class="row">
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
                            <%-- Terms and Conditions --%>
                            <h5 class="sett-title p-t-0">Terms & Conditions</h5>
                            <div id="dvTandC" class="summernote-editor"></div>
                            <%-- Payment Terms --%>
                            <h5 class="sett-title p-t-0 m-t-10">Payment Terms</h5>
                            <div class="summernote-editor2"></div>
                            <div id="dvPaymentTerm" class="summernote-editor"></div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane" id="additionalAddressTab">
                    <div class="row">
                        <div class="col-md-6">
                            <h5 class="sett-title p-t-0">Supplier Address<a href="#" class="pull-right change-address"><small class="text-green"><i class="fa fa-edit"></i>&nbsp;change</small></a></h5>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Name</label>
                                            <div class="col-sm-3">
                                                <select class="form-control" id="ddlSalutation">
                                                    <option value="Mr">Mr.</option>
                                                    <option value="Mrs">Mrs.</option>
                                                    <option value="Ms">Ms.</option>
                                                    <option value="Miss">Miss.</option>
                                                </select>
                                            </div>
                                            <div class="col-sm-5">
                                                <input type="text" class="form-control" id="txtContactName" placeholder="Contact Name" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Contact Address 1</label>
                                            <div class="col-sm-8">
                                                <textarea class="form-control" id="txtAddress1"></textarea>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Contact Address 2</label>
                                            <div class="col-sm-8">
                                                <textarea class="form-control" id="txtAddress2"></textarea>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">City</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtCity" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Zipcode</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtZipCode" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Country</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList class="form-control" ID="ddlCountry" runat="server" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">State</label>
                                            <div class="col-sm-8">
                                                <select class="form-control" id="ddlStates">
                                                    <option value="0">--Select--</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Email</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtEmail" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Phone 1</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtPhone1" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Phone 2</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtPhone2" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="address-tab hidden">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12 overflow-content hidden">
                <div class="col-md-12">
                </div>
                <div class="col-md-12 p-t-10">
                </div>
                <div class="col-md-12 p-t-10">
                </div>
            </div>
        </div>
    </div>
    <script>
        //Find distance between Search-Panel and Summary-Panel
        $(window).resize(function () {
            var dataHeight = Math.abs($('.search-quote-panel').offset().top - $('.summary-panel').offset().top) - 105;
            if (dataHeight) {
                $('.view-h').css('min-height', dataHeight);
            }
        });

        //calculate function call
        setInterval(calculateSummary, 1000);
        setInterval(calculation, 1000);
        function calculation() {

            var tr = $('#listTable>tbody').children('tr');
            for (var i = 0; i < tr.length; i++) {
                var tempTax = 0;
                var qty = parseFloat($(tr[i]).children('td:nth-child(7)').text());

                var gross = parseFloat($(tr[i]).children('td:nth-child(7)').text()) * parseFloat($(tr[i]).children('td:nth-child(6)').children('input').val());
                gross = parseFloat(gross);//Gross Amount

                var taxper = parseFloat($(tr[i]).children('td:nth-child(4)').text());
                var tax = parseFloat($(tr[i]).children('td:nth-child(6)').children('input').val()) * (taxper / 100);
                tax = parseFloat(tax);//Tax Amount

                var net = gross + (tax * qty);
                net = parseFloat(net);//Net amount
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax.toFixed(2));
                $(tr[i]).children('td:nth-child(9)').text(gross.toFixed(2)); //gross amount
                $(tr[i]).children('td:nth-child(8)').text(tempTax.toFixed(2));  //tax amount
                $(tr[i]).children('td:nth-child(10)').text(net.toFixed(2));  //net amount
                qty = 0;
            }
        }
        function calculateSummary() {

            var tr = $('#listTable > tbody').children('tr');
            var tax = 0;
            var gross = 0;
            var net = 0;
            var round = 0.0;
            var temp = 0;
            var qty = $('#txtQuantity').val();
            for (var i = 0; i < tr.length; i++) {
                qty = parseFloat($(tr[i]).children('td:nth-child(7)').text());
                tax += parseFloat($(tr[i]).children('td:nth-child(8)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(9)').text());
                net += parseFloat($(tr[i]).children('td:nth-child(10)').text());
            }
            temp = net;
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
            var le = tr.length;
            $('#lblTotalItem').text(le);

            tax = parseFloat(tax.toFixed(2));
            gross = parseFloat(gross.toFixed(2));
            $('#lblGross').text(gross);
            $('#lblNet').text(net);
            $('#lblTax').text(tax);
            $('#lblTotal').text(gross);
        }
    </script>
    <script>

        $(document).ready(function () {

            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            $('#btnPrint').hide();
            $('#btnMail').hide();

            $("#modalMail").on('hidden.bs.modal', function () {
                $('.before-send').show();
                $('.after-send').hide();
                $('#txtMailAddress').val('');
            });


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
                    $('#btnMail').hide();
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
                    $('#btnMail').hide();
                    $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                }
            }
            $('[data-toggle="popover"]').popover({
                content: "<textarea placeholder=\" Enter Narration Here\"></textarea>"
            });
            //function for date picker
            $('#txtEntryDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            $('#txtDueDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            //lock location once selected
            $('#ddlSupplier').change(function () {
                if ($(this).val() != 0) {
                    $(this).prop('disabled', true);
                }
            });
            //Close details modal
            $('#modalClose').on('click', function () {
                $('#viewModal').modal('hide');
            });

            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtEntryDate').datepicker('setDate', today);
            var supplierId = $('#ddlSupplier').val();
            var locationId = $.cookie('bsl_2');
            var ReturnType = $('#ddlItem').val();

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
                OnComplete: function () { miniLoading('stop') }
            });

            //lookup initialization ends here

            function AddToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var taxper = parseFloat(tempItem.TaxPercentage);
                var rate = parseFloat(tempItem.CostPrice);
                var qty = parseFloat($('#txtQuantity').val());
                var TaxAmount = parseFloat(rate * (taxper / 100)).toFixed(2);
                var GrossAmount = parseFloat((qty * rate).toFixed(2));
                var NetAmount = parseFloat(GrossAmount + (TaxAmount * qty)).toFixed(2);
                var tempTax = (TaxAmount * qty).toFixed(2);
                //
                //Add item click
                var itmType = $('#ddlItem').val();
                //itmType=0 stock
                //itmType=1 damage
                //check if the return from stock 
                if (itmType == 0) {
                    var data = $.cookie('bsl_2');
                    supplirId = $('#ddlSupplier').val();
                    ItemId = tempItem.ItemID;
                    var InstID = tempItem.InstanceId;
                    var response;
                    $.ajax({
                        url: $(hdApiUrl).val() + 'api/PurchaseReturn/GetPurchaseEntry?SupplierId=' + supplirId + '&ItemId=' + ItemId + '&instanceId=' + InstID,
                        method: 'POST',
                        data: JSON.stringify(data),
                        contentType: 'application/json',
                        dataType: 'JSON',
                        success: function (response) {
                            $('#viewModal').modal('show');
                            if (response !== '') {
                                $('#listDetailTable tbody').children().remove();
                                var productName = $('#txtChooser').val();
                                var clickBit = false;
                                var viewQty = qty;//(TextBox)quantity inside the detail modal
                                $('#txtViewQuantity').val(qty);
                                $('#titleModal').text(productName);
                                var html = '';
                                $(response).each(function (index) {
                                    html += '<li id="' + this.ID + '" class="card"><div class="row"><div class="col-xs-5">Invoice:' + this.InvoiceNo + '<div class="text-center m-t-10"><label class="label">' + this.EntryNo + '</label></div><div class="text-center text-muted">Dated :<label class="text-muted m-b-0">' + this.InvoiceDateString + '</label></div></div><div class="col-xs-7"><small>Tax :<label>' + this.TaxAmount + '</label></small><small class="pull-right">Gross :<label>' + this.Gross + '</label></small><div >Net Amount <br /><label class="m-b-0">' + this.NetAmount + '</label></div><div><label class="badge badge-info">' + this.TotalItems + '</label><a href="#" id="clickBill" class="btn btn-sm">View Bill</a></div></div></div>';
                                })
                                $('#listItems').children().remove();
                                $('#listItems').append(html);

                                //click of list item
                                $('#viewModal').off().on('click', '#clickBill', function () {
                                    clickBit = true;//for validating selected entry
                                    var stockQty;//Quantity from DB
                                    var ped_id = 0;
                                    var pe_id = $(this).closest('li').attr('id');
                                    var li = $(this).closest('li');
                                    var data = $.cookie('bsl_2');
                                    if (pe_id != '0' && pe_id != '') {
                                        $.ajax({
                                            url: $(hdApiUrl).val() + 'api/PurchaseReturn/GetPurchaseEntryDetails?ID=' + pe_id,
                                            method: 'POST',
                                            data: JSON.stringify(data),
                                            contentType: 'application/json',
                                            dataType: 'JSON',
                                            success: function (register) {
                                                $('#listDetailTable tbody').children().remove();
                                                html = '';
                                                $(register[0].Products).each(function (index) {
                                                    html += InstID == this.InstanceId && this.ItemID == ItemId ? '<tr style="background-color: #fff;border: 2px solid #7fd7ff;">' : '<tr>';
                                                    html += '<td style="display:none">' + this.PedId + '</td>';
                                                    html += '<td style="display: none">' + this.ItemID + '</td>';
                                                    html += '<td>' + this.Name + '</td>';
                                                    html += '<td>' + this.ItemCode + '</td>';
                                                    html += '<td style="text-align:right">' + this.CostPrice + '</td>';
                                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                                    html += '</tr>';
                                                })
                                                $('#listDetailTable tbody').children().remove();
                                                $('#listDetailTable tbody').append(html);
                                                $('#listItems').children('li').removeClass('item-list-active');
                                                li.addClass('item-list-active');
                                                $('#viewModal').on('click', '#btnSelect', function () {
                                                    if (clickBit) {
                                                        var viewQty = $('#txtViewQuantity').val();
                                                        var html2 = '';
                                                        var qtyCheck = false;
                                                        $(register[0].Products).each(function (index) {
                                                            if (this.ItemID == ItemId && this.InstanceId == InstID) {

                                                                stockQty = this.Quantity;
                                                                var Name = this.Name;
                                                                var itemCode = this.ItemCode;
                                                                var taxPer = this.TaxPercentage;
                                                                var Mrp = this.MRP;
                                                                var CostPrice = this.CostPrice;
                                                                var tax = this.TaxAmount;
                                                                var Gross = this.Gross;
                                                                var IsNegBillingAllowed = JSON.parse($('#hdSettings').val()).AllowNegativeBilling;
                                                                var net = this.NetAmount;
                                                                var instanceId = this.InstanceId;
                                                                html2 += '<tr><td style="display:none">' + instanceId + '</td><td>' + Name + '</td><td>' + itemCode + '</td><td style="text-align:right">' + taxPer + '</td><td style="text-align:right">' + Mrp + '</td><td style="text-align:right"><input type="number" class="edit-value" value="' + CostPrice + '"/></td><td style="text-align:right">' + viewQty + '</td><td style="text-align:right">' + tax + '</td><td style="text-align:right">' + Gross + '</td><td style="text-align:right">' + net + '</td><td class="hidden">0</td><td style="text-align:right">Stock</td><td style="display:none">0</td><td style="display:none">' + this.ItemID + '</td><td>  <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
                                                                if (!IsNegBillingAllowed && this.Quantity < viewQty || viewQty == '' || viewQty <= 0) {
                                                                    qtyCheck = true;
                                                                }
                                                            }
                                                        });
                                                        if (!qtyCheck) {
                                                            var ItemExist = false;
                                                            var tbody = $('#listTable > tbody');
                                                            var tr = tbody.children('tr');
                                                            for (var i = 0; i < tr.length; i++) {
                                                                var ID = $(tr[i]).children('td:nth-child(1)').text();
                                                                var itm = $(tr[i]).children('td:nth-child(14)').text();

                                                                if (InstID == ID && itm == ItemId) {

                                                                    ItemExist = true;
                                                                    var existingQty = parseFloat($(tr[i]).children('td:nth-child(7)').text());
                                                                    var newQty = existingQty + parseFloat(viewQty);
                                                                    //Quantity check for add to list
                                                                    if (stockQty < newQty) {
                                                                        errorAlert('Quantiy Exceeds');
                                                                        break;
                                                                    }
                                                                    else {
                                                                        ItemExist = true;
                                                                        successAlert('Quantity Updated');
                                                                        $('#viewModal').modal('hide');
                                                                        $(tr[i]).children('td:nth-child(7)').text(parseFloat(newQty));
                                                                        sessionStorage.removeItem('tempItem');
                                                                        $('#txtQuantity').val('');
                                                                        $('#txtChooser').val('');
                                                                        $('#txtChooser').focus();
                                                                    }

                                                                }
                                                            }
                                                            if (!ItemExist) {
                                                                $('#listTable > tbody').append(html2);
                                                                sessionStorage.removeItem('tempItem');
                                                                clickBit = false;
                                                                $('#txtQuantity').val('');
                                                                $('#txtChooser').val('');
                                                                $('#txtChooser').focus();
                                                                $('#viewModal').modal('hide');
                                                                //Binding Event to remove button
                                                                $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });

                                                            }
                                                        }
                                                        else {
                                                            errorAlert('Check Quantiy');
                                                        }
                                                    }
                                                    //else {
                                                    //    errorAlert('Select An Entry');
                                                    //}
                                                });

                                            },
                                            error: function (xhr) {
                                                alert(xhr.responseText);
                                                console.log(xhr);
                                            }
                                        });
                                    }
                                });
                            }
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }
                    //check if the return from Damage 
                else {


                    var data = $.cookie('bsl_2');
                    supplirId = $('#ddlSupplier').val();
                    ItemId = tempItem.ItemID;
                    instanceId = tempItem.InstanceId
                    var response;
                    $.ajax({
                        url: $(hdApiUrl).val() + 'api/PurchaseReturn/GetSalesReturn/?SupplierId=' + supplirId + '&ItemId=' + ItemId + '&InstanceId=' + instanceId,
                        method: 'POST',
                        data: JSON.stringify(data),
                        contentType: 'application/json',
                        dataType: 'JSON',
                        success: function (response) {
                            $('#viewModal').modal('show');
                            if (response !== '') {
                                $('#listDetailTable tbody').children().remove();
                                var productName = $('#txtChooser').val();
                                var clickBit = false;
                                var viewQty = qty;//(TextBox)quantity inside the detail modal
                                $('#txtViewQuantity').val(qty);
                                $('#titleModal').text(productName);
                                var html = '';
                                $(response).each(function (index) {
                                    html += '<li id="' + this.ID + '" data-customer="' + this.CustomerId + '" class="card"><div class="row"><div class="col-xs-5"><div class="text-center m-t-10"><label class="label">' + this.BillNo + '</label></div><div class="text-center text-muted">Dated :<label class="text-muted m-b-0">' + this.ReturnDateString + '</label></div></div><div class="col-xs-7"><small>Tax :<label>' + this.TaxAmount + '</label></small><small class="pull-right">Gross :<label>' + this.Gross + '</label></small><div >Net Amount <br /><label class="m-b-0">' + this.NetAmount + '</label></div><div><label class="badge badge-info">' + this.TotalItems + '</label><a href="#" id="clickBill" class="btn btn-sm">View Bill</a></div></div></div>';

                                })
                                $('#listItems').children().remove();
                                $('#listItems').append(html);

                                //click of list item
                                $('#viewModal').off().on('click', '#clickBill', function () {
                                    clickBit = true;//for validating selected entry
                                    var SretdId = 0;
                                    var SretId = $(this).closest('li').attr('id');
                                    var stockQty;//Quantity from DB
                                    var data = $.cookie('bsl_2');
                                    var li = $(this).closest('li');
                                    var customerId = $(this).closest('li').data('customer');//return customer id for identiy the bill from sales return or damage entry
                                    var type = customerId == 0 ? 1 : 0;//type=0 sales return damage[cusId!=0]; type=1 damage entry[cusId=0] 

                                    if (SretId != '0' && SretId != '') {
                                        $.ajax({
                                            url: $(hdApiUrl).val() + 'api/PurchaseReturn/GetSalesReturnDetails?ID=' + SretId + '&type=' + type,
                                            method: 'POST',
                                            data: JSON.stringify(data),
                                            contentType: 'application/json',
                                            dataType: 'JSON',
                                            success: function (register) {

                                                $('#listDetailTable tbody').children().remove();
                                                html = '';
                                                $(register[0].Products).each(function (index) {

                                                    html += ItemId == this.ItemID && instanceId == this.InstanceId ? '<tr style="background-color: #fff;border: 2px solid #7fd7ff;cursor: pointer;">' : '<tr>';
                                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                                    html += '<td style="display: none" >' + this.ItemID + '</td>';
                                                    html += '<td>' + this.Name + '</td>';
                                                    html += '<td>' + this.ItemCode + '</td>';
                                                    html += '<td style="text-align:right">' + this.CostPrice + '</td>';
                                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                                    html += '<td style="display: none">' + this.DetailsID + '</td>';
                                                    html += this.ItemID == ItemId && instanceId == this.InstanceId ? '<td><a id="addToList" href="#"><i class="fa fa-check hidden"></i></a></td>' : '';
                                                    html += '</tr>';
                                                })
                                                $('#listDetailTable tbody').children().remove();
                                                $('#listDetailTable tbody').append(html);
                                                $('#listItems').children('li').removeClass('item-list-active');
                                                li.addClass('item-list-active');

                                                //tr click for select from list  {modal}
                                                $('tr').one().click(function () {
                                                    tr = $('tbody').children('tr');
                                                    for (var i = 0; i < tr.length; i++) {
                                                        $('tr').children('td:nth-child(11)').children('a').children().addClass('hidden');
                                                    }
                                                    $(this).children('td:nth-child(11)').children('a').children().removeClass('hidden');
                                                    var checkTick = $(this).children('td:nth-child(10)').text();//check tick is sretd_id for validating ticked one from list
                                                    $('#viewModal').off().on('click', '#btnSelect', function () {

                                                        if (clickBit) {
                                                            var html2 = '';
                                                            var viewQty = $('#txtViewQuantity').val();
                                                            var qtyCheck = false;
                                                            $(register[0].Products).each(function (index) {

                                                                if (this.ItemID == ItemId && checkTick == this.DetailsID) {
                                                                    stockQty = this.Quantity;
                                                                    instanceId = this.InstanceId;
                                                                    var Name = this.Name;
                                                                    var itemCode = this.ItemCode;
                                                                    var taxPer = this.TaxPercentage;
                                                                    var Mrp = this.MRP;
                                                                    var CostPrice = this.CostPrice;
                                                                    var tax = this.TaxAmount;
                                                                    var Gross = this.Gross;
                                                                    var net = this.NetAmount;
                                                                    var IsNegBillingAllowed = JSON.parse($('#hdSettings').val()).AllowNegativeBilling;
                                                                    html2 += '<tr><td style="display:none">' + instanceId + '</td><td>' + Name + '</td><td>' + itemCode + '</td><td style="text-align:right">' + taxPer + '</td><td style="text-align:right">' + Mrp + '</td><td style="text-align:right"><input type="number" class="edit-value" value="' + CostPrice + '"/></td><td style="text-align:right" contenteditable="true" class="numberonly">' + viewQty + '</td><td style="text-align:right">' + tax + '</td><td style="text-align:right">' + Gross + '</td><td style="text-align:right">' + net + '</td><td class="hidden">1</td><td style="text-align:right">Damage</td><td style="display:none">' + this.DetailsID + '</td> <td style="display:none">' + this.ItemID + '</td><td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
                                                                    if (!IsNegBillingAllowed && viewQty <= this.Quantity && viewQty !== '' && viewQty > 0) {

                                                                        var ItemExist = false;
                                                                        var tbody = $('#listTable > tbody');
                                                                        var tr = tbody.children('tr');
                                                                        for (var i = 0; i < tr.length; i++) {
                                                                            var ID = $(tr[i]).children('td:nth-child(1)').text();
                                                                            var detail = $(tr[i]).children('td:nth-child(13)').text();
                                                                            if (instanceId == ID && detail == this.DetailsID) {

                                                                                ItemExist = true;
                                                                                var existingQty = parseFloat($(tr[i]).children('td:nth-child(7)').text());
                                                                                var newQty = existingQty + parseFloat(viewQty);

                                                                                //Quantity check for add to list
                                                                                if (stockQty < newQty) {
                                                                                    errorAlert('Quantiy Exceeds');

                                                                                }
                                                                                else {

                                                                                    successAlert('Quantity Updated');
                                                                                    $('#viewModal').modal('hide');
                                                                                    $(tr[i]).children('td:nth-child(7)').text(parseFloat(newQty));
                                                                                    sessionStorage.removeItem('tempItem');
                                                                                    $('#txtQuantity').val('');
                                                                                    $('#txtChooser').val('');
                                                                                    $('#txtChooser').focus();
                                                                                }

                                                                            }
                                                                        }
                                                                        if (!ItemExist) {
                                                                            $('#listTable > tbody').append(html2);
                                                                            sessionStorage.removeItem('tempItem');
                                                                            clickBit = false;
                                                                            $('#txtQuantity').val('');
                                                                            $('#txtChooser').val('');
                                                                            $('#txtChooser').focus();
                                                                            $('#viewModal').modal('hide');
                                                                            //Binding Event to remove button
                                                                            $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });

                                                                        }





                                                                    }
                                                                    else {
                                                                        errorAlert('Check Quantiy');
                                                                    }
                                                                }
                                                            });
                                                            //Quantity check for add to list

                                                        }
                                                        else {
                                                            errorAlert('Select An Entry');

                                                        }

                                                    });
                                                });


                                            },
                                            error: function (xhr) {
                                                alert(xhr.responseText);
                                                console.log(xhr);
                                            }
                                        });
                                    }


                                });
                            }


                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });

                }


            }
            //Add Item to list
            $('#btnAdd').click(function () {
                var qty = $('#txtQuantity').val();
                var item = $('#txtChooser').val();
                if (qty !== '' && qty !== 0 && item !== '') {
                    AddToList();
                }

            });

            //Add Item to list with enter key           
            $('#txtQuantity').keypress(function (e) {
                var qty = $('#txtQuantity').val();
                var item = $('#txtChooser').val();
                if (qty !== '' && qty !== 0 && item !== '') {
                    if (e.which == 13) {
                        e.preventDefault();
                        AddToList();
                    }
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
                        var dueDate = $('#txtDueDate').val();
                        var entryDate = $('#txtEntryDate').val();
                        var narration = $('#txtNarration').val();
                        var rOff = $('#txtRoundOff').val();
                        var LocationId = $.cookie('bsl_2');
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var createdBy = $.cookie('bsl_3');
                        var address = [];
                        var addressdetail = {};
                        addressdetail.Salutation = $('#ddlSalutation').val();
                        addressdetail.ContactName = $('#txtContactName').val();
                        addressdetail.Address1 = $('#txtAddress1').val();
                        addressdetail.Address2 = $('#txtAddress2').val();
                        addressdetail.City = $('#txtCity').val();
                        addressdetail.StateID = $('#ddlStates').val();
                        addressdetail.CountryID = $('#ddlCountry').val();
                        addressdetail.Zipcode = $('#txtZipCode').val();
                        addressdetail.Phone1 = $('#txtPhone1').val();
                        addressdetail.Phone2 = $('#txtPhone2').val();
                        addressdetail.Email = $('#txtEmail').val();
                        address.push(addressdetail);
                        //var priority = $('#ChkPriority').is(':checked');
                        //Additional Details tab data
                        var TandC = $('#dvTandC').summernote('code');
                        var PaymentTerms = $('#dvPaymentTerm').summernote('code');
                        for (var i = 0; i < tr.length; i++) {

                            var instanceId = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(7)').text();
                            var returnFrom = $(tr[i]).children('td:nth-child(11)').text();
                            var itemId = $(tr[i]).children('td:nth-child(14)').text();
                            var rate = $(tr[i]).children('td:nth-child(6)').children('input').val();
                            var detail = { "Quantity": qty, "CreatedBy": createdBy, "ReturnFrom": returnFrom };
                            detail.ItemID = itemId
                            detail.InstanceId = instanceId;
                            detail.CostPrice = rate;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.BillingAddress = address;
                        data.SupplierId = $('#ddlSupplier').val();
                        data.DueDate = dueDate;
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.EntryDate = entryDate;
                        data.DueDateString = dueDate;
                        data.EntryDateString = entryDate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.LocationId = $('#ddlLocation').val();
                        data.ReturnType = $('#ddlReason').val();
                        data.CreatedBy = $.cookie('bsl_3');
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.UserId = $.cookie('bsl_3');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.LocationId = $.cookie('bsl_2');
                        data.RoundOff = rOff;
                        //Adding Additional data to Data Array
                        data.TermsandConditon = TandC;
                        data.Payment_Terms = PaymentTerms;

                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/PurchaseReturn/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    resetRegister();
                                    $('#lblOrderNumber').text(response.Object.OrderNo);
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

            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaseReturn/get/' + id,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        try {
                            var html = '';
                            var register = response;
                            LoadAddress(register.SupplierId);
                            $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                            //$('#ChkPriority').prop('checked', register.Priority);
                            $('#ddlSupplier').val(register.SupplierId);
                            $('#ddlSupplier').select2('val', register.SupplierId);
                            $('#ddlReason').val(register.ReturnType);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);
                            $('#txtNarration').val(register.Narration);
                            if (isClone) {
                                $('#hdId').val('0');
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                                $('#btnPrint').hide();
                                $('#btnMail').hide();
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                            }
                            else {
                                $('#hdId').val(register.ID);
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                $('#btnPrint').show();
                                $('#btnMail').show();
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                            }

                            html = '';
                            $(register.Products).each(function () {
                                console.log(this.CostPrice);
                                html += '<tr>';
                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                html += '<td>' + this.Name + '</td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                html += '<td style="text-align:right" contenteditable="true" class="numberonly">' + this.Quantity + '</td>';
                                html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                html += '<td style="text-align:right">' + this.Gross + '</td>';
                                html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                html += '<td class="hidden">' + this.ReturnFrom + '</td>';
                                html += this.ReturnFrom == 0 ? '<td style="text-align:right">Stock</td>' : '<td>Damage</td>';
                                html += '<td style="display:none">' + this.DetailsID + '</td>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                html += '</tr>';
                            });
                            $('#lblGross').val(register.Gross);
                            $('#lblOrderNumber').text(register.BillNo);
                            $('#lblTotalAmount').val(register.Gross);
                            $('#lblTaxAmount').val(register.Gross);
                            $('#lblNetAmount').val(register.NetAmount);
                            $('#listTable tbody').append(html);
                            $('#ddlSupplier').prop('disabled', true);
                            $('#findModal').modal('hide');
                            $('#txtContactName').val(register.BillingAddress[0].ContactName);
                            $('#txtPhone1').val(register.BillingAddress[0].Phone1);
                            $('#txtPhone2').val(register.BillingAddress[0].Phone2);
                            $('#txtEmail').val(register.BillingAddress[0].Email);
                            $('#hdEmail').val(register.BillingAddress[0].Email);
                            $('#ddlSalutation').val(register.BillingAddress[0].Salutation);
                            $('#txtZipCode').val(register.BillingAddress[0].Zipcode);
                            $('#txtAddress1').val(register.BillingAddress[0].Address1);
                            $('#txtAddress2').val(register.BillingAddress[0].Address2);
                            $('#txtCity').val(register.BillingAddress[0].City);
                            $('#ddlCountry').val(register.BillingAddress[0].CountryID);
                            loadStates(register.BillingAddress[0].StateID);
                            //Fetching and binding data for Additional Detail Tab
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
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        loading('stop', null);
                    },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });


            }

            $('#btnPrint').click(function () {

                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/purchase/Print/" + type + "/" + number + "/Return?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'PurchaseReturn', 800, 700);
                }
            });

            function loadStates(state) {
                var selectedstate = state | 0;
                var selected = $('#ddlCountry').val();
                if (selected == "0") {
                    $('#ddlStates').empty();
                    $('#ddlStates').append('<option value="0">--Select--</option>');
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
                            $('#ddlStates').empty();
                            $('#ddlStates').append('<option value="0">--Select--</option>');
                            $.each(data, function () {
                                $("#ddlStates").append($("<option/>").val(this.StateId).text(this.State));
                            });
                            $("#ddlStates").val(selectedstate);
                        },
                        failure: function () {
                            console.log("Error")
                        }
                    });
                }
            }

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
                        if (response.length > 0) {
                            $(response).each(function (index) {
                                html += '<ul class="address-lists list-unstyled m-t-20">';
                                html += '<li title="click to change Supplier address"><span class="hidden id">' + this.ID + '</span>';
                                html += '<p><b><span class="salutation">' + this.Salutation + '</span> <span class="sup-name">' + this.Name + '</span></b>';
                                if (this.Address1 != "") {
                                    html += '<p><span class="address1">' + this.Address1 + '</span></p>';
                                }
                                if (this.Address2 != "") {
                                    html += '<p><span class="address2">' + this.Address2 + '</span></p>';
                                }
                                if (this.City != "") {
                                    html += '<p><span class="city">' + this.City + '</span></p> ';
                                }
                                if (this.StateId != 0) {
                                    html += '<p><span class="state-id hidden">' + this.StateId + '</span><span class="state">' + this.State + '</span></p>';
                                }
                                html += '<p><span class="country-id hidden">' + this.CountryId + '</span><span class="country">' + this.Country + '</span></p>';
                                if (this.ZipCode != '') {
                                    html += '<p> <span class="zipcode">' + this.ZipCode + '</span></p>';
                                }
                                if (this.Phone1 != "") {
                                    html += '<p><b>Ph: </b><span class="phone1">' + this.Phone1 + '</span>, ';
                                }
                                if (this.Phone2 != "") {
                                    html += '<span class="phone2">' + this.Phone2 + '</span></p>';
                                }
                                if (this.Email != "") {
                                    html += '<span class="email">' + this.Email + '</span></p>';
                                }
                                html += '</li>';
                                html += '</ul>';
                            });
                        }
                        else {
                            html += '<ul class="address-lists list-unstyled m-t-20">No Other Contacts found</ul>';
                        }

                        $('.address-tab').append(html);

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            $('#ddlSupplier').change(function () {
                var id = $('#ddlSupplier').val();
                if (id != 0) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Suppliers/Get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            if (response.Contact_Name != null) {
                                $('#txtContactName').val(response.Contact_Name);
                            }
                            else {
                                $('#txtContactName').val(response.Name);
                            }
                            $('#txtPhone1').val(response.Phone1);
                            $('#txtPhone2').val(response.Phone2);
                            $('#txtAddress1').val(response.Address1);
                            $('#txtAddress2').val(response.Address2);
                            $('#txtCity').val(response.City);
                            //alert(response.Salutation);
                            $('#ddlCountry').val(response.CountryId);
                            loadStates(response.StateId);
                            $('#ddlSalutation').val(response.Salutation);
                            $('#txtZipCode').val(response.ZipCode);
                            LoadAddress(id);
                            $('.address-tab').addClass('hidden');

                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }
            });



            $('.change-address').click(function () {
                $('.address-tab').removeClass('hidden');
            });

            $(document).on('click', '.address-lists li', function () {
                var addressid = $(this).closest('li').children('.id').text();
                var Salutation = $(this).closest('li').find('.salutation').text();
                var Name = $(this).closest('li').find('.sup-name').text();
                var address1 = $(this).closest('li').find('.address1').text();
                var address2 = $(this).closest('li').find('.address2').text();
                var City = $(this).closest('li').find('.city').text();
                var ZipCode = $(this).closest('li').find('.zipcode').text();
                var Phone1 = $(this).closest('li').find('.phone1').text();
                var Phone2 = $(this).closest('li').find('.phone2').text();
                var Email = $(this).closest('li').find('.email').text();
                var countryID = $(this).closest('li').find('.country-id').text();
                if (countryID == undefined || countryID == '') {
                    countryID = 0;
                }
                var StateID = $(this).closest('li').find('.state-id').text();
                if (StateID == undefined || StateID == '') {
                    StateID = '0';
                }
                $('#ddlSalutation').val(Salutation);
                $('#txtContactName').val(Name);
                $('#txtAddress1').val(address1);
                $('#txtAddress2').val(address2);
                $('#txtCity').val(City);
                $('#txtZipCode').val(ZipCode);
                $('#txtPhone1').val(Phone1);
                $('#txtPhone2').val(Phone2);
                $('#txtEmail').val(Email);
                $('#ddlCountry').val(countryID);
                loadStates(StateID);
                $('.address-tab').addClass('hidden');
            });

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
                        //inititalize select 2 ddl
                        $("#ddlSupplierFilter").select2({
                            width: '100%'
                        });

                        // Apply Filter Click
                        $('#applyFilter').click(function () {
                            //Filter Logic Here
                            var supplierId = $('#ddlSupplierFilter').val() == '0' ? null : $('#ddlSupplierFilter').val();
                            var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                            var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                            refreshTable(supplierId, fromDate, toDate);
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
                        url: $('#hdApiUrl').val() + 'api/purchaseReturn/GetFilterDetails?SupplierId=' + supplierID + '&from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.BillNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.Supplier + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                //html += this.Priority ? '<td><span class="label label-danger">High</span></td>' : '<td><span class="label label-success">Normal</span></td>';
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
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                            miniLoading('save');
                        },
                        beforeSend: function () { miniLoading('start') },
                        complete: function () { miniLoading('stop') }
                    });
                };
            });

            $('#btnMail').click(function () {
                $('#txtMailAddress').val($('#hdEmail').val());

            });

            $(document).on('click', '#btnSend', function (e) {
                $('.before-send').fadeOut('slow', function () {
                    $('.after-send').fadeIn();
                    var toaddr = $('#txtMailAddress').val();
                    var id = $('#hdId').val();
                    var modifiedBy = $.cookie('bsl_3');
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/PurchaseReturn/SendMail?purchaseid=' + id + '&toaddress=' + toaddr + '&userid=' + $.cookie('bsl_3'),
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify($(location).attr('protocol') + '//' + $(location).attr('host') + '/Purchase/Print/' + type + '/' + number + '/Return?id=' + id + '&location=' + $.cookie('bsl_2')),
                        success: function (response) {
                            if (response.Success) {
                                successAlert(response.Message);
                                $("#modalMail").modal('hide');
                                resetRegister();
                            }
                            else {
                                errorAlert(response.Message);
                                $("#modalMail").modal('hide');
                            }
                        },
                        error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                    });
                });
            });


            //BtnNew starts here 
            $('#btnNew').click(function () {
                resetRegister();
            });

            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#ddlSupplier').select2('val', 0);
                $('#txtEntryDate').datepicker('setDate', today);
                $('#ddlSupplier').prop('disabled', false);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#btnPrint').hide();
                LoadTandCSettings();
                $('#btnMail').hide();
                $('#ddlCostCenter').select2('val', 0);
                $('#ddlJob').select2('val', 0);
            }//Reset ends here


            $('#ddlCountry').change(function () {
                loadStates(0);
            })

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
                                url: $('#hdApiUrl').val() + 'api/PurchaseReturn/delete/' + id,
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
                                    alert(xhr.responseText);
                                    console.log(xhr);
                                    miniLoading('stop');
                                },
                                beforeSend: function () { miniLoading('start') },
                                complete: function () { miniLoading('stop') }
                            });
                        }


                    });
                }

            });
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
