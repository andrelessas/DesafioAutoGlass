using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Command.RemoverFornecedorCommand;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class RemoverFornecedorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly RemoverFornecedorCommandHandler _removerFornecedorCommandHandler;

        public RemoverFornecedorCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWork.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _removerFornecedorCommandHandler = new RemoverFornecedorCommandHandler(_unitOfWork.Object);
        }

        [Fact]
        public async void ExcluirFornecedorValido_QuandoExecutado_FornecedorExcluidoLogicamente()
        {
            //Arrange
            var fornecedor = new Fornecedor(
                new Faker().Company.CompanyName(),
                "111111111111111");

            _fornecedorRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(fornecedor);
            //Act
            await _removerFornecedorCommandHandler.Handle(new RemoverFornecedorCommand(1), new CancellationToken());
            //Assert
            Assert.Equal("INATIVO",fornecedor.Status);
            _unitOfWork.Verify(x=>x.SaveChangesAsync(),Times.Once);
        }

        [Fact]
        public void RemoverFornecedorQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _removerFornecedorCommandHandler.Handle(new RemoverFornecedorCommand(1),new CancellationToken())).Result;
            Assert.Equal("Fornecedor não encontrado.",result.Message);
        }

    }
}