using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.RemoverProdutoCommand
{
    public class RemoverProdutoCommandHandler : IRequestHandler<RemoverProdutoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoverProdutoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoverProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _unitOfWork.Produto.GetByIdAsync(request.IdProduto);
            if (produto == null)
                throw new ExcecoesPersonalizadas("Produto não encontrado.");

            produto.RemoverProduto();

            _unitOfWork.Produto.Update(produto);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}