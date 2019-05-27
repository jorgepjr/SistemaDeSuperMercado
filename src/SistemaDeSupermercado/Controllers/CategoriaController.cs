using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;

namespace SistemaDeSupermercado.Controllers
{

    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IToastNotification toastNotification;
        public CategoriaController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            this.context = context;
            this.toastNotification = toastNotification;
        }


        public IActionResult Index()
        {

            return View();

        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDto categoriaDto)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria {
                    Id = categoriaDto.Id,
                    Nome = categoriaDto.Nome,
                    Status = true
                };

                context.Add(categoria);
                context.SaveChanges();
                toastNotification.AddSuccessToastMessage("Salvo com sucesso!");
                return RedirectToAction("EditarCategoria", "Gestao");

            }
            else
            {
                return View("../Gestao/NovaCategoria");
            }

        }

        [HttpPost]
        public IActionResult Atualizar(CategoriaDto categoriaDto)
        {
            if (ModelState.IsValid)
            {
                var categoria = context.Categoria.First(cat => cat.Id == categoriaDto.Id);
                categoria.Nome = categoriaDto.Nome;
                context.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {

                return View("../Gestao/EditarCategoria");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var categoria = context.Categoria.First(x => x.Id == id);

                if (!categoria.Status)
                {
                    categoria.Status = true;
                    context.SaveChanges();
                    return RedirectToAction("Categorias", "Gestao");


                }
                else
                {
                    categoria.Status = false;
                    context.SaveChanges();
                }
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
                return View("../Gestao/Categorias");
            }
        }
    }
}
