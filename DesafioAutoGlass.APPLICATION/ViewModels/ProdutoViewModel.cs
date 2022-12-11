
namespace DesafioAutoGlass.CORE.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string StatusProduto { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
    }
}