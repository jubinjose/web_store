using Store.Model;

namespace Store.BLL.Interface
{
    public interface IEmailService
    {
        OpResult SendEmail(string from, string fromName, string to, string subject, string body);
    }
}
