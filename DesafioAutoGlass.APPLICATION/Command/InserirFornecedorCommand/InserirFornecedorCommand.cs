using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.InserirFornecedorCommand
{
    public class InserirFornecedorCommand:IRequest<Unit>
    {
        public string DescricaoFornecedor { get; set; }
        public string CNPJ { get; set; }
    }
}