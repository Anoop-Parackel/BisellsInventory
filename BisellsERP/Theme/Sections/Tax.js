function createNewTax(url, user,company,successCallback) {
    var html = '<div id="createNewTaxModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"> <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Tax</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="form-group"> <input id="txtMasterSectionTaxName" type="text" class="form-control" placeholder="Tax Name" /></div><div class="form-group"> <input type="text" class="form-control" id="txtMasterSectionTaxPercentage" placeholder="Tax Percentage" /></div><div class="col-sm-8"><div class="row"><p style="display:none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"> <button type="button" class="btn btn-default master-section-save">Save</button> <button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
    $('body').append(html);

    //For preventing dismiss onclick outside
    $('#createNewTaxModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewTaxModal').modal('show');
    //event handler for cancel
    $('#createNewTaxModal .master-section-close').off().on('click', function () {
        $('#createNewTaxModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewTaxModal .close').off().on('click', function () {
        $('#createNewTaxModal').modal('hide').remove();
    });

    //event handler for save
    $('#createNewTaxModal .master-section-save').off().on('click', function () {
        var ddlText = Number($('#txtMasterSectionTaxPercentage').val());
        $(this).text('Saving...');
        var tax={};
        tax.Name=$('#txtMasterSectionTaxName').val();
        tax.Percentage =Number($('#txtMasterSectionTaxPercentage').val());
        tax.CreatedBy = user;
        tax.CompanyId = company;
        tax.Status = 1;
        tax.ID = 0;
        $.ajax({
            url: url + 'api/Taxes/save',
            method:'POST',
            dataType:'JSON',
            data: JSON.stringify(tax),
            contentType:'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewTaxModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewTaxModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewTaxModal .master-section-save').text('Save');
                }
                console.log(data);
            },
            error: function (xhr) {
                $('#createNewTaxModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });
   
  

}