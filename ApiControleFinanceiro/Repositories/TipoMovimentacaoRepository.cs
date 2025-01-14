using ApiControleFinanceiro.Context;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;

namespace ApiControleFinanceiro.Repositories
{
    public class TipoMovimentacaoRepository : Repository<TipoMovimentacao>, ITipoMovimentacaoRepository
    {
        public TipoMovimentacaoRepository(AppDbContext context) : base(context) { }
    }
}
