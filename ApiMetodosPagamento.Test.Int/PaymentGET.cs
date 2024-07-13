using ApiMetodosPagamento.Models;
using System.Net;
using System.Net.Http.Json;

namespace ApiMetodosPagamento.Test.Int
{
    public class PaymentGET : IClassFixture<ApiMetodosPagamentoWebAppFactory>
    {
        private readonly ApiMetodosPagamentoWebAppFactory _appFactory;

        public PaymentGET()
        {
            this._appFactory = new ApiMetodosPagamentoWebAppFactory();
        }

        [Fact]
        public async Task ShouldReturnAllPaymentMethods()
        {
            // Arrange
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.OK;


            // Act
            HttpResponseMessage response = await client.GetAsync("/payment");
            IEnumerable<Payment>? payments = await response.Content.ReadFromJsonAsync<IEnumerable<Payment>>();


            // Assert
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.NotNull(payments);
        }


        [Fact]
        public async Task ShouldReturnCorrectPaymentById()
        {
            // Arrange
            Payment expectedPayment = this._appFactory.GetExistingPaymentOrCreate();
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.OK;


            // Act
            HttpResponseMessage response = await client.GetAsync($"/payment/{expectedPayment.Id}");
            Payment? payment = await response.Content.ReadFromJsonAsync<Payment>();


            // Assert
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.NotNull(payment);
            Assert.Equal(expectedPayment.Name, payment.Name);
            Assert.Equal(expectedPayment.Method, payment.Method);
            Assert.Equal(expectedPayment.MinPrice, payment.MinPrice);
            Assert.Equal(expectedPayment.MaxParcels, payment.MaxParcels);
            Assert.Equal(expectedPayment.CardTax, payment.CardTax);
        }
    }
}