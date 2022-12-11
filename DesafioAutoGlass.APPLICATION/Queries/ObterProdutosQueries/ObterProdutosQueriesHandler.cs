using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Models;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.CORE.ViewModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Queries.ObterProdutosQueries
{
    public class ObterProdutosQueriesHandler : IRequestHandler<ObterProdutosQueries, PaginationResult<ProdutoViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObterProdutosQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginationResult<ProdutoViewModel>> Handle(ObterProdutosQueries request, CancellationToken cancellationToken)
        {
            var produtos = await _unitOfWork.Produto.GetAllAsync(request.Query, request.Page);
            if (produtos == null || !produtos.Data.Any())
                throw new ExcecoesPersonalizadas("Nenhum produto encontrado.",404);

            return _mapper.Map<PaginationResult<ProdutoViewModel>>(produtos);
        }
    }
}