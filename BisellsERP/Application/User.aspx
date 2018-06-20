<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="BisellsERP.Application.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        #ddlDays {
            position: absolute;
            z-index: 10;
            bottom: -3em;
        }

        .setting-bg-clr {
            background-color: whitesmoke;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">

        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdUserId" ClientIDMode="Static" runat="server" Value="0" />

        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Users</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Account</a></li>
                    <li class="active">Users</li>
                </ol>
            </div>
        </div>

        <%--new master form--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>Add New User </a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">
                            <div class="row">

                                <%-- Contents here --%>
                                <div class="col-md-8">

                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label for="ddlLoc">Location</label>
                                                <asp:DropDownList ClientIDMode="Static" CssClass="form-control input-sm" ID="ddlLoc" runat="server"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtUName">Email:</label>
                                                <asp:TextBox data-validation="email" data-validation-error-msg="<small style='color:red'>Invalid Email Format</small>" ID="txtUName" ClientIDMode="Static" runat="server" class="form-control input-sm" placeholder="Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtFullName">Full Name:</label>
                                                <asp:TextBox data-validation="required" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtFullName" ClientIDMode="Static" runat="server" class="form-control input-sm" placeholder="FullName"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlEmpName">Employee Name</label>
                                                <asp:DropDownList ClientIDMode="Static" CssClass="searchDropdown" ID="ddlEmpName" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtPassword">Password:</label>
                                                <asp:TextBox data-validation="required" TextMode="Password" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtPassword" ClientIDMode="Static"
                                                    runat="server" class="form-control input-sm" placeholder="Password"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtConfirmPass">Confirm Password : </label>
                                                <asp:TextBox data-validator="reqiuired" TextMode="Password" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtConfirmPass" ClientIDMode="Static" runat="server" class="form-control input-sm" placeholder="ChangePassword"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                    <label class="control-label"><i class="fa fa-gear"></i>Settings</label>
                                <div class="col-md-4 setting-bg-clr">

                                    <div class="row">
                                        <div class="col-md-12 m-b-10">
                                            <div class="form-group">
                                            
                                                <div class="m-t-15">
                                                    <div class="checkbox checkbox-inline checkbox-primary">
                                                        <asp:CheckBox ID="chkDisable" ClientIDMode="Static" Text="Disable user" runat="server" />
                                                    </div>
                                                    <div class="checkbox checkbox-inline checkbox-primary">
                                                        <asp:CheckBox ID="chkExpiry" ClientIDMode="Static" runat="server" Text="Password change at next login" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">

                                            <%--<asp:RadioButtonList ID="rdChange" runat="server" ClientIDMode="Static">
                                                <asp:ListItem ></asp:ListItem>
                                                <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>--%>


                                            <%-- CHANGE NAMES FOR RADIO BUTTONS --%>

                                            <div class="col-md-5">
                                                <div class="row">
                                                    <div class="radio radio-primary">
                                                        <asp:RadioButton ID="RadioButton1" ClientIDMode="Static" GroupName="pwdExpiry" runat="server" Value="1" Text="Password Expires" />
                                                    </div>
                                                    <asp:DropDownList ID="ddlDays" ClientIDMode="Static" CssClass="form-control input-sm hide m-t-20" runat="server" Style="">
                                                        <asp:ListItem Value="0">-Select Days-</asp:ListItem>
                                                        <asp:ListItem Value="30">After 30 Days</asp:ListItem>
                                                        <asp:ListItem Value="40">After 40 Days</asp:ListItem>
                                                        <asp:ListItem Value="60">After 60 Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-7">
                                                <div class="row">
                                                    <div class="radio radio-primary">
                                                        <asp:RadioButton ID="RadioButton2" ClientIDMode="Static" Checked="true" GroupName="pwdExpiry" runat="server" Value="0" Text="Password Never Expires" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="btn-toolbar pull-right m-t-10">
                                            <button id="btnSave" type="button" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>Add</button>
                                            <button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>Close</button>
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

    <%--list table--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel  b-r-8">
                <div class="panel-body">
                    <%-- TABLE HERE --%>
                    <table id="table" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey">
                            <tr>
                                <th>ID</th>
                                <th>User Name</th>
                                <th>Full Name</th>
                                <th>Employee Name</th>
                                <th>#</th>
                            </tr>
                        </thead>
                    </table>
                    <%-- TABLE END --%>
                </div>
            </div>
        </div>
    </div>

        <script>
            //All funcrtions inside document ready
            $(document).ready(function () {
                //Fetching API url
                var apirul = $('#hdApiUrl').val();
                //validation of password and confirm password
                $("#btnSaveConfirmed").click(function () {;
                    var password = $("#txtPassword").val()
                    var confirmPassword = $("#txtConfirmPass").val();
                    if (password != confirmPassword) {
                        errorAlert('Passwords do not match');
                        return false;
                    }
                    return true;
                });

                //Days Dropdown Select Toggle
                $('#RadioButton1').change(function () {
                    if ($(this).is(':checked')) {
                        $('#ddlDays').removeClass('hide');
                    }
                });
                $('#RadioButton2').change(function () {
                    if ($(this).is(':checked')) {
                        $('#ddlDays').addClass('hide');
                    }
                });

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
                        
                        showConfirmButton: true, closeOnConfirm: true,
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
                       
                        showConfirmButton: true, closeOnConfirm: true,
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
                        url: apirul + '/api/Users/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            $('#txtUName').val(response.UserName);
                            $('#txtFullName').val(response.FullName);
                            $('#ddlLoc').val(response.LocationId);
                            $('#ddlEmpName').val(response.EmployeeId);
                            $('#ddlEmpName').select2('val', response.EmployeeId);
                            $('#ddlDays').val(response.ExpiryPeriod);
                            $('#chkExpiry').val(response.ForcePasswordChange);
                            $('#hdUserId').val(response.ID);
                            $('#add-item-portlet').addClass('in');
                            $('#btnSave').html('<i class="ion-checkmark-round"></i>Update');
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

                    deleteMaster({
                        "url": apirul + '/api/Users/Delete/',
                        "id": id,
                        "successMessage": 'User has been deleted ',
                        "successFunction": RefreshTable
                    });

                });

                //independent function to load table with data
                function RefreshTable() {
                    $.ajax({
                        url: apirul + '/api/Users/get',
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
                                    { data: 'UserName' },
                                    { data: 'FullName' },
                                    { data: 'EmployeeId' },
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
  <%--  <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>--%>
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
