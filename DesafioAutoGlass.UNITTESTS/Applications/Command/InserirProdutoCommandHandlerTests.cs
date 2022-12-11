using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.InserirProdutoCommand;
using DesafioAutoGlass.APPLICATION.Validations;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class InserirProdutoCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly InserirProdutoCommandHandler _inserirFornecedorCommandHandler;
        public InserirProdutoCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWork.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _unitOfWork.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _inserirFornecedorCommandHandler = new InserirProdutoCommandHandler(_unitOfWork.Object);
        }

        [Fact]
        public async void CadastroProdutoValido_QuandoExecutado_ProdutoCadastradoComSucesso()
        {
            //Arrange
            _fornecedorRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Fornecedor());
            //Act
            await _inserirFornecedorCommandHandler.Handle(new InserirProdutoCommand(), new CancellationToken());
            //Assert
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Invalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _inserirFornecedorCommandHandler.Handle(new InserirProdutoCommand(), new CancellationToken())).Result;
            Assert.Equal("O fornecedor informado não existe.", result.Message);
        }

        [Theory]
        [InlineData("", "2022-9-10 10:30:15", "2022-9-10 10:30:15")]
        public void InserirProdutoInvalido_RetornarExcecoesFluentValidations(string descricaoProduto, DateTime dataFabricacao, DateTime dataValidade)
        {
            //Arrange
            var inserirProdutoCommand = new InserirProdutoCommandValidation();
            var produto = new InserirProdutoCommand{DescricaoProduto = descricaoProduto, DataFabricacao = dataFabricacao,DataValidade = dataValidade};
            //Act
            var result = inserirProdutoCommand.Validate(produto);
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