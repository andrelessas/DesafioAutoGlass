using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Models;
using DesafioAutoGlass.CORE.ViewModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Queries.ObterProdutosQueries
{
    public class ObterProdutosQueries:IRequest<PaginationResult<ProdutoViewModel>>
    {
        public string Query { get; set; }
        public int Page { get; set; }

        public ObterProdutosQueries(string query, int page)
        {
            Query = query;
            Page = page;
        }
    }
}