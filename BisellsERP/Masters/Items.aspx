<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="BisellsERP.Masters.Items" %>

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

        .tab-content .form-group {
            margin-bottom: 5px;
        }

        .tab-content .form-control {
            background-color: transparent;
            border: 1px solid #ddd;
        }

        .tab-content .control-label {
            color: #9E9E9E;
            font-size: 13px;
            text-align: left;
        }

        .nav.nav-tabs + .tab-content, .tabs-vertical-env .tab-content {
            padding: 20px;
        }

        td[contenteditable="true"] {
            border: 1px solid #9E9E9E;
            background-color: #e8f3fc;
        }

        .addInstance, .deleteInstance, .updateInstance, .editInstance, .cancelInstance {
            cursor: pointer;
        }
        /*#instanceTable {
            width: 600px;
        }*/

            #instanceTable tbody tr td {
                height: 40px;
            }

            #instanceTable thead tr th, #instanceTable tbody tr td {
                color: #607D8B;
                text-align: center;
            }

            #instanceTable > tbody > tr > td, #instanceTable > tbody > tr > th, #instanceTable > tfoot > tr > td, #instanceTable > tfoot > tr > th {
                padding: 4px;
                padding-top: 10px;
            }

        .nav.nav-tabs > li > a, .nav.tabs-vertical > li > a {
            line-height: 35px;
            border-radius: 5px 5px 0 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <div class="">
        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />

        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-5">
                <h3 class="page-title m-t-0">Items Master</h3>
            </div>
            <div class="col-sm-7">
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Inventory</a></li>
                    <li class="active">Items</li>
                </ol>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <ul class="nav nav-tabs navtab-bg">
                    <li class="active">
                        <a href="#primary-tab" data-toggle="tab" aria-expanded="true">
                            <span class="visible-xs"><i class="fa fa-cog"></i></span>
                            <span class="hidden-xs">Primary Information</span>
                        </a>
                    </li>
                    <li class="pull-right">
                        <div class="btn-toolbar pull-right m-r-10 m-t-0">
                            <button id="btnFind" type="button" accesskey="f" data-toggle="tooltip" data-placement="bottom" title="Search" class="btn btn-default waves-effect btn-sm" data-toggle="modal" data-target="#findProduct"><i class="md-search"></i></button>
                            <button id="btnSave" accesskey="s" type="button" data-toggle="tooltip" data-placement="bottom" title="Save the current item " class="btn btn-default waves-effect btn-sm"><i class="md-add-circle-outline"></i></button>
                            <button id="btnCancel" type="button" data-toggle="tooltip" data-placement="bottom" title="Reset the current item" class="btn btn-default waves-effect waves-light btn-sm"><i class="md-settings-backup-restore"></i></button>
                        </div>
                    </li>
                </ul>
                <div class="tab-content">

                    <%-- Primary Tab Contents --%>
                    <div class="tab-pane active" id="primary-tab">
                        <div class="row">
                            <div class="">
                                <div class="col-sm-8 p-l-0">
                                    <div class="col-md-4 p-l-0">
                                        <div class="form-horizontal">
                                            <label class="control-label col-sm-5" for="txtItemCode">Item Code</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtItemCode" data-validation-qty="1-4" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-horizontal">
                                            <%--<label class="control-label col-sm-5" for="txtItemName">Name:</label>--%>
                                            <div class="col-sm-12">
                                                <asp:TextBox data-validation="required" MaxLength="40" placeholder="Enter Item Name.." data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtItemName" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 m-t-30 p-l-0">
                                        <div class="form-horizontal">
                                            <label class="control-label col-sm-5" for="ddlStatus">Status</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ClientIDMode="Static" ID="ddlStatus" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Active" Value="1" />
                                                    <asp:ListItem Text="In Active" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 m-t-30">
                                        <div class="form-horizontal">
                                            <label class="control-label col-sm-5" for="txtOEM">OEM Code</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtOEM" ClientIDMode="Static" MaxLength="20" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 m-t-30">
                                        <div class="form-horizontal">
                                            <label class="control-label col-sm-5" for="txtHSCode">HS Code</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtHSCode" ClientIDMode="Static" MaxLength="20" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <%--<label class="control-label" for="txtDescription">Description:</label>--%>
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" ClientIDMode="Static" Rows="4" placeholder="Item Description..." runat="server" class="form-control input-sm "></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="">
                                <div class="col-md-12">
                                    <h4 class="title-heading"><i class="ion-social-buffer"></i>&nbsp;Additional Information</h4>
                                </div>
                                <div class="col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlTax">Tax </label>
                                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control input-sm" ID="ddlTax" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlUOM">UOM </label>
                                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control input-sm" ID="ddlUOM" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" for="txtBarcode">Barcode:</label>
                                        <asp:TextBox ID="txtBarcode" ClientIDMode="Static" runat="server" class="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlTax">MRP:</label>
                                        <asp:TextBox ID="txtMrp" ClientIDMode="Static" Text="0.00" runat="server" class="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlUOM">Cost Price:</label>
                                        <asp:TextBox ID="txtCost" Text="0.00" ClientIDMode="Static" runat="server" class="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" for="txtBarcode">Selling Price:</label>
                                        <asp:TextBox ID="txtSell" Text="0.00" ClientIDMode="Static" runat="server" class="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="">
                                <div class="col-md-12">
                                    <h4 class="title-heading"><i class="ion-social-buffer"></i>&nbsp;Categories</h4>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlGroup">Group</label>
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlGroup" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlCategory">Category </label>
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlType">Type</label>
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlType" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" for="ddlBrand">Brand</label>
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlBrand" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="">
                                <div class="col-md-12">
                                    <h4 class="title-heading"><i class="ion-social-buffer"></i>&nbsp;Instances</h4>
                                </div>
                                <div class="col-md-8">
                                    <table id="instanceTable" class="table table-responsive">
                                        <thead>
                                            <tr>
                                                <th style="display: none">InstanceId</th>
                                                <th>MRP</th>
                                                <th>Cost Price</th>
                                                <th>Selling Price</th>
                                                <th>Bar Code</th>
                                                <th>Edit / Delete</th>
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
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--<button type="button" class="btn btn-info btn-lg" >Open Modal</button>--%>

        <!-- Modal -->
        <div id="findProduct" class="modal animated fadeIn" role="dialog">
            <div class="modal-dialog  modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <%-- TABLE HERE --%>
                        <table id="table" class="table table-hover table-striped table-responsive">
                            <thead class="bg-blue-grey">
                                <tr>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Tax</th>
                                    <th>Category</th>
                                    <th>Brand</th>
                                    <th>Code</th>
                                    <th>Type</th>
                                    <th>Group</th>
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

        <%--Added Item Table--%>
    </div>

    <script>
        //All functions inside document ready
        $(document).ready(function () {

            //check if selling price is grater than oru equal to Mrp
            function CheckSellingPrice()
            {
                var sp = parseFloat($('#txtSell').val());
                var TaxPer = parseFloat($('#ddlTax option:selected').text());
                var Mrp = parseFloat($('#txtMrp').val());
                if ((sp * (TaxPer / 100) + sp) > Mrp)
                {
                   errorAlert('Selling Price must not be grater than Mrp');
                   $('#txtSell').val('0.00');
                }
                
            }

            //CheckSellingPrice function call 
            $('#txtSell,#txtMrp,#ddlTax').change(function () {
                CheckSellingPrice();
            });

            $('#txtItemCode').prop('disabled', true);

            //Fetching API url
            var apirul = $('#hdApiUrl').val();

            //Loading table
            RefreshTable();
            
            //Initialises form validation if implemented any
            $.validate();

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
                        $('#instanceTable').children('tbody').children().remove();
                    }
                    else {

                    }

                });

            });
           
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

            //edit functionality
            $(document).on('click', '.edit-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + 'api/Items/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response)
                    {
                        reset();
                        $('#txtItemCode').prop('disabled', false);
                        $('#instanceTable tbody').children().remove();
                        $('#txtItemCode').val(response.ItemCode);
                        $('#txtItemName').val(response.Name);
                        $('#txtDescription').val(response.Description);
                        $('#txtRemarks').val(response.Remarks);
                        $('#txtOEM').val(response.OEMCode);
                        $('#txtHSCode').val(response.HSCode);
                        $('#ddlUOM').val(response.UnitID);
                        $('#ddlTax').val(response.TaxId);
                        $('#ddlType').val(response.TypeID);
                        $('#ddlCategory').val(response.CategoryID);
                        $('#ddlGroup').val(response.GroupID);
                        $('#ddlBrand').val(response.BrandID);
                        $('#ddlBrand').select2('val', response.BrandID);
                        $('#txtBarcode').val(response.Barcode);
                        $('#hdItemId').val(response.ItemID);
                        $('#txtMrp').val(response.MRP);
                        $('#txtCost').val(response.CostPrice);
                        $('#txtSell').val(response.SellingPrice);
                        $('#ddlStatus').val(response.Status);
                        $('#btnSave').html('<i class="ion-checkmark-round"></i>&nbsp;Update');
                        $(response.Instances).each(function () {
                            $('#instanceTable tbody').prepend('<tr><td style="display:none">' + this.ID + '</td><td contenteditable="false">' + this.Mrp + '</td><td contenteditable="false">' + this.CostPrice + '</td><td contenteditable="false">' + this.SellingPrice + '</td><td contenteditable="false">'+this.Barcode+'</td><td><i class="text-info md-edit editInstance"></i>&nbsp;<i class="md-delete text-danger deleteInstance hidden"></i>&nbsp;<i class="md-clear text-danger cancelInstance"></i></td></tr>');
                        });
                        $('#findProduct').modal('hide');
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
                    "url": apirul + '/api/Items/delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
                    "successFunction": RefreshTable
                });

            });

            //independent function to load table with data

            function RefreshTable() {
                $.ajax({
                    url: apirul + '/api/Items/get/',
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
                                { data: 'ItemID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'TaxPercentage' },
                                { data: 'Category' },
                                { data: 'Brand' },
                                { data: 'ItemCode' },
                                { data: 'Type' },
                                { data: 'Group' },
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

            // --Instance Table Scripts Below-- //
            //Adding New Instance
            $('.addInstance').click(function () {
                var editingCount = $('.updateInstance').length;
                if (editingCount  > 0) {
                }
                else if ($('#hdItemId').val() != '0') {
                    $('#instanceTable tbody').prepend('<tr><td style="display:none">0</td><td contenteditable="true">0.00</td><td contenteditable="true">0.00</td><td contenteditable="true">0.00</td><td contenteditable="true">0</td><td><i class="md-done text-info updateInstance"></i>&nbsp;&nbsp;&nbsp;<i class="md-delete text-danger deleteInstance hidden"></i>&nbsp;&nbsp;&nbsp;<i class="md-clear text-danger cancelInstance"></i></td></tr>');
                }

                else {
                    errorAlert('You cannot create an instance to a new product. Save one first.');
                }
            });

            //Removing Instance
            $('body').on('click', '.cancelInstance', function () {
                $(this).closest('tr').remove();
            });

            //Deleting Instance
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
                instance.ItemId = $('#hdItemId').val();
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
            });

        });
    </script>

    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
   <%-- <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>--%>
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>

</asp:Content>
