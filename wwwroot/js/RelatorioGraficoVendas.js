function CarregarGrafico() {
    var ano = $("#selectAno").val();
    var dataDe = "01/01/" + ano;
    var dataAte = "01/12/" + ano;

    $(function () {
        $.ajax({
            contentType: "application/json",
            type: "GET",
            url: "/Relatorios/RelatorioGraficoVendasCarregado",
            data: { "dataDe": dataDe.toString(), "dataAte": dataAte.toString() },
            success:
                function (data) {
                    $("#canvasGrafico").remove();
                    $("#divGrafico").append('<canvas class="line-chart" id="canvasGrafico"></canvas>');                 
                    var ctx = $(".line-chart");
                    var chartGraph = new Chart(ctx, {
                        type: "bar",
                        data: {
                            labels: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],

                            datasets: [
                                {
                                    label: "Ano de " + ano,
                                    data: data,
                                    backgroundColor: "rgba(0,102,255, 0.5)"
                                }

                            ]
                        }

                    });
                }
        });
    });


    
}