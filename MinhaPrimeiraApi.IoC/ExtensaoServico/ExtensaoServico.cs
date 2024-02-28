using Microsoft.Extensions.DependencyInjection;
using MinhaPrimeiraApi.Servicos.Interfaces;
using MinhaPrimeiraApi.Servicos.Servicos;

namespace MinhaPrimeiraApi.IoC.ExtensaoServico
{
    public static class ExtensaoServico
    {
        public static IServiceCollection RegistrarServicos(this IServiceCollection servicos)
        {
            servicos.AddScoped<IProdutoServico, ProdutoServico>();

            return servicos;
        }
    }
}