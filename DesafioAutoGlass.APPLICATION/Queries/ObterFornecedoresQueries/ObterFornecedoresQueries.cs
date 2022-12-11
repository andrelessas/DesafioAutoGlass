using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Models;
using DesafioAutoGlass.CORE.ViewModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Queries.ObterFornecedoresQueries
{
    public class ObterFornecedoresQueries:IRequest<PaginationResult<FornecedorViewModel>>
    {
        public string Query { get; set; }
        public int Page { get; set; }

        public ObterFornecedoresQueries(string query, int page)
        {
            Query = query;
            Page = page;
        }
    }
}