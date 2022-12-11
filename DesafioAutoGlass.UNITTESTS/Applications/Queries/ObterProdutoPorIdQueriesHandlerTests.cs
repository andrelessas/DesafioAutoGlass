using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Queries.ObterProdutoPorIdQueries;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.UNITTESTS.Configurations;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Queries
{
    public class ObterProdutoPorIdQueriesHandlerTests:AutoMapperConfiguration
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly ObterProdutoPorIdQueriesHandler _obterProdutoPorIdQueriesHandler;

        public ObterProdutoPorIdQueriesHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _unitOfWork.SetupGet(x=>x.Produto).Returns(_produtoRepository.Object);
            _obterProdutoPorIdQueriesHandler = new ObterProdutoPorIdQueriesHandler(_unitOfWork.Object,Mapper);
        }

        [Fact]
        public async void ObterProduto_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                1);

            _produtoRepository.Setup(x=>x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            var result = await _obterProdutoPorIdQueriesHandler.Handle(new ObterProdutoPorIdQueries(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Descricao,result.Descricao);
            Assert.Equal(produto.DataFabricacao,result.DataFabricacao);
            Assert.Equal(produto.DataValidade,result.DataVencimento);
            Assert.Equal(produto.Status,result.StatusProduto);
        }

        [Fact]
        public void ObterProdutoQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterProdutoPorIdQueriesHandler.Handle(new ObterProdutoPorIdQueries(1),new CancellationToken())).Result;
            Assert.Equal("Nenhum produto encontrado.",result.Message);
            Assert.Equal(404,result.StatusCode);
        }
    }
}