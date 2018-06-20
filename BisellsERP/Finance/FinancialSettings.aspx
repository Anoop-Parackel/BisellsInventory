<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinancialSettings.aspx.cs" Inherits="BisellsERP.Finance.FinancialSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .addInstance, .deleteInstance, .updateInstance, .editInstance, .cancelInstance {
            cursor: pointer;
        }

        #wrapper, body {
            overflow: hidden;
        }

        .masters-wrap > div {
            height: calc(100vh - 83px);
        }

            .masters-wrap > div .nav {
                height: calc(100vh - 135px);
                background-color: #fff;
                box-shadow: 0 2px 1px 0 #ccc;
            }

        .masters-wrap .nav > li {
            border-bottom: 1px solid #f3f3f3;
        }

            .masters-wrap .nav > li > a {
                color: #90A4AE !important;
            }

                .masters-wrap .nav > li > a:hover {
                    color: #757575 !important;
                }

            .masters-wrap .nav > li.active > a {
                color: #3cba9f !important;
            }

            .masters-wrap .nav > li.active {
                border-left: 3px solid #3cba9f;
            }

            .masters-wrap .nav > li > a:focus, .nav > li > a:hover {
                background-color: transparent;
            }

            .masters-wrap .nav > li > a {
                line-height: 35px;
            }

        .tab-content > .tab-pane > .panel {
            height: calc(100vh - 135px);
            box-shadow: 0 2px 1px 0 #ccc;
        }

        .sett-title {
            border-bottom: 1px dashed #ececec;
            padding-top: 20px;
            padding-bottom: 5px;
            margin-top: 0;
            margin-bottom: 15px;
            color: #3cba9f;
            margin-left: -5px;
        }


        .tab-pane label {
            /*color: #78909c;*/
            font-weight: 100;
            text-align: left !important;
            font-size: 12px;
        }

            .tab-pane label > i {
                margin-left: 2px;
                color: #B0BEC5;
            }

        .tab-pane .form-group {
            margin-bottom: 5px;
        }

        li.list-break > p {
            margin-bottom: 0;
            background-color: #FAFAFA;
            padding: 8px;
        }

        .treeview-css ul, .treeview-css li {
            padding: 0;
            margin: 0;
            list-style: none;
        }

        .treeview-css input {
            position: absolute;
            opacity: 0;
        }

        .treeview-css {
            font: normal 15px "Segoe UI", Arial, Sans-serif;
            -moz-user-select: none;
            -webkit-user-select: none;
            user-select: none;
        }

            .treeview-css a {
                color: #00f;
                text-decoration: none;
            }

                .treeview-css a:hover {
                    text-decoration: underline;
                }

            .treeview-css input + label + ul {
                margin: 0 0 0 22px;
            }

            .treeview-css input ~ ul {
                display: none;
            }

            .treeview-css label,
            .treeview-css label::before {
                cursor: pointer;
            }

            .treeview-css input:disabled + label {
                cursor: default;
                opacity: .6;
            }

            .treeview-css input:checked:not(:disabled) ~ ul {
                display: block;
            }

            .treeview-css label,
            .treeview-css label::before {
                background: url("/Theme/images/treeview-ico-sprite.png") no-repeat;
            }

                .treeview-css label,
                .treeview-css a,
                .treeview-css label::before {
                    display: inline-block;
                    height: 16px;
                    line-height: 16px;
                    vertical-align: middle;
                    font-size: 15px;
                    font-weight: 600;
                }

                    .treeview-css label::before {
                        content: "";
                        width: 16px;
                        margin: 0 2px 0 0;
                        vertical-align: middle;
                        background-position: 0 -32px;
                    }

            .treeview-css input:checked + label::before {
                background-position: 0 -16px;
            }

        .edit-group {
            cursor: pointer;
        }

        .headGroup {
            font-weight: 600;
            font-size: 15px;
        }

        .child-head {
            font-size: 14px;
            margin-left: 10px !important;
            margin-top: 1px !important;
        }

        .Report-Table {
            margin-top: 5px;
            height: 380px;
            overflow-x: hidden;
        }
        /* webkit adjacent element selector bugfix */
        @media screen and (-webkit-min-device-pixel-ratio:0) {
            .treeview-css {
                -webkit-animation: webkit-adjacent-element-selector-bugfix infinite 1s;
            }

            @-webkit-keyframes webkit-adjacent-element-selector-bugfix {
                from {
                    padding: 0;
                }

                to {
                    padding: 0;
                }
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
    <div class="masters-wrap">
        <div class="col-sm-3 p-0">
            <h3 class="m-t-0">Financial Settings</h3>
            <div class="tabs-vertical-env">
                <ul class="nav" id="menu">
                    <li class="active" id="Group">
                        <a href="#v-1" data-toggle="tab" aria-expanded="true">Group</a>
                    </li>
                    <li class="hidden" id="Head">
                        <a href="#v-2" data-toggle="tab" aria-expanded="true">Account Head</a>
                    </li>
                    <li class="" id="Type">
                        <a href="#v-3" data-toggle="tab" aria-expanded="true">Voucher Types</a>
                    </li>
                    <li class="" id="VSettings">
                        <a href="#v-4" data-toggle="tab" aria-expanded="true">Voucher Settings</a>
                    </li>
                    <li class="" id="RSettings">
                        <a href="#v-5" data-toggle="tab" aria-expanded="true">Report Settings</a>
                    </li>
                    <li class="" id="Cost">
                        <a href="#v-6" data-toggle="tab" aria-expanded="true">Cost Center</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="col-sm-9 p-0">
            <div class="col-md-12 m-t-40">
                <div class="tab-content">

                    <%-- Account Group --%>
                    <div class="tab-pane active" id="v-1">
                        <div class="panel">
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Group</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ClientIDMode="Static" runat="server" CssClass="form-control" ID="txtAccountGroupName" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Parent Group</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" CssClass="form-control" ClientIDMode="Static" ID="ddlParentGroup">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Description</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" TextMode="MultiLine" cols="50" Rows="3" ClientIDMode="Static" class="form-control" ID="txtAccountGroupDescription"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlGroupStatus" CssClass="form-control" ClientIDMode="Static" runat="server">
                                                                <asp:ListItem Value="0">Enabled</asp:ListItem>
                                                                <asp:ListItem Value="1">Disabled</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Is Affect Gross Profit</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlIsAffectGP" CssClass="form-control" ClientIDMode="Static" runat="server">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Account Nature</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlAccountNatureGroup" CssClass="form-control" ClientIDMode="Static" runat="server">
                                                                <asp:ListItem Value="0">Asset</asp:ListItem>
                                                                <asp:ListItem Value="1">Income</asp:ListItem>
                                                                <asp:ListItem Value="3">Liability</asp:ListItem>
                                                                <asp:ListItem Value="2">Expense</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddGroup"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7" id="Grouptree">
                                        <h5 class="sett-title p-t-0">Groups</h5>
                                        <asp:TreeView ID="moduleTree" OnSelectedNodeChanged="moduleTree_SelectedNodeChanged" ClientIDMode="Static" runat="server" CssClass="nunito-font nopost" Target="_self" Font-Size="medium" ForeColor="#004767" LineImagesFolder="~/TreeLineImages">
                                            <LeafNodeStyle HorizontalPadding="5px" VerticalPadding="3px" CssClass="nopost" />
                                            <ParentNodeStyle Font-Bold="True" CssClass="nopost" />
                                            <RootNodeStyle Font-Bold="True" CssClass="nopost" />
                                            <SelectedNodeStyle Font-Bold="True" CssClass="edit-entry-group" ForeColor="#41BC76" />
                                        </asp:TreeView>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <!--Account Head -->
                    <div class="tab-pane hidden" id="v-2">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Account Head</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtAccountheadName" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Account Group</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlParentGroups">
                                                                <option value="0">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Opening Date</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtHeadOpeningDate" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Opening Balance</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtHeadOpeningBalance" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Category</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlHeadCategory">
                                                                <option value="0">Asset</option>
                                                                <option value="1">Supplier Payments</option>
                                                                <option value="2">Bank Deposit</option>
                                                                <option value="3">Purchase Head</option>
                                                                <option value="4">Customer Reciept</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlHeadStatus">
                                                                <option value="0">Enabled</option>
                                                                <option value="1">Disabled</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Is Debit</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlHeadisDebit">
                                                                <option value="0">No</option>
                                                                <option value="1">Yes</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Account Nature</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlHeadAccountNature" disabled="disabled">
                                                                <option selected="selected" value="0">Asset</option>
                                                                <option value="1">Income</option>
                                                                <option value="2">Liability</option>
                                                                <option value="3">Expense</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Account Type</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlAccountType">
                                                                <option value="0">Normal</option>
                                                                <option value="1">Bank</option>
                                                                <option value="2">Cash</option>
                                                                <option value="3">Stock</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <h5 class="sett-title p-t-0">Other Details</h5>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Contact Person</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtHeadContactPerson" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Phone</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" class="form-control" id="txtHeadContactPhone" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Email</label>
                                                        <div class="col-sm-8">
                                                            <input type="email" class="form-control" id="txtHeadContactEmail" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Address</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control" id="txtHeadContactAddress" cols="50" rows="3"></textarea>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Description</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control" id="txtHeadDescription" cols="50" rows="3"></textarea>
                                                        </div>
                                                    </div>
                                                    <h5 class="sett-title p-t-0">Settings</h5>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Default Reverse Head</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control" id="ddlReverseHead">
                                                                <option value="0">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Data SQL</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control" id="txtDataSQL" cols="50" rows="3"></textarea>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Transaction SQL</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control" id="txtTransactionSQL" cols="50" rows="3"></textarea>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Amount SQL</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtAmountSQL" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Ref Table Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtRefTable" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Data ID Field</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtIDField" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Data Name Field</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtNameField" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddHead"><i class="fa fa-plus"></i>&nbsp;<span>Add Account Head</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7" id="HeadTree"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--VOUCHER TYPE -->
                    <div class="tab-pane" id="v-3">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Voucher Type</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTypeName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Numbering Starts From</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtStartsFrom" placeholder="Order" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlTypeStatus">
                                                                <option value="1">Active</option>
                                                                <option value="0">Inactive</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Numbering</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlNumbering">
                                                                <option value="1">Automatic</option>
                                                                <option value="0">Manual</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Numbering Restarts On</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlNumberingRestarts">
                                                                <option value="1">Yearly</option>
                                                                <option value="2">Monthly</option>
                                                                <option value="0">Never</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Is Debit</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlIsDebitType">
                                                                <option value="1">Yes</option>
                                                                <option value="0">No</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Display In Journal</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlDisplayInJournal">
                                                                <option value="1">Yes</option>
                                                                <option value="0">No</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddType"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Voucher List</h5>
                                        <table id="tableVoucherType" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Numbering Starts From</th>
                                                    <th>Status</th>
                                                    <th>#</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--VOUCHER SETTINGS -->
                    <div class="tab-pane" id="v-4">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h5 class="sett-title p-t-0 col-sm-5">Update Voucher Settings</h5>
                                    </div>
                                    <div class="col-sm-12" style="margin-top: -15px">
                                        <div class="col-sm-8">
                                            <div class="col-sm-6">
                                                <div class="form-horizontal">
                                                    <label class="control-label">Voucher Type</label>
                                                    <select id="ddlVoucherTypeSettings" class="form-control">
                                                        <option value="0">--Select--</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 m-t-20">
                                                <div class="checkbox checkbox-primary pull-right">
                                                    <input type="checkbox" id="chkByGroup" /><label>By Group</label>
                                                </div>
                                                <input type="hidden" value="0" id="hdnIsGroup" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="btn-toolbar m-t-20 pull-right">
                                                <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddVoucherSettings"><i class=""></i>&nbsp;<span>Update</span></button>
                                                <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 Head-Table Report-Table">
                                        <table class="table table-responsive" id="SettingsTable">
                                            <thead>
                                                <tr>
                                                    <th class="hidden">ID</th>
                                                    <th>Account Heads</th>
                                                    <th>
                                                        <div class="checkbox checkbox-primary">
                                                            <input type="checkbox" id="chkDebit" class="checkbox checkbox-primary" /><label>Dr</label>
                                                        </div>
                                                    </th>
                                                    <th>
                                                        <div class="checkbox checkbox-primary">
                                                            <input type="checkbox" id="chkCredit" class="checkbox checkbox-primary" /><label>Cr</label>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-12 Report-Table Group-Table hidden">
                                        <table class="table table-responsive" id="GroupTable">
                                            <thead>
                                                <tr>
                                                    <th class="hidden">ID</th>
                                                    <th>Account Groups</th>
                                                    <th>
                                                        <div class="checkbox checkbox-primary">
                                                            <input type="checkbox" id="chkisDebitGroup" class="checkbox checkbox-primary" /><label>Dr</label>
                                                        </div>
                                                    </th>
                                                    <th>
                                                        <div class="checkbox checkbox-primary">
                                                            <input type="checkbox" id="chkisCreditGroup" class="checkbox checkbox-primary" /><label>Cr</label>
                                                        </div>
                                                    </th>
                                                    <th class="hidden"></th>
                                                    <th class="hidden"></th>
                                                    <th class="hidden"></th>
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
                    <!--REPORT SETTINGS -->
                    <div class="tab-pane" id="v-5">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Report Settings</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Report Type:</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlReportType">
                                                                <option value="0">Trading A/C</option>
                                                                <option value="1">Profit & Loss A/C</option>
                                                                <option value="2">Balance Sheet</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Account Group</label>
                                                        <div class="col-sm-8">
                                                            <select runat="server" class="form-control input-sm" id="ddlAccountGroupReport" clientidmode="static">
                                                                <option value="0">--select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Side</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlReportSide">
                                                                <option value="0">Left</option>
                                                                <option value="1">Right</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Is Minus</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlIsMinus">
                                                                <option value="0">Yes</option>
                                                                <option value="1" selected="selected">No</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Order</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtReportOrder" placeholder="Order" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddReportSettings"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Voucher List</h5>
                                        <table id="tableReportSettings" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Order</th>
                                                    <th>Postion</th>
                                                    <th>#</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--COST CENTER -->
                    <div class="tab-pane" id="v-6">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Cost Center</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" class="form-control" id="txtCostCentrName" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlCostCenterStatus">
                                                                <option value="1">Active</option>
                                                                <option value="0">Inactive</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddCostCenter"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Cost Centers List</h5>
                                        <table id="tableCostCenter" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Status</th>
                                                    <th>#</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnTypeID" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnCostCenterID" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnReportSettings" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnGroupID" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnHeadID" runat="server" Value="0" ClientIDMode="Static" />
    </div>
    <script>
        $(document).ready(function () {

            //Scrollbar
            $(".tab-content > .tab-pane > .panel").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });

            $(".Report-Table").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });


            //Fetching API url
            var apiurl = $('#hdApiUrl').val();
            getTree();

            LoadAdditionaldata();
            LoadVoucherTypes();


            //Event to trigger the selected li
            $('#menu li').click(function () {
                var select = $(this).attr('id');
                switch (select) {
                    case 'Group':
                        getTree();//Loads the tree view in group tab
                        break;
                    case 'Head':
                        AccountHeadTree();
                        LoadAdditionaldata();
                        break;
                    case 'Type':
                        RefreshTableVoucherType();
                        break;
                    case 'VSettings':
                        LoadVoucherSettings();
                        break;
                    case 'RSettings':
                        RefreshTableReport();
                        getAccountGroups();
                        break;
                    case 'Cost':
                        RefreshTableCostCenter();
                        break;
                    default:
                        break;

                }

            });
            //**Functions To Load Tables or Trees**//

            //Voucher Type
            function RefreshTableVoucherType() {

                $.ajax({
                    url: apiurl + '/api/VoucherType/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableVoucherType').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['copy', 'excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                               { data: 'Numbering Starts From' },
                                { data: 'Status', 'render': function (status) { if (status == 0) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-type"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-type"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                });
            }

            //Cost Center
            function RefreshTableCostCenter() {
                $.ajax({
                    url: apiurl + '/api/CostCenter/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableCostCenter').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['copy', 'excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-cost"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-cost"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                });
            }

            //Report Settings
            function RefreshTableReport() {
                $.ajax({
                    url: apiurl + '/api/ReportSettings/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        $('#tableReportSettings').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['copy', 'excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'AccountGroupName' },
                                { data: 'Order' },
                                { data: 'Position', 'render': function (status) { if (status == 0) return '<label class="label label-default">Left </label>'; else return '<label class="label label-default">Right</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-report"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-report"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                });
            }

            //funtion to get account group tree
            function getTree() {
                var company = $.cookie("bsl_1");
                $('.treeview-css').remove();
                var html = '<div class="treeview-css"><ul>';
                $.ajax({
                    url: apiurl + '/api/AccountGroup/GetTree?company=' + company,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        if (response[0].HasChildren) {
                            for (var i = 0; i < response.length; i++) {
                                for (var j = 0; j < response[i].Children.length; j++) {
                                    if (response[i].Children[j].HasChildren) {
                                        //Top Tree nodes(Parent Nodes)
                                        html += '<li><input type="checkbox" checked="checked" /><label for="' + response[i].Children[j].ModuleId + '" class="edit-group" id="' + response[i].Children[j].ModuleId + '">' + response[i].Children[j].Name + '</label><ul>'
                                        for (var k = 0; k < response[i].Children[j].Children.length; k++) {
                                            html += '<li class="edit-group child-head" id="' + response[i].Children[j].Children[k].ModuleId + '">' + response[i].Children[j].Children[k].Name + '</li>';
                                        }
                                        html += '</ul>';
                                    }
                                    else {
                                        //For childrens
                                        html += '<li class="edit-group headGroup" id="' + response[i].Children[j].ModuleId + '"><input type="checkbox" checked="checked" /><label for="' + response[i].Children[j].ModuleId + '">' + response[i].Children[j].Name + '</label></li>';
                                    }
                                }

                            }
                            html += '</ul></li>';
                        }
                        else {
                            //To Load if No Child nodes are available for Main Node
                            html += '<li><input type="checkbox" id="item-1" checked="checked" /><label for="item-1">' + response[0].Name + '</label><ul>'
                        }
                        html += '</ul></div>';
                        $('#Grouptree').append(html);
                    },
                    error: function (xhr) {
                        console.log(xhr);
                    }
                });
            }

            //Function to load Account head tree(Not Used Now)
            function AccountHeadTree() {
                var company = $.cookie("bsl_1");
                var html = '<div class="treeview-css"><ul>';
                $.ajax({
                    url: apiurl + '/api/AccountHeadMaster/GetTree?company=' + company,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        //for (var i = 0; i < response.length; i++) {
                        //    if (response[i].parentID == 0) {
                        //        html += '<li><input type="checkbox" checked="checked" /><label for="' + response[i].Fag_Name + '" class="edit-group" id="' + response[i].parentID + '">' + response[i].Fag_Name + '</label><ul>';
                        //        if (response[i].Chidren.length > 0) {
                        //            for (var j = 0; j < response[i].Children.length; j++) {
                        //                html += '<li class="edit-group" id="' + response[i].Children[j].HeadID + '">' + response[i].Children[j].HeadName + '</li>';
                        //            }
                        //        }
                        //        html += '</ul>';
                        //    }
                        //    else {
                        //        html += '<li><input type="checkbox" checked="checked" /><label for="' + response[i].parent + '" class="edit-group" id="' + response[i].parentID + '">' + response[i].parent + '</label><ul>';
                        //        html += '<li><input type="checkbox" checked="checked" /><label for="' + response[i].GroupName + '" class="edit-group" id="' + response[i].GroupID + '">' + response[i].GroupName + '</label><ul>';
                        //        if (response[i].Children.length > 0) {
                        //            for (var j = 0; j < response[i].Children.length; j++) {
                        //                if (response[i].Children[j].ChildHead.length > 0) {
                        //                    for (var k = 0; k < response[i].Children[j].ChildHead.length; k++) {
                        //                        html += '<li class="edit-group" id="' + response[i].Children[j].ChildHead[k].HeadID + '">' + response[i].Children[j].ChildHead[k].HeadName + '</li>';
                        //                    }
                        //                }
                        //                html += '</ul></ul>';
                        //            }
                        //        }
                        //    }
                        //}

                        //if (response[0].parentID == 0) {
                        //    html += '<li><input type="checkbox" checked="checked" /><label for="' + response[0].Fag_ParentID + '" class="edit-group" id="' + response[0].Fag_ParentID + '">' + response[0].Fag_Name + '</label><ul>';
                        //    if (response[0].Fah_FagID != 0) {
                        //        for (var i = 0; i < response.length; i++) {
                        //            html += '<li class="edit-group" id="' + response[0].Fah_ID + '">' + response[0].Fah_Name + '</li>';
                        //        }
                        //    }
                        //html += '</ul>';
                        //html += '</ul></li>';
                        //console.log(html);
                        //}
                        ////Procedure to load data for head Tree
                        $('#HeadTree').append(html);
                    },
                    error: function (xhr) {
                        console.log(xhr);
                    }
                });

            }
            //Function to load data for voucher settings

            $('#chkByGroup').on('click', function () {
                if ($('#chkByGroup').prop('checked')) {
                    $('.Group-Table').removeClass('hidden');
                    $('.Head-Table').addClass('hidden');
                    $('#hdnIsGroup').val("1");
                    LoadVoucherSettings();
                }
                else {
                    $('.Head-Table').removeClass('hidden');
                    $('.Group-Table').addClass('hidden');
                    $('#hdnIsGroup').val("0");
                    LoadVoucherSettings();
                }
            });



            function LoadVoucherSettings() {
                var VoucherType = $('#ddlVoucherTypeSettings').val();
                var isGroup = $('#hdnIsGroup').val();
                var html = '';
                $.ajax({
                    url: apiurl + '/api/VoucherSettings/GetTable?VoucherType=' + VoucherType+'&isGroup='+isGroup,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        for (var i = 0; i < response.Table.length; i++) {
                            html += '<tr>'
                            html += '<td class="IDcol hidden">' + response.Table[i].ID + '</td>';
                            html += '<td>' + response.Table[i].Name + '</td>'
                            if (response.Table[i].AllowDr == true) {
                                html += '<td><div class="checkbox checkbox-primary m-b-0"><input type="checkbox" class="debitbox" checked="checked" /><label></label></div></td>'
                            }
                            else {
                                html += '<td><div class="checkbox checkbox-primary m-b-0"><input type="checkbox" class="debitbox" /><label></label></div></td>'
                            }
                            if (response.Table[i].AllowCr == true) {
                                html += '<td><div class="checkbox checkbox-primary m-b-0"><input type="checkbox" class="creditbox" checked="checked" /><label></label></div></td>'
                            }
                            else {
                                html += '<td><div class="checkbox checkbox-primary m-b-0"><input type="checkbox" class="creditbox" /><label></label></div></td>'
                            }
                            if (isGroup != 0) {
                                if (response.Table[i].TotalHead > response.Table[i].DrHead && response.Table[i].DrHead>0) {
                                    html += '<td class="DrPartial hidden">1</td>';
                                }
                                else {
                                    html += '<td class="DrPartial hidden">0</td>';
                                }
                                if (response.Table[i].TotalHead > response.Table[i].CrHead && response.Table[i].CrHead>0) {
                                    html += '<td class="CrPartial hidden">1</td>';
                                }
                                else {
                                    html += '<td class="CrPartial hidden">0</td>';
                                }
                                html += '<td class="TotalHeads hidden">' + response.Table[i].TotalHead + '</td>'
                            }
                            html += '</tr>';
                        }
                        if (isGroup != 0) {
                            $('#GroupTable tbody').empty();
                            $('#GroupTable tbody').append(html);
                        }
                        else {
                            $('#SettingsTable tbody').empty();
                            $('#SettingsTable tbody').append(html);
                        }
                        //console.log(response);
                    },
                    error: function (xhr) {
                        console.log(xhr);
                    }
                });
            }

            ///***Save Or Update Functions***///

            //Voucher Type save or Update function
            $('#btnAddType').click(function () {
                var Voucher = {};
                Voucher.ID = $('#hdnTypeID').val();
                Voucher.Name = $('#txtTypeName').val();
                Voucher.Numbering = $('#ddlNumbering').val();
                Voucher.NumberStartFrom = $('#txtStartsFrom').val();
                Voucher.RestartNumber = $('#ddlNumberingRestarts').val();
                Voucher.IsDebit = $('#ddlIsDebitType').val();
                Voucher.DisplayInJournal = $('#ddlDisplayInJournal').val();
                Voucher.CompanyId = $.cookie("bsl_1");
                Voucher.CreatedBy = $.cookie('bsl_3');
                Voucher.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apiurl + 'api/VoucherType/Save',
                    method: 'POST',
                    datatype: 'JSON',
                    data: JSON.stringify(Voucher),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        if (response.Success) {
                            successAlert(response.success);
                            RefreshTableVoucherType();
                            reset();
                            $('#btnAddType').html('<i class="fa fa-plus"></i>&nbsp;Add Voucher');
                            $('#hdnTypeID').val("0");
                        }
                        else {
                            errorAlert("Failed To save");
                        }
                    },
                    error: function (xhr) {
                        errorAlert("Something went wrong.Please try Again Later");
                    }
                });
            });


            //Cost Center
            $('#btnAddCostCenter').click(function () {
                var cost = {};
                cost.Name = $('#txtCostCentrName').val();
                cost.Status = $('#ddlCostCenterStatus').val();
                cost.ID = $('#hdnCostCenterID').val();
                cost.CompanyId = $.cookie("bsl_1");
                cost.CreatedBy = $.cookie('bsl_3');
                cost.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apiurl + 'api/CostCenter/Save',
                    method: 'POST',
                    datatype: 'JSON',
                    data: JSON.stringify(cost),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        if (response.Success) {
                            if (cost.ID == 0) {
                                successAlert(response.success);
                            }
                            else {
                                successAlert("Updated Successfully");
                            }
                            RefreshTableCostCenter();
                            reset();
                            $('#btnAddCostCenter').html('<i class="fa fa-plus"></i>&nbsp;Add Cost Center');
                            $('#hdnCostCenterID').val("0");
                        }
                        else {
                            errorAlert("Failed To save");
                        }
                    },
                    error: function (xhr) {
                        errorAlert("Something went wrong.Please try Again Later " + xhr.message);
                    }
                });
            });


            //Save Report Settings
            $('#btnAddReportSettings').click(function () {
                var Report = {};
                Report.AccountGroupID = $('#ddlAccountGroupReport').val();
                Report.ReportID = $('#ddlReportType').val();
                Report.Postion = $('#ddlReportSide').val();
                Report.Order = $('#txtReportOrder').val();
                Report.isMinus = $('#ddlIsMinus').val();
                Report.ID = $('#hdnReportSettings').val();
                Report.CompanyId = $.cookie("bsl_1");
                Report.CreatedBy = $.cookie('bsl_3');
                Report.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apiurl + 'api/ReportSettings/Save',
                    method: 'POST',
                    datatype: 'JSON',
                    data: JSON.stringify(Report),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        if (response.Success) {
                            if (Report.ID == 0) {
                                successAlert(response.success);
                            }
                            else {
                                successAlert("Updated Successfully");
                            }
                            RefreshTableReport();
                            reset();
                            $('#btnAddReportSettings').html('<i class="fa fa-plus"></i>&nbsp;Add Report Settings');
                            $('#hdnReportSettings').val("0");
                        }
                        else {
                            errorAlert("Failed To save");
                        }
                    },
                    error: function (xhr) {
                        errorAlert("Something went wrong.Please try Again Later " + xhr.message);
                    }
                });
            });

            //Saves Accountgroup
            $('#btnAddGroup').click(function () {
                var group = {};
                group.Name = $('#txtAccountGroupName').val();
                group.Description = $('#txtAccountGroupDescription').val();
                group.AccountType = $('#ddlAccountNatureGroup').val();
                group.Disable = $('#ddlGroupStatus').val();
                group.ParentId = $('#ddlParentGroup').val();
                group.IsAffectGP = $('#ddlIsAffectGP').val();
                group.Id = $('#hdnGroupID').val();
                group.Company = $.cookie("bsl_1");
                group.CreatedBy = $.cookie('bsl_3');
                group.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apiurl + 'api/AccountGroup/Save',
                    method: 'POST',
                    datatype: 'JSON',
                    data: JSON.stringify(group),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        if (response.Success) {
                            if (group.Id == 0) {
                                successAlert(response.success);
                            }
                            else {
                                successAlert("Updated Successfully");
                            }
                            getTree();
                            reset();
                            $('#btnAddGroup').html('<i class="fa fa-plus"></i>&nbsp;Add Report Settings');
                            $('#hdnGroupID').val("0");
                        }
                        else {
                            errorAlert("Failed To save");
                        }
                    },
                    error: function (xhr) {
                        errorAlert("Something went wrong.Please try Again Later " + xhr.message);
                    }
                });
            });


            //Save Function for account heads
            $('#btnAddHead').click(function () {
                var Head = {};
                Head.ParentId = $('#ddlParentGroups').val();
                Head.ParentId = $('#AccountGroupId').val();
                Head.Name = $('#txtAccountheadName').val();
                Head.Address = $('#txtHeadContactAddress').val();
                Head.ContactPerson = $('#txtHeadContactPerson').val();
                Head.Description = $('#txtHeadDescription').val();
                Head.OpeningBalance = $('#txtHeadOpeningBalance').val();
                Head.IsDebit = $('#ddlHeadisDebit').val();
                Head.OpeningDate = $('#txtHeadOpeningDate').val();
                Head.Phone = $('#txtHeadContactPhone').val();
                Head.Email = $('#txtHeadContactEmail').val();
                Head.AccountNature = $('#ddlHeadAccountNature').val();
                Head.status = $('#ddlHeadStatus').val();
                Head.AccountType = $('#ddlAccountType').val();
                Head.CompanyID = $.cookie('bsl_1');
                Head.CreatedBy = $.cookie('bsl_3');
                Head.ModifiedBy = $.cookie('bsl_3');
                Head.Category = $('#ddlHeadCategory').val();
                Head.ReverseHeadId = $('#ddlReverseHead').val();
                Head.DataSQL = $('#txtDataSQL').val();
                Head.AmountSQL = $('#txtAmountSQL').val();
                Head.SQLTable = $('#txtRefTable').val();
                Head.SQLID = $('#txtIDField').val();
                Head.SQLName = $('#txtNameField').val();
                Head.TransactionSQL = $('#txtTransactionSQL').val();
                Head.ID = $('#hdnHeadID').val();
                $.ajax({
                    url: apiurl + 'api/AccountHeadMaster/Save',
                    method: 'POST',
                    datatype: 'JSON',
                    data: JSON.stringify(Head),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        console.log(response);
                        if (response.Success) {
                            if (Head.ID == 0) {
                                successAlert(response.success);
                            }
                            else {
                                successAlert("Updated Successfully");
                            }
                            reset();
                            $('#btnAddHead').html('<i class="fa fa-plus"></i>&nbsp;Add Account Head');
                            $('#hdnHeadID').val("0");
                        }
                        else {
                            errorAlert("Failed To save");
                        }
                    },
                    error: function (xhr) {
                        errorAlert("Something went wrong.Please try Again Later " + xhr.message);
                    }
                });

            });



            //Saves Voucher Settings
            $('#btnAddVoucherSettings').click(function () {
                var headID = '';
                var DrPartial = '';
                var CrPartial = '';
                var Dr = '';
                var Cr = '';
                if ($('#hdnIsGroup').val()!=0) {
                    var tr = $('#GroupTable tbody').children('tr');
                }
                else {
                    var tr = $('#SettingsTable tbody').children('tr');
                }
                //Loop is Used to get all the head ID as a string
                for (var i = 0; i < tr.length; i++) {
                    headID += $(tr[i]).children('td').eq(0).text() + "|";
                }
                headID = headID.slice(0, -1);
                //Loop is Used to get all the Debit as a string
                for (var i = 0; i < tr.length; i++) {
                    if ($(tr[i]).children('td').eq(2).children().find('.debitbox').prop('checked') == true) {
                        Dr += '1';
                        Dr += '|';
                    }
                    else {
                        Dr += '0';
                        Dr += '|';
                    }
                }
                //Loop is Used to get all the Credit as a string
                Dr = Dr.slice(0, -1);
                for (var i = 0; i < tr.length; i++) {
                    if ($(tr[i]).children('td').eq(3).children().find('.creditbox').prop('checked') == true) {
                        Cr += '1';
                        Cr += '|';
                    }
                    else {
                        Cr += '0';
                        Cr += '|';
                    }
                }
                Cr = Cr.slice(0, -1);

                if ($('#hdnIsGroup').val() != 0) {
                    for (var i = 0; i < tr.length; i++) {
                        if ($(tr[i]).children('td').eq(4).text() == '1' && $(tr[i]).children('td').eq(2).children().find('.debitbox').prop('checked') == true) {
                            DrPartial += '1';
                            DrPartial += '|';
                        }
                        else {
                            DrPartial += '0';
                            DrPartial += '|';
                        }
                    }
                    DrPartial = DrPartial.slice(0, -1);
                    for (var i = 0; i < tr.length; i++) {
                        console.log();
                        if ($(tr[i]).children('td').eq(5).text() == '1' && $(tr[i]).children('td').eq(3).children().find('.creditbox').prop('checked') == true) {
                            CrPartial += '1';
                            CrPartial += '|';
                        }
                        else {
                            CrPartial += '0';
                            CrPartial += '|';
                        }
                    }
                    CrPartial = CrPartial.slice(0, -1);
                }
                var Settings = {};
                Settings.CreatedBy = $.cookie('bsl_3');
                Settings.byGroup = $('#hdnIsGroup').val();
                Settings.CompanyID = $.cookie('bsl_1');
                Settings.TypeID = $('#ddlVoucherTypeSettings').val();
                Settings.HeadID = headID;
                if ($('#hdnIsGroup').val()==0) {
                    Settings.AllowDr = Dr;
                    Settings.AllowCr = Cr;
                }
                else {
                    Settings.DrPart = DrPartial;
                    Settings.CrPart = CrPartial;
                    Settings.AllowDr = Dr;
                    Settings.AllowCr = Cr;
                }
                console.log(Settings);
                if (Settings.TypeID!=0) {
                    $.ajax({
                        url: apiurl + 'api/VoucherSettings/Save',
                        method: 'POST',
                        datatype: 'JSON',
                        data: JSON.stringify(Settings),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            if (response.Success) {
                                successAlert(response.success);
                                console.log(response);
                                reset();
                                $('#btnAddVoucherSettings').html('<i class="fa fa-plus"></i>&nbsp;Update Settings');
                                $('#SettingsTable tbody').empty();
                                $('#GroupTable tbody').empty();
                                $('#hdnIsGroup').val('0');
                                $('#chkByGroup').trigger('click');
                            }
                            else {
                                errorAlert("Failed To save");
                            }
                        },
                        error: function (xhr) {
                            errorAlert("Something went wrong.Please try Again Later " + xhr.message);
                        }
                    });
                }
                else {
                    errorAlert('Please select a voucher type');
                }
                
            });
            ///**EDIT***////


            //Voucher Type
            $(document).on('click', '.edit-entry-type', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apiurl + '/api/VoucherType/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtTypeName').val(response.Name);
                        $('#ddlTypeStatus').val(response.Disable);
                        $('#ddlIsDebitType').val(response.IsDebit);
                        $('#ddlDisplayInJournal').val(response.DisplayInJournal);
                        $('#txtStartsFrom').val(response.NumberStartFrom);
                        $('#ddlNumberingRestarts').val(response.RestartNumber);
                        $('#ddlNumbering').val(response.Numbering);
                        $('#hdnTypeID').val(response.ID);
                        $('#btnAddType').html('<i class=""></i>&nbsp;Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Cost Center
            $(document).on('click', '.edit-entry-cost', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apiurl + '/api/CostCenter/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtCostCentrName').val(response.Name);
                        $('#ddlCostCenterStatus').val(response.Status);
                        $('#hdnCostCenterID').val(response.ID);
                        $('#btnAddCostCenter').html('<i class=""></i>&nbsp;Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Report Settings
            $(document).on('click', '.edit-entry-report', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                $.ajax({
                    url: apiurl + '/api/ReportSettings/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        reset();
                        $('#txtReportOrder').val(response.Order);
                        $('#ddlReportType').val(response.ReportID);
                        $('#ddlReportSide').val(response.Postion);
                        $('#ddlAccountGroupReport').val(response.AccountGroupID);
                        $('#hdnReportSettings').val(response.ID);
                        $('#ddlIsMinus').val(response.isMinus);
                        $('#btnAddReportSettings').html('<i class=""></i>&nbsp;Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Account Group Edit Functionality
            $(document).on('click', '.edit-group', function () {
                var id = $(this).attr('id');
                $.ajax({
                    url: apiurl + '/api/AccountGroup/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        //console.log(response);
                        reset();
                        $('#txtAccountGroupName').val(response.Name);
                        $('#ddlParentGroup').val(response.ParentId);
                        $('#txtAccountGroupDescription').val(response.Description);
                        $('#ddlGroupStatus').val(response.Disable);
                        $('#ddlIsAffectGP').val(response.IsAffectGP);
                        $('#ddlAccountNatureGroup').val(response.AccountType);
                        $('#hdnGroupID').val(response.Id);
                        $('#btnAddGroup').html('<i class=""></i>&nbsp;Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });


            //***Delete Entry**//

            //Voucher type
            $(document).on('click', '.delete-entry-type', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apiurl + '/api/VoucherType/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTableVoucherType
                });

            });

            //Cost Center
            $(document).on('click', '.delete-entry-cost', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apiurl + '/api/CostCenter/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully',
                    "successFunction": RefreshTableCostCenter
                });
            });

            //Deletes report Settings
            $(document).on('click', '.delete-entry-report', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apiurl + '/api/ReportSettings/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully',
                    "successFunction": RefreshTableReport
                });

            });


            //ADDITIONAL FUNCTIONS

            //Loads The data for dropdownlist in Account Heads
            function LoadAdditionaldata() {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/AccountHeadMaster/GetHeads?CompanyId=' + Company,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        //Gets the Account Heads and Loads into the dropdownlist
                        $('#ddlReverseHead').empty();
                        $('#ddlReverseHead').append('<option value="0">--Select--</option>');
                        $(response).each(function () {
                            $('#ddlReverseHead').append('<option value="' + this.ID + '">' + this.Name + '</option>');
                        });

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
                $.ajax({
                    //Loads the account groups and loads to dropdownlist
                    url: apiurl + '/api/AccountHeadMaster/GetGroups?CompanyId=' + Company,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#ddlParentGroups').empty();
                        $('#ddlParentGroups').append('<option value="0">--Select--</option>');
                        $(response).each(function () {
                            $('#ddlParentGroups').append('<option value="' + this.AccountGroup_Id + '">' + this.Name + '</option>');
                        });

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            }


            function getAccountGroups() {
                $("#ddlAccountGroupReport").empty();
                var company = $.cookie("bsl_1");
                $.ajax({
                    //Loads the account groups and loads to dropdownlist
                    url: apiurl + '/api/AccountHeadMaster/GetGroups?CompanyId=' + company,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $.each(response, function () {
                            $('#ddlAccountGroupReport').append('<option value="' + this.AccountGroup_Id + '">' + this.Name + '</option>');
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Used to get the account nature of a group in account head tab
            $('#ddlParentGroups').change(function () {
                var id = $('#ddlParentGroups :selected').val();
                $.ajax({
                    url: apiurl + '/api/AccountHeadMaster/get1/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        var type = response;
                        $('#ddlHeadAccountNature').val(type);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
            });

            //DatePicker Function
            $('#txtHeadOpeningDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true,
            });

            //To Bind the Tables(Voucher Settings)
            $('#ddlVoucherTypeSettings').change(function () {
                LoadVoucherSettings();
            });

            //Loads the voucher Types for dropdown list
            function LoadVoucherTypes() {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/VoucherSettings/GetVoucherTypes',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#ddlVoucherTypeSettings').empty();
                        $('#ddlVoucherTypeSettings').append('<option value="0">--Select--</option>');
                        $(response).each(function () {
                            $('#ddlVoucherTypeSettings').append('<option value="' + this.ID + '">' + this.Name + '</option>');
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //To select/Deselect all the checkbox unbder debit or credit
            $('#chkDebit').click(function () {
                if ($('#chkDebit').prop('checked') == true) {
                    $('.debitbox').prop("checked", true);
                }
                else {
                    $('.debitbox').prop("checked", false);
                }
            });

            //To select/Deselect all the checkbox unbder debit or credit
            $('#chkisDebitGroup').click(function () {
                if ($('#chkisDebitGroup').prop('checked') == true) {
                    $('.debitbox').prop("checked", true);
                }
                else {
                    $('.debitbox').prop("checked", false);
                }
            });



            //To select/Deselect all the checkbox unbder debit or credit
            $('#chkCredit').click(function () {
                if ($('#chkCredit').prop('checked') == true) {
                    $('.creditbox').prop("checked", true);
                }
                else {
                    $('.creditbox').prop("checked", false);
                }
            });

            //To select/Deselect all the checkbox unbder debit or credit
            $('#chkisCreditGroup').click(function () {
                if ($('#chkisCreditGroup').prop('checked') == true) {
                    $('.creditbox').prop("checked", true);
                }
                else {
                    $('.creditbox').prop("checked", false);
                }
            });


            $('.refresh-data').click(function () {
                reset();
                $('#btnAddGroup').html('<i class="fa fa-plus"></i>&nbsp;Add AccountGroup');
                $('#btnAddHead').html('<i class="fa fa-plus"></i>&nbsp;Add Account Head');
                $('#btnAddVoucherSettings').html('<i class="fa fa-plus"></i>&nbsp;Update Settings');
                $('#btnAddType').html('<i class="fa fa-plus"></i>&nbsp;Add Voucher');
                $('#btnAddReportSettings').html('<i class="fa fa-plus"></i>&nbsp;Add Report Settings');
                $('#btnAddCostCenter').html('<i class="fa fa-plus"></i>&nbsp;Add Cost Center');
                $('#SettingsTable tbody').empty();
            });

        });
    </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <!--<link rel="stylesheet" href="http://kendo.cdn.telerik.com/2017.3.1026/styles/kendo.common.min.css" />
    <link rel="stylesheet" href="http://kendo.cdn.telerik.com/2017.3.1026/styles/kendo.blueopal.min.css" />
    <script src="http://cdn.kendostatic.com/2014.2.716/js/angular.min.js"></script>
    <script src="http://cdn.kendostatic.com/2014.2.716/js/kendo.all.min.js"></script>-->
</asp:Content>
