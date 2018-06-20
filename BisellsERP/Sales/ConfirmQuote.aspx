<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmQuote.aspx.cs" Inherits="BisellsERP.Sales.ConfirmQuote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>
           .mini-stat.clearfix.bx-shadow {
               height: 90px;
           }

           #btnLoad {
               height: 30px;
               padding: 0 18px;
           }

           .btn-filter {
               background-color: whitesmoke;
               border: 1.2px solid rgba(239, 83, 80, .8);
               transition: all 300ms ease;
           }

           .btn {
               padding: 5px 7px;
           }

           .test-class {
           
           }

           .btn-save {
               bottom: 10px;
               right: 60px;
               height: 27px;
               width: 74px;
           }

           .btn-filter:hover, .btn-filter:active, .btn-filter:focus {
               box-shadow: none;
           }

           .btn-filter i {
               font-size: large;
               color: #ef5350;
           }

           .print-float {
               position: fixed;
               bottom: 15px;
               right: 35px;
               height: 60px;
               width: 60px;
               border-radius: 50%;
           }

               .print-float i {
                   font-size: 1.6em;
                   color: #263238;
               }

           .print-float {
               position: fixed;
               bottom: 15px;
               right: 35px;
               height: 60px;
               width: 60px;
               border-radius: 50%;
               transform: scale(.8);
               background-color: rgba(255, 215, 65, .7);
               transition: all 200ms ease-in-out;
           }

               .print-float:hover {
                   background-color: #ffd740;
                   transform: scale(.9);
               }

               .print-float i {
                   font-size: 1.6em;
                   color: #263238;
               }

           .l-font {
               font-size: 1.5em;
           }
           .panel {
               margin-bottom: 10px;
           }
           tbody tr td {
               padding: 5px !important;
               /*font-size: smaller;*/
           }

           .panel .panel-body {
               padding: 10px;
               padding-top: 30px;
           }

           .edit-value {
               background-color: transparent;
               width: 40px;
               text-align: center;
           }

           .portlet .portlet-heading {
               padding: 5px;
               padding-top: 30px;
           }

           /*.btn {
            padding: 3px 8px;
        }*/

           .modal-content-h-lg {
               height: 65vh;
               overflow-y: auto;
               background-color: whitesmoke;
           }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
     <div class="">

        <asp:HiddenField runat="server" Value="0" ID="hdId" ClientIDMode="Static" />
        <%-- ---- Page Title ---- --%>
        <div class="row p-b-5">
            <div class="col-sm-3">
                <h3 class="page-title m-t-0">Confirm Sales Quote</h3>
            </div>

        </div>
        <div class="panel panel-default b-r-8">
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="col-sm-3 ">
                        <div class="input-inside-wrap">
                            <asp:TextBox ID="txtFromDate" ClientIDMode="Static" CssClass="form-control" placeholder="From Date" runat="server"></asp:TextBox>
                            <a href="#" class="inside-btn-clear"><i class="fa fa-undo" onclick="$('#txtFromDate').val('')"></i></a>
                        </div>
                    </div>
                    <div class="col-sm-3 ">
                        <div class="input-inside-wrap">
                            <asp:TextBox ID="txtToDate" ClientIDMode="Static" CssClass="form-control" placeholder="To Date" runat="server"></asp:TextBox>
                            <a href="#" class="inside-btn-clear"><i class="fa fa-undo"></i></a>
                        </div>
                    </div>
                    <div class="col-sm-2 ">
                        <div class="input-inside-wrap">
                            <label class="title-label">Customer</label>
                            <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2 ">
                        <div class="col-sm-3 text-center">

                            <button id="btnLoad" type="button" class="btn btn-rounded btn-filter"><i class="ion-social-buffer"></i></button>

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <%-- ---- Data Table ---- --%>
        <div class="row data-table">
            <div class="col-sm-12  p-t-5">
                <div class="panel panel-default b-r-8">
                    <div class="panel-body p-t-10">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-hover table-scroll ">
                                    <thead>
                                        <tr>
                                            <th class="hidden">Sq Id</th>
                                            <th>Bill No</th>
                                            <th>Date</th>
                                            <th>Customer</th>
                                            <th>Tax</th>
                                            <th>Gross</th>
                                            <th>Net</th>
                                            <th>Status</th>
                                            <th>Confirm</th>
                                            <th></th>
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
        </div>


    </div>
    <%--Detail list modal--%>
    <div id="DetailModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">

            <!-- Modal content-->
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Sales Quote Detail</h4>
                </div>
                <div class="modal-body p-b-0">
                    <table id="tblDetail" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey">
                            <tr>
                                <th class="hidden">SqdId</th>
                                <th>Item </th>
                                <th> Code</th>
                                <th>Tax%</th>
                                <th>MRP</th>
                                <th>Rate</th>
                                <th>Qty</th>
                                <th>Tax</th>
                                <th>Gross</th>
                                <th>Net</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <%--Detail list modal ends here--%>
     <script>
         //document ready function starts here

         $(document).ready(function ()
         {
          //Listing items in the table
             $(document).on('click', '.btn-filter', function ()
             {
               var fromDate = $('#txtFromDate').val();
               var toDate = $('#txtToDate').val();
               var customerId = $('#ddlCustomer').val();
               var locationId = $.cookie('bsl_2');
               $.ajax
                   ({
                        url: $('#hdApiUrl').val() + 'api/SalesQuote/get/?fromdate=' + fromDate + '&todate=' + toDate + '&CustomerId=' + customerId,
                        method: 'POST',
                        dataType: 'JSON',
                        data: locationId,
                        contentType: 'application/json;charset=utf-8',
                        success: function (data)
                        {
                            var response = data;
                           var html = '';
                            $('#listTable').DataTable().destroy();
                            $('#listTable tbody').children().remove();
                            $(response).each(function (index)
                            {
                                html += '<tr>';
                                html += '<td class="hidden">' + this.ID + '</td>';
                                html += '<td>' + this.QuoteNumber + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.CustomerName + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += this.status == 0 ? '<td><span class="label label-danger">In Active</span></td>' : '<td><span class="label label-default">Active</span></td>';
                                html +=this.ApprovedStatus!=0?'<td><button type="button" class="btn-save btn btn-danger btn-sm">Revert</button></td>':'<td><button type="button" class="btn-save btn btn-primary btn-sm">Confirm</button></td>';
                                html += '<td><a class="view-register" title="view" href="#DetailModal" data-toggle="modal" data-target="#DetailModal"><i class="fa fa-eye"></i></a></td>';
                                html += '</tr>';
                                $('#listTable tbody').append(html);
                                html = '';

                            });

                            //binding event to modal
                            $(document).on('click', '.view-register', function ()
                            {
                                var registerId = $(this).closest('tr').children('td').eq(0).text();
                                for (var i = 0; i < response.length; i++)
                                {
                                    if (response[i].ID == registerId)
                                    {
                                        var html = '';
                                        $(response[i].Products).each(function ()
                                        {

                                            html += '<tr>';
                                            html += '<td class="hidden">' + this.DetailsID + '</td>';
                                            html += '<td>' + this.Name + '</td>';
                                            html += '<td>' + this.ItemCode + '</td>';
                                            html += '<td>' + this.TaxPercentage + '</td>';
                                            html += '<td>' + this.MRP + '</td>';
                                            html += '<td>' + this.SellingPrice + '</td>';
                                            html += '<td>' + this.Quantity + '</td>';
                                            html += '<td>' + this.TaxAmount + '</td>';
                                            html += '<td>' + this.Gross + '</td>';
                                            html += '<td>' + this.NetAmount + '</td>';
                                            html += '</tr>';
                                        });
                                        $('#tblDetail tbody').children().remove();
                                        $('#tblDetail tbody').append(html);
                                        break;
                                    }
                                }
                            });
                            $('#listTable').dataTable({ destroy: true });
                        },
                        error: function (xhr)
                        {
                            alert(xhr.responseText);
                            console.log(xhr);
                            complete: loading('stop', null);
                        },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); },
                    });
                })
            
            //save entry
             $(document).on('click', '.btn-save', function ()
             {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var currentButton = $(this);
                var ModifiedBy = $.cookie('bsl_3');
                $.ajax
                    ({
                    url: $('#hdApiUrl').val() + 'api/SalesQuote/ToggleConfirm/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(ModifiedBy),
                    success: function (data)
                    {
                        var response = data;
                     
                        if (response.Success)
                        {
                            successAlert(response.Message);
                            if (response.Object)
                            {
                                currentButton.text('Revert').removeClass('btn-primary').addClass('btn-danger');
                            }
                            else
                            {
                                currentButton.text('Confirm').removeClass('btn-danger').addClass('btn-primary');
                            }
                         }
                        else
                        {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr)
                    {
                        alert(xhr.responseText);
                        console.log(xhr);
                        complete: loading('stop', null);
                    },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });
             });

             $('#txtFromDate, #txtToDate').datepicker
             ({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true,
            });

          // Below script used for to close the date picker (auto close is not working properly)
            $('#txtFromDate').datepicker()
          .on('changeDate', function (ev)
          {
              $('#txtToDate').datepicker('hide');
          });

            //Reset This Register
            function resetRegister()
            {
                reset();
                $('#listTable tbody').children().remove();
                $('#tblDetail tbody').children().remove();
                var date = new Date();
                $('#ddlCustomer').select2('val', 0);
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#txtEntryDate').datepicker('setDate', today);
                $('#hdId').val('');
            }
             //Reset ends here

        });
      //document ready function ends here
</script>
<link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
<script src="../Theme/assets/datatables/datatables.min.js"></script>
<script src="../Theme/Custom/Commons.js"></script>
<link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
<script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
