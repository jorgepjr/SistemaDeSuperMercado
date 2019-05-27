//Declaração de variáveis
var enderecoProduto = "https://localhost:44341/Produto/Pesquisar/";
var produto;
var compra = [];
var __totalVenda__ = 0.0;

//Início
atualizarTotal();

$("#posvenda").hide();
//Funcoes
function atualizarTotal() {
    $("#finalizarCompra").click(function () {
        $("#totalVenda").html(__totalVenda__);
    });
}

function preencherFormulario(dadosDoProduto) {
    $("#campoNome").val(dadosDoProduto.nome);
    $("#campoCategoria").val(dadosDoProduto.categoria.nome);
    $("#campoFornecedor").val(dadosDoProduto.fornecedor.nome);
    $("#campoPreco").val(dadosDoProduto.precoDeVenda);
}

function zerarFormulario() {
    $("#campoNome").val("");
    $("#campoCategoria").val("");
    $("#campoFornecedor").val("");
    $("#campoPreco").val("");
    $("#campoQuantidade").val("");
}

function adicionarNaTable(prod, quant) {
    var produtoTemp = {};

    Object.assign(produtoTemp, produto);

    var venda = { produto: produtoTemp, quantidade: quant, subtotal: produtoTemp.precoDeVenda * quant };
    __totalVenda__ += venda.subtotal;

    compra.push(venda);

    $("#vendas").append(`
<tr>
<td>${prod.id}</td>
<td>${prod.nome}</td>
<td>${quant}</td>
<td>${prod.precoDeVenda}</td>
<td>${prod.medicao}</td>
<td>${prod.precoDeVenda * quant}</td>
<td><button class="btn btn-danger">Remover</button</td>
</tr>
`);
}

$("#produtoForm").on("submit", function (event) {
    event.preventDefault();
    var produtoParaTable = produto;
    var qtd = $("#campoQuantidade").val();

    console.log(produtoParaTable);
    console.log(qtd);

    //var produto = undefined;
    adicionarNaTable(produtoParaTable, qtd);
    zerarFormulario();
});


//Ajax
$("#pesquisar").click(function () {
    var codProduto = $("#codProduto").val();
    var enderecoTemporario = enderecoProduto + codProduto;
    $.post(enderecoTemporario, function (dados, status) {
        produto = dados;

        var med = "";
        switch (produto.medicao) {
            case 0:
                med = "K";
                break;
            case 1:
                med = "M";
                break;
            case 2:
                med = "U";
                break;
            default:
                med = "U";
                break;
        }
        produto.medicao = med;
        preencherFormulario(produto);

    }).fail(function () {
        alert("Produto inválido!");
    });
});

//Finalização da venda

$("#finalizarVenda").click(function () {
    if (__totalVenda__ <= 0) {
        alert("Compra inválida, nenhum produto adicionado!");
        return;
    }
    var _valorPago = $("#valorPago").val();
    console.log(typeof _valorPago);
    if (!isNaN(_valorPago)) {// não é um número
        _valorPago = parseFloat(_valorPago);
        if (_valorPago >= __totalVenda__) {
            $("#posvenda").show();
            $("#prevenda").hide();
            $("#valorPago").prop("disabled", true);
            var _troco = _valorPago - __totalVenda__;
            $("#troco").val(_troco);

            //Converter Array de obj produto em array Ids
            compra.forEach(elemento => {
                elemento.produto = elemento.produto.id;
            });

            //Enviar dados para o back-end

        } else {
            alert("valor pago muito baixo!");
            return;
        }
    } else {
        alert("Valor pago inválido!");
        return;
    }
});

function restaurarModal() {
    $("#posvenda").hide();
    $("#prevenda").show();
    $("#valorPago").prop("disabled", false);
    $("#troco").val("");
    $("#valorPago").val("");
}

$("#fecharModal").click(function () {
    restaurarModal();
});