using ApiMetodosPagamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiMetodosPagamento.Test.Int
{
    public class PaymentDELETE : IClassFixture<ApiMetodosPagamentoWebAppFactory>
    {
        private readonly ApiMetodosPagamentoWebAppFactory _appFactory;

        public PaymentDELETE()
        {
            this._appFactory = new ApiMetodosPagamentoWebAppFactory();
        }


        [Fact]
        public async Task ShouldReturnOkWhenDeletingValidPayment()
        {
            // Arrange
            Payment? validPayment = this._appFactory.GetExistingPaymentOrCreate();
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.OK;


            // Act
            HttpResponseMessage response = await client.DeleteAsync($"/payment/{validPayment.Id}");


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.Null(this._appFactory._context.Payments.FirstOrDefault(p => p.Id == validPayment.Id));
        }

        
        [Fact]
        public async Task ShouldReturnNotFoundWhenDeletingInvalidPayment()
        {
            // Arrange
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;

            // Act
            HttpResponseMessage response = await client.DeleteAsync("/payment/999999");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
