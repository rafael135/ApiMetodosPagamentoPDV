using System.ComponentModel.DataAnnotations;
using ApiMetodosPagamento.Enums;

namespace ApiMetodosPagamento.Models;

public class Payment
{
    public int Id { get; set; }
    [MaxLength(80)]
    public string Name { get; set; }
    public MethodsEnum Method { get; set; }

    public double? MinPrice { get; set; } = null;

    public double? CardTax { get; set; } = null;
    public int? MaxParcels { get; set; } = null;

    public Payment()
    {

    }

    public Payment(string name, MethodsEnum method, double? minPrice, double? cardTax, int? maxParcels)
    {
        Name = name;
        Method = method;
        MinPrice = (method != MethodsEnum.Cash) ? minPrice : null;
        CardTax = (method != MethodsEnum.CreditCard) ? null : cardTax;
        MaxParcels = (method != MethodsEnum.CreditCard) ? null : maxParcels;
    }
}
