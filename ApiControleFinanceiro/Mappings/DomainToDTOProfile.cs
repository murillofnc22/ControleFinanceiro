using ApiControleFinanceiro.DTOs;
using ApiControleFinanceiro.Entities;
using AutoMapper;

namespace ApiControleFinanceiro.Mappings
{
    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile() 
        {
            CreateMap<InstituicaoFinanceira, InstituicaoFinanceiraDTO>().ReverseMap();
            CreateMap<Conta, ContaDTO>().ReverseMap();
            CreateMap<MeioDePagamento, MeioDePagamentoDTO>().ReverseMap();
            CreateMap<TipoDeConta, TipoDeContaDTO>().ReverseMap();
            CreateMap<TipoMovimentacao, TipoMovimentacaoDTO>().ReverseMap();
            CreateMap<Movimentacao, MovimentacaoDTO>().ReverseMap();
        }
    }
}
