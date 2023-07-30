
var title = $('#list-title');
var content = $('#list-content');

// load list reports by role admin
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'ADMIN') {
        title.html(`<h4 class="my-4">List Reports</h4>`);
        content.html(`<div class="d-flex my-2">
        <div>
            <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
            <span class="btn-sm btn-primary columns" data-column="0">Report Id</span>
            <!--<span class="btn-sm btn-primary columns" data-column="1">User</span>
            <span class="btn-sm btn-primary columns" data-column="2">Inventory</span>
            <span class="btn-sm btn-primary columns" data-column="3">Transaction</span>-->
            <span class="btn-sm btn-primary columns" data-column="4">Report Name</span>
            <span class="btn-sm btn-primary columns" data-column="5">Description</span>
            <span class="btn-sm btn-primary columns" data-column="6">Type</span>
            <!--<span class="btn-sm btn-primary columns" data-column="7">Report Date</span>
            <span class="btn-sm btn-primary columns" data-column="8">Last Modified</span>-->
        </div>
        <div class="ms-auto">
            <span id="btn-add" class="btn-sm btn-primary button-actions">Add new +</span>
            <span id="btn-export" class="btn-sm btn-primary button-actions">Export Reports</span>
        </div>
    </div>
    <table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <th>Report Id</th>
                <th>User</th>
                <th>Inventory</th>
                <th>Transaction</th>
                <th>Report Name</th>
                <th>Description</th>
                <th>Type</th>
                <th>Report Date</th>
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

    listReports();

    $('#btn-add').click(function () {
        console.log(this.textContent);
    });
    
    $('#btn-export').click(function () {
        console.log(this.textContent);
    });
});

function listReports() {
    $.ajax({
        url: 'https://localhost:7210/odata/Reports?$expand=User,Transaction',
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
                        data: 'InventoryID'
                    },
                    {
                        data: 'Transaction.TransactionType',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'green';
                                if (data === 'OUT') {
                                    color = 'red';
                                }
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'Name',
                    },
                    { data: 'Description' },
                    {
                        data: 'ReportType',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'red';
                                if (data === 'TRANS') {
                                    color = 'orange';
                                }
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'ReportDate' },
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
                    api.columns([0, 1, 2, 3, 4, 5, 6, 7, 8])
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

function showModalDelete(id) {
    console.log(id);
}