using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.ViewModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Queries.ObterFornecedorPorIdQueries
{
    public class ObterFornecedorPorIdQueries:IRequest<FornecedorViewModel>
    {
        public int Id { get; set; }

        public ObterFornecedorPorIdQueries(int id)
        {
            Id = id;
        }
    }
}