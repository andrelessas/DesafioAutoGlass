using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.InserirProdutoFornecedorCommand
{
    public class InserirProdutoFornecedorCommandHandler : IRequestHandler<InserirProdutoFornecedorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InserirProdutoFornecedorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(InserirProdutoFornecedorCommand request, CancellationToken cancellationToken)
        {
            var idFornecedor = await _unitOfWork.Fornecedor.MaxIdAsync() + 1;
            
            await _unitOfWork.BeginTransactionAsync();
            
            _unitOfWork.Fornecedor.Insert(new Fornecedor(request.Fornecedor.DescricaoFornecedor, request.Fornecedor.CNPJ));
            _unitOfWork.Produto.Insert(new Produto(request.DescricaoProduto, request.DataFabricacao, request.DataValidade, idFornecedor));
            
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return Unit.Value;
        }
    }
}