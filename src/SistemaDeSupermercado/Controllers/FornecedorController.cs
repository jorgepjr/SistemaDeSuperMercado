using Microsoft.AspNetCore.Mvc;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;
using System.Linq;

namespace SistemaDeSupermercado.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ApplicationDbContext context;

        public FornecedorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDto fornecedorDto)
        {
            if (ModelState.IsValid)
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorDto.Nome;
                fornecedor.Email = fornecedorDto.Email;
                fornecedor.Telefone = fornecedorDto.Telefone;
                fornecedor.Status = true;

                context.Add(fornecedor);
                context.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");

            }
            else
            {
                return View("../Gestao/NovoFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(FornecedorDto fornecedorDto)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = context.Fornecedor.First(f => f.Id == fornecedorDto.Id);
                fornecedor.Nome = fornecedorDto.Nome;
                fornecedor.Email = fornecedorDto.Email;
                fornecedor.Telefone = fornecedorDto.Telefone;

                context.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else
            {

                return View("../Gestao/EditarFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int? id)
        {
            if (id != null)
            {
                var fornecedor = context.Fornecedor.First(f => f.Id == id);
                fornecedor.Status = false;
                context.SaveChanges();
            }

            return RedirectToAction("Fornecedores", "Gestao");
        }
    }
}