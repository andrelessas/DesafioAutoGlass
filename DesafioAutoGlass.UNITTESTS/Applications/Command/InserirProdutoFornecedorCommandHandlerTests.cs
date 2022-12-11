using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.InserirProdutoFornecedorCommand;
using DesafioAutoGlass.APPLICATION.InputModels;
using DesafioAutoGlass.APPLICATION.Validations;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using Moq;
using Xunit;

namespace DesafioAutoGlass.UNITTESTS.Applications.Command
{
    public class InserirProdutoFornecedorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly InserirProdutoFornecedorCommandHandler _inserirProdutoFornecedorCommandHandler;

        public InserirProdutoFornecedorCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWork.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _unitOfWork.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _inserirProdutoFornecedorCommandHandler = new InserirProdutoFornecedorCommandHandler(_unitOfWork.Object);
        }

        [Fact]
        public async void EfetuarCadastroProdutoEFornecedor_QuandoExecutado_EfetuarCadastroComSucesso()
        {
            //Arrange
            _fornecedorRepository.Setup(x=>x.MaxIdAsync()).ReturnsAsync(5);
            var produtoFornecedor = new InserirProdutoFornecedorCommand{Fornecedor = new FornecedorInputModel()};
            //Act
            await _inserirProdutoFornecedorCommandHandler.Handle(produtoFornecedor,new CancellationToken());
            //Assert
            _unitOfWork.Verify(x=>x.BeginTransactionAsync(),Times.Once);
            _unitOfWork.Verify(x=>x.SaveChangesAsync(),Times.Once);
            _unitOfWork.Verify(x=>x.CommitAsync(),Times.Once);
        }

        [Theory]
        [InlineData("","2022-9-10 10:30:15", "2022-9-10 10:30:15","","")]
        [InlineData("","2022-9-10 10:30:15", "2022-9-10 10:30:15","","1111111111111111111")]
        public void EfetuarCadastroComProdutoFornecedorInvalido_RetornarExcecoesFluentValidations(string descricaoProduto,DateTime dataFabricacao,DateTime dataValidade,
            string descricaoFornecedor, string cnpj)
        {
            //Arrange
            var fornecedor = new FornecedorInputModel{DescricaoFornecedor = descricaoFornecedor, CNPJ = cnpj};
            var produtoFornecedor = new InserirProdutoFornecedorCommand{DescricaoProduto = descricaoFornecedor,
                DataFabricacao = dataFabricacao,
                DataValidade = dataValidade,
                Fornecedor = fornecedor};

            var inserirProdutoFornecedorCommandValidation = new InserirProdutoFornecedorCommandValidation();
            //Act
            var result = inserirProdutoFornecedorCommandValidation.Validate(produtoFornecedor);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x=>x.ErrorMessage).ToList();
            Assert.True(
                erros.Contains("Necessário informar a descrição do produto.") ||
                erros.Contains("Necessário informar a data de fabricação do produto.") ||
                erros.Contains("Necessário informar a data de validade do produto.") ||
                erros.Contains("Data de fabricação do produto não pode ser maior ou igual a data de vencimento.") ||
                erros.Contains("Necessário informar o CNPJ do fornecedor.") ||
                erros.Contains("CNPJ inválido."));
        }
    }
}