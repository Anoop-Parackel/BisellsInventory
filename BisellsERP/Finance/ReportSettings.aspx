﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportSettings.aspx.cs" Inherits="BisellsERP.Finance.ReportSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Report Settings</title>
    <style>
        a > small {
            background-color: #d03232;
            color: white;
            border-radius: 2px;
            padding-left: 3px;
            padding-right: 3px;
        }

        thead tr th {
            color: white;
        }

        .portlet .portlet-heading .portlet-widgets {
            line-height: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="container-fluid">

        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Report Settings</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Finance</a></li>
                    <li class="active">Report Settings</li>
                </ol>
            </div>
        </div>

        <%--new master form--%>

        <%-- Add New Item Portlet --%>
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Report Setting </a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">

                            <%-- CREATION FORM HERE --%>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Report Type:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlReportType" runat="server">
                                                <asp:ListItem Value="0">Trading A/C</asp:ListItem>
                                                <asp:ListItem Value="1">Profit & Loss A/C</asp:ListItem>
                                                <asp:ListItem Value="2">Balance Sheet</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Account Group </label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlAccountGroup" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Side</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlSide" runat="server">
                                                <asp:ListItem Value="0">Left</asp:ListItem>
                                                <asp:ListItem Value="1">Right</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Order:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:textbox runat="server" ID="txtAccountGroupOrder" cssClass="form-control" MaxLength="20" ClientIDMode="Static" placeholder="Order"></asp:textbox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="btn-toolbar pull-right m-t-30">
                                        <button id="btnSave" accesskey="s" type="button" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>&nbsp;Save</button>
                                        <button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>&nbsp;Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--Added Item Table--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel">
                    <div class="panel-body">
                        <table id="table" class="table table-hover table-striped table-responsive">
                            <thead class="bg-blue-grey">
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
    <script>
        //All functions inside document ready
        $(document).ready(function () {
            //Fetching API url
            var apirul = $('#hdApiUrl').val();

            //Loading table
            RefreshTable();
            //Initialises form validation if implemented any
            $.validate();

            //new entry
            $('#btnNew').click(function () {
                reset();
            });
            //cancel entry
            $('#btnCancel').click(function () {
                swal({
                    title: "Cancel?",
                    text: "Are you sure you want to cancel?",
                    
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Cancel this Entry"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        reset();
                        $('#add-item-portlet').removeClass('in');
                    }
                    else {

                    }

                });

            });

            //save functionality. 
            //This is not a asynchronous ajax call. 
            //Handled directly by code behind
            $('#btnSave').click(function () {
                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                    
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            $('#btnSaveConfirmed').trigger('click');
                        }

                    });
            });

            //edit functionality
            $(document).on('click', '.edit-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/ReportSettings/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtAccountGroupOrder').val(response.Order);
                        $('#ddlReportType').val(response.ReportID);
                        $('#ddlSide').val(response.Postion);
                        $('#ddlAccountGroup').val(response.AccountGroupID);
                        $('#hdItemId').val(response.ID);
                        $('#btnSave').html('Update');
                        $('#add-item-portlet').addClass('in');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apirul + '/api/ReportSettings/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully',
                    "successFunction": RefreshTable
                });

            });

            //Open entry section
            $('#btnAdd').click(function () {
                $('#masterEntry').slideDown('slow');
            });

            //independent function to load table with data
            function RefreshTable() {
                $.ajax({
                    url: apirul + '/api/ReportSettings/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#table').dataTable({
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
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                });
            }
        });
    </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
  
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
