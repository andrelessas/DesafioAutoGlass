using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Command.EditarFornecedorCommand;
using DesafioAutoGlass.APPLICATION.Validations;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class EditarFornecedorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorks;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly EditarFornecedorCommandHandler _editarFornecedorCommandHandler;
        private readonly Fornecedor _fornecedor;
        private readonly EditarFornecedorCommand _editarFornecedorCommand;
        private readonly EditarFornecedorCommandValidation _editarFornecedorCommandValidation;

        public EditarFornecedorCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWork>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWorks.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _editarFornecedorCommandHandler = new EditarFornecedorCommandHandler(_unitOfWorks.Object);

            _fornecedor = new Fornecedor(
                new Faker().Company.CompanyName(),
                "11111111111111111"
            );

            _editarFornecedorCommand = new Faker<EditarFornecedorCommand>()
                .RuleFor(x => x.Id, 1)
                .RuleFor(x => x.DescricaoFornecedor, y => y.Company.CompanyName())
                .RuleFor(x => x.CNPJ, "11111111111111")
                .Generate();

            _editarFornecedorCommandValidation = new EditarFornecedorCommandValidation();
        }

        [Fact]
        public async Task FornecedorComDadosCorretos_AlterarCadastroFornecedor_RetornarFornecedorAlteradoAsync()
        {
            //Arrange
            _fornecedorRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_fornecedor);
            //Act
            await _editarFornecedorCommandHandler.Handle(_editarFornecedorCommand, new CancellationToken());
            //Assert
            _unitOfWorks.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void FornecedorComDadosInvalido_AlterarCadastroFornecedor_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _editarFornecedorCommandHandler.Handle(_editarFornecedorCommand, new CancellationToken())).Result;
            Assert.Equal("Fornecedor não encontrado.", result.Message);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("asdadadasdasd", "9999999999999")]
        public void _RetornarExcecoesFluentValidations(string descricaoFornecedor, string CNPJ)
        {
            //Arrange
            var editarFornecedorCommand = new EditarFornecedorCommand{DescricaoFornecedor = descricaoFornecedor,CNPJ = CNPJ};
            //Act
            var result = _editarFornecedorCommandValidation.Validate(editarFornecedorCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x=>x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o CNPJ do fornecedor.") ||
                erros.Contains("Necessário informar o nome do Fornecedor.") ||
                erros.Contains("CNPJ inválido."));
        }
    }
}