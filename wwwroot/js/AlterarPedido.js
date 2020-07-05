var cliente;
var clienteJSON;
var valorTaxaAtual;
var valorDescontoAtual;
var clienteAtual;
var produtoAtual = JSON.parse($("#produtoPedidoJSON").val());
var codigoPedido = parseFloat($("#codigoPedido").val());
var produtoAtualList = new Array();
var produtoPedido = new Array();
var delay = 1000;

produtoAtualList = produtoAtual;
produtoPedido = produtoAtual;


$(document).ready(function () {
    

    if ($("#clienteJSON").val() == "null") {
        $("#cliente").val("SEM CLIENTE")
    } else {
        clienteAtual = JSON.parse($("#clienteJSON").val());
        $("#cliente").val(clienteAtual.nome);
    }
    produtoAtualList.forEach(function (produtos) {
        var codigoProduto = produtos.codigo_produto;
        if ($("#taxa").val() == "") {
            valorTaxaAtual = 0;
        } else {
            valorTaxaAtual = parseFloat($("#taxa").val().replace(",", "."));          
        }
        if ($("#DescontoInput").val() == "") {
            valorDescontoAtual = 0;
        } else {
            valorDescontoAtual = parseFloat($("#DescontoInput").val().replace(",", "."));
            
        }
        
      
        $.ajax({
            contentType: "application/json",
            type: "GET",
            url: "/Pedido/BuscarProdutoID",
            data: { "idProduto": codigoProduto },
            success:
                function (data) {
                    data = JSON.parse(data);
                    $("#tabelaProduto tbody").append(
                        "<tr>" +
                        "<td data-codigo='"+ data.codigo_produto +"'>" + data.codigo_produto + "</td>" +
                        "<td>" + data.nome + "</td>" +
                        "<td data-valor='" + produtos.valor_final + "'> " + produtos.valor_final + " </td>" +
                        "<td data-quantidade='" + produtos.quantidade + "'> " + produtos.quantidade + " </td>" +
                        "<td>" + "<button type='button' class='btn btn-danger btn-sm ExcluirProduto' onclick='ExcluirProduto(this)'>Excluir</button>" + "</td>" +
                        "</tr>"


                    );

                }
        });

    });
});

function AdicionarCliente(contador) {
    $("#tabelaCliente td#nomeCliente").each(function (index, value) { //Percorre e obtem todas as colunas "Nome" da tabela
        if (index == contador - 1) { //obtem a linha onde o index for igual ao contador passado por parametro


            $("#cliente").val(value.textContent);
            $("#cliente").attr("disabled", true);
        } else {

        }
        $("#tabelaCliente td#codigoCliente").each(function (index, value) { //Percorre e obtem todas as colunas "Código" da tabela
            if (index == contador - 1) { //obtem a linha onde o index for igual ao contador passado por parametro
                cliente = value.textContent; //insere o código do cliente selecionado na variável global Cliente



            }
        });
        $(function () {
            $.ajax({
                contentType: "application/json",
                type: "GET",
                url: "/Pedido/BuscarClienteID",
                data: { "idCliente": cliente},
                success:
                    function (data) {
                        data = JSON.parse(data);
                        var clienteSTRING = JSON.stringify(data);
                        clienteJSON = clienteSTRING.replace("\n", "*");
                        $("#clienteJSON").val(clienteJSON);


                    }

            });


        })
    });

}

