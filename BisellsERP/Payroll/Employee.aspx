<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="BisellsERP.Payroll.EmployeeNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">

        .form-control[disabled] {
            background-color: #fafafa !important;
        }

        .m-b-5 {
            margin-bottom: 5px !important;
        }

        .asp-txtbox {
            background-color: #ffffff !important;
            border-radius: 5px;
        }

        .pay-amount {
            color: #41bd76;
            font-weight: bold;
            font-size: larger;
        }

        .btn-hov:hover {
            transform: translateY(-3px);
        }

        .input-no-border {
            border: none;
            text-align: center;
            border-bottom: 1px solid #e8e8e8;
            width: 100%;
            font-size: 30px;
            border-radius: 0;
        }

        #imgphoto {
            border-radius: 50%;
            border: 1px solid #e6e6e6;
        }

        .fa-camera {
            font-size: 30px;
            line-height: 53px;
            color: #004566;
        }

        #btnCapture {
            border-radius: 30px;
            width: 55px;
            height: 55px;
            padding: 0;
            background-color: transparent;
        }

        .profile-pic {
            height: 250px;
            width: 250px;
            margin-bottom: 15px;
        }
        @media only screen and (max-width: 768px) {
            .profile-pic {
                height: 125px;
                width: 125px;
                margin-bottom: 15px;
            }
        }
        .profile-detail .control-label, .gym-detail .control-label {
            color: #607D8B;
            font-weight: 100;
        }
        .profile-detail .form-group, .gym-detail .form-group {
            margin-bottom: 5px;
        }
        .profile-detail h4 {
            border-top: 1px dashed #ececec;
            padding-top: 10px;
            margin-top: 0;
        }
        .profile-detail .form-control, .gym-detail .form-control {
            background-color: transparent;
            border: 1px solid #CFD8DC;
        }
        .btn-upload {
            background-color: transparent;
            border: 1px solid #607D8B;
            border-radius: 20px;
            opacity: .8;
            transition: all .3s ease-in-out;
        }
            .btn-upload:hover {
                opacity: 1;
                background-color: #37474F;
                color: #CFD8DC;
            }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">


    <div class="">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp Add New Employee</a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">
        <div class="row p-b-5">

            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group">
                   
                  <%-- <button id="btnSave" accesskey="s" type="button" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>Save</button>--%>
                </div>
            </div>
        </div>

    <div class="row">
        <div class="col-sm-12">
            <div class="panel profile-detail b-r-8">
                <div class="panel-body">
                    <div class="col-sm-3 col-md-4 text-center m-t-30">
                        <asp:Image ID="imgphoto" ClientIDMode="Static" runat="server" ImageUrl="../Theme/images/profile-pic.jpg" CssClass="img-circle profile-pic"></asp:Image>
                        <div class="col-sm-12 col-md-6 col-md-offset-3 m-t-20 m-b-10">
                            <div class="fileUpload btn btn-upload btn-block btn-sm waves-effect waves-light">
                                <span><i class="ion-upload m-r-5"></i>Upload Photo</span>
                                <asp:FileUpload ID="upImage" ClientIDMode="Static" runat="server" CssClass="upload" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-6 col-md-offset-3">
                            <div class="fileUpload btn btn-upload btn-block btn-sm waves-effect waves-light">
                                <span><i class="ion-camera m-r-5"></i>Take Photo</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-9 col-md-8">
                        <div class="row m-b-10">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Title</label>
                                    <select id="ddlTitle" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="Mr">Mr</option>
                                        <option value="Mrs">Mrs</option>
                                        <option value="Miss">Miss</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">First Name</label>
                                    <input type="text" id="txtFirstName" class="form-control" required="required" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Last Name</label>
                                     <input type="text" id="txtLastName" class="form-control"/>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-3">
                                <div class="form-group">
                                    <label class="control-label">DOB</label>
                                     <input type="text" id="txtDOB" class="form-control" maxlength="10" required="required"/>
                                </div>
                            </div>
                               <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label">Gender</label>
                            <select id="ddlGender" class="form-control">
                                <option value="0">--Select--</option>
                                <option value="Male">Male</option>
                                <option value="Female">Female</option>
                            </select>
                        </div>
                    </div>
                            <div class="col-sm-6 col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Blood Group</label>
                                    <input type="text" id="txtBloodGroup" class="form-control" />
                                </div>
                            </div>
                                 <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label">Marital Status</label>
                            <select id="ddlMaritalStatus" class="form-control">
                                <option value="0">--Select--</option>
                                <option value="Married">Married</option>
                                <option value="Single">Single</option>
                            </select>
                        </div>
                    </div>
                        </div>

                        <div class="row">
                            <h4 class="m-l-10"><i class="md-recent-actors"></i>&nbsp;Contact</h4>
                            <div class="col-sm-5 col-md-6">
                                <div class="form-group">
                                    <label class="control-label">Address</label>
                                      <textarea id="txtAddress" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                            <div class="col-sm-7 col-md-6">
                                <div class="row">
                                    <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Mobile</label>
                                          <input type="text" id="txtMobile" class="form-control"  required="required"/>
                                    </div>
                                </div>
                                    <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                         <input type="text" id="txtEmail" class="form-control"  required="required"/>
                                    </div>
                                </div>
                                    <div class="col-sm-3 hidden">
                                    <div class="form-group">
                                        <label class="control-label">Office Number</label>
                                         <input type="text" id="txtOfficeNumber" class="form-control"/>
                                    </div>
                                </div>

                                    <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                         <input type="text" id="txtCity" class="form-control"/>
                                    </div>
                                </div>
                                    <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">ZipCode</label>
                                         <input type="text" id="txtZipCode" class="form-control"/>
                                    </div>
                                </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <h4 class="m-l-10"><i class="md-assignment-late"></i>&nbsp;Additional Information</h4>
                            <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label">Nationality</label>
                            <asp:DropDownList ID="ddlNationality" ClientIDMode="Static" runat="server" AutoPostBack="false" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                                <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label">Department</label>
                            <asp:DropDownList ID="ddlDept" ClientIDMode="Static" runat="server" AutoPostBack="false" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                                <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label">Designation</label>
                            <asp:DropDownList ID="ddlDesig" runat="server" ClientIDMode="Static" AutoPostBack="false" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                                <div class="col-sm-3">
                        <div class="form-group">
                            <label class="control-label">Status</label>
                            <select id="ddlStatus" class="form-control">
                                <option value="1">Active</option>
                                <option value="0">Inactive</option>
                            </select>
                        </div>
                    </div>
                            
                        </div>
                         <div class="row">
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
    </div></div>
                            </div>
                        </div>
                    </div>
                </div>
         <div class="row">
            <div class="col-lg-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row" style="overflow-x: auto">
                            <table id="table" class="table table-hover table-striped table-responsive">
                                 <thead class="bg-blue-grey">
                                    <tr >
                                        <th>ID</th>
                                        <th>Name</th>
                                        <th>DOB</th>
                                        <th>Gender</th>
                                        <th>Marital Status</th>
                                        <th>Nationality </th>
                                        <th>Mobile</th>
                                        <th>Department</th>
                                        <th>Designation</th>
                                        <th>Status</th>
                                        <th style="width:50px">#</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $('#txtDOB').datepicker({
                todayHighlight: true,
                autoclose: true,
                format: 'dd/M/yyyy'
            });
        });
    </script>

    <input type="hidden" id="txtoffice" class="form-control" />
    <input type="hidden" id="txtmemberId" class="form-control"/>
    <input type="hidden" id="txtbillingdate" class="form-control"/>
    <input type="hidden" id="hdnid" value="0"/>
    <script>

        $(document).ready(function () {
            var apiurl = $('#hdApiUrl').val();

            //Loading table
            RefreshTable();

            $('#upImage').change(function () {
                var reader = new FileReader();
                reader.onload = function (e) {
                    // get loaded data and render thumbnail.
                    document.getElementById("imgphoto").src = e.target.result;
                };
                reader.readAsDataURL(this.files[0]);
            });
            var x = '';

            $('#btnSave').off().click(function () {
                save();
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
            function save() {

                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                   
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save",
                    closeOnConfirm: true
                },
                function (isConfirm) {
                    if (isConfirm) {
                        var data = {};
                        data.Title = $('#ddlTitle').val();
                        data.ID = $('#hdnid').val();
                        data.FirstName = $('#txtFirstName').val();
                        data.LastName = $('#txtLastName').val();
                        data.DOB = $('#txtDOB').val();
                        data.Gender = $('#ddlGender').val();
                        data.BloodGroup = $('#txtBloodGroup').val();
                        data.MaritalStatus = $('#ddlMaritalStatus').val();
                        data.Address = $('#txtAddress').val();
                        data.Mobile = $('#txtMobile').val();
                        data.Email = $('#txtEmail').val();
                        data.City = $('#txtCity').val();
                        data.ZipCode = $('#txtZipCode').val();
                        data.DepartmentId = $('#ddlDept').val();
                        data.DesignationId = $('#ddlDesig').val();
                        data.Status = $('#ddlStatus').val();
                        data.NationalityId = $('#ddlNationality').val();
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.PhotoBase64 = $("#imgphoto").prop('src').split(',')[1];
                        console.log(data);
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/Employee/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    $('#add-item-portlet').removeClass('in');
                                    RefreshTable();
                                    ResetRegister();
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


            }//Save Function Ends here

            //edit functionality
            $(document).on('click', '.edit-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                
                $.ajax({
                    url: apiurl + '/api/Employee/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#ddlTitle').val(response.Title);
                        $('#txtFirstName').val(response.FirstName);
                        $('#txtLastName').val(response.LastName);
                        $('#txtAddress').val(response.Address);
                        $('#txtEmail').val(response.Email);
                        $('#txtZipCode').val(response.ZipCode);
                        $('#txtCity').val(response.City);
                        $('#txtDOB').val(response.DobString);
                        $('#ddlGender').val(response.Gender);
                        $('#ddlMaritalStatus').val(response.MaritalStatus);
                        $('#ddlNationality').val(response.NationalityId);
                        $('#txtMobile').val(response.Mobile);
                        $('#txtBloodGroup').val(response.BloodGroup);
                        $('#ddlDept').val(response.DepartmentId);
                        $('#ddlDesig').val(response.DesignationId);
                        $('#ddlStatus').val(response.Status);
                        $('#hdnid').val(response.ID);
                        $('#btnSave').html('<i class="ion-checkmark-round"></i>&nbsp;Update');
                        $('#add-item-portlet').addClass('in')
                        $('body').scrollTop();
                        $("#imgphoto").prop('src', 'data:image/png;base64,'+response.PhotoBase64)
                        console.log(response)
                      
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
                    "url": apiurl + '/api/Employee/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Employee has been deleted from list',
                    "successFunction": RefreshTable
                });

            });
            function ResetRegister()
            {
               
                $('#hdnid').val('');
                $('#txtFirstName').val('');
                $('#txtAddress').val('');
                $('#txtBloodGroup').val('');
                $('#txtCity').val('');
                $('#txtDOB').val('');
                $('#txtEmail').val('');
                $('#txtAddress').val('');
                $('#txtLastName').val('');
                $('#txtMobile').val('');
                $('#txtZipCode').val('');
                $('#ddlNationality').val('0');
                $('#ddlTitle').val('0');
                $('#ddlDept').val('0');
                $('#ddlDesig').val('0');
                $('#ddlGender').val('0');
                $('#ddlMaritalStatus').val('0');
              
            }


            //independent function to load table with data
            function RefreshTable() {
                $.ajax({
                    url: apiurl + '/api/Employee/get/',
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
                                { data: 'FirstName' },
                                { data: 'DobString' },
                                { data: 'Gender' },
                                { data: 'MaritalStatus' },
                                { data: 'Nationality' },
                                { data: 'Mobile' },
                                { data: 'DepartmentName' },
                                { data: 'Designation' },
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
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
     <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
