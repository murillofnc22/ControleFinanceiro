using System.ComponentModel.DataAnnotations;

namespace ApiControleFinanceiro.DTOs
{
    public class TipoDeContaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É preciso informar o tipo de conta!")]
        [MaxLength(50)]
        public string? TipoConta { get; set; }
    }
}