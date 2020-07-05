var botaoExcluir = $(".excluirCliente");

$(document).ready(function () {
    $("#tabelaClientes").DataTable({
        "retrieve": true,
        "paging": false,
        "info": false

    });
});

$(botaoExcluir).on("click", function (event) {
    var resultado = confirm("Deseja Realmente Excluir o Cliente?");
    if (resultado) {
        
    }
    else
    {
        event.preventDefault();
    }

});


