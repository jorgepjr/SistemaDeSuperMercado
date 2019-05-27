using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;

namespace SistemaDeSupermercado.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IToastNotification toastMessage;

        public ProdutoController(IToastNotification toastMessage, ApplicationDbContext context)
        {
            this.toastMessage = toastMessage;
            this.context = context;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDto produtoDto)
        {
            if (ModelState.IsValid)
            {
                var produto = new Produto
                {
                    Nome = produtoDto.Nome,
                    Categoria = context.Categoria.First(categoria => categoria.Id == produtoDto.CategoriaId),
                    Fornecedor = context.Fornecedor.First(fornecedor => fornecedor.Id == produtoDto.FornecedorId),
                    PrecoDeCusto = float.Parse(produtoDto.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat),
                    PrecoDeVenda = float.Parse(produtoDto.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat),
                    Medicao = produtoDto.Medicao,
                    Status = true
                };

                context.Produto.Add(produto);
                context.SaveChanges();
                toastMessage.AddSuccessToastMessage("Produto cadastrado com sucesso!");
                return RedirectToAction("ListaDeProdutos", "Gestao");
            }

            else
            {
                ViewBag.Categorias = context.Categoria.ToList();
                ViewBag.Fornecedores = context.Fornecedor.ToList();

                return View("../Gestao/NovoProduto");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(ProdutoDto produtoDto)
        {
            if (ModelState.IsValid)
            {
                var produto = context.Produto.First(x => x.Id == produtoDto.Id);
                produto.Nome = produtoDto.Nome;
                produto.Categoria = context.Categoria.First(categoria => categoria.Id == produtoDto.CategoriaId);
                produto.Fornecedor = context.Fornecedor.First(fornecedor => fornecedor.Id == produtoDto.FornecedorId);
                produto.PrecoDeCusto = produtoDto.PrecoDeCusto;
                produto.PrecoDeVenda = produtoDto.PrecoDeVenda;
                produto.Medicao = produtoDto.Medicao;
                context.SaveChanges();

                return RedirectToAction("Produtos", "Gestao");

            }
            else
            {
                ViewBag.Categorias = context.Categoria.ToList();
                ViewBag.Fornecedores = context.Fornecedor.ToList();

                return View("../Gestao/NovoProduto");
            }
        }

        public IActionResult Deletar(int? id)
        {
            if (id != null)
            {
                var produto = context.Produto.First(x => x.Id == id);
                produto.Status = false;
                context.SaveChanges();

            }
            return RedirectToAction("Produtos", "Gestao");
        }

        //// POST: Item/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var produto = await context.Produto.FindAsync(id);
        //    context.Produto.Remove(produto);
        //    await context.SaveChangesAsync();
        //    return RedirectToAction("Produto", "Gestao");
        //}

        [HttpPost]
        public IActionResult Pesquisar(int id)
        {
            if (id > 0)
            {
                var produto = context.Produto.Where(p => p.Status == true)
                    .Include(p => p.Categoria)
                    .Include(p => p.Fornecedor).First(p => p.Id == id);
                if (produto != null)
                {
                    var estoque = context.Estoque.First(e => e.Produto.Id == produto.Id);
                    if (estoque == null)
                    {
                        produto = null;
                    }
                }

                if (produto != null)
                {
                    Promocao promocao;

                    try
                    {
                        promocao = context.Promocao.First(p => p.Produto.Id == produto.Id && p.Status == true);
                    }
                    catch (Exception e)
                    {
                        promocao = null;

                    }

                    if (promocao != null)
                    {
                        produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Porcentegem / 100));
                    }
                }
                if (produto != null)
                {
                    Response.StatusCode = 200;
                    return Json(produto);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(null);
                }
            }
            else
            {
                Response.StatusCode = 404;
                return Json(null);
            }
        }

        [HttpPost]
        public IActionResult GerarVenda([FromBody] SaidaDto[] dados)
        {
            return Ok(dados);
        }

        public class SaidaDto
        {
            public int produto;
            public int quantidade;
            public float subTotal;
        }
    }
}
