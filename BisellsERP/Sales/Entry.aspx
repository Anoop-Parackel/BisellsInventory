<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="BisellsERP.Sales.Entry" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sales New Invoice</title>

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

        .panel {
            margin-bottom: 0;
        }

        tbody tr td {
            padding: 5px !important;
            font-size: smaller;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 30px;
        }

        .edit-value {
            background-color: transparent;
            width: 40px;
            text-align: center;
        }

        .portlet .portlet-heading {
            padding: 5px;
            padding-top: 30px;
        }

        .portlet {
            margin-bottom: 5px;
        }

        .link-btn {
            padding: 2px 7px;
        }

            .link-btn i {
                font-size: 20px;
            }

        .expand-btn {
            display: inline-block;
            height: 100%;
        }

            .expand-btn a {
                display: inline-block;
            }

        .toggleName {
            margin: 3px 6px 3px 0;
        }

            .toggleName i {
                font-size: 1.2em;
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
        <%--<button type="button" id="btnPrint" accesskey="p" class="btn btn-warning btn-lg waves-effect waves-light print-float"><i class="ion ion-printer"></i></button>--%>

        <asp:HiddenField runat="server" Value="" ID="hdTandC" ClientIDMode="Static" />
        <%-- <asp:HiddenField runat="server" Value="0" ID="hdId" ClientIDMode="Static" />--%>
        <input type="hidden" value="0" id="hdId" />
        <input id="hdEmail" type="hidden" value="0" />
        <input type="hidden" value="" id="hdnConvertedFrom" />
        <input type="hidden" value="false" id="hdnStockAffect" />

        <%-- ---- Page Title ---- --%>
        <div class="row p-b-10">
            <div class="col-sm-3">
                <h3 class="page-title m-t-0">Invoice</h3>
            </div>
            <div class="col-sm-9">
                <div class="btn-toolbar pull-right" role="group">
                    <button type="button" accesskey="v" data-toggle="tooltip" data-placement="bottom" title="View previous sales Invoice" id="btnFind" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                    <button id="btnNew" type="button" accesskey="n" data-toggle="tooltip" data-placement="bottom" title="Start a new invoice. Unsaved data will be lost." class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                    <button type="button" id="btnSave" accesskey="s" data-toggle="tooltip" data-placement="bottom" title="Save the current invoice" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                    <button id="btnSavePrint" type="button" accesskey="a" data-toggle="tooltip" data-placement="bottom" title="Save & Print the current invoice" class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i>&nbsp;Save & Print</button>
                    <button type="button" id="btnPrint" accesskey="p" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i></button>
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
                        <div class="panel b-r-8">
                            <div class="panel-body">
                                <div class="col-sm-7">
                                    <label class="title-label">Add to list from here..<a class="add-master" id="addnewItem" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="bottom" title="Add new Item"></i></a></label>
                                    <%--<asp:TextBox ID="txtChooser" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Choose Item"></asp:TextBox>--%>
                                    <input type="text" autocomplete="off" class="form-control" id="txtChooser" placeholder="Choose Item" />
                                    <div id="descWrap" class="hide">
                                        <label>Description</label>
                                        <%--<input type="text" class="form-control" />--%>
                                        <textarea id="txtDescription" cols="30" rows="4" class="form-control"></textarea>
                                        <p class="text-muted text-right m-t-10"><i>Press <kbd>ESC</kbd> after completion</i></p>
                                    </div>

                                    <div id="lookup">
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                                    <%--<asp:TextBox ID="txtQuantity" TextMode="Number" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Qty"></asp:TextBox>--%>
                                    <input type="text" autocomplete="off" class="form-control" id="txtQuantity" placeholder="Qty" />
                                </div>
                                <div class="col-sm-2 text-center">
                                    <div class="row">
                                        <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List" class="btn btn-icon btn-primary"><i class="ion-plus"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-5">
                        <div class="portlet b-r-8">
                            <div class="portlet-heading portlet-default p-b-10">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <label class="title-label">Select Customers<a class="add-master" id="addCustomer" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="bottom" title="Add new Customer."></i></a></label>
                                        <div class="col-xs-9">
                                            <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-xs-1">
                                            <div class="row">
                                                <span data-toggle="tooltip" title="View Details" data-trigger="hover" class="expand-btn">
                                                    <a data-toggle="collapse" data-parent="#accordion1" href="#bg-default" class="" aria-expanded="true">
                                                        <i class="md md-unfold-more rot-180" style="font-size: 1.5em;"></i>
                                                    </a>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="row">
                                                <div class="btn-group" data-toggle="tooltip" title="Link Sales Orders" data-trigger="hover">
                                                    <button type="button" id="btnLinkPo" class="btn btn-icon btn-warning link-btn">
                                                        <i class="md md-link rot-135"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div id="bg-default" class="panel-collapse collapse p-t-5" aria-expanded="true" style="">
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label class="title-label">Name</label>
                                            <%--<asp:TextBox ID="txtName" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" placeholder="Name"></asp:TextBox>--%>
                                            <input type="text" autocomplete="off" class="form-control input-sm" id="txtName" placeholder="Name" />

                                        </div>
                                        <div class="col-sm-6">
                                            <label class="title-label">Contact</label>
                                            <%--<asp:TextBox ID="txtPhone" autocomplete="off" ClientIDMode="Static" TextMode="Number" runat="server" CssClass="form-control input-sm" placeholder="Contact"></asp:TextBox>--%>
                                            <input type="text" autocomplete="off" class="form-control input-sm" id="txtPhone" placeholder="Contact" />
                                        </div>
                                        <div class="col-sm-12">
                                            <%--<asp:TextBox ID="txtAddress" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control input-sm m-t-10" placeholder="Address"></asp:TextBox>--%>
                                            <input type="text" autocomplete="off" class="form-control input-sm m-t-10" id="txtAddress" placeholder="Address" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-sm-2">
                        <div class="panel b-r-8">
                            <div class="panel-body" style="padding-top: 17.5px; padding-bottom: 17.5px;">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <span>Invoice No : </span>
                                        <asp:Label ID="lblOrderNumber" runat="server" ClientIDMode="Static" class="badge badge-danger pull-right"><b>KU1368B</b></asp:Label>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row">
                                        <span>Date : </span>
                                        <input type="text" id="txtEntryDate" style="width: 75Px;" class="date-info" value="01/Oct/2017" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-sm-12 additional-detail m-b-10">
                <a data-toggle="collapse" data-parent="#accordion2" href="#bg-default1" class="pull-right toggleName" aria-expanded="true">
                    <span class="toggleText"><i class="ion-ios7-minus-outline"></i></span>
                </a>
                <div class="portlet b-r-8">
                    <div id="bg-default1" class="panel-collapse collapse in p-t-5" aria-expanded="true" style="">
                        <div class="portlet-body p-t-25 p-r-40">
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <label class="title-label">Payment Mode</label>
                                            <select id="ddlMop" class="form-control">
                                                <option value="0">Cash</option>
                                                <option value="1">Credit</option>
                                            </select>
                                        </div>
                                        <div class="col-sm-7">
                                            <label class="title-label">Sales Executive</label>
                                            <asp:DropDownList ID="ddlSalesPerson" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-9">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <label class="title-label">Vehicle<a class="add-master" id="addnewVehicle" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="right" title="Add new Vehicle."></i></a></label>
                                            <asp:DropDownList ID="ddlVehicle" ClientIDMode="Static" CssClass="form-control round-no-border" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="title-label">Freight Tax<a class="add-master" id="addnewtax" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="right" title="Add new Tax."></i></a></label>
                                            <asp:DropDownList ID="ddlFreightTax" ClientIDMode="Static" CssClass="form-control round-no-border" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label class="title-label">Freight Amount</label>
                                            <%--<asp:TextBox ID="txtFreightAmount" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Freight Amount"></asp:TextBox>--%>
                                            <input type="text" autocomplete="off" class="form-control" id="txtFreightAmount" placeholder="Freight Amount" />
                                        </div>

                                        <div class="col-sm-2">
                                            <label class="title-label">Despatch<a class="add-master" id="addnewDespatch" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="right" title="Add new Despatch."></i></a></label>

                                            <asp:DropDownList runat="server" ID="ddlDespatch" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <label class="title-label">Cartons</label>
                                            <%--<asp:TextBox ID="txtCartons" autocomplete="off" ClientIDMode="Static" TextMode="Number" runat="server" CssClass="form-control" placeholder="Cartons"></asp:TextBox>--%>
                                            <input type="text" autocomplete="off" id="txtCartons" class="form-control" placeholder="Cartons" />

                                        </div>
                                        <div class="col-sm-3">
                                            <div class="col-sm-6">
                                                <label class="title-label">LPO</label>
                                                <%-- <asp:TextBox ID="txtLPO" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="LPO"></asp:TextBox>--%>
                                                <input type="text" autocomplete="off" id="txtLPO" class="form-control" placeholder="LPO" />
                                            </div>
                                            <div class="col-sm-6">
                                                <label class="title-label">DO</label>
                                                <%--<asp:TextBox ID="txtDo" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="DO"></asp:TextBox>--%>
                                                <input type="text" autocomplete="off" id="txtDo" class="form-control" placeholder="DO" />
                                            </div>
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
            <div class="col-sm-12 m-b-10">
                <div class="panel panel-default view-h b-r-8 ">
                    <div class="panel-body p-t-10">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th class="hidden">item Id</th>
                                            <th class="hidden">Detail Id</th>
                                            <th style="width: 400px">Item </th>
                                            <th>Code</th>
                                            <th style="text-align: right">Tax%</th>
                                            <th style="text-align: right">MRP</th>
                                            <th style="text-align: right">Rate</th>
                                            <th style="text-align: right">Qty</th>
                                            <th style="text-align: right">Discount</th>
                                            <th style="text-align: right">Discount %</th>
                                            <th style="text-align: right">Tax</th>
                                            <th style="text-align: right">Gross</th>
                                            <th style="text-align: right">Net</th>
                                            <th></th>
                                            <th class="hidden">SchemeId</th>
                                            <th class="hidden">instanceId</th>
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

    <%-- ---- Summary Panel ---- --%>
    <div class="row summary-panel summary-fixed">
        <div class="col-sm-9 col-lg-10">
            <div class="mini-stat clearfix bx-shadow b-r-8">
                <div class="row">
                    <div class="col-sm-2 text-center">
                        <label class="w-100 light-font-color">Total Items</label>
                        <%-- Total Items --%>
                        <%--            <asp:Label ID="lblTotalItem" CssClass="l-font" ClientIDMode="Static" runat="server" Text="0"></asp:Label>--%>
                        <span class="l-font" id="lblTotalItem" />
                    </div>
                    <div class="col-sm-2 text-center">
                        <label class="w-100 light-font-color">Gross </label>
                        <%-- Gross Amount --%>
                        <%-- <asp:Label ID="lblGross" ClientIDMode="Static" CssClass="l-font" runat="server" Text="0"></asp:Label>--%>
                        <span class="l-font" id="lblGross" />
                    </div>
                    <div class="col-sm-2 text-center">

                        <label class="w-100 light-font-color">Discount </label>
                        <%-- Total Amount --%>
                        <%--<asp:TextBox ID="txtDiscount" TextMode="Number" AutoComplete="off" CssClass="w-100 l-font" Style="border: none; text-align: center; background-color: transparent; font-size: 20px" ClientIDMode="Static" runat="server" placeholder="Discount">0.00</asp:TextBox>--%>
                        <input type="text" autocomplete="off" class="w-100 l-font" style="border: none; text-align: center; background-color: transparent; font-size: 20px" placeholder="0.00" id="txtDiscount" />
                    </div>
                    <div class="col-sm-2 text-center">
                        <label class="w-100 light-font-color">Tax </label>
                        <%-- Tax Amount --%>
                        <%-- <asp:Label ID="lblTax" CssClass="l-font" runat="server" ClientIDMode="Static" Text="0"></asp:Label>--%>
                        <span class="l-font" id="lblTax" />
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
                            <input type="text" class="w-100" autocomplete="off" id="txtNarration" placeholder="Enter narration here.." />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-3 col-lg-2">
            <%-- NET AMOUNT --%>
            <div class="mini-stat clearfix bx-shadow b-r-8">
                <div class="col-sm-2"><span class="currency">$</span></div>
                <div class="col-sm-10">
                    <h3 class="text-right text-primary m-0">
                        <%--<asp:Label ID="lblNet" ClientIDMode="Static" runat="server" CssClass="counter" Text="XX"></asp:Label>--%>
                        <span class="counter" id="lblNet" />
                    </h3>
                    <div class="mini-stat-info text-right text-muted">Net Amount </div>
                </div>

            </div>
        </div>
    </div>

    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Sales Invoices &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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
                    <table id="tblRegister" class="table table-hover table-striped table-responsive table-scroll">
                        <thead>
                            <tr>
                                <th class="hidden">Po Id</th>
                                <th>Invoice No</th>
                                <th>Date</th>
                                <th>Customer</th>
                                <th>Tax</th>
                                <th>Gross</th>
                                <th>Net </th>
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


    <%-- ---- Modal for Quote Building ---- --%>
    <div id="myModal" class="modal animated fadeIn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-w-lg">
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                    <div class="modal-title" id="myModalLabel">
                        <span style="padding-right: 5px">Request Date: 
                                <label id="mdlRequestDate" class="text-danger">xx</label>
                        </span>
                    </div>
                </div>
                <div class="modal-body modal-body-lg">
                    <table id="poTable" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey ">
                            <tr>
                                <th class="hidden">ID</th>
                                <th class="text-white">Estimate No</th>
                                <th class="text-white">Customer</th>
                                <th class="text-white">Estimate Date</th>
                                <th class="text-white" style="text-align: right;">Tax</th>
                                <th class="text-white" style="text-align: right;">Gross</th>
                                <th class="text-white" style="text-align: right;">Net </th>
                                <th style="text-align: right;">
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
                            <h4 class="m-b-0">Total SOs : &nbsp<label class="text-success" id="noOfItems"></label></h4>
                        </div>

                        <div class="col-sm-5 col-md-7">
                            <div class="btn-toolbar pull-right">
                                <button id="btnMergePO" class="btn btn-primary waves-effect waves-light" aria-expanded="true" type="button"><i class="ion ion-steam"></i>Merge</button>
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

                                        <div class="form-group hidden">
                                            <label class="control-label col-sm-4">Validity </label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtValidity" class="form-control input-sm" />
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
            var dataHeight = Math.abs($('.additional-detail').offset().top - $('.summary-panel').offset().top) - 105;
            if (dataHeight) {
                $('.view-h').css({
                    'max-height': dataHeight,
                    'min-height': dataHeight
                });
            }
        });

        $(function () {
            $('.toggleName').click(function () {
                $('.toggleText > i').toggleClass('ion-ios7-plus-outline');
            });
        });


        //calculate function call
        setInterval(calculateSummary, 1000);
        setInterval(calculation, 1000);
        //calculation function starts here
        function calculation() {

            var tr = $('#listTable>tbody').children('tr');
            for (var i = 0; i < tr.length; i++) {
                var tempTax = 0;
                var qty = parseFloat($(tr[i]).children('td:nth-child(8)').text());

                var gross = parseFloat($(tr[i]).children('td:nth-child(8)').text()) * parseFloat($(tr[i]).children('td:nth-child(7)').text());
                gross = parseFloat(gross.toFixed(2));//Gross Amount

                var taxper = parseFloat($(tr[i]).children('td:nth-child(5)').text());
                var tax = parseFloat($(tr[i]).children('td:nth-child(7)').text()) * (taxper / 100);
                tax = parseFloat(tax);//Tax Amount

                var net = gross + (tax * qty);
                net = parseFloat(net.toFixed(2));//Net amount

                var discount = (parseFloat($(tr[i]).children('td:nth-child(6)').text()) * parseFloat($(tr[i]).children('td:nth-child(8)').text())) - net;
                discount = parseFloat(discount.toFixed(2));

                var discountPer = discount / (parseFloat($(tr[i]).children('td:nth-child(6)').text()) * parseFloat($(tr[i]).children('td:nth-child(8)').text()));
                var disPer = discountPer * 100;
                disPer = parseFloat(disPer.toFixed(2));
                disPer = isNaN(disPer) ? 0 : disPer;
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax.toFixed(2));
                var mrp = parseFloat($(tr[i]).children('td:nth-child(6)').text());//mrp
                if (mrp == 0) {
                    discount = 0;
                    disPer = 0;
                }
                $(tr[i]).children('td:nth-child(12)').text(gross); //gross amount
                $(tr[i]).children('td:nth-child(11)').text(tempTax.toFixed(2));  //tax amount
                $(tr[i]).children('td:nth-child(13)').text(net);  //net amount
                $(tr[i]).children('td:nth-child(9)').text(discount); //discount amount
                $(tr[i]).children('td:nth-child(10)').text(disPer);// discount percentage
                qty = 0;
            }
        }
        //calculation function ends here
        function calculateSummary() {
            var tr = $('#listTable > tbody').children('tr');
            var tax = 0;
            var gross = 0;
            var net = 0;
            var discount = 0;
            var round = 0.0;
            //Adding Freight Amount with net amount
            var freightAmount = parseFloat($('#txtFreightAmount').val());
            var temp = 0;
            for (var i = 0; i < tr.length; i++) {
                tax += parseFloat($(tr[i]).children('td:nth-child(11)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(12)').text());

            }

            if (JSON.parse($('#hdSettings').val()).IsDiscountEnabled == true) {
                for (var i = 0; i < tr.length; i++) {
                    net += parseFloat($(tr[i]).children('td:nth-child(13)').text() - 0);
                }
                net = !isNaN(parseFloat($('#txtDiscount').val())) ? net - parseFloat($('#txtDiscount').val()) : net - 0;
            }
            else {
                //  alert('');
                for (var i = 0; i < tr.length; i++) {
                    net += parseFloat($(tr[i]).children('td:nth-child(13)').text());
                    //alert(net);
                }
            }
            temp = net;
            if (JSON.parse($('#hdSettings').val()).AutoRoundOff) {
                //Adding Freight Amount with net amount
                freightAmount = isNaN(freightAmount) ? 0 : freightAmount;
                net += freightAmount;
                var roundoff = Math.round(net) - net;
                net = Math.round(net);
                roundoff = parseFloat(roundoff.toFixed(2));
                $('#txtRoundOff').val(roundoff);
            }
            else {
                //Adding Freight Amount with net amount
                freightAmount = isNaN(freightAmount) ? 0 : freightAmount;
                net += freightAmount;

                var roundoff = parseFloat($('#txtRoundOff').val());
                net = net + parseFloat($('#txtRoundOff').val());
                net = net.toFixed(2);
            }
            var le = tr.length;
            $('#lblTotalItem').text(le);
            tax = parseFloat(tax.toFixed(2));
            gross = parseFloat(gross.toFixed(2));
            //console.log($('#txtDiscount').val());

            //     console.log(freightAmount);
            net = parseFloat(net);
            //   net += freightAmount;
            net = net.toFixed(2);
            $('#lblGross').text(gross);
            $('#lblNet').text(net);
            $('#lblTax').text(tax);

        }
    </script>

    <script>
        //document ready function starts here
        $(document).ready(function () {

            var dataHeight = Math.abs($('.additional-detail').offset().top - $('.summary-panel').offset().top) - 105;
            if (dataHeight) {
                $('.view-h').css({
                    'max-height': dataHeight,
                    'min-height': dataHeight
                });
            }

            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            if (JSON.parse($('#hdSettings').val()).IsDiscountEnabled == true) {
                $('#txtDiscount').prop('disabled', false);
            }
            else {
                $('#txtDiscount').prop('disabled', true);
            }

            $('#btnPrint').hide();
            $('#btnMail').hide();

            $("#modalMail").on('hidden.bs.modal', function () {
                $('.before-send').show();
                $('.after-send').hide();
                $('#txtMailAddress').val('');
            });

            //Summer-note initilization
            //Init HTML EDITOR
            $('.additional-settings-button').click(function () {
                $('#dvTandC').summernote({
                    height: 450,
                    focus: false,
                });
                $('#dvPaymentTerm').summernote({
                    height: 450,
                    focus: false,
                });
                $(".overflow-content").niceScroll({
                    cursorcolor: "#616161",
                    cursorwidth: "6px"
                });
            });



            //Initilization of Summary note ends.

            $('.change-address').click(function () {
                $('.address-tab').removeClass('hidden');
            });

            //Loading default TandC
            LoadTandCSettings();


            //To load the Tand C in settings
            function LoadTandCSettings() {

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


            //Getting Entry for edit purpose if user asked for it
            var Params = getUrlVars();
            if (Params.UID != undefined && !isNaN(Params.UID)) {
                if (Params.MODE == 'clone') {
                    getRegister(true, Params.UID);
                } else if (Params.MODE == 'edit') {
                    getRegister(false, Params.UID);
                }
                //The Data will be loaded for convertion according to the type
                else if (Params.MODE == 'convert') {
                    getConvertData(Params.UID, Params.TYPE);
                }
                else {
                    resetRegister();
                }


            }
            else {
                if (Params.JOB != undefined && !isNaN(Params.JOB)) {
                    var job = Params.JOB;
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
                                $('#txtName').val(this.Customer);
                                $('#txtPhone').val(this.CustomerPhone);
                                $('#txtAddress').val(this.CustomerAddress);
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

                    $('#ddlCostCenter').select2('val', Params.COSTCENTER);
                    $('#hdId').val('0');
                    $('#btnPrint').hide();
                    $('#btnMail').hide();
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                    $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
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

            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtEntryDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtEntryDate').datepicker()
          .on('changeDate', function (ev) {
              $('#txtEntryDate').datepicker('hide');
          });

            //lookup initialization


            lookup({
                textBoxId: 'txtChooser',
                url: $('#hdApiUrl').val() + 'api/search/ItemsFromPurchaseWithScheme?locationid=' + $.cookie('bsl_2') + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'SellingPrice', 'Stock', 'SchemeId', 'IsService', 'TrackInventory', 'Description'],
                visibility: [false, false, true, true, true, true, true, true, false, false, false, false],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate', 'Stock', 'SchemeId', 'IsService', 'TrackInventory', 'Description'],
                key: 'ItemID',
                dataToShow: 'Name',
                ddl1Id: 'ddlCustomer',
                ddl1Param: 'customerid',
                onNullParameter: function () { errorField('.select2-choice'); },
                OnZeroResults: function () { },
                OnComplete: function () { miniLoading('stop') },
                OnLoading: function () { miniLoading('start') },
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
                    $('#txtName').val('');
                    $('#txtPhone').val('');
                    $('#txtAddress').val('');
                }

            }


            //Function to get the data for converting
            //Parameter 1.ID --->ID of the register which is to be converted
            //Parameter 2.TYPE-->TYPE of the Register Eg:-Estimate:EST
            function getConvertData(id, TYPE) {
                if (TYPE == 'EST') {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SalesEstimate/get/' + id,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            try {
                                var html = '';
                                var register = response;
                                console.log(response);
                                LoadAddress(register.CustomerId);
                                $('#txtNarration').val(register.Narration);
                                $('#btnPrint').show();
                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                    html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                    html += '<td><b>' + this.ItemCode + '</b></td>';
                                    html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                    html += '<td style="text-align:right">' + this.MRP + '</td>';
                                    html += '<td contenteditable="true" class="numberonly" style="text-align:right">' + this.SellingPrice + '</td>';
                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                    html += '<td style="text-align:right">0</td>';
                                    html += '<td style="text-align:right">0</td>';
                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                    html += '<td style="text-align:right"> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '<td style="display:none">' + this.SchemeId + '</td>';
                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                    html += '</tr>';
                                });
                                $('#ddlCustomer').select2('val', register.CustomerId);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                populateJobs(register.JobId, false);
                                $('#ddlCustomer').val(register.CustomerId);
                                $('#txtName').val(register.CustomerName);
                                $('#txtAddress').val(register.CustomerAddress);
                                $('#txtPhone').val(register.ContactNo);
                                $('#lblGross').val(register.Gross);
                                $('#lblTotalAmount').val(register.Gross);
                                $('#lblTaxAmount').val(register.Gross);
                                $('#lblNetAmount').val(register.NetAmount);
                                $('#txtRoundOff').val(register.RoundOff);
                                $('#listTable tbody').append(html);
                                $('#findModal').modal('hide');
                                $('#txtDiscount').val(register.Discount);
                                $('#txtContactName').val(register.BillingAddress[0].ContactName);
                                $('#txtPhone1').val(register.BillingAddress[0].Phone1);
                                $('#txtPhone2').val(register.BillingAddress[0].Phone2);
                                $('#ddlSalutation').val(register.BillingAddress[0].Salutation);
                                $('#txtZipCode').val(register.BillingAddress[0].Zipcode);
                                $('#txtAddress1').val(register.BillingAddress[0].Address1);
                                $('#txtAddress2').val(register.BillingAddress[0].Address2);
                                $('#txtCity').val(register.BillingAddress[0].City);
                                $('#txtEmail').val(register.BillingAddress[0].Email);
                                $('#hdEmail').val(register.BillingAddress[0].Email);
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
                                $('#txtValidity').val(register.Validity);
                                $('#txtETA').val(register.ETA);
                                $('#hdnConvertedFrom').val(TYPE);
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
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); }
                    });
                }
                else if(TYPE == 'SQT'){
                    resetRegister();
                    $('#hdnConvertedFrom').val(TYPE);
                    $.ajax({
                            url: $('#hdApiUrl').val() + 'api/SalesQuote/get/' + id,
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify($.cookie('bsl_2')),
                            contentType: 'application/json;charset=utf-8',
                            success: function (response) {
                                try {
                                    var html = '';
                                    var register = response;
                                    console.log(response);
                                    LoadAddress(register.CustomerId);
                                    //$('#txtEntryDate').datepicker(register.EntryDateString);
                                    $('#txtNarration').val(register.Narration);
                                    //$('#txtInvoiceNo').val(register.InvoiceNo);
                                        $('#btnPrint').show();
                                    $(register.Products).each(function () {
                                        html += '<tr>';
                                        html += '<td style="display:none">' + this.ItemID + '</td>';
                                        html += '<td style="display:none">' + this.DetailsID + '</td>';
                                        html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                        html += '<td>' + this.ItemCode + '</td>';
                                        html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                        html += '<td style="text-align:right">' + this.MRP + '</td>';
                                        html += '<td contenteditable="true" class="numberonly" style="text-align:right">' + this.SellingPrice + '</td>';
                                        html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                        html += '<td style="text-align:right" contenteditable="true" class="numberonly">0</td>';
                                        html += '<td style="text-align:right">0</td>';
                                        html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                        html += '<td style="text-align:right">' + this.Gross + '</td>';
                                        html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                        html += '<td style="text-align:right"> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                        html += '<td style="display:none">' + this.SchemeId + '</td>';
                                        html += '<td style="display:none">' + this.InstanceId + '</td>';
                                        html += '</tr>';
                                    });
                                    $('#ddlCustomer').select2('val', register.CustomerId);
                                    $('#ddlCostCenter').select2('val', register.CostCenterId);
                                    populateJobs(register.JobId, false);

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
                                    $('#txtValidity').val(register.Validity);
                                    $('#txtETA').val(register.ETA);
                                    $('#ddlCustomer').val(register.CustomerId);
                                    $('#txtName').val(register.Customer_Name);
                                    $('#lblOrderNumber').text(register.QuoteNumber);
                                    $('#txtAddress').val(register.CustomerAddress);
                                    $('#txtPhone').val(register.ContactNo);
                                    $('#lblGross').val(register.Gross);
                                    $('#lblTotalAmount').val(register.Gross);
                                    $('#txtDiscount').val(register.Discount);
                                    $('#lblTaxAmount').val(register.Gross);
                                    $('#lblNetAmount').val(register.NetAmount);
                                    $('#txtRoundOff').val(register.RoundOff);
                                    $('#listTable tbody').append(html);
                                    $('#findModal').modal('hide');
                                    $('#txtContactName').val(register.BillingAddress[0].ContactName);
                                    $('#txtPhone1').val(register.BillingAddress[0].Phone1);
                                    $('#txtPhone2').val(register.BillingAddress[0].Phone2);
                                    $('#ddlSalutation').val(register.BillingAddress[0].Salutation);
                                    $('#txtZipCode').val(register.BillingAddress[0].Zipcode);
                                    $('#txtAddress1').val(register.BillingAddress[0].Address1);
                                    //console.log(register.BillingAddress[0].Address2);
                                    $('#txtAddress2').val(register.BillingAddress[0].Address2);
                                    $('#txtCity').val(register.BillingAddress[0].City);
                                    $('#txtEmail').val(register.BillingAddress[0].Email);
                                    $('#hdEmail').val(register.BillingAddress[0].Email);
                                    $('#ddlCountry').val(register.BillingAddress[0].CountryID);
                                    loadStates(register.BillingAddress[0].StateID);
                                    
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
                                miniLoading('stop');
                            },
                            beforeSend: function () { miniLoading('start'); },
                            complete: function () { miniLoading('stop'); }
                        });
                }
                else if (TYPE=='DLN') {
                    resetRegister();
                    $('#hdnConvertedFrom').val(TYPE);
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SalesDeliveryNote/get/' + id,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            try {
                                var html = '';
                                var register = response;
                                LoadAddress(register.CustomerId);
                                //$('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                                $('#txtNarration').val(register.Narration);
                                //$('#txtInvoiceNo').val(register.InvoiceNo);
                                //$('#ddlSupplier').val(register.SupplierId);
                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                    html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                    html += '<td>' + this.ItemCode + '</td>';
                                    html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                    html += '<td style="text-align:right">' + this.MRP + '</td>';
                                    html += '<td contenteditable="true" class="numberonly" style="text-align:right">' + this.CostPrice + '</td>';
                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                    html += '<td style="text-align:right" contenteditable="true" class="numberonly">0</td>';
                                    html += '<td style="text-align:right">0</td>';
                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                    html += '<td style="text-align:right"> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '<td style="display:none">' + this.SchemeId + '</td>';
                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                    html += '</tr>';
                                });
                                $('#ddlCustomer').select2('val', register.CustomerId);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                populateJobs(register.JobId, false);
                                $('#ddlCustomer').val(register.CustomerId);

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

                                $('#dvPaymentTerm').summernote('code', register.PaymentTerms);
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
                                $('#txtName').val(register.CustomerName);
                                $('#lblOrderNumber').text(register.DeliveryNoteNumber);
                                $('#txtAddress').val(register.CustomerAddress);
                                $('#txtPhone').val(register.ContactNo);
                                $('#lblGross').val(register.Gross);
                                $('#lblTotalAmount').val(register.Gross);
                                $('#lblTaxAmount').val(register.Gross);
                                $('#lblNetAmount').val(register.NetAmount);
                                $('#txtRoundOff').val(register.RoundOff);
                                $('#listTable tbody').append(html);
                                $('#findModal').modal('hide');
                                //Additional fields to load saved address from the delivery note register
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
                                $('#hdnStockAffect').val(true);
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
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); }
                    });
                }
            }



            //loading customer details from controller
            $('#ddlCustomer').off().change(function (e) {

                populateJobs(e.selectedJob | '0', true);

            });
            //lock customer once selected
            $('#ddlCustomer').change(function () {
                if ($(this).val() != 0) {
                    //$(this).prop('disabled', true);
                }
            });

            //Redirected from Settle bills 
            var Cust = getUrlVars();
            if (Cust.CUSTOMER != undefined && !isNaN(Cust.CUSTOMER)) {
                $('#ddlCustomer').select2('val', Cust.CUSTOMER);
                $('#ddlCustomer').val(Cust.CUSTOMER).trigger("change");
            }
            //function for add items to list starts here
            function AddToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var qty = parseFloat($('#txtQuantity').val());
                var gross = parseFloat((qty * tempItem.SellingPrice).toFixed(2));
                var taxAmt = parseFloat((tempItem.SellingPrice * (tempItem.TaxPercentage / 100)));
                taxAmt = taxAmt.toFixed(2);
                var net = parseFloat(taxAmt * qty);
                net = parseFloat(net + gross).toFixed(2);
                var discount = (parseFloat($('#txtQuantity').val()) * parseFloat(tempItem.MRP)) - net;
                discount = discount.toFixed(2);
                var discountPer = parseFloat(discount) / parseFloat(tempItem.MRP);
                var disPer = discountPer * 100;
                disPer = parseFloat(disPer.toFixed(2));
                var description = tempItem.Description.replace(/\n/g, '<br/>');

                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null & !isNaN(qty)) {

                    var IsNegBillingAllowed = JSON.parse($('#hdSettings').val()).AllowNegativeBilling;
                    var TrackInventory = tempItem.TrackInventory;     //Change IsService to Type  0 for Item 1 for Service and 2 for Inventories
                    //Check Stock only for Type 0.
                    //Change False to Type 0
                    if ((!IsNegBillingAllowed && parseFloat(tempItem.Stock) < qty) && TrackInventory == 'true') {
                        console.log(TrackInventory);
                        $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                    }
                        //if (!IsNegBillingAllowed && parseFloat(tempItem.Stock) < qty) {
                        //    $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                        //}
                    else {
                        var Rows = $('#listTable > tbody').children('tr');
                        var itemExists = false;
                        var rowOfItem;
                        $(Rows).each(function () {
                            var itemId = $(this).children('td').eq(0).text();
                            var instanceId = $(this).children('td').eq(15).text();
                            if (tempItem.ItemID == itemId && tempItem.InstanceId == instanceId) {
                                itemExists = true;
                                rowOfItem = this;
                            }

                        });

                        if (itemExists) {
                            var existingQty = parseFloat($(rowOfItem).children('td').eq(7).text());
                            var newQty = existingQty + qty;
                            if (parseFloat(tempItem.Stock) < newQty && TrackInventory == 'true') {

                                $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                            }
                            else {
                                $(rowOfItem).children('td').eq(7).replaceWith('<td style="text-align:right;" contenteditable="true" class="numberonly">' + newQty + '</td>');
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
                            html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td style="display:none">0</td><td><b>' + tempItem.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + description + '</i></td><td>' + tempItem.ItemCode + '</td><td style="text-align:right">' + tempItem.TaxPercentage + '</td><td style="text-align:right">' + tempItem.MRP + '</td><td contenteditable="true" class="numberonly" style="text-align:right">' + tempItem.SellingPrice + '</td><td style="text-align:right" contenteditable="true" class="numberonly">' + qty + '</td><td style="text-align:right">' + discount + '</td><td style="text-align:right">' + disPer + '</td><td style="text-align:right">' + (taxAmt * qty).toFixed(2) + '</td><td style="text-align:right">' + gross + '</td><td style="text-align:right">' + net + '</td><td style="text-align:right;"><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td><td style="display:none">' + tempItem.SchemeId + '</td><td style="display:none">' + tempItem.InstanceId + '</td></tr>';
                            $('#listTable > tbody').prepend(html);
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
            //addtolist function ends here
            //AddTolist function call
            $('#btnAdd').click(function () {
                AddToList();
            });

            //Add Item to list with enter key function call        
            $('#txtQuantity').keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    AddToList();
                }

            });
            //binding event to select All checkbox
            $('#poTable').on('change', '.chk-all', function () {

                if ($(this).is(':checked')) {
                    var rows = $('#poTable tbody').children('tr');
                    for (var i = 0; i < rows.length; i++) {
                        $('.chk-single').prop('checked', true);
                        $(rows[i]).addClass('selected-row');
                    }
                }
                else {
                    var rows = $('#poTable tbody').children('tr');
                    for (var i = 0; i < rows.length; i++) {
                        $('.chk-single').prop('checked', false);
                        $(rows[i]).removeClass('selected-row');
                    }
                }

            });

            //binding event to single checkbox
            $('#poTable').on('click', '.chk-single', function () {

                if ($(this).is(':checked')) {
                    $(this).closest('tr').children('td').eq(6).children('input').focus().select();
                    $(this).closest('tr').addClass('selected-row');
                }
                else {
                    $(this).closest('tr').removeClass('selected-row');
                }
            });

            //Save function call
            $('#btnSave').off().click(function () {
                save(false);

            });
            //Printing the Request
            $('#btnPrint').click(function () {
                var id = $('#hdId').val();
                if (id == '0') {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/sales/Prints/" + type + "/" + number + "/Entry?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'salesEntry', 800, 700);
                }
                else {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/sales/Prints/" + type + "/" + number + "/Entry?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'salesEntry', 800, 700);
                }
            });
            //save and print entry function call
            $('#btnSavePrint').click(function () {
                save(true);

            });

            //link with PO
            $('#btnLinkPo').click(function () {
                $('#mdlRequestDate').text($('#txtEntryDate').val());

                if ($('#ddlCustomer').val() != 0) {
                    $('#poTable tbody').children().remove();

                    var customerId = $('#ddlCustomer').val();
                    var locationId = $.cookie('bsl_2');
                    var companyId = $.cookie('bsl_1');
                    var financialYear = $.cookie('bsl_4');
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SalesEntry/GetSalesQuotes/',
                        method: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        dataType: 'JSON',
                        data: JSON.stringify({ 'LocationId': locationId, 'CustomerId': customerId }),
                        success: function (response) {
                            //console.log(response);
                            var customerId = response.CustomerId;
                            var customerName = response.CustomerName;
                            var customerAddress = response.CustomerAddress;
                            var html = '';
                            $(response).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.QuoteNumber + '</td>';
                                html += '<td>' + this.CustomerName + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td style="text-align:right;">' + this.TaxAmount + '</td>';
                                html += '<td style="text-align:right;">' + this.Gross + '</td>';
                                html += '<td style="text-align:right;">' + this.NetAmount + '</td>';
                                html += '<td style="text-align:right;"><input type="checkbox" class="checkbox chk-single"></td>';
                                html += '</tr>';
                            });

                            $('#poTable tbody').append(html);
                            //Merge POs
                            $('body').off().on('click', '#btnMergePO', function () {
                                rows = $('#poTable tbody').children('tr');
                                html = '';

                                $(rows).each(function () {

                                    if ($(this).children('td').eq(7).children('input').is(':checked')) {
                                        var sqdId = $(this).children('td').eq(0).text();

                                        for (var i = 0; i < response.length; i++) {
                                            if (response[i].ID == sqdId) {
                                                console.log(response[i]);

                                                $(response[i].Products).each(function () {
                                                    html += '<tr>';
                                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                                    html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                                    html += '<td>' + this.ItemCode + '</td>';
                                                    html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                                    html += '<td style="text-align:right">' + this.MRP + '</td>';
                                                    html += '<td style="text-align:right">' + this.SellingPrice + '</td>';
                                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                                    html += '<td style="text-align:right;">0</td>';
                                                    html += '<td style="text-align:right;">0</td>';
                                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                                    html += '<td style="text-align:right;"><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                                    html += '<td style="display:none">' + this.SchemeId + '</td>';
                                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                                    html += '</tr>'
                                                });
                                                $('#dvTandC').summernote('code', response[i].TermsandConditon);
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


                                                $('#dvPaymentTerm').summernote('code', response[i].Payment_Terms);
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
                                                //$('#txtValidity').val(response[i].Validity);
                                                //$('#txtETA').val(response[i].ETA);
                                                break;

                                            }
                                        }

                                    }

                                });
                                var customerId = $('#ddlCustomer').val();
                                var customerName = $('#txtName').val();
                                var customerAddress = $('#txtAddress').val();
                                resetRegister();
                                $('#listTable tbody').children().remove();
                                $('#ddlCustomer').select2('val', customerId);
                                $('#ddlCustomer').val(customerId);
                                $('#txtName').val(customerName);
                                $('#txtAddress').val(customerAddress);
                                $('#listTable tbody').append(html);
                                $('#poTable tbody').children().remove();
                                $('#myModal').modal('hide');

                                //Binding Event to remove button
                                $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                            });//Merge Pos end here

                            $('#myModal').modal('show');
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); }
                    });
                }
                else {
                    errorField('#ddlCustomer');
                }

            });//link with PO ends here

            //Delete functionality starts here
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
                                url: $('#hdApiUrl').val() + 'api/SalesEntry/delete/' + id,
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
                                complete: function () { miniLoading('stop'); },
                            });
                        }


                    });
                }
            });
            //delete function ends here
            $('#addCustomer').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewCustomer(Url, User, Company, function (id, name) {
                    $('#ddlCustomer').append('<option value="' + id + '">' + name + '</option>')
                    $('#ddlCustomer').select2('val', id);
                    $('#ddlCustomer').trigger("change");
                });
            });

            $('#addnewVehicle').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewVehicle(Url, User, Company, function (id, name) {
                    $('#ddlVehicle').append('<option value="' + id + '">' + name + '</option>');
                    $('#ddlVehicle').val(id);
                });
            });
            $('#addnewtax').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewTax(Url, User, Company, function (id, name) {
                    $('#ddlFreightTax').append('<option value="' + id + '">' + name + '</option>');
                    $('#ddlFreightTax').val(id);
                });
            });
            $('#addnewDespatch').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewDespatch(Url, User, Company, function (id, name) {
                    $('#ddlDespatch').append('<option value="' + id + '">' + name + '</option>');
                    $('#ddlDespatch').val(id);
                });
            });
            $('#addnewItem').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewItem(Url, User, Company, function (id, name) {
                    console.log(id + "" + name);
                });
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
                resetRegister();
                $('#findModal').modal('show');
                refreshTable(null, null, null);
                function refreshTable(customerID, fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/SalesEntry/Get?CustomerId=' + customerID + '&from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            //console.log(response);
                            var invoiceNo = $('#txtInvoiceNo').val();
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.SalesBillNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.Customer + '</td>';
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
                            $('#tblRegister').off().on('click', '.edit-register', function (e) {
                                resetRegister();
                                var registerId = $(this).closest('tr').children('td').eq(0).text();
                                getRegister(false, registerId);
                            });

                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); }
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
                        url: $('#hdApiUrl').val() + 'api/SalesEntry/SendMail?salesId=' + id + '&toaddress=' + toaddr + '&userid=' + $.cookie('bsl_3'),
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify($(location).attr('protocol') + '//' + $(location).attr('host') + '/Sales/Prints/' + type + '/' + number + '/Entry?id=' + id + '&location=' + $.cookie('bsl_2')),
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
                $('#txtEmail').val(email);
                $('#ddlCountry').val(countryID);
                loadStates(StateID);
                $('.address-tab').addClass('hidden');
            });


            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/SalesEntry/Get/' + id,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (data) {
                        try {
                            console.log(data)
                            var register = data;
                            LoadAddress(register.CustomerId);
                            $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                            $('#txtNarration').val(register.Narration);
                            $('#txtInvoiceNo').val(register.InvoiceNo);
                            $('#ddlMop').val(register.ModeOfPayment);
                            $('#ddlCustomer').select2('val', register.CustomerId);
                            $('#ddlCustomer').val(register.CustomerId);
                            $('#ddlSalesPerson').val(register.SalesPersonId);
                            $('#ddlDespatch').val(register.DespatchId);
                            $('#ddlVehicle').val(register.VehicleId);
                            $('#txtName').val(register.Careof);
                            $('#txtAddress').val(register.CustomerAddress);
                            $('#txtPhone').val(register.CustomerNo);
                            $('#ddlFreightTax').val(register.FreightTaxId);
                            $('#txtLPO').val(register.LPO);
                            $('#txtDo').val(register.DO);
                            $('#txtFreightAmount').val(register.FreightAmount);
                            $('#txtCartons').val(register.Cartons);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            if (isClone) {
                                $('#hdId').val('0');
                                $('#btnPrint').hide();
                                $('#btnMail').hide();
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                            }
                            else {
                                $('#hdId').val(register.ID);
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                $('#btnPrint').show();
                                $('#btnMail').show();
                                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                            }
                            var html = '';
                            $(register.Products).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td style="display:none">' + this.QuoteDetailId + '</td>';
                                html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td contenteditable="true" class="numberonly" style="text-align:right">' + this.SellingPrice + '</td>';
                                html += '<td style="text-align:right" contenteditable="true" class="numberonly">' + this.Quantity + '</td>';
                                html += '<td style="text-align:right">0</td>';
                                html += '<td style="text-align:right">0</td>';
                                html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                html += '<td style="text-align:right">' + this.Gross + '</td>';
                                html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                html += '<td style="display:none">' + this.SchemeId + '</td>';
                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                html += '</tr>';

                            });
                            $('#lblOrderNumber').text(register.SalesBillNo);
                            populateJobs(register.JobId, false);
                            $('#lblGross').val(register.Gross);
                            $('#lblTotalAmount').val(register.Gross);
                            $('#lblTaxAmount').val(register.TaxAmount);
                            $('#lblNetAmount').val(register.NetAmount);
                            $('#txtRoundOff').val(register.RoundOff);
                            $('#txtDiscount').val(register.Discount);
                            $('#txtContactName').val(register.BillingAddress[0].ContactName);
                            $('#txtPhone1').val(register.BillingAddress[0].Phone1);
                            $('#txtPhone2').val(register.BillingAddress[0].Phone2);
                            $('#ddlSalutation').val(register.BillingAddress[0].Salutation);
                            $('#txtZipCode').val(register.BillingAddress[0].Zipcode);
                            $('#txtAddress1').val(register.BillingAddress[0].Address1);
                            $('#txtAddress2').val(register.BillingAddress[0].Address2);
                            $('#txtCity').val(register.BillingAddress[0].City);
                            $('#ddlCountry').val(register.BillingAddress[0].CountryID);
                            $('#txtEmail').val(register.CustomerEmail);
                            $('#hdEmail').val(register.BillingAddress[0].Email);
                            loadStates(register.BillingAddress[0].StateID);
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
                            //$('#txtValidity').val(register.Validity);
                            //$('#txtETA').val(register.ETA);

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
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start') },
                    complete: function () { miniLoading('stop'); },
                });
            }
            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#ddlCustomer').prop('disabled', false);
                $('#ddlCustomer').select2('val', 0);
                $('#ddlJob').select2();
                var date = new Date();
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#txtEntryDate').datepicker('setDate', today);
                $('#hdId').val('');
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#btnPrint').hide();
                $('#btnMail').hide();
                LoadTandCSettings();
                $('#btnSavePrint').html('<i class=\"ion ion-printer"\></i>&nbsp;Save & Print');
                $('#ddlCostCenter').select2('val', 0);
                $('#ddlJob').select2('val', 0);
                $('#hdnConvertedFrom').val('');
                $('#hdnStockAffect').val(false);
            }//Reset ends here
            //Function for Saving the register
            function save(printSave) {

                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",

                    showConfirmButton: true, closeOnConfirm: true,
                    async: false,
                    showCancelButton: true,

                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        var data = {};
                        var arr = [];
                        var address = [];
                        var tbody = $('#listTable > tbody');
                        var tr = tbody.children('tr');
                        var entryDate = $('#txtEntryDate').val();
                        var narration = $('#txtNarration').val();
                        var InvoiceNo = $('#txtInvoiceNo').text();
                        var rOff = $('#txtRoundOff').val();
                        var discount = $('#txtDiscount').val();
                        var LocationId = $.cookie('bsl_2');
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var createdBy = $.cookie('bsl_3');
                        //Additional Data
                        var TandC = $('#dvTandC').summernote('code');
                        var PaymentTerms = $('#dvPaymentTerm').summernote('code');
                        var Validity = $('#txtValidity').val();
                        var ETA = $('#txtETA').val();
                        for (var i = 0; i < tr.length; i++) {
                            var itemId = $(tr[i]).children('td').eq(0).text();
                            var desc = $(tr[i]).children('td').eq(2).children('i').html();
                            var sqdId = $(tr[i]).children('td').eq(1).text();
                            var qty = $(tr[i]).children('td').eq(7).text();
                            var schemeId = $(tr[i]).children('td').eq(14).text();
                            var instanceId = $(tr[i]).children('td').eq(15).text();
                            var Rate = $(tr[i]).children('td').eq(6).text();
                            //var TaxPer = $(tr[i]).children('td').eq(4).text();
                            var detail = { "Quantity": qty };
                            detail.InstanceId = instanceId;
                            detail.ItemID = itemId;
                            detail.QuoteDetailId = sqdId;
                            detail.SchemeId = schemeId;
                            detail.Description = desc;
                            detail.SellingPrice = Rate;
                            //Checks The current entry is convertion or not.If Convertion the Converted from will store TYPE from the queryString.Otherwise null
                            if ($('#hdnConvertedFrom').val()=="") {
                                detail.ConvertedFrom = null;
                            }
                            else {
                                if (detail.QuoteDetailId==0) {
                                    detail.ConvertedFrom = null;
                                }
                                else {
                                    detail.ConvertedFrom = $('#hdnConvertedFrom').val();
                                }
                               
                            }
                            detail.AffectedinStock=$('#hdnStockAffect').val();
                            //detail.TaxPercentage = TaxPer;
                            arr.push(detail);
                        }
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
                        data.ID = $('#hdId').val();
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.EntryDate = entryDate;
                        data.CustomerId = $('#ddlCustomer').val();
                        data.Customer = $('#txtName').val();
                        data.RoundOff = rOff;
                        data.CustomerNo = $('#txtPhone').val();
                        data.CustomerAddress = $('#txtAddress').val();
                        data.ModeOfPayment = $('#ddlMop').val();
                        data.SalesPersonId = $('#ddlSalesPerson').val();
                        data.Discount = discount;
                        data.VehicleId = $('#ddlVehicle').val();
                        data.LPO = $('#txtLPO').val();
                        data.DO = $('#txtDo').val();
                        data.FreightTaxId = $('#ddlFreightTax').val();
                        data.FreightAmount = $('#txtFreightAmount').val();
                        data.DespatchId = $('#ddlDespatch').val();
                        data.Cartons = $('#txtCartons').val();
                        data.EntryDateString = entryDate;
                        data.Products = arr;
                        data.Narration = narration;
                        data.ModifiedBy = createdBy;
                        data.LocationId = LocationId;
                        data.CompanyId = CompanyId;
                        data.FinancialYear = FinancialYear;
                        data.CreatedBy = createdBy;
                        //Additional Data
                        data.TermsandConditon = TandC;
                        data.Payment_Terms = PaymentTerms;
                        data.BillingAddress = address;
                        //data.Validity = Validity;
                        //data.ETA = ETA;
                        console.log(data);
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/SalesEntry/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    
                                    $('#lblOrderNumber').text(response.Object.OrderNo);
                                    if (printSave == true) {
                                        var type = JSON.parse($('#hdSettings').val()).TemplateType;
                                        var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                                        var url = "/sales/Prints/" + type + "/" + number + "/Entry?id=" + response.Object.Id + "&location=" + $.cookie('bsl_2');
                                        PopupCenter(url, 'salesEntry', 800, 700);

                                    }
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
                            complete: function () { miniLoading('stop'); },
                        });


                    }


                });

            }
            //Save Function Ends here

        });
        //document ready function ends here
        //function for calculation inside link
        setInterval(function () {
            var rows = $('#poTable tbody').children('tr');
            var noOfItems = 0.00;
            rows.each(function (index) {
                if ($(this).find('.chk-single').is(':checked')) {

                    noOfItems++;
                }

            });
            $('#noOfItems').text(noOfItems);
        }, 500);



        //Find Filter Function
        $(function () {
            $('#txtChooser').popover({
                placement: 'bottom',
                trigger: 'manual',
                html: true,
                content: $('#descWrap').html()
            })
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


        $('#ddlCountry').change(function () {
            loadStates(0);
        })

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


    </script>
    <%--summer note linking--%>

    <link href="../Theme/assets/summernote/summernote.css" rel="stylesheet" />
    <script src="../Theme/assets/summernote/summernote.min.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/Sections/Customer.js"></script>
    <script src="../Theme/Sections/Vehicle.js"></script>
    <script src="../Theme/Sections/Tax.js"></script>
    <script src="../Theme/Sections/Despatch.js"></script>
    <script src="../Theme/Sections/Items.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <!-- Date Range Picker -->

    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
</asp:Content>
