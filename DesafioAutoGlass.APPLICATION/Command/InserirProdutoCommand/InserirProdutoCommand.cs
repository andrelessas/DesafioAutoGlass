using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.InputModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.InserirProdutoCommand
{
    public class InserirProdutoCommand : IRequest<Unit>
    {
        public string DescricaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; } 
        public int IdFornecedor { get; set; }
    }
}