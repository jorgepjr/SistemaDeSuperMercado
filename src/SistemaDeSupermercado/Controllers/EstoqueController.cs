using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SistemaDeSupermercado.Data;
using SistemaDeSupermercado.Models;

namespace SistemaDeSupermercado.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IToastNotification toastMessage;

        public EstoqueController(ApplicationDbContext context, IToastNotification toastMessage)
        {
            this.context = context;
            this.toastMessage = toastMessage;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoque)
        {
            context.Add(estoque);
            context.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}