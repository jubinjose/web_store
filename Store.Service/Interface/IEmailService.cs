using Store.Model;

namespace Store.Service
{
    public interface IEmailService
    {
        OpResult SendEmail(string from, string fromName, string to, string subject, string body);
    }
}
