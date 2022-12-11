using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Command.RemoverProdutoCommand;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class RemoverProdutoCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly RemoverProdutoCommandHandler _removerProdutoCommandHandler;

        public RemoverProdutoCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _unitOfWork.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _removerProdutoCommandHandler = new RemoverProdutoCommandHandler(_unitOfWork.Object);
        }

        [Fact]
        public async void RemoverProdutoValido_QuandoExecutado_RemoverLogicamente()
        {
            //Arrange
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                DateTime.Now,
                DateTime.Now.AddDays(9),
                1);

            _produtoRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            await _removerProdutoCommandHandler.Handle(new RemoverProdutoCommand(1), new CancellationToken());
            //Assert
            Assert.Equal("INATIVO", produto.Status);
            _unitOfWork.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void RemoverProdutoQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _removerProdutoCommandHandler.Handle(new RemoverProdutoCommand(1), new CancellationToken())).Result;
            Assert.Equal("Produto não encontrado.",result.Message);
        }
    }
}