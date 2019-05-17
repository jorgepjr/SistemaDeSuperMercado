using System.ComponentModel.DataAnnotations;

namespace SistemaDeSupermercado.Models.DTO
{
    public class CategoriaDto
    {

        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
    }


}
