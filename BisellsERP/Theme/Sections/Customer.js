function createNewCustomer(url, user, company, successCallback) {
    var html = '<div id="createNewCustomerModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Customer</h4></div><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionCustomerName" type="text" class="form-control" placeholder="Name" /></div></div><div class="col-sm-3"><div class="form-group"><select id="ddlCustomerSectionSalutation" class="form-control"><option value="Mr">Mr.</option><option value="Mrs">Mrs.</option><option value="Ms">Ms.</option><option value="Miss">Miss.</option></select></div></div><div class="col-sm-9"><div class="form-group"><input type="text" class="form-control" id="txtMasterCustomerSectionContactName" placeholder="Contact Name" /></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionCustomerAddress1" type="text" class="form-control" placeholder="Address Line 1" /></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionCustomerAddress2" type="text" class="form-control" placeholder="Address Line 2" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterCustomerSectionPhone1" type="text" class="form-control" placeholder="Phone 1" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterCustomerSectionPhone2" type="text" class="form-control" placeholder="Phone 2" /></div></div><div class="col-sm-6"><div class="form-group"><input type="text" id="txtMasterCustomerSectionZipCode" placeholder="Zipcode" class="form-control" /></div></div><div class="col-sm-6"><div class="form-group"><input type="text" class="form-control" id="txtMasterSectionCustomerCity" placeholder="City" /></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterCustomerSectionEmail" type="email" pattern="/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/" class="form-control" placeholder="Email" required /></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionCustomerCountry" class="form-control" clientidmode="Static"><option value="0">--Select Country--</option></select></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionCustomerStates" class="form-control"><option value="0">--Select States--</option></select></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionCustomerTax1" type="text" class="form-control" placeholder="TRN/GST No" /></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionCustomerTax2" type="text" class="form-control" placeholder="CIN/Reg No" /></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionCustomerCreditAmount" type="text" class="form-control" placeholder="Credit Amount" /></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionCustomerCreditPeriod" type="text" class="form-control" placeholder="Credit Period" /></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionCustomerLockAmount" type="text" class="form-control" placeholder="Lock Amount" /></div></div><div class="col-sm-4"><div class="form-group"><input id="txtMasterSectionCustomerLockPeriod" type="text" class="form-control" placeholder="Lock Period" /></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';

    //get country list
    $.ajax({
        type: "POST",
        url: url+"api/customers/Getcountry?companyId=" + company,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify($.cookie("bsl_1")),
        dataType: "json",
        success: function (data) {
            $.each(data, function () {
                $("#ddlSectionCustomerCountry").append($("<option/>").val(this.Country_Id).text(this.Name));
            });
        },
        failure: function () {
            $('.master-section-error').text('Sorry. Something went wrong. Try again later');
            $('#masterSectionPError').css('display', 'block');
        }
    });
    $('body').append(html);

    //For preventing dismiss onclick outside
    $('#createNewCustomerModal').modal({ backdrop: 'static', keyboard: false });

    $('#createNewCustomerModal').modal('show');
    //event handler for cancel
    $('#createNewCustomerModal .master-section-close').off().on('click', function () {
        $('#createNewCustomerModal').modal('hide').remove();
    });

    //for closing the modal on close button
    $('#createNewCustomerModal .close').off().on('click', function () {
        $('#createNewCustomerModal').modal('hide').remove();
    });

    //To Load state
    $('#createNewCustomerModal #ddlSectionCustomerCountry').on('change', function () {
        var selected = $('#ddlSectionCustomerCountry').val();
        if (selected==0) {
            $('#ddlSectionCustomerStates').empty();
            $('#ddlSectionCustomerStates').append("<option>--Select States--</option>")
        }
        else {
            $('#ddlSectionCustomerStates').empty();
            $('#ddlSectionCustomerStates').append("<option>--Select States--</option>")
            $.ajax({
                type: "POST",
                url: url+"/api/customers/GetStates?id=" + selected,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify($.cookie("bsl_1")),
                dataType: "json",
                success: function (data) {
                    $.each(data, function () {
                        $("#ddlSectionCustomerStates").append($("<option/>").val(this.StateId).text(this.State));
                    });
                },
                failure: function () {
                    $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                    $('#masterSectionPError').css('display', 'block');
                }
            });
        }
    });
    //event handler for save
    $('#createNewCustomerModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionCustomerName').val();
        $(this).text('Saving...');
        var Customer = {};
        Customer.Name = $('#txtMasterSectionCustomerName').val();
        Customer.Address1 = $('#txtMasterSectionCustomerAddress1').val();
        Customer.Address2 = $('#txtMasterSectionCustomerAddress2').val();
        Customer.CountryId = $('#ddlSectionCustomerCountry :selected').val();
        Customer.StateId = $('#ddlSectionCustomerStates :selected').val();
        Customer.Phone1 = $('#txtMasterCustomerSectionPhone1').val();
        Customer.Phone2 = $('#txtMasterCustomerSectionPhone2').val();
        Customer.Email = $('#txtMasterCustomerSectionEmail').val();
        Customer.Taxno1 = $('#txtMasterSectionCustomerTax1').val();
        Customer.Taxno2 = $('#txtMasterSectionCustomerTax2').val();
        Customer.CreditAmount = $('#txtMasterSectionCustomerCreditAmount').val();
        Customer.CreditPeriod = $('#txtMasterSectionCustomerCreditPeriod').val();
        Customer.LockPeriod = $('#txtMasterSectionCustomerLockPeriod').val();
        Customer.LockAmount = $('#txtMasterSectionCustomerLockAmount').val();
        Customer.Salutation = $('#ddlCustomerSectionSalutation').val();
        Customer.ZipCode = $('#txtMasterCustomerSectionZipCode').val();
        Customer.City = $('#txtMasterSectionCustomerCity').val();
        Customer.Contact_Name = $('#txtMasterCustomerSectionContactName').val();
        Customer.ID = 0;
        Customer.Status = 1;
        Customer.CreatedBy = user;
        Customer.CompanyId = company;
        $.ajax({
            url: url + 'api/Customers/save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Customer),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    console.log(data);
                    $('#createNewCustomerModal .master-section-save').text('Saved');
                    successCallback(data.Object.Id, ddlText);
                    setTimeout(function () { $('#createNewCustomerModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewCustomerModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewCustomerModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });


}