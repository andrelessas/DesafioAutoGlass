using DesafioAutoGlass.APPLICATION.Command.EditarProdutoCommand;
using DesafioAutoGlass.APPLICATION.Command.InserirProdutoCommand;
using DesafioAutoGlass.APPLICATION.Command.InserirProdutoFornecedorCommand;
using DesafioAutoGlass.APPLICATION.Command.RemoverProdutoCommand;
using DesafioAutoGlass.APPLICATION.Queries.ObterProdutoPorIdQueries;
using DesafioAutoGlass.APPLICATION.Queries.ObterProdutosQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAutoGlass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : MainController
    {
        public ProdutoController(IMediator mediator) : base(mediator) { }

        ///<summary>
        ///Retorna listagem de produtos.
        ///</summary>
        ///<param name = 'query'> Se informado, será consultado produtos por Status ou Descrição </param>
        /// ///<param name = 'page'> Pagina que será exibida </param>
        [HttpGet]
        public async Task<IActionResult> ObterAsync(string query, int page)
        {
            var produtos = await _mediator.Send(new ObterProdutosQueries(query, page), new CancellationToken());
            if (produtos == null)
                return NotFound();

            return Ok(produtos);
        }
        ///<summary>
        ///Retorna listagem de produtos.
        ///</summary>
        /// ///<param name = 'page'> Pagina que será exibida </param>
        [HttpGet("todos")]
        public async Task<IActionResult> ObterAsync(int page)
        {
            var produtos = await _mediator.Send(new ObterProdutosQueries("", page), new CancellationToken());
            if (produtos == null)
                return NotFound();

            return Ok(produtos);
        }

        ///<summary>
        ///Retornar produto por ID
        ///</summary>
        ///<param name = 'id'> Id do produto. </param>
        [HttpGet("porid")]
        public async Task<IActionResult> ObterPorIdAsync(int id)
        {
            var produto = await _mediator.Send(new ObterProdutoPorIdQueries(id), new CancellationToken());
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        ///<summary>
        /// Inserir novo produto e novo fornecedor
        ///</summary>
        ///<param name = 'inputModel'> Parametros para criação do produto. </param>
        [HttpPost("produto_fornecedor")]
        public async Task<IActionResult> InserirAsync(InserirProdutoFornecedorCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Inserir novo produto.
        ///</summary>
        ///<param name = 'inputModel'> Parametros para criação do produto. </param>
        [HttpPost]
        public async Task<IActionResult> InserirAsync(InserirProdutoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Alterar cadastro de produto.
        ///</summary>
        ///<param name = 'inputModel'> Parametros para alterar o produto. </param>
        [HttpPut]
        public async Task<IActionResult> AlterarAsync(EditarProdutoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Remover produto alterando o Status para INATIVO.
        ///</summary>
        ///<param name = 'id'> Id do produto que será removido. </param>
        [HttpPut("removerproduto")]
        public async Task<IActionResult> AlterarAsync(int id)
        {
            await _mediator.Send(new RemoverProdutoCommand(id));
            return Ok();
        }
    }
}