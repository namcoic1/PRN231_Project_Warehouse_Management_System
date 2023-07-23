
$(document).ready(function () {

    $('#message').html('');

    // save info user if remember
    if (localStorage.getItem('userInfo')) {
        var user = JSON.parse(localStorage.getItem('userInfo'));

        if (user.Remember === 'checked') {
            $("#inputName").val(user.UserName);
            $("#inputPassword").val(user.Password);
            $('#inputCheck').prop('checked', true);
        }
        else {
            $('#inputCheck').removeProp('checked');
        }
    }

    // show or hide password
    $('#toggle-password').click(function (e) {
        var password = $('#inputPassword');
        $(this).toggleClass("fa-eye fa-eye-slash");
        password.attr('type') === 'password' ? password.attr('type', 'text') : password.attr('type', 'password')
    });

    // authen when login
    $("#loginForm").submit(function (e) {
        e.preventDefault();

        var username = $("#inputName").val();
        var password = $("#inputPassword").val();
        var remember = $('.remember:checked').val();
        var userInfo = {
            UserName: username,
            Password: password,
            Remember: remember === undefined ? 'notchecked' : remember
        };

        getToken(userInfo);
    });
});

function getToken(userInfo) {
    $.ajax({
        url: "https://localhost:7210/api/Users/Authenticate",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(userInfo),
        success: function (result, status, xhr) {
            var sessionObject = {
                expiresAt: result.validTo
            };

            if (result.refreshToken) {
                sessionObject.token = result.refreshToken;
                if (result.refreshToken !== '') {
                    localStorage.setItem("token", JSON.stringify(sessionObject));
                    localStorage.setItem("userInfo", JSON.stringify(userInfo));
                    $('#message').html('Login successfully.');
                    $('#message').css("color", "green");
                    setTimeout(function () {
                        window.location.href = '/Main';
                    }, 1000);
                }
                else {
                    $('#message').html('Username or password is not valid.');
                    $('#message').css("color", "red");
                    setTimeout(function () {
                        window.location.reload(true);
                    }, 1000);
                }
            }
            else {
                sessionObject.token = result.token;
                if (result.token !== '') {
                    localStorage.setItem("token", JSON.stringify(sessionObject));
                    localStorage.setItem("userInfo", JSON.stringify(userInfo));
                    $('#message').html('Login successfully.');
                    $('#message').css("color", "green");
                    setTimeout(function () {
                        window.location.href = '/Main';
                    }, 1000);
                }
                else {
                    $('#message').html('Username or password is not valid.');
                    $('#message').css("color", "red");
                    setTimeout(function () {
                        window.location.reload(true);
                    }, 1000);
                }
            }
        },
        error: function (xhr, status, error) {
        }
    });
}