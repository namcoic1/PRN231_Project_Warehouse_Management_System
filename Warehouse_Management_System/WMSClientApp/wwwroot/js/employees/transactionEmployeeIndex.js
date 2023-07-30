
var title = $('#list-title');
var content = $('#list-content');

// load list transactions by role employee
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'EMPLOYEE') {
        title.html(`<h4 class="my-4">List Transactions</h4>`);
        var head = `<div class="d-flex my-2">
        <div>
            <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
            <span class="btn-sm btn-primary columns" data-column="0">Id</span>
            <!--<span class="btn-sm btn-primary columns" data-column="1">User</span>
            <span class="btn-sm btn-primary columns" data-column="2">Supplier</span>
            <span class="btn-sm btn-primary columns" data-column="3">Customer</span>
            <span class="btn-sm btn-primary columns" data-column="4">Location</span>
            <span class="btn-sm btn-primary columns" data-column="5">Product</span>
            <span class="btn-sm btn-primary columns" data-column="6">Carrier</span>-->
            <span class="btn-sm btn-primary columns" data-column="7">Quantity</span>
            <span class="btn-sm btn-primary columns" data-column="8">Freight</span>
            <span class="btn-sm btn-primary columns" data-column="9">Address</span>
            <!--<span class="btn-sm btn-primary columns" data-column="10">Postal Code</span>-->
            <span class="btn-sm btn-primary columns" data-column="11">Type</span>
                       <!--<span class="btn-sm btn-primary columns" data-column="12">Start Date</span>
            <span class="btn-sm btn-primary columns" data-column="13">Last Modified</span>-->
        </div>
        <div class="ms-auto">
            <span id="btn-add" class="btn-sm btn-primary button-actions">Add new +</span>
            <span id="btn-export" class="btn-sm btn-primary button-actions">Export Transactions</span>
        </div>
    </div>
    <table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <th>Id</th>
                <th>User</th>
                <th>Supplier</th>
                <th>Customer</th>
                <th>Location</th>
                <th>Product</th>
                <th>Carrier</th>
                <th>Quantity</th>
                <th>Freight</th>
                <th>Address</th>
                <th>Postal Code</th>
                <th>Type</th>
                <th>Start Date</th>
                <th>Last Modified</th>
                <th>
                    #
                </th>`;
        var distinguish = localStorage.getItem('role') === 'ADMIN' ? `<th>
                    #
                </th>` : ``;
        var body = `</tr>
        </thead>
    </table>`;
        content.html(head + distinguish + body);
    }

    listTransactions();

    $('#btn-add').click(function () {
        console.log(this.textContent);
    });

    $('#btn-export').click(function () {
        console.log(this.textContent);
    });
});

function listTransactions() {
    $.ajax({
        url: 'https://localhost:7210/api/Transactions/GetAllTransactionsByUser',
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
                        data: 'user.fullName',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'green';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        },
                        visible: false  
                    },
                    {
                        data: 'supplier.name'
                    },
                    {
                        data: 'customer.name',
                    },
                    {
                        data: 'location.name'
                    },
                    {
                        data: 'product.name'
                    },
                    {
                        data: 'carrier.name',
                    },
                    {
                        data: 'quantity',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'blue';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'freight',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'red';
                                return `<span style="color:${color}">$${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'address' },
                    {
                        data: 'postalCode',
                    },
                    {
                        data: 'transactionType',
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
                        data: 'transactionDate',
                    },
                    { data: 'lastModified' },
                    {
                        data: '',
                        searchable: false,
                        orderable: false,
                        render: function (data, type, row) {
                            return `<span id="btn-update" class= "btn-sm btn-secondary button-actions" onclick="showModalEdit(${row.id})">Update</span>`;
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