function AdicionarProduto(quantidade) { //Adiciona Produto ao Pedido

    var produtoSelect = $("#produtos option:selected").val();
    var repete = false;

    for (var i = 0; i < produtoPedido.length; i++) {      
        if (produtoPedido[i].codigo_produto == produtoSelect) {
            repete = true;
           
        } else {

        }
    }

    if (repete) {

        for (var i = 0; i < produtoPedido.length; i++) {
            var quantidadeConvertido = parseFloat(quantidade).toFixed(2);
            var quantidadeAtual = parseFloat(produtoPedido[i].quantidade).toFixed(2);            
            var quantidadeFinal = parseInt(quantidadeConvertido) + parseInt(quantidadeAtual);
            
            var valorProduto = produtoPedido[i].valor_final;
            var valorTotalAtual = parseFloat($("#ValorTotalInput").val().replace(",", "."));
            var valorTotalNovo = (valorTotalAtual - (quantidadeAtual * valorProduto)) + (valorProduto * quantidadeFinal);
            produtoPedido[i].quantidade = quantidadeFinal;
            $("#tabelaProduto tbody tr").each(function (index, value) {
                if (index == i) {
                    $(value).find("td[data-quantidade]").html(quantidadeFinal);
                    $(value).find("td[data-quantidade]").attr("data-quantidade", quantidadeFinal);
                    


                }
            });
            $("#ValorTotalInput").val(valorTotalNovo.toFixed(2).replace(".", ","));
            $("#produtoPedidoJSON").val(JSON.stringify(produtoPedido));
            
        }
        
    } else {
        $(function () {

            $.ajax({
                contentType: "application/json",
                type: "GET",
                url: "/Pedido/BuscarProdutoID",
                data: { "idProduto": produtoSelect },
                success:

                    function (data) {
                        data = JSON.parse(data);


                        $("#tabelaProduto tbody").append(
                            "<tr>" +
                            "<td data-codigo='" + data.codigo_produto + "'>" + data.codigo_produto + "</td>" +
                            "<td>" + data.nome + "</td>" +
                            "<td data-valor='" + data.valor + "'> " + data.valor + " </td>" +
                            "<td data-quantidade='" + quantidade + "'> " + quantidade + " </td>" +
                            "<td>" + "<button type='button' class='btn btn-danger btn-sm ExcluirProduto' onclick='ExcluirProduto(this)'>Excluir</button>" + "</td>" +
                            "</tr>"


                        );
                        var valor = data.valor;
                        var valorRemanescente = $("#ValorTotalInput").val();
                        if (valorRemanescente == "") {
                            $("#ValorTotalInput").val((valor * quantidade).toFixed(2).replace(".", ","));

                        } else {
                            var valorRemanescenteConvertido = parseFloat($("#ValorTotalInput").val().replace(",", "."));
                            valor = valorRemanescenteConvertido + (valor * quantidade);
                            $("#ValorTotalInput").val(valor.toFixed(2).replace(".", ","));
                        }
                        produtoPedido.push({
                            "codigo_pedido": codigoPedido,
                            "codigo_produto": produtoSelect,
                            "quantidade": quantidade,
                            "valor_final": data.valor
                        });
                        $("#produtoPedidoJSON").val(JSON.stringify(produtoPedido));

                    }
            })

        });
    }

  


}

function Verifica() { //Verifica se foi passado uma quantidade de produto a ser inserido no sistema.
    var quantidade = $("#quantidade").val();
    if (quantidade == "") {
        alert("Insira uma Quantidade ANALISE");
    } else {
        AdicionarProduto(quantidade);
    }
}
$(":button").on("click", function () { //Fecha o modal de clientes
    $("#modalExemplo").modal('hide');

});

function ExcluirProduto(item) {
    var tr = $(item).closest("tr"); //Busca a linha mais próxima do objeto passado pelo botão
    var valor = $(item).closest("tr").find("td[data-valor]").data("valor"); // busca a linha mais proxima, e a coluna com atributo data-valor (onde está o valor do produto)
    var quantidade = $(item).closest("tr").find("td[data-quantidade]").data("quantidade"); //busca a quantidade de produto inserido, indo até a coluna mais proxima com o atributo data-quantidade
    var valorTotalProduto = valor * quantidade; // multiplica o valor do produto pela quantidade inserida
    var valortotalconvertido = parseFloat($("#ValorTotalInput").val().replace(",", ".")); //Converte o que está no campo Valor Total em Float e troca a virgula por ponto.
    var valorFinal = valortotalconvertido.toFixed(2) - valorTotalProduto.toFixed(2); //subtrai o valor do produto pelo valor total do pedido
    $("#ValorTotalInput").val(valorFinal.toFixed(2).replace(".", ",")); //insere o novo valor total
    tr.fadeOut(400, function () {
        tr.remove(); //remove a linha
    });
    setTimeout(function () {
        AtualizarJsonProdutos();
    },delay);

}

