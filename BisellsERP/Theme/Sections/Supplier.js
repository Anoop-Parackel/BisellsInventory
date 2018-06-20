function createNewSupplier(url, user, company, successCallback) {
    var html = '<div id="createNewSupplierModal" class="modal animated fadeIn master-section" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button><h4 class="modal-title">Create new Supplier</h4><div class="modal-body p-b-0"><div class="row"><div class="col-sm-12"><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionSupplierName" type="text" class="form-control" placeholder="Name" /></div></div><div class="col-sm-3"><div class="form-group"><select id="ddlSupplierSectionSalutation" class="form-control"><option value="Mr">Mr.</option><option value="Mrs">Mrs.</option><option value="Ms">Ms.</option><option value="Dr">Dr.</option></select></div></div><div class="col-sm-9"><div class="form-group"><input type="text" id="txtSupplierSectionContactName" class="form-control" placeholder="Contact Name" /></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionSupplierAddress1" type="text" class="form-control" placeholder="Address Line 1" /></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionSupplierAddress2" type="text" class="form-control" placeholder="Address Line 2" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionSupplierPhone1" type="text" class="form-control" placeholder="Phone 1" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionSupplierPhone2" type="text" class="form-control" placeholder="Phone 2" /></div></div><div class="col-sm-6"><div class="form-group"><input type="text" id="txtSupplierSectionCity" class="form-control" placeholder="City" /></div></div><div class="col-sm-6"><div class="form-group"><input type="text" id="txtSupplierSectionZipcode" class="form-control" placeholder="Zipcode" /></div></div><div class="col-sm-12"><div class="form-group"><input id="txtMasterSectionSupplierEmail" type="text" class="form-control" placeholder="Email" /></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionSupplierCountry" class="form-control"><option value="0">--Select Country--</option></select></div></div><div class="col-sm-6"><div class="form-group"><select id="ddlSectionSupplierStates" class="form-control"><option value="0">--Select States--</option></select></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionSupplierTax1" type="text" class="form-control" placeholder="TRN/GST No" /></div></div><div class="col-sm-6"><div class="form-group"><input id="txtMasterSectionSupplierTax2" type="text" class="form-control" placeholder="CIN/Reg No" /></div></div></div><div class="col-sm-8"><div class="row"><p style="display: none" id="masterSectionPError" class="text-danger m-t-5"><i class="fa fa-warning m-r-5"></i><span class="master-section-error">error</span></p></div></div><div class="col-sm-4"><div class="btn-toolbar pull-right"><button type="button" class="btn btn-default master-section-save">Save</button><button type="button" class="btn btn-danger master-section-close">Cancel</button></div></div></div></div></div></div></div></div>';
    $.ajax({
        type: "POST",
        url: url+"api/customers/Getcountry?companyId=" + company,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify($.cookie("bsl_1")),
        dataType: "json",
        success: function (data) {
            $.each(data, function () {
                $("#ddlSectionSupplierCountry").append($("<option/>").val(this.Country_Id).text(this.Name));
            });
        },
        failure: function () {
            $('.master-section-error').text('Sorry. Something went wrong. Try again later');
            $('#masterSectionPError').css('display', 'block');
        }
    });


    $('body').append(html);

    //For preventing dismiss onclick outside
    $('#createNewSupplierModal').modal({ backdrop: 'static', keyboard: false })

    $('#createNewSupplierModal').modal('show');
    //event handler for cancel
    $('#createNewSupplierModal .master-section-close').off().on('click', function () {
        $('#createNewSupplierModal').modal('hide').remove();

    });

    //for closing the modal on close button
    $('#createNewSupplierModal .close').off().on('click', function () {
        $('#createNewSupplierModal').modal('hide').remove();
    });

    //event handler for save
    $('#createNewSupplierModal .master-section-save').off().on('click', function () {
        var ddlText = $('#txtMasterSectionSupplierName').val();
        $(this).text('Saving...');
        var Supplier = {};
        Supplier.Name = $('#txtMasterSectionSupplierName').val();
        Supplier.Address1 = $('#txtMasterSectionSupplierAddress1').val();
        Supplier.Address2 = $('#txtMasterSectionSupplierAddress2').val();
        Supplier.CountryId = $('#ddlSectionSupplierCountry :selected').val();
        Supplier.StateId = $('#ddlSectionSupplierStates :selected').val();
        Supplier.Phone1 = $('#txtMasterSectionSupplierPhone1').val();
        Supplier.Phone2 = $('#txtMasterSectionSupplierPhone2').val();
        Supplier.Email = $('#txtMasterSectionSupplierEmail').val();
        Supplier.Taxno1 = $('#txtMasterSectionSupplierTax1').val();
        Supplier.Taxno2 = $('#txtMasterSectionSupplierTax2').val();
        Supplier.Contact_Name = $('#txtSupplierSectionContactName').val();
        Supplier.Salutation = $('#ddlSupplierSectionSalutation').val();
        Supplier.ZipCode = $('#txtSupplierSectionZipcode').val();
        Supplier.City = $('#txtSupplierSectionCity').val();
        Supplier.Status = 1;
        Supplier.ID = 0;
        Supplier.CreatedBy = user;
        Supplier.CompanyId = company;
        $.ajax({
            url: url+'api/Suppliers/Save',
            method: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(Supplier),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    $('#createNewSupplierModal .master-section-save').text('Saved');
                    successCallback(data.Object, ddlText);
                    setTimeout(function () { $('#createNewSupplierModal').modal('hide').remove(); }, 500);
                }
                else {
                    $('.master-section-error').text(data.Message);
                    $('#masterSectionPError').css('display', 'block');
                    $('#createNewSupplierModal .master-section-save').text('Save');
                }
            },
            error: function (xhr) {
                $('#createNewSupplierModal .master-section-save').text('Save');
                console.log(xhr); $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                $('#masterSectionPError').css('display', 'block');
            }
        });
    });
    //To Load state
    $('#createNewSupplierModal #ddlSectionSupplierCountry').on('change', function () {
        var selected = $('#ddlSectionSupplierCountry').val();
        if (selected == 0)
        {
            $('#ddlSectionSupplierStates').empty();
            $('#ddlSectionSupplierStates').append("<option>--Select States--</option>")
        }
        else {
            $('#ddlSectionSupplierStates').empty();
            $('#ddlSectionSupplierStates').append("<option>--Select States--</option>")
            $.ajax({
                type: "POST",
                url: url+"api/customers/GetStates?id=" + selected,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify($.cookie("bsl_1")),
                dataType: "json",
                success: function (data) {
                    $('#ddlSectionSupplierStates').empty;
                    $.each(data, function () {
                        $("#ddlSectionSupplierStates").append($("<option/>").val(this.StateId).text(this.State));
                    });
                },
                failure: function () {
                    $('.master-section-error').text('Sorry. Something went wrong. Try again later');
                    $('#masterSectionPError').css('display', 'block');
                }
            });
        }
        
    });


}