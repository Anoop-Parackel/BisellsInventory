function createNewGroup(url, user, company, successCallback) {
    var html = '<div id="createNewGroupModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Group</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionGroupName" type="text" class="form-control" placeholder="Group Name" /></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div> </div></div>';
    $('body').append(html);


    //For preventing dismiss onclick outside
    $('#createNewGroupModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewGroupModal').modal('show');

    //event handler for cancel
    $('#createNewGroupModal .master-section-close').off().on('click', function () {
        $('#createNewGroupModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewGroupModal .close').off().on('click', function () {
        $('#createNewGroupModal').modal('hide').remove();
    });


    //event handler for save
    $('#createNewGroupModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionGroupName').val();
        $(this).text('Saving...');
        var Group = {};
        Group.Name = $('#txtMasterSectionGroupName').val();
        Group.Status = 1;
        Group.CreatedBy = user;
        Group.CompanyId = company;
        Group.ID = 0;
        $.ajax({
            url: url+'api/Groups/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Group),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewGroupModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewGroupModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewGroupModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewGroupModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}