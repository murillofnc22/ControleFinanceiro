using ApiControleFinanceiro.Context;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;

namespace ApiControleFinanceiro.Repositories
{
    public class InstituicaoFinanceiraRepository : Repository<InstituicaoFinanceira>, IInstituicaoFinanceiraRepository
    {
        public InstituicaoFinanceiraRepository(AppDbContext context) : base(context) { }
    }
}
