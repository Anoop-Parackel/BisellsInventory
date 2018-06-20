function createNewInstance(url, user, company,itemId,itemName,successCallback) {
    var html = '<div id="createNewInstancesModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new instance for '+itemName+'</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-4"><div class="form-group"><input id="txtSectionMrp" type="text" class="form-control" placeholder="MRP"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtSectionCostPrice" type="text" class="form-control" placeholder="Cost Price"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtSectionSellingprice" type="text" class="form-control" placeholder="Selling Price"/></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
  
    $('body').append(html);

    //For preventing dismiss onclick outside
    $('#createNewInstancesModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewInstancesModal').modal('show');
    //event handler for cancel
    $('#createNewInstancesModal .master-section-close').off().on('click', function () {
        $('#createNewInstancesModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewInstancesModal .close').off().on('click', function () {
        $('#createNewInstancesModal').modal('hide').remove();
    });


    //event handler for save
    $('#createNewInstancesModal .master-section-save').off().on('click', function () {
        var mrp = $('#txtSectionMrp').val();
        var costPrice = $('#txtSectionCostPrice').val();
        var sellingPrice = $('#txtSectionSellingprice').val();
        $(this).text('Saving...');
        var ItemInstance = {};
        ItemInstance.ItemId = itemId;
        ItemInstance.Mrp = mrp;
        ItemInstance.SellingPrice = sellingPrice;
        ItemInstance.CostPrice = costPrice;
        ItemInstance.Status = 1;
        ItemInstance.Barcode = 0;      
        ItemInstance.CreatedBy = user;
        ItemInstance.CompanyId = company;
        $.ajax({
            url: url + 'api/ItemInstance/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(ItemInstance),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    console.log('test');
                    $('#createNewInstancesModal .master-section-save').text('Saved');
                    successCallback(data.Object, mrp, costPrice, sellingPrice);
                    setTimeout(function () { $('#createNewInstancesModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewInstancesModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewInstancesModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}