var dataTable

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#myTable").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            {"data": "title", "width": "15%"},
            {"data": "isbn", "width": "15%"},
            {"data": "price", "width": "15%"},
            {"data": "author", "width": "15%"},
            {"data": "category.name", "width": "15%"},
            {
                "data": "id", "render": function (data) {
                    return `
                        <a class="text-green-500" href="/Admin/Product/Upsert?id=${data}" >Edit</a>
                        <a class="text-red-500" >Remove</a>
                           `
                }
            }
        ]
    });
}