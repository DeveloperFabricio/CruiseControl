using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace CruiseControl.Application.Email
{
    public class EmailReserves
    {
        private readonly string _applicationName = "SeuAplicativo";
        private readonly string _senderEmailAddress = "seuemail@gmail.com"; 
        private readonly string _templateFilePath = "EmailTemplates/ReservaCarroEmailTemplate.html";

        public void SendReservationConfirmationEmail(string recipientEmail, ReservationDetails reservationDetails)
        {
            var service = GetGmailService();

            var message = CreateMessage(recipientEmail, reservationDetails);
            SendMessage(service, _senderEmailAddress, message);
        }

        private GmailService GetGmailService()
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { GmailService.Scope.MailGoogleCom },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        private Message CreateMessage(string recipientEmail, ReservationDetails reservationDetails)
        {
            var emailContent = File.ReadAllText(_templateFilePath);
            emailContent = ReplacePlaceholders(emailContent, reservationDetails);

            var message = new Message();
            message.Raw = Base64UrlEncode(emailContent);

            return message;
        }

        private string ReplacePlaceholders(string emailContent, ReservationDetails reservationDetails)
        {
            return emailContent
                .Replace("{NomeCliente}", reservationDetails.NomeCliente)
                .Replace("{ModeloCarro}", reservationDetails.ModeloCarro)
                .Replace("{DataRetirada}", reservationDetails.DataRetirada.ToString("dd/MM/yyyy"))
                .Replace("{DataDevolucao}", reservationDetails.DataDevolucao.ToString("dd/MM/yyyy"))
                .Replace("{LocalRetirada}", reservationDetails.LocalRetirada)
                .Replace("{LocalDevolucao}", reservationDetails.LocalDevolucao)
                .Replace("{PrecoTotal}", reservationDetails.PrecoTotal.ToString("C2"));
        }

        private void SendMessage(GmailService service, string userId, Google.Apis.Gmail.v1.Data.Message emailMessage)
        {
            service.Users.Messages.Send(emailMessage, userId).Execute();
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }

    public class ReservationDetails
    {
        public string NomeCliente { get; set; }
        public string ModeloCarro { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string LocalRetirada { get; set; }
        public string LocalDevolucao { get; set; }
        public decimal PrecoTotal { get; set; }
    }
}
