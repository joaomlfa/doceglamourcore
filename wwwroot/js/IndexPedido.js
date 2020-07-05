var excluirProduto = $(".excluirPedido");

$(document).ready(function () {
    $("#tabelaPedidos").DataTable({
        "retrieve": true,
        "paging": false,
        "info": false

    });
});

$(excluirProduto).on("click", function () {
    var resultado = confirm("Deseja Realmente Excluir o Pedido?");
    if (resultado) {

    }
    else {
        event.preventDefault();
    }
});
