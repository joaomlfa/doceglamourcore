var excluirProduto = $(".excluirProduto");

$(document).ready(function () {
    $("#tabelaProdutos").DataTable({
        "retrieve": true,
        "paging": false,
        "info": false

    });
});

$(excluirProduto).on("click", function () {
    var resultado = confirm("Deseja Realmente Excluir o Produto?");
    if (resultado) {

    }
    else {
        event.preventDefault();
    }
});

