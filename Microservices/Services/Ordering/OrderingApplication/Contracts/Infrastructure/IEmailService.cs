using OrderingApplication.Models;

namespace OrderingApplication.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
