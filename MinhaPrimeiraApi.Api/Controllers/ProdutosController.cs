using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Servicos.Dto;
using MinhaPrimeiraApi.Servicos.Interfaces;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoServico _produtoServico;

        public ProdutosController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ObterTodosProdutos()
        {
            var produtos = await _produtoServico.ObterTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ProdutoDto>> ObterProdutoPorId(string id)
        {
            var produto = await _produtoServico.ObterProdutoPorId(ObjectId.Parse(id));
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoInserirDto>> SalvarProduto(ProdutoInserirDto novoProduto)
        {
            var produtoInserido = await _produtoServico.SalvarProduto(novoProduto);
            return Ok(produtoInserido);
        }

        [HttpPut]
        public async Task<ActionResult<ProdutoDto>> AlterarProduto(ProdutoDto produto)
        {
            var produtoAlterado = await _produtoServico.AlterarProduto(produto);
            return Ok(produtoAlterado);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeletarProdutoPorId(string id)
        {
            await _produtoServico.DeletarProdutoPorId(ObjectId.Parse(id));
            
            return Ok();
        }
    }
}