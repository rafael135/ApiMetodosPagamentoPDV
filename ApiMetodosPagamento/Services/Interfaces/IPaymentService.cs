using ApiMetodosPagamento.Data.DTOs;
using ApiMetodosPagamento.Models;

namespace ApiMetodosPagamento.Services.Interfaces
{
    public interface IPaymentService
    {
        public List<Payment> GetAllPayments();
        public Payment? GetPaymentById(int id);
        public Payment AddNewPayment(CreatePaymentDTO dto);
        public bool UpdateExistingPayment(UpdatePaymentMethodDTO dto);
        public bool DeletePayment(int id);
    }
}
