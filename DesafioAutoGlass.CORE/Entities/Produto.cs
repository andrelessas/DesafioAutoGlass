using System.ComponentModel.DataAnnotations.Schema;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.CORE.Utils;

namespace DesafioAutoGlass.CORE.Entities
{
    public class Produto
    {
        public Produto(string descricao, DateTime dataFabricacao, DateTime dataValidade, int idFornecedor)
        {
            Descricao = descricao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;

            Status = ConstStatus.ATIVO;
            IdFornecedor = idFornecedor;
            DataCadastro = DateTime.Now;
        }

        public Produto()
        {
            
        }

        public int ID { get; private set; }
        public string Descricao { get; private set; }
        public string Status { get; private set; }
        public DateTime DataFabricacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public int IdFornecedor { get; private set; }
        public Fornecedor Fornecedor { get; private set; }

        public void RemoverProduto()
        {
            if (Status == ConstStatus.INATIVO)
                throw new ExcecoesPersonalizadas("O produto já está com Status INATIVO.");

            Status = ConstStatus.INATIVO;
            DataAtualizacao = DateTime.Now;
        }

        public void AlterarProduto(string descricao, DateTime dataFabricacao, DateTime dataValidade, int idFornecedor)
        {
            Descricao = descricao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            IdFornecedor = idFornecedor;
            DataAtualizacao = DateTime.Now;
        }
    }
}