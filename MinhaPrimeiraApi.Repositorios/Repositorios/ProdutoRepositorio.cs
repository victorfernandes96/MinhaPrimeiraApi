using MinhaPrimeiraApi.Entidades.Entidades;
using MinhaPrimeiraApi.Repositorios.Contexto;
using MinhaPrimeiraApi.Repositorios.Interfaces;
using MinhaPrimeiraApi.Repositorios.Repositorios.Base;

namespace MinhaPrimeiraApi.Repositorios.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(IMongoDbContexto contexto) : base(contexto)
        {
        }
    }
}