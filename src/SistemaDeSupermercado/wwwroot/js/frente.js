
var enderecoProduto = "https://localhost:44341/Produto/Pesquisar/";
var produto;
var compra = [];


//Funcoes
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

function zerarFormulario() {
    $("#campoNome").val("");
    $("#campoCategoria").val("");
    $("#campoFornecedor").val("");
    $("#campoPreco").val("");
    $("#campoQuantidade").val("");
}

function preencherFormulario(dadosDoProduto) {
    $("#campoNome").val(dadosDoProduto.nome);
    $("#campoCategoria").val(dadosDoProduto.categoria.nome);
    $("#campoFornecedor").val(dadosDoProduto.fornecedor.nome);
    $("#campoPreco").val(dadosDoProduto.precoDeVenda);
}

function adicionarNaTable(prod, quant) {
    var produtoTemp = {};

    Object.assign(produtoTemp, produto);

    compra.push(produtoTemp)

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