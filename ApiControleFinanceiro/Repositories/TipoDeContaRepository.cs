using ApiControleFinanceiro.Context;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;

namespace ApiControleFinanceiro.Repositories
{
    public class TipoDeContaRepository : Repository<TipoDeConta>, ITipoDeContaRepository
    {
        public TipoDeContaRepository(AppDbContext context) : base(context) { }
    }
}
