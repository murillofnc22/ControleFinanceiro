using System.ComponentModel.DataAnnotations;

namespace ApiControleFinanceiro.DTOs
{
    public class MeioDePagamentoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A descrição do Meio de Pagamento deve ser informada!")]
        [MaxLength(100)]
        public string? Descricao { get; set; }
    }
}
