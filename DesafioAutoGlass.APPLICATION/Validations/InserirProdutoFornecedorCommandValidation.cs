using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.InserirProdutoFornecedorCommand;
using FluentValidation;

namespace DesafioAutoGlass.APPLICATION.Validations
{
    public class InserirProdutoFornecedorCommandValidation : AbstractValidator<InserirProdutoFornecedorCommand>
    {
        public InserirProdutoFornecedorCommandValidation()
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

            RuleFor(x => x.Fornecedor.CNPJ)
                .NotEmpty()
                .WithMessage("Necessário informar o CNPJ do fornecedor.")
                .Must(ValidarCNPJ)
                .WithMessage("CNPJ inválido.");

            RuleFor(x => x.Fornecedor.DescricaoFornecedor)
                .NotEmpty()
                .WithMessage("Necessário informar o nome do Fornecedor.");
        }

        private bool ValidarDataFabricacao(DateTime dataFabricacao, DateTime dataValidade)
        {
            return dataValidade > dataFabricacao;
        }

        private bool ValidarCNPJ(string CNPJ)
        {
            var regex = new Regex(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}");
            return regex.IsMatch(CNPJ);
        }
    }
}
