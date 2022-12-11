using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Command.EditarFornecedorCommand;
using DesafioAutoGlass.APPLICATION.Command.InserirFornecedorCommand;
using DesafioAutoGlass.APPLICATION.Command.RemoverFornecedorCommand;
using DesafioAutoGlass.APPLICATION.Queries.ObterFornecedorPorIdQueries;
using DesafioAutoGlass.APPLICATION.Queries.ObterFornecedoresQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAutoGlass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : MainController
    {
        public FornecedorController(IMediator mediator)
            : base(mediator) { }

        ///<summary>
        ///Obter listagem de fornecedores
        ///</summary>
        ///<param name = 'query'> Se informado, será consultado fornecedores por Status, Descrição ou CNPJ </param>
        ///<param name = 'page'> Pagina atualiza da listagem </param>
        [HttpGet]
        public async Task<IActionResult> ObterAsync(string query, int page)
        {
            var fornecedores = await _mediator.Send(new ObterFornecedoresQueries(query, page), new CancellationToken());
            if (fornecedores == null)
                return NotFound();

            return Ok(fornecedores);
        }

        ///<summary>
        ///Obter listagem de fornecedores
        ///</summary>
        ///<param name = 'page'> Pagina atualiza da listagem </param>
        [HttpGet("todos")]
        public async Task<IActionResult> ObterAsync(int page)
        {
            var fornecedores = await _mediator.Send(new ObterFornecedoresQueries("", page), new CancellationToken());
            if (fornecedores == null)
                return NotFound();

            return Ok(fornecedores);
        }

        ///<summary>
        ///Obter fornecedor por Id
        ///</summary>
        ///<param name = 'id'> Id do fonecedor. </param>
        [HttpGet("porid")]
        public async Task<IActionResult> ObterPorIdAsync(int id)
        {
            var fornecedor = await _mediator.Send(new ObterFornecedorPorIdQueries(id), new CancellationToken());
            if (fornecedor == null)
                return NotFound();

            return Ok(fornecedor);
        }

        ///<summary>
        ///Inserir novo fornecedor.
        ///</summary>
        ///<param name = 'inputModel'> Parametros para criação do fornecedor. </param>
        [HttpPost]
        public async Task<IActionResult> InserirAsync(InserirFornecedorCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Alterar cadastro de fornecedor.
        ///</summary>
        ///<param name = 'inputModel'> Parametros para alterar o fornecedor. </param>
        [HttpPut]
        public async Task<IActionResult> AlterarAsync(EditarFornecedorCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Remover fornecedor alterando o Status para INATIVO.
        ///</summary>
        ///<param name = 'id'> Id do fornecedor que será removido. </param>
        [HttpPut("removerfornecedor")]
        public async Task<IActionResult> AlterarAsync(int id)
        {
            await _mediator.Send(new RemoverFornecedorCommand(id));
            return Ok();
        }
    }
}