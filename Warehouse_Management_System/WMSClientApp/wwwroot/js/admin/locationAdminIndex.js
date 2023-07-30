
var title = $('#list-title');
var content = $('#list-content');

// load list locations by role admin
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'ADMIN') {
        title.html(`<h4 class="my-4">List Locations</h4>`);
        var head = `<div class="d-flex my-2">
        <div>
            <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
            <span class="btn-sm btn-primary columns" data-column="0">Location Id</span>
            <!--<span class="btn-sm btn-primary columns" data-column="1">User</span>
            <span class="btn-sm btn-primary columns" data-column="2">Location Name</span>
            <span class="btn-sm btn-primary columns" data-column="3">Capacity</span>-->
            <span class="btn-sm btn-primary columns" data-column="4">Address</span>
            <!--<span class="btn-sm btn-primary columns" data-column="5">Postal Code</span>-->
            <span class="btn-sm btn-primary columns" data-column="6">Contact</span>
            <!--<span class="btn-sm btn-primary columns" data-column="7">Last Modified</span>-->
        </div>`;
        var distinguish = localStorage.getItem('role') === 'ADMIN' ? `<div class="ms-auto">
            <span id="btn-add" class="btn-sm btn-primary button-actions">Add new +</span>
            <!--<span class="btn-sm btn-primary button-actions">Export Locations</span>-->
        </div></div>` : `</div>`;
        var body = `<table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <th>Location Id</th>
                <th>User</th>
                <th>Location Name</th>
                <th>Capacity</th>
                <th>Address</th>
                <th>Postal Code</th>
                <th>Contact</th>
                <th>Last Modified</th>
                <th>
                    #
                </th>
            </tr>
        </thead>
    </table>`;
        content.html(head + distinguish + body);
    }

    listLocations();

    $('#btn-add').click(function () {
        console.log(this.textContent);
    });
});

function listLocations() {
    $.ajax({
        url: 'https://localhost:7210/odata/Locations?$expand=User',
        type: 'GET',
        beforeSend: function (request) {
            $('.progress').show();
            $('.preLoaderBar').show();
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (result, status, xhr) {
            var table = $('#myTable').DataTable({
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
                        data: 'User.FullName',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'green';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'Name',
                    },
                    {
                        data: 'Capacity',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'green';
                                if (data > 1000) {
                                    color = 'orange';
                                }
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'Address', },
                    { data: 'PostalCode', },
                    { data: 'ContactNumber', },
                    { data: 'LastModified', },
                    {
                        data: '',
                        searchable: false,
                        orderable: false,
                        render: function (data, type, row) {
                            return `<span id"btn-update" class= "btn-sm btn-secondary button-actions" onclick="showModalEdit(${row.Id})">Update</span>`;
                        }
                    }
                ],
                processing: true,
                lengthMenu: [
                    [5, 10, 25, 50, 100, -1],
                    [5, 10, 25, 50, 100, 'All']
                ],
                scrollX: true,
                scrollCollapse: true,
                pagingType: 'full_numbers',
                initComplete: function () {
                    let api = this.api();
                    api.on('click', 'tbody td', function () {
                        if (this.textContent !== "Update") {
                            api.search(this.textContent).draw();
                        }
                    });
                    api.columns([0, 1, 2, 3, 4, 5, 6, 7])
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

function showModalEdit(id) {
    console.log(id);
}
