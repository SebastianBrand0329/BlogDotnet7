var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblCategory").DataTable({
        "ajax": {
            "url": "/Admin/Categorys/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" }, // Utiliza "Id" en lugar de "id"
            { "data": "name", "width": "30%" }, // Utiliza "Name" en lugar de "name"
            { "data": "order", "width": "20%" }, // Utiliza "Order" en lugar de "order"
            {
                "data": "id", // Utiliza "Id" en lugar de "id"
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/Admin/Categorys/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:150px;">
                            <i class="far fa-edit"></i>Editar
                            </a>
                            &nbsp;
                            <a onclick=Delete("/Admin/Categorys/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                            <i class="far fa-trash-alt"></i>Borrar
                            </a>
                        </div>
                        `;
                }, "width": "30%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 de 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}