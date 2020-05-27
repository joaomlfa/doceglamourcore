
var cliente;
var clienteJSON;
var intens = new Object();
intens.produtos = new Array();
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
        $(function(){
            $.ajax({
                contentType: "application/json",
                type: "GET",
                url: "/Pedido/BuscarClienteID",
                dataType: "json",
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

function AdicionarProduto(quantidade) {
    var produtoSelect = $("#produtos option:selected").val();
    $(function () {
        $.ajax({
            contentType: "application/json",
            type: "GET",
            url: "/Pedido/BuscarProdutoID",
            dataType: "json",
            success:
                
                function (data) {
                    data = JSON.parse(data);
                    $("#tabelaProduto tbody").append(
                        "<tr>"+
                            "<td>" + data.codigo_produto + "</td>" +
                            "<td>" + data.nome + "</td>" +
                        "<td data-valor='"+data.valor+"'>" + data.valor + "</td>" +
                            "<td>" + quantidade + "</td>" +
                        "<td>" + "<button type='button' class='btn btn-danger btn-sm ExcluirProduto' onclick='ExcluirProduto(this)'>Excluir</button>"  + "</td>"+
                        "</tr>"
                    
                    
                    );
                    var valor = data.valor;
                    var valorRemanescente = $("#ValorTotalInput").val();
                    if (valorRemanescente == "") {
                        $("#ValorTotalInput").val(valor * quantidade);
                        
                    } else {
                        var convertido = parseFloat(valorRemanescente);
                        valor = convertido + (valor * quantidade);
                        $("#ValorTotalInput").val(valor.toFixed(2));
                    }
            }
        })
  
    });
    
 
}

function Verifica() {
    var quantidade = $("#quantidade").val();
    if (quantidade == "") {
        alert("Insira uma Quantidade ANALISE");
    } else {
        AdicionarProduto(quantidade);    
    }
}
$(":button").on("click", function () {
    $("#modalExemplo").modal('hide');
   
});

function ExcluirProduto(item) {
    var tr = $(item).closest("tr"); //Busca a linha mais próxima do objeto passado pelo botão
    var valor = $(item).closest("tr").find("td[data-valor]").data("valor"); // busca a linha mais proxima, e a coluna com atributo data-valor (onde está o valor do produto)
    var valortotal = $("#ValorTotalInput").val(); //busca o valor total do pedido no momento
    var valorFinal = valortotal - valor; //remove o valor do produto da linha removida
    $("#ValorTotalInput").val(valorFinal.toFixed(2)); //insere o novo valor total
    tr.fadeOut(400, function () {
        tr.remove(); //remove a linha
    })
   
}

function somenteNumeros(num) { // função que permite apenas Números. Inserir no Input no argumento Onkeyup = "somenteNumeros(this)"
    var er = /[^0-9.,]/;
    er.lastIndex = 0;
    var campo = num;
    if (er.test(campo.value)) {
        campo.value = "";
    }
}

$("#DescontoInput").change(function () {
    var valorAtual = $("#ValorTotalInput").val();
    var valorDesconto = $("#DescontoInput").val().replace(",",".");
    var valorFinal = valorAtual - valorDesconto;
    if (valorFinal < 0) {
        Swal.fire(
            'Ops!!',
            'Valor do Desconto maior que o Valor do Pedido',
            'error'

        );
    } else {
        $("#ValorTotalInput").val(valorFinal.toFixed(2));
    }
});

$("#salvarpedido").click(function () {
    var pedido = new Object();
    pedido.Pedidos = new Array();
    var tema = $("#tema").val();
    var nomecrianca = $("#crianca").val();
    var idadeCrianca = $("#idade").val();
    var dataentrega = $("#dataentrega").val();
    var datapedido = new Date();
    var taxaentrega = $("#taxa").val();
    var observacoes = $("#obs").val();
    var valortotal = $("#ValorTotalInput").val();
    var desconto = $("#DescontoInput").val();
    var valorsinal = $("#ValorSinalInput").val();
    var statuspedido = $("#status").val();
    var cliente = $("#clienteJSON").val();
    
    pedido.Pedidos.push({
        "cliente": [
            cliente
        ],
        "tema": tema,
        "valor_total": valortotal,
        "nome_crianca": nomecrianca,
        "idadecrianca": idadeCrianca,
        "data_pedido": datapedido.getUTCDate,
        "valor_sinal": valorsinal,
        "taxa_entrega": taxaentrega,
        "desconto": desconto,
        "obs": observacoes,
        "status": statuspedido
    });
    var pedidoSTRING = JSON.stringify(pedido.Pedidos);
    $.ajax({
        type: "POST",
        url: "/Pedido/AdicionarPedido",
        data: { pedido: pedidoSTRING },
        success: function (data) {

        }
    });
});