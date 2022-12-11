using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.InserirFornecedorCommand
{
    public class InserirFornecedorCommandHandler : IRequestHandler<InserirFornecedorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InserirFornecedorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(InserirFornecedorCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Fornecedor.Insert(new Fornecedor(request.DescricaoFornecedor, request.CNPJ));
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}