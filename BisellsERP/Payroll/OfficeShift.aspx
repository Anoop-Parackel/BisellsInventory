<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OfficeShift.aspx.cs" Inherits="BisellsERP.Payroll.OfficeShift" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">
        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Office Shift</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Payroll</a></li>
                    <li class="active">Office Shift</li>
                </ol>
            </div>
        </div>
        <%--new master form--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Shift </a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">
                            <div class="row">
                                <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                    <label class="form-label semibold">Shift Name:</label>
                                    <div class="form-control-wrapper form-control-icon-left" style="padding-left: 10px; padding-right: 10px">
                                        <asp:TextBox data-validation="required" MaxLength="35" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtName" ClientIDMode="Static" runat="server" class="form-control " placeholder="Name"></asp:TextBox>
                                        <i class="font-icon font-icon-burger"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">Monday:</label>
                                            <asp:TextBox ID="txtMonClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtMonClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">TuesDay:</label>
                                            <asp:TextBox ID="txtTueClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtTueClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">WednesDay:</label>
                                            <asp:TextBox ID="txtWedClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtWedClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">ThursDay:</label>
                                            <asp:TextBox ID="txtThuClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtThuClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">FriDay:</label>
                                            <asp:TextBox ID="txtFriClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtFriClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">Saturday:</label>
                                            <asp:TextBox ID="txtSatClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtSatClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <label class="form-label semibold">Sunday:</label>
                                            <asp:TextBox ID="txtSunClockIn" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox ID="txtSunClockOut" runat="server" ClientIDMode="Static" Text="00:00:00" CssClass="date-info"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-9">
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
                                    <th>Day</th>
                                    <th>Monday</th>
                                    <th>Tuesday</th>
                                    <th>Wednesday</th>
                                    <th>Thursday</th>
                                    <th>Friday</th>
                                    <th>Saturday</th>
                                    <th>Sunday</th>
                                    <th>#</th>
                                </tr>
                            </thead>
                        </table>
                        <%-- TABLE END --%>
                    </div>
                </div>
            </div>
        </div>
 <script type="text/javascript">
     //document ready function starts here

     $(document).ready(function ()
     {
       /* Time setting */
         $('#txtMonClockIn,#txtMonClockOut,#txtTueClockIn,#txtTueClockOut,#txtWedClockIn,#txtWedClockOut,#txtThuClockIn,#txtThuClockOut,#txtFriClockIn,#txtFriClockOut,#txtSatClockIn,#txtSatClockOut,#txtSunClockIn,#txtSunClockOut').clockpicker
        ({
           autoclose: true,
           format: '00:00:00',
           todayHighlight: true
       });
       //Fetching API url
                var apirul = $('#hdApiUrl').val();
       //Loading table
                RefreshTable();
                //Initialises form validation if implemented any
                $.validate();
        //new entry
                $('#btnNew').click(function ()
                {
                    reset();
                });
                //cancel entry
                $('#btnCancel').click(function ()
                {
                    swal
                        ({
                        title: "Cancel?",
                        text: "Are you sure you want to cancel?",
                     
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        closeOnConfirm: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Cancel this Entry"
                    },
                    function (isConfirm)
                    {
                        if (isConfirm)
                        {
                            reset();
                            $('#add-item-portlet').removeClass('in');
                        }
                        else
                        {

                        }

                    });

                });
         //cancel entry function ends here

                //save functionality. 
                //This is not a asynchronous ajax call. 
                //Handled directly by code behind
                $('#btnSave').click(function ()
                {
                    swal({
                        title: "Save?",
                        text: "Are you sure you want to save?",
                      
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Save"
                    },
                        function (isConfirm)
                        {
                            if (isConfirm)
                            {
                                $('#btnSaveConfirmed').trigger('click');
                            }

                        });
                });

                //edit functionality starts here
                $(document).on('click', '.edit-entry', function ()
                {
                   var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                   $.ajax
                       ({
                        url: apirul + '/api/OfficeShift/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response) {
                            reset();
                            $('#txtName').val(response.Name);
                            $('#txtMonClockIn').val(response.MondayShift);
                            $('#txtMonClockOut').val(response.MondayOutShift);
                            $('#txtTueClockIn').val(response.TuesdayShift);
                            $('#txtTueClockOut').val(response.TuesdayOutShift);
                            $('#txtWedClockIn').val(response.WednesdayShift);
                            $('#txtWedClockOut').val(response.WednesdayOutShift);
                            $('#txtThuClockIn').val(response.ThursdayShift);
                            $('#txtThuClockOut').val(response.ThursdayOutShift);
                            $('#txtFriClockIn').val(response.FridayShift);
                            $('#txtFriClockOut').val(response.FridayOutShift);
                            $('#txtSatClockIn').val(response.SaturdayShift);
                            $('#txtSatClockOut').val(response.SaturdayOutShift);
                            $('#txtSunClockIn').val(response.SundayShift);
                            $('#txtSunClockOut').val(response.SundayOutShift);
                            $('#hdId').val(response.ID);
                            $('#add-item-portlet').addClass('in');
                            $('#btnSave').html('<i class="ion-checkmark-round"></i>Update');
                        },
                        error: function (xhr)
                        {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                });
         //edit functionality ends here
                //delete functionality
                $(document).on('click', '.delete-entry', function ()
                {
                    var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                    var modifiedBy = $.cookie("bsl_3");
                    deleteMaster
                    ({
                        "url": apirul + '/api/OfficeShift/Delete/',
                        "id": id,
                        "modifiedBy": modifiedBy,
                        "successMessage": 'Office Shift has been deleted from Payroll',
                        "successFunction": RefreshTable
                    });

                });
         //delete function ends here
                //Open entry section
                $('#btnAdd').click(function ()
                {
                    $('#masterEntry').slideDown('slow');
                });

                //independent function to load table with data
                function RefreshTable()
                {
                  $.ajax
                        ({
                        url: apirul + '/api/OfficeShift/get/',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response)
                        {
                           
                            $('#table').dataTable
                                ({
                                responsive: true,
                                dom: 'Blfrtip',
                                lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                                buttons: ['copy', 'excel', 'csv', 'print'],
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
                                        data: function ()
                                        {
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
         //Refresh table function ends here
     });
     //documnet ready function ends here
</script>
</div>
<script src="../Plugins/ClockPicker/jquery-clockpicker.min.js"></script>
<link href="../Plugins/ClockPicker/jquery-clockpicker.min.css" rel="stylesheet" />
<link href="../Plugins/ClockPicker/bootstrap-clockpicker.min.css" rel="stylesheet" />
<script src="../Plugins/ClockPicker/bootstrap-clockpicker.min.js"></script>
<link href="../Plugins/ClockPicker/bootstrap-clockpicker.css" rel="stylesheet" />
<link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
<script src="../Theme/assets/datatables/datatables.min.js"></script>
<link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
<script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
<script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
<script src="../Theme/Custom/Commons.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>
</asp:Content>

