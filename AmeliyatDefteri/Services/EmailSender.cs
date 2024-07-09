using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AmeliyatDefteri.Services
{
    public class EmailSender: IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.office365.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("d49377963@gmail.com", "9731.@.1379")
        };
 
        return client.SendMailAsync(
            new MailMessage(from: "d49377963@gmail.com",
                            to: email,
                            subject,
                            message
                            ));
        
    }
    }
}