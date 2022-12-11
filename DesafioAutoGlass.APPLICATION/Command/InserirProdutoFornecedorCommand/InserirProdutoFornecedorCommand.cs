using DesafioAutoGlass.APPLICATION.InputModels;
using MediatR;

namespace DesafioAutoGlass.APPLICATION.Command.InserirProdutoFornecedorCommand
{
    public class InserirProdutoFornecedorCommand:IRequest<Unit>
    {
        public string DescricaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; } 
        public FornecedorInputModel Fornecedor { get; set; }
    }
}