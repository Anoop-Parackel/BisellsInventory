<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Holiday.aspx.cs" Inherits="BisellsERP.Payroll.Holiday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Holiday</title>
    <link href="../Theme/assets/bootstrap-year-calendar/bootstrap-year-calendar.css" rel="stylesheet" />
    <style>
        .content-page {
            min-height: 300px;
        }

        .calendar {
            overflow: unset;
            padding: 0 0 10px;
        }

        .popover {
            min-width: 250px;
            transform: translateX(-90px);
        }
            .popover .control-label {
                font-weight: 100;
                color: #6e98ad;
                font-size: 13px;
                margin-bottom: 0;
                margin-top: 5px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%--  Page Title--%>
    <div class="row p-b-10">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Holidays</h3>
        </div>
    </div>
    <div class="row">
        <div id="calendar" style="background-color: white;">
        </div>
        <div id="holiday" class="hidden">
            <input type="hidden" class="holiday-id" id="hdId" value="0" />
            <input type="hidden" class="holiday-date" id="lblHolidayDate" />
            <label class="control-label">Holiday Title</label>
            <textarea id="txtNam" cols="30" rows="1" class="form-control input-sm holiday-name" required="required"></textarea>
            <label class="control-label">Description</label>
            <textarea id="txtDescription" cols="30" rows="2" class="form-control input-sm holiday-desc" required="required"></textarea>
            <div class="row">
                <div class="col-md-12">
                    <div class="btn-toolbar m-t-15 pull-right">
                        <button type="button" class="btn btn-default btn-sm btn-save"><i class="ion-checkmark-round"></i>&nbsp;Add Holiday</button>
                        <button type="button" class="btn btn-danger btn-sm btn-close">X</button>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <script>
        $('document').ready(function () {
            $.ajax({
                url: $('#hdApiUrl').val() + 'api/Holiday/Get?companyid=' + $.cookie('bsl_1'),
                method: 'POST',
                dataType: 'JSON',
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        data[i].startDate = new Date(Date.parse(data[i].startDate));
                        data[i].endDate = data[i].startDate;
                    }

                    console.log(data);
                    $('#calendar').calendar({
                        clickDay: function (e) {
                            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', ];
                            var date = e.date.getDate() + '/' + months[e.date.getMonth()] + '/' + e.date.getFullYear();
                            $('td.day').popover('hide');
                            $('#holiday').find('#lblHolidayDate').val(date);
                            if (e.events[0] != null && e.events[0] != undefined) {
                                console.log(e.events[0].Name);
                                $('#holiday').find('#txtNam').html(e.events[0].Name);
                                $('#holiday').find('#hdId').val(e.events[0].Id);
                                $('#holiday').find('#txtDescription').html(e.events[0].Description);
                                $('#holiday').find('.btn-save').html('<i class="ion-checkmark-round"></i> Update Holiday');
                            }
                            else {
                                $('#holiday').find('.btn-save').html('<i class="ion-plus-round"></i> Add Holiday');
                            }
                            $(e.element[0]).popover({
                                placement: 'auto',
                                html: true,
                                content: $('#holiday').html()
                            });
                            $(e.element[0]).popover('show');
                            $('#holiday').find('#txtNam').html('');
                            $('#holiday').find('#hdId').val('0');
                            $('#holiday').find('#txtDescription').html('');
                            $('#holiday').find('#lblHolidayDate').val('');

                        },
                        dataSource: data
                    });
                }
            })

            $(document).on('click', '.btn-save', function () {
                var date = $(this).closest('div.popover-content').find('.holiday-date').val();
                var name = $(this).closest('div.popover-content').find('.holiday-name').val();
                var desc = $(this).closest('div.popover-content').find('.holiday-desc').val();
                saveEvent(date, name, desc);
            });

            function saveEvent(date, name, desc) {

                var holiday = {};
                holiday.Name = name;
                holiday.Description = desc;
                holiday.Date = date;
                holiday.Id = $('#hdId').val();
                holiday.CompanyId = $.cookie('bsl_1');
                holiday.CreatedBy = $.cookie('bsl_3');
                holiday.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $(hdApiUrl).val() + 'api/Holiday/Save',
                    method: 'POST',
                    data: JSON.stringify(holiday),
                    contentType: 'application/json',
                    dataType: 'JSON',
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                            $('.popover').popover('hide');
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        complete: miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop') },
                });
            }

            $(document).on('click', '.btn-close', function () {
                $('.popover').popover('hide');
            });

        });
    </script>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/Sections/Customer.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/bootstrap-year-calendar/bootstrap-year-calendar.js"></script>
</asp:Content>
