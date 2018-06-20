<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reserve.aspx.cs" Inherits="BisellsERP.Settings.Inventory.ReserveProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        .ul-lookup > .card {
            background-color: #fff;
            height: 65px;
            width: 80%;
            padding: 5px 10px;
            box-shadow: 0 2px 1px 0 #ccc;
            margin: 10px auto;
        }
        .ul-lookup > .card > div:nth-child(2) > div {
            display: inline-block;
            margin-left: 20px;
        }
        .ul-lookup > .card > div:first-child input {
            display: inline-block;
            width: 85%;
        }
                .ul-lookup input[type=number] {
            width: 100%;
            border-radius: 3px;
            border: 1px solid #cfd8dc;
            height: 34px;
            padding: 4px;
            font-size: 16px;
            color: #607D8B;
        }
                .ul-lookup .control-label {
                    color: #546E7A;
                }

        /* Style for Add Lookup*/
        .ul-lookup-add > .card > div:first-child input {
            display: inline-block;
            width: 85%;
        }

        .ul-lookup-add button {
            background-color: #607D8B;
            border: 1px solid #607d8b;
            color: #fff;
        }

        .ul-lookup input[type=number].edit-mode{
            border: solid 1px #38bbf7;
            text-align:center;
        }
        .ul-lookup input[type=number].disabled-mode {
            text-align: center;
            background-color: transparent;
            cursor: not-allowed !important;
        }

        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <%------ Page Title ------%>
    <div class="row m-b-10">
        <div class="col-sm-4">
            <input type="hidden" value="0" id="hdId" />
            <h3 class="page-title m-t-0">Reserve Products</h3>
        </div>
    </div>
    <div class="row">
        <ul id="listItems" class="list-unstyled ul-lookup ul-lookup-add m-b-30">
            <li class="card">
                <div class="col-xs-4 m-t-10">
                     
                    <h4 class="text-muted m-t-0"><i class="fa fa-ioxhost"></i><input autocomplete="off" type="text" id="txtItem" class="form-control m-l-15" placeholder="Search Item..." /></h4>
                  <div id="lookup"></div>
                </div>
                <div class="col-xs-3 text-center">
                    <div>
                        <label class="control-label">MRP</label>
                        <h4 class="m-0">0.00</h4>
                    </div>
                    <div>
                        <label class="control-label">Selling Price</label>
                        <h4 class="m-0">0.00</h4>
                    </div>
                </div>
                <div class="col-xs-3 m-t-10">
                    <div class="form-horizontal">
                         <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                         <label class="control-label col-xs-3">Qty</label>
                         <div class="col-xs-9">
                            <input type="number" id="txtQuantity" class="" />
                        </div>
                    </div>
                </div>
                <div class="col-xs-2 text-center m-t-10">
                    <button id="btnReserve" type="button" class="btn btn-block"><i class="fa fa-bookmark-o m-r-5"></i>Reserve</button>
                </div>
            </li>
        </ul>
        <ul id="addlist" class="list-unstyled ul-lookup">
           
        </ul>
    </div>
 <script type="text/javascript">

     //document ready function starts here

         $(document).ready(function ()
         {
             //Load details of the products 
             $.ajax({
                 url: $('#hdApiUrl').val() + 'api/Reserve/GetReservedProducts',
                 method: 'POST',
                 dataType: 'JSON',
                 data:JSON.stringify($.cookie('bsl_2')),
                 contentType: 'application/json;charset=utf-8',
                 success: function (response) {
                   var html = '';
                     $(response).each(function ()
                     {
                         html += '<li data-instance="'+this.InstanceId+'" value="' + this.ItemID + '" class="card"><div class="col-xs-4 m-t-15"><h4 class="text-muted m-t-0"><i class="fa fa-ioxhost"></i><span class="m-l-15">' + this.Name + '</span></h4></div><div class="col-xs-3 text-center"><div><label class="control-label">MRP</label><h4 class="m-0">' + this.MRP + '</h4></div><div><label class="control-label">Selling Price</label><h4 class="m-0">' + this.SellingPrice + '</h4></div></div><div class="col-xs-3 m-t-10"><div class="form-horizontal"><label class="control-label col-xs-3">Qty</label><div class="col-xs-9"><input type="number" value="' + this.ReservedQuantity + '" class="disabled-mode txtRQty"  disabled="true"/></div></div></div><div class="col-xs-2 text-center m-t-10"><div class="row"><div class="btn-toolbar pull-right"><button type="button" value="edit" class="btn btn-info waves-effect waves-ripple edit-reserve"><i class="ion-edit"></i></button><button type="button" class="btn btn-danger waves-effect waves-ripple delete-reserve"><i class="ion-trash-a"></i></button></div></div></div></li>';
                         $('#addlist').prepend(html);
                         html = '';
                        
                     });
                     },
                 error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
             })

             //lookup initialization
             lookup({
                 textBoxId: 'txtItem',
                 url: $('#hdApiUrl').val() + 'api/search/items?CompanyId=' + $.cookie('bsl_1') + '&keyword=',
                 lookupDivId: 'lookup',
                 focusToId: 'txtQuantity',
                 storageKey: 'tempItem',
                 heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'SellingPrice'],
                 visibility: [false, false, true, true, true, true, true],
                 alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Rate'],
                 key: 'ItemID',
                 dataToShow: 'Name',
                 OnLoading: function () { miniLoading('start') },
                 OnComplete: function () { miniLoading('stop') }
             });
            //lookup initialization ends here

             //Add Item to list with enter key
             $('#txtQuantity').keypress(function (e)
             {
                 if (e.which == 13)
                 {
                     e.preventDefault();
                     save();
                 }
             });
             //Add item with enter key ends here
         });
     //document ready function ends here

     //save function call
         $('#btnReserve').off().click(function ()
         {
           save();
         });

     //Edit functionality strats here
         $('body').off().on('click', '.edit-reserve', function ()
         {
             if ($(this).val() == 'edit')
             {
                 $(this).closest('li').find('.txtRQty').removeClass('disabled-mode').addClass('edit-mode').prop('disabled', false);
                 $(this).html('<i class="ion-checkmark-round"></i>');
                 $(this).val('save');
             }
             else
             {
                 var button = $(this);
                 var li = $(this).closest('li');
                 var id = $(li).val();
                 var instanceId = parseInt($(li).attr('data-instance'));
                 var qty = $(this).closest('li').find('.txtRQty').val();
                 var locationId = $.cookie('bsl_2');
                 var modifiedBy = $.cookie('bsl_3');
                 $.ajax({
                     url: $(hdApiUrl).val() + 'api/Reserve/UpdateReserve?InstanceId=' + instanceId + '&id=' + id + '&locationid=' + locationId + '&ModifiedBy=' + modifiedBy,
                     method: 'POST',
                     data: JSON.stringify(qty),
                     contentType: 'application/json;charset=utf-8',
                     dataType: 'JSON',
                     success: function (response)
                     {
                         if (response.Success)
                         {
                             button.closest('li').find('.txtRQty').removeClass('edit-mode').addClass('disabled-mode').prop('disabled', true);
                             button.html('<i class="ion-edit"></i>');
                             button.val('edit');
                             successAlert(response.Message);
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
                     }
                 });
               }
         });
     //Edit functionality ends here

     //Delete functionality starts here
         $(document).on('click', '.delete-reserve', function ()
         {
             var li = $(this).closest('li');
             var id = $(li).val();
             var instanceId = parseInt($(li).attr('data-instance'));
             $.ajax({
                 url: $(hdApiUrl).val() + 'api/Reserve/DeleteReserve?id=' + id + '&instanceid=' + instanceId,
                 method: 'POST',
                dataType: 'JSON',
                success: function (response)
                {
                    if (response.Success)
                    {
                      li.remove();
                      successAlert(response.Message);
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
                 }
             });
         });
     //Delete function ends here

     //Save function starts here
         function save()
         {
             var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
             var data = {};
             var qty = $('#txtQuantity').val();
             var id = tempItem.ItemID;
             var instanceId = tempItem.InstanceId;
             var createdBy = $.cookie('bsl_3');
             data.Quantity = qty;
             var locationId=$.cookie('bsl_2');
                 $.ajax({
                     url: $(hdApiUrl).val() + 'api/Reserve/ReserveQty?InstanceId=' + instanceId + '&itemid=' + id + '&locationid=' + locationId+'&CreatedBy='+createdBy,
                     method: 'POST',
                     data: JSON.stringify(qty),
                     contentType: 'application/json;charset=utf-8',
                     dataType: 'JSON',
                     success: function (response)
                     {
                         if (response.Success)
                         {
                             var html = '';
                             html += '<li value="' + id + '" data-instance=' + instanceId + ' class="card"><div class="col-xs-4 m-t-15"><h4 class="text-muted m-t-0"><i class="fa fa-ioxhost"></i><span class="m-l-15">' + tempItem.Name + '</span></h4></div><div class="col-xs-3 text-center"><div><label class="control-label">MRP</label><h4 class="m-0">' + tempItem.MRP + '</h4></div><div><label class="control-label">Selling Price</label><h4 class="m-0">' + tempItem.SellingPrice + '</h4></div></div><div class="col-xs-3 m-t-10"><div class="form-horizontal"><label class="control-label col-xs-3">Qty</label><div class="col-xs-9"><input type="number" value="' + qty + '" class="txtRQty  disabled-mode" disabled="true"/></div></div></div><div class="col-xs-2 text-center m-t-10"><div class="row"><div class="btn-toolbar pull-right"><button  type="button" class="btn btn-info waves-effect waves-ripple  edit-reserve" value="edit"><i class="ion-edit"></i></button><button type="delete-reserve"  class="btn btn-danger waves-effect waves-ripple delete-reserve"><i class="ion-trash-a"></i></button></div></div></div></li>';
                             $('#addlist').prepend(html);
                             html = '';
                             $('#txtQuantity').val('');
                             $('#txtItem').val('');
                             $('#txtItem').focus();
                             successAlert(response.Message);
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
                     }
                 });
         }
     //Save function ends here
</script>
<script src="/Theme/Custom/Commons.js"></script>
<link href="/Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
<script src="/Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>

