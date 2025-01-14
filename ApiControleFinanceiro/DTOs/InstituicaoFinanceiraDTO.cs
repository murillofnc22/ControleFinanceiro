using System.ComponentModel.DataAnnotations;

namespace ApiControleFinanceiro.DTOs
{
    public class InstituicaoFinanceiraDTO
    {
        public int Id { get; set; }
        public int CodigoInstituicao { get; set; }
        
        [Required(ErrorMessage = "É necessário informar o nome da instituição!")]
        [MaxLength(100)]
        public string? NomeInstituicao { get; set; }
    }
}
