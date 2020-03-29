$(document).ready(function () {
    $(".excluirCliente").click(function (e) {
        var resultado = confirm("Deseja realmente Excluir o cliente?");
        if (resultado == false) {
            e.preventDefault();
        }


    });



});