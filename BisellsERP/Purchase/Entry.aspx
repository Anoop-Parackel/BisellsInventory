<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="BisellsERP.Purchase.Entry" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Purchase Bill</title>

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

        .panel {
            margin-bottom: 10px;
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
            text-align: right !important;
        }

        .link-quote-btn {
            padding: 2px 7px;
        }

            .link-quote-btn i {
                font-size: 20px;
            }

        .daterangepicker.dropdown-menu.ltr.opensleft.show-calendar {
            right: auto !important;
        }
        /*Below style is for quick add section */
        #quickAddLink span {
            background-color: #f1f1f1;
            border-radius: 3px;
            padding: 3px;
            position: relative;
            top: -3px;
            right: -32px;
            border: 1px solid #b4cfda;
        }

        #quickAddLink a {
            color: #6b7c8a;
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

    <%--hidden fileds--%>
    <asp:HiddenField runat="server" Value="" ID="hdTandC" ClientIDMode="Static" />
    <input type="hidden" value="0" id="hdId" />
    <input id="hdEmail" type="hidden" value="0" />
    <input type="hidden" value="" id="hdnConvertedFrom" />
    <input type="hidden" value="false" id="hdnStockAffect" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-5">
        <div class="col-sm-2">
            <h3 class="page-title m-t-0">Purchase Bill</h3>
        </div>
        <div class="col-sm-10">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" accesskey="v" id="btnFind" data-toggle="tooltip" data-placement="bottom" title="View previous purchase bill" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new bill. Unsaved data will be lost " class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current entry" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                <button id="btnSavePrint" accesskey="A" type="button" data-toggle="tooltip" data-placement="bottom" title="Save & Print the current bill" class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i>&nbsp;Save & Print</button>
                <button type="button" accesskey="p" id="btnPrint" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light "><i class="ion ion-printer"></i></button>
                <button id="btnMail" type="button" class="btn btn-default waves-effect waves-light" data-toggle="modal" data-target="#modalMail"><i class="icon ion-chatbox"></i>&nbsp;Mail</button>
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
                            <div class="col-sm-8">
                                <label class="title-label">Add to list from here..<a class="add-master" id="addItem" href="#"><i class="quick-add fa  fa-plus-square-o"></i></a></label>
                                <input type="text" id="txtChooser" autocomplete="off" class="form-control" placeholder="Choose Item" />
                                <div id="descWrap" class="hide">
                                    <label>Description</label>
                                    <textarea id="txtDescription" cols="30" rows="4" class="form-control"></textarea>
                                    <p class="text-muted text-right m-t-10"><i>Press <kbd>ESC</kbd> after completion</i></p>
                                </div>
                                <div id="lookup">
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                                <input type="text" id="txtQuantity" autocomplete="off" class="form-control" placeholder="Qty" />
                            </div>
                            <div class="col-sm-1 text-center">
                                <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List" class="btn btn-icon btn-primary"><i class="ion-plus"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="panel b-r-8">
                        <div class="panel-body">
                            <div class="col-sm-6">
                                <label class="title-label">Supplier<a class="add-master" id="addnewSupplier" href="#"><i class="quick-add fa  fa-plus-square-o" data-toggle="tooltip" data-placement="right" title="Add new Supplier"></i></a></label>
                                <asp:DropDownList ID="ddlSupplier" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                <label class="title-label">Invoice No</label>
                                <input type="text" id="txtInvoiceNo" autocomplete="off" class="form-control" />
                            </div>
                            <div class="col-sm-3 text-center">
                                <div class="btn-group" data-toggle="tooltip" title="Link Orders">
                                    <button type="button" id="btnLinkPo" class="btn btn-icon waves-effect waves-light btn-warning link-quote-btn">
                                        <i class="md md-link rot-135"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3">
                    <div class="panel b-r-8">
                        <div class="panel-body" style="padding-top: 7px; padding-bottom: 7px;">
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
                                <div>
                                    <div class="col-sm-12">
                                        <span>Invoice Date : </span>
                                        <input type="text" id="txtDueDate" style="width: 60%;" class="date-info" value="01/Oct/2017" />
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
                                        <th class="hidden">Item ID</th>
                                        <th class="hidden">Pqd Id</th>
                                        <th>Item </th>
                                        <th>Code</th>
                                        <th style="text-align: right;">Tax%</th>
                                        <th style="text-align: right;">MRP</th>
                                        <th style="text-align: right;">Rate&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                        <th style="text-align: right;">Order Qty</th>
                                        <th style="text-align: right;">Qty&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                        <th style="display: none">SP&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                        <th style="text-align: right;">Tax</th>
                                        <th style="text-align: right;">Gross</th>
                                        <th style="text-align: right;">Net</th>
                                        <th style="display: none">InstanceID</th>
                                        <th style="display: none">IsGRN</th>
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
                        <%--<label id="lblTotalItem" class="l-font"></label>--%>
                        <span id="lblTotalItem" class="l-font"></span>
                    </div>
                    <div class="col-sm-2 text-center">
                        <label class="w-100 light-font-color">Gross </label>
                        <%-- Gross Amount --%>
                       <%-- <label id="lblGross" class="l-font"></label>--%>
                        <span id="lblGross" class="l-font"></span>
                    </div>
                    <div class="col-sm-2 text-center">

                        <label class="w-100 light-font-color">Discount </label>
                        <%-- Total Amount --%>
                        <input type="text" id="txtDiscount" autocomplete="off" class="w-100 l-font" style="border: none; text-align: center; background-color: transparent; font-size: 20px" placeholder="Discount" value="0.00" />
                    </div>
                    <div class="col-sm-2 text-center">
                        <label class="w-100 light-font-color">Tax </label>
                        <%-- Tax Amount --%>
                       <%-- <label id="lblTax" class="l-font"></label>--%>
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
            <div class="mini-stat clearfix bx-shadow b-r-8">
                <div class="col-sm-2"><span class="currency">$</span></div>
                <div class="col-sm-10">
                    <h3 class="text-right text-primary m-0">
                      <%--  <label id="lblNet" class="counter"></label>--%>
                        <span id="lblNet" class="counter"></span>
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
                    <h4 class="modal-title">Previous Purchase Bills &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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
                    <table id="tblRegister" class="table table-hover table-striped table-responsive table-scroll">
                        <thead>
                            <tr>
                                <th class="hidden">PurchaseEntryId</th>
                                <th>Bill No</th>
                                <th>Date</th>
                                <th>Supplier</th>
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
                        <span>Supplier:
                        <label id="mdlSupplier" class="badge badge-danger pw-15">32</label>
                        </span>
                        <asp:CheckBox ID="chkGetPO" runat="server" ClientIDMode="Static" Text="Show Purchase Orders" />
                        <span class="pull-right" style="padding-right: 5px">Request Date: 
                        <label id="mdlRequestDate" class="text-danger">xx</label>
                        </span>
                    </div>
                </div>
                <div class="modal-body modal-body-lg">
                    <table id="poTable" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey ">
                            <tr>
                                <th class="hidden">PurchaseQuoteID</th>
                                <th class="hidden">SupplierID</th>
                                <th class="text-white">Order No</th>
                                <th class="text-white">Quote Date</th>
                                <th class="text-white">Due Date</th>
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
                            <h4 class="m-b-0">Total POs : &nbsp<label class="text-success" id="noOfItems"></label></h4>
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
    <%-- section --%>

    <%-- section --%>
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
                        <span class="">Supplier Address</span>
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
                                        <div class="form-group">
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

        //calculate function and calculate summary function call
        setInterval(calculateSummary, 1000);

        setInterval(calculation, 1000);

        //function calculation starts here
        function calculation()
        {
            var tr = $('#listTable>tbody').children('tr');
            for (var i = 0; i < tr.length; i++) {
                var tempTax = 0;
                var qty = parseFloat($(tr[i]).children('td:nth-child(9)').children('input').val());

                var gross = qty * parseFloat($(tr[i]).children('td:nth-child(7)').children('input').val());
                gross = parseFloat(gross);//Gross Amount

                var taxper = parseFloat($(tr[i]).children('td:nth-child(5)').text());
                var tax = parseFloat($(tr[i]).children('td:nth-child(7)').children('input').val()) * (taxper / 100);
                tax = parseFloat(tax);//Tax Amount

                var net = gross + (tax * qty);
                net = parseFloat(net);//Net amount
                tempTax = qty * tax;
                tempTax = parseFloat(tempTax);
                $(tr[i]).children('td:nth-child(12)').text(gross.toFixed(2)); //gross amount
                $(tr[i]).children('td:nth-child(11)').text(tempTax.toFixed(2));  //tax amount
                $(tr[i]).children('td:nth-child(13)').text(net.toFixed(2));  //net amount
                qty = 0;
            }
        }
        //calculation function ends here

        //calculateSummary function starts here
        function calculateSummary() {

            var tr = $('#listTable > tbody').children('tr');
            var tax = 0;
            var gross = 0;
            var net = 0;
            var temp = 0;
            for (var i = 0; i < tr.length; i++) {
                tax += parseFloat($(tr[i]).children('td:nth-child(11)').text());
                gross += parseFloat($(tr[i]).children('td:nth-child(12)').text())

            }

            if (JSON.parse($('#hdSettings').val()).IsDiscountEnabled == true) {
                for (var i = 0; i < tr.length; i++) {
                    net += parseFloat($(tr[i]).children('td:nth-child(13)').text() - 0);
                }
                net = !isNaN(parseFloat($('#txtDiscount').val())) ? net - parseFloat($('#txtDiscount').val()) : net - 0;
            }
            else {

                for (var i = 0; i < tr.length; i++) {
                    net += parseFloat($(tr[i]).children('td:nth-child(13)').text());
                }
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
            tax = parseFloat(tax);
            gross = parseFloat(gross.toFixed(2));
            $('#lblGross').text(gross);
            $('#lblNet').text(net.toFixed(2));
            $('#lblTax').text(tax.toFixed(2));
        }
        //calculate summary function ends here

    </script>
    <script>

        //Document ready function starts here
        $(document).ready(function ()
        {
            //Hide buttons
            $('#btnPrint').hide();
            $('#btnMail').hide();

            //Load currency symbol
            var currency = JSON.parse($('#hdSettings').val()).CurrencySymbol;
            $('.currency').html(currency);

            //Check discount
            if (JSON.parse($('#hdSettings').val()).IsDiscountEnabled == true) {
                $('#txtDiscount').prop('disabled', false);
            }
            else {
                $('#txtDiscount').prop('disabled', true);
            }

            //Redirected from Settlebills
            var Sup = getUrlVars();
            if (Sup.SUPPLIER != undefined && !isNaN(Sup.SUPPLIER)) {
                $('#ddlSupplier').val(Sup.SUPPLIER);
                $('#ddlSupplier').select2('val', Sup.SUPPLIER);
            }

            //Summer-note initilization
            //Init HTML EDITOR
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
            //Initilization of Summary note ends.

            //Function call for Loading default TandC 
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

            //Function for handling URL values starts here
            var Params = getUrlVars();
            console.log(isNaN(Params.UID));
            if (Params.UID != undefined && !isNaN(Params.UID)) {

                if (Params.MODE == 'clone') {
                    getRegister(true, Params.UID);
                } else if (Params.MODE == 'edit') {

                    getRegister(false, Params.UID);
                }
                else if (Params.MODE=='convert') {
                    getConvertData(Params.UID, Params.TYPE);
                }
                else {
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
            //Function for handling URL values ends here

            //Mail modal
            $("#modalMail").on('hidden.bs.modal', function () {
                $('.before-send').show();
                $('.after-send').hide();
                $('#txtMailAddress').val('');
            });

            //Date range
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
            $('#txtEntryDate').datepicker('setDate', today);
            $('#txtDueDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtEntryDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtEntryDate').datepicker('hide');
           });

            //save and print entry function call
            $('#btnSavePrint').click(function () {
                save(true);

            });

            //lookup initialization
            var companyId = $.cookie('bsl_1');
            lookup({
                textBoxId: 'txtChooser',
                url: $(hdApiUrl).val() + 'api/search/items?CompanyId=' + companyId + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'CostPrice', 'Description'],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate', 'Description'],
                visibility: [false, false, true, true, true, true, true, false],
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

            //function for add products to the table 
            function AddToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var taxper = parseFloat(tempItem.TaxPercentage);
                var rate = $('#listTable > tbody').children('td').eq(6).children('input').val();
                var qty = parseFloat($('#txtQuantity').val());
                var TaxAmount = parseFloat(((rate * (taxper / 100)) * qty).toFixed(2));
                var GrossAmount = parseFloat((qty * rate).toFixed(2));
                var NetAmount = parseFloat((GrossAmount + TaxAmount).toFixed(2));
                var description = tempItem.Description.replace(/\n/g, '<br/>');
                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null & !isNaN(qty)) {
                    var Rows = $('#listTable > tbody').children('tr');
                    var itemExists = false;
                    var rowOfItem;
                    $(Rows).each(function () {
                        var pedId = $(this).children('td').eq(0).text();
                        var instanceId = $(this).children('td').eq(14).text();
                        if (tempItem.ItemID == pedId && tempItem.InstanceId == instanceId) {
                            itemExists = true;
                            rowOfItem = this;
                        }

                    });

                    if (itemExists) {
                        var existingQty = parseFloat($(rowOfItem).children('td').eq(8).children('input').val());
                        var newQty = existingQty + qty;
                        $(rowOfItem).children('td').eq(8).replaceWith('<td style="text-align:right"><input type="number" class="edit-value" value="' + newQty + '"/></td>');
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
                        html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td style="display:none">0</td><td><b>' + tempItem.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + description + '</i><td>' + tempItem.ItemCode + '</td><td style="text-align:right">' + tempItem.TaxPercentage + '</td><td style="text-align:right">' + tempItem.MRP + '</td><td style="text-align:right"><input type="number" class="edit-value numberonly" value="' + tempItem.CostPrice + '"/></td><td style="text-align:right">0</td><td style="text-align:right"><input type="number" class="edit-value" value="' + qty + '"/> </td><td style="display:none"> <input type="number" class="edit-value" value="0"/></td><td style="text-align:right">' + TaxAmount + '</td><td style="text-align:right">' + GrossAmount + '</td><td style="text-align:right">' + NetAmount + '</td><td style="display:none">0</td><td style="display:none">' + tempItem.InstanceId + '</td><td style="display:none">0</td><!-- From GRN Or Not --><td style="text-align:right">  <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
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
            //addtolist function ends here

            //AddToList function call
            $('#btnAdd').click(function () {
                AddToList();
            });

            //Add Item to list with enter key           
            $('#txtQuantity').keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    AddToList();
                }
            });

            //Find Filter Function
            $(function () {
                $('#txtChooser').popover({
                    placement: 'bottom',
                    trigger: 'manual',
                    html: true,
                    content: $('#descWrap').html()
                })
            });

            //Save function call
            $('#btnSave').off().click(function () {
                save(false);
            });

            //link with PO function starts here
            $('#btnLinkPo').click(function () {
                if ($('#ddlSupplier').val() != 0) {
                    $('#mdlRequestDate').text($('#txtEntryDate').val());
                    $('#poTable tbody').children().remove();
                    $('#mdlSupplier').text('Nil');
                    $('#chkGetPO').prop('checked', false);
                    var supplierId = $('#ddlSupplier').val();
                    var locationId = $.cookie('bsl_2');
                    var companyId = $.cookie('bsl_1');
                    var financialYear = $.cookie('bsl_4');
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/GRN/GetSupplierWiseDetailsConfirmed/',
                        method: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        dataType: 'JSON',
                        data: JSON.stringify({ "SupplierId": supplierId, "LocationId": locationId }),
                        success: function (response) {
                            console.log(response);
                            var html = '';
                            $(response).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.GRNID + '</td>';
                                html += '<td style="display:none">' + this.SupplierId + '</td>';
                                html += '<td>' + this.InvoiceNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += '<td><input type="checkbox" class="checkbox chk-single"></td>';
                                html += '</tr>';
                                $('#mdlSupplier').text(this.Supplier);
                            });
                            $('#poTable tbody').append(html);
                            //Merge POs
                            $('#btnMergePO').click(function () {
                                rows = $('#poTable tbody').children('tr');
                                html = '';
                                $(rows).each(function () {
                                    if ($(this).children('td').eq(8).children('input').is(':checked')) {
                                        var poId = $(this).children('td').eq(0).text();
                                        for (var i = 0; i < response.length; i++) {
                                            if (response[i].GRNID == poId) {
                                                $(response[i].Products).each(function () {
                                                    html += '<tr>';
                                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                                    html += '<td style="display:none">' + this.RequestDetailId + '</td>';
                                                    html += '<td>' + this.Name + '</td>';
                                                    html += '<td>' + this.ItemCode + '</td>';
                                                    html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                                    html += '<td style="text-align:right">' + this.MRP + '</td>';
                                                    html += '<td style="text-align:right"><input type="number" class="edit-value numberonly" value="' + this.CostPrice + '"/></td>';
                                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                                    html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.ModifiedQuantity + '"/></td>';
                                                    html += '<td style="display:none"><input type="number" class="edit-value" value="0.00"/></td>';
                                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                                    html += '<td style="display:none">0</td>';
                                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                                    html += '<!-- Is GRN 1-GRN 0 other --><td style="display:none">1</td>';
                                                    html += '<td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                                    html += '</tr>'
                                                });
                                                $('#listTable tbody').children().remove();
                                                $('#listTable tbody').append(html);
                                                $('#ddlSupplier').prop('disabled', true);
                                                $('#poTable tbody').children().remove();
                                                //$('#chkGetPO').removeAttr('checked');
                                                $('#myModal').modal('hide');
                                                break;
                                            }
                                        }
                                    }
                                });
                                //Binding Event to remove button
                                $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                            });
                            //Merge Pos end here
                            $("#myModal").modal('show');
                        },
                        error: function (err) {
                            alert(err.responseText

                                );
                            loading('stop', null);
                        },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); },
                    });
                }
                else {
                    errorField('#ddlSupplier');
                }
            });
            //link with PO ends here

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

            //Print register
            $('#btnPrint').click(function () {
                var id = $('#hdId').val();
                if (id != 0) {
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    var url = "/purchase/Print/" + type + "/" + number + "/Entry?id=" + id + "&location=" + $.cookie('bsl_2');
                    PopupCenter(url, 'purchaseEntry', 800, 700);
                }
            });

            //To Get the data according to the checkbox(in link PO the modal)
            $('#chkGetPO').click(function ()
            {
                if ($(this).prop('checked') == true) {
                    //Gets the data from the Order Tables
                    if ($('#ddlSupplier').val() != 0) {
                        $('#mdlRequestDate').text($('#txtEntryDate').val());
                        $('#poTable tbody').children().remove();
                        $('#mdlSupplier').text('Nil');
                        var supplierId = $('#ddlSupplier').val();
                        var locationId = $.cookie('bsl_2');
                        var companyId = $.cookie('bsl_1');
                        var financialYear = $.cookie('bsl_4');
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/PurchaseQuote/GetSupplierWiseDetailsConfirmed/',
                            method: 'POST',
                            contentType: 'application/json;charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify({ "SupplierId": supplierId, "LocationId": locationId }),
                            success: function (response) {
                                var html = '';
                                $(response).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ID + '</td>';
                                    html += '<td style="display:none">' + this.SupplierId + '</td>';
                                    html += '<td>' + this.QuoteNumber + '</td>';
                                    html += '<td>' + this.EntryDateString + '</td>';
                                    html += '<td>' + this.DueDateString + '</td>';
                                    html += '<td>' + this.TaxAmount + '</td>';
                                    html += '<td>' + this.Gross + '</td>';
                                    html += '<td>' + this.NetAmount + '</td>';
                                    html += '<td><input type="checkbox" class="checkbox chk-single"></td>';
                                    html += '</tr>';
                                    $('#mdlSupplier').text(this.Supplier);
                                });
                                $('#poTable tbody').append(html);
                                //Merge POs
                                $('#btnMergePO').click(function () {
                                    rows = $('#poTable tbody').children('tr');
                                    html = '';
                                    $(rows).each(function () {
                                        if ($(this).children('td').eq(8).children('input').is(':checked')) {
                                            var poId = $(this).children('td').eq(0).text();
                                            for (var i = 0; i < response.length; i++) {
                                                if (response[i].ID == poId) {
                                                    $(response[i].Products).each(function () {
                                                        html += '<tr>';
                                                        html += '<td style="display:none">' + this.ItemID + '</td>';
                                                        html += '<td style="display:none">' + this.DetailsID + '</td>';
                                                        html += '<td>' + this.Name + '</td>';
                                                        html += '<td>' + this.ItemCode + '</td>';
                                                        html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                                        html += '<td style="text-align:right">' + this.MRP + '</td>';
                                                        html += '<td style="text-align:right"><input type="number" class="edit-value numberonly" value="' + this.CostPrice + '"/></td>';
                                                        html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                                        html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.Quantity + '"/></td>';
                                                        html += '<td style="display:none"><input type="number" class="edit-value" value="0.00"/></td>';
                                                        html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                                        html += '<td style="text-align:right">' + this.Gross + '</td>';
                                                        html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                                        html += '<td style="display:none">0</td>';
                                                        html += '<td style="display:none">' + this.InstanceId + '</td>';
                                                        html += '<!-- Is GRN 1-GRN 0 other --><td style="display:none">0</td>';
                                                        html += '<td style="text-align:right"><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                                        html += '</tr>'
                                                    });
                                                    $('#listTable tbody').children().remove();
                                                    $('#listTable tbody').append(html);
                                                    $('#ddlSupplier').prop('disabled', true);
                                                    $('#poTable tbody').children().remove();
                                                    $('#myModal').modal('hide');
                                                    break;
                                                }
                                            }
                                        }
                                    });
                                    //Binding Event to remove button
                                    $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                                });
                                //Merge Pos end here
                                $("#myModal").modal('show');
                            },
                            error: function (err) {
                                alert(err.responseText

                                    );
                                loading('stop', null);
                            },
                            beforeSend: function () { loading('start', null) },
                            complete: function () { loading('stop', null); },
                        });
                    }
                    else {
                        errorField('#ddlSupplier');
                    }
                }
                else {
                    //Gets The Data From the GRN table
                    if ($('#ddlSupplier').val() != 0) {
                        $('#mdlRequestDate').text($('#txtEntryDate').val());
                        $('#poTable tbody').children().remove();
                        $('#mdlSupplier').text('Nil');
                        var supplierId = $('#ddlSupplier').val();
                        var locationId = $.cookie('bsl_2');
                        var companyId = $.cookie('bsl_1');
                        var financialYear = $.cookie('bsl_4');
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/GRN/GetSupplierWiseDetailsConfirmed/',
                            method: 'POST',
                            contentType: 'application/json;charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify({ "SupplierId": supplierId, "LocationId": locationId }),
                            success: function (response) {
                                console.log(response);
                                var html = '';
                                //Merges the Data into Table
                                $(response).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.GRNID + '</td>'; //Main ID
                                    html += '<td style="display:none">' + this.SupplierId + '</td>';
                                    html += '<td>' + this.InvoiceNo + '</td>';
                                    html += '<td>' + this.EntryDateString + '</td>';
                                    html += '<td>' + this.EntryDateString + '</td>';
                                    html += '<td>' + this.TaxAmount + '</td>';
                                    html += '<td>' + this.Gross + '</td>';
                                    html += '<td>' + this.NetAmount + '</td>';
                                    html += '<td><input type="checkbox" class="checkbox chk-single"></td>';
                                    html += '</tr>';
                                    $('#mdlSupplier').text(this.Supplier);
                                });
                                $('#poTable tbody').append(html);
                                //Merge POs
                                $('#btnMergePO').click(function () {
                                    rows = $('#poTable tbody').children('tr');
                                    html = '';
                                    $(rows).each(function () {
                                        if ($(this).children('td').eq(8).children('input').is(':checked')) {
                                            var poId = $(this).children('td').eq(0).text();
                                            for (var i = 0; i < response.length; i++) {
                                                if (response[i].GRNID == poId) {
                                                    $(response[i].Products).each(function () {
                                                        html += '<tr>';
                                                        html += '<td style="display:none">' + this.ItemID + '</td>';
                                                        html += '<td style="display:none">' + this.RequestDetailId + '</td>';
                                                        html += '<td>' + this.Name + '</td>';
                                                        html += '<td>' + this.ItemCode + '</td>';
                                                        html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                                        html += '<td>' + this.MRP + '</td>';
                                                        html += '<td><input type="number" class="edit-value numberonly" value="' + this.CostPrice + '"/></td>';
                                                        html += '<td>' + this.Quantity + '</td>';
                                                        html += '<td><input type="number" class="edit-value" value="' + this.ModifiedQuantity + '"/></td>';
                                                        html += '<td style="display:none"><input type="number" class="edit-value" value="0.00"/></td>';
                                                        html += '<td>' + this.TaxAmount + '</td>';
                                                        html += '<td>' + this.Gross + '</td>';
                                                        html += '<td>' + this.NetAmount + '</td>';
                                                        html += '<td style="display:none">0</td>';
                                                        html += '<td style="display:none">' + this.DetailsID + '</td>';
                                                        html += '<!-- Is GRN 1-GRN 0 other --><td style="display:none">1</td>';
                                                        html += '<td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                                        html += '</tr>'
                                                    });
                                                    $('#listTable tbody').children().remove();
                                                    $('#listTable tbody').append(html);
                                                    $('#ddlSupplier').prop('disabled', true);
                                                    $('#poTable tbody').children().remove();
                                                    $('#myModal').modal('hide');
                                                    break;
                                                }
                                            }
                                        }
                                    });
                                    //Binding Event to remove button
                                    $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                                });
                                //Merge Pos end here
                                $("#myModal").modal('show');
                            },
                            error: function (err) {
                                alert(err.responseText

                                    );
                                loading('stop', null);
                            },
                            beforeSend: function () { loading('start', null) },
                            complete: function () { loading('stop', null); },
                        });
                    }
                    else {
                        errorField('#ddlSupplier');
                    }
                }

            });

            //Function for Saving the register
            function save(printSave) {
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
                          var invoicedate = $('#txtDueDate').val();
                          var narration = $('#txtNarration').val();
                          var discount = $('#txtDiscount').val();
                          var rOff = $('#txtRoundOff').val();
                          var InvoiceNo = $('#txtInvoiceNo').text();
                          var LocationId = $.cookie('bsl_2');
                          var CompanyId = $.cookie('bsl_1');
                          var FinancialYear = $.cookie('bsl_4');
                          var createdBy = $.cookie('bsl_3');
                          var TandC = $('#dvTandC').summernote('code');
                          var PaymentTerms = $('#dvPaymentTerm').summernote('code');
                          var Validity = $('#txtValidity').val();
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
                          var ETA = $('#txtETA').val();
                          for (var i = 0; i < tr.length; i++) {
                              var itemId = $(tr[i]).children('td').eq(0).text();
                              var pqdId = $(tr[i]).children('td').eq(13).text();
                              var poId = $(tr[i]).children('td').eq(1).text();
                              var desc = $(tr[i]).children('td').eq(2).children('i').html();
                              var mrp = $(tr[i]).children('td').eq(5).text();
                              var rate = $(tr[i]).children('td').eq(6).children('input').val();
                              var qty = $(tr[i]).children('td').eq(8).children('input').val();
                              var instanceId = $(tr[i]).children('td').eq(14).text();
                              var IsGRN = $(tr[i]).children('td').eq(15).text();
                              var IsMigrated;
                              var detail = { "Quantity": qty };
                              detail.DetailsID = instanceId;
                              detail.ItemID = itemId;
                              detail.QuoteDetailId = pqdId;
                              detail.MRP = mrp;
                              detail.CostPrice = rate;
                              detail.QuoteDetailId = poId;
                              detail.IsGRN = IsGRN;
                              detail.Description = desc;
                              detail.AffectedinStock = $('#hdnStockAffect').val();
                              console.log(detail);
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
                              arr.push(detail);
                          }
                          data.BillingAddress = address;
                          data.ID = $('#hdId').val();
                          data.SupplierId = $('#ddlSupplier').val();
                          data.InvoiceNo = $('#txtInvoiceNo').val();
                          data.RoundOff = rOff;
                          data.CostCenterId = $('#ddlCostCenter').val();
                          data.JobId = $('#ddlJob').val();
                          data.EntryDate = entryDate;
                          data.IsMigrated = false;
                          data.EntryDateString = entryDate;
                          data.EntryDate = entryDate;
                          data.Discount = discount;
                          data.InvoiceDateString = invoicedate;
                          data.InvoiceDate = invoicedate;
                          data.Products = arr;
                          data.Narration = narration;
                          data.ModifiedBy = $.cookie('bsl_3');
                          data.LocationId = $.cookie('bsl_2');
                          data.CompanyId = $.cookie('bsl_1');
                          data.FinancialYear = $.cookie('bsl_4');
                          data.CreatedBy = $.cookie('bsl_3');
                          data.UserId = $.cookie('bsl_3');
                          data.Validity = Validity;
                          data.TermsandConditon = TandC;
                          data.Payment_Terms = PaymentTerms;
                          data.ETA = ETA;
                          $.ajax({
                              url: $(hdApiUrl).val() + 'api/PurchaseEntry/Save',
                              method: 'POST',
                              data: JSON.stringify(data),
                              contentType: 'application/json',
                              dataType: 'JSON',
                              success: function (response) {
                                  console.log(response);
                                  if (response.Success) {
                                      successAlert(response.Message);
                                      $('#lblOrderNo').text(response.Object.OrderNo);
                                      if (printSave == true) {
                                          var type = JSON.parse($('#hdSettings').val()).TemplateType;
                                          var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                                          var url = "/Purchase/Print/" + type + "/" + number + "/entry?id=" + response.Object.Id + "&location=" + $.cookie('bsl_2');
                                          PopupCenter(url, 'purchaseEntry', 800, 700);
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
                              complete: function () { miniLoading('stop') }

                          });
                      }

                  });
            }
            //Save Function Ends here

            //Find function starts here
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
                resetRegister();
                $('#findModal').modal('show');
                refreshTable(null, null, null);
                function refreshTable(supplierID, fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/purchaseEntry/Get?SupplierId=' + supplierID + '&from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var supplierId = $('#ddlSupplier').val();
                            var invoiceNo = $('#txtInvoiceNo').val();
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.EntryNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.Supplier + '</td>';
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

            //Add mail address to mail modal textbox
            $('#btnMail').click(function () {
                $('#txtMailAddress').val($('#hdEmail').val());

            });

            //Sending mail
            $(document).on('click', '#btnSend', function (e) {
                $('.before-send').fadeOut('slow', function () {
                    $('.after-send').fadeIn();
                    var toaddr = $('#txtMailAddress').val();
                    var id = $('#hdId').val();
                    var modifiedBy = $.cookie('bsl_3');
                    var type = JSON.parse($('#hdSettings').val()).TemplateType;
                    var number = JSON.parse($('#hdSettings').val()).TemplateNumber;
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/PurchaseEntry/SendMail?purchaseid=' + id + '&toaddress=' + toaddr + '&userid=' + $.cookie('bsl_3'),
                        method: 'POST',
                        datatype: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify($(location).attr('protocol') + '//' + $(location).attr('host') + '/Purchase/Print/' + type + '/' + number + '/Entry?id=' + id + '&location=' + $.cookie('bsl_2')),
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

            //BtnNew for reset the page
            $('#btnNew').click(function () {
                resetRegister();
            });

            //Function to get the data for converting
            //Parameter 1.ID --->ID of the register which is to be converted
            //Parameter 2.TYPE-->TYPE of the Register Eg:-Estimate:EST
            function getConvertData(id, TYPE) {
                if (TYPE == 'PQT') {
                    resetRegister();
                    $('#hdnConvertedFrom').val(TYPE);
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/purchasequote/get/' + id,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            try {
                                var html = '';
                                var register = response;
                                LoadAddress(register.SupplierId);
                                //$('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                                //$('#txtDueDate').datepicker("update", new Date(register.DueDateString));
                                if (register.IsApproved == 1) {
                                    $('#btnConfirm').prop('disabled', true)
                                    $('#btnMail').show();
                                    $('#btnPrint').show();
                                }
                                else {
                                    $('#btnPrint').hide();
                                    $('#btnMail').hide();
                                    $('#btnConfirm').prop('disabled', false);
                                }
                                $('#txtNarration').val(register.Narration);
                                $('#ddlLocation').val(register.LocationId);
                                $('#ddlSupplier').val(register.SupplierId);
                                $('#ddlSupplier').select2('val', register.SupplierId);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                $('#ddlJob').select2('val', register.JobId);
                                $('#txtETA').val(register.ETA);
                                var html = '';
                                console.log(register);
                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                    html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                    html += '<td>' + this.ItemCode + '</td>';
                                    html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                    html += '<td style="text-align:right">' + this.MRP + '</td>';
                                    html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                    html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.Quantity + '"/>'
                                    html += '<td style="display:none"><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                    html += '<td style="display:none">' + this.RequestDetailId + '</td>';
                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                    html += '<!-- GRN if 1 otherwise 0 --><td style="display:none">0</td>';
                                    html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '</tr>';
                                });
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
                                $('#lblGross').val(register.Gross);
                                //$('#lblOrderNo').text(register.QuoteNumber);
                                $('#lblTotalAmount').val(register.Gross);
                                $('#lblTaxAmount').val(register.TaxAmount);
                                $('#lblNetAmount').val(register.NetAmount);
                                $('#txtRoundOff').val(register.RoundOff);
                                $('#listTable tbody').append(html);
                                $('#findModal').modal('hide');
                                //binding delete
                                $('.delete-row').click(function () {
                                    $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                                });


                            }
                            catch (err) {
                                console.log(err);

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
                        complete: function () { miniLoading('stop') }
                    });
                }
                else if (TYPE=='GRN') {
                    resetRegister();
                    $('#hdnConvertedFrom').val(TYPE);
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/GRN/get/' + id,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            try {
                                var register = response;
                                console.log(response);
                                var html = '';
                                LoadAddress(register.SupplierId);
                                //$('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                                //$('#txtDueDate').datepicker("update", new Date(register.EntryDateString));
                                $('#txtNarration').val(register.Narration);
                                $('#ddlLocation').val(register.LocationId);
                                $('#ddlSupplier').val(register.SupplierId);
                                $('#ddlSupplier').select2('val', register.SupplierId);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                html = '';
                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                    html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                    html += '<td>' + this.ItemCode + '</td>';
                                    html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                    html += '<td style="text-align:right">' + this.CostPrice + '</td>';
                                    html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                    html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                    html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.Quantity + '"/>'
                                    html += '<td style="display:none"><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                    html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                    html += '<td style="text-align:right">' + this.Gross + '</td>';
                                    html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                    html += '<td style="display:none">' + this.RequestDetailId + '</td>';
                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                    html += '<!-- GRN if 1 otherwise 0 --><td style="display:none">1</td>';
                                    html += '<td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '</tr>';
                                });
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
                                $('#lblGross').val(register.Net);
                                $('#ddlJob').select2('val', register.JobId);
                                //$('#lblOrderNo').text(register.InvoiceNo);
                                $('#lblTotalAmount').val(register.Net);
                                $('#lblTaxAmount').val(register.TaxAmount);
                                $('#lblNetAmount').val(register.Net);
                                $('#txtRoundOff').val(register.RoundOff);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                $('#ddlJob').select2('val', register.JobId);
                                $('#hdnStockAffect').val(true);
                                $('#listTable tbody').append(html);
                                $('#findModal').modal('hide');
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
            }

            //Function loading single entry
            function getRegister(isClone, id) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/purchaseEntry/get/' + id,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        try {
                            var register = response;
                            LoadAddress(register.SupplierId);
                            $('#txtEntryDate').datepicker("update", new Date(register.EntryDateString));
                            $('#txtDueDate').datepicker("update", new Date(register.InvoiceDateString));
                            $('#txtNarration').val(register.Narration);
                            $('#txtInvoiceNo').val(register.InvoiceNo);
                            $('#ddlSupplier').val(register.SupplierId);
                            $('#ddlSupplier').select2('val', register.SupplierId);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);

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

                            var html = '';
                            $(register.Products).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td style="display:none">' + this.QuoteDetailId + '</td>';
                                html += '<td><b>' + this.Name + '</b><br/><i contenteditable="true" spellcheck="false">' + this.Description + '</i></td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td style="text-align:right">' + this.TaxPercentage + '</td>';
                                html += '<td style="text-align:right">' + this.MRP + '</td>';
                                html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.CostPrice + '"/></td>';
                                html += '<td style="text-align:right">' + this.Quantity + '</td>';
                                html += '<td style="text-align:right"><input type="number" class="edit-value" value="' + this.Quantity + '"/>';
                                html += '<td style="display:none"><input type="number" class="edit-value" value="' + this.SellingPrice + '"/></td>';
                                html += '<td style="text-align:right">' + this.TaxAmount + '</td>';
                                html += '<td style="text-align:right">' + this.Gross + '</td>';
                                html += '<td style="text-align:right">' + this.NetAmount + '</td>';
                                html += '<td style="display:none">0</td>';
                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                html += '<!-- GRN if 1 otherwise 0 --><td style="display:none">' + this.IsGRN + '</td>';
                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                html += '</tr>';
                            });
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
                            $('#txtValidity').val(register.Validity);
                            $('#txtETA').val(register.ETA);
                            $('#lblGross').val(register.Gross);
                            $('#lblTotalAmount').val(register.Gross);
                            $('#lblTaxAmount').val(register.Gross);
                            $('#lblNetAmount').val(register.NetAmount);
                            $('#txtDiscount').val(register.Discount);
                            $('#txtRoundOff').val(register.RoundOff);
                            $('#listTable tbody').append(html);
                            $('#lblOrderNo').text(register.EntryNo);
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
                        loading('stop', null);
                    },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });
            }

            //Reset the Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#ddlSupplier').prop('disabled', false);
                $('#txtEntryDate').datepicker('setDate', today);
                $('#txtDueDate').datepicker('setDate', today);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#btnSavePrint').html('<i class=\"ion ion-printer"\></i>&nbsp;Save & Print');
                $('#btnPrint').hide();
                LoadTandCSettings();
                $('#btnMail').hide();
                $('#ddlSupplier').select2('val', 0);
                $('#ddlCostCenter').select2('val', 0);
                $('#ddlJob').select2('val', 0);
                $('#hdnConvertedFrom').val('');
                $('#hdnStockAffect').val(false);
            }
            //Reset ends here

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

                        var id = $('#hdId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/PurchaseEntry/Delete/' + id,
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
            //delete function ends here

            //Load states
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
                            $('#ddlCountry').val(response.CountryId);
                            loadStates(response.StateId);
                            $('#ddlSalutation').val(response.Salutation);
                            $('#txtZipCode').val(response.ZipCode);
                            LoadAddress(id);
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                }
            });

            //Hide address block
            $('.change-address').click(function () {
                $('.address-tab').removeClass('hidden');
            });

            //Load address
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

            $('#ddlCountry').change(function () {
                loadStates(0);
            })

            //Functions to Add Masters
            $('#addnewSupplier').click(function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewSupplier(Url, User, Company, function (id, name) {
                    $('#ddlSupplier').append('<option value="' + id + '">' + name + '</option>')
                    $('#ddlSupplier').select2('val', id);
                    $('#ddlSupplier').trigger('change');
                });
            });

            //Function to add item or service
            $('#addnewItem').click(function () {
                $('#quickAddLink').fadeToggle('hidden');
            });

            //Function to add Service
            $('#addService').click(function () {
                $('#quickAddLink').fadeToggle('hidden');
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewService(Url, User, Company, function (id, name) {
                    console.log(id + ':' + name);
                });
            });

            //Function to add Item
            $('#addItem').click(function () {
                $('#quickAddLink').fadeToggle('hidden');
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                createNewItem(Url, User, Company, function (id, name) {
                    console.log(id + ':' + name);
                });
            });

            //Adding quick instance
            $('body').on('click', '.quick-add-instance', function () {
                var Url = $('#hdApiUrl').val();
                var User = $.cookie('bsl_3');
                var Company = $.cookie('bsl_1');
                var tr = $(this).closest('tr');
                var itemId = tr.children('td:nth-child(1)').text();
                var itemName = tr.children('td:nth-child(3)').text();
                var successCallback = function (instanceId, mrp, cp, sp) {
                    tr.children('td:nth-child(6)').text(mrp);
                    tr.children('td:nth-child(15)').text(instanceId);
                    tr.children('td:nth-child(7)').children('input').val(cp);
                }
                createNewInstance(Url, User, Company, itemId, itemName, successCallback)
            });
            //End of quick instance

            //Load supplier address
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

        }); //Document ready function ends here
    </script>
    <%--summer note linking--%>
    <link href="../Theme/assets/summernote/summernote.css" rel="stylesheet" />
    <script src="../Theme/assets/summernote/summernote.min.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/Sections/Service.js"></script>
    <script src="../Theme/Sections/Supplier.js"></script>
    <script src="../Theme/Sections/Items.js"></script>
    <script src="../Theme/Sections/Instances.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <!-- Date Range Picker -->
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>

</asp:Content>
