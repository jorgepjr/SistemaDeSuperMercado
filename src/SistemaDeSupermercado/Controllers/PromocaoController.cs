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
    public class PromocaoController : Controller
    {
        private readonly ApplicationDbContext context;

        public PromocaoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Salvar(PromocaoDto promocaoDto)
        {
            if (ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoDto.Nome;
                promocao.Produto = context.Produto.First(pr => pr.Id == promocaoDto.ProdutoId);
                promocao.Porcentegem = promocaoDto.Porcentegem;
                promocao.Status = true;

                context.Promocao.Add(promocao);
                context.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                ViewBag.Produtos = context.Produto.ToList();
                return View("..Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDto promocaoDto)
        {
            if (ModelState.IsValid)
            {
                var promocao = context.Promocao.First(x => x.Id == promocaoDto.Id);
                promocao.Nome = promocaoDto.Nome;
                promocao.Produto = context.Produto.First(p => p.Id == promocaoDto.ProdutoId);
                promocao.Porcentegem = promocaoDto.Porcentegem;

                context.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }

            else
            {


                return View("../Gestao/EditarPromocao");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int? id)
        {
            if (id != null)
            {
                var promocao = context.Promocao.First(p => p.Id == id);
                promocao.Status = false;

                context.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Gestao");

        }
    }
}