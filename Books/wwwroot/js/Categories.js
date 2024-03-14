let categoriesTable;
$(document).ready(function () {
    categoriesTable = $('#CategoryTable').DataTable({

        "ajax": {
            "url" : "/Admin/Category/GetCategories"
        },
        "columns": [

            { "data": "name" },
            {
                "data": "categoryId",
                "render": function (data) {

                    return `
                        <a class="btn btn-primary" href="/Admin/Category/EditCategory?id=${data}">Edit</a>
                        <a class="btn btn-danger" onclick=RemoveCategory("/Admin/Category/Delete/${data}")>Delete</a>
                    `

                }

            }


        ]



    });
});



function RemoveCategory(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                url: url,
                type: 'DELETE',
                success: function (data) {

                    if (data.success) {
                        categoriesTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }

                }

            })


        }
    });

}