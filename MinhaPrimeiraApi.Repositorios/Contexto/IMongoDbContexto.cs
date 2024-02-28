using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Repositorios.Contexto
{
    public interface IMongoDbContexto : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SalvarAlteracoes();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}