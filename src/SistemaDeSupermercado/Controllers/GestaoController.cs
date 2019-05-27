using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models.DTO;

namespace SistemaDeSupermercado.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IToastNotification toastMessage;

        public GestaoController(IToastNotification toastMessage, ApplicationDbContext context)
        {
            this.toastMessage = toastMessage;
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

        //[Authorize(Policy ="PossuiPerfil")]
        public IActionResult Categorias()
        {
            var categorias = context.Categoria.ToList();

            return View(categorias);
        }



        public IActionResult Fornecedores()
        {
            var fornecedores = context.Fornecedor.Where(x => x.Status == true).ToList();

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
                                .Where(x => x.Status == true).ToList();
            return View(produtos);
        }
        public IActionResult EditarProduto(int id)
        {
            var produto = context.Produto.Include(x => x.Categoria).Include(x => x.Fornecedor).First(x => x.Id == id);
            ProdutoDto produtoView = new ProdutoDto();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.CategoriaId = produto.Categoria.Id;
            produtoView.FornecedorId = produto.Fornecedor.Id;
            produtoView.Medicao = produto.Medicao;
            ViewBag.Categorias = context.Categoria.ToList();
            ViewBag.Fornecedores = context.Fornecedor.ToList();
            return View(produtoView);

        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = context.Categoria.First(cat => cat.Id == id);
            CategoriaDto CategoriaView = new CategoriaDto();
            CategoriaView.Id = categoria.Id;
            CategoriaView.Nome = categoria.Nome;
            return View(CategoriaView);
        }

        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = context.Fornecedor.First(f => f.Id == id);
            FornecedorDto FornecedorView = new FornecedorDto();
            FornecedorView.Id = fornecedor.Id;
            FornecedorView.Nome = fornecedor.Nome;
            FornecedorView.Email = fornecedor.Email;
            FornecedorView.Telefone = fornecedor.Telefone;
            return View(FornecedorView);
        }

        public IActionResult Promocoes()
        {
            var promocoes = context.Promocao.Include(x => x.Produto).Where(x => x.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = context.Produto.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id)
        {
            var promocao = context.Promocao.Include(x => x.Produto).First(p => p.Id == id);
            PromocaoDto PromocaoView = new PromocaoDto();
            PromocaoView.Id = promocao.Id;
            PromocaoView.Nome = promocao.Nome;
            PromocaoView.ProdutoId = promocao.Produto.Id;
            PromocaoView.Porcentegem = promocao.Porcentegem;
            ViewBag.Produtos = context.Produto.ToList();

            return View(PromocaoView);

        }

        public IActionResult Estoque()
        {
            var listaDeEstoques = context.Estoque.Include(x => x.Produto).ToList();
            return View(listaDeEstoques);
        }

        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = context.Produto.ToList();
            return View();
        }

        public IActionResult EditarEstoque()
        {
            return View();
        }

        public IActionResult DetalharFornecedor(int id)
        {
            var fornecedor = context.Fornecedor.Find(id);

            return View(fornecedor);
        }

        public IActionResult Detalhes()
        {
            return View(ViewData["fornecedor"]);


        }
    }
}
