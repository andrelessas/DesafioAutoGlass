using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.ViewModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Queries.ObterProdutoPorIdQueries
{
    public class ObterProdutoPorIdQueries:IRequest<ProdutoViewModel>
    {
        public int Id { get; set; }

        public ObterProdutoPorIdQueries(int id)
        {
            Id = id;
        }
    }
}