<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Return.aspx.cs" Inherits="BisellsERP.Sales.Return" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sales New Return</title>
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

        .round-no-border {
            border-radius: 4px;
            border: none;
            background-color: #ECEFF1;
        }

        .panel {
            margin-bottom: 10px;
        }

        tbody tr td {
            padding: 5px;
            font-size: smaller;
        }

        .quote-date {
            text-align: right;
            border: none;
            color: #014d6f;
            cursor: pointer;
            font-size: smaller;
            width: 50%;
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
            height: 80px;
        }

            .item-list > .card .col-xs-5, .item-list > .card .col-xs-7 {
                height: 70px;
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
                color: #616161;
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
        <%--<asp:HiddenField runat="server" Value="0" ID="hdId" ClientIDMode="Static" />--%>
        <input id="hdId" type="hidden" value="0" />
        <input id="hdEmail" type="hidden" value="0" />
        <%-- ---- Page Title ---- --%>
        <div class="row p-b-5">
            <div class="col-sm-4">
                <h3 class="page-title m-t-0">Credit Note</h3>
            </div>
            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group">
                    <button type="button" accesskey="v" id="btnFind" data-toggle="tooltip" data-placement="bottom" title="View previous credit note" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                    <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new credit note. Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                    <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current credit note" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                    <button type="button" id="btnPrint" accesskey="p" data-toggle="tooltip" data-placement="bottom" title="Print the current credit note" class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i></button>
                    <button id="btnMail" type="button" class="btn btn-default waves-effect waves-light" data-toggle="modal" data-target="#modalMail"><i class="icon ion-chatbox"></i>&nbsp;Mail</button>
                    <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion ion-trash-b"></i></button>
                </div>
                <%--<div class="pull-right form-inline">
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
                </div>--%>
            </div>
        </div>

        <%-- ---- Search Quote Panel ---- --%>
        <div class="row search-quote-panel">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="panel b-r-5">
                            <div class="panel-body">
                                <div class="col-sm-8">
                                    <label class="title-label">Add  to list from here..</label>
                                    <%--<asp:TextBox ID="txtChooser" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Choose Item"></asp:TextBox>--%>
                                    <input type="text" id="txtChooser" autocomplete="off" class="form-control" placeholder="Choose Item" />
                                    <div id="lookup">
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                                    <%--<asp:TextBox ID="txtQuantity" TextMode="Number" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Qty"></asp:TextBox>--%>
                                      <input type="text" id="txtQuantity" autocomplete="off" class="form-control" placeholder="Qty" />
                                </div>
                                <div class="col-sm-2">
                                    <div class="row text-center">
                                        <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List" class="btn btn-icon btn-primary"><i class="ion-plus"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-5">
                        <div class="panel b-r-5">
                            <div class="panel-body">
                                <div class="col-sm-7">
                                    <label class="title-label">Customer:</label>
                                    <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-sm-5">
                                    <label class="title-label">Reasons</label>
                                    <select class="form-control" id="ddlReason">
                                        <option value="0">Damage</option>
                                         <option value="1">Wrong Supply</option>
                                    </select>
                                 <%--   <asp:DropDownList ID="ddlReason" ClientIDMode="Static" CssClass="form-control" runat="server">--%>
                                      <%-- 
                                        <asp:ListItem Value="0">Damage</asp:ListItem>
                                        <asp:ListItem Value="1">Wrong Supply</asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="panel b-r-8">
                            <div class="panel-body p-t-5 p-b-5">
                                <div class="row m-t-10">
                                    <div class="col-sm-12">
                                        <span>Order No : <b></b></span>
                                        <asp:Label ID="lblOrderNo" runat="server" ClientIDMode="Static" class="badge badge-danger pull-right"><b>KU1368B</b></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="row m-b-10">
                                    <div class="col-sm-12">
                                        <span>Date : </span>
                                  <%--      <asp:TextBox ID="txtEntryDate" runat="server" ClientIDMode="Static" Text="21/Dec/2017" CssClass="quote-date"></asp:TextBox>--%>
                                        <input type="text" id="txtEntryDate" class="quote-date" />
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
                <div class="panel panel-default view-h b-r-10">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th class="hidden">Detail_id</th>
                                            <th class="hidden">Sed_Id</th>
                                            <th>Item </th>
                                            <th>Code</th>
                                            <th style="text-align: right">Tax%</th>
                                            <th style="text-align: right">MRP</th>
                                            <th style="text-align: right">Rate</th>
                                            <th style="text-align: right">Qty</th>
                                            <th style="text-align: right">Tax</th>
                                            <th style="text-align: right">Gross</th>
                                            <th style="text-align: right">Net</th>
                                            <th style="display: none">InstanceId</th>
                                            <th style="display: none">ItemId</th>
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
                           <%-- <asp:Label ID="lblTotalItem" CssClass="l-font" ClientIDMode="Static" runat="server" Text="0"></asp:Label>--%>
                            <span id="lblTotalItem" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Gross </label>
                            <%-- Gross Amount --%>
                            <%--<asp:Label ID="lblGross" ClientIDMode="Static" CssClass="l-font" runat="server" Text="0"></asp:Label>--%>
                            <span id="lblGross" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">

                            <label class="w-100 light-font-color">Total </label>
                            <%-- Total Amount --%>
                        <%--    <asp:Label ID="lblTotal" CssClass="l-font" ClientIDMode="Static" runat="server" Text="0"></asp:Label>--%>
                               <span id="lblTotal" class="l-font"></span>
                        </div>
                        <div class="col-sm-2 text-center">
                            <label class="w-100 light-font-color">Tax </label>
                            <%-- Tax Amount --%>
                         <%--   <asp:Label ID="lblTax" CssClass="l-font" runat="server" ClientIDMode="Static" Text="0"></asp:Label>--%>
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
                                <%--<asp:TextBox ID="txtNarration" CssClass="w-100" TextMode="MultiLine" Rows="3" ClientIDMode="Static" runat="server" placeholder="Enter narration here.."></asp:TextBox>--%>
                                <input type="text" id="txtNarration" class="w-100" placeholder="Enter narration here.." />
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
                           <%-- <asp:Label ID="lblNet" ClientIDMode="Static" runat="server" CssClass="counter" Text="XX"></asp:Label>--%>
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
            <div class="modal-content ">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Credit Notes &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
                    <div id="filterWrap" class="hide">
                        <label>Filter by Customer :</label>
                        <asp:DropDownList ID="ddlCustomerFilter" ClientIDMode="Static" CssClass="" runat="server"></asp:DropDownList>
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
                                <th>Customer</th>
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
                                                Net 
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
                                            <th style="text-align: right;">Rate</th>
                                            <th style="text-align: right;">Qty</th>
                                            <th style="text-align: right;">Tax</th>
                                            <th style="text-align: right;">Gross</th>
                                            <th style="text-align: right;">Net </th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                            <div class="col-xs-4 col-xs-offset-5">
                               <%-- <asp:TextBox ID="txtViewQuantity" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Qty"></asp:TextBox>--%>
                                <input type="text" id="txtViewQuantity" class="form-control" placeholder="Qty" />
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
                            <h5 class="sett-title p-t-0">Billing Address<a href="#" class="pull-right change-address"><small class="text-green"><i class="fa fa-edit"></i>&nbsp;change</small></a></h5>

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
                                                <%--<input type="text" class="form-control input-sm" />--%>
                                                <textarea class="form-control" id="txtAddress1"></textarea>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Contact Address 2</label>
                                            <div class="col-sm-8">
                                                <%--<input type="text" class="form-control input-sm" />--%>
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
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Email</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtEmail" class="form-control" />
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
                var qty = parseFloat($(tr[i]).children('td:nth-child(8)').text());

                var gross = parseFloat($(tr[i]).children('td:nth-child(8)').text()) * parseFloat($(tr[i]).children('td:nth-child(7)').text());
                gross = parseFloat(gross);//Gross Amount

                var taxper = parseFloat($(tr[i]).children('td:nth-child(5)').text());
                var tax = parseFloat($(tr[i]).children('td:nth-child(7)').text()) * (taxper / 100);
                tax = parseFloat(tax);//Tax Amount

                var net = gross + (tax * qty);
                net = parseFloat(net);//Net amount
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax.toFixed(2));
                $(tr[i]).children('td:nth-child(10)').text(gross.toFixed(2)); //gross amount
                $(tr[i]).children('td:nth-child(9)').text(tempTax.toFixed(2));  //tax amount
                $(tr[i]).children('td:nth-child(11)').text(net.toFixed(2));  //net amount
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
                qty = parseFloat($(tr[i]).children('td:nth-child(8)').text());
                tax += parseFloat($(tr[i]).children('td:nth-child(9)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(10)').text());
                net += parseFloat($(tr[i]).children('td:nth-child(11)').text());

            }
            temp = net;
            if (JSON.parse($('#hdSettings').val()).AutoRoundOff) {
                var roundoff = Math.round(net) - net;
                net = Math.round(net);
                roundoff = parseFloat(roundoff);
                $('#txtRoundOff').val(roundoff.toFixed(2));
            }
            else {
                var roundoff = parseFloat($('#txtRoundOff').val());
                net = net + parseFloat($('#txtRoundOff').val());
                net = net;
            }

            var le = tr.length;
            $('#lblTotalItem').text(le);

            tax = parseFloat(tax.toFixed(2));
            gross = parseFloat(gross.toFixed(2));
            $('#lblGross').text(gross);
            $('#lblNet').text(net.toFixed(2));
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
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Jobs/GetCustomerwiseJob/' + job,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            console.log(response);
                            $('#ddlJob').children('option').remove();
                            $('#ddlJob').append('<option value="0">--select--</option>');
                            $('#ddlJob').select2('val', 0);
                            $(response).each(function () {
                                $('#ddlJob').append('<option value="' + this.ID + '">' + this.JobName + '</option>');
                                $('#ddlJob').select2('val', this.ID);
                                $('#ddlCustomer').select2('val', this.CustomerId);
                            });

                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
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
                    $('#btnPrint').hide();
                    $('#btnMail').hide();
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                    $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                }
            }
            //lookup initialization 
            var companyId = $.cookie('bsl_1');
            lookup({
                textBoxId: 'txtChooser',
                url: $('#hdApiUrl').val() + 'api/search/ItemsFromSalesCustomerWise?CompanyId=' + companyId + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'SellingPrice'],
                visibility: [false, false, true, true, true, true, true],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate'],
                key: 'ItemID',
                dataToShow: 'Name',
                ddl1Id: 'ddlCustomer',
                ddl1Param: 'customerid',
                OnLoading: function () { miniLoading('start') },
                OnComplete: function () { miniLoading('stop') }
            });
            //lookup initialization ends here

            $('[data-toggle="popover"]').popover({
                content: "<textarea placeholder=\" Enter Narration Here\"></textarea>"
            });
            //function for date picker
            $('#txtEntryDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });


            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtEntryDate').datepicker('setDate', today);
            var billNo = $('#ddlBillno').val();

            function populateJobs(jobId, changeAddress) {
                var id = $('#ddlCustomer').val();
                if (id != 0) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Customers/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            console.log(response);
                            if (changeAddress) {
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
                            }
                            $('#ddlJob').children('option').remove();
                            $('#ddlJob').append('<option value="0">--select--</option>');
                            $('#ddlJob').select2('val', 0);
                            $(response.Jobs).each(function () {
                                $('#ddlJob').append('<option value="' + this.ID + '">' + this.JobName + '</option>');
                            });
                            $('#ddlJob').select2('val', jobId);
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }
                else {
                    $('#ddlCustomer').off('change');

                }
            }

            $('#ddlCustomer').off().change(function (e) {
                populateJobs(e.selectedJob | '0', true);
            });
            function AddToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var taxper = parseFloat(tempItem.TaxPercentage);
                var rate = parseFloat(tempItem.SellingPrice);
                var qty = parseFloat($('#txtQuantity').val());
                var TaxAmount = parseFloat(rate * (taxper / 100)).toFixed(2);
                var GrossAmount = parseFloat((qty * rate).toFixed(2));
                var NetAmount = parseFloat(GrossAmount + (TaxAmount * qty)).toFixed(2);
                var tempTax = (TaxAmount * qty).toFixed(2);

                var data = $.cookie('bsl_2');
                customerId = $('#ddlCustomer').val();
                ItemId = tempItem.ItemID;
                InstId = tempItem.InstanceId;
                var response;
                $.ajax({
                    url: $(hdApiUrl).val() + 'api/SalesReturn/GetSalesEntry?customerId=' + customerId + '&ItemId=' + ItemId + '&InstanceId=' + InstId,
                    method: 'POST',
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    dataType: 'JSON',
                    success: function (response) {
                        $('#viewModal').modal('show');

                        if (response !== '') {
                            $('#listDetailTable tbody').children().remove();
                            $('#txtViewQuantity').val(qty);
                            var clickBit = false;
                            var viewQty = qty;//(TextBox)quantity inside the detail modal
                            var productName = $('#txtChooser').val();

                            $('#titleModal').text(productName);
                            var html = '';
                            $(response).each(function (index) {
                                html += ' <li id="' + this.ID + '" class="card"><div class="row"><div class="col-xs-5"><div class="text-center m-t-10"><label class="label">' + this.SalesBillNo + '</label></div><div class="text-center text-muted">Dated :<label class="text-muted m-b-0">' + this.EntryDateString + '</label></div></div><div class="col-xs-7"><small>Tax :<label>' + this.TaxAmount + '</label></small><small class="pull-right">Gross :<label>' + this.Gross + '</label></small><div >Net Amount <br /><label class="m-b-0">' + this.NetAmount + '</label></div><div><label class="badge badge-info">' + this.TotalItems + '</label><a href="#" id="clickBill" class="btn btn-sm">View Bill</a></div></div></div>';

                            })
                            $('#listItems').children().remove();
                            $('#listItems').append(html);

                            //click of list item
                            $('#viewModal').off().on('click', '#clickBill', function () {
                                clickBit = true;//for validating selected entry
                                var stockQty;//Quantity from DB
                                var sed_id;
                                var Se_Id = $(this).closest('li').attr('id');
                                var li = $(this).closest('li');
                                var data = $.cookie('bsl_2');
                                if (Se_Id !== '0' && Se_Id !== '') {
                                    $.ajax({
                                        url: $(hdApiUrl).val() + 'api/SalesReturn/GetSalesEntryDetails?ID=' + Se_Id,
                                        method: 'POST',
                                        data: JSON.stringify(data),
                                        contentType: 'application/json',
                                        dataType: 'JSON',
                                        success: function (register) {
                                            $('#listDetailTable tbody').children().remove();
                                            html = '';
                                            $(register[0].Products).each(function (index) {
                                                html += this.ItemID == ItemId ? '<tr  style="background-color: #fff;border: 2px solid #7fd7ff;cursor: pointer;">' : '<tr>';
                                                html += '<td style="display:none">' + this.SedId + '</td>';
                                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                                html += '<td>' + this.Name + '</td>';
                                                html += '<td>' + this.ItemCode + '</td>';
                                                html += '<td contenteditable="true" class="numberonly" style="text-align:right;">' + this.SellingPrice + '</td>';
                                                html += '<td style="text-align:right;" contenteditable="true" class="numberonly">' + this.Quantity + '</td>';
                                                html += '<td style="text-align:right;">' + this.TaxAmount + '</td>';
                                                html += '<td style="text-align:right;">' + this.Gross + '</td>';
                                                html += '<td style="text-align:right;">' + this.NetAmount + '</td>';
                                                html += this.InstanceId == InstId ? '<td><a id="addToList" href="#"><i class="fa fa-check hidden"></i></a></td>' : '';
                                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                                html += '</tr>';

                                            })
                                            $('#listDetailTable tbody').children().remove();
                                            $('#listDetailTable tbody').append(html);
                                            $('#listItems').children('li').removeClass('item-list-active');
                                            li.addClass('item-list-active');
                                            //tr click for select from list  {modal}
                                            $('tr').off().click(function () {

                                                tr = $('tbody').children('tr');
                                                for (var i = 0; i < tr.length; i++) {
                                                    $('tr').children('td:nth-child(10)').children('a').children().addClass('hidden');
                                                }
                                                $(this).children('td:nth-child(10)').children('a').children().removeClass('hidden');
                                                var checkTick = $(this).children('td:nth-child(1)').text();//check tick is sed_id for validating ticked one from list

                                                //select items to list
                                                $('#viewModal').on('click', '#btnSelect', function () {
                                                    if (clickBit) {
                                                        $('#ddlCustomer').attr("disabled", "true");

                                                        var viewQty = $('#txtViewQuantity').val();
                                                        var html2 = '';
                                                        var qtyCheck = false;

                                                        $(register[0].Products).each(function (index) {

                                                            //finding item in the Modal for add to list
                                                            if (this.ItemID == ItemId && checkTick == this.SedId && this.InstanceId == InstId) {
                                                                stockQty = this.Quantity;
                                                                sed_id = this.SedId;
                                                                var Name = this.Name;
                                                                var itemCode = this.ItemCode;
                                                                var taxPer = this.TaxPercentage;
                                                                var Mrp = this.MRP;
                                                                var sellingPrice = this.SellingPrice;
                                                                var tax = this.TaxAmount;
                                                                var Gross = this.Gross;
                                                                var net = this.NetAmount;
                                                                html2 = '<tr><td style="display:none">' + sed_id + '</td><td style="display:none">0</td><td>' + Name + '</td><td>' + itemCode + '</td><td style="text-align:right">' + taxPer + '</td><td style="text-align:right">' + Mrp + '</td><td contenteditable="true" class="numberonly" style="text-align:right">' + sellingPrice + '</td><td style="text-align:right"  contenteditable="true" class="numberonly">' + viewQty + '</td><td style="text-align:right">' + tax + '</td><td style="text-align:right">' + Gross + '</td><td style="text-align:right">' + net + '</td><td style="display:none">' + this.InstanceId + '</td><td style="display:none">' + this.ItemID + '</td><td>  <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
                                                                if (viewQty <= this.Quantity && viewQty !== '' && viewQty > 0) {
                                                                    //Quantity check for add to list
                                                                    var ItemExist = false;
                                                                    var tbody = $('#listTable > tbody');
                                                                    var tr = tbody.children('tr');

                                                                    for (var i = 0; i < tr.length; i++) {
                                                                        var ID = $(tr[i]).children('td:nth-child(1)').text();
                                                                        if (sed_id == ID) {

                                                                            ItemExist = true;
                                                                            var existingQty = parseFloat($(tr[i]).children('td:nth-child(8)').text());
                                                                            var newQty = existingQty + parseFloat(viewQty);
                                                                            //Quantity check for add to list
                                                                            if (stockQty < newQty) {
                                                                                errorAlert('Quantiy Exceeds');
                                                                            }
                                                                            else {
                                                                                successAlert('Quantity Updated');
                                                                                $('#viewModal').modal('hide');
                                                                                $(tr[i]).children('td:nth-child(8)').text(parseFloat(newQty));
                                                                                sessionStorage.removeItem('tempItem');
                                                                                $('#txtQuantity').val('');
                                                                                $('#txtChooser').val('');
                                                                                $('#txtChooser').focus();
                                                                            }


                                                                        }

                                                                    }

                                                                    if (!ItemExist) {
                                                                        $('#listTable > tbody').prepend(html2);
                                                                        clickBit = false;
                                                                        sessionStorage.removeItem('tempItem');
                                                                        $('#txtQuantity').val('');
                                                                        $('#txtChooser').val('');
                                                                        $('#txtChooser').focus();
                                                                        $('#viewModal').modal('hide');
                                                                        //Binding Event to remove button
                                                                        $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });

                                                                    }


                                                                }


                                                            }


                                                        });

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
            //Add Item to list
            $('#btnAdd').click(function () {
                var qty = $('#txtQuantity').val();
                var item = $('#txtChooser').val();
                if (qty !== '' && qty !== 0 && item !== '') {
                    AddToList();
                }


            });

            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/SalesReturn/get/' + id,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        try {
                            var html = '';
                            var register = response;
                            LoadAddress(register.CustomerId);
                            $('#ddlBillno').val(register.EntryId);
                            $('#lblOrderNo').text(register.BillNo);
                            $('#txtEntryDate').datepicker("update", new Date(register.ReturnDateString));
                            $('#txtCustomer').val(register.CustomerId);
                            $('#ddlReason').val(register.ReturnType);
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
                                html += '<tr>';
                                html += '<td style="display:none">' + this.SedId + '</td>';
                                html += '<td style="display:none">' + this.DetailsID + '</td>';
                                html += '<td>' + this.Name + '</td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td contenteditable="true" class="numberonly" style="text-align:right">' + this.SellingPrice + '</td>';
                                html += '<td style="text-align:right" contenteditable="true" class="numberonly">' + this.Quantity + '</td>';
                                html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                html += '<td style="text-align:right">' + this.Gross + '</td>';
                                html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';

                                html += '</tr>';
                            });
                            $('#lblGross').val(register.Gross);
                            $('#ddlBillno').val(register.EntryId);
                            $('#ddlCustomer').select2('val', register.CustomerId);
                            $('#ddlCustomer').val(register.CustomerId);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            populateJobs(register.JobId, false);
                            $('#lblOrderNumber').text(register.BillNo);
                            $('#lblTotalAmount').val(register.Gross);
                            $('#lblTaxAmount').val(register.Gross);
                            $('#ddlCustomer').attr("disabled", "true");
                            $('#lblNetAmount').val(register.NetAmount);
                            $('#listTable tbody').append(html);
                            $('#findModal').modal('hide');
                            $('#txtContactName').val(register.BillingAddress[0].ContactName);
                            $('#txtPhone1').val(register.BillingAddress[0].Phone1);
                            $('#txtPhone2').val(register.BillingAddress[0].Phone2);
                            $('#ddlSalutation').val(register.BillingAddress[0].Salutation);
                            $('#txtZipCode').val(register.BillingAddress[0].Zipcode);
                            $('#txtAddress1').val(register.BillingAddress[0].Address1);
                            $('#txtAddress2').val(register.BillingAddress[0].Address2);
                            $('#txtCity').val(register.BillingAddress[0].City);
                            $('#ddlCountry').val(register.BillingAddress[0].CountryID);
                            $('#txtEmail').val(register.BillingAddress[0].Email);
                            $('#hdEmail').val(register.BillingAddress[0].Email);
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
                        if (response.length > 0) {
                            $(response).each(function (index) {
                                html += '<ul class="address-lists list-unstyled m-t-20">';
                                html += '<li title="click to change billing address"><span class="hidden id">' + this.CustomerAddressID + '</span>';
                                html += '<p><b><span class="salutation">' + this.Salutation + '</span> <span class="cust-name">' + this.Name + '</span></b>';
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


            $('.change-address').click(function () {
                $('.address-tab').removeClass('hidden');
            });

            $(document).on('click', '.address-lists li', function () {
                var addressid = $(this).closest('li').children('.id').text();
                var Salutation = $(this).closest('li').find('.salutation').text();
                var Name = $(this).closest('li').find('.cust-name').text();
                var address1 = $(this).closest('li').find('.address1').text();
                var address2 = $(this).closest('li').find('.address2').text();
                var City = $(this).closest('li').find('.city').text();
                var ZipCode = $(this).closest('li').find('.zipcode').text();
                var Phone1 = $(this).closest('li').find('.phone1').text();
                var Phone2 = $(this).closest('li').find('.phone2').text();
                var email = $(this).closest('li').find('.email').text();
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
                $('#ddlCountry').val(countryID);
                $('#txtEmail').val(Email);
                loadStates(StateID);
                $('.address-tab').addClass('hidden');
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
                        var ReturnDate = $('#txtEntryDate').val();
                        var rOff = $('#txtRoundOff').val();
                        var narration = $('#txtNarration').val();
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

                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(8)').text();
                            var Mrp = $(tr[i]).children('td:nth-child(6)').text();
                            var Rate = $(tr[i]).children('td:nth-child(7)').text();
                            var instanceID = $(tr[i]).children('td:nth-child(12)').text();
                            var itemId = $(tr[i]).children('td:nth-child(13)').text();
                            var detail = { "Quantity": qty };
                            detail.ItemID = itemId;
                            detail.InstanceId = instanceID;
                            detail.SedId = id;
                            detail.MRP = Mrp;
                            detail.SellingPrice = Rate;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.CustomerId = $('#ddlCustomer').val();
                        data.BillingAddress = address;
                        data.ReturnDate = ReturnDate;
                        data.RoundOff = rOff;
                        data.ReturnType = $('#ddlReason').val();
                        data.ReturnDateString = ReturnDate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.CreatedBy = createdBy;
                        data.ModifiedBy = createdBy
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.LocationId = $.cookie('bsl_2');
                        //Adding Additional data to Data Array
                        data.TermsandConditon = TandC;
                        data.Payment_Terms = PaymentTerms;
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/SalesReturn/Save',
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
                            beforeSend: function () { miniLoading('start'); },
                            complete: function () { miniLoading('stop'); },
                        });
                    }

                });


            }//Save Function Ends here

            $('#btnPrint').click(function () {

                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/Sales/Prints/" + type + "/" + number + "/Return?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'PurchaseReturn', 800, 700);
                }
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
                        $("#ddlCustomerFilter").select2({
                            width: '100%'
                        });

                        // Apply Filter Click
                        $('#applyFilter').click(function () {
                            //Filter Logic Here
                            var customerId = $('#ddlCustomerFilter').val() == '0' ? null : $('#ddlCustomerFilter').val();
                            var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                            var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                            refreshTable(customerId, fromDate, toDate);
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
                function refreshTable(customerID, fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SalesReturn/Get?CustomerId=' + customerID + '&from=' + fromDate + '&to=' + toDate,
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
                                html += '<td>' + this.ReturnDateString + '</td>';
                                html += '<td>' + this.Customer + '</td>';
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
                            $('#tblRegister').off().on('click', '.edit-register', function (e) {
                                var registerId = $(this).closest('tr').children('td').eq(0).text();
                                getRegister(false, registerId);
                            });
                        },
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
                        url: $('#hdApiUrl').val() + 'api/SalesReturn/SendMail?salesId=' + id + '&toaddress=' + toaddr + '&userid=' + $.cookie('bsl_3'),
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify($(location).attr('protocol') + '//' + $(location).attr('host') + '/Sales/Prints/' + type + '/' + number + '/Return?id=' + id + '&location=' + $.cookie('bsl_2')),
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


            $('#ddlCountry').change(function () {
                loadStates(0);
            })

            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#btnPrint').hide();
                $('#btnMail').hide();
                LoadTandCSettings();
                $('#ddlCustomer').select2('val', 0);
                $('#ddlCustomer').removeAttr("disabled");
                $('#ddlCostCenter').select2('val', 0);
                $('#ddlJob').select2('val', 0);
                //Set Request Date to current date
                var date = new Date();
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#txtEntryDate').datepicker('setDate', today);
            }//Reset ends here

            //Close details modal
            $('#modalClose').on('click', function () {
                $('#viewModal').modal('hide');
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
                    }, function (isConfirm) {

                        var id = $('#hdId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/SalesReturn/delete/' + id,
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
                                beforeSend: function () { miniLoading('start'); },
                                complete: function () { miniLoading('stop'); },
                            });
                        }

                    });
                }

            });

            //Init HTML EDITOR(Summer note Editor Initilization)
            $('.additional-settings-button').click(function () {
                $('#dvPaymentTerm').summernote({
                    height: 450,
                    focus: false,
                });
                $('#dvTandC').summernote({
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
    <!-- Date Range Picker -->
   
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
</asp:Content>
