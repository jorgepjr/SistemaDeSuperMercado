using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;

namespace SistemaDeSupermercado.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext context;

        public GestaoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NovaCategoria(int id = 0)
        {
            if (id == 0)
            {
                return View(new CategoriaDto());
            }
            else
            {
                return View(context.Categoria.Where(x => x.Id.Equals(id)).FirstOrDefault());
            }
        }

        public IActionResult Categorias()
        {
            var categorias = context.Categoria.ToList();

            return View(categorias);
        }



        public IActionResult Fornecedores()
        {
            var fornecedores = context.Fornecedor.ToList();

            return View(fornecedores);
        }

        public IActionResult NovoFornecedor(int id = 0)
        {
            if (id == 0)
            {
                return View(new FornecedorDto());
            }
            else
            {
                return View(context.Fornecedor.Where(x => x.Id.Equals(id)).FirstOrDefault());
            }
        }
        public IActionResult NovoProduto(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Categorias = context.Categoria.ToList();
                ViewBag.Fornecedores = context.Fornecedor.ToList();
                return View(new ProdutoDto());
            }
            else
            {
                return View(context.Produto.Where(x => x.Id.Equals(id)).FirstOrDefault());
            }
        }

        public IActionResult Produtos()
        {
            var produtos = context.Produto
                                .Include(x => x.Categoria)
                                .Include(x => x.Fornecedor)
                                .ToList();
            return View();
        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = context.Categoria.First(cat => cat.Id == id);
            CategoriaDto CategoriaView = new CategoriaDto();
            CategoriaView.Id = categoria.Id;
            CategoriaView.Nome = categoria.Nome;
            return View(CategoriaView);
        }
    }
}
