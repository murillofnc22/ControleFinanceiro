using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiControleFinanceiro.DTOs
{
    public class MovimentacaoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário informar a descrição da movimentação!")]
        [MaxLength(100)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "É necessário informar o valor da movimentação!")]
        [Precision(10, 2)]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "É necessário informar a Data de Vencimento da movimentação!")]        
        public DateTime DataPagamento { get; set; }
        [Required(ErrorMessage = "É necessário informar a Data da movimentação!")]
        public DateTime DataMovimentacao { get; set; }
        [Required(ErrorMessage = "É necessário informar o tipo da movimentação!")]
        public int TipoMovimentoId { get; set; }
        [Required(ErrorMessage = "É necessário informar o meio de pagamento da movimentação!")]
        public int MeioPagamentoId { get; set; }        
    }
}
