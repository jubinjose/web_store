using Store.Model;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Store.Service
{
    public class EmailService : IEmailService
    {
        public OpResult SendEmail(string from, string fromName, string to, string subject, string body)
        {
            const String HOST = "email-smtp.us-west-2.amazonaws.com";

            // The port you will connect to on the Amazon SES SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            const int PORT = 587;

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(from, fromName)
            };
            message.To.Add(new MailAddress(to));
            message.Subject = subject;

            //message.Body = body;

            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            // Create and configure a new SmtpClient
            SmtpClient client =
                new SmtpClient(HOST, PORT);
            // Pass SMTP credentials
            var awsCredsFromFile = File.ReadAllText(@"D:\jubin\creds\aws_creds.txt");
            var split = awsCredsFromFile.Split(',');
            client.Credentials =
                new NetworkCredential(split[0], split[1]);
            // Enable SSL encryption
            client.EnableSsl = true;

            // Send the email. 
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                return OpResult.ExceptionResult(ex);
            }

            return OpResult.SuccessResult();
        }
    }
}
