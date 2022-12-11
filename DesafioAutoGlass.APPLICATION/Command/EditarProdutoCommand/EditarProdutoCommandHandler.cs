using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.EditarProdutoCommand
{
    public class EditarProdutoCommandHandler : IRequestHandler<EditarProdutoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditarProdutoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(EditarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _unitOfWork.Produto.GetByIdAsync(request.IdProduto);
            
            if(produto == null)
                throw new ExcecoesPersonalizadas("Produto não encontrado.");

            Fornecedor fornecedor = await _unitOfWork.Fornecedor.GetByIdAsync(request.IdFornecedor);
            if(fornecedor == null)
                throw new ExcecoesPersonalizadas("O fornecedor informado não existe. Para vincular o produto ao fornecedor, antes é necessárion efetuar o cadastro do fornecedor.");

            produto.AlterarProduto(request.DescricaoProduto,
                request.DataFabricacao,
                request.DataValidade,
                request.IdFornecedor);

            _unitOfWork.Produto.Update(produto); 
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;            
        }
    }
}