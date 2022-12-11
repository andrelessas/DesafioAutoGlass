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
    public class ProdutoTests
    {
        [Fact]
        public void DadoAsInformacoesDeProdutoCorreta_AtualizarCadastro_RetornarProdutoAlterado()
        {
            //Arrange
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                1);       

            var produtoAlterado = new Produto(
                new Faker().Commerce.ProductName(),
                DateTime.Now.AddDays(1),
                DateTime.Now.AddMonths(3),
                5);       
            //Act
            produto.AlterarProduto(
                produtoAlterado.Descricao,
                produtoAlterado.DataFabricacao,
                produtoAlterado.DataValidade,
                produtoAlterado.IdFornecedor);
            //Assert
            Assert.Equal(produtoAlterado.Descricao,produto.Descricao);
            Assert.Equal(produtoAlterado.DataFabricacao,produto.DataFabricacao);
            Assert.Equal(produtoAlterado.DataValidade,produto.DataValidade);
            Assert.Equal(produtoAlterado.IdFornecedor,produto.IdFornecedor);
            Assert.NotNull(produto.DataAtualizacao);
        }

        [Fact]
        public void DadoProdutoATIVO_ExecutarRemoverProduto_RetornarProdutoINATIVO()
        {
            //Arrange
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                1);                
            //Act
            produto.RemoverProduto();
            //Assert
            Assert.Equal("INATIVO",produto.Status);
        }

        [Fact]
        public void DadoProdutoINATIVO_ExecutarRemoverProduto_RetornarExcecao()
        {
            //Arrange 
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                1); 
            
            produto.RemoverProduto();
            //Act - Assert
            var result = Assert.Throws<ExcecoesPersonalizadas>(() => produto.RemoverProduto());
            Assert.Equal("O produto já está com Status INATIVO.",result.Message);
        }
    }
}