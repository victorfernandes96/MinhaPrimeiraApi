using MinhaPrimeiraApi.Repositorios.Contexto;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Repositorios.UoW
{

    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoDbContexto _contexto;

        public UnitOfWork(IMongoDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CommitAsync()
        {
            var qtdAlteracoes = await _contexto.SalvarAlteracoes();

            return qtdAlteracoes > 0;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}