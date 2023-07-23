
var title = $('#list-title');
var content = $('#list-content');
// load list inventories by role admin
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'ADMIN') {
        title.html(`<h4 class="my-4">List Inventories</h4>`);
        content.html(`<div class="d-flex my-2">
        <div>
            <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
            <span class="btn-sm btn-primary columns" data-column="0">Inventory Id</span>
            <!--<span class="btn-sm btn-primary columns" data-column="1">Location</span>
            <span class="btn-sm btn-primary columns" data-column="2">Product</span>
            <span class="btn-sm btn-primary columns" data-column="3">Quantity</span>
            <span class="btn-sm btn-primary columns" data-column="4">Price</span>-->
            <span class="btn-sm btn-primary columns" data-column="5">Description</span>
            <!--<span class="btn-sm btn-primary columns" data-column="6">Status</span>
            <span class="btn-sm btn-primary columns" data-column="7">LastModified</span>-->
        </div>
        <div class="ms-auto">
            <span id="btn-add" class="btn-sm btn-primary button-actions">Add new +</span>
            <span id="btn-export" class="btn-sm btn-primary button-actions">Export Inventories</span>
        </div>
    </div>
    <table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <th>Inventory Id</th>
                <th>Location</th>
                <th>Product</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Description</th>
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

    listInventories();

    $('#btn-add').click(function () {
        console.log(this.textContent);
    });

    $('#btn-export').click(function () {
        console.log(this.textContent);
    });

    // make events
    //$('#myTable tbody').on('click', 'td', function () {
    //    alert('Clicked on: ' + this.innerHTML);
    //});
});

function listInventories() {
    $.ajax({
        url: 'https://localhost:7210/odata/Inventories?$expand=Location,Product',
        type: 'GET',
        beforeSend: function (request) {
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
                        data: 'Location.Name',
                    },
                    {
                        data: 'Product.Name',
                    },
                    {
                        data: 'Quantity',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'blue';
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'Price',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'red';
                                return `<span style="color:${color}">$${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'Description' },
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
        error: function (xhr, status, error) {
            if (xhr.status === 401 || xhr.status === 403 || xhr.status === 404) {
                window.location.href = "/Error";
            }
        }
    });
}