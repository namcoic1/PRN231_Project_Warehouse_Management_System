
var content;

$body = $("body");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    $('#message').html('');

    if (!localStorage.getItem('token')) {
        content = `<form id="loginForm">
        <h5 class="text-center mb-3" style="font-weight:bold; color:blue;">Warehouse Login</h5>
        <p id="message"></p>
        <div class="mb-2">
            <label for="inputName" class="form-label">Username</label>
            <input type="text" class="form-control" id="inputName" placeholder="username" required>
        </div>
        <div class="mb-2">
            <label for="inputPassword" class="form-label">Password</label>
            <div class="d-flex">
                <input type="password" class="form-control" id="inputPassword" placeholder="password" required>
                <span id="toggle-password" toggle="#password-field" class="fa fa-fw fa-eye field_icon"></span>
            </div>
        </div>
        <div class="mb-2 form-check">
            <input type="checkbox" class="form-check-input remember" id="inputCheck" value="checked">
            <label class="form-check-label" for="inputCheck">Remember me</label>
        </div>
        <button type="submit" id="button-submit" class="btn btn-primary">Login</button>
    </form>`;
    }
    else {
        content = ``;
        window.location.href = "/Error";
    }
    $('.login-content').html(content);

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
        beforeSend: function (request) {
            $('.modals').show();
        },
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
                    }, 2000);
                }
                else {
                    $('#message').html('Username or password is not valid.');
                    $('#message').css("color", "red");
                    setTimeout(function () {
                        window.location.reload(true);
                    }, 2000);
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
                    }, 2000);
                }
                else {
                    $('#message').html('Username or password is not valid.');
                    $('#message').css("color", "red");
                    setTimeout(function () {
                        window.location.reload(true);
                    }, 2000);
                }
            }
        },
        complete: function () {
            setTimeout(function () {
                $('.modals').hide();
            }, 2000);
        },
        error: function (xhr, status, error) {
        }
    });
}