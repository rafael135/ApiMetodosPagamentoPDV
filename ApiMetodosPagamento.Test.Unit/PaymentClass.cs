using ApiMetodosPagamento.Models;

namespace ApiMetodosPagamento.Test.Unit
{
    public class PaymentClass
    {
        [Fact]
        public void CashMethod()
        {
            Payment payment = new Payment("À Vista", Enums.MethodsEnum.Cash, 40, 1.5, 8);


            // Assert
            Assert.Null(payment.MinPrice);
            Assert.Null(payment.MaxParcels);
            Assert.Null(payment.CardTax);
        }


        [Fact]
        public void CardMethod()
        {
            // Arrange
            double minPrice = 40;
            double cardTax = 1.6;
            int maxParcels = 24;


            Payment payment = new Payment("Cartão", Enums.MethodsEnum.CreditCard, minPrice, cardTax, maxParcels);

            // Assert
            Assert.Equal(minPrice, payment.MinPrice);
            Assert.Equal(cardTax, payment.CardTax);
            Assert.Equal(maxParcels, payment.MaxParcels);
        }
    }
}