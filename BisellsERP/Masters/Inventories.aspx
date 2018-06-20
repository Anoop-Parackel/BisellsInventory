<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventories.aspx.cs" Inherits="BisellsERP.Masters.Inventories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventories</title>
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

            .masters-wrap > div .nav-menu {
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
         .btn-upload {
            background-color: transparent;
            border: 1px solid #4abfa6;
            border-radius: 20px;
            opacity: .8;
            padding: 3px 12px;
            transition: all .3s ease-in-out;
        }
            .btn-upload:hover {
                opacity: .9;
                background-color: #eefffb;
                color: #4abfa6;
                box-shadow: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="masters-wrap">
        <div class="col-sm-2 p-0">
            <h3 class="m-t-0">Inventory</h3>
            <div class="tabs-vertical-env">
                <ul class="nav nav-menu" id="menu">
                    <li class="active" id="Item">
                        <a href="#v-5" data-toggle="tab" aria-expanded="true">Product / Service</a>
                    </li>
                    <%--  <li class="" id="Services">
                        <a href="#v-12" data-toggle="tab" aria-expanded="false">Service</a>
                    </li>--%>
                    <li class="" id="Brand">
                        <a href="#v-1" data-toggle="tab" aria-expanded="false">Brand</a>
                    </li>

                    <li class="" id="Group">
                        <a href="#v-4" data-toggle="tab" aria-expanded="true">Group</a>
                    </li>
                    <li class="" id="Category">
                        <a href="#v-2" data-toggle="tab" aria-expanded="false">Category</a>
                    </li>
                    <li class="" id="Type">
                        <a href="#v-8" data-toggle="tab" aria-expanded="true">Product Type</a>
                    </li>
                    <li class="" id="Tax">
                        <a href="#v-7" data-toggle="tab" aria-expanded="true">Tax</a>
                    </li>
                    <li class="" id="Unit">
                        <a href="#v-9" data-toggle="tab" aria-expanded="true">Unit</a>
                    </li>
                    <li class="list-break">
                        <p></p>
                    </li>
                    <li class="" id="Vehicles">
                        <a href="#v-10" data-toggle="tab" aria-expanded="true">Vehicles</a>
                    </li>
                    <li class="" id="Despatch">
                        <a href="#v-11" data-toggle="tab" aria-expanded="true">Despatch</a>
                    </li>
                    <li class="" id="Supplier">
                        <a href="#v-6" data-toggle="tab" aria-expanded="true">Supplier</a>
                    </li>
                    <li class="" id="Customer">
                        <a href="#v-3" data-toggle="tab" aria-expanded="false">Customer</a>
                    </li>
                    <li class="" id="Leads">
                        <a href="#v-12" data-toggle="tab" aria-expanded="false">Leads</a>
                    </li>
                </ul>
            </div>
        </div>

        <div class="col-sm-10 p-0">
            <div class="col-md-12 m-t-40">
                <div class="tab-content">

                    <%-- BRAND MASTER --%>
                    <div class="tab-pane" id="v-1">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Brand</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtBrandName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Order</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtBrandOrder" placeholder="Order" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlBrandStatus">
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
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddBrand"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Brand List</h5>
                                        <table id="tableBrand" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Order</th>
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

                    <%-- CATEGORY MASTER --%>
                    <div class="tab-pane" id="v-2">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">

                                        <h5 class="sett-title p-t-0">Add New Category</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtCategoryName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Order</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtCategoryOrder" placeholder="Order" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlCategoryStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" id="btnAddCategory" type="button"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Category List</h5>
                                        <table id="tableCategory" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Order</th>
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


                    <%--  LEADS--%>
                     <div class="tab-pane" id="v-12">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Lead</h5>
                                            <div class="row">
                                            <label class="control-label col-sm-4 m-l--5 m-t-20"></label>
                                            <div class="col-sm-8 m-b-15">
                                                <div class="col-sm-2">
                                                    <img id="imgLeadPhoto" src="../Theme/images/profile-pic.jpg" style="width:100%" class="img-circle profile-pic" />
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10">
                                                        <span><i class="ion-upload m-r-5"></i>Upload</span>
                                                        <input type="file" class="upload" id="btnLeadProfileUpload"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 m-r--5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10 remove-pic">
                                                        <span id="btnLeadRemoveProfile"><i class="ion-trash-b m-r-5"></i>Remove</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                   
                                                      <div class="form-group">
                                                        <label class="control-label col-md-4">Source</label>
                                                        <div class="col-md-8">
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
                                                       <div class="form-group">
                                                        <label class="control-label col-md-4">Assigned</label>
                                                        <div class="col-md-8">
                                                             <select class="form-control input-sm" id="ddlAssign">
                                                                <option value="0"  selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                      <div class="form-group">
                                                        <label class="control-label col-sm-4">Contact</label>
                                                        <div class="col-sm-3">
                                                            <select class="form-control" id="ddlLeadSalutation">
                                                                <option value="Mr">Mr.</option>
                                                                <option value="Mrs">Mrs.</option>
                                                                <option value="Ms">Ms.</option>
                                                                <option value="Miss">Miss.</option>
                                                            </select>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <input type="text" class="form-control" id="txtLeadContactName" placeholder="Contact Name" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Address 1</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadAddr1" placeholder="Address Line 1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Address 2</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadAddr2" placeholder="Address Line 2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                        <div class="form-group">
                                                        <label class="control-label col-sm-4">City</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadCity" placeholder="City" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">ZipCode</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadZipCode" placeholder="ZipCode" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Phone 1</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadPh1" placeholder="Phone Number 1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Phone 2</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadPh2" placeholder="Phone Number 2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5 class="sett-title p-t-0"></h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Country</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlLeadCountry">
                                                                <option value="0" disabled="disabled" selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">State/Provincial</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlLeadState">
                                                                <option value="0" disabled="disabled" selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Email</label>
                                                        <div class="col-sm-8">
                                                            <div class="form-control-wrapper">
                                                                <input type="email" id="txtLeadEmail" class="form-control input-sm" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">TIN/GST No</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadTax1" placeholder="TIN/GST No" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">CIN/Reg No</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtLeadTax2" placeholder="CIN/Reg No" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                     <div class="form-group">
                                                        <label class="control-label col-md-4">Lead Status</label>
                                                        <div class="col-md-8">
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
                                                </div>
                                            </div>
                                        </div>
                                        <h5 class="sett-title p-t-0"></h5>
                                       
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddLead"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Lead List</h5>
                                        <table id="tableLead" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Phone 1</th>
                                                    <th>#</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <%-- CUSTOMER MASTER --%>
                    <div class="tab-pane" id="v-3">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">

                                        <h5 class="sett-title p-t-0">Add New Customer</h5>
                                          <div class="row">
                                            <label class="control-label col-sm-4 m-l--5 m-t-20"></label>
                                            <div class="col-sm-8 m-b-15">
                                                <div class="col-sm-2">
                                                    <img id="imgCustomerPhoto" src="../Theme/images/profile-pic.jpg" style="width:100%" class="img-circle profile-pic" />
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10">
                                                        <span><i class="ion-upload m-r-5"></i>Upload</span>
                                                        <input type="file" class="upload" id="btnCustomerProfileUpload"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 m-r--5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10 remove-pic">
                                                        <span id="btnCustomerRemoveProfile"><i class="ion-trash-b m-r-5"></i>Remove</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Name</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Contact</label>
                                                        <div class="col-sm-3">
                                                            <select class="form-control" id="ddlCustSalutation">
                                                                <option value="Mr">Mr.</option>
                                                                <option value="Mrs">Mrs.</option>
                                                                <option value="Ms">Ms.</option>
                                                                <option value="Miss">Miss.</option>
                                                            </select>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <input type="text" class="form-control" id="txtCustContactName" placeholder="Contact Name" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Address 1</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerAddress1" placeholder="Address Line 1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Address 2</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerAddress2" placeholder="Address Line 2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">City</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerCity" placeholder="City" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">ZipCode</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerZipCode" placeholder="ZipCode" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Phone 1</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerPhone1" placeholder="Phone Number 1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Phone 2</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerPhone2" placeholder="Phone Number 2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5 class="sett-title p-t-0"></h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Country</label>
                                                        <div class="col-sm-9">
                                                            <select class="form-control input-sm" id="ddlCustomerCountry">
                                                                <option value="0" disabled="disabled" selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">State/Provincial</label>
                                                        <div class="col-sm-9">
                                                            <select class="form-control input-sm" id="ddlCustomerState">
                                                                <option value="0" disabled="disabled" selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Email</label>
                                                        <div class="col-sm-9">
                                                            <div class="form-control-wrapper">
                                                                <input type="email" id="txtCustomerEmail" class="form-control input-sm" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">TRN/GST No</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerTax1" placeholder="TRN/GST No" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">CIN/Reg No</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerTax2" placeholder="CIN/Reg No" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Status</label>
                                                        <div class="col-sm-9">
                                                            <select class="form-control input-sm" id="ddlCustomerStatus">
                                                                <option value="1">Active</option>
                                                                <option value="0">Inactive</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5 class="sett-title p-t-0"></h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Credit Amount</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerCreditAmount" placeholder="Credit Amount" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Credit Period (In Days)</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerCreditPeriod" placeholder="Credit Period" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Lock Amount</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerLockAmount" placeholder="Lock Amount" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Lock Period (In Days)</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtCustomerLockPeriod" placeholder="Lock Period" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddCustomer"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Customer List</h5>
                                        <table id="tableCustomer" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Phone 1</th>
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

                    <%-- GROUP MASTER --%>
                    <div class="tab-pane" id="v-4">
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
                                                            <input type="text" id="txtGroupName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Order</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtGroupOrder" placeholder="Order" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlGroupStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" id="btnAddNewGroup" type="button"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Group List</h5>
                                        <table id="tableGroup" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Order</th>
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

                    <%-- ITEM MASTER --%>
                    <div class="tab-pane active" id="v-5">
                        <div class="panel">
                            <div class="panel-body p-0" style="overflow-x: hidden;">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <ul class="nav nav-inner nav-tabs navtab-bg">
                                            <li class="active">
                                                <a href="#item" data-toggle="tab" aria-expanded="true">
                                                    <span>Item</span>
                                                </a>
                                            </li>
                                            <li class="">
                                                <a href="#service" id="serviceTab" data-toggle="tab" aria-expanded="false">
                                                    <span>Service</span>
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="item">
                                                <div class="row">

                                                    <div class="row">
                                                        <div class="col-sm-5">

                                                            <h5 class="sett-title p-t-0">Add New Item</h5>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-horizontal">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Item Code</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="text" id="txtItemCode" class="form-control input-sm" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Name</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="text" id="txtItemName" placeholder="Name" class="form-control input-sm" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">OEM Code</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="text" id="txtItemOEMCode" placeholder="OEM Code" class="form-control input-sm" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">HS Code</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="text" id="txtItemHSCode" placeholder="HS Code" class="form-control input-sm" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Status</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemStatus">
                                                                                    <option value="1">Active</option>
                                                                                    <option value="0">Inactive</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Description</label>
                                                                            <div class="col-sm-8">
                                                                                <textarea class="form-control input-sm" placeholder="Description" rows="3" cols="50" id="txtItemDescription"></textarea>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <h5 class="sett-title p-t-0">Additional Information</h5>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-horizontal">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Tax</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemTax">
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">UOM</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemUOM">
                                                                                    <option value="0">--select--</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Barcode</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="text" class="form-control input-sm" id="txtItemBarcode" placeholder="Barcode" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">MRP</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="number" class="form-control input-sm" id="txtItemMRP" title="MRP" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Cost Price</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="number" class="form-control input-sm" id="txtItemCP" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Selling Price</label>
                                                                            <div class="col-sm-8">
                                                                                <input type="number" class="form-control input-sm" id="txtItemSP" title="Must be less than MRP" />
                                                                                <small id="lblPriceText" class="hidden" style="color: #8d778d;">Selling Price must not be grater than Mrp </small>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <h5 class="sett-title p-t-0">Categories</h5>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-horizontal">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Group</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemGroup">
                                                                                    <option value="0">--select--</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Category</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemCategory">
                                                                                    <option value="0">--select--</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Type</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemType">
                                                                                    <option value="0">--select--</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4">Brand</label>
                                                                            <div class="col-sm-8">
                                                                                <select class="form-control input-sm" id="ddlItemBrand">
                                                                                    <option value="0">--select--</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4"></label>
                                                                            <div class="col-sm-8">
                                                                                <div class="checkbox checkbox-primary">
                                                                                    <input type="checkbox" id="chkTrack" class="checkbox checkbox-primary track" checked="true" /><label>Track for Inventory</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <h5 class="sett-title p-t-0">Instances</h5>
                                                            <div class="col-md-12">
                                                                <table id="instanceTable" class="table table-responsive hidden">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="display: none">InstanceId</th>
                                                                            <th>MRP</th>
                                                                            <th>CP</th>
                                                                            <th>SP</th>
                                                                            <th>BarCode</th>
                                                                            <th>#</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody></tbody>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <td colspan="6" id="newInstance" class="text-center text-success addInstance"><i class="md-add-circle-outline"></i>&nbsp;Add New Instance</td>
                                                                        </tr>
                                                                    </tfoot>
                                                                </table>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="btn-toolbar m-t-20">
                                                                        <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddItem"><i></i>&nbsp;<span>Save</span></button>
                                                                        <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <h5 class="sett-title p-t-0">Items List</h5>
                                                            <table id="tableItem" class="table table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>ID</th>
                                                                        <th>Name</th>
                                                                        <th>Tax</th>
                                                                        <th>Brand</th>
                                                                        <th>Status</th>
                                                                        <th>#</th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>

                                                    </div>

                                                </div>

                                            </div>
                                            <div class="tab-pane " id="service">
                                                <div class="row">
                                                    <div class="col-sm-5">

                                                        <h5 class="sett-title p-t-0">Add New Service</h5>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-horizontal">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4">Item Code</label>
                                                                        <div class="col-sm-8">
                                                                            <input type="text" id="txtServiceItemCode" placeholder="Item Code" class="form-control input-sm" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4">Name</label>
                                                                        <div class="col-sm-8">
                                                                            <input type="text" id="txtServiceName" placeholder="Name" class="form-control input-sm" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4">Description</label>
                                                                        <div class="col-sm-8">
                                                                            <textarea class="form-control input-sm" placeholder="Description" id="txtServiceDescription" rows="3" cols="50"></textarea>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4">Tax</label>
                                                                        <div class="col-sm-8">
                                                                            <select class="form-control input-sm" id="ddlServiceTax">
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4">Rate</label>
                                                                        <div class="col-sm-8">
                                                                            <input type="number" id="txtServiceRate" placeholder="Rate" class="form-control input-sm" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4">Status</label>
                                                                        <div class="col-sm-8">
                                                                            <select class="form-control input-sm" id="ddlServiceStatus">
                                                                                <option value="1">Active</option>
                                                                                <option value="0">Inactive</option>
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4"></label>
                                                                        <div class="col-sm-8">
                                                                            <div class="checkbox checkbox-primary">
                                                                                <input type="checkbox" id="chkTrackService" class="checkbox checkbox-primary track-service" /><label>Track for Inventory</label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="btn-toolbar m-t-20">
                                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddService"><i></i>&nbsp;<span>Save</span></button>
                                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <h5 class="sett-title p-t-0">Service List</h5>
                                                        <table id="tableService" class="table table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>ID</th>
                                                                    <th>Name</th>
                                                                    <th>SellingPrice</th>
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
                    </div>

                    <%-- SUPPLIER MASTER --%>
                    <div class="tab-pane" id="v-6">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">

                                        <h5 class="sett-title p-t-0">Add New Supplier</h5>
                                          <div class="row">
                                            <label class="control-label col-sm-4 m-l--5 m-t-20"></label>
                                            <div class="col-sm-8 m-b-15">
                                                <div class="col-sm-2">
                                                    <img id="imgPhoto" src="../Theme/images/profile-pic.jpg" style="width:100%" class="img-circle profile-pic" />
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10">
                                                        <span><i class="ion-upload m-r-5"></i>Upload</span>
                                                        <input type="file" class="upload" id="btnProfileUpload"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 m-r--5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10 remove-pic">
                                                        <span id="btnRemoveProfile"><i class="ion-trash-b m-r-5"></i>Remove</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Name</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Contact Name</label>
                                                        <div class="col-sm-3">
                                                            <select id="ddlSupplierSalutation" class="form-control input-sm">
                                                                <option value="Mr">Mr.</option>
                                                                <option value="Mrs">Mrs.</option>
                                                                <option value="Ms">Ms.</option>
                                                                <option value="Dr">Dr.</option>
                                                            </select>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <input type="text" class="form-control input-sm" placeholder="Contact name" id="txtSupplierContactName" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Address 1</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierAddres1" placeholder="Address Line 1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Address 2</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierAddress2" placeholder="Address Line 2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">City</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierCity" placeholder="City" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">ZipCode</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierZipCode" placeholder="ZipCode" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5 class="sett-title p-t-0"></h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Country</label>
                                                        <div class="col-sm-9">
                                                            <select class="form-control input-sm" id="ddlSupplierCountry">
                                                                <option value="0" disabled="disabled" selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">State/Provincial</label>
                                                        <div class="col-sm-9">
                                                            <select class="form-control input-sm" id="ddlSupplierState">
                                                                <option value="0" disabled="disabled" selected="selected">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Phone 1</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierPhone1" placeholder="Phone Number 1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Phone 2</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierPhone2" placeholder="Phone Number 2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Email</label>
                                                        <div class="col-sm-9">
                                                            <input type="email" data-validation="email" id="txtSupplierEmail" placeholder="Email" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5 class="sett-title p-t-0"></h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">TRN/GST</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierTax1" placeholder="TRN/GST" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">CIN/Reg No</label>
                                                        <div class="col-sm-9">
                                                            <input type="text" id="txtSupplierTax2" placeholder="CIN/Reg No" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-3">Status</label>
                                                        <div class="col-sm-9">
                                                            <select class="form-control input-sm" id="ddlSupplierStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddSupplier"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Supplier List</h5>
                                        <table id="tableSuppliers" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Phone1</th>
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

                    <%-- TAX MASTER --%>
                    <div class="tab-pane" id="v-7">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">

                                        <h5 class="sett-title p-t-0">Add New Tax</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Tax Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTaxName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Percentage</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtTaxPercentage" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Type</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTaxType" placeholder="Tax Type" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlTaxStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddTax"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Tax List</h5>
                                        <table id="tableTax" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Percentage</th>
                                                    <th>Type</th>
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

                    <%-- TYPE MASTER --%>
                    <div class="tab-pane" id="v-8">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">

                                        <h5 class="sett-title p-t-0">Add New Type</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTypeName" placeholder="Product Type Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Order</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTypeOrder" placeholder="Order" class="form-control input-sm" />
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
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" id="btnAddType" type="button"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Product Type List</h5>
                                        <table id="tableType" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Order</th>
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

                    <%-- UNIT MASTER --%>
                    <div class="tab-pane" id="v-9">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Unit of Measurement</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtUOMName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Short Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtUOMShortName" placeholder="Short Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlUOMStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddUOM"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Units List</h5>
                                        <table id="tableUnit" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Short Name</th>
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

                    <%-- VEHICLE MASTER --%>
                    <div class="tab-pane" id="v-10">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Vehicle</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtVehicleName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Number</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtVehicleNumber" placeholder="Number" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Type</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtVehicleType" placeholder="Type" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Owner</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtVehicleOwner" placeholder="Owner" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlVehicleStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddVehicle"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Vehicle List</h5>
                                        <table id="tableVehicle" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Number</th>
                                                    <th>Type</th>
                                                    <th>Owner</th>
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

                    <%-- DESPATCH MASTER --%>
                    <div class="tab-pane" id="v-11">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">

                                        <h5 class="sett-title p-t-0">Add New Despatch</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtDespatchName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Address</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtDespatchAddress" placeholder="Address" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Phone</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtDespatchNumber" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Mobile</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtDespatchMobile" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Contact Person</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtDespatchContact" placeholder="Contact Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Contact Person Phone</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtDespatchContactPhone" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Narration</label>
                                                        <div class="col-sm-8">
                                                            <textarea class="form-control input-sm" placeholder="Description" id="txtDespatchNarration" rows="3" cols="50"></textarea>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlDespatchStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" type="button" id="btnAddDespatch"><i></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Despatch List</h5>
                                        <table id="tableDespatch" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Address</th>
                                                    <th>MobileNo</th>
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

                    <%-- SERVICE MASTER --%>
                </div>
            </div>
        </div>
        <input type="hidden" value="0" id="hdnItemID" />
        <input type="hidden" value="0" id="hdnBrandID" />
        <input type="hidden" value="0" id="hdnCategoryID" />
        <input type="hidden" value="0" id="hdnTypeID" />
        <input type="hidden" value="0" id="hdnUnitID" />
        <input type="hidden" value="0" id="hdnTaxID" />
        <input type="hidden" value="0" id="hdnVehicleID" />
        <input type="hidden" value="0" id="hdnGroupID" />
        <input type="hidden" value="0" id="hdnDespatchID" />
        <input type="hidden" value="0" id="hdnSupplierID" />
        <input type="hidden" value="0" id="hdnCustomerID" />
        <input type="hidden" value="0" id="hdServiceId" />
        <input type="hidden" value="0" id="hdLeadId" />
        <asp:HiddenField ID="hdnID" runat="server" ClientIDMode="Static" Value="0" />
        <asp:HiddenField ID="hdnSectionName" runat="server" ClientIDMode="Static" Value="0" />
        <asp:HiddenField ID="hdnMode" runat="server" ClientIDMode="Static" Value="0" />

    </div>

    <script>
        $(document).ready(function () {
            $(".tab-content > .tab-pane > .panel").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });
            $('#btnProfileUpload').change(function () {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(this.files[0]);
            });
            $('#btnCustomerProfileUpload').change(function () {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgCustomerPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(this.files[0]);
            });
            $('#btnLeadProfileUpload').change(function () {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgLeadPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(this.files[0]);
            });
            $('.remove-pic').click(function () {
                $('#imgPhoto').attr('src', '../Theme/images/profile-pic.jpg');
                $('#imgCustomerPhoto').attr('src', '../Theme/images/profile-pic.jpg');
                $('#imgLeadPhoto').attr('src', '../Theme/images/profile-pic.jpg');
            });
            //Fetching API url
            var apirul = $('#hdApiUrl').val();

            CheckSection();

            //For Loading The items section on page load(Initial Load)
            RefreshTableItem();
            loadAdditionalData();
            loadSupplierCountry();
            loadCustomerCountry();
            loadLeadCountry();

            //Gets the datatable for the selected li
            $('#menu > li').click(function () {
                var selected = $(this).attr('id');
                switch (selected) {
                    case 'Item':
                        RefreshTableItem();
                        loadAdditionalData();
                        break;
                    case 'Brand':
                        RefreshTableBrand();
                        break;
                    case 'Group':
                        RefreshTableGroup();
                        break;
                    case 'Category':
                        RefreshTableCategory();
                        break;
                    case 'Type':
                        RefreshTableType();
                        break;
                    case 'Tax':
                        RefreshTableTax();
                        break;
                    case 'Unit':
                        RefreshTableUnit();
                        break;
                    case 'Vehicles':
                        RefreshTableVehicle();
                        break;
                    case 'Despatch':
                        RefreshTableDespatch();
                        break;
                    case 'Supplier':
                        RefreshTableSuppliers();
                        loadSupplierCountry();
                    case 'Customer':
                        RefreshTableCustomer();
                        loadCustomerCountry();
                        break;
                    case 'Leads':
                        RefreshTableLead(null,null,null,null);
                        loadLeadCountry();
                        loadLeadEmployee();
                        break;
                        //case 'service':
                        //    RefreshTableService();
                        //    loadAdditionalData();
                        //    break;
                    default:
                        break;
                }
            });

            $('#Lead').click(function () {
                alert('');
            })

            $('#serviceTab').click(function () {
                RefreshTableService();
                loadAdditionalData();
            })
            ///* Functions to load the Tables *///

            //Brand Table
            function RefreshTableBrand() {
                $.ajax({
                    url: apirul + '/api/Brands/get',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableBrand').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Order' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-brand"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-brand"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Service Table
            function RefreshTableService() {

                $.ajax({
                    url: apirul + '/api/Items/GetDetailsForService',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableService').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ItemID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'SellingPrice' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-service"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-service"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Item table
            function RefreshTableItem() {
                $.ajax({
                    url: apirul + '/api/Items/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableItem').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ItemID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'TaxPercentage' },
                                { data: 'Brand' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Item"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-item"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Group Table
            function RefreshTableGroup() {
                $.ajax({
                    url: apirul + '/api/Groups/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableGroup').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Order' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Group"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-group"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Category Table
            function RefreshTableCategory() {
                $.ajax({
                    url: apirul + '/api/Categories/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableCategory').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                               { data: 'Order' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Category"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-category"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Product type Table
            function RefreshTableType() {
                $.ajax({
                    url: apirul + '/api/ProductTypes/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableType').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Order' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Type"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-type"><i class="fa fa-times" style="color:red"></i></a>'
                                    }
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Tax Table
            function RefreshTableTax() {
                $.ajax({
                    url: apirul + '/api/Taxes/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableTax').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Percentage' },
                                { data: 'Type' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-tax"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-tax"><i class="fa fa-times" style="color:red"></i></a>'
                                    }
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Unit Table
            function RefreshTableUnit() {
                $.ajax({
                    url: apirul + '/api/units/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableUnit').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'ShortName' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Unit"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-unit"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Vehicle Table
            function RefreshTableVehicle() {
                $.ajax({
                    url: apirul + '/api/Vehicles/get',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableVehicle').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Number' },
                                { data: 'Type' },
                                { data: 'Owner' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-vehicle"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-vehicle"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Despatch Table

            function RefreshTableDespatch() {
                $.ajax({
                    url: apirul + '/api/despatch/get',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableDespatch').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Address' },
                                { data: 'MobileNo' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-despatch"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-despatch"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Suppliers Table
            function RefreshTableSuppliers() {
                $.ajax({
                    url: apirul + '/api/Suppliers/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableSuppliers').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Phone1' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Supplier"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-supplier"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Customer Table
            function RefreshTableCustomer() {
                $.ajax({
                    url: apirul + '/api/Customers/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableCustomer').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                   { data: 'Phone1' },
                                        { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-customer"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-customer"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Lead Table 
            function RefreshTableLead(status,employeeId,from,to) {
                $.ajax({
                    url: apirul + '/api/Leads/GetLeads/?Status='+status+'&EmployeeId='+employeeId+'&From='+from+'&To='+to,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableLead').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                   { data: 'Phone1' },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Lead"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-Lead"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();


                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }
            //////* Functions to Save or Update *//////

            //Saves Brand
            $('#btnAddBrand').click(function () {
                var brand = {};
                brand.Name = $('#txtBrandName').val();
                brand.Order = $('#txtBrandOrder').val();
                brand.Status = $('#ddlBrandStatus').val();
                brand.ID = $('#hdnBrandID').val();
                brand.CompanyId = $.cookie("bsl_1");
                brand.CreatedBy = $.cookie('bsl_3');
                brand.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Brands/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(brand),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            RefreshTableBrand();
                            reset();
                            $('#btnAddBrand').html('Save');
                            $('#hdnBrandID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Service
            $('#btnAddService').click(function () {
                var item = {};
                item.Name = $('#txtServiceName').val();
                item.ItemCode = $('#txtServiceItemCode').val();
                item.Description = $('#txtServiceDescription').val();
                item.SellingPrice = $('#txtServiceRate').val();
                item.TaxId = $('#ddlServiceTax').val();
                item.Status = $('#ddlServiceStatus').val();
                item.ItemID = $('#hdServiceId').val();
                item.IsService = 1;
                item.CompanyId = $.cookie("bsl_1");
                item.CreatedBy = $.cookie('bsl_3');
                item.ModifiedBy = $.cookie('bsl_3');
                if ($('#chkTrackService').prop('checked') == true) {
                    track_Inventory = 1;
                }
                else {
                    track_Inventory = 0;
                }
                item.TrackInventory = track_Inventory;
                $.ajax({
                    url: apirul + 'api/Items/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(item),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            RefreshTableService();
                            reset();
                            $('#btnAddService').html('<i></i>&nbsp;Save');
                            $('#hdServiceId').val("0");
                            $('.track-service').attr('checked', false);
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });


            //Saves Category
            $('#btnAddCategory').click(function () {
                var Category = {};
                Category.Name = $('#txtCategoryName').val();
                Category.Order = $('#txtCategoryOrder').val();
                Category.Status = $('#ddlCategoryStatus').val();
                Category.CompanyId = $.cookie("bsl_1");
                Category.CreatedBy = $.cookie('bsl_3');
                Category.ID = $('#hdnCategoryID').val();
                Category.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Categories/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Category),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            RefreshTableCategory();
                            reset();
                            $('#btnAddCategory').html('Save');
                            $('#hdnCategoryID').val("0");
                        }
                        else {
                            errorAlert(data.Message)
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Saves Group
            $('#btnAddNewGroup').click(function () {
                var Group = {};
                Group.Name = $('#txtGroupName').val();
                Group.Order = $('#txtGroupOrder').val();
                Group.Status = $('#ddlGroupStatus').val();
                Group.ID = $('#hdnGroupID').val();
                Group.CompanyId = $.cookie("bsl_1");
                Group.CreatedBy = $.cookie('bsl_3');
                Group.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Groups/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Group),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableGroup();
                            $('#btnAddNewGroup').html('Save');
                            $('#hdnGroupID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Saves Product Type
            $('#btnAddType').click(function () {
                var Type = {};
                Type.Name = $('#txtTypeName').val();
                Type.Order = $('#txtTypeOrder').val();
                Type.Status = $('#ddlTypeStatus').val();
                Type.CompanyId = $.cookie("bsl_1");
                Type.CreatedBy = $.cookie('bsl_3');
                Type.ID = $('#hdnTypeID').val();
                Type.Modifiedby = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/ProductTypes/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Type),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableType();
                            $('#btnAddType').html('Save');
                            $('#hdnTypeID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Tax
            $('#btnAddTax').click(function () {
                var Tax = {};
                Tax.Name = $('#txtTaxName').val();
                Tax.Percentage = $('#txtTaxPercentage').val();
                Tax.Type = $('#txtTaxType').val();
                Tax.Status = $('#ddlTaxStatus').val();
                Tax.ID = $('#hdnTaxID').val();
                Tax.ModifiedBy = $.cookie('bsl_3');
                Tax.CompanyId = $.cookie("bsl_1");
                Tax.CreatedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Taxes/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Tax),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            RefreshTableTax();
                            reset();
                            $('#btnAddTax').html('Save');
                            $('#hdnTaxID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Unit
            $('#btnAddUOM').click(function () {
                var Unit = {};
                Unit.Name = $('#txtUOMName').val();
                Unit.ShortName = $('#txtUOMShortName').val();
                Unit.Status = $('#ddlUOMStatus').val();
                Unit.ID = $('#hdnUnitID').val();
                Unit.ModifiedBy = $.cookie('bsl_3');
                Unit.CompanyId = $.cookie("bsl_1");
                Unit.CreatedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Units/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Unit),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableUnit();
                            $('#btnAddUOM').html('Save');
                            $('#hdnUnitID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Items
            $('#btnAddItem').click(function () {
                var track_Inventory = 0;
                //Added to check whether the item should check the stock in entry page
                //If Track Inventory is true or 1 checks the stock at the time of entry
                if ($('#chkTrack').prop('checked') == true) {
                    track_Inventory = 1;
                }
                else {
                    track_Inventory = 0;
                }
                var Item = {};
                Item.Name = $('#txtItemName').val();
                Item.ItemCode = $('#txtItemCode').val();
                Item.UnitID = $('#ddlItemUOM').val();
                Item.Description = $('#txtItemDescription').val();
                Item.HSCode = $('#txtItemHSCode').val();
                Item.OEMCode = $('#txtItemHSCode').val();
                Item.TypeID = $('#ddlItemType').val();
                Item.CategoryID = $('#ddlItemCategory').val();
                Item.GroupID = $('#ddlItemGroup').val();
                Item.BrandID = $('#ddlItemBrand').val();
                Item.TaxId = $('#ddlItemTax').val();
                Item.MRP = $('#txtItemMRP').val();
                Item.Barcode = $('#txtItemBarcode').val();
                Item.SellingPrice = $('#txtItemSP').val();
                $('#lblPriceText').addClass('hidden')
                Item.CostPrice = $('#txtItemCP').val();
                Item.ItemCode = $('#txtItemCode').val();
                Item.Status = $('#ddlItemStatus').val();
                Item.ItemID = $('#hdnItemID').val();
                Item.ModifiedBy = $.cookie('bsl_3');
                Item.CompanyId = $.cookie("bsl_1");
                Item.CreatedBy = $.cookie('bsl_3');
                Item.TrackInventory = track_Inventory;
                console.log(Item);
                $.ajax({
                    url: apirul + 'api/Items/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Item),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableItem();
                            $('#btnAddItem').html('<i ></i>Save');
                            $('#hdnItemID').val("0");
                            $('#instanceTable').addClass('hidden');
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Vehicles
            $('#btnAddVehicle').click(function () {
                var Vehicle = {};
                Vehicle.Name = $('#txtVehicleName').val();
                Vehicle.Number = $('#txtVehicleNumber').val();
                Vehicle.Type = $('#txtVehicleType').val();
                Vehicle.Owner = $('#txtVehicleOwner').val();
                Vehicle.Status = $('#ddlVehicleStatus').val();
                Vehicle.ID = $('#hdnVehicleID').val();
                Vehicle.ModifiedBy = $.cookie('bsl_3');
                Vehicle.CompanyId = $.cookie("bsl_1");
                Vehicle.CreatedBy = $.cookie('bsl_3');
                console.log(Vehicle);
                $.ajax({
                    url: apirul + 'api/Vehicles/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Vehicle),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableVehicle();
                            $('#btnAddVehicle').html('Save');
                            $('#hdnVehicleID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Despatch
            $('#btnAddDespatch').click(function () {
                var Despatch = {};
                Despatch.Name = $('#txtDespatchName').val();
                Despatch.Address = $('#txtDespatchAddress').val();
                Despatch.PhoneNo = $('#txtDespatchNumber').val();
                Despatch.MobileNo = $('#txtDespatchMobile').val();
                Despatch.ContactPerson = $('#txtDespatchContact').val();
                Despatch.ContactPersonPhone = $('#txtDespatchContactPhone').val();
                Despatch.Status = $('#ddlDespatchStatus').val();
                Despatch.Narration = $('#txtDespatchNarration').val();
                Despatch.ID = $('#hdnDespatchID').val();
                Despatch.ModifiedBy = $.cookie('bsl_3');
                Despatch.CompanyId = $.cookie("bsl_1");
                Despatch.CreatedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Despatch/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Despatch),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableDespatch();
                            $('#btnAddDespatch').html('Save');
                            $('#hdnDespatchID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Save Supplier
            $('#btnAddSupplier').click(function () {
                var Supplier = {};
                Supplier.Name = $('#txtSupplierName').val();
                Supplier.Address1 = $('#txtSupplierAddres1').val();
                Supplier.Address2 = $('#txtSupplierAddress2').val();
                Supplier.CountryId = $('#ddlSupplierCountry').val();
                Supplier.StateId = $('#ddlSupplierState').val();
                Supplier.Phone1 = $('#txtSupplierPhone1').val();
                Supplier.Phone2 = $('#txtSupplierPhone2').val();
                Supplier.Email = $('#txtSupplierEmail').val();
                Supplier.Taxno1 = $('#txtSupplierTax1').val();
                Supplier.Taxno2 = $('#txtSupplierTax1').val();
                Supplier.Status = $('#ddlSupplierStatus').val();
                Supplier.ID = $('#hdnSupplierID').val();
                Supplier.Salutation = $('#ddlSupplierSalutation').val();
                Supplier.City = $('#txtSupplierCity').val();
                Supplier.ZipCode = $('#txtSupplierZipCode').val();
                Supplier.Contact_Name = $('#txtSupplierContactName').val();
                Supplier.ProfileImageB64 = $('#imgPhoto').attr('src').split(',')[1];
                Supplier.ModifiedBy = $.cookie('bsl_3');
                Supplier.CompanyId = $.cookie("bsl_1");
                Supplier.CreatedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apirul + 'api/Suppliers/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Supplier),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableSuppliers();
                            $('#btnAddSupplier').html('Save');
                            $('#hdnSupplierID').val("0");
                            $('#imgPhoto').attr('src', '../Theme/images/profile-pic.jpg');
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });

            });


            //Save Customer
            $('#btnAddCustomer').click(function () {
                var Customer = {};
                Customer.Name = $('#txtCustomerName').val();
                Customer.Address1 = $('#txtCustomerAddress1').val();
                Customer.Address2 = $('#txtCustomerAddress2').val();
                Customer.CountryId = $('#ddlCustomerCountry').val();
                Customer.StateId = $('#ddlCustomerState').val();
                Customer.Phone1 = $('#txtCustomerPhone1').val();
                Customer.Phone2 = $('#txtCustomerPhone2').val();
                Customer.Email = $('#txtCustomerEmail').val();
                Customer.Taxno1 = $('#txtCustomerTax1').val();
                Customer.Taxno2 = $('#txtCustomerTax2').val();
                Customer.CreditAmount = $('#txtCustomerCreditAmount').val();
                Customer.CreditPeriod = $('#txtCustomerCreditPeriod').val();
                Customer.LockPeriod = $('#txtCustomerLockPeriod').val();
                Customer.LockAmount = $('#txtCustomerLockAmount').val();
                Customer.ID = $('#hdnCustomerID').val();
                Customer.Status = $('#ddlCustomerStatus').val();
                Customer.ModifiedBy = $.cookie('bsl_3');
                Customer.CompanyId = $.cookie("bsl_1");
                Customer.CreatedBy = $.cookie('bsl_3');
                Customer.Salutation = $('#ddlCustSalutation').val();
                Customer.City = $('#txtCustomerCity').val();
                Customer.ZipCode = $('#txtCustomerZipCode').val();
                Customer.Contact_Name = $('#txtCustContactName').val();
                Customer.ProfileImageB64 = $('#imgCustomerPhoto').attr('src').split(',')[1];
                console.log(Customer);
                $.ajax({
                    url: apirul + 'api/Customers/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Customer),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableCustomer();
                            $('#btnAddCustomer').html('Save');
                            $('#hdnCustomerID').val("0");
                            $('#imgCustomerPhoto').attr('src', '../Theme/images/profile-pic.jpg');
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            })


            //Save Lead
            $('#btnAddLead').click(function () {
                var Lead = {};
                Lead.Name = $('#txtLeadName').val();
                Lead.Address1 = $('#txtLeadAddr1').val();
                Lead.Address2 = $('#txtLeadAddr2').val();
                Lead.CountryId = $('#ddlLeadCountry').val();
                Lead.StateId = $('#ddlLeadState').val();
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
                Lead.ProfileImageB64 = $('#imgLeadPhoto').attr('src').split(',')[1];
                Lead.Source = $('#ddlLeadSource').val();
                Lead.AssignId = $('#ddlAssign').val();
                $.ajax({
                    url: apirul + 'api/Leads/SaveLeads',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Lead),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            successAlert(data.Message);
                            reset();
                            RefreshTableLead(null,null,null,null);
                            $('#btnAddLead').html('Save');
                            $('#hdLeadId').val("0");
                            $('#imgLeadPhoto').attr('src', '../Theme/images/profile-pic.jpg');
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });

            })

            //////* Functions To Get The Data For Update *//////

            //Edit Brand
            $(document).on('click', '.edit-entry-brand', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                console.log(id);
                $.ajax({
                    url: apirul + '/api/Brands/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtBrandName').val(response.Name);
                        $('#txtBrandOrder').val(response.Order);
                        $('#ddlBrandStatus').val(response.Status);
                        $('#hdnBrandID').val(response.ID);
                        $('#btnAddBrand').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Edit Service
            $(document).on('click', '.edit-entry-service', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                $.ajax({
                    url: apirul + '/api/Items/GetDetailsForService/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        console.log(response[0]);
                        $('#txtServiceName').val(response[0].Name);
                        $('#txtServiceDescription').val(response[0].Description);
                        $('#ddlServiceTax').val(response[0].TaxId);
                        $('#txtServiceRate').val(response[0].SellingPrice);
                        $('#ddlServiceStatus').val(response[0].Status);
                        $('#hdServiceId').val(response[0].ItemID);
                        $('#txtServiceItemCode').val(response[0].ItemCode);
                        $('#btnAddService').html('<i></i>Update');
                        if (response[0].TrackInventory) {
                            $('.track-service').attr('checked', true);
                        }
                        else {
                            $('.track-service').attr('checked', false)
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });


            //Edit Group
            $(document).on('click', '.edit-entry-Group', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Groups/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtGroupName').val(response.Name);
                        $('#txtGroupOrder').val(response.Order);
                        $('#ddlGroupStatus').val(response.Status);
                        $('#hdnGroupID').val(response.ID);
                        $('#btnAddNewGroup').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });


            //Edit Category
            $(document).on('click', '.edit-entry-Category', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Categories/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtCategoryName').val(response.Name);
                        $('#txtCategoryOrder').val(response.Order);
                        $('#ddlCategoryStatus').val(response.Status);
                        $('#hdnCategoryID').val(response.ID);
                        $('#btnAddCategory').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });


            //Edit Product Type
            $(document).on('click', '.edit-entry-Type', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/ProductTypes/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtTypeName').val(response.Name);
                        $('#txtTypeOrder').val(response.Order);
                        $('#ddlTypeStatus').val(response.Status);
                        $('#hdnTypeID').val(response.ID);
                        $('#btnAddType').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Edit Tax
            $(document).on('click', '.edit-entry-tax', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Taxes/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtTaxName').val(response.Name);
                        $('#txtTaxPercentage').val(response.Percentage);
                        $('#txtTaxType').val(response.Type);
                        $('#ddlTaxStatus').val(response.Status);
                        $('#hdnTaxID').val(response.ID);
                        $('#btnAddTax').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });


            //Edit Unit
            $(document).on('click', '.edit-entry-Unit', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Units/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtUOMName').val(response.Name);
                        $('#txtUOMShortName').val(response.ShortName);
                        $('#ddlUOMStatus').val(response.Status);
                        $('#hdnUnitID').val(response.ID);
                        $('#btnAddUOM').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });


            //Edit Items
            $(document).on('click', '.edit-entry-Item', function () {
                $('#instanceTable').removeClass('hidden');
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + 'api/Items/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#instanceTable tbody').children().remove();
                        //$('#txtItemCode').prop('disabled', false);
                        $('#txtItemCode').val(response.ItemCode);
                        $('#txtItemName').val(response.Name);
                        $('#txtItemDescription').val(response.Description);
                        $('#txtRemarks').val(response.Remarks);
                        $('#txtItemOEMCode').val(response.OEMCode);
                        $('#txtItemHSCode').val(response.HSCode);
                        $('#ddlItemUOM').val(response.UnitID);
                        $('#ddlItemTax').val(response.TaxId);
                        $('#ddlItemType').val(response.TypeID);
                        $('#ddlItemCategory').val(response.CategoryID);
                        $('#ddlItemGroup').val(response.GroupID);
                        $('#ddlItemBrand').val(response.BrandID);
                        $('#txtItemBarcode').val(response.Barcode);
                        $('#hdnItemID').val(response.ItemID);
                        $('#txtItemMRP').val(response.MRP);
                        $('#txtItemCP').val(response.CostPrice);
                        $('#txtItemSP').val(response.SellingPrice);
                        $('#lblPriceText').addClass('hidden')
                        $('#ddlItemStatus').val(response.Status);
                        //Added to check whether the item should check the stock in entry page
                        //If Track Inventory is true checks the stock at the time of entry
                        if (response.TrackInventory) {
                            $('.track').attr('checked', true);
                        }
                        else {
                            $('.track').attr('checked', false)
                        }
                        $('#btnAddItem').html('<i ></i>&nbsp;Update');
                        $(response.Instances).each(function () {
                            $('#instanceTable tbody').prepend('<tr><td style="display:none">' + this.ID + '</td><td contenteditable="false">' + this.Mrp + '</td><td contenteditable="false">' + this.CostPrice + '</td><td contenteditable="false">' + this.SellingPrice + '</td><td contenteditable="false">' + this.Barcode + '</td><td><i class="text-info md-edit editInstance"></i>&nbsp;<i class="md-delete text-danger deleteInstance"></i>&nbsp;<i class="md-clear text-danger cancelInstance hidden"></i></td></tr>');
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Edit Vehicles
            $(document).on('click', '.edit-entry-vehicle', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Vehicles/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtVehicleName').val(response.Name);
                        $('#txtVehicleNumber').val(response.Number);
                        $('#txtVehicleType').val(response.Type);
                        $('#txtVehicleOwner').val(response.Owner);
                        $('#ddlVehicleStatus').val(response.Status);
                        $('#hdnVehicleID').val(response.ID);
                        $('#btnAddVehicle').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Edit Despatch
            $(document).on('click', '.edit-entry-despatch', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Despatch/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtDespatchName').val(response.Name);
                        $('#txtDespatchAddress').val(response.Address);
                        $('#txtDespatchNumber').val(response.PhoneNo);
                        $('#txtDespatchMobile').val(response.MobileNo);
                        $('#txtDespatchContact').val(response.ContactPerson);
                        $('#txtDespatchContactPhone').val(response.ContactPersonPhone);
                        $('#txtDespatchNarration').val(response.Narration);
                        $('#ddlDespatchStatus').val(response.Status);
                        $('#hdnDespatchID').val(response.ID);
                        $('#btnAddDespatch').html('<i></i>Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Edit Suppliers
            $(document).on('click', '.edit-entry-Supplier', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Suppliers/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {

                        var countryId = response.CountryId;
                        $.ajax({
                            url: apirul + '/api/Customers/getStates/' + countryId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (data) {
                                $('#ddlSupplierCountry').val(response.CountryId);
                                $('#ddlSupplierState').children('option').remove();
                                $('#ddlSupplierState').append('<option value="0">--select--</option>');
                                $(data).each(function () {
                                    $('#ddlSupplierState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                });
                                $('#txtSupplierName').val(response.Name);
                                $('#txtSupplierAddres1').val(response.Address1);
                                $('#txtSupplierAddress2').val(response.Address2);
                                $('#txtSupplierPhone1').val(response.Phone1);
                                $('#txtSupplierPhone2').val(response.Phone2);
                                $('#txtSupplierEmail').val(response.Email);
                                $('#txtSupplierTax1').val(response.Taxno1);
                                $('#txtSupplierTax2').val(response.Taxno2);
                                $('#ddlSupplierStatus').val(response.Status);
                                $('#ddlSupplierState :selected').val(response.StateId);
                                $('#ddlSupplierState').select2('val', response.StateId);
                                $('#ddlSupplierState').val(response.StateId);
                                $('#ddlSupplierCountry').val(response.CountryId);
                                $('#hdnSupplierID').val(response.ID);
                                $('#txtSupplierCity').val(response.City);
                                $('#txtSupplierZipCode').val(response.ZipCode);
                                $('#ddlSupplierSalutation').val(response.Salutation).trigger('change');
                                $('#txtSupplierContactName').val(response.Contact_Name);
                                $('#imgPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                                $('#btnAddSupplier').html('<i></i>Update');
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

            //Edit Customer
            $(document).on('click', '.edit-entry-customer', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Customers/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        var countryId = response.CountryId;
                        $.ajax({
                            url: apirul + '/api/Customers/getStates/' + countryId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (data) {
                                $('#ddlCustomerCountry').val(response.CountryId);
                                $('#ddlCustomerState').children('option').remove();
                                $('#ddlCustomerState').append('<option value="0">--select--</option>');
                                $(data).each(function () {
                                    $('#ddlCustomerState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                });
                                $('#ddlCustomerState').val(response.StateId);
                                $('#txtCustomerName').val(response.Name);
                                $('#txtCustomerAddress1').val(response.Address1);
                                $('#txtCustomerAddress2').val(response.Address2);
                                $('#txtCustomerPhone1').val(response.Phone1);
                                $('#txtCustomerPhone2').val(response.Phone2);
                                $('#txtCustomerTax1').val(response.Taxno1);
                                $('#txtCustomerTax2').val(response.Taxno2);
                                $('#txtCustomerEmail').val(response.Email);
                                //$('#txtCustomerTax1').val(response.Taxno1);
                                //$('#txtCustomertax2').val(response.Taxno2);
                                $('#txtCustomerCreditAmount').val(response.CreditAmount);
                                $('#txtCustomerCreditPeriod').val(response.CreditPeriod);
                                $('#txtCustomerLockAmount').val(response.LockAmount);
                                $('#txtCustomerLockPeriod').val(response.LockPeriod);
                                $('#ddlCustomerStatus').val(response.Status);
                                $('#hdnCustomerID').val(response.ID);
                                $('#txtCustomerCity').val(response.City);
                                $('#txtCustomerZipCode').val(response.ZipCode);
                                $('#txtCustContactName').val(response.Contact_Name);
                                $('#ddlCustSalutation').val(response.Salutation).trigger('change');
                                $('#imgCustomerPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                                $('#btnAddCustomer').html('<i></i>Update');
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

            //Edit Lead
            $(document).on('click', '.edit-entry-Lead', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Leads/GetLeads/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        var countryId = response.CountryId;
                        $.ajax({
                            url: apirul + '/api/Customers/getStates/' + countryId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (data) {
                                $('#ddlLeadCountry').val(response.CountryId);
                                $('#ddlLeadState').children('option').remove();
                                $('#ddlLeadState').append('<option value="0">--select--</option>');
                                $(data).each(function () {
                                    $('#ddlLeadState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                });
                                $('#ddlLeadState').val(response.StateId);
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
                                $('#ddlAssign').val(response.AssignId);
                                $('#imgLeadPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                                $('#btnAddLead').html('<i></i>Update');
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
            //////* Functions To Delete the Selected data *//////

            //Delete brand
            $(document).on('click', '.delete-entry-brand', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                var modifiedBy = ($.cookie("bsl_3"));
                deleteMaster({
                    "url": apirul + '/api/Brands/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Brand has been deleted from inventory',
                    "successFunction": RefreshTableBrand
                });
            });

            //Delete Service
            $(document).on('click', '.delete-entry-service', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                var modifiedBy = ($.cookie("bsl_3"));
                deleteMaster({
                    "url": apirul + '/api/Items/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Service has been deleted from inventory',
                    "successFunction": RefreshTableService
                });
            });

            //Delete Group
            $(document).on('click', '.delete-entry-group', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Groups/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Group has been deleted from inventory',
                    "successFunction": RefreshTableGroup
                });
            });

            //Delete Category
            $(document).on('click', '.delete-entry-category', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apirul + '/api/Categories/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTableCategory
                });
            });

            //Delete Item
            $(document).on('click', '.delete-entry-item', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Items/delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTableItem
                });
            });

            //Delete Type
            $(document).on('click', '.delete-entry-type', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/ProductTypes/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTableType
                });
            });

            //Delete Tax
            $(document).on('click', '.delete-entry-tax', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Taxes/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTableTax
                });
                reset();
            });

            //Delete Unit
            $(document).on('click', '.delete-entry-unit', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Units/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Unit has been deleted from inventory',
                    "successFunction": RefreshTableUnit
                });
                reset();
            });

            //Delete Vehicles
            $(document).on('click', '.delete-entry-vehicle', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Vehicles/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Vehicle has been deleted from inventory',
                    "successFunction": RefreshTableVehicle
                });
                reset();
            });


            //Delete Despatch
            $(document).on('click', '.delete-entry-despatch', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                var modifiedBy = ($.cookie("bsl_3"));

                deleteMaster({
                    "url": apirul + '/api/Despatch/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Despatch has been deleted from inventory',
                    "successFunction": RefreshTableDespatch
                });
                reset();
            });

            //Delete Supplier
            $(document).on('click', '.delete-entry-supplier', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Suppliers/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',

                    "successFunction": RefreshTableSuppliers
                });
                reset();
            });

            //Delete Customer
            $(document).on('click', '.delete-entry-customer', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Customers/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTableCustomer
                });
                reset();
            });

            //Delete Lead
            $(document).on('click', '.delete-entry-Lead', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Leads/DeleteLeads/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Lead has been deleted from inventory',
                    "successFunction": RefreshTableLead
                });
                reset();
            });
            //////* Additional Funtions *//////

            //Function to Load the dropdown list data for Items
            function loadAdditionalData() {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    type: "POST",
                    async: false,
                    url: apirul + "api/items/GetAsscociateData?CompanyID=" + Company,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($.cookie("bsl_1")),
                    dataType: "json",
                    success: function (data) {
                        $('#ddlItemType').empty();
                        $('#ddlItemType').append('<option value="0">--Select--</option>');
                        $('#ddlItemBrand').empty();
                        $('#ddlItemBrand').append('<option value="0">--Select--</option>');
                        $('#ddlItemCategory').empty();
                        $('#ddlItemCategory').append('<option value="0">--Select--</option>');
                        $('#ddlItemGroup').empty();
                        $('#ddlItemGroup').append('<option value="0">--Select--</option>');
                        $('#ddlItemTax').empty();
                        $('#ddlItemTax').append('<option value="0">--Select--</option>');
                        $('#ddlServiceTax').empty();
                        $('#ddlServiceTax').append('<option value="0">--Select--</option>');
                        $('#ddlItemUOM').empty();
                        $('#ddlItemUOM').append('<option value="0">--Select--</option>');
                        $.each(data.Types, function () {
                            $("#ddlItemType").append($("<option/>").val(this.Type_Id).text(this.Name));
                        });
                        $.each(data.Brands, function () {
                            $('#ddlItemBrand').append($("<option/>").val(this.Brand_ID).text(this.Name));
                        });
                        $.each(data.Categories, function () {
                            $('#ddlItemCategory').append($("<option/>").val(this.Category_Id).text(this.Name));
                        });
                        $.each(data.Groups, function () {
                            $('#ddlItemGroup').append($("<option/>").val(this.Group_ID).text(this.Name));
                        });
                        $.each(data.Taxes, function () {
                            $('#ddlItemTax').append($("<option/>").val(this.Tax_Id).text(this.Percentage));
                        });
                        $.each(data.Taxes, function () {
                            $('#ddlServiceTax').append($("<option/>").val(this.Tax_Id).text(this.Percentage));
                        });
                        $.each(data.Unit, function () {
                            $('#ddlItemUOM').append($("<option/>").val(this.Unit_Id).text(this.Short_Name));
                        });
                     
                    },
                    failure: function () {
                        $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                        $('#masterSectionPError').css('display', 'block');
                    }
                });
            }


            //Function to load supplier country
            function loadSupplierCountry() {
                var company = $.cookie("bsl_1");
                $.ajax({
                    type: "POST",
                    url: apirul + "api/customers/Getcountry?companyId=" + company,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($.cookie("bsl_1")),
                    dataType: "json",
                    success: function (data) {
                        $('ddlSupplierCountry').empty();
                        $('ddlSupplierCountry').append('<option value="0">--Select--</option>');
                        $.each(data, function () {
                            $("#ddlSupplierCountry").append($("<option/>").val(this.Country_Id).text(this.Name));
                        });
                    },
                    failure: function () {
                        console.log("Error")
                    }
                });
            }


            //Function to load Customer country
            function loadCustomerCountry() {
                var company = $.cookie("bsl_1");
                $.ajax({
                    type: "POST",
                    url: apirul + "api/customers/Getcountry?companyId=" + company,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($.cookie("bsl_1")),
                    dataType: "json",
                    success: function (data) {
                        $('ddlCustomerCountry').empty();
                        $('ddlCustomerCountry').append('<option value="0">--Select--</option>');
                        $.each(data, function () {
                            $("#ddlCustomerCountry").append($("<option/>").val(this.Country_Id).text(this.Name));
                        });
                    },
                    failure: function () {
                        console.log("Error")
                    }
                });
            }

            //Function to load Lead country
            function loadLeadCountry() {
                var company = $.cookie("bsl_1");
                $.ajax({
                    type: "POST",
                    url: apirul + "api/customers/Getcountry?companyId=" + company,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($.cookie("bsl_1")),
                    dataType: "json",
                    success: function (data) {
                        $('ddlLeadCountry').empty();
                        $('ddlLeadCountry').append('<option value="0">--Select--</option>');
                        $.each(data, function () {
                            $("#ddlLeadCountry").append($("<option/>").val(this.Country_Id).text(this.Name));
                        });
                    },
                    failure: function () {
                        console.log("Error")
                    }
                });
            }

            //load employee
            function loadLeadEmployee() {
                var company = $.cookie("bsl_1");
                $.ajax({
                    type: "POST",
                    url: apirul + "api/Leads/GetEmployee?companyId=" + company,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($.cookie("bsl_1")),
                    dataType: "json",
                    success: function (data) {
                        console.log(data)
                        $('ddlAssign').empty();
                        $('ddlAssign').append('<option value="0">--Select--</option>');
                        $.each(data, function () {
                            $("#ddlAssign").append($("<option/>").val(this.Employee_Id).text(this.First_Name));
                        });
                    },
                    failure: function () {
                        console.log("Error")
                    }
                });
            }

            //Event to load State by getting the coountry ID
            $('#ddlSupplierCountry').change(function () {
                var selected = $('#ddlSupplierCountry').val();
                if (selected == 0) {
                    $('#ddlSupplierState').empty();
                    $('#ddlSupplierState').append("<option>--Select States--</option>")
                }
                else {
                    $('#ddlSupplierState').empty();
                    $('#ddlSupplierState').append("<option>--Select States--</option>")
                    $.ajax({
                        type: "POST",
                        url: apirul + "api/customers/GetStates?id=" + selected,
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($.cookie("bsl_1")),
                        dataType: "json",
                        success: function (data) {
                            $('#ddlSupplierState').empty;
                            $.each(data, function () {
                                $("#ddlSupplierState").append($("<option/>").val(this.StateId).text(this.State));
                            });
                        },
                        failure: function () {
                            console.log("Error");
                        }
                    });
                }
            });

            //Load Customer State by getting the coountry ID
            $('#ddlCustomerCountry').change(function () {
                var selected = $('#ddlCustomerCountry').val();
                if (selected == 0) {
                    $('#ddlCustomerState').empty();
                    $('#ddlCustomerState').append("<option value='0'>--Select States--</option>")
                }
                else {
                    $('#ddlCustomerState').empty();
                    $('#ddlCustomerState').append("<option value='0'>--Select States--</option>")
                    $.ajax({
                        type: "POST",
                        url: apirul + "api/customers/GetStates?id=" + selected,
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($.cookie("bsl_1")),
                        dataType: "json",
                        success: function (data) {
                            $('#ddlCustomerState').empty;
                            $.each(data, function () {
                                $("#ddlCustomerState").append($("<option/>").val(this.StateId).text(this.State));
                            });
                        },
                        failure: function () {
                            console.log("Error");
                        }
                    });
                }
            });

            //Load Lead country state by getting the country id
            $('#ddlLeadCountry').change(function () {
                var selected = $('#ddlLeadCountry').val();
                if (selected == 0) {
                    $('#ddlLeadState').empty();
                    $('#ddlLeadState').append("<option>--Select States--</option>")
                }
                else {
                    $('#ddlLeadState').empty();
                    $('#ddlLeadState').append("<option>--Select States--</option>")
                    $.ajax({
                        type: "POST",
                        url: apirul + "api/customers/GetStates?id=" + selected,
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($.cookie("bsl_1")),
                        dataType: "json",
                        success: function (data) {
                            $('#ddlLeadState').empty;
                            $.each(data, function () {
                                $("#ddlLeadState").append($("<option/>").val(this.StateId).text(this.State));
                            });
                        },
                        failure: function () {
                            console.log("Error");
                        }
                    });
                }
            });

            //Adding Instances
            $('.addInstance').click(function () {
                var editingCount = $('.updateInstance').length;
                if (editingCount > 0) {
                }
                else if ($('#hdnItemID').val() != '0') {
                    $('#instanceTable tbody').prepend('<tr><td style="display:none">0</td><td contenteditable="true">0.00</td><td contenteditable="true">0.00</td><td contenteditable="true">0.00</td><td contenteditable="true">0</td><td><i class="md-done text-info updateInstance"></i>&nbsp;&nbsp;&nbsp;<i class="md-delete text-danger deleteInstance hidden"></i>&nbsp;&nbsp;&nbsp;<i class="md-clear text-danger cancelInstance"></i></td></tr>');
                }

                else {
                    errorAlert('You cannot create an instance to a new product. Save one first.');
                }
            });



            //Removing Instances
            $('body').on('click', '.cancelInstance', function () {
                $(this).closest('tr').remove();
            });

            //Deleting Instances
            $('body').on('click', '.deleteInstance', function () {
                var tr = $(this).closest('tr');
                swal({
                    title: "",
                    text: "Are you sure you want to delete?",
                    showCancelButton: true,
                    showConfirmButton: true,
                    closeOnConfirm: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Delete"
                },
                  function (isConfirm) {
                      if (isConfirm) {

                          $.ajax({
                              url: $('#hdApiUrl').val() + 'api/ItemInstance/Delete/' + tr.children('td').eq(0).text(),
                              method: 'DELETE',
                              dataType: 'JSON',
                              contentType: 'application/json;charset=utf-8',
                              data: JSON.stringify($.cookie('bsl_3')),
                              success: function (response) {
                                  if (response.Success) {
                                      successAlert(response.Message);
                                      tr.remove();
                                  }
                                  else {
                                      errorAlert(response.Message);
                                  }
                              },
                              error: function (xhr) {
                                  alert(xhr.responseText);
                                  console.log(xhr);
                              }
                          });
                      }

                  });
            });


            //Updating Instance
            $('body').on('click', '.updateInstance', function () {
                var updateButton = $(this);
                var row = $(this).closest('tr');
                var instance = {};
                instance.CreatedBy = $.cookie('bsl_3');
                instance.ModifiedBy = $.cookie('bsl_3');
                instance.ID = row.children('td').eq(0).text();
                instance.ItemId = $('#hdnItemID').val();
                instance.Mrp = row.children('td').eq(1).text();
                instance.SellingPrice = row.children('td').eq(3).text();
                instance.CostPrice = row.children('td').eq(2).text();
                instance.Barcode = row.children('td').eq(4).text();
                console.log(instance);

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/ItemInstance/Save',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: 'application/Json;charset=utf-8',
                    data: JSON.stringify(instance),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                            row.children('td').eq(0).text(response.Object);
                            row.find('td:not(:last-child)').removeAttr('contenteditable');
                            updateButton.addClass('md-edit editInstance').removeClass('md-done updateInstance');
                            row.find('.deleteInstance').removeClass('hidden');
                            row.find('.cancelInstance').addClass('hidden');

                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr); }

                });


            });


            //Editing Instance
            $('body').on('click', '.editInstance', function () {
                $(this).closest('tr').find('td:not(:last-child)').attr('contenteditable', true);
                $(this).removeClass('md-edit editInstance').addClass('md-done updateInstance');
                $(this).closest('td').find('.cancelInstance').addClass('hidden');
                $(this).closest('td').find('.deleteInstance').removeClass('hidden');
            });


            //Used to Refresh The Page Contents
            $('.refresh-data').on('click', function () {
                reset();
                $('#hdnItemID').val("0");
                $('#hdnBrandID').val("0");
                $('#hdnCategoryID').val("0");
                $('#hdnTypeID').val("0");
                $('#hdnUnitID').val("0");
                $('#hdnTaxID').val("0");
                $('#hdnVehicleID').val("0");
                $('#hdnGroupID').val("0");
                $('#hdnDespatchID').val("0");
                $('#hdnSupplierID').val("0");
                $('#hdnCustomerID').val("0");
                $('#hdServiceId').val("0");
                $('#hdLeadId').val("0");
                $('#instanceTable tr').remove();
                $('#lblPriceText').addClass('hidden')
                //$('#txtItemCode').attr('disabled', 'disabled');
                $('#btnAddBrand').html('<i class="fa fa-plus"></i>&nbsp;Add Brand');
                $('#btnAddCategory').html('<i class="fa fa-plus"></i>&nbsp;Add Category');
                $('#btnAddNewGroup').html('<i class="fa fa-plus"></i>&nbsp;Add Group');
                $('#btnAddType').html('<i class="fa fa-plus"></i>&nbsp;Add Type');
                $('#btnAddTax').html('<i class="fa fa-plus"></i>&nbsp;Add Tax');
                $('#btnAddUOM').html('<i class="fa fa-plus"></i>&nbsp;Add Unit');
                $('#btnAddItem').html('<i ></i>Save');
                $('#btnAddVehicle').html('<i class="fa fa-plus"></i>&nbsp;Add Vehicle');
                $('#btnAddDespatch').html('<i class="fa fa-plus"></i>&nbsp;Add Despatch');
                $('#btnAddSupplier').html('<i class="fa fa-plus"></i>&nbsp;Add Supplier');
                $('#btnAddCustomer').html('<i class="fa fa-plus"></i>&nbsp;Add Customer');
                $('#btnAddService').html('<i class="fa fa-plus"></i>&nbsp;Add Service');
                $('#btnAddLead').html('<i class="fa fa-plus"></i>&nbsp;Add Lead');
                $('.track-service').attr('checked', false);
            })

            //check if selling price is grater than oru equal to Mrp
            function CheckSellingPrice() {
                var sp = parseFloat($('#txtItemSP').val());
                var TaxPer = parseFloat($('#ddlItemTax option:selected').text());
                var Mrp = parseFloat($('#txtItemMRP').val());
                if ((sp * (TaxPer / 100) + sp) > Mrp) {
                    $('#lblPriceText').removeClass('hidden')
                }
                else {
                    $('#lblPriceText').addClass('hidden')
                }
            }

            //Events to Check selling price is greater than MRP
            $('#txtItemSP,#txtItemCP,#ddlItemTax').change(function () {
                CheckSellingPrice();
            });


            //Function to perform Url operations
            function CheckSection() {
                var ID = $('#hdnID').val();
                var Mode = $('#hdnMode').val();
                var Section = $('#hdnSectionName').val();
                $('#v-5').removeClass('active');
                if (ID != "0" && Mode == 'EDIT') {
                    switch (Section) {
                        case 'ITEM':
                            $('#v-5').addClass('active');
                            GetItems(ID, Mode);
                            break;
                        case 'SERVICE':
                            $('#v-12').addClass('active');
                            $('#Item').removeClass('active');
                            RefreshTableService();
                            GetSErvice(ID, Mode);
                            break;
                        case 'BRAND':
                            $('#menu >li').removeClass('active');
                            $('#v-1').addClass('active');
                            $('#Item').removeClass('active');
                            RefreshTableBrand();
                            getBrands(ID, Mode);
                            break;
                        case 'GROUP':
                            $('#menu >li').removeClass('active');
                            $('#Group').addClass('active');
                            $('#v-4').addClass('active');
                            RefreshTableGroup();
                            getGroups(ID, Mode);
                            break;
                        case 'TYPE':
                            $('#menu >li').removeClass('active');
                            $('#Type').addClass('active');
                            $('#v-8').addClass('active');
                            RefreshTableType();
                            getProductType(ID, Mode);
                            break;
                        case 'CATEGORY':
                            $('#menu >li').removeClass('active');
                            $('#Category').addClass('active');
                            $('#v-2').addClass('active');
                            RefreshTableCategory();
                            GetCategory(ID, Mode);
                            break;
                        case 'UNIT':
                            $('#menu >li').removeClass('active');
                            $('#Unit').addClass('active');
                            $('#v-9').addClass('active');
                            RefreshTableUnit();
                            GetUnit(ID, Mode);
                            break;
                        case 'TAX':
                            $('#menu >li').removeClass('active');
                            $('#v-7').addClass('active');
                            $('#Tax').addClass('active');
                            RefreshTableTax();
                            GetTax(ID, Mode);
                            break;
                        case 'DESPATCH':
                            $('#menu >li').removeClass('active');
                            $('#v-11').addClass('active');
                            $('#Despatch').addClass('active');
                            RefreshTableDespatch();
                            getDespatch(ID, Mode);
                            break;
                        case 'VEHICLE':
                            $('#menu >li').removeClass('active');
                            $('#v-10').addClass('active');
                            $('#Vehicles').addClass('active');
                            RefreshTableVehicle();
                            GetVehicle(ID, Mode);
                            break;
                        case 'SUPPLIER':
                            $('#menu >li').removeClass('active');
                            $('#v-6').addClass('active');
                            $('#Supplier').addClass('active');
                            RefreshTableSuppliers();
                            getSupplierData(ID, Mode);
                            break;
                        case 'CUSTOMER':
                            $('#menu >li').removeClass('active');
                            $('#v-3').addClass('active');
                            $('#Customer').addClass('active');
                            RefreshTableCustomer();
                            getCustomerData(ID, Mode);
                            break;
                        case 'LEAD':
                            $('#menu >li').removeClass('active');
                            $('#v-12').addClass('active');
                            $('#Lead').addClass('active');
                            RefreshTableLead(null,null,null,null);
                            getLeadData(ID, Mode);
                            loadLeadEmployee();
                            break;
                        default:
                            $('#v-5').addClass('active');
                            break;
                    }
                }
                else if (ID != "0" && Mode == "CLONE") {
                    switch (Section) {
                        case 'ITEM':
                            $('#v-5').addClass('active');
                            GetItems(ID, Mode);
                            $('#hdnItemID').val("0");
                            $('#btnAddBrand').html('<i class="fa fa-clone"></i>&nbsp;Clone');
                            break;
                        case 'BRAND':
                            $('#menu >li').removeClass('active');
                            $('#v-1').addClass('active');
                            $('#Item').removeClass('active');
                            RefreshTableBrand();
                            $('#hdnBrandID').val("0");
                            getBrands(ID, Mode);
                            $('#btnAddBrand').html('<i class="fa fa-clone"></i>&nbsp;Clone');
                            break;
                        case 'SERVICE':
                            $('#menu >li').removeClass('active');
                            $('#v-12').addClass('active');
                            $('#Item').removeClass('active');
                            RefreshTableService();
                            $('#hdServiceId').val("0");
                            getService(ID, Mode);
                            $('#btnAddService').html('<i class="fa fa-clone"></i>&nbsp;Clone');
                            break;
                        case 'GROUP':
                            $('#menu >li').removeClass('active');
                            $('#Group').addClass('active');
                            $('#v-4').addClass('active');
                            RefreshTableGroup();
                            getGroups(ID, mode);
                            $('#hdnGroupID').val("0");
                            break;
                        case 'TYPE':
                            $('#menu >li').removeClass('active');
                            $('#Type').addClass('active');
                            $('#v-8').addClass('active');
                            RefreshTableType();
                            getProductType(ID, Mode);
                            $('#hdnTypeID').val("0");
                            break;
                        case 'CATEGORY':
                            $('#menu >li').removeClass('active');
                            $('#Category').addClass('active');
                            $('#v-2').addClass('active');
                            RefreshTableCategory();
                            GetCategory(ID, Mode);
                            break;
                        case 'UNIT':
                            $('#menu >li').removeClass('active');
                            $('#Unit').addClass('active');
                            $('#v-9').addClass('active');
                            RefreshTableUnit();
                            GetUnit(ID, Mode);
                            $('#hdnUnitID').val("0");
                            break;
                        case 'TAX':
                            $('#menu >li').removeClass('active');
                            $('#v-7').addClass('active');
                            $('#Tax').addClass('active');
                            RefreshTableTax();
                            GetTax(ID, Mode);
                            $('#hdnTaxID').val("0");
                            break;
                        case 'DESPATCH':
                            $('#menu >li').removeClass('active');
                            $('#v-11').addClass('active');
                            $('#Despatch').addClass('active');
                            RefreshTableDespatch();
                            getDespatch(ID, Mode);
                            $('#hdnDespatchID').val("0");
                            break;
                        case 'VEHICLE':
                            $('#menu >li').removeClass('active');
                            $('#v-10').addClass('active');
                            $('#Vehicles').addClass('active');
                            RefreshTableVehicle();
                            GetVehicle(ID, Mode);
                            $('#hdnVehicleID').val("0");
                            break;
                        case 'SUPPLIER':
                            $('#menu >li').removeClass('active');
                            $('#v-6').addClass('active');
                            $('#Supplier').addClass('active');
                            RefreshTableSuppliers();
                            getSupplierData(ID, Mode);
                            $('#hdnSupplierID').val("0");
                            break;
                        case 'CUSTOMER':
                            $('#menu >li').removeClass('active');
                            $('#v-3').addClass('active');
                            $('#Customer').addClass('active');
                            RefreshTableCustomer();
                            getCustomerData(ID, Mode);
                            $('#hdnCustomerID').val("0");
                            break;
                        case 'LEAD':
                            $('#menu >li').removeClass('active');
                            $('#v-12').addClass('active');
                            $('#Customer').addClass('active');
                            RefreshTableLead(null,null,null,null);
                            getLeadData(ID, Mode);
                            loadLeadEmployee();
                            $('#hdLeadId').val("0");
                            break;
                        default:
                            $('#v-5').addClass('active');
                            break;
                    }
                }
                else if (ID == "0") {
                    switch (Section) {
                        case 'ITEM':
                            $('#v-5').addClass('active');
                            loadAdditionalData();
                            break;
                        case 'BRAND':
                            $('#menu >li').removeClass('active');
                            $('#v-1').addClass('active');
                            $('#Item').removeClass('active');
                            RefreshTableBrand();
                            break;
                        case 'SERVICE':
                            $('#menu >li').removeClass('active');
                            $('#v-12').addClass('active');
                            $('#Item').removeClass('active');
                            RefreshTableService();
                            break;
                        case 'GROUP':
                            $('#menu >li').removeClass('active');
                            $('#Group').addClass('active');
                            $('#v-4').addClass('active');
                            break;
                        case 'TYPE':
                            $('#menu >li').removeClass('active');
                            $('#Type').addClass('active');
                            $('#v-8').addClass('active');
                            RefreshTableType();
                            break;
                        case 'CATEGORY':
                            $('#menu >li').removeClass('active');
                            $('#Category').addClass('active');
                            $('#v-2').addClass('active');
                            RefreshTableCategory();
                            break;
                        case 'TAX':
                            $('#menu >li').removeClass('active');
                            $('#v-7').addClass('active');
                            $('#Tax').addClass('active');
                            RefreshTableTax();
                            break;
                        case 'VEHICLE':
                            $('#menu >li').removeClass('active');
                            $('#v-10').addClass('active');
                            $('#Vehicles').addClass('active');
                            RefreshTableVehicle();
                            break;
                        case 'SUPPLIER':
                            $('#menu >li').removeClass('active');
                            $('#v-6').addClass('active');
                            $('#Supplier').addClass('active');
                            RefreshTableSuppliers();
                            break;
                        case 'CUSTOMER':
                            $('#menu >li').removeClass('active');
                            $('#v-3').addClass('active');
                            $('#Customer').addClass('active');
                            RefreshTableCustomer();
                            break;
                        case 'LEAD':
                            $('#menu >li').removeClass('active');
                            $('#v-12').addClass('active');
                            $('#Leads').addClass('active');
                            RefreshTableLead(null,null,null,null);
                            loadLeadEmployee();
                            break;
                        case 'DESPATCH':
                            $('#menu >li').removeClass('active');
                            $('#v-11').addClass('active');
                            $('#Despatch').addClass('active');
                            RefreshTableDespatch();
                            break;
                        default:
                            $('#v-5').addClass('active');
                            loadAdditionalData();
                            break;
                    }
                }
                else {
                    $('#v-5').addClass('active');
                    loadAdditionalData();
                }
            }

            //Gets the items
            function GetItems(id, Mode) {
                $.ajax({
                    url: apirul + 'api/Items/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        console.log(response);
                        try {
                            $('#instanceTable tbody').children().remove();
                            $('#instanceTable').removeClass('hidden');
                            //$('#txtItemCode').prop('disabled', false);
                            $('#txtItemCode').val(response.ItemCode);
                            $('#txtItemName').val(response.Name);
                            $('#txtItemDescription').val(response.Description);
                            $('#txtRemarks').val(response.Remarks);
                            $('#txtItemOEMCode').val(response.OEMCode);
                            $('#txtItemHSCode').val(response.HSCode);
                            $('#ddlItemUOM').val(response.UnitID);
                            $('#ddlItemTax').val(response.TaxId);
                            $('#ddlItemType').val(response.TypeID);
                            $('#ddlItemCategory').val(response.CategoryID);
                            $('#ddlItemGroup').val(response.GroupID);
                            $('#ddlItemBrand').val(response.BrandID);
                            $('#txtItemBarcode').val(response.Barcode);
                            $('#txtItemMRP').val(response.MRP);
                            $('#txtItemCP').val(response.CostPrice);
                            $('#txtItemSP').val(response.SellingPrice);
                            $('#lblPriceText').addClass('hidden')
                            $('#ddlItemStatus').val(response.Status);
                            if (Mode == 'CLONE') {
                                $('#hdnItemID').val("0");
                                $('#btnAddItem').html('<i class="fa fa-clone"></i>&nbsp;Clone');
                            }
                            else {
                                $('#hdnItemID').val(response.ItemID);
                                $('#btnAddItem').html('<i class="ion-checkmark-round"></i>&nbsp;Update');
                            }
                            $(response.Instances).each(function () {
                                $('#instanceTable tbody').prepend('<tr><td style="display:none">' + this.ID + '</td><td contenteditable="false">' + this.Mrp + '</td><td contenteditable="false">' + this.CostPrice + '</td><td contenteditable="false">' + this.SellingPrice + '</td><td contenteditable="false">' + this.Barcode + '</td><td><i class="text-info md-edit editInstance"></i>&nbsp;<i class="md-delete text-danger deleteInstance"></i>&nbsp;<i class="md-clear text-danger cancelInstance hidden"></i></td></tr>');
                            });
                        } catch (e) {
                            errorAlert('No Data Found');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Brand for edit
            function getBrands(id, mode) {
                $.ajax({
                    url: apirul + '/api/Brands/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtBrandName').val(response.Name);
                            $('#txtBrandOrder').val(response.Order);
                            $('#ddlBrandStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnBrandID').val("0");
                                $('#btnAddBrand').html('<i class="fa fa-clone"></i>Clone');
                            } else {
                                $('#hdnBrandID').val(response.ID);
                                $('#btnAddBrand').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert('No Data Found');
                        }

                    },
                    error: function (xhr) {
                        errorAlert("No data Found");
                    }
                });
            }

            //Get Service
            function getService(id, mode) {
                $.ajax({
                    url: apirul + '/api/Items/GetDetailsForService/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtServiceName').val(response.Name);
                            $('#txtServiceDescription').val(response.Description);
                            $('#ddlServiceTax').val(response.TaxId);
                            $('#txtServiceRate').val(response.SellingPrice);
                            $('#ddlServiceStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdServiceId').val("0");
                                $('#btnAddService').html('<i class="fa fa-clone"></i>Clone');
                            } else {
                                $('#hdServiceId').val(response.ItemID);
                                $('#btnAddService').html('<i></i>Update');
                            }
                        } catch (e) {
                            errorAlert('No Data Found');
                        }

                    },
                    error: function (xhr) {
                        errorAlert("No data Found");
                    }
                });
            }

            //Groups for edit
            function getGroups(id, mode) {
                $.ajax({
                    url: apirul + '/api/Groups/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtGroupName').val(response.Name);
                            $('#txtGroupOrder').val(response.Order);
                            $('#ddlGroupStatus').val(response.Status);
                            if (true) {
                                $('#hdnGroupID').val("0");
                                $('#btnAddNewGroup').html('<i class="fa fa-clone"></i>Clone');
                            }
                            else {
                                $('#hdnGroupID').val(response.ID);
                                $('#btnAddNewGroup').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert("No data Found");
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Category for edit
            function GetCategory(id, mode) {
                $.ajax({
                    url: apirul + '/api/Categories/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtCategoryName').val(response.Name);
                            $('#txtCategoryOrder').val(response.Order);
                            $('#ddlCategoryStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnCategoryID').val("0");
                                $('#btnAddCategory').html('<i class="fa fa-clone"></i>Clone');
                            }
                            else {
                                $('#hdnCategoryID').val(response.ID);
                                $('#btnAddCategory').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert("No data Found");
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            function GetUnit(id, mode) {
                $.ajax({
                    url: apirul + '/api/Units/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtUOMName').val(response.Name);
                            $('#txtUOMShortName').val(response.ShortName);
                            $('#ddlUOMStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnUnitID').val("0");
                                $('#btnAddUOM').html('<i class="fa fa-clone"></i>Clone');
                            }
                            else {
                                $('#hdnUnitID').val(response.ID);
                                $('#btnAddUOM').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert('No data Found');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            function GetTax(id, mode) {
                $.ajax({
                    url: apirul + '/api/Taxes/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtTaxName').val(response.Name);
                            $('#txtTaxPercentage').val(response.Percentage);
                            $('#txtTaxType').val(response.Type);
                            $('#ddlTaxStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnTaxID').val("0");
                                $('#btnAddTax').html('<i class="fa fa-clone"></i>Clone');
                            }
                            else {
                                $('#hdnTaxID').val(response.ID);
                                $('#btnAddTax').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert('No Data Found');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            function getProductType(id, mode) {
                $.ajax({
                    url: apirul + '/api/ProductTypes/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtTypeName').val(response.Name);
                            $('#txtTypeOrder').val(response.Order);
                            $('#ddlTypeStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnTypeID').val("0");
                                $('#btnAddType').html('<i class="fa fa-clone"></i>Clone');
                            }
                            else {
                                $('#hdnTypeID').val(response.ID);
                                $('#btnAddType').html('<i class="ion-checkmark-round"></i>Update');
                            }

                        } catch (e) {
                            errorAlert('No data found');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            function GetVehicle(id, mode) {
                $.ajax({
                    url: apirul + '/api/Vehicles/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtVehicleName').val(response.Name);
                            $('#txtVehicleNumber').val(response.Number);
                            $('#txtVehicleType').val(response.Type);
                            $('#txtVehicleOwner').val(response.Owner);
                            $('#ddlVehicleStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnVehicleID').val("0");
                                $('#btnAddVehicle').html('<i class="fa fa-clone"></i>Clone');
                            }
                            else {
                                $('#hdnVehicleID').val(response.ID);
                                $('#btnAddVehicle').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert('No Data Found');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            function getDespatch(id, mode) {
                $.ajax({
                    url: apirul + '/api/Despatch/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        try {
                            $('#txtDespatchName').val(response.Name);
                            $('#txtDespatchAddress').val(response.Address);
                            $('#txtDespatchNumber').val(response.PhoneNo);
                            $('#txtDespatchMobile').val(response.MobileNo);
                            $('#txtDespatchContact').val(response.ContactPerson);
                            $('#txtDespatchContactPhone').val(response.ContactPersonPhone);
                            $('#txtDespatchNarration').val(response.Narration);
                            $('#ddlDespatchStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnDespatchID').val("0");
                                $('#btnAddDespatch').html('<i class="fa fa-clone"></i>Update');
                            }
                            else {
                                $('#hdnDespatchID').val(response.ID);
                                $('#btnAddDespatch').html('<i class="ion-checkmark-round"></i>Update');
                            }
                        } catch (e) {
                            errorAlert('No data found');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            function getSupplierData(id, mode) {
                $.ajax({
                    url: apirul + '/api/Suppliers/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        try {
                            var countryId = response.CountryId;
                            $.ajax({
                                url: apirul + '/api/Customers/getStates/' + countryId,
                                method: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'Json',
                                data: JSON.stringify($.cookie("bsl_1")),
                                success: function (data) {
                                    try {
                                        $('#ddlSupplierCountry').val(response.CountryId);
                                        $('#ddlSupplierState').children('option').remove();
                                        $('#ddlSupplierState').append('<option value="0">--select--</option>');
                                        $(data).each(function () {
                                            $('#ddlSupplierState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                        });
                                        $('#txtSupplierName').val(response.Name);
                                        $('#txtSupplierAddres1').val(response.Address1);
                                        $('#txtSupplierAddress2').val(response.Address2);
                                        $('#txtSupplierPhone1').val(response.Phone1);
                                        $('#txtSupplierPhone2').val(response.Phone2);
                                        $('#txtSupplierEmail').val(response.Email);
                                        $('#txtSupplierTax1').val(response.Taxno1);
                                        $('#txtSupplierTax2').val(response.Taxno2);
                                        $('#ddlSupplierStatus').val(response.Status);
                                        $('#ddlSupplierState').val(response.StateId);
                                        $('#ddlSupplierCountry').val(response.CountryId);
                                        $('#imgPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                                        if (mode == 'CLONE') {
                                            $('#hdnSupplierID').val("0");
                                            $('#btnAddSupplier').html('<i class="fa fa-clone"></i>Clone');
                                        }
                                        else {
                                            $('#hdnSupplierID').val(response.ID);
                                            $('#btnAddSupplier').html('<i class="ion-checkmark-round"></i>Update');
                                        }
                                    } catch (e) {
                                        errorAlert('No Data Found');
                                    }
                                },
                                error: function (xhr) {
                                    alert(xhr.responseText);
                                    console.log(xhr);
                                    complete: loading('stop', null);
                                },
                            });
                        } catch (e) {
                            errorAlert('No Data Found');
                        }
                    }
                });
            }
            function getCustomerData(id, mode) {
                $.ajax({
                    url: apirul + '/api/Customers/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        try {
                            var countryId = response.CountryId;
                            $.ajax({
                                url: apirul + '/api/Customers/getStates/' + countryId,
                                method: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'Json',
                                data: JSON.stringify($.cookie("bsl_1")),
                                success: function (data) {
                                    try {
                                        $('#ddlCustomerCountry').val(response.CountryId);
                                        $('#ddlCustomerState').children('option').remove();
                                        $('#ddlCustomerState').append('<option value="0">--select--</option>');
                                        $(data).each(function () {
                                            $('#ddlCustomerState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                        });
                                        $('#ddlCustomerState').val(response.StateId);
                                        $('#txtCustomerName').val(response.Name);
                                        $('#txtCustomerAddress1').val(response.Address1);
                                        $('#txtCustomerAddress2').val(response.Address2);
                                        $('#txtCustomerPhone1').val(response.Phone1);
                                        $('#txtCustomerPhone2').val(response.Phone2);
                                        $('#txtCustomerTax1').val(response.Taxno1);
                                        $('#txtCustomerTax2').val(response.Taxno2);
                                        $('#txtCustomerEmail').val(response.Email);
                                        //$('#txtCustomerTax1').val(response.Taxno1);
                                        //$('#txtCustomertax2').val(response.Taxno2);
                                        $('#txtCustomerCreditAmount').val(response.CreditAmount);
                                        $('#txtCustomerCreditPeriod').val(response.CreditPeriod);
                                        $('#txtCustomerLockAmount').val(response.LockAmount);
                                        $('#txtCustomerLockPeriod').val(response.LockPeriod);
                                        $('#ddlCustomerStatus').val(response.Status);
                                        $('#imgCustomerPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                                        if (mode == 'CLONE') {
                                            $('#hdnCustomerID').val("0");
                                            $('#btnAddCustomer').html('<i class="fa fa-clone"></i>Update');
                                        }
                                        else {
                                            $('#hdnCustomerID').val(response.ID);
                                            $('#btnAddCustomer').html('<i class="ion-checkmark-round"></i>Update');
                                        }
                                    } catch (e) {
                                        errorAlert('No Data Found');
                                    }
                                },
                                error: function (xhr) {
                                    alert(xhr.responseText);
                                    console.log(xhr);
                                    complete: loading('stop', null);
                                },
                            });
                        } catch (e) {
                            errorAlert('No Data Found');
                        }
                    }
                });
            }

            //Lead data for edit
            function getLeadData(id, mode) {
                $.ajax({
                    url: apirul + '/api/Leads/GetLeads/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response)
                        try {
                            var countryId = response.CountryId;
                            $.ajax({
                                url: apirul + '/api/Customers/getStates/' + countryId,
                                method: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'Json',
                                data: JSON.stringify($.cookie("bsl_1")),
                                success: function (data) {
                                    try {
                                        $('#ddlLeadCountry').val(response.CountryId);
                                        $('#ddlLeadState').children('option').remove();
                                        $('#ddlLeadState').append('<option value="0">--select--</option>');
                                        $(data).each(function () {
                                            $('#ddlLeadState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                        });
                                        $('#ddlLeadState').val(response.StateId);
                                        $('#txtLeadName').val(response.Name);
                                        $('#txtLeadAddr1').val(response.Address1);
                                        $('#txtLeadAddr2').val(response.Address2);
                                        $('#txtLeadPh1').val(response.Phone1);
                                        $('#txtLeadPh2').val(response.Phone2);
                                        $('#txtLeadTax1').val(response.Taxno1);
                                        $('#txtLeadTax2').val(response.Taxno2);
                                        $('#txtLeadEmail').val(response.Email);
                                        $('#ddlLeadPrimaryStatus').val(response.Status);
                                        $('#ddlLeadSource').val(response.Source);
                                        $('#ddlAssign').val(response.AssignId);
                                        $('#imgLeadPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                                        if (mode == 'CLONE') {
                                            $('#hdLeadId').val("0");
                                            $('#btnAddLead').html('<i class="fa fa-clone"></i>Update');
                                        }
                                        else {
                                            $('#hdLeadId').val(response.ID);
                                            $('#btnAddLead').html('<i class="ion-checkmark-round"></i>Update');
                                        }
                                    } catch (e) {
                                        errorAlert('No Data Found');
                                    }
                                },
                                error: function (xhr) {
                                    alert(xhr.responseText);
                                    console.log(xhr);
                                    complete: loading('stop', null);
                                },
                            });
                        } catch (e) {
                            errorAlert('No Data Found');
                        }
                    }
                });
            }
        });


    </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
</asp:Content>
