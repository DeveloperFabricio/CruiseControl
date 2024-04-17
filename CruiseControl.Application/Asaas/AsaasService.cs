using Microsoft.Extensions.Configuration;
using System.Text;

namespace CruiseControl.Application.Asaas
{
    public class AsaasService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public AsaasService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://www.asaas.com/api/v3/"); 
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["AsaasConfig:AccessToken"]}");
        }

        public async Task<string> CreateChargeAsync(string customerEmail, decimal amount, string description)
        {
            var requestContent = new StringContent(
                $"customer={customerEmail}&value={amount}&description={description}",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync("payments", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
           
            return "chargeId";
        }
    }
}
