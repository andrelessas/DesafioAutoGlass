using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using DesafioAutoGlass.APPLICATION.Queries.ObterFornecedoresQueries;
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
    public class ObterFornecedoresQueriesHandlerTests : AutoMapperConfiguration
    {
        private readonly Mock<IUnitOfWork> _unitOfWorks;
        private readonly Mock<IFornecedorRepository> _fornecedorRepository;
        private readonly ObterFornecedoresQueriesHandler _obterFornecedoresQueriesHandler;
        private List<Fornecedor> _fornecedores;
        private PaginationResult<Fornecedor> _paginationResult;

        public ObterFornecedoresQueriesHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWork>();
            _fornecedorRepository = new Mock<IFornecedorRepository>();
            _unitOfWorks.SetupGet(x => x.Fornecedor).Returns(_fornecedorRepository.Object);
            _obterFornecedoresQueriesHandler = new ObterFornecedoresQueriesHandler(_unitOfWorks.Object, Mapper);
            _fornecedores = new List<Fornecedor>();
            _paginationResult = new PaginationResult<Fornecedor>();

            for (int i = 1; i <= 10; i++)
            {
                _fornecedores.Add(new Fornecedor(
                    new Faker("pt_BR").Company.CompanyName(),
                    new Faker("pt_BR").Company.Cnpj(true))
                    );
            }
        }

        [Fact]
        public async void ObterFornecedoresAtivos_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            for (int i = 0; i < _fornecedores.Count(); i++)
            {
                if(i % 2 == 0)
                    _fornecedores[i].RemoverFornecedor();
            }
            var fornecedoresAtivo = _fornecedores.Where(x=>x.Status == "ATIVO").ToList();            
            _paginationResult.Data = fornecedoresAtivo;
            _paginationResult.Page = 1;
            _fornecedorRepository.Setup(x=>x.GetAllAsync(It.IsAny<string>(),It.IsAny<int>())).ReturnsAsync(_paginationResult);
            //Act
            var result = await _obterFornecedoresQueriesHandler.Handle(new ObterFornecedoresQueries("",_paginationResult.Page),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(fornecedoresAtivo.Count(),result.Data.Count());
            for (int i = 0; i < fornecedoresAtivo.Count(); i++)
            {
                Assert.Equal(fornecedoresAtivo[i].Descricao,((FornecedorViewModel)result.Data.ElementAt(i)).DescricaoFornecedor);
                Assert.Equal(fornecedoresAtivo[i].CNPJ,((FornecedorViewModel)result.Data.ElementAt(i)).Cnpj);                
            }

        }

        [Fact]
        public async void ObterFornecedorPorDescricao_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var fornecedorPorDescricao = _fornecedores.Where(x=>x.Descricao == _fornecedores[5].Descricao).ToList();
            _paginationResult.Data = fornecedorPorDescricao;
            _paginationResult.Page = 1;
            _fornecedorRepository.Setup(x=>x.GetAllAsync(It.IsAny<string>(),It.IsAny<int>())).ReturnsAsync(_paginationResult);
            //Act
            var result = await _obterFornecedoresQueriesHandler.Handle(new ObterFornecedoresQueries("",_paginationResult.Page),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(fornecedorPorDescricao.Count(),result.Data.Count());
            for (int i = 0; i < fornecedorPorDescricao.Count(); i++)
            {
                Assert.Equal(fornecedorPorDescricao[i].Descricao,((FornecedorViewModel)result.Data.ElementAt(i)).DescricaoFornecedor);
                Assert.Equal(fornecedorPorDescricao[i].CNPJ,((FornecedorViewModel)result.Data.ElementAt(i)).Cnpj);                
            }

        }

        [Fact]
        public async void ObterFornecedorPorCNPJ_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var fornecedorPorCNPJ = _fornecedores.Where(x=>x.CNPJ == _fornecedores[5].CNPJ).ToList();
            _paginationResult.Data = fornecedorPorCNPJ;
            _paginationResult.Page = 1;
            _fornecedorRepository.Setup(x=>x.GetAllAsync(It.IsAny<string>(),It.IsAny<int>())).ReturnsAsync(_paginationResult);
            //Act
            var result = await _obterFornecedoresQueriesHandler.Handle(new ObterFornecedoresQueries("",_paginationResult.Page),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(fornecedorPorCNPJ.Count(),result.Data.Count());
            for (int i = 0; i < fornecedorPorCNPJ.Count(); i++)
            {
                Assert.Equal(fornecedorPorCNPJ[i].Descricao,((FornecedorViewModel)result.Data.ElementAt(i)).DescricaoFornecedor);
                Assert.Equal(fornecedorPorCNPJ[i].CNPJ,((FornecedorViewModel)result.Data.ElementAt(i)).Cnpj);                
            }

        }

        [Fact]
        public void ObterFornecedorQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterFornecedoresQueriesHandler.Handle(new ObterFornecedoresQueries("",1),new CancellationToken())).Result;
            Assert.Equal("Nenhum fornecedor encontrado.",result.Message);
            Assert.Equal(404,result.StatusCode);
        }

    }
}