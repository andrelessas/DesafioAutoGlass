using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.RemoverFornecedorCommand;
using FluentValidation;

namespace DesafioAutoGlass.APPLICATION.Validations
{
    public class RemoverFornecedorCommandValidation:AbstractValidator<RemoverFornecedorCommand>
    {
        public RemoverFornecedorCommandValidation()
        {
            RuleFor(x=>x.Id)
                .GreaterThan(1)
                .WithMessage("Necessário informar o código do fornecedor que será removido.");
        }
    }
}