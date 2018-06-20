<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayrollEntities.aspx.cs" Inherits="BisellsERP.Payroll.Payrolls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Payroll</title>
    <style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="masters-wrap">
        <div class="col-sm-2 p-0">
            <h3 class="m-t-0">Payroll</h3>
            <div class="tabs-vertical-env">
                <ul class="nav" id="menu">
                    <li class="active" id="Departments">
                        <a href="#v-1" data-toggle="tab" aria-expanded="true">Departments</a>
                    </li>
                    <li class="" id="Designation">
                        <a href="#v-2" data-toggle="tab" aria-expanded="false">Designations</a>
                    </li>
                    <li class="" id="Salary">
                        <a href="#v-3" data-toggle="tab" aria-expanded="false">Salary Components</a>
                    </li>
                    <li class="" id="Hourly">
                        <a href="#v-4" data-toggle="tab" aria-expanded="false">Hourly Template</a>
                    </li>
                    <li class="" id="Template">
                        <a href="#v-5" data-toggle="tab" aria-expanded="true">Payroll Template</a>
                    </li>
                    <li class="" id="OfficeShift">
                        <a href="#v-6" data-toggle="tab" aria-expanded="true">Office Shift</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="col-sm-10 p-0">
            <div class="col-md-12 m-t-40">
                <div class="tab-content">
                    <!-- Department -->
                    <div class="tab-pane active" id="v-1">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Department</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Department</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtDepartmentName" placeholder="Department Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlDepartmentStatus">
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
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddDepartment"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Department List</h5>
                                        <table id="tableDepartments" class="table table-hover">
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
                    <!-- Designation -->
                    <div class="tab-pane" id="v-2">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Designation</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Designation</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtDesignationName" placeholder="Designation Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Departments</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlDepartments">
                                                                <option value="0">--Select--</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlDesignationStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" id="btnAddDesignation" type="button"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Designations List</h5>
                                        <table id="tableDesignation" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Designation</th>
                                                    <th>Department</th>
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
                    <!-- Salary Components -->
                    <div class="tab-pane" id="v-3">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Salary Component</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtComponentName" placeholder="Component Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Type</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlComponentType">
                                                                <option value="0">--Select--</option>
                                                                <option value="Addition">Addition</option>
                                                                <option value="Deduction">Deduction</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Order</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlCalculationType">
                                                                <option value="0">--Select--</option>
                                                                <option value="Percentage">Percentage</option>
                                                                <option value="Amount">Amount</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlComponentStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" id="btnAddComponent" type="button"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Components List</h5>
                                        <table id="tableComponent" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>ComponentType</th>
                                                    <th>CalculationType</th>
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
                    <!-- Hourly Template-->
                    <div class="tab-pane" id="v-4">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Hourly Template</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Title</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtHourlyTemplateTitle" placeholder="Title" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Rate</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtHourlyRate" placeholder="Rate" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlHourlyStatus">
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
                                                    <button class="btn btn-green waves-effect waves-light btn-xs" id="btnAddHourly" type="button"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Templates List</h5>
                                        <table id="tableHourly" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Title</th>
                                                    <th>Hourly Rate</th>
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
                    <!-- Payroll Template -->
                    <div class="tab-pane" id="v-5">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Payroll Template</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name Of Template</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTemplateName" placeholder="Template" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Basic Salary</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtBasicSalary" placeholder="Basic" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">OverTime Rate</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtOverTime" placeholder="OT Rate" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">House Rent Allowance</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtHRAllowance" placeholder="HR Allowance" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Medical Allowance</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtMedical" placeholder="Medical ALlowance" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Provident Fund</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtPF" placeholder="PF" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Tax Deduction</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTaxDeduction" placeholder="Tax Deduction" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Travelling Allowance</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTA" placeholder="Travelling Allowance" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Dearness Allowance</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtDA" placeholder="DA" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Security Deposit</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtSecurityDeposit" placeholder="Security Deposit" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Incentives</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtIncentives" placeholder="Incentives" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Gross Salary</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtGross" placeholder="Gross Salary" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Total Allowance</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTotalAllowance" placeholder="Total Allowance" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Total Deduction</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtTotalDeduction" placeholder="Total Deductions" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Net Salary</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtNetSalary" placeholder="Net Salary" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Status</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlTemplateStatus">
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
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddTemplate"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Templates List</h5>
                                        <table id="tableTemplates" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Payroll Template</th>
                                                    <th>Basic Salary</th>
                                                    <th>Net Salary</th>
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
                    <!-- Office Shift -->
                    <div class="tab-pane" id="v-6">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New OfficeShift</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtShiftName" class="form-control input-sm" placeholder="Name" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Monday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtMonIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtMonOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Tuesday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtTueIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtTueOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Wednesday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtWedIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtWedOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Thursday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtThuIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtThuOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Friday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtFriIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtFriOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Saturday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtSatIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtSatOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Sunday</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtSunIn" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtSunOut" class="form-control input-sm date-info" value="00:00" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddNewShift"><i class=""></i>&nbsp;<span>Save</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Schedule List</h5>
                                        <table id="tableShift" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Day</th>
                                                    <th>Mon</th>
                                                    <th>Tue</th>
                                                    <th>Wed</th>
                                                    <th>Thu</th>
                                                    <th>Fri</th>
                                                    <th>Sat</th>
                                                    <th>Sun</th>
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
            <input type="hidden" id="hdnDesigID" value="0" />
            <input type="hidden" id="hdnComponentID" value="0" />
            <input type="hidden" id="hdnDeptID" value="0" />
            <input type="hidden" id="hdnTemplateID" value="0" />
            <input type="hidden" id="hdnShiftID" value="0" />
            <input type="hidden" id="hdnHourlyID" value="0" />
            <asp:HiddenField ID="hdnID" runat="server" ClientIDMode="Static" Value="0" />
            <asp:HiddenField ID="hdnSectionName" runat="server" ClientIDMode="Static" Value="0" />
            <asp:HiddenField ID="hdnMode" runat="server" ClientIDMode="Static" Value="0" />
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(".tab-content > .tab-pane > .panel").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });

            //Fetching API url
            var apiurl = $('#hdApiUrl').val();


            //Inital load of department table
            RefreshTableDepartments();

            loadDepartments();

            UrlEdit();

            //Selects The tab for selected li.Used to Load the table
            $("#menu >li").click(function () {
                var select = $(this).attr('id');
                switch (select) {
                    case 'Departments':
                        RefreshTableDepartments();
                        break;
                    case 'Designation':
                        RefreshTableDesignation();
                        loadDepartments();
                        break;
                    case 'Salary':
                        RefreshTableComponets();
                        break;
                    case 'Hourly':
                        RefreshTableHourly();
                        break;
                    case 'Template':
                        console.log('Template');
                        RefreshTableTemplate();
                        break;
                    case 'OfficeShift':
                        RefreshTableShift();
                        break;
                    default:
                        console.log('default');
                        break;
                }
            });

            //////////******Functions To Load Tables********///////////

            //Departments Table
            function RefreshTableDepartments() {
                $.ajax({
                    url: apiurl + '/api/Departments/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableDepartments').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-dept"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-dept"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    }
                });
            }

            //Designations Table

            function RefreshTableDesignation() {
                $.ajax({
                    url: apiurl + '/api/Designations/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableDesignation').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Department' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-desig"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-desig"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    }
                });
            }

            //Salary Components
            function RefreshTableComponets() {
                $.ajax({
                    url: apiurl + '/api/SalaryComponents/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableComponent').dataTable
                            ({
                                responsive: true,
                                dom: 'Blfrtip',
                                lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                                buttons: ['excel', 'csv', 'print'],
                                data: response,
                                destroy: true,
                                columns: [
                                    { data: 'ID', className: 'hidden-td' },
                                    { data: 'Name' },
                                    { data: 'ComponentType' },
                                    { data: 'CalculationType' },
                                    { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
                                    {
                                        data: function () {
                                            return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-sal"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-sal"><i class="fa fa-times" style="color:red"></i></a>'
                                        },
                                        sorting: false
                                    }
                                ]
                            });
                        $('[data-toggle="tooltip"]').tooltip();

                    }
                });
            }

            //Hourly Templates
            function RefreshTableHourly() {
                $.ajax
                    ({
                        url: apiurl + '/api/HourlyTemplate/get/',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            $('#tableHourly').dataTable
                                ({
                                    responsive: true,
                                    dom: 'Blfrtip',
                                    lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                                    buttons: ['excel', 'csv', 'print'],
                                    data: response,
                                    destroy: true,
                                    columns: [
                                        { data: 'ID', className: 'hidden-td' },
                                        { data: 'Title' },
                                        { data: 'Rate' },
                                        { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                        {
                                            data: function () {
                                                return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Hourly"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-Hourly"><i class="fa fa-times" style="color:red"></i></a>'
                                            },
                                            sorting: false
                                        }
                                    ]
                                });
                            $('[data-toggle="tooltip"]').tooltip();

                        }
                    });
            }

            //Payroll Templates
            function RefreshTableTemplate() {
                $.ajax
                    ({
                        url: apiurl + '/api/PayRollTemplate/get/',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            $('#tableTemplates').dataTable
                                ({
                                    responsive: true,
                                    dom: 'Blfrtip',
                                    lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                                    buttons: ['excel', 'csv', 'print'],
                                    data: response,
                                    destroy: true,
                                    columns: [
                                        { data: 'ID', className: 'hidden-td' },
                                        { data: 'Grade' },
                                        { data: 'BasicSalary' },
                                        { data: 'NetSalary' },
                                        { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                        {
                                            data: function () {
                                                return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-Temp"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-Temp"><i class="fa fa-times" style="color:red"></i></a>'
                                            },
                                            sorting: false
                                        }
                                    ]
                                });
                            $('[data-toggle="tooltip"]').tooltip();

                        }
                    });
            }

            function RefreshTableShift() {
                $.ajax
                      ({
                          url: apiurl + '/api/OfficeShift/get/',
                          method: 'POST',
                          contentType: 'application/json; charset=utf-8',
                          dataType: 'Json',
                          data: JSON.stringify($.cookie("bsl_1")),
                          success: function (response) {
                              $('#tableShift').dataTable
                                  ({
                                      responsive: true,
                                      dom: 'Blfrtip',
                                      lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                                      buttons: ['excel', 'csv', 'print'],
                                      data: response,
                                      destroy: true,
                                      columns: [
                                          { data: 'ID', className: 'hidden-td' },
                                          { data: 'Name' },
                                          { data: 'MondayShift' },
                                          { data: 'TuesdayShift' },
                                          { data: 'WednesdayShift' },
                                          { data: 'ThursdayShift' },
                                          { data: 'FridayShift' },
                                          { data: 'SaturdayShift' },
                                          { data: 'SundayShift' },
                                          {
                                              data: function () {
                                                  return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-shift"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-Shift"><i class="fa fa-times" style="color:red"></i></a>'
                                              },
                                              sorting: false
                                          }
                                      ]
                                  });
                              $('[data-toggle="tooltip"]').tooltip();
                          }
                      });
            }

            //////////******Save or Update********///////////

            //Departments
            $('#btnAddDepartment').click(function () {
                var dept = {};
                dept.Name = $('#txtDepartmentName').val();
                dept.Status = $('#ddlDepartmentStatus').val();
                dept.CompanyId = $.cookie("bsl_1");
                dept.CreatedBy = $.cookie('bsl_3');
                dept.ModifiedBy = $.cookie('bsl_3');
                dept.ID = $('#hdnDeptID').val();
                console.log(dept);
                $.ajax({
                    url: apiurl + 'api/Departments/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(dept),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            if (dept.ID == 0) {
                                successAlert(data.Message);
                            }
                            else {
                                successAlert(data.Message);
                            }
                            RefreshTableDepartments();
                            reset();
                            $('#btnAddDepartment').html('<i class=""></i>&nbsp;Save');
                            $('#hdnDeptID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });
            //Designations

            $('#btnAddDesignation').click(function () {
                var desig = {};
                desig.Name = $('#txtDesignationName').val();
                desig.DepartmentId = $('#ddlDepartments').val();
                desig.Status = $('#ddlDesignationStatus').val();
                desig.CompanyId = $.cookie("bsl_1");
                desig.CreatedBy = $.cookie('bsl_3');
                desig.ModifiedBy = $.cookie('bsl_3');
                desig.ID = $('#hdnDesigID').val();
                $.ajax({
                    url: apiurl + 'api/Designations/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(desig),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            if (desig.ID == 0) {
                                successAlert(data.Message);
                            }
                            else {
                                successAlert(data.Message);
                            }
                            RefreshTableDesignation();
                            reset();
                            $('#btnAddDesignation').html('<i class=""></i>&nbsp;Save');
                            $('#hdnDesigID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });

            //Salary Components
            $('#btnAddComponent').click(function () {
                var Component = {};
                Component.Name = $('#txtComponentName').val();
                Component.ComponentType = $('#ddlComponentType').val();
                Component.CalculationType = $('#ddlCalculationType').val();
                Component.Status = $('#ddlComponentStatus').val();
                Component.CompanyId = $.cookie("bsl_1");
                Component.CreatedBy = $.cookie('bsl_3');
                Component.ModifiedBy = $.cookie('bsl_3');
                Component.ID = $('#hdnComponentID').val();
                $.ajax({
                    url: apiurl + 'api/SalaryComponents/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Component),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            if (Component.ID == 0) {
                                successAlert(data.Message);
                            }
                            else {
                                successAlert(data.Message);
                            }
                            RefreshTableComponets();
                            reset();
                            $('#btnAddComponent').html('<i class=""></i>&nbsp;Save');
                            $('#hdnComponentID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });

            //Hourly Template
            $('#btnAddHourly').click(function () {
                var Hourly = {};
                Hourly.Title = $('#txtHourlyTemplateTitle').val();
                Hourly.Rate = $('#txtHourlyRate').val();
                Hourly.Status = $('#ddlHourlyStatus').val();
                Hourly.ID = $('#hdnHourlyID').val();
                Hourly.CompanyId = $.cookie("bsl_1");
                Hourly.CreatedBy = $.cookie('bsl_3');
                Hourly.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: apiurl + 'api/HourlyTemplate/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Hourly),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            if (Hourly.ID == 0) {
                                successAlert(data.Message);
                            }
                            else {
                                successAlert(data.Message);
                            }
                            RefreshTableHourly();
                            reset();
                            $('#btnAddHourly').html('<i class=""></i>&nbsp;Save');
                            $('#hdnHourlyID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });

            //Payroll Templates
            $('#btnAddTemplate').click(function () {
                var template = {};
                template.Grade = $('#txtTemplateName').val();
                template.BasicSalary = $('#txtBasicSalary').val();
                template.OvertimeRate = $('#txtOverTime').val();
                template.HouseRentAllowance = $('#txtHRAllowance').val();
                template.MedicalAllowance = $('#txtMedical').val();
                template.TravellingAllowance = $('#txtTA').val();
                template.DearnessAllowance = $('#txtDA').val();
                template.SecurityDeposit = $('#txtSecurityDeposit').val();
                template.ProvidentFund = $('#txtPF').val();
                template.TaxDeduction = $('#txtTaxDeduction').val();
                template.GrossSalary = $('#txtGross').val();
                template.TotalAllowance = $('#txtTotalAllowance').val();
                template.TotalDeduction = $('#txtTotalDeduction').val();
                template.NetSalary = $('#txtNetSalary').val();
                template.Incentives = $('#txtIncentives').val();
                template.Status = $('#ddlTemplateStatus').val();
                template.CompanyId = $.cookie("bsl_1");
                template.CreatedBy = $.cookie('bsl_3');
                template.ModifiedBy = $.cookie('bsl_3');
                template.ID = $('#hdnTemplateID').val();
                $.ajax({
                    url: apiurl + 'api/PayRollTemplate/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(template),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            if (template.ID == 0) {
                                successAlert(data.Message);
                            }
                            else {
                                successAlert(data.Message);
                            }
                            RefreshTableTemplate();
                            reset();
                            $('#btnAddTemplate').html('<i class=""></i>&nbsp;Save');
                            $('#hdnTemplateID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });

            //Office Shift Save
            $('#btnAddNewShift').click(function () {
                var Shift = {};
                Shift.Name = $('#txtShiftName').val();
                Shift.MondayInTime = $('#txtMonIn').val();
                Shift.MondayOutTime = $('#txtMonOut').val();
                Shift.TuesdayInTime = $('#txtTueIn').val();
                Shift.TuesdayOutTime = $('#txtTueOut').val();
                Shift.WednesdayInTime = $('#txtWedIn').val();
                Shift.WednesdayOutTime = $('#txtWedOut').val();
                Shift.ThursdayInTime = $('#txtThuIn').val();
                Shift.ThursdayOutTime = $('#txtThuOut').val();
                Shift.FridayInTime = $('#txtFriIn').val();
                Shift.FridayOutTime = $('#txtFriOut').val();
                Shift.SaturdayInTime = $('#txtSatIn').val();
                Shift.SaturdayOuttime = $('#txtSatOut').val();
                Shift.SundayInTime = $('#txtSunIn').val();
                Shift.SundayOutTime = $('#txtSunOut').val();
                Shift.CompanyId = $.cookie("bsl_1");
                Shift.CreatedBy = $.cookie('bsl_3');
                Shift.ModifiedBy = $.cookie('bsl_3');
                Shift.ID = $('#hdnShiftID').val();
                console.log(Shift);
                $.ajax({
                    url: apiurl + 'api/OfficeShift/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Shift),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            if (Shift.ID == "0") {
                                successAlert(data.Message);
                            }
                            else {
                                successAlert(data.Message);
                            }
                            RefreshTableShift();
                            reset();
                            $('#btnAddNewShift').html('<i class=""></i>&nbsp;Save');
                            $('#hdnShiftID').val("0");
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });

            //////////******Edit The Entries********///////////
            //Department
            $(document).on('click', '.edit-entry-dept', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                console.log(id);
                $.ajax({
                    url: apiurl + '/api/Departments/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        console.log(response);
                        $('#txtDepartmentName').val(response.Name);
                        $('#ddlDepartmentStatus').val(response.Status);
                        $('#hdnDeptID').val(response.ID);
                        $('#btnAddDepartment').html('<i class=""></i>&nbsp;Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Designation
            $(document).on('click', '.edit-entry-desig', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apiurl + '/api/Designations/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {

                        reset();
                        $('#txtDesignationName').val(response.Name);
                        $('#ddlDesignationStatus').val(response.Status);
                        $('#ddlDepartments').val(response.DepartmentId);
                        $('#hdnDesigID').val(response.ID);
                        $('#btnAddDesignation').html('<i class=""></i>&nbsp;Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Salary Components
            $(document).on('click', '.edit-entry-sal', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax
                    ({
                        url: apiurl + '/api/SalaryComponents/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            $('#txtComponentName').val(response.Name);
                            $('#ddlComponentType').val(response.ComponentType);
                            $('#ddlCalculationType').val(response.CalculationType);
                            $('#ddlComponentStatus').val(response.Status);
                            $('#hdnComponentID').val(response.ID);
                            $('#add-item-portlet').addClass('in');
                            $('#btnAddComponent').html('<i class=""></i>&nbsp;Update');
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
            });

            //Hourly Template
            $(document).on('click', '.edit-entry-Hourly', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax
                    ({
                        url: apiurl + '/api/HourlyTemplate/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            console.log(response);
                            reset();
                            $('#txtHourlyTemplateTitle').val(response.Title);
                            $('#txtHourlyRate').val(response.Rate);
                            $('#ddlHourlyStatus').val(response.Status);
                            $('#hdnHourlyID').val(response.ID);
                            $('#btnAddHourly').html('<i class=""></i>&nbsp;Update');
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
            });

            //Payroll Template
            $(document).on('click', '.edit-entry-Temp', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax
                    ({
                        url: apiurl + '/api/PayRollTemplate/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            $('#txtTemplateName').val(response.Grade);
                            $('#txtBasicSalary').val(response.BasicSalary);
                            $('#txtOverTime').val(response.OvertimeRate);
                            $('#txtHRAllowance').val(response.HouseRentAllowance);
                            $('#txtMedical').val(response.MedicalAllowance);
                            $('#txtPF').val(response.ProvidentFund);
                            $('#txtTaxDeduction').val(response.TaxDeduction);
                            $('#txtTA').val(response.TravellingAllowance);
                            $('#txtDA').val(response.DearnessAllowance);
                            $('#txtSecurityDeposit').val(response.SecurityDeposit);
                            $('#txtIncentives').val(response.Incentives);
                            $('#txtGross').val(response.GrossSalary);
                            $('#txtTotalAllowance').val(response.TotalAllowance);
                            $('#txtTotalDeduction').val(response.TotalDeduction);
                            $('#txtNetSalary').val(response.NetSalary);
                            $('#ddlTemplateStatus').val(response.Status);
                            $('#hdnTemplateID').val(response.ID);
                            $('#btnAddTemplate').html('<i class=""></i>&nbsp;Update');
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
            });

            //Office Shift Edit
            $(document).on('click', '.edit-entry-shift', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax
                    ({
                        url: apiurl + '/api/OfficeShift/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            console.log(response);
                            reset();
                            $('#txtShiftName').val(response.Name);
                            $('#txtMonIn').val(response.MondayShift);
                            $('#txtMonOut').val(response.MondayOutShift);
                            $('#txtTueIn').val(response.TuesdayShift);
                            $('#txtTueOut').val(response.TuesdayOutShift);
                            $('#txtWedIn').val(response.WednesdayShift);
                            $('#txtWedOut').val(response.WednesdayOutShift);
                            $('#txtThuIn').val(response.ThursdayShift);
                            $('#txtThuOut').val(response.ThursdayOutShift);
                            $('#txtFriIn').val(response.FridayShift);
                            $('#txtFriOut').val(response.FridayOutShift);
                            $('#txtSatIn').val(response.SaturdayShift);
                            $('#txtSatOut').val(response.SaturdayOutShift);
                            $('#txtSunIn').val(response.SundayShift);
                            $('#txtSunOut').val(response.SundayOutShift);
                            $('#hdnShiftID').val(response.ID);
                            $('#btnAddNewShift').html('<i class=""></i>&nbsp;Update');
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
            });


            //////////******Delete selected Data********///////////

            //Delete department
            $(document).on('click', '.delete-entry-dept', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apiurl + '/api/Departments/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Company has been deleted from inventory',
                    "successFunction": RefreshTableDepartments
                });
                reset();
            });

            //Delete designation
            $(document).on('click', '.delete-entry-desig', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apiurl + '/api/Designations/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Designation has been deleted from inventory',
                    "successFunction": RefreshTableDesignation,
                });
                reset();
            });

            //Delete Salary Components
            $(document).on('click', '.delete-entry-sal', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster
                ({
                    "url": apiurl + '/api/SalaryComponents/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Salary Component has been deleted from inventory',
                    "successFunction": RefreshTableComponets
                });
                reset();
            });

            //Delete Hourly Template
            $(document).on('click', '.delete-entry-Hourly', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster
                 ({
                     "url": apiurl + '/api/HourlyTemplate/Delete/',
                     "id": id,
                     "modifiedBy": modifiedBy,
                     "successMessage": 'Hourly Template has been deleted from Payroll',
                     "successFunction": RefreshTableHourly
                 });
                reset();
            });

            //Delete Payroll Template
            $(document).on('click', '.delete-entry-Temp', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apiurl + '/api/PayRollTemplate/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'PayRoll Template has been deleted from Payroll',
                    "successFunction": RefreshTableTemplate
                });
                reset();
            });

            //Delete Office Shifts
            $(document).on('click', '.delete-entry-Shift', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster
                ({
                    "url": apiurl + '/api/OfficeShift/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Office Shift has been deleted from Payroll',
                    "successFunction": RefreshTableShift
                });
                reset();

            });


            //////////******Additional Functions********///////////

            //Loads the Departments
            function loadDepartments() {
                var company = $.cookie("bsl_1");
                $.ajax({
                    type: "POST",
                    url: apiurl + "api/departments/Get",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($.cookie("bsl_1")),
                    dataType: "json",
                    success: function (data) {
                        $("#ddlDepartments").empty();
                        $("#ddlDepartments").append('<option value=0>--Select--</option>');
                        $.each(data, function () {
                            $("#ddlDepartments").append($("<option/>").val(this.ID).text(this.Name));
                        });
                    },
                    failure: function () {
                        console.log("Error")
                    }
                });
            }


            //Clock picker for office shift module
            $('#txtMonIn,#txtMonOut,#txtTueIn,#txtTueOut,#txtWedIn,#txtWedOut,#txtThuIn,#txtThuOut,#txtFriIn,#txtFriOut,#txtSatIn,#txtSatOut,#txtSunIn,#txtSunOut').clockpicker
            ({
                autoclose: true,
                format: '00:00:00',
                todayHighlight: true
            });


            //Clears the data in all fields and hidden values
            $('.refresh-data').on('click', function () {
                reset();
                $('#hdnDesigID').val("0");
                $('#hdnComponentID').val("0");
                $('#hdnDeptID').val("0");
                $('#hdnTemplateID').val("0");
                $('#hdnShiftID').val("0");
                $('#hdnHourlyID').val("0");
                $('#btnAddDepartment').html('<i class="fa fa-plus"></i>&nbsp;Add Department');
                $('#btnAddDesignation').html('<i class="fa fa-plus"></i>&nbsp;Add Designation');
                $('#btnAddComponent').html('<i class="fa fa-plus"></i>&nbsp;Add Component');
                $('#btnAddHourly').html('<i class="fa fa-plus"></i>&nbsp;Add Template');
                $('#btnAddTemplate').html('<i class="fa fa-plus"></i>&nbsp;Add Template');
                $('#btnAddNewShift').html('<i class="fa fa-plus"></i>&nbsp;Add New Shift');
            });

            function UrlEdit() {
                var ID = $('#hdnID').val();
                var mode = $('#hdnMode').val();
                var Section = $('#hdnSectionName').val();
                if (ID != "0" && mode == 'EDIT') {
                    $('#v-1').removeClass('active');
                    switch (Section) {
                        case 'DEPT':
                            $('#menu >li').removeClass('active');
                            $('#Departments').addClass('active');
                            $('#v-1').addClass('active');
                            loadDeptData(ID, mode);
                            break;
                        case 'DESIG':
                            $('#menu >li').removeClass('active');
                            $('#Designation').addClass('active');
                            $('#v-2').addClass('active');
                            loadDesigData(ID, mode);
                            RefreshTableDesignation();
                            break;
                        case 'SALARY':
                            $('#menu >li').removeClass('active');
                            $('#Salary').addClass('active');
                            $('#v-3').addClass('active');
                            loadSalaryComponet(ID, mode);
                            RefreshTableComponets();
                            break;
                        case 'HOURLY':
                            $('#menu >li').removeClass('active');
                            $('#Hourly').addClass('active');
                            $('#v-4').addClass('active');
                            loadHourly(ID, mode);
                            RefreshTableHourly();
                            break;
                        case 'PAYROLL':
                            $('#menu >li').removeClass('active');
                            $('#Template').addClass('active');
                            $('#v-5').addClass('active');
                            loadPayroll(ID, mode);
                            RefreshTableTemplate();
                            break;
                        case 'OFFICE':
                            $('#menu >li').removeClass('active');
                            $('#OfficeShift').addClass('active');
                            $('#v-6').addClass('active');
                            loadOffice(ID, mode);
                            RefreshTableShift();
                            break;
                        default:
                            break;
                    }
                }
                else if (ID != "0" && mode == 'CLONE') {
                    $('#v-1').removeClass('active');
                    switch (Section) {
                        case 'DEPT':
                            $('#menu >li').removeClass('active');
                            $('#Departments').addClass('active');
                            $('#v-1').addClass('active');
                            loadDeptData(ID, mode);
                            RefreshTableDepartments();
                            break;
                        case 'DESIG':
                            $('#menu >li').removeClass('active');
                            $('#Designation').addClass('active');
                            $('#v-2').addClass('active');
                            loadDesigData(ID, mode);
                            RefreshTableDesignation();
                            break;
                        case 'SALARY':
                            $('#menu >li').removeClass('active');
                            $('#Salary').addClass('active');
                            $('#v-3').addClass('active');
                            loadSalaryComponet(ID, mode);
                            RefreshTableComponets();
                            break;
                        case 'HOURLY':
                            $('#menu >li').removeClass('active');
                            $('#Hourly').addClass('active');
                            $('#v-4').addClass('active');
                            loadHourly(ID, mode);
                            RefreshTableHourly();
                            break;
                        case 'PAYROLL':
                            $('#menu >li').removeClass('active');
                            $('#Template').addClass('active');
                            $('#v-5').addClass('active');
                            loadPayroll(ID, mode);
                            RefreshTableTemplate();
                            break;
                        case 'OFFICE':
                            $('#menu >li').removeClass('active');
                            $('#OfficeShift').addClass('active');
                            $('#v-6').addClass('active');
                            loadOffice(ID, mode);
                            RefreshTableShift();
                            break;
                        default:
                            break;
                    }
                }
                else if (ID == "0") {
                    switch (Section) {
                        case 'DEPT':
                            $('#menu >li').removeClass('active');
                            $('#Departments').addClass('active');
                            $('#v-1').addClass('active');
                            RefreshTableDepartments();
                            break;
                        case 'DESIG':
                            $('#v-1').removeClass('active');
                            $('#menu >li').removeClass('active');
                            $('#Designation').addClass('active');
                            $('#v-2').addClass('active');
                            RefreshTableDesignation();
                            break;
                        case 'SALARY':
                            $('#menu >li').removeClass('active');
                            $('#v-1').removeClass('active');
                            $('#Salary').addClass('active');
                            $('#v-3').addClass('active');
                            RefreshTableComponets();
                            break;
                        case 'HOURLY':
                            $('#menu >li').removeClass('active');
                            $('#v-1').removeClass('active');
                            $('#Hourly').addClass('active');
                            $('#v-4').addClass('active');
                            RefreshTableHourly();
                            break;
                        case 'PAYROLL':
                            $('#menu >li').removeClass('active');
                            $('#v-1').removeClass('active');
                            $('#Template').addClass('active');
                            $('#v-5').addClass('active');
                            RefreshTableTemplate();
                            break;
                        case 'OFFICE':
                            $('#menu >li').removeClass('active');
                            $('#v-1').removeClass('active');
                            $('#OfficeShift').addClass('active');
                            $('#v-6').addClass('active');
                            RefreshTableShift();
                            break;
                        default:
                            $('#v-1').addClass('active');
                            break;
                    }
                }
                else {
                    $('#v-1').addClass('active');
                }
            }
            function loadDeptData(id, mode) {
                $.ajax({
                    url: apiurl + '/api/Departments/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        console.log(response);
                        try {
                            $('#txtDepartmentName').val(response.Name);
                            $('#ddlDepartmentStatus').val(response.Status);
                            if (mode == 'CLONE') {
                                $('#hdnDeptID').val("0");
                                $('#btnAddDepartment').html('<i class="ion-checkmark-round"></i>Clone');
                            }
                            else {
                                $('#hdnDeptID').val(response.ID);
                                $('#btnAddDepartment').html('<i class="ion-checkmark-round"></i>Update');
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
            function loadDesigData(id, mode) {
                $.ajax({
                    url: apiurl + '/api/Designations/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {

                        reset();
                        try {
                            $('#txtDesignationName').val(response.Name);
                            $('#ddlDesignationStatus').val(response.Status);
                            $('#ddlDepartments').val(response.DepartmentId);
                            if (mode == 'CLONE') {
                                $('#hdnDesigID').val("0");
                                $('#btnAddDesignation').html('<i class="ion-checkmark-round"></i>Clone');
                            }
                            else {
                                $('#hdnDesigID').val(response.ID);
                                $('#btnAddDesignation').html('<i class="ion-checkmark-round"></i>Update');
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
            function loadSalaryComponet(id, mode) {
                $.ajax
                    ({
                        url: apiurl + '/api/SalaryComponents/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            try {
                                $('#txtComponentName').val(response.Name);
                                $('#ddlComponentType').val(response.ComponentType);
                                $('#ddlCalculationType').val(response.CalculationType);
                                $('#ddlComponentStatus').val(response.Status);
                                if (mode == 'CLONE') {
                                    $('#hdnComponentID').val("0");
                                    $('#btnAddComponent').html('<i class="ion-checkmark-round"></i>Clone');
                                }
                                else {
                                    $('#hdnComponentID').val(response.ID);
                                    $('#btnAddComponent').html('<i class="ion-checkmark-round"></i>Update');
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

            function loadHourly(id, mode) {
                $.ajax
                    ({
                        url: apiurl + '/api/HourlyTemplate/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            try {
                                $('#txtHourlyTemplateTitle').val(response.Title);
                                $('#txtHourlyRate').val(response.Rate);
                                $('#ddlHourlyStatus').val(response.Status);
                                if (mode == 'CLONE') {
                                    $('#hdnHourlyID').val("0");
                                    $('#btnAddHourly').html('<i class="ion-checkmark-round"></i>Clone');
                                }
                                else {
                                    $('#hdnHourlyID').val(response.ID);
                                    $('#btnAddHourly').html('<i class="ion-checkmark-round"></i>Update');
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
            function loadPayroll(id,mode) {
                $.ajax
                    ({
                        url: apiurl + '/api/PayRollTemplate/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            try {
                                $('#txtTemplateName').val(response.Grade);
                                $('#txtBasicSalary').val(response.BasicSalary);
                                $('#txtOverTime').val(response.OvertimeRate);
                                $('#txtHRAllowance').val(response.HouseRentAllowance);
                                $('#txtMedical').val(response.MedicalAllowance);
                                $('#txtPF').val(response.ProvidentFund);
                                $('#txtTaxDeduction').val(response.TaxDeduction);
                                $('#txtTA').val(response.TravellingAllowance);
                                $('#txtDA').val(response.DearnessAllowance);
                                $('#txtSecurityDeposit').val(response.SecurityDeposit);
                                $('#txtIncentives').val(response.Incentives);
                                $('#txtGross').val(response.GrossSalary);
                                $('#txtTotalAllowance').val(response.TotalAllowance);
                                $('#txtTotalDeduction').val(response.TotalDeduction);
                                $('#txtNetSalary').val(response.NetSalary);
                                $('#ddlTemplateStatus').val(response.Status);
                                if (mode == 'CLONE') {
                                    $('#hdnTemplateID').val("0");
                                    $('#btnAddTemplate').html('<i class="ion-checkmark-round"></i>Clone');
                                }
                                else {
                                    $('#hdnTemplateID').val(response.ID);
                                    $('#btnAddTemplate').html('<i class="ion-checkmark-round"></i>Update');
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
            function loadOffice(id,mode) {
                $.ajax
                    ({
                        url: apiurl + '/api/OfficeShift/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            console.log(response);
                            reset();
                            try {
                                $('#txtShiftName').val(response.Name);
                                $('#txtMonIn').val(response.MondayShift);
                                $('#txtMonOut').val(response.MondayOutShift);
                                $('#txtTueIn').val(response.TuesdayShift);
                                $('#txtTueOut').val(response.TuesdayOutShift);
                                $('#txtWedIn').val(response.WednesdayShift);
                                $('#txtWedOut').val(response.WednesdayOutShift);
                                $('#txtThuIn').val(response.ThursdayShift);
                                $('#txtThuOut').val(response.ThursdayOutShift);
                                $('#txtFriIn').val(response.FridayShift);
                                $('#txtFriOut').val(response.FridayOutShift);
                                $('#txtSatIn').val(response.SaturdayShift);
                                $('#txtSatOut').val(response.SaturdayOutShift);
                                $('#txtSunIn').val(response.SundayShift);
                                $('#txtSunOut').val(response.SundayOutShift);
                                if (mode == 'CLONE') {
                                    $('#hdnShiftID').val("0");
                                    $('#btnAddNewShift').html('<i class="ion-checkmark-round"></i>Clone');
                                }
                                else {
                                    $('#hdnShiftID').val(response.ID);
                                    $('#btnAddNewShift').html('<i class="ion-checkmark-round"></i>Update');
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

        });
    </script>

    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <!-- Clock picker plugins for Office shift -->
    <script src="../Plugins/ClockPicker/jquery-clockpicker.min.js"></script>
    <link href="../Plugins/ClockPicker/jquery-clockpicker.min.css" rel="stylesheet" />
    <link href="../Plugins/ClockPicker/bootstrap-clockpicker.min.css" rel="stylesheet" />
    <script src="../Plugins/ClockPicker/bootstrap-clockpicker.min.js"></script>
    <link href="../Plugins/ClockPicker/bootstrap-clockpicker.css" rel="stylesheet" />
</asp:Content>
