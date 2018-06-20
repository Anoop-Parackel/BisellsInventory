//These Functions can be used all over the app
//Author: Nithin, Jibi

//global variables for handling xhr requests in a more elegant way
//Strict warning: Do not use these variables anywhere else on the app
var xhrTimer;
var theXRequest;


//popup
function PopupCenter(url, title, w, h) {

    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;
    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;
    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;
    var newWindow = window.open(url, title, 'scrollbars=yes,toolbar=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    if (window.focus) { newWindow.focus(); }
};

//Loading Spinner
// eg: loading(start,your message here);
function loading(flag, message) {
    message = message || 'loading...';
    $('.loading-overlay p').html(message);
    if (flag === 'start') {
        $('.loading-overlay').css('display', 'block');
    }
    if (flag === 'stop') {
        $('.loading-overlay').css('display', 'none');
    }
}
//Mini Loading Spinner
// eg: miniLoading('start');
function miniLoading(flag) {
    if (flag === 'start') {
        $('.mini-wrap').addClass('show');
    }
    if (flag === 'stop') {
        $('.mini-wrap').removeClass('show');
    }
}
function searchOnTable(input, table, indexToSearch) {
    var filter = input.val().toUpperCase();
    var rows = table.children('tbody').children('tr');
    for (var i = 0; i < rows.length; i++) {
        if ($(rows[i]).children('td').eq(1).html().toUpperCase().indexOf(filter) > -1) {
            $(rows[i]).css('display', 'table-row');
        }
        else {
            $(rows[i]).css('display', 'none');
        }
    }
}
var Notification = function () { };
Notification.prototype.autoHide = function (style, position, title, text) {
    var icon = "fa fa-adjust";
    if (style == "error") {
        icon = "fa fa-exclamation";
    } else if (style == "warning") {
        icon = "fa fa-warning";
    } else if (style == "success") {
        icon = "fa fa-check";
    } else if (style == "info") {
        icon = "fa fa-question";
    } else {
        icon = "fa fa-adjust";
    }
    $.notify({
        title: title,
        text: text,
        image: "<i class='" + icon + "'></i>"
    }, {
        style: 'metro',
        className: style,
        globalPosition: position,
        showAnimation: "show",
        showDuration: 0,
        hideDuration: 0,
        autoHideDelay: 5000, //Autohide Time
        autoHide: true,
        clickToHide: true
    });
},
    Notification.prototype.init = function () {
    },
    $.Notification = new Notification, $.Notification.Constructor = Notification

//Dashboard doughnut progressbar
function updateDonutProgress(el, percent, donut) {
    percent = Math.round(percent);
    if (percent > 100) {
        percent = 100;
    } else if (percent < 0) {
        percent = 0;
    }
    var deg = Math.round(360 * (percent / 100));

    if (percent > 50) {
        $(el + ' .pie').css('clip', 'rect(auto, auto, auto, auto)');
        $(el + ' .right-side').css('transform', 'rotate(180deg)');
    } else {
        $(el + ' .pie').css('clip', 'rect(0, 1em, 1em, 0.5em)');
        $(el + ' .right-side').css('transform', 'rotate(0deg)');
    }
    if (donut) {
        $(el + ' .right-side').css('border-width', '0.1em');
        $(el + ' .left-side').css('border-width', '0.1em');
        $(el + ' .shadow').css('border-width', '0.1em');
    } else {
        $(el + ' .right-side').css('border-width', '0.5em');
        $(el + ' .left-side').css('border-width', '0.5em');
        $(el + ' .shadow').css('border-width', '0.5em');
    }
    $(el + ' .num').text(percent);
    $(el + ' .left-side').css('transform', 'rotate(' + deg + 'deg)');
}

$('.inside-btn-clear').click(function () {
    $(this).parent('div.input-inside-wrap').children('input[type="text"]').val('');
});

//Updated Focus to error fields
function errorField(fieldId) {
    var field = $(fieldId);

    field.attr({
        'data-toggle': 'tooltip',
        'title': 'This field cannot be left blank'
    }).tooltip('show');
    field.on('shown.bs.tooltip', function () {
        $('.tooltip').addClass('animated bounce');
    })
    setTimeout(function () {
        field.removeAttr('data-toggle title').tooltip('destroy');
    }, 2000);
}



function highlightRow(object, color) {
    var originalColor = $(object).css('background-color');
    //$(object).fadeOut('10').css('color', color).fadeIn('10').fadeOut('10').css('color', originalColor).fadeIn('10');
    //$(object).css('background-color', color);
    //setTimeout(function () { $(object).css('background-color', originalColor); }, 500);
    $(object).addClass('animated shake');
    setTimeout(function () { $(object).removeClass('animated'); $(object).removeClass('shake'); }, 500);


}

//Success Alert
function successAlert(msg) {
    var message = "Saved Successfully";
    if (msg != null) {
        message = msg;
    }

    $.Notification.autoHide('black', 'bottom right', 'Success', message);

}

//Error Alert
function errorAlert(msg) {
    var message = 'Something went wrong. Try again';
    if (msg != null) {
        message = msg;
    }
    $.Notification.autoHide('error', 'bottom right', 'Alert!', message);


    //swal({
    //    title: '',
    //    text: message,
    //    type: 'error',
    //});

}

//Reset Form
function reset() {
    $('form')[0].reset();
}

//Delete Master
function deleteMaster(options) {

    var url = options.url;
    var id = options.id;
    var successMessage = options.successMessage;
    var successFunction = options.successFunction;
    var modifiedBy = options.modifiedBy;

    swal({
        title: "Delete?",
        text: "Are you sure you want to delete?",
        showConfirmButton: true, closeOnConfirm: true,
        showCancelButton: true,

        cancelButtonText: "No",
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Delete"
    },
    function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: url + id,
                method: 'DELETE',
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                data: JSON.stringify(modifiedBy),
                success: function (response) {

                    if (response.Success) {
                        successFunction();
                        successAlert(response.Message)

                    }
                    else {
                        errorAlert(response.Message);
                    }
                },
                error: function (xhr, err, status) {
                    alert(xhr.responseText);
                    console.log(xhr);
                }
            });
        }
    }

                    );
}

