using System.ComponentModel.DataAnnotations;
using ApiControleFinanceiro.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiControleFinanceiro.DTOs
{
    public class ContaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário informar uma descrição para a conta!")]
        [MaxLength(200)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "É necessário informar um saldo para a conta!")]
        [Precision(10, 2)]
        public decimal Saldo { get; set; }
        [Required(ErrorMessage = "É necessário informar a Instituição Financeira da conta!")]
        public int InstituicaoId { get; set; }
        [Required(ErrorMessage = "É necessário informar o tipo da conta!")]
        public int TipoContaId { get; set; }
    }
}