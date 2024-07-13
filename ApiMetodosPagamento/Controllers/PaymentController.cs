using ApiMetodosPagamento.Data.DAL;
using ApiMetodosPagamento.Data.DTOs;
using ApiMetodosPagamento.Models;
using ApiMetodosPagamento.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiMetodosPagamento.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {


        /// <summary>
        /// Retorna um array com todos os métodos de pagamento registrados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso encontre um ou mais métodos de pagamento registrados</response>
        /// <response code="204">Caso não encontre um método de pagamento registrado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentMethods([FromServices] PaymentService paymentService)
        {
            List<Payment> payments = paymentService.GetAllPayments();

            if(payments.Count > 0)
            {
                return Ok(payments);
            }

            return NoContent();
        }

        /// <summary>
        /// Obtém o pagamento com id especificado
        /// </summary>
        /// <param name="id">chave primária do pagamento</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso encontre o método de pagamento</response>
        /// <response code="404">Caso não encontre o método de pagamento</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentById([FromServices] PaymentService paymentService, int id)
        {
            Payment? payment = paymentService.GetPaymentById(id);

            if(payment is null)
            {
                return NotFound();
            }

            return Ok(payment);
        }


        // XML que será utilizado pelo Swagger para gerar a documentação da API:
        /// <summary>
        /// Adiciona um novo método de pagamento
        /// </summary>
        /// <param name="paymentDto">Objeto com os campos necessários para criação de uma forma de pagamento</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod([FromServices] PaymentService paymentService, [FromBody] CreatePaymentDTO paymentDto)
        {
            Payment newPayment = paymentService.AddNewPayment(paymentDto);

            return CreatedAtAction(nameof(GetPaymentById), new { id = newPayment.Id }, newPayment);
        }


        /// <summary>
        /// Atualiza um método de pagamento já registrado
        /// </summary>
        /// <param name="paymentDto">Objeto DTO para atualizar a forma de pagamento</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso encontre o método de pagamento e o atualize com sucesso</response>
        /// <response code="404">Caso não encontre o método de pagamento</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod([FromServices] PaymentService paymentService, UpdatePaymentMethodDTO paymentDto)
        {
            bool result = paymentService.UpdateExistingPayment(paymentDto);

            if(result == true)
            {
                return Ok();
            }

            return NotFound();
        }


        /// <summary>
        /// Exclui um método de pagamento registrado
        /// </summary>
        /// <param name="id">Chave primária da forma de pagamento</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso encontre o método de pagamento e realize a sua exclusão</response>
        /// <response code="404">Caso não encontre o método de pagamento</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod([FromServices] PaymentService paymentService, int id)
        {
            bool result = paymentService.DeletePayment(id);

            if(result == true)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
