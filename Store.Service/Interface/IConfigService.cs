namespace Store.Service
{
    public interface IConfigService
    {
        string GetJwtKey();

        string GetFromEmailAddress();

        string GetFromEmailName();

        string GetSmtpUser();

        string GetSmtpPassword();
    }
}