//Trim string to specified length

function trimIt(string, length) {
    var newString = '';
    if (string.length < length + 3) {
        return string;
    }
    else {
        for (var i = 0; i < length - 3; i++) {
            newString += string[i];
        }
        newString += '...';
        return newString;
    }
}



function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}


function lookup(options) {
    
    var keyword = options.keyword;
    var visibility = options.visibility;
    var url = options.url;
    var ddl1 = $('#' + options.ddl1Id);
    var ddl1Param = options.ddl1Param;
    var ddl2Param = options.ddl2Param;
    var ddl2 = $('#' + options.ddl2Id);
    var textBox = $('#' + options.textBoxId);
    var lookupDiv = $('#' + options.lookupDivId);
    var storage = options.storageKey;
    var focusTo = $('#' + options.focusToId);
    var heads = options.heads;
    var alias = options.alias;
    var key = options.key;
    var dataToShow = options.dataToShow;

    var headColor = options.headColor == undefined ? '#00BCD4' : options.headColor;
    textBox.off().on('keyup', function (e) {

        $('body').one('click', function () { lookupDiv.children().remove(); })

        if (e.which == 27) {
            lookupDiv.children().remove();
        }
        else if (e.which == 40) {
           //stop doing anything if user presses up arrow
        }
        else if (e.which == 38) {
            //stop doing anything if user presses down arrow
        }
        else if (e.which == 13) {
            var row = $('.focused-tr');
            var cells = row.children('td');
            var item = {};
            for (var l = 0; l < cells.length; l++) {
                item[heads[l]] = $(cells[l]).text();
            }
            sessionStorage.setItem(storage, JSON.stringify(item));
            lookupDiv.children().remove();
            textBox.val(item[dataToShow]);
            if (textBox.val() != '' && textBox.val() != null && textBox != undefined) {
                focusTo.focus();
                try{

                options.OnSelection(item);
                }
                catch (err) {
                    console.log(err);
                }

            }
        }

        else {
            if (textBox.val() != '' && textBox.val() != null && textBox != undefined) {
                function buildUrl() {
                    var x = '';
                    if (options.ddl1Param != undefined && options.ddl1Param != null) {
                        if (ddl1.val() == '0') {
                            try {
                                options.onNullParameter();
                            }
                            catch (error) {

                            }
                        }
                        x += '&' + ddl1Param + '=' + ddl1.val();
                    }
                    if (options.ddl2Id != undefined && options.ddl2Id != null) {
                        if (ddl2.val() == '0') {
                            try {
                                options.onNullParameter();
                            }
                            catch (error) {

                            }
                        }
                        x += '&' + ddl2Param + '=' + ddl2.val();
                    }
                    return x;
                }
                var keyword = $(this).val();
                if (theXRequest) { theXRequest.abort() } // If there is an existing XHR, abort it.
                clearTimeout(xhrTimer); // Clear the timer so we don't end up with dupes.
                xhrTimer = setTimeout(function () { // assign timer a new timeout 
                    theXRequest = $.ajax({
                        url: url + keyword + buildUrl(),
                        method: 'GET',
                        dataType: 'JSON',
                        success: function (items) {
                            lookupDiv.children().remove();
                            html = '<table style="position: absolute; z-index: 9; background-color: white; box-shadow: 0 0 10px 3px rgba(0, 0, 0, .1);" class="table table-hover table-bordered table-condensed"><thead style="background-color: ' + headColor + ' !important; display: table; width: 100%; table-layout: fixed"><tr>';
                            for (var k = 0; k < heads.length; k++) {
                                if (visibility[k] == false) {
                                    html += '<th style="display:none ">' + alias[k] + '</th>';
                                }
                                else {
                                    html += '<th style="color: white; font-size: smaller;">' + alias[k] + '</th>';
                                }

                            }
                            html += '</tr></thead><tbody style="display: block; max-height: 37vh;height:100%; overflow:auto;">';

                            for (var i = 0; i < items.length; i++) {
                                html += '<tr  style="display: table; width: 100%; table-layout: fixed" class="lookupTr">';
                                for (var j = 0; j < heads.length; j++) {
                                    
                                    if (visibility[j] == false) {
                                        html += '<td style="display:none">' + items[i][heads[j]] + '</td>';
                                    }
                                    else {
                                        html += '<td>' + items[i][heads[j]] + '</td>';
                                    }
                                }
                                html += '</tr>';
                            }
                            html += '</tbody></table>';
                            var lookupObject = $.parseHTML(html);
                            if (items.length > 0) {
                                lookupDiv.append(lookupObject);
                            }
                            else {
                                try {
                                    options.OnZeroResults();
                                }
                                catch (error) {

                                }
                            }
                            lookupDiv.css({
                                'position': 'absolute',
                                'width': '250%'
                            });
                            //Binding Event to the newly generated lookup rows
                            lookupDiv.off().on('click', '.lookupTr', function () {
                                var row = $(this);
                                var cells = row.children('td');
                                var item = {};
                                for (var l = 0; l < cells.length; l++) {
                                    item[heads[l]] = $(cells[l]).text();
                                }
                                sessionStorage.setItem(storage, JSON.stringify(item));
                                lookupDiv.children().remove();
                                textBox.val(item[dataToShow]);
                                focusTo.focus();
                                try {

                                    options.OnSelection(item);
                                }
                                catch (err) {
                                    console.log(err);
                                }
                            });


                        },
                        error: function (xhr) {
                            console.log(xhr.responseText);
                            try {
                                options.OnComplete();
                            }
                            catch (error) {
                                console.log(error);
                            }
                        },
                        beforeSend: function () {
                            try{
                                options.OnLoading();
                            }
                            catch (error) {
                                console.log(error);
                            }
                        },
                        complete: function () {
                            try {
                                options.OnComplete();
                            }
                            catch (error) {
                                console.log(error);
                            }
                        }

                    });
                }, 500); // delay, tweak for faster/slower

            }
        }


    });

    textBox.keydown(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
        else if (e.which == 40 || e.keycode==40) {
            var hasFocused = false;
            var allRows = lookupDiv.children('table').children('tbody').children('tr');

            $(allRows).each(function () {
                if ($(this).hasClass('focused-tr')) {
                    hasFocused = true;

                }
            });


            if (!hasFocused) {
                allRows.eq(0).css({ 'color': '#00BCD4', 'background-color': 'lightgray' });
                allRows.eq(0).addClass('focused-tr');
                var scrollPosition = $('.focused-tr').height() * $('.focused-tr').index() - (150);
                $('#' + options.lookupDivId + ' tbody').scrollTop(scrollPosition);
            }
            else {


                var focusedTr = $('.focused-tr');
                focusedTr.next().css({ 'color': '#00BCD4', 'background-color': 'lightgray' });
                focusedTr.next().addClass('focused-tr');
                focusedTr.css({ 'color': 'inherit', 'background-color': 'inherit' });
                focusedTr.removeClass('focused-tr');
                var scrollPosition = $('.focused-tr').height() * $('.focused-tr').index() - (150);
                $('#' + options.lookupDivId + ' tbody').scrollTop(scrollPosition);
            }
        }
        else if (e.which == 38 || e.keycode == 38) {
            var hasFocused = false;
            var allRows = lookupDiv.children('table').children('tbody').children('tr');

            $(allRows).each(function () {
                if ($(this).hasClass('focused-tr')) {
                    hasFocused = true;

                }
            });


            if (!hasFocused) {
                allRows.eq(0).css({ 'color': '#00BCD4', 'background-color': 'lightgray' });
                allRows.eq(0).addClass('focused-tr');
                var scrollPosition = $('.focused-tr').height() * $('.focused-tr').index() - (150);
                $('#' + options.lookupDivId + ' tbody').scrollTop(scrollPosition);
            }
            else {
                var focusedTr = $('.focused-tr');
                focusedTr.prev().css({ 'color': '#00BCD4', 'background-color': 'lightgray' });
                focusedTr.prev().addClass('focused-tr');
                focusedTr.css({ 'color': 'inherit', 'background-color': 'inherit' });
                focusedTr.removeClass('focused-tr');
                var scrollPosition = $('.focused-tr').height() * $('.focused-tr').index() - (150);
                $('#' + options.lookupDivId + ' tbody').scrollTop(scrollPosition);
            }
        }
    })

}

//Full screen API
function launchFullScreen(element) {
    if (element.requestFullscreen) {
        element.requestFullscreen();
    } else if (element.mozRequestFullScreen) {
        element.mozRequestFullScreen();
    } else if (element.webkitRequestFullscreen) {
        element.webkitRequestFullscreen();
    } else if (element.msRequestFullscreen) {
        element.msRequestFullscreen();
    }
}
//close full screen
function exitFullscreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
    }
}