using ApiControleFinanceiro.Context;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;

namespace ApiControleFinanceiro.Repositories
{
    public class MeioDePagamentoRepository : Repository<MeioDePagamento>, IMeioDePagamentoRepository
    {
        public MeioDePagamentoRepository(AppDbContext context) : base(context) { }
    }
}
