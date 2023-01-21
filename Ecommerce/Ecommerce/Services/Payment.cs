using Ecommerce.Models;
using Ecommerce.Models.Payment;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Services
{
    public class Payment : IPayment
    {
        public async Task<IFramesOfPayment> CheckCredit(double TotalAmount)
        {
            var AuthToken = await GetAuthToken();
            double totalAmount = TotalAmount*100;
            var getOrderId = await SecodStep(AuthToken.token , totalAmount);
            var thirdToken = await ThirdStep(AuthToken.token, getOrderId.id , totalAmount);
            var PaymentToken = thirdToken.token;
            var cardPayment = await CardPayment(PaymentToken);
            return new IFramesOfPayment
            {
                iFramMasterCard = cardPayment.iFramMasterCard,
                iFramVisa = cardPayment.iFramVisa,
            };
        }

        private async Task<authToken> GetAuthToken()
        {
            var URL = "https://accept.paymob.com/api/auth/tokens";
            var Api_key = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SnVZVzFsSWpvaWFXNXBkR2xoYkNJc0luQnliMlpwYkdWZmNHc2lPalkzTlRNd01pd2lZMnhoYzNNaU9pSk5aWEpqYUdGdWRDSjkuaG1YUXlaM2xBM1hua1AyVmNWZTA5Ym84ME95YjN6Mml3R0k4TkJzS1RJMUd3djdpX3NRUmZYV3F2SEJDOVJ6RVpTUzFwbDFFcHpBbkoydE05QnBHZlE=";
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
        private async Task<OrderResponse> SecodStep(string token , double totalAmount)
        {
            var URL = "https://accept.paymob.com/api/ecommerce/orders";
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
            var response = await httpClient.PostAsync(URL, httpContent);

            var Result = await response.Content.ReadAsStringAsync();

            OrderResponse ContentDto = JsonSerializer.Deserialize<OrderResponse>(Result);

            return new OrderResponse
            {
                id = ContentDto.id,

            };

        }
        private async Task<authToken> ThirdStep(string token, decimal id , double totalAmount)
        {
            var URL = "https://accept.paymob.com/api/acceptance/payment_keys";
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
                integration_id = 3295332

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
