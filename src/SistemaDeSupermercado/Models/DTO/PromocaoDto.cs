using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeSupermercado.Models.DTO
{
    public class PromocaoDto
    {

        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        [MinLength(3)]
        public string Nome { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        [Range(0, 100)]
        public float Porcentegem { get; set; }

    }
}

