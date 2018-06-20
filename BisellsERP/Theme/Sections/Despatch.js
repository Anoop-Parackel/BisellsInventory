function createNewDespatch(url, user, company, successCallback) {
    var html = '<div id="createNewDespatchModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Despatch</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionDespatchName" type="text" class="form-control" placeholder="Despatch Name" /></div></div><div class="col-sm-12"><div class="form-group"><textarea id="txtMasterSectionDespatchAddress" type="text" class="form-control" placeholder="Despatch Address" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionDespatchPhone" type="text" class="form-control" placeholder="Despatch Phone" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionDespatchMobile" type="text" class="form-control" placeholder="Despatch Mobile" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionDespatchContact" type="text" class="form-control" placeholder="Despatch Contact" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionDespatchContactPhone" type="text" class="form-control" placeholder="Despatch Phone" /></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
    $('body').append(html);

    //for closing the modal on close button
    $('#createNewDespatchModal .close').off().on('click', function () {
        $('#createNewDespatchModal').modal('hide').remove();
    });

    //For preventing dismiss onclick outside
    $('#createNewDespatchModal').modal({ backdrop: 'static', keyboard: false });

    $('#createNewDespatchModal').modal('show');
    //event handler for cancel
    $('#createNewDespatchModal .master-section-close').off().on('click', function () {
        $('#createNewDespatchModal').modal('hide').remove();

    });
    //event handler for save
    $('#createNewDespatchModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionDespatchName').val();
        $(this).text('Saving...');
        var Despatch = {};
        Despatch.Name = $('#txtMasterSectionDespatchName').val();
        Despatch.Address = $('#txtMasterSectionDespatchAddress').val();
        Despatch.PhoneNo = $('#txtMasterSectionDespatchPhone').val();
        Despatch.MobileNo = $('#txtMasterSectionDespatchMobile').val();
        Despatch.ContactPerson = $('#txtMasterSectionDespatchContact').val();
        Despatch.ContactPersonPhone = $('#txtMasterSectionDespatchContactPhone').val();
        Despatch.Status = 1;
        Despatch.ID = 0;
        Despatch.CreatedBy = user;
        Despatch.CompanyId = company;
        $.ajax({
            url: url + 'api/Despatch/save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Despatch),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewDespatchModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewDespatchModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewDespatchModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewDespatchModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}