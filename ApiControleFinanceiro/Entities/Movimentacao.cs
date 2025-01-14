namespace ApiControleFinanceiro.Entities
{
    public class Movimentacao : Entity
    {
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataMovimentacao { get; set; }
        
        public int TipoMovimentoId { get; set; }
        public TipoMovimentacao? TipoMovimento { get; set; }
        public int MeioPagamentoId { get; set; }
        public MeioDePagamento? MeioPagamento { get; set; }
    }
}
