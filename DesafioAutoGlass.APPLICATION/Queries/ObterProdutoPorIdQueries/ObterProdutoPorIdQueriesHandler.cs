using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.CORE.ViewModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Queries.ObterProdutoPorIdQueries
{
    public class ObterProdutoPorIdQueriesHandler : IRequestHandler<ObterProdutoPorIdQueries, ProdutoViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObterProdutoPorIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProdutoViewModel> Handle(ObterProdutoPorIdQueries request, CancellationToken cancellationToken)
        {
            var produto = await _unitOfWork.Produto.GetByIdAsync(request.Id);
            if(produto == null)
                throw new ExcecoesPersonalizadas("Nenhum produto encontrado.",404);

            return _mapper.Map<ProdutoViewModel>(produto);
        }
    }
}