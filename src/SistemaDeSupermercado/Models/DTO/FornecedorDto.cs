using System.ComponentModel.DataAnnotations;

namespace SistemaDeSupermercado.Models.DTO
{
    public class FornecedorDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O {0} deve ter no máximo 50 caracteres")]
        [MinLength(2, ErrorMessage = "O {0} dever ter no mínimo 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage ="{0} inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage ="{0} inválido")]
        public string Telefone { get; set; }

    }
}
