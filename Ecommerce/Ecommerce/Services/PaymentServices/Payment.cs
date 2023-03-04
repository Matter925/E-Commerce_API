using Ecommerce.Data;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Dtos;
using Ecommerce.Models;
using Ecommerce.Models.Payment;
using Ecommerce.Repositories.IRepository;
using Ecommerce.Repositories.OrderServices;
using Ecommerce.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Services
{
    public class Payment : IPayment
    {
        private readonly PaymentSettings _paymentSettings;
        private readonly ApplicationDbContext _context;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        private readonly IMailingService _mailingService;
        public Payment(IOptions<PaymentSettings> paymentSettings, ApplicationDbContext context, IOrderRepository orderRepository, IMailingService mailingService , ICartRepository cartRepository)
        {
            _paymentSettings = paymentSettings.Value;
            _context = context;
            _cartRepository = cartRepository;
           _orderRepository = orderRepository;
            
            _mailingService = mailingService;
        }

        public async Task<GeneralRetDto> PaymentCallback(ResponsePayment data)
        {
            var PaymentOrderId = data.obj.order.id;
            var transactionId = data.obj.id;
            var amount = data.obj.amount_cents;
            var success = data.obj.success;
            if (success)
            {
                // Payment is successful, update order status, send confirmation email, etc.
                var orderInfo = await _orderRepository.GetByPaymentId(PaymentOrderId);
                var order = await _orderRepository.UpdateOrderStatus(PaymentOrderId);
                if (order.Success)
                {
                    
                        //*********** Send Mail To User **********************************
                        string subject = "تأكيد الطلب";
                       string body = $"  تم الدفع بنجاح مبلغ : {orderInfo.TotalAmount}";
                            
                        var user = await _context.Carts.Include(c => c.User).SingleOrDefaultAsync(d => d.Id == orderInfo.CartId);

                        await _mailingService.SendEmailAsync(user.User.Email, subject, body);
                        await _cartRepository.DeleteAll(user.Id);
                        return new GeneralRetDto
                        {
                            Message = "Succssefully",
                            Success = true,

                        };

                }
                return new GeneralRetDto
                {
                    Message = "Faild in update order status",
                    Success = false,

                };


            }

            // Payment is unsuccessful, update order status, send failure notification email, etc.

            return new GeneralRetDto
            {
                Message = "payment faild",
                Success = false,

            };
        }

        //------------------------------*****************************-------------------------------------------------------

        public async Task<IFramesOfPayment> CheckCredit(int CartId)
        {
            var Api_key = _paymentSettings.Api_key;
            var URLToken = _paymentSettings.URLToken;
            var URLOrder = _paymentSettings.URLOrder;
            var URLPayKey = _paymentSettings.URLPayKey;
            int Integration_Id = _paymentSettings.Integration_Id;

            var cartItem = await _cartRepository.GetItems(CartId);
            if (!cartItem.Items.Any())
            {
                return new IFramesOfPayment
                {
                    Message = "The Cart is empty !!",
                    Success = false,
                };
            };
            var cart = await _context.Carts.FindAsync(CartId);

            double totalAmount = cartItem.Total * 100;

            //----------------------------------------------------------------------------------------------

            var AuthToken = await GetAuthToken(Api_key, URLToken);

            var getOrderId = await SecodStep(AuthToken.token, totalAmount, URLOrder);

            //--------------addNewOrder--------------------------------------------------------------------------

            var order = new OrderDto
            {
                UserId = cart.UserId,
                TotalAmount = cartItem.Total,
                OrderDate = DateTime.Now,
                OrderStatus = false,
                CartId = CartId,
                PaymentOrderId = (int)getOrderId.id,
            };
            var addorder = await _orderRepository.CreateOrder(order);
            if (!addorder.Success)
            {
                return new IFramesOfPayment
                {
                    Message = addorder.Message,
                    Success = false,
                };
            };

            //------------------------------------------------------------------------------------------------------
            var thirdToken = await ThirdStep(AuthToken.token, getOrderId.id, totalAmount, URLPayKey, Integration_Id);
            var PaymentToken = thirdToken.token;
            var cardPayment = await CardPayment(PaymentToken);
            return new IFramesOfPayment
            {
                Message = "Successfully",
                Success = true,
                iFramMasterCard = cardPayment.iFramMasterCard,
                iFramVisa = cardPayment.iFramVisa,
            };
        }



        //**************------------------------------------------------------------------------------------------------------

        private async Task<authToken> GetAuthToken(string Api_key, string URL)
        {


            var json = new
            {
                api_key = Api_key
            };
            var jsonString = JsonSerializer.Serialize(json);
            HttpClient httpClient = new HttpClient();
            StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(URL, httpContent);

            var Result = await response.Content.ReadAsStringAsync();
            authToken ContentDto = JsonSerializer.Deserialize<authToken>(Result);

            return new authToken
            {
                token = ContentDto.token,
            };


        }
        private async Task<OrderResponse> SecodStep(string token, double totalAmount, string URLOrder)
        {

            var json = new
            {
                auth_token = token,
                delivery_needed = false,
                amount_cents = totalAmount,
                currency = "EGP",
                items = new List<object> { },

            };
            var jsonString = JsonSerializer.Serialize(json);
            HttpClient httpClient = new HttpClient();
            StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(URLOrder, httpContent);

            var Result = await response.Content.ReadAsStringAsync();

            OrderResponse ContentDto = JsonSerializer.Deserialize<OrderResponse>(Result);

            return new OrderResponse
            {
                id = ContentDto.id,

            };

        }
        private async Task<authToken> ThirdStep(string token, decimal id, double totalAmount, string URLPayKey, int Integration_Id)
        {

            var json = new
            {
                auth_token = token,
                amount_cents = totalAmount,
                expiration = 3600,
                order_id = id,
                billing_data = new
                {
                    apartment = "803",
                    email = "claudette09@exa.com",
                    floor = "42",
                    first_name = "Clifford",
                    street = "Ethan Land",
                    building = "8028",
                    phone_number = "+86(8)9135210487",
                    shipping_method = "PKG",
                    postal_code = "01898",
                    city = "Jaskolskiburgh",
                    country = "CR",
                    last_name = "Nicolas",
                    state = "Utah"
                },
                currency = "EGP",
                integration_id = Integration_Id

            };
            var jsonString = JsonSerializer.Serialize(json);
            HttpClient httpClient = new HttpClient();
            StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(URLPayKey, httpContent);

            var Result = await response.Content.ReadAsStringAsync();

            authToken ContentDto = JsonSerializer.Deserialize<authToken>(Result);

            return new authToken
            {
                token = ContentDto.token,

            };
        }

        private async Task<IFramesOfPayment> CardPayment(string TokenPayment)
        {
            var iFramMasterCard = $"https://accept.paymob.com/api/acceptance/iframes/722271?payment_token={TokenPayment} ";
            var iFramVisa = $"https://accept.paymob.com/api/acceptance/iframes/722272?payment_token={TokenPayment} ";

            return new IFramesOfPayment
            {
                iFramMasterCard = iFramMasterCard,
                iFramVisa = iFramVisa,
            };


        }

        
    }
}
