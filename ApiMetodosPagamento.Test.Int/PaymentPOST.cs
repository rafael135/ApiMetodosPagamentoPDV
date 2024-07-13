using ApiMetodosPagamento.Data.DTOs;
using ApiMetodosPagamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiMetodosPagamento.Test.Int
{
    public class PaymentPOST : IClassFixture<ApiMetodosPagamentoWebAppFactory>
    {
        private readonly ApiMetodosPagamentoWebAppFactory _appFactory;

        public PaymentPOST()
        {
            this._appFactory = new ApiMetodosPagamentoWebAppFactory();
        }


        [Fact]
        public async Task ShouldRegisterNewPaymentMethod()
        {
            // Arrange
            CreatePaymentDTO data = new CreatePaymentDTO()
            {
                Name = "Cartão de crédito",
                Method = Enums.MethodsEnum.CreditCard,
                CardTax = 0.05,
                MaxParcels = 12,
                MinPrice = 20
            };
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.Created;

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/payment", data);
            Payment? createdPayment = await response.Content.ReadFromJsonAsync<Payment>();


            // Assert
            Assert.NotNull(createdPayment);
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.Equal(createdPayment.Name, data.Name);
            Assert.Equal(createdPayment.Method, data.Method);
            Assert.Equal(createdPayment.CardTax, data.CardTax);
            Assert.Equal(createdPayment.MinPrice, data.MinPrice);
            Assert.Equal(createdPayment.MaxParcels, data.MaxParcels);
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenRegisteringInvalidPayment()
        {
            // Arrange
            CreatePaymentDTO data = new CreatePaymentDTO()
            {
                Name = "Cartão de crédito",
                CardTax = 0.05,
                MaxParcels = 12,
                MinPrice = 20
            };
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.BadRequest;

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("/payment", data);


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
