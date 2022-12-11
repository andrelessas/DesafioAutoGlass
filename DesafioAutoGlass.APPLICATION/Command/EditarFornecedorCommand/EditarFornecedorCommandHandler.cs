using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.EditarFornecedorCommand
{
    public class EditarFornecedorCommandHandler : IRequestHandler<EditarFornecedorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditarFornecedorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(EditarFornecedorCommand request, CancellationToken cancellationToken)
        {
            var fornecedor = await _unitOfWork.Fornecedor.GetByIdAsync(request.Id);
            if (fornecedor == null)
                throw new ExcecoesPersonalizadas("Fornecedor não encontrado.");

            fornecedor.AlterarFornecedor(request.DescricaoFornecedor, request.CNPJ);
            _unitOfWork.Fornecedor.Update(fornecedor);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }

    }
}