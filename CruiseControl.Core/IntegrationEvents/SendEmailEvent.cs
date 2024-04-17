namespace CruiseControl.Core.IntegrationEvents
{
    public sealed record SendEmailEvent
    (
        string Origin,
        string Destiny,
        string Subject,
        string Body
     );
}
