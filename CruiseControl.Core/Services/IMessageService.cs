namespace CruiseControl.Core.Services
{
    public interface IMessageService
    {
        void Publish(string queue, byte[] message);
    }
}
