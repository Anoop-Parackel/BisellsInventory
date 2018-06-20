function createNewItem(url, user, company, successCallback) {
    var html = '<div id="createNewItemModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Item</h4></div><div class="modal-body p-b-0"><div class="col-sm-4"><div class="col-sm-6 radio radio-primary"><input type="radio" class="radio radio-primary product-type" name="type" value="1" checked="checked" /><label>Item</label></div><div class="col-sm-6 radio radio-primary m-t-10"><input type="radio" name="type" value="0" class="radio radio-primary product-type" /><label>Service</label></div></div><div class="col-sm-4"></div><div class="row item-section"><div class="col-sm-12"><div class="col-sm-8"><div class="form-group"><input id="txtMasterSectionItemName" type="text" class="form-control" placeholder="Name"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemCode" type="text" class="form-control" placeholder="Item Code"/></div></div><div class="col-sm-12"><div class="form-group"><textarea id="txtMasterSectionItemDescription" type="text" class="form-control" placeholder="Description"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemOEM" type="text" class="form-control" placeholder="OEM Code"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemHSCode" type="text" class="form-control" placeholder="HS Code"/></div></div><div class="col-sm-4"><div class="form-group"><div class="checkbox checkbox-primary"><input id="chkMasterSectionTrackInventory" type="checkbox" checked="true" class="checkbox checkbox-primary"/><label>Track Inventory</label></div></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionItemTax" class="form-control"><option value="0">--Select Tax--</option></select></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionItemUnit" class="form-control"><option value="0">--Select Unit--</option></select></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemMRP" type="text" class="form-control" placeholder="MRP"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemCost" type="text" class="form-control" placeholder="Cost Price"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemSP" type="text" class="form-control" placeholder="Selling Price"/></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionItemGroup" class="form-control"><option value="0">--Select Group--</option></select></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionItemCategory" class="form-control"><option value="0">--Select Category--</option></select></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionItemType" class="form-control"><option value="0">--Select Type--</option></select></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionItemBrand" class="form-control"><option value="0">--Select Brand--</option></select></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save-item">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div><div class="row service-section hidden"><div class="col-sm-12"><div class="col-sm-8"><div class="form-group"><input id="txtMasterSectionServiceName" type="text" class="form-control" placeholder="Name"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionServiceCode" type="text" class="form-control" placeholder="Item Code"/></div></div><div class="col-sm-12"><div class="form-group"><textarea id="txtMasterSectionServicDescription" type="text" class="form-control" placeholder="Description"/></div></div><div class="col-sm-4"><div class="form-group"><select id="ddlSectionServiceTax" class="form-control"><option value="0">--Select Tax--</option></select></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionServicSP" type="text" class="form-control" placeholder="Selling Price"/></div></div><div class="col-sm-4"><div class="form-group"><div class="checkbox checkbox-primary"><input id="chkMasterSectionServiceTrackInventory" type="checkbox" class="checkbox checkbox-primary" /><label>Track Inventory</label></div></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionServicError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save-service">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
    $.ajax({
        type: "POST",
        url: url+"api/items/GetAsscociateData?CompanyID=" + company,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify($.cookie("bsl_1")),
        dataType: "json",
        success: function (data) {
            console.log(data)
            $.each(data.Types, function () {
                $("#ddlSectionItemType").append($("<option/>").val(this.Type_Id).text(this.Name));
            });
            $.each(data.Brands, function () {
                $('#ddlSectionItemBrand').append($("<option/>").val(this.Brand_ID).text(this.Name));
            });
            $.each(data.Categories, function () {
                $('#ddlSectionItemCategory').append($("<option/>").val(this.Category_Id).text(this.Name));
            });
            $.each(data.Groups, function () {
                $('#ddlSectionItemGroup').append($("<option/>").val(this.Group_ID).text(this.Name));
            });
            $.each(data.Taxes, function () {
                $('#ddlSectionItemTax').append($("<option/>").val(this.Tax_Id).text(this.Percentage));
            });
            $.each(data.Unit, function () {
                $('#ddlSectionItemUnit').append($("<option/>").val(this.Unit_Id).text(this.Short_Name));
            });
        },
        failure: function () {
            $('.master-section-error').text('Sorry. Something went wrong. Try again later');
            $('#masterSectionPError').css('display', 'block');
        }
    });

    $.ajax({
        type: "POST",
        url: url + "api/items/GetAsscociateData?CompanyID=" + company,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify($.cookie("bsl_1")),
        dataType: "json",
        success: function (data) {
            console.log(data)

            $.each(data.Taxes, function () {
                $('#ddlSectionServiceTax').append($("<option/>").val(this.Tax_Id).text(this.Percentage));
            });
        },
        failure: function () {
            $('.master-section-error').text('Sorry. Something went wrong. Try again later');
            $('#masterSectionServiceError').css('display', 'block');
        }
    });

    $('body').append(html);
    
    //For preventing dismiss onclick outside
    $('#createNewItemModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewItemModal').modal('show');
    //event handler for cancel
    $('#createNewItemModal .master-section-close').off().on('click', function () {
        $('#createNewItemModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewItemModal .close').off().on('click', function () {
        $('#createNewItemModal').modal('hide').remove();
    });

    $('.product-type').change(function () {
        var type = $(this).val();
        if (type==0) {
            $('.item-section').addClass('hidden');
            $('.service-section').removeClass('hidden');
        }
        else {
            $('.item-section').removeClass('hidden');
            $('.service-section').addClass('hidden');
        }
    });


    //event handler for save
    $('#createNewItemModal .master-section-save-item').off().on('click', function () {
        var ddlText = $('#txtMasterSectionItemName').val();
        $(this).text('Saving...');
        var Item = {};
        Item.Name = $('#txtMasterSectionItemName').val();
        Item.UnitID = $('#ddlSectionItemUnit :selected').val();
        Item.Description = $('#txtMasterSectionItemDescription').val();
        Item.HSCode = $('#txtMasterSectionItemHSCode').val();
        Item.OEMCode = $('#txtMasterSectionItemOEM').val();
        Item.TypeID = $('#ddlSectionItemType :selected').val();
        Item.CategoryID = $('#ddlSectionItemCategory :selected').val();
        Item.GroupID = $('#ddlSectionItemGroup :selected').val();
        Item.BrandID = $('#ddlSectionItemBrand :selected').val();
        Item.TaxId = $('#ddlSectionItemTax :selected').val();
        Item.MRP = $('#txtMasterSectionItemMRP').val();
        if ($('#chkMasterSectionTrackInventory').prop('checked') == true) {
            track_Inventory = 1;
        }
        else {
            track_Inventory = 0;
        }
        Item.TrackInventory = track_Inventory;
        Item.SellingPrice = $('#txtMasterSectionItemSP').val();
        Item.CostPrice = $('#txtMasterSectionItemCost').val();
        Item.ItemCode = $('#txtMasterSectionItemCode').val();
        Item.Status = 1;
        Item.ItemID = 0;
        Item.CreatedBy = user;
        Item.CompanyId = company;
        $.ajax({
            url: url+'api/Items/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Item),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewItemModal .master-section-save-item').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewItemModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewItemModal .master-section-save-item').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewItemModal .master-section-save-item').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });

    $('.master-section-save-service').off().on('click', function () {
        $(this).text('Saving...');
        var Item = {};
        Item.Name = $('#txtMasterSectionServiceName').val();
        Item.ItemCode = $('#txtMasterSectionServiceCode').val();
        Item.Description = $('#txtMasterSectionServicDescription').val();
        Item.TaxId = $('#ddlSectionServiceTax :selected').val();
        Item.SellingPrice = $('#txtMasterSectionServicSP').val();
        Item.Status = 1;
        Item.CreatedBy = user;
        Item.CompanyId = company;
        if ($('#chkMasterSectionServiceTrackInventory').prop('checked') == true) {
            track_Inventory = 1;
        }
        else {
            track_Inventory = 0;
        }
        Item.TrackInventory = track_Inventory;
        Item.TrackInventory = 1;
        Item.IsService = 1;
        $.ajax({
            url: url + 'api/Items/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Item),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('.master-section-save-service').text('Saved');
                    successCallback(data.Object);
                    setTimeout(function () { $('#createNewItemModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('.master-section-save-service').text('Save');
                }
            },
            error: function (xhr) {
                $('.master-section-save-service').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}