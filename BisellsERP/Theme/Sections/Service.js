function createNewService(url, user, company, successCallback) {
    var html = '<div id="createNewServiceModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Service</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-8"><div class="form-group"><input id="txtMasterSectionServiceName" type="text" class="form-control" placeholder="Name"/></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionItemCode" type="text" class="form-control" placeholder="Item Code"/></div></div><div class="col-sm-12"><div class="form-group"><textarea id="txtMasterSectionServicDescription" type="text" class="form-control" placeholder="Description"/></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionServiceTax" class="form-control"><option value="0">--Select Tax--</option></select></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionServicSP" type="text" class="form-control" placeholder="Selling Price"/></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionServicError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
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
    $('#createNewServiceModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewServiceModal').modal('show');
    //event handler for cancel
    $('#createNewServiceModal .master-section-close').off().on('click', function () {
        $('#createNewServiceModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewServiceModal .close').off().on('click', function () {
        $('#createNewServiceModal').modal('hide').remove();
    });


    //event handler for save
    $('#createNewServiceModal .master-section-save').off().on('click', function () {
        $(this).text('Saving...');
        var Item = {};
        Item.Name = $('#txtMasterSectionServiceName').val();
        Item.ItemCode = $('#txtMasterSectionItemCode').val();
        Item.Description = $('#txtMasterSectionServicDescription').val();
        Item.TaxId = $('#ddlSectionServiceTax :selected').val();
        Item.SellingPrice = $('#txtMasterSectionServicSP').val();
        Item.Status = 1;
        Item.CreatedBy = user;
        Item.CompanyId = company;
        Item.IsService = 1;
        $.ajax({
            url: url + 'api/Items/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Item),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewServiceModal .master-section-save').text('Saved');
                    successCallback(data.Object);
                    setTimeout(function () { $('#createNewServiceModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewServiceModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewServiceModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}