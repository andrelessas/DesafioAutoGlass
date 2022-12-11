using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.InserirProdutoCommand
{
    public class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InserirProdutoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
        {
            var fornecedor = await _unitOfWork.Fornecedor.GetByIdAsync(request.IdFornecedor);
            if(fornecedor == null)
                throw new ExcecoesPersonalizadas("O fornecedor informado não existe.");

            _unitOfWork.Produto.Insert(new Produto(request.DescricaoProduto, request.DataFabricacao, request.DataValidade, fornecedor.Id));

            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}