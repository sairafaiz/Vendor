var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({

        "ajax": { url: '/Vendor/GetAll' },

                   

        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'email', "width": "10%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'notes', "width": "10%" },
         
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/Vendor/upsert?id=${data}"  class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a onClick=Delete('/Vendor/Delete?id=${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>Delete
                            </a>
                            <a href="/Vendor/Details?id=${data}"  class="btn btn-primary mx-2">
                                <i class="bi bi-eye"></i>Details
                            </a>
                   </div>
                    `
                },
                "width": "15%"
            }


        ],

    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}