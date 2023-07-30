
var title = $('#list-title');
var content = $('#list-content');
var table = $('#myTable').DataTable();

// load list categories by role admin
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'ADMIN') {
        title.html(`<h4 class="my-4">List Categories</h4>`);
        content.html(`<div class="d-flex my-2">
        <div>
            <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
            <span class="btn-sm btn-primary columns" data-column="0">Category Id</span>
            <!--<span class="btn-sm btn-primary columns" data-column="1">Category Name</span>
            <span class="btn-sm btn-primary columns" data-column="2">Status</span>
            <span class="btn-sm btn-primary columns" data-column="3">Last Modified</span>-->
        </div>
        <div class="ms-auto">
            <span id="btn-add" class="btn-sm btn-primary button-actions">Add new +</span>
        </div>
    </div>
    <table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <th>Category Id</th>
                <th>Category Name</th>
                <th>Status</th>
                <th>Last Modified</th>
                <th>
                    #
                </th>
                <th>
                    #
                </th>
            </tr>
        </thead>
    </table>`);
    }

    listCategories();

    $('#btn-add').click(function () {
        // clear items
        $('#add-name').val(null);
        $('#add-status').val('true');
        //$('span.text-danger.small').empty();
        // show modal
        $("#add-category-modal").modal("show");
    });
});

