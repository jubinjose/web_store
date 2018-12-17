using System;
using System.Configuration;

namespace Store.Service
{
    public class ConfigService : IConfigService
    {
        public static ConfigService _instance;
        private ConfigService() { }

        public static ConfigService GetInstance()
        {
            if (_instance == null) _instance = new ConfigService();
            return _instance;
        }


        public string GetFromEmailAddress()
        {
            return ConfigurationManager.AppSettings["from_email_address"];
        }

        public string GetFromEmailName()
        {
            return ConfigurationManager.AppSettings["from_email_name"];
        }

        public string GetJwtKey()
        {
            return ConfigurationManager.AppSettings["jwtkey"];
        }

        public string GetSmtpPassword()
        {
            return ConfigurationManager.AppSettings["smtp_password"];
        }

        public string GetSmtpUser()
        {
            return ConfigurationManager.AppSettings["smtp_user"];
        }
    }
}
