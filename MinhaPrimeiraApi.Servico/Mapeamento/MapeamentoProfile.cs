using AutoMapper;
using MinhaPrimeiraApi.Entidades.Entidades;
using MinhaPrimeiraApi.Servicos.Dto;
using MongoDB.Bson;

namespace MinhaPrimeiraApi.Servicos.Mapeamento
{
    public class MapeamentoProfile : Profile
    {
        public MapeamentoProfile()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id.ToString()));

            CreateMap<ProdutoDto, Produto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => ObjectId.Parse(x.Id)));

            CreateMap<Produto, ProdutoInserirDto>()
                .ReverseMap();
        }
    }
}