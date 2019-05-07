using Microsoft.AspNetCore.Mvc;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;

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
    }
}