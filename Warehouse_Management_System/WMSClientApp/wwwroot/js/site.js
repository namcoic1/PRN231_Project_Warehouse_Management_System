// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// start write js below

var nav_admin = $('#nav-item-admin');
var nav_employee = $('#nav-item-employee');
var btn_login = $('#button-login-logout');
var lastDate = new Date();
nav_admin.html('');
nav_employee.html('');

//load navi by role
$(document).ready(function () {
    if (localStorage.getItem('token')) {
        showNavi();
    }
});

//show or hide navi
//$(window).on('load', function () {
//$('.dropdown').hover(function () {
//    $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
//}, function () {
//    $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
//});
//});

//reload page when no action and refresh token
$(document.body).bind("mousemove keypress", function (e) {
    lastDate = new Date();
});
if (localStorage.getItem('token')) {
    setInterval(refresh, 1000);
}

function showNavi() {
    var token = JSON.parse(localStorage.getItem('token')).token;
    $.ajax({
        url: 'https://localhost:7210/api/Users/Role',
        type: 'GET',
        dataType: 'json',
        beforeSend: function (request) {
            request.setRequestHeader('Authorization', 'Bearer ' + token);
        },
        success: function (response) {
            var content;

            if (response.role === 'ADMIN') {
                content = `<div class="dropdown">
                                <a class="nav-link text-dark dropdown-toggle" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
                                    Admin Management
                                </a>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                    <li><a class="dropdown-item" href="/Admin/RoleIndex">Role</a></li>
                                    <li><a class="dropdown-item" href="/Admin/UserIndex">User</a></li>
                                    <li><a class="dropdown-item" href="/Admin/CategoryIndex">Category</a></li>
                                    <li><a class="dropdown-item" href="/Admin/SupplierIndex">Supplier</a></li>
                                    <li><a class="dropdown-item" href="/Admin/CustomerIndex">Customer</a></li>
                                    <li><a class="dropdown-item" href="/Admin/CarrierIndex">Carrier</a></li>
                                    <li><a class="dropdown-item" href="/Admin/LocationIndex">Location</a></li>
                                    <li><a class="dropdown-item" href="/Admin/ProductIndex">Product</a></li>
                                    <li><a class="dropdown-item" href="/Admin/InventoryIndex">Inventory</a></li>
                                    <li><a class="dropdown-item" href="/Admin/TransactionIndex">Transaction</a></li>
                                    <li><a class="dropdown-item" href="/Admin/ReportIndex">Report</a></li>
                                </ul>
                            </div>`;
                nav_admin.html(content);
            } else if (response.role === 'EMPLOYEE') {
                content = `<div class="dropdown">
                                <a class="nav-link text-dark dropdown-toggle" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
                                    Employee Management
                                </a>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                    <li><a class="dropdown-item" href="/Employees/LocationIndex">Location</a></li>
                                    <li><a class="dropdown-item" href="/Employees/TransactionIndex">Transaction</a></li>
                                </ul>
                                    </div>`;
                nav_employee.html(content);
            }
            localStorage.setItem("role", response.role);
            btn_login.html(`<a id="logout" onclick="logout()" class="text-white btn btn-secondary">Logout</a>`);
        },
        error: function (xhr, status, error) {
        }
    });
}

function logout() {
    clearInterval(refresh);
    localStorage.removeItem('role');
    localStorage.removeItem('token');

    var user = JSON.parse(localStorage.getItem('userInfo'));
    var remember = user.Remember;
    if (remember !== 'checked') {
        localStorage.removeItem('userInfo');
    }

    btn_login.html(`<a id="login" href="/Login" class="btn btn-primary">Login</a>`);
    window.location.href = '/Login';
}

function refresh() {
    var currentDate = new Date();
    var sessionObject = JSON.parse(localStorage.getItem('token'));
    if (sessionObject) {
        var expirationDate = sessionObject.expiresAt;
        if (Date.parse(currentDate) > Date.parse(expirationDate)) {
            $.ajax({
                url: "https://localhost:7210/api/Users/Authenticate",
                type: "POST",
                contentType: "application/json",
                data: localStorage.getItem('userInfo'),
                success: function (result, status, xhr) {
                    //console.log(result);
                    sessionObject.expiresAt = result.validTo;
                    if (result.refreshToken) {
                        sessionObject.token = result.refreshToken;
                        localStorage.setItem("token", JSON.stringify(sessionObject));
                    }
                    else {
                        sessionObject.token = result.token;
                        localStorage.setItem("token", JSON.stringify(sessionObject));
                    }
                },
                error: function (xhr, status, error) {
                }
            });
        }
        else if (currentDate.getTime() - lastDate.getTime() >= 120000) {
            window.location.reload(true);
        }
    }
}