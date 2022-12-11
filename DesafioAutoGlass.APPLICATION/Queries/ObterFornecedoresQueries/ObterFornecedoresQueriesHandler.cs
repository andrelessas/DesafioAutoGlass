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

namespace DesafioAutoGlass.APPLICATION.Queries.ObterFornecedoresQueries
{
    public class ObterFornecedoresQueriesHandler : IRequestHandler<ObterFornecedoresQueries, PaginationResult<FornecedorViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObterFornecedoresQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginationResult<FornecedorViewModel>> Handle(ObterFornecedoresQueries request, CancellationToken cancellationToken)
        {
            var fornecedores = await _unitOfWork.Fornecedor.GetAllAsync(request.Query, request.Page);

            if (fornecedores == null || !fornecedores.Data.Any())
                throw new ExcecoesPersonalizadas("Nenhum fornecedor encontrado.",404);

            return _mapper.Map<PaginationResult<FornecedorViewModel>>(fornecedores);
        }
    }
}