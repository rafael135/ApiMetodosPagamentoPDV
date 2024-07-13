using ApiMetodosPagamento.Data.DAL;
using ApiMetodosPagamento.Data.DTOs;
using ApiMetodosPagamento.Models;
using ApiMetodosPagamento.Services.Interfaces;

namespace ApiMetodosPagamento.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentDAL _paymentDAL;

        public PaymentService(PaymentDAL paymentDAl)
        {
            _paymentDAL = paymentDAl;
        }

        public List<Payment> GetAllPayments()
        {
            return this._paymentDAL.GetAll();
        }

        public Payment? GetPaymentById(int id)
        {
            return this._paymentDAL.GetBy((p) => p.Id == id);
        }

        public Payment AddNewPayment(CreatePaymentDTO dto)
        {
            Payment payment = new Payment(dto.Name, dto.Method, dto.MinPrice, dto.CardTax, dto.MaxParcels);

            this._paymentDAL.Add(payment);

            return payment;
        }

        public bool UpdateExistingPayment(UpdatePaymentMethodDTO dto)
        {
            Payment? payment = this._paymentDAL.GetBy((p) => p.Id == dto.Id);

            if(payment is null)
            {
                return false;
            }

            payment.Name = dto.Name;
            payment.Method = dto.Method;
            payment.MinPrice = dto.MinPrice;
            payment.CardTax = dto.CardTax;
            payment.MaxParcels = dto.MaxParcels;

            this._paymentDAL.Update(payment);
            return true;
        }

        public bool DeletePayment(int id)
        {
            Payment? payment = this._paymentDAL.GetBy(p => p.Id == id);

            if (payment is null)
            {
                return false;
            }

            this._paymentDAL.Delete(payment);

            return true;
        }
    }
}
