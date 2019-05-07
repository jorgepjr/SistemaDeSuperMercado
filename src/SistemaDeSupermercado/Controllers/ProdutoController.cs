using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;

namespace SistemaDeSupermercado.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProdutoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDto produtoDto)
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto();
                produto.Nome = produtoDto.Nome;
                produto.Categoria = context.Categoria.First(categoria => categoria.Id == produtoDto.CategoriaId);
                produto.Fornecedor = context.Fornecedor.First(fornecedor => fornecedor.Id == produtoDto.FornecedorId);
                produto.PrecoDeCusto = produtoDto.PrecoDeCusto;
                produto.PrecoDeVenda = produtoDto.PrecoDeVenda;
                produto.Medicao = produtoDto.Medicao;
                produto.Status = true;
                context.Produto.Add(produto);
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
    }
}