function createNewJobs(url, user, company, successCallback) {
    
    var html = '<div id="createNewJobModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Job</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionJobName" type="text" class="form-control" placeholder="Job Name" /></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionJobCustomerlist" class="form-control"><option value="0">--Select--</option></select></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionSiteAddress" type="text" class="form-control" placeholder="Site Address" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionContactName" type="text" class="form-control" placeholder="Contact Name" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionContactAddress" type="text" class="form-control" placeholder="Contact Address" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionStartDate" type="text" class="form-control" clientidmode="static" placeholder="DD/MMM/YYYY" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionEstimateAmount" type="text" class="form-control" placeholder="Estimated Amount" /></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';


    $.ajax({
        type: "POST",
        url: url + "api/Jobs/GetCustomer",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify($.cookie("bsl_1")),
        dataType: "json",
        success: function (data) {
            console.log(data);
            $.each(data, function () {
                $("#ddlSectionJobCustomerlist").append($("<option/>").val(this.Customer_Id).text(this.Name));
            });
        },
        failure: function () {
            $('.master-section-error').text('Sorry. Something went wrong. Try again later');
            $('#masterSectionPError').css('display', 'block');
        }
    });

    

    $('body').append(html);

    //For preventing dismiss onclick outside
    $('#createNewJobModal').modal({ backdrop: 'static', keyboard: false });

    $('#txtMasterSectionStartDate').datepicker({
        autoClose: true,
        format: 'dd/M/yyyy',
        todayHighlight: true
    });

    $('#txtMasterSectionStartDate').datepicker()
          .on('changeDate', function (ev) {
              $('#txtMasterSectionStartDate').datepicker('hide');
          });

    $('#createNewJobModal').modal('show');
    //event handler for cancel
    $('#createNewJobModal .master-section-close').off().on('click', function () {
        $('#createNewJobModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewJobModal .close').off().on('click', function () {
        $('#createNewJobModal').modal('hide').remove();
    });
    //event handler for save
    $('#createNewJobModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionJobName').val();
        $(this).text('Saving...');
        var Job = {};
        Job.JobName = $('#txtMasterSectionJobName').val();
        Job.CustomerId = $('#ddlSectionJobCustomerlist').val();
        Job.SiteAddress = $('#txtMasterSectionSiteAddress').val();
        Job.ContactName = $('#txtMasterSectionContactName').val();
        Job.ContactAddress = $('#txtMasterSectionContactAddress').val();
        Job.StartDate = $('#txtMasterSectionStartDate').val();
        Job.EstimatedAmount = $('#txtMasterSectionEstimateAmount').val();
        Job.Status = 0;
        Job.CreatedBy = user;
        Job.CompanyId = company;
        Job.ID = 0;
        $.ajax({
            url: url + 'api/Jobs/save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Job),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewJobModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewJobModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewJobModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewJobModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });



}