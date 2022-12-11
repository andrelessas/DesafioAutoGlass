using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.CORE.Utils;

namespace DesafioAutoGlass.CORE.Entities
{
    public class Fornecedor
    {
        public Fornecedor(string descricao, string cNPJ)
        {
            Descricao = descricao;
            CNPJ = cNPJ;
            DataCadastro = DateTime.Now;
            Status = ConstStatus.ATIVO;
        }

        public Fornecedor()
        {
            
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public string CNPJ { get; private set; }
        public string Status { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public List<Produto> Produto { get; private set; }

        public void AlterarFornecedor(string descricao, string cNPJ)
        {
            Descricao = descricao;
            CNPJ = cNPJ;
            DataAtualizacao = DateTime.Now;
        }

        public void RemoverFornecedor()
        {
            if(Status == ConstStatus.INATIVO)
                throw new ExcecoesPersonalizadas("O fornecedor já está com Status INATIVO.");
            
            Status = ConstStatus.INATIVO;
            DataAtualizacao = DateTime.Now;
        }



    }
}