using ApiMetodosPagamento.Models;

namespace ApiMetodosPagamento.Data.DAL
{
    public class PaymentDAL : GenericDAL<Payment>
    {
        public PaymentDAL(ApiMetodosPagamentoContext context) : base(context)
        {
            
        }
    }
}
