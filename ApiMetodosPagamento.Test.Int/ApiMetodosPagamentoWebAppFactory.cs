using ApiMetodosPagamento.Data;
using ApiMetodosPagamento.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMetodosPagamento.Test.Int
{
    public class ApiMetodosPagamentoWebAppFactory : WebApplicationFactory<Program>
    {
        public readonly ApiMetodosPagamentoContext _context;
        public ApiMetodosPagamentoWebAppFactory()
        {
            IServiceScope scope = this.Services.CreateScope();

            ApiMetodosPagamentoContext? context = scope.ServiceProvider.GetRequiredService<ApiMetodosPagamentoContext>();

            if(context is null)
            {
                throw new NullReferenceException();
            }

            this._context = context;
        }

        public Payment GetExistingPaymentOrCreate()
        {
            Payment? payment = this._context.Payments.FirstOrDefault();

            if(payment is null)
            {
                payment = new PaymentDataBuilder().Generate();

                this._context.Payments.Add(payment);
                this._context.SaveChanges();
            }

            return payment;
        }
    }
}
