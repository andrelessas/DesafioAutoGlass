using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using DesafioAutoGlass.APPLICATION.Queries.ObterFornecedorPorIdQueries;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.UNITTESTS.Configurations;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Queries
{
    public class ObterFornecedorPorIdQueriesHandlerTests : AutoMapperConfiguration
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly ObterFornecedorPorIdQueriesHandler _obterFornecedorPorIdQueriesHandler;

        public ObterFornecedorPorIdQueriesHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWork.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _obterFornecedorPorIdQueriesHandler = new ObterFornecedorPorIdQueriesHandler(_unitOfWork.Object, Mapper);
        }

        [Fact]
        public async void ObterFornecedor_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var fornecedor = new Fornecedor(
                new Faker("pt_BR").Company.CompanyName(),
                new Faker("pt_BR").Company.Cnpj());

            _fornecedorRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(fornecedor);
            //Act
            var result = await _obterFornecedorPorIdQueriesHandler.Handle(new ObterFornecedorPorIdQueries(1), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(fornecedor.CNPJ, result.Cnpj);
            Assert.Equal(fornecedor.Descricao, result.DescricaoFornecedor);
        }

        [Fact]
        public void ObterFornecedorQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterFornecedorPorIdQueriesHandler.Handle(new ObterFornecedorPorIdQueries(1), new CancellationToken())).Result;
            Assert.Equal("Fornecedor não encontrado.",result.Message);
            Assert.Equal(404,result.StatusCode);
        }
    }
}