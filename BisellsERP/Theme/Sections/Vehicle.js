function createNewVehicle(url, user, company, successCallback) {
    var html = '<div id="createNewVehicleModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Vehicle</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionVehicleName" type="text" class="form-control" placeholder="Name" /></div><div class="form-group"><input id="txtMasterSectionVehicleNumber" type="text" class="form-control" placeholder="Vehicle Number" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionVehicleType" type="text" class="form-control" placeholder="Type" /></div><div class="form-group"><input id="txtMasterSectionVehicleOwner" type="text" class="form-control" placeholder="Owner" /></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
    $('body').append(html);


    //For preventing dismiss onclick outside
    $('#createNewVehicleModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewVehicleModal').modal('show');
    //event handler for cancel
    $('#createNewVehicleModal .master-section-close').off().on('click', function () {
        $('#createNewVehicleModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewVehicleModal .close').off().on('click', function () {
        $('#createNewVehicleModal').modal('hide').remove();
    });

    //event handler for save
    $('#createNewVehicleModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionVehicleName').val();
        $(this).text('Saving...');
        var Vehicle = {};
        Vehicle.Name = $('#txtMasterSectionVehicleName').val();
        Vehicle.Number = $('#txtMasterSectionVehicleNumber').val();
        Vehicle.Type = $('#txtMasterSectionVehicleType').val();
        Vehicle.Owner = $('#txtMasterSectionVehicleOwner').val();
        Vehicle.Status = 1;
        Vehicle.ID = 0;
        Vehicle.CreatedBy = user;
        Vehicle.CompanyId = company;
        $.ajax({
            url: url + 'api/Vehicles/save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Vehicle),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewVehicleModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewVehicleModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewVehicleModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewVehicleModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}