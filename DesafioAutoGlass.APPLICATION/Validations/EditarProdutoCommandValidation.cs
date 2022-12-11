using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.EditarProdutoCommand;
using FluentValidation;

namespace DesafioAutoGlass.APPLICATION.Validations
{
    public class EditarProdutoCommandValidation : AbstractValidator<EditarProdutoCommand>
    {
        public EditarProdutoCommandValidation()
        {
            RuleFor(x => x.DescricaoProduto)
                .NotEmpty()
                .WithMessage("Necessário informar a descrição do produto.");
            
            RuleFor(x => x.DataFabricacao)
                .NotEmpty()
                .WithMessage("Necessário informar a data de fabricação do produto.");
            
            RuleFor(x => x.DataValidade)
                .NotEmpty()
                .WithMessage("Necessário informar a data de validade do produto.");

            RuleFor(x => x)
                .Must(x => ValidarDataFabricacao(x.DataFabricacao, x.DataValidade))
                .WithMessage("Data de fabricação do produto não pode ser maior ou igual a data de vencimento.");
        }

        private bool ValidarDataFabricacao(DateTime dataFabricacao, DateTime dataValidade)
        {
            return dataValidade > dataFabricacao;
        }
    }
}