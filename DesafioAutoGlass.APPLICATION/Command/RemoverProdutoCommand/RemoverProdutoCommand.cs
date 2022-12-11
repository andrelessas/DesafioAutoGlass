using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.RemoverProdutoCommand
{
    public class RemoverProdutoCommand:IRequest<Unit>
    {
        public int IdProduto { get; set; }

        public RemoverProdutoCommand(int idProduto)
        {
            IdProduto = idProduto;
        }
    }
}