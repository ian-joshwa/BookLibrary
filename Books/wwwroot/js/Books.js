var bookTable;

$(document).ready(function () {

    bookTable = $('#BookTable').DataTable({

        "ajax": {
            "url": "/Admin/Book/GetBooks"
        },
        "columns": [

            { "data": "bookName" },
            { "data": "publishYear" },
            { "data": "coverPrice" },
            { "data": "category.name" },
            {
                "data": "status",
                "render": function (data) {
                    if (data) {
                        return "Available";
                    }
                    else {
                        return "Not Available";
                    }
                }
            },
            {
                "data": "bookId",
                "render": function (data) {
                    return `
                        <a class="btn btn-primary" href="/Admin/Book/EditBook?id=${data}">Edit</a>
                        <a class="btn btn-danger" onclick=RemoveBook("/Admin/Book/Delete/${data}")>Delete</a>
                    `
                }
            }

        ]


    });

});


function RemoveBook(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        $.ajax({

            url: url,
            type: 'DELETE',
            success: function (data) {

                if (data.success) {
                    bookTable.ajax.reload();
                    toastr.success(data.message);
                }
                else {
                    toastr.error(data.message);
                }

            }


        });


    });


}