function somenteNumeros(num) { // função que permite apenas Números. Inserir no Input no argumento Onkeyup = "somenteNumeros(this)"
    var er = /[^0-9,]/;
    er.lastIndex = 0;
    var campo = num;
    if (er.test(campo.value)) {
        campo.value = "";
    }
}

$("#DescontoInput").change(function () { //Verifica se o valor do desconto não foi maior que o valor do pedido.

    var valorDesconto = $("#DescontoInput").val().replace(",", ".");
    var valorAtual = $("#ValorTotalInput").val().replace(",", ".");
    if (valorDesconto == "") {
        var valorAtualConvertido = parseFloat(valorAtual);
        var valorFinal = valorAtualConvertido + valorDescontoAtual;
        $("#ValorTotalInput").val(valorFinal.toFixed(2).replace(".", ","));
        valorDescontoAtual = 0;
    } else {
        var valorAtualConvertido = parseFloat(valorAtual);
        var valorDescontoConvertido = parseFloat(valorDesconto);
        var valorFinal = valorAtualConvertido + valorDescontoAtual;
        valorFinal = valorFinal - valorDescontoConvertido;
       
        
        if (valorFinal < 0) {
            Swal.fire(
                'Ops!!',
                'Valor do Desconto maior que o Valor do Pedido',
                'error'

            );
            $("#DescontoInput").val(valorDescontoAtual.toFixed(2).replace(".", ","));
        } else {
            $("#ValorTotalInput").val(valorFinal.toFixed(2).replace(".", ","));
            valorDescontoAtual = valorDescontoConvertido;
        }
    }
    
    
});


$("#taxa").change(function () { // Método responsável por ler alterações no campo Taxa de entrega
    var valorTotal = $("#ValorTotalInput").val();
    var valorTaxaEditada = $("#taxa").val();
    if (valorTaxaEditada == "") {
        if (valorTotal == "") {
            valorTaxaAtual = 0;
            $("#ValorTotalInput").val(valorTaxaAtual);
        } else {
            
            var valorTotalConvertido = parseFloat($("#ValorTotalInput").val().replace(",", "."));
            var valorfinal = valorTotalConvertido - valorTaxaAtual;
            $("#ValorTotalInput").val(valorfinal.toFixed(2).replace(".", ","));
            valorTaxaAtual = 0;
        }

    } else {
        if (valorTotal == "") {
            valorTaxaAtual = parseFloat($("#taxa").val().replace(",", "."));
            $("#ValorTotalInput").val(valorTaxaAtual);
        } else {
            valorTaxaAtual = parseFloat($("#taxa").val().replace(",", "."));
            var valorTotalConvertido = parseFloat($("#ValorTotalInput").val().replace(",", "."));
            var valorfinal = valorTotalConvertido + valorTaxaAtual;
            $("#ValorTotalInput").val(valorfinal.toFixed(2).replace(".", ","));
            
        }
    }


    
});

function AtualizarJsonProdutos() { // Função atualiza o Json de produtos ao excluir um produto, percorrendo a tabela novamente e refazendo o mesmo.
    produtoPedido = [];
    $("#tabelaProduto tbody tr").each(function () {
        var colunas = $(this).find("td");
        var produtosAtualizados = {
            "codigo_pedido": codigoPedido,
            "codigo_produto": colunas[0].innerHTML,
            "quantidade": parseInt(colunas[3].innerHTML),
            "valor_final": parseFloat(colunas[2].innerHTML)
        };
        produtoPedido.push(produtosAtualizados);
        
    });
    $("#produtoPedidoJSON").val(JSON.stringify(produtoPedido));
    
}
