using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.RemoverFornecedorCommand
{
    public class RemoverFornecedorCommand:IRequest<Unit>
    {
        public RemoverFornecedorCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}