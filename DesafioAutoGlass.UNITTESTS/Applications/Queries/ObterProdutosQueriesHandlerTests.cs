using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Queries.ObterProdutosQueries;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Models;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.CORE.ViewModels;
using DesafioAutoGlass.UNITTESTS.Configurations;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Queries
{
    public class ObterProdutosQueriesHandlerTests : AutoMapperConfiguration
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly ObterProdutosQueriesHandler _obterProdutosQueriesHandler;
        private List<Produto> _produtos;
        private PaginationResult<Produto> _paginationResult;

        public ObterProdutosQueriesHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _unitOfWork.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _obterProdutosQueriesHandler = new ObterProdutosQueriesHandler(_unitOfWork.Object, Mapper);

            _produtos = new List<Produto>();

            _paginationResult = new PaginationResult<Produto>();

            for (int i = 0; i < 10; i++)
            {
                _produtos.Add(new Produto(
                    new Faker().Commerce.ProductName(),
                    DateTime.Now,
                    DateTime.Now.AddMonths(1),
                    1));
            }
        }

        [Fact]
        public async void ObterProdutosAtivos_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            for (int i = 0; i < _produtos.Count(); i++)
            {
                if (i % 2 == 0)
                    _produtos[i].RemoverProduto();
            }
            var produtosAtivo = _produtos.Where(x => x.Status == "ATIVO").ToList();
            _paginationResult.Data = produtosAtivo;
            _paginationResult.Page = 1;
            _produtoRepository.Setup(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(_paginationResult);
            //Act
            var result = await _obterProdutosQueriesHandler.Handle(new ObterProdutosQueries("", _paginationResult.Page), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(produtosAtivo.Count(), result.Data.Count());
            for (int i = 0; i < produtosAtivo.Count(); i++)
            {
                Assert.Equal(produtosAtivo[i].Descricao, ((ProdutoViewModel)result.Data.ElementAt(i)).Descricao);
                Assert.Equal(produtosAtivo[i].DataFabricacao, ((ProdutoViewModel)result.Data.ElementAt(i)).DataFabricacao);
                Assert.Equal(produtosAtivo[i].DataValidade, ((ProdutoViewModel)result.Data.ElementAt(i)).DataVencimento);
                Assert.Equal(produtosAtivo[i].Status, ((ProdutoViewModel)result.Data.ElementAt(i)).StatusProduto);
            }

        }

        [Fact]
        public async void ObterProdutosPorDescricao_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var produtosPorDescricao = _produtos.Where(x => x.Descricao == _produtos[2].Descricao).ToList();
            _paginationResult.Data = produtosPorDescricao;
            _paginationResult.Page = 1;
            _produtoRepository.Setup(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(_paginationResult);
            //Act
            var result = await _obterProdutosQueriesHandler.Handle(new ObterProdutosQueries("", _paginationResult.Page), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(produtosPorDescricao.Count(), result.Data.Count());
            for (int i = 0; i < produtosPorDescricao.Count(); i++)
            {
                Assert.Equal(produtosPorDescricao[i].Descricao, ((ProdutoViewModel)result.Data.ElementAt(i)).Descricao);
                Assert.Equal(produtosPorDescricao[i].DataFabricacao, ((ProdutoViewModel)result.Data.ElementAt(i)).DataFabricacao);
                Assert.Equal(produtosPorDescricao[i].DataValidade, ((ProdutoViewModel)result.Data.ElementAt(i)).DataVencimento);
                Assert.Equal(produtosPorDescricao[i].Status, ((ProdutoViewModel)result.Data.ElementAt(i)).StatusProduto);
            }

        }

        [Fact]
        public void ObterProdutoQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterProdutosQueriesHandler.Handle(new ObterProdutosQueries("", 1), new CancellationToken())).Result;
            Assert.Equal("Nenhum produto encontrado.", result.Message);
            Assert.Equal(404, result.StatusCode);
        }
    }
}