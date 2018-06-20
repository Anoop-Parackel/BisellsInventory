<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockAdjustment.aspx.cs" Inherits="BisellsERP.Settings.Inventory.StockAdjustment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        tbody tr td {
            padding: 5px !important;
            font-size: larger;
        }

        .form-inline .checkbox input[type=checkbox], .form-inline .radio input[type=radio] {
            top: -8px;
            left: -3px;
            height: 15px;
            width: 17px;
        }
        .stat-h {
            min-height: 50vh;
            max-height: 75vh;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%-- ---- Page Title ---- --%>
    <div class="row p-b-5">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Stock Adjustment</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
                <button id="btnSave" accesskey="s" type="button" class="btn btn-success waves-effect waves-light"><i class="ion-checkmark-round btn-save"></i>&nbsp;Save</button>
            </div>
        </div>
    </div>

    <%-- ---- List Table ---- --%>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default stat-h b-r-8">
                <div class="panel-body p-t-10">
                    <div class="col-sm-12">
                        <table id="listTable" class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="hidden">Id</th>
                                    <th>
                                        <div class="checkbox checkbox-primary">
                                            <input type="checkbox" class="chk-all" /><label></label>
                                        </div>
                                    </th>
                                    <th>Product</th>
                                    <th>Mrp</th>
                                    <th>Selling Price</th>
                                    <th>System Stock</th>
                                    <th>Current Stock</th>
                                    <th>Adjusted Quantity</th>
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
    <script>
        //documnet ready function starts here
        $(document).ready(function () {

            LoadDetails();
            //save function call
            $('#btnSave').off().click(function () {
                save();
            });
            function LoadDetails() {
                //populating items in the table
                var locationId = $.cookie('bsl_2');
                $.ajax
                     ({
                         url: $('#hdApiUrl').val() + '/api/StockAdjustment/GetStock/?LocationId=' + locationId,
                         method: 'POST',
                         dataType: 'JSON',
                         contentType: 'application/json;charset=utf-8',
                         success: function (data) {
                             var response = data;
                             var html = '';
                             $('#listTable').DataTable().destroy();
                             $('#listTable tbody').children().remove();
                             $(response).each(function (index) {
                                 html += '<tr>';
                                 html += '<td class="hidden">' + this.ItemID + '</td>';
                                 html += '<td class="no-sort"><div class="checkbox"><input type="checkbox" name="edit' + index + '"  class="checkbox checkbox-success view chk-edit" /><label></label></div></td>';
                                 html += '<td style="width:350px">' + this.Name + '</td>';
                                 html += '<td>' + this.MRP + '</td>';
                                 html += '<td>' + this.SellingPrice + '</td>';
                                 html += '<td>' + this.Stock + '</td>';
                                 html += '<td><input type="number" disabled="true" class="edit-value ins-val" value="' + this.Stock + '"/></td>';
                                 html += '<td>0</td>';
                                 html += '</tr>';
                             });
                             $('#listTable tbody').append(html);
                             $('#listTable').dataTable({
                                 "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
                             });
                             html = '';
                         },
                         error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                     });
                //populate table function ends here
            }

            //mark all check boxes
            $('#listTable').on('change', '.chk-all', function () {
                var rows = $('#listTable tbody').children('tr');
                if ($(this).is(':checked')) {
                    $('#listTable').DataTable().rows().nodes().each(function () {

                        $(this).find('.chk-edit').prop('checked', true);
                        $(this).find('.ins-val').prop('disabled', false);
                    });
                }
                else {
                    $('#listTable').DataTable().rows().nodes().each(function () {

                        $(this).find('.chk-edit').prop('checked', false);
                        $(this).find('.ins-val').prop('disabled', true);
                    });
                }
            });

            //enable textbox
            $(document).on('change', '.chk-edit', function () {
                $(this).closest('tr').find('.ins-val').prop('disabled', function (i, v) { return !v; });
                $(this).closest('tr').children('td').find('.ins-val').focus().select();
            });

            //save function starts here
            function save() {
                loading('start');
                $('#listTable').DataTable().destroy()
                var tbody = $('#listTable > tbody');
                var tr = tbody.children('tr');
                var arr = [];
                for (var i = 0; i < tr.length; i++) {
                    if ($(tr[i]).children('td:nth-child(2)').find('.chk-edit').prop('checked')) {
                        var id = $(tr[i]).children('td:nth-child(1)').text();
                        var item = $(tr[i]).children('td:nth-child(3)').text();
                        var locationId = $.cookie('bsl_2');
                        var createdBy = $.cookie('bsl_3');
                        var mrp = $(tr[i]).children('td:nth-child(4)').text();
                        var sp = $(tr[i]).children('td:nth-child(5)').text();
                        var sysStock = $(tr[i]).children('td:nth-child(6)').text();
                        var currStock = $(tr[i]).children('td:nth-child(7)').children('input').val();
                        var adjStock = $(tr[i]).children('td:nth-child(8)').text();
                        var detail = {};
                        detail.ID = id;
                        detail.Name = item;
                        detail.MRP = mrp;
                        detail.SellingPrice = sp;
                        detail.SystemStock = sysStock;
                        detail.CurrentStock = currStock;
                        detail.LocationId = locationId;
                        detail.CreatedBy = createdBy;
                        arr.push(detail);
                       console.log(arr)
                    }
                }
                $.ajax
                ({
                    url: $('#hdApiUrl').val() + 'api/StockAdjustment/Save',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(arr),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            successAlert(response.Message);
                            reset();
                            LoadDetails();
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        loading('stop');
                    },
                    beforeSend: function () { loading('start'); },
                    complete: function () { loading('stop'); },
                });  
            }
            //save function ends here

            //calculate adjust quantity in each row
            $(document).on('keyup', '.ins-val', function () {
                if (isNaN($(this).val()) || $(this).val() == '') {
                    $(this).val('0');
                }
                var sysStock = parseFloat($(this).closest('tr').children('td:nth-child(6)').text());
                var curStock = parseFloat($(this).val());
                var adjStock = curStock - sysStock;
                if (adjStock < 0) {
                    $(this).closest('tr').children('td:nth-child(8)').html('<i style="color:red" class="fa   fa-arrow-circle-down"></i>' + '&nbsp;' + Math.abs(adjStock)); //Adjusted Quantity
                }
                else if (adjStock > 0) {
                    $(this).closest('tr').children('td:nth-child(8)').html('<i style="color:green" class="fa   fa-arrow-circle-up"></i>' + '&nbsp;' + Math.abs(adjStock)); //Adjusted Quantity
                }
                else {
                    $(this).closest('tr').children('td:nth-child(8)').html(Math.abs(adjStock));
                }
            })
            //calculate function ends here

        });
        //document ready function ends here

    </script>
    <link href="/Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="/Theme/assets/datatables/datatables.min.js"></script>
    <script src="/Theme/Custom/Commons.js"></script>
    <link href="/Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="/Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
