using CruiseControl.Application.GoogleCalendar.RabbitMQServiceCalendar;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;

namespace CruiseControl.Application.GoogleCalendar
{
    public class GoogleCalendarService
    {
        private readonly IConfiguration _configuration;
        private readonly RabbitMQService _rabbitMQService;

        const string CALENDAR = "primary";

        public GoogleCalendarService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<CalendarService> ConnectGoogleCalendar(string[] scopes)
        {
            try
            {
                string applicationName = "YourApplicationName";
                UserCredential credential;
                using (var stream = new FileStream("CredentialGoogleCalendar.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    );
                }

                var services = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName
                });

                return services;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Event>> GetEventsOnDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            try
            {
                var scopes = new[] { CalendarService.Scope.CalendarReadonly };
                var service = await ConnectGoogleCalendar(scopes);

                var startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
                var endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, DateTimeKind.Utc);

                var request = service.Events.List(CALENDAR);
                request.TimeMin = startTime;
                request.TimeMax = endTime;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                var eventsResult = await request.ExecuteAsync(cancellationToken);

                _rabbitMQService.PostMessage("Message to be sent to RabbitMQ");


                return eventsResult.Items.ToList();
            }
            catch (Exception)
            {
                Console.WriteLine($"Error when fetching events from Google Calendar.");
                return new List<Event>();
            }
        }

    }
}
