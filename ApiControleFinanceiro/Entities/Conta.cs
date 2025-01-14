namespace ApiControleFinanceiro.Entities
{
    public class Conta : Entity
    {
        public string? Descricao { get; set; }
        public decimal Saldo { get; set; }
        
        private DateTime _ultimaAtualizacao;
        public DateTime UltimaAtualizacao
        {
            get { return _ultimaAtualizacao; }
            set { _ultimaAtualizacao = DateTime.Now; }
        }
        public int InstituicaoId { get; set; }
        public InstituicaoFinanceira? Instituicao { get; set; }
        
        public int TipoContaId { get; set; }
        public TipoDeConta? TipoConta { get; set; }
    }
}
