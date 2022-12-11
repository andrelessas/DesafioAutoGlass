using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.EditarFornecedorCommand
{
    public class EditarFornecedorCommand:IRequest<Unit>
    {
        public int Id { get; set; }        
        public string DescricaoFornecedor { get; set; }
        public string CNPJ { get; set; }
    }
}