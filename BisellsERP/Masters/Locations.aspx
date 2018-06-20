<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Locations.aspx.cs" Inherits="BisellsERP.Masters.Locations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Location</title>
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
<asp:Content ID="Content3" ContentPlaceHolderID="childContent" runat="server">
    <div class="">

        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdLocationId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Locations</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Inventory</a></li>
                    <li class="active">Location</li>
                </ol>
            </div>
        </div>
        <%--new master form--%>
        <div class="row">

            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Location </a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Name:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox data-validation="required" MaxLength="30" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtName" ClientIDMode="Static" runat="server" class="form-control " placeholder="Name"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Address 1:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtAddress1" ClientIDMode="Static" runat="server" class="form-control " placeholder="Address1"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Address 2:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtAddress2" ClientIDMode="Static" runat="server" class="form-control " placeholder="Address2"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Contact:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtContact" onkeypress="return this.value.length<=15" ClientIDMode="Static" textmode="Number" runat="server" class="form-control " placeholder="Contact"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Contact Person:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtContactPerson" ClientIDMode="Static" MaxLength="40" runat="server" class="form-control " placeholder="Contact Person"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Registration Id 1:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtRegId1" ClientIDMode="Static" runat="server" MaxLength="25" class="form-control " placeholder="RegistrationId1"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Registration Id 2:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtRegId2" ClientIDMode="Static" runat="server" MaxLength="25" class="form-control " placeholder="RegistrationId2"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Status </label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlStatus" runat="server">

                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="btn-toolbar pull-right m-t-30">
                                        <button id="btnSave" accesskey="s" type="button" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>Add</button>
                                        <button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>Close</button>
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
                <div class="panel">
                    <div class="panel-body">
                        <%-- TABLE HERE --%>
                        <table id="table" class="table table-hover table-striped table-responsive">
                            <thead class="bg-blue-grey">
                                <tr>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Address1</th>
                                    <th>Address2</th>
                                    <th>Contact</th>
                                    <th>Contact Person</th>
                                    <th>Registration Id1</th>
                                    <th>Registration Id2</th>
                                    <th>Status</th>
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
                        url: apirul + '/api/Locations/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            $('#txtName').val(response.Name);
                            $('#txtAddress1').val(response.Address1);
                            $('#txtAddress2').val(response.Address2);
                            $('#txtContact').val(response.Contact);
                            $('#txtContactPerson').val(response.ContactPerson);
                            $('#txtRegId1').val(response.RegId1);
                            $('#txtRegId2').val(response.RegId2);
                            $('#ddlStatus').val(response.Status);
                            $('#hdLocationId').val(response.ID);
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
                    var modifiedBy = $.cookie("bsl_3");
                    deleteMaster({
                        "url": apirul + '/api/Locations/Delete/',
                        "id": id,
                        "modifiedBy": modifiedBy,
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
                        url: apirul + '/api/Locations/get/',
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
                                    { data: 'Name' },
                                    { data: 'Address1' },
                                    { data: 'Address2' },
                                    { data: 'Contact' },
                                    { data: 'ContactPerson' },
                                    { data: 'RegId1' },
                                    { data: 'RegId2' },
                                    { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
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
  <%--      <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>--%>
        <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
    </div>
</asp:Content>
