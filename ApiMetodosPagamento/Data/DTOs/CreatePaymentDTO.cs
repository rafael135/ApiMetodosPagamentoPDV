using ApiMetodosPagamento.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiMetodosPagamento.Data.DTOs
{
    public class CreatePaymentDTO
    {
        [Required(ErrorMessage = "Nome não preenchido!")]
        [MinLength(2, ErrorMessage = "Nome para método de pagamento escolhido curto demais!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Método não escolhido!")]
        [Range(minimum: 1, maximum: 3, ErrorMessage = "Método de pagamento inválido!")]
        public MethodsEnum Method { get; set; }

        public double? MinPrice { get; set; } = null;

        public double? CardTax { get; set; } = null;
        public int? MaxParcels { get; set; } = null;
    }
}
