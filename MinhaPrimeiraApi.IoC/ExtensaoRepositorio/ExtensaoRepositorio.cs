using Microsoft.Extensions.DependencyInjection;
using MinhaPrimeiraApi.Repositorios.Contexto;
using MinhaPrimeiraApi.Repositorios.Interfaces;
using MinhaPrimeiraApi.Repositorios.Repositorios;
using MinhaPrimeiraApi.Repositorios.UoW;

namespace MinhaPrimeiraApi.IoC.ExtensaoRepositorio
{
    public static class ExtensaoRepositorio
    {
        public static IServiceCollection RegistrarRepositorios(this IServiceCollection servicos)
        {
            servicos.AddScoped<IMongoDbContexto, MongoDbContexto>();
            servicos.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            servicos.AddScoped<IUnitOfWork, UnitOfWork>();

            return servicos;
        }
    }
}