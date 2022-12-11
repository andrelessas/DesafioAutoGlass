using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DesafioAutoGlass.APPLICATION.Command.EditarProdutoCommand;
using DesafioAutoGlass.APPLICATION.Validations;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class EditarProdutoCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorks;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly EditarProdutoCommand _editarProdutoCommand;
        private readonly EditarProdutoCommandValidation _editarProdutoCommandValidation;
        private readonly EditarProdutoCommandHandler _editarProdutoCommandHandler;

        public EditarProdutoCommandTests()
        {
            _unitOfWorks = new Mock<IUnitOfWork>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _unitOfWorks.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _unitOfWorks.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _editarProdutoCommand = new Faker<EditarProdutoCommand>()
                .RuleFor(x => x.DescricaoProduto, y => y.Commerce.ProductName())
                .RuleFor(x => x.DataFabricacao, DateTime.Now)
                .RuleFor(x => x.DataValidade, DateTime.Now.AddMonths(3))
                .RuleFor(x => x.IdFornecedor, 3)
                .Generate();

            _editarProdutoCommandValidation = new EditarProdutoCommandValidation();
            _editarProdutoCommandHandler = new EditarProdutoCommandHandler(_unitOfWorks.Object);
        }

        [Fact]
        public async Task ProdutoValido_AlterarCadastroProduto_AlterarProdutoAsync()
        {
            //Arrange
            _produtoRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Produto());
            _fornecedorRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Fornecedor());
            //Act
            await _editarProdutoCommandHandler.Handle(_editarProdutoCommand, new CancellationToken());
            //Assert
            _unitOfWorks.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void AlterarProdutoQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _editarProdutoCommandHandler.Handle(new EditarProdutoCommand(), new CancellationToken())).Result;
            Assert.Equal("Produto não encontrado.", result.Message);
        }

        [Fact]
        public void AlterarProdutoComFornecedorQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange 
            _produtoRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Produto());
            //Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _editarProdutoCommandHandler.Handle(new EditarProdutoCommand(), new CancellationToken())).Result;
            Assert.Equal("O fornecedor informado não existe. Para vincular o produto ao fornecedor, antes é necessárion efetuar o cadastro do fornecedor.", result.Message);
        }

        [Theory]
        [InlineData("", "2022-9-10 10:30:15", "2022-9-10 10:30:15")]
        [InlineData("dddddddddd", "2022-9-10 10:30:15", "2022-9-9 10:30:15")]
        public void ProdutoInvalido_RetornarExcecoesFluentValidations(string descricao, DateTime dataFabricacao, DateTime dataValidade)
        {
            //Arrange
            var command = new EditarProdutoCommand { DescricaoProduto = descricao, DataFabricacao = dataFabricacao, DataValidade = dataValidade };
            //Act
            var result = _editarProdutoCommandValidation.Validate(command);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x=>x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar a descrição do produto.") ||
                erros.Contains("Necessário informar a data de fabricação do produto.") ||
                erros.Contains("Necessário informar a data de validade do produto.") ||
                erros.Contains("Data de fabricação do produto não pode ser maior ou igual a data de vencimento."));
        }
    }
}