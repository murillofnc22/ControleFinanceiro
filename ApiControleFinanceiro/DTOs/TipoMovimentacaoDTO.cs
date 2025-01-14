using System.ComponentModel.DataAnnotations;

namespace ApiControleFinanceiro.DTOs
{
    public class TipoMovimentacaoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário informar uma descrição para a movimentação!")]
        [MaxLength(100)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "É necessário informar se movimenta saldo!")]
        public bool MovimentaSaldo { get; set; }
    }
}
