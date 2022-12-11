using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Notifications;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Entities
{
    public class FornecedorTests
    {
        [Fact]
        public void DadoAsInformacoesDeFornecedorCorreta_AtualizarCadastro_RetornarFornecedorAlterado()
        {
            //Arrange
            var fornecedor = new Fornecedor(
                new Faker().Company.CompanyName(),
                "99.999.999/9999-99");

            var fornecedorAlterado = new Fornecedor(
                new Faker().Company.CompanyName(),
                "11.111.111/1111-11");
            //Act
            fornecedor.AlterarFornecedor(fornecedorAlterado.Descricao,fornecedorAlterado.CNPJ);
            //Assert
            Assert.Equal(fornecedorAlterado.CNPJ,fornecedor.CNPJ);
            Assert.Equal(fornecedorAlterado.Descricao,fornecedor.Descricao);
        }

        [Fact]
        public void DadoFornecedorATIVO_ExecutarRemoverFornecedor_RetornarFornecedorINATIVO()
        {
            //Arrange
            var fornecedor = new Fornecedor(
                new Faker().Commerce.ProductName(),
                "99.999.999/9999-99");
            //Act
            fornecedor.RemoverFornecedor();
            //Assert
            Assert.Equal("INATIVO",fornecedor.Status);
        }

        [Fact]
        public void DadoFornecedorINATIVO_ExecutarRemoverFornecedor_RetornarExcecao()
        {
            //Arrange 
            var fornecedor = new Fornecedor(
                new Faker().Commerce.ProductName(),
                "99.999.999/9999-99"); 
            
            fornecedor.RemoverFornecedor();
            //Act - Assert
            var result = Assert.Throws<ExcecoesPersonalizadas>(() => fornecedor.RemoverFornecedor());
            Assert.Equal("O fornecedor já está com Status INATIVO.",result.Message);
        }
    }
}