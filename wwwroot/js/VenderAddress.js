var dataTable;

$(document).ready(function () {
    loadDataTable();
});

var queryString = window.location.search;
let matches = queryString.match(/(\d+)/);

var number = matches[0];

var id = parseInt(number);
console.log(AddressID);


function loadDataTable() {
    dataTable = $('#tblData').DataTable({

        "ajax": { url: '/Vendor/GetAllAddress?VendorAddressID=' + id },

                   

        "columns": [
            { data: 'addressLine1', "width": "10%" },
            { data: 'addressLine2', "width": "10%" },
            { data: 'landMark', "width": "10%" },
            { data: 'cityID', "width": "10%" },
            { data: 'vendorID', "width": "10%" },
            { data: 'pinCode', "width": "10%" },
         
            {
                data: 'addressID',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/Vendor/upsert?AddressID=${data}"  class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a onClick=Delete('/Vendor/DeleteAddress?id=${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>Delete
                            </a>
                          
                   </div>
                    `
                },
                "width": "15%"
            }


        ]

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


//<a href="/Vendor/Details?id=${data}" class="btn btn-primary mx-2">
//    <i class="bi bi-eye"></i>Details
//</a>