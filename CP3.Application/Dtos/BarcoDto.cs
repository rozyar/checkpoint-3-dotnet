using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public void Validate()
        {
            var validator = new BarcoDtoValidation();
            var result = validator.Validate(this);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(b => b.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório");

            RuleFor(b => b.Modelo)
                .NotEmpty().WithMessage("Modelo é obrigatório");

            RuleFor(b => b.Ano)
                .GreaterThan(0).WithMessage("Ano deve ser maior que zero");

            RuleFor(b => b.Tamanho)
                .GreaterThan(0).WithMessage("Tamanho deve ser maior que zero");
        }
    }
}
