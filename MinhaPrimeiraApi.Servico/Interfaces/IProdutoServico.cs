using MinhaPrimeiraApi.Servicos.Dto;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        Task<IEnumerable<ProdutoDto>> ObterTodosProdutos();
        Task<ProdutoDto> ObterProdutoPorId(ObjectId id);
        Task<ProdutoInserirDto> SalvarProduto(ProdutoInserirDto novoProduto);
        Task<ProdutoDto> AlterarProduto(ProdutoDto produto);
        Task DeletarProdutoPorId(ObjectId id);
    }
}