function listCategories() {
    $.ajax({
        url: 'https://localhost:7210/odata/Categories',
        type: 'GET',
        beforeSend: function (request) {
            $('.progress').show();
            $('.preLoaderBar').show();
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (result, status, xhr) {
            table = $('#myTable').DataTable({
                data: result.value,
                columns: [
                    {
                        data: 'Id',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = '#00BFFF';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        },
                        visible: false
                    },
                    {
                        data: 'Name'
                    },
                    {
                        data: 'Status',
                        render: function (data, type) {
                            if (type === 'display') {
                                let status = 'BLOCK';
                                let color = 'red';
                                if (data === true) {
                                    status = 'ACTIVE';
                                    color = 'green';
                                }
                                data = status;
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'LastModified' },
                    {
                        data: '',
                        searchable: false,
                        orderable: false,
                        render: function (data, type, row) {
                            return `<span id="btn-update" class= "btn-sm btn-secondary button-actions" onclick="showModalEdit(${row.Id})">Update</span>`;
                        }
                    },
                    {
                        data: '',
                        searchable: false,
                        orderable: false,
                        render: function (data, type, row) {
                            return `<span id="btn-delete" class= "btn-sm btn-danger button-actions" onclick="showModalDelete(${row.Id})">Delete</span>`;
                        }
                    }
                ],
                processing: true,
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [
                    [5, 10, 25, 50, 100, -1],
                    [5, 10, 25, 50, 100, 'All']
                ],
                pagingType: 'full_numbers',
                initComplete: function () {
                    let api = this.api();
                    api.on('click', 'tbody td', function () {
                        if (this.textContent !== "Update" && this.textContent !== "Delete") {
                            api.search(this.textContent).draw();
                        }
                    });
                    api.columns([0, 1, 2, 3])
                        .every(function () {
                            let column = this;
                            let title = column.header().textContent;
                            let input = document.createElement('input');
                            input.placeholder = title;
                            column.header().append(input);
                            input.addEventListener('keyup', () => {
                                if (column.search() !== this.value) {
                                    column.search(input.value).draw();
                                }
                            });
                        });
                }
            });

            $('.columns').each(function (index, column) {
                $(column).click(function (e) {
                    e.preventDefault();
                    let columnIdx = e.target.getAttribute('data-column');
                    let column = table.column(columnIdx);
                    column.visible(!column.visible());
                });
            });
        },
        complete: function () {
            $('.progress').hide();
            $('.preLoaderBar').hide();
        },
        error: function (xhr, status, error) {
            if (xhr.status === 401 || xhr.status === 403 || xhr.status === 404) {
                window.location.href = "/Error";
            }
        }
    });
}

function checkNullInput(selector, error) {
    if ($(selector).val().length === 0) {
        if (selector === '#add-upw') {
            $(selector).parent().siblings('.text-danger').text(error);
        } else if (selector === '#confirm-upw') {
            $(selector).parent().siblings('.text-danger').text(error);
        } else {
            $(selector).siblings('.text-danger').text(error);
        }
    } else {
        if (selector === '#add-upw') {
            $(selector).parent().siblings('.text-danger').text('');
        } else if (selector === '#confirm-upw') {
            $(selector).parent().siblings('.text-danger').text('');
        } else {
            $(selector).siblings('.text-danger').text('');
        }
    }
}

function modalAddSubmitForm() {
    // Validation Input
    //checkNullInput("#add-name", 'Full name must be not null or empty');

    var error = false;
    //$('span.text-danger.small').each(function () {
    //    if ($(this).text().trim() !== '') {
    //        error = true;
    //        return;
    //    }
    //});

    if (error === false) {
        var data_category = {
            name: $("#add-name").val(),
            status: $("#add-status").val(),
            lastModified: new Date()
        };
    }

    $.ajax({
        url: "https://localhost:7210/odata/Categories",
        type: "POST",
        data: JSON.stringify(data_category),
        contentType: "application/json",
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (response) {
            $('#add-message').html('');
            $('#add-message').html(`Add category (${data_category.name}) successfully.`);
            $('#add-message').css('color', 'green');
        },
        error: function (error) {
            $('#add-message').html('');
            $('#add-message').html(`Add category (${data_category.name}) fail.`);
            $('#add-message').css('color', 'red');
        }
    });
}

function showModalEdit(id) {
    $.ajax({
        url: `https://localhost:7210/odata/Categories(${id})`,
        type: "GET",
        contentType: "application/json;",
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (result, status, xhr) {
            // set data to view
            $('#edit-id').val(result.Id);
            $('#edit-name').val(result.Name);
            $('#edit-status').val(`${result.Status}`);
            // show modal
            $('#edit-category-modal').modal('show');
        },
        error: function (xhr, status, error) {
        }
    });
}

function modalEditSubmitForm() {
    var data_category = {
        id: $('#edit-id').val(),
        name: $("#edit-name").val(),
        status: $("#edit-status").val(),
        lastModified: new Date()
    };

    $.ajax({
        url: `https://localhost:7210/odata/Categories(${data_category.id})`,
        type: "PUT",
        data: JSON.stringify(data_category),
        contentType: "application/json",
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (response) {
            $('#update-message').html('');
            $('#update-message').html(`Edit category (${data_category.name}) successfully.`);
            $('#update-message').css('color', 'green');
        },
        error: function (error) {
            $('#update-message').html('');
            $('#update-message').html(`Edit category (${data_category.name}) fail.`);
            $('#update-message').css('color', 'red');
        }
    });
}

function showModalDelete(id) {
    $.ajax({
        url: `https://localhost:7210/odata/Categories(${id})`,
        type: "GET",
        contentType: "application/json;",
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (result, status, xhr) {
            // set data to view
            $('#id').html(result.Id);
            $('#name').html(`'${result.Name}'`);
            // show modal
            $('#delete-category-modal').modal('show');
        },
        error: function (xhr, status, error) {
        }
    });
}

function modalDeleteSubmit() {
    var id = $('#id').html();
    var name = $('#name').html();

    $.ajax({
        url: `https://localhost:7210/odata/Categories(${id})`,
        type: "DELETE",
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (response) {
            $('#delete-message').html('');
            $('#delete-message').html(`Delete category (${name}) successfully.`);
            $('#delete-message').css('color', 'green');
        },
        error: function (error) {
            $('#delete-message').html('');
            $('#delete-message').html(`Delete category (${name}) fail.`);
            $('#delete-message').css('color', 'red');
        }
    });
}
