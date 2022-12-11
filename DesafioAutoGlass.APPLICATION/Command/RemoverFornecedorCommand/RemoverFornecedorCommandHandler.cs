using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.RemoverFornecedorCommand
{
    public class RemoverFornecedorCommandHandler : IRequestHandler<RemoverFornecedorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoverFornecedorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoverFornecedorCommand request, CancellationToken cancellationToken)
        {
            var fornecedor = await _unitOfWork.Fornecedor.GetByIdAsync(request.Id);
            if (fornecedor == null)
                throw new ExcecoesPersonalizadas("Fornecedor não encontrado.");

            fornecedor.RemoverFornecedor();

            _unitOfWork.Fornecedor.Update(fornecedor);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}