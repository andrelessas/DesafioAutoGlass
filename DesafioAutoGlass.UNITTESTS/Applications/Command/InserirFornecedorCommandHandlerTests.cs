using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Command.InserirFornecedorCommand;
using DesafioAutoGlass.APPLICATION.Validations;
using DesafioAutoGlass.CORE.Interfaces;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class InserirFornecedorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly InserirFornecedorCommandHandler _inserirFornecedorCommandHandler;
        private readonly InserirFornecedorCommandValidation _inserirFornecedorCommandHandlerValidation;

        public InserirFornecedorCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWork.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _inserirFornecedorCommandHandler = new InserirFornecedorCommandHandler(_unitOfWork.Object);
            _inserirFornecedorCommandHandlerValidation = new InserirFornecedorCommandValidation();
        }

        [Fact]
        public async void InserirFornecedorValido_QuandoExecutado_FornecedorCadastradoComSucesso()
        {
            //Arrange
            var inserirFornecedorCommand = new Faker<InserirFornecedorCommand>()
                .RuleFor(x => x.DescricaoFornecedor, y => y.Company.CompanyName())
                .RuleFor(x => x.CNPJ, "11.111.111/1111-11")
                .Generate();
            //Act
            await _inserirFornecedorCommandHandler.Handle(inserirFornecedorCommand, new CancellationToken());
            //Assert
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData("","")]
        [InlineData("sdadasdasdsadasd","11111111111111111")]
        public void InserirFornecedorInvalido_RetornarExcecoesFluentValidations(string descricaoFornecedor,string cnpj)
        {
            //Arrange
            var fornecedor = new InserirFornecedorCommand{DescricaoFornecedor = descricaoFornecedor, CNPJ = cnpj};
            //Act
            var result = _inserirFornecedorCommandHandlerValidation.Validate(fornecedor);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x=>x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o CNPJ do fornecedor.") ||
                erros.Contains("CNPJ inválido.") ||
                erros.Contains("Necessário informar o nome do Fornecedor."));
                
        }
    }
}