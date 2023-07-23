
var title = $('#list-title');
var content = $('#list-content');
// load list users by role admin
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'ADMIN') {
        title.html(`<h4 class="my-4">List Users</h4>`);
        content.html(`<div class="d-flex my-2">
        <div>
            <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
            <span class="btn-sm btn-primary columns" data-column="0">Id</span>
            <!--<span class="btn-sm btn-primary columns" data-column="1">Role</span>
            <span class="btn-sm btn-primary columns" data-column="2">Name</span>
            <span class="btn-sm btn-primary columns" data-column="3">Title</span>-->
            <span class="btn-sm btn-primary columns" data-column="4">Gender</span>
            <!--<span class="btn-sm btn-primary columns" data-column="5">BirthDate</span>
            <span class="btn-sm btn-primary columns" data-column="6">HireDate</span>-->
            <span class="btn-sm btn-primary columns" data-column="7">Address</span>
            <span class="btn-sm btn-primary columns" data-column="8">Contact</span>
            <span class="btn-sm btn-primary columns" data-column="9">Notes</span>
            <span class="btn-sm btn-primary columns" data-column="10">Picture</span>
            <!--<span class="btn-sm btn-primary columns" data-column="11">Manager</span>
            <span class="btn-sm btn-primary columns" data-column="12">Status</span>
            <span class="btn-sm btn-primary columns" data-column="13">Last Modified</span>-->
        </div>
        <div class="ms-auto">
            <span id="btn-add" class="btn-sm btn-primary button-actions">Add new +</span>
            <!--<span class="btn-sm btn-primary button-actions">Export Users</span>-->
        </div>
    </div>
    <table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <th>Id</th>
                <th>Role</th>
                <th>Name</th>
                <th>Title</th>
                <th>Gender</th>
                <th>BirthDate</th>
                <th>HireDate</th>
                <th>Address</th>
                <th>Contact</th>
                <th>Notes</th>
                <th>Picture</th>
                <th>Manager</th>
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

    listUsers();

    $('#btn-add').click(function () {
        console.log(this.textContent);
    });

    // make events
    //$('#myTable tbody').on('click', 'td', function () {
    //    alert('Clicked on: ' + this.innerHTML);
    //});
});

function listUsers() {
    $.ajax({
        url: 'https://localhost:7210/api/Users/GetAllUsersByAdmin',
        //url: 'https://localhost:7210/odata/Users?expand=Role,Manager,Location',
        type: 'GET',
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (result, status, xhr) {
            var table = $('#myTable').DataTable({
                //dataSrc: '',
                //dataSrc: 'value',
                data: result,
                columns: [
                    {
                        data: 'id',
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
                        data: 'role.name',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'green';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'fullName'
                    },
                    {
                        data: 'title'
                    },
                    {
                        data: 'gender',
                        render: function (data, type) {
                            if (type === 'display') {
                                let gender = 'FEMALE';
                                let color = '#FF1493';
                                if (data === 1) {
                                    gender = 'MALE';
                                    color = 'blue';
                                }
                                data = gender;
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'birthDate' },
                    { data: 'hireDate' },
                    { data: 'address' },
                    { data: 'contactNumber' },
                    { data: 'notes' },
                    { data: 'picture' },
                    {
                        data: 'manager.fullName',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'red';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'status',
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
                    { data: 'lastModified' },
                    {
                        searchable: false,
                        orderable: false,
                        render: function (data, type) {
                            return `<span id="btn-update" class= "btn-sm btn-secondary button-actions">Update</span>`;
                        }
                    },
                    {
                        searchable: false,
                        orderable: false,
                        render: function (data, type) {
                            return `<span id="btn-delete" class= "btn-sm btn-danger button-actions">Delete</span>`;
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
                    api.columns([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
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
        error: function (xhr, status, error) {
            if (xhr.status === 401 || xhr.status === 403 || xhr.status === 404) {
                window.location.href = "/Error";
            }
        }
    });
}