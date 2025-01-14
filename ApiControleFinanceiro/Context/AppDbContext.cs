using System.Security.Cryptography.Xml;
using ApiControleFinanceiro.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiControleFinanceiro.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<InstituicaoFinanceira> InstituicaoFinanceiras { get; set; }
        public DbSet<MeioDePagamento> MeioDePagamentos { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<TipoDeConta> TipoDeContas { get; set; }
        public DbSet<TipoMovimentacao> TipoMovimentacoes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // aplica as configurações de mapeamento das entidades
            // do banco de dados contidas em uma determinada assembly
            // (conjunto de classes) ao objeto ModelBuilder durante a
            // criação do modelo.
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);            

            builder.Entity<Conta>().Property(c => c.Descricao).HasMaxLength(200).IsRequired();
            builder.Entity<Conta>().Property(c => c.InstituicaoId).IsRequired();
            builder.Entity<Conta>().Property(c => c.Saldo).HasPrecision(10, 2).IsRequired();
            builder.Entity<Conta>().Property(c => c.TipoContaId).IsRequired();

            builder.Entity<InstituicaoFinanceira>().Property(c => c.NomeInstituicao).HasMaxLength(100).IsRequired();
            builder.Entity<InstituicaoFinanceira>().HasData(
                new InstituicaoFinanceira { Id = 1, CodigoInstituicao = 341, NomeInstituicao = "Itaú"},
                new InstituicaoFinanceira { Id = 2, CodigoInstituicao = 104, NomeInstituicao = "Caixa" },
                new InstituicaoFinanceira { Id = 3, CodigoInstituicao = 260, NomeInstituicao = "Nubank" },
                new InstituicaoFinanceira { Id = 4, CodigoInstituicao = 77, NomeInstituicao = "Inter" },
                new InstituicaoFinanceira { Id = 5, NomeInstituicao = "PeerBR" }
                );

            builder.Entity<MeioDePagamento>().Property(c => c.Descricao).HasMaxLength(100).IsRequired();

            builder.Entity<Movimentacao>().Property(c => c.Descricao).HasMaxLength(100).IsRequired();
            builder.Entity<Movimentacao>().Property(c => c.Valor).HasPrecision(10, 2).IsRequired();
            builder.Entity<Movimentacao>().Property(c => c.TipoMovimentoId).IsRequired();
            builder.Entity<Movimentacao>().Property(c => c.MeioPagamentoId).IsRequired();

            builder.Entity<TipoDeConta>().Property(c => c.TipoConta).HasMaxLength(50).IsRequired();
            builder.Entity<TipoDeConta>().HasData(
                new TipoDeConta { Id = 1, TipoConta = "Conta Corrente" },
                new TipoDeConta { Id = 2, TipoConta = "Poupança" },
                new TipoDeConta { Id = 3, TipoConta = "Investimento" }
                );

            builder.Entity<TipoMovimentacao>().Property(c => c.Descricao).HasMaxLength(100);
            builder.Entity<TipoMovimentacao>().Property(c => c.Descricao).IsRequired();
            builder.Entity<TipoMovimentacao>().HasData(
                new TipoMovimentacao { Id = 1, Descricao = "Crédito", MovimentaSaldo = true },
                new TipoMovimentacao { Id = 2, Descricao = "Débito", MovimentaSaldo = true },
                new TipoMovimentacao { Id = 3, Descricao = "Transferência", MovimentaSaldo = false }
                );
        }
    }
}
