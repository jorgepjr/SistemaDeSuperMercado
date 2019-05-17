using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeSupermercado.Models.DTO
{
    public class ProdutoDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="O capo {0} é obrigatório")]
        [StringLength(100, ErrorMessage ="O {0} deve ter no máximo 100 caracteres")]
        [MinLength(2, ErrorMessage ="O {0} deve ter no mínimo 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Categoria do produto é obrigatório")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage ="Fornecedor do produto é obrigatório")]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage ="Preço de custo do produto é obrigatório")]
        public float PrecoDeCusto { get; set; }
        public string PrecoDeCustoString { get; set; }

        [Required(ErrorMessage ="Preço de venda do produto é obrigatório")]
        public float PrecoDeVenda { get; set; }
        public string PrecoDeVendaString { get; set; }

        [Required(ErrorMessage ="Medição de venda é obrigatória")]
        [Range(0, 2, ErrorMessage ="Medição inválida")]
        public int Medicao { get; set; }

    }
}
