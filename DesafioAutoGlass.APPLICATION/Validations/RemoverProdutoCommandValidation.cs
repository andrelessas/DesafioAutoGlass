using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.RemoverProdutoCommand;
using FluentValidation;

namespace DesafioAutoGlass.APPLICATION.Validations
{
    public class RemoverProdutoCommandValidation:AbstractValidator<RemoverProdutoCommand>
    {
        public RemoverProdutoCommandValidation()
        {
            RuleFor(x=>x.IdProduto)
                .GreaterThan(0)
                .WithMessage("Necessário informar o código do produto que será removido.");
        }
    }
}