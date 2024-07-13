using ApiMetodosPagamento.Data.DTOs;
using ApiMetodosPagamento.Enums;
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
    public class PaymentPUT : IClassFixture<ApiMetodosPagamentoWebAppFactory>
    {
        private readonly ApiMetodosPagamentoWebAppFactory _appFactory;

        public PaymentPUT()
        {
            this._appFactory = new ApiMetodosPagamentoWebAppFactory();
        }

        [Fact]
        public async Task ShouldReturnOkWhenUpdatingValidPayment()
        {
            // Arrange
            Payment validPayment = this._appFactory.GetExistingPaymentOrCreate();
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.OK;

            UpdatePaymentMethodDTO updatedData = new UpdatePaymentMethodDTO()
            {
                Id = validPayment.Id,
                Name = "Nome alterado",
                Method = Enums.MethodsEnum.CreditCard,
                CardTax = validPayment.CardTax,
                MaxParcels = 10,
                MinPrice = new Random().Next(5, 25)
            };

            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdatePaymentMethodDTO>("/payment", updatedData);


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenUpdatingExistingPaymentWithInvalidName()
        {
            // Arrange
            Payment validPayment = this._appFactory.GetExistingPaymentOrCreate();
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.BadRequest;

            UpdatePaymentMethodDTO updatedData = new UpdatePaymentMethodDTO()
            {
                Id = validPayment.Id,
                Name = "r",
                Method = Enums.MethodsEnum.CreditCard,
                CardTax = validPayment.CardTax,
                MaxParcels = 10,
                MinPrice = 20
            };

            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdatePaymentMethodDTO>("/payment", updatedData);


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }


        [Fact]
        public async Task ShouldReturnBadRequestWhenUpdatingExistingPaymentWithInvalidMethod()
        {
            // Arrange
            Payment validPayment = this._appFactory.GetExistingPaymentOrCreate();
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.BadRequest;

            UpdatePaymentMethodDTO updatedData = new UpdatePaymentMethodDTO()
            {
                Id = validPayment.Id,
                Name = "Nome Alterado",
                Method = Enum.Parse<MethodsEnum>("5"),
                CardTax = validPayment.CardTax,
                MaxParcels = 10,
                MinPrice = 20
            };

            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdatePaymentMethodDTO>("/payment", updatedData);


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }


        [Fact]
        public async Task ShouldReturnNotFoundWhenTryingToUpdateNonExistentPayment()
        {
            // Arrange
            Payment payment = new PaymentDataBuilder().Generate();
            UpdatePaymentMethodDTO data = new UpdatePaymentMethodDTO()
            {
                Id= 92083928,
                Name = payment.Name,
                Method = payment.Method,
                MinPrice = payment.MinPrice,
                CardTax = payment.CardTax,
                MaxParcels = payment.MaxParcels,
            };
            HttpClient client = this._appFactory.CreateClient();
            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;

            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdatePaymentMethodDTO>("/payment", data);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
