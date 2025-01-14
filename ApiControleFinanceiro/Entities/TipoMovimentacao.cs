namespace ApiControleFinanceiro.Entities
{
    public class TipoMovimentacao : Entity
    {
        public string? Descricao { get; set; }
        public bool MovimentaSaldo { get; set; }
    }
}
