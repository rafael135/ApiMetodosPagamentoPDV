using ApiMetodosPagamento.Enums;
using ApiMetodosPagamento.Models;
using Bogus;

namespace ApiMetodosPagamento.Test.Int
{
    public class PaymentDataBuilder : Faker<Payment>
    {
        public string? Name { get; set; }
        public MethodsEnum? Method { get; set; }
        public double? MinPrice { get; set; }
        public double? CardTax { get; set; }
        public int? MaxParcels { get; set; }

        public PaymentDataBuilder()
        {
            CustomInstantiator(f =>
            {
                string name = this.Name ?? f.Commerce.ProductName();
                MethodsEnum method = this.Method ?? f.PickRandom<MethodsEnum>();
                double minPrice = this.MinPrice ?? f.Random.Double() * f.Random.Int(5, 25);
                double cardTax = this.CardTax ?? f.Random.Double() * 50;
                int maxParcels = this.MaxParcels ?? f.Random.Int(4, 24);

                return new Payment(name, method, minPrice, cardTax, maxParcels);
            });
        }

        public Payment Generate => Generate();
    }
}
