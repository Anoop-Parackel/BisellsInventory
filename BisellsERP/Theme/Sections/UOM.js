function createNewUOM(url, user, company, successCallback) {
    var html = '<div id="createNewUOMModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"> <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new UOM</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="form-group"> <input id="txtMasterSectionUOMName" type="text" class="form-control" placeholder="UOM Name" /></div><div class="form-group"> <input type="text" class="form-control" id="txtMasterSectionUOMShort" placeholder="Short Name" /></div><div class="col-sm-8"><div class="row"><p style="display:none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"> <button type="button" class="btn btn-default master-section-save">Save</button> <button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
    $('body').append(html);

    //For preventing dismiss onclick outside
    $('#createNewUOMModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewUOMModal').modal('show');
    //event handler for cancel
    $('#createNewUOMModal .master-section-close').off().on('click', function () {
        $('#createNewUOMModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewUOMModal .close').off().on('click', function () {
        $('#createNewUOMModal').modal('hide').remove();
    });

    //event handler for save
    $('#createNewUOMModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionUOMShort').val();
        $(this).text('Saving...');
        var UOM = {};
        UOM.Name = $('#txtMasterSectionUOMName').val();
        UOM.ShortName = $('#txtMasterSectionUOMShort').val();
        UOM.CreatedBy = user;
        UOM.CompanyId = company;
        UOM.Status = 1;
        UOM.ID = 0;
        $.ajax({
            url: url+'api/Units/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(UOM),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewUOMModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewUOMModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewUOMModal .master-section-save').text('Save');
                }
                console.log(data);
            },
            error: function (xhr) {
                $('#createNewUOMModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}