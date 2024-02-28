using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MinhaPrimeiraApi.Servicos.Mapeamento;

namespace MinhaPrimeiraApi.IoC.ExtensaoMapeamento
{
    public static class ExtensaoMapeamento
    {
        public static IServiceCollection RegistrarMapeamentos(this IServiceCollection servicos)
        {
            MapperConfiguration mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapeamentoProfile());
            });

            servicos.AddSingleton(c => mapper.CreateMapper());

            return servicos;
        }
    }
}