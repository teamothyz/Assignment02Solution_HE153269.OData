function send(url, method, data, callback) {
    //const securityToken = $('input[name="__RequestVerificationToken"]').val();
    const token = $.cookie('AccessToken');
    $.ajax({
        url: url,
        data: data,
        headers: {
            //"RequestVerificationToken": securityToken,
            'Authorization': 'Bearer ' + token
        },
        cache: false,
        processData: false,
        type: method,
        contentType: 'application/json',
        success: function (result) {
            callback(result);
        },
        error: function (err) {
            console.error(err.toString());
            callback('error');
        }
    });
}

function submitForm(relativeUrl, method) {
    var url = 'http://localhost:5137/' + relativeUrl;
    var formData = convertFormToJSON('#my-form');
    send(url, method, formData, commonCallback);
}

function convertFormToJSON(form) {
    return JSON.stringify($(form)
        .serializeArray()
        .reduce(function (json, { name, value }) {
            if (name != '__RequestVerificationToken') {
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