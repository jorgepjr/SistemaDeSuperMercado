using SistemaDeSupermercado.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeSupermercado.Models
{
    public class Categoria
    {
        public Categoria()
        {
        }


        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }


        
    }
}
