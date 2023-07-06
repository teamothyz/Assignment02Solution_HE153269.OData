function send(url, method, data, callback) {
    var token = $.cookie('AccessToken');
    var event = $('#submit-btn').attr('onclick');
    $('#submit-btn').attr('onclick', '');

    $.ajax({
        url: url,
        data: data,
        headers: { 'Authorization': 'Bearer ' + token },
        cache: false,
        processData: false,
        type: method,
        contentType: 'application/json',

        success: (result) => {
            callback(result);
            $('#submit-btn').attr('onclick', event);
        },

        error: (err) => {
            console.error(err.toString());
            callback('error');
            $('#submit-btn').attr('onclick', event);
        }
    });
}

function sendDelete(relativeUrl, eventSelector, removeSelector) {
    if (!confirm('Bạn chắc chắn muốn xóa?')) return;

    var token = $.cookie('AccessToken');
    var url = 'http://localhost:5137/' + relativeUrl;
    var onclickEvent = $(eventSelector).attr('onclick');
    $(eventSelector).attr('onclick', '');

    $.ajax({
        url: url,
        headers: { 'Authorization': 'Bearer ' + token },
        cache: false,
        processData: false,
        type: 'DELETE',

        success: (result) => {
            console.log(result);
            $(removeSelector).remove();
            $('#error').addClass('d-none');
            $('#success').removeClass('d-none');
        },

        error: (err) => {
            console.error(err.toString());
            $(eventSelector).attr('onclick', onclickEvent);
            $('#error').removeClass('d-none');
            $('#success').addClass('d-none');
        }
    });
}

function submitForm(relativeUrl, method) {
    var url = 'http://localhost:5137/' + relativeUrl;
    var formData = convertFormToJSON('#my-form');
    send(url, method, formData, commonCallback);
}

function convertFormToJSON(form) {
    return JSON.stringify($(form).serializeArray()
        .reduce(function (json, { name, value }) {
            if (name == '__RequestVerificationToken') {
                return json;
            }
            var type = $('[name="' + name + '"]').attr('type');
            if (type === 'date') {
                var dateValue = new Date(value);
                value = dateValue.toISOString().replace('.000Z', 'Z');
            }
            if (type === 'number') {
                json[name] = parseFloat(value);
            }
            else {
                json[name] = value;
            }
            return json;
        }, {}));
}

function commonCallback(response) {
    if (response == "error") {
        $('#error').removeClass('d-none');
        $('#success').addClass('d-none');
    }
    else {
        $('#error').addClass('d-none');
        $('#success').removeClass('d-none');
    }
}

function commonDeleteCallback(selector) {
    $(selector).remove();
}