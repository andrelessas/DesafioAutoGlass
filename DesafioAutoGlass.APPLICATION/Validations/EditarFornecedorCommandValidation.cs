using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.EditarFornecedorCommand;
using FluentValidation;

namespace DesafioAutoGlass.APPLICATION.Validations
{
    public class EditarFornecedorCommandValidation:AbstractValidator<EditarFornecedorCommand>
    {
        public EditarFornecedorCommandValidation()
        {
            RuleFor(x=>x.CNPJ)
                .NotEmpty()
                .WithMessage("Necessário informar o CNPJ do fornecedor.")
                .Must(ValidarCNPJ)
                .WithMessage("CNPJ inválido.");

            RuleFor(x=>x.DescricaoFornecedor)
                .NotEmpty()
                .WithMessage("Necessário informar o nome do Fornecedor.");
        }

        private bool ValidarCNPJ(string CNPJ)
        {
            var regex = new Regex(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}");
            return regex.IsMatch(CNPJ);
        }

    }
}