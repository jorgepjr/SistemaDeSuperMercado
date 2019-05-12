using FluentValidation;
using SistemaDeSupermercado.Models;
using SistemaDeSupermercado.Models.DTO;

namespace SistemaDeSupermercado.Validator
{
    public class CategoriaValidator : AbstractValidator<CategoriaDto>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().Length(3, 50)
                .WithMessage("*Nome deve ter entre 3 e 50 caracteres");


        }
    }
}
