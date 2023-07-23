
var title = $('#list-title');
var content = $('#list-content');
// load list roles by role admin
$(document).ready(function () {

    title.html('');
    content.html('');

    if (localStorage.getItem('token') && localStorage.getItem('role') === 'ADMIN') {
        title.html(`<h4 class="my-4">List Roles</h4>`);
        content.html(`<!--<table id="myTable" class="display compact nowrap row-border cell-border" style="width:100%" >
    <div id="chart-output" style="margin-bottom: 1em;" class="chart-display"></div>-->
    <div class="py-2">
        <span style="color:green">Show</span>/<span style="color:red">Hidden</span> columns:
        <span class="btn-sm btn-primary columns" data-column="0">Role Id</span>
        <!--<span class="btn-sm btn-primary columns" data-column="1">Role Name</span>
        <span class="btn-sm btn-primary columns" data-column="2">Last Modified</span>-->
    </div>
    <table id="myTable" class="table table-bordered table-striped table-hover table-responsive" style="width:100%">
        <thead>
            <tr>
                <!--<th>#</th>-->
                <th>Role Id</th>
                <th>Role Name</th>
                <th>Last Modified</th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>`);
    }

    listRoles();
});

function listRoles() {
    $.ajax({
        url: 'https://localhost:7210/odata/Roles',
        type: 'GET',
        beforeSend: function (request) {
            if (localStorage.getItem('token')) {
                request.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.getItem('token')).token);
            }
        },
        success: function (result, status, xhr) {
            var table = $('#myTable').DataTable({
                data: result.value,
                // setting columns for table
                columns: [
                    //{
                    //    //index
                    //    searchable: false,
                    //    orderable: false,
                    //    targets: 0
                    //},
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
                        data: 'Name',
                        render: function (data, type) {
                            if (type === 'display') {
                                let color = 'red';
                                if (data == 'EMPLOYEE') {
                                    color = 'green';
                                }
                                return `<span style="color:${color}">${data}</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'LastModified' }
                ],
                //order: [[1, 'asc']],
                //// seting options for table
                //deferRender: true,
                //select: true,
                //scrollX: true,
                //scrollY: '?px',
                //scrollCollapse: true,
                //serverSide: true,
                //deferLoading: 1,
                processing: true,
                ordering: true,
                lengthChange: true,
                searching: true,
                lengthMenu: [
                    [5, 10, 25, 50, 100, -1],
                    [5, 10, 25, 50, 100, 'All']
                ],
                paging: true,
                pagingType: 'full_numbers',
                // search single column in table
                initComplete: function () {
                    let api = this.api();
                    // search when click tds in table
                    api.on('click', 'tbody td', function () {
                        //api.search(this.innerHTML).draw();
                        api.search(this.textContent).draw();
                    });
                    api.columns()
                        .every(function () {
                            let column = this;
                            let title = column.header().textContent;
                            // Create input element
                            let input = document.createElement('input');
                            input.placeholder = title;
                            //column.header().replaceChildren(input);
                            column.header().append(input);
                            // events when user input
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