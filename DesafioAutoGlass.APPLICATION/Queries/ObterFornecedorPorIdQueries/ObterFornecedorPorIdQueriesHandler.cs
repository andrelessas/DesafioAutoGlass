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

namespace DesafioAutoGlass.APPLICATION.Queries.ObterFornecedorPorIdQueries
{
    public class ObterFornecedorPorIdQueriesHandler : IRequestHandler<ObterFornecedorPorIdQueries, FornecedorViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObterFornecedorPorIdQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FornecedorViewModel> Handle(ObterFornecedorPorIdQueries request, CancellationToken cancellationToken)
        {
            var fornecedor =  await _unitOfWork.Fornecedor.GetByIdAsync(request.Id);
            if(fornecedor == null)
                throw new ExcecoesPersonalizadas("Fornecedor não encontrado.",404);
            
            return _mapper.Map<FornecedorViewModel>(fornecedor);
        }
    }
}