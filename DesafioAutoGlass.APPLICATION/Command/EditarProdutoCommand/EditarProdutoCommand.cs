using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.EditarProdutoCommand
{
    public class EditarProdutoCommand:IRequest<Unit>
    {
        public int IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int IdFornecedor { get; set; }        
    }
}