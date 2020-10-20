using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AfriLearnBackend.Services
{
    public static  class EmailServices
    {
        public static  string SendEmailForPasswordRecoveryCode(string Email)
        {
            var generatedcode = GeneratePasswordRecoveryCode();
            try
            {
                var credentials = new NetworkCredential("reaiotorg@gmail.com", "magic@3.");
                var mail = new MailMessage()
                {
                    From = new MailAddress("reaiotorg@gmail.com"),
                    Subject = "Password Recovery",
                    Body = $"Your Password recovery code is {generatedcode}" ,
                    
                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(Email));
               var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);
                return generatedcode;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private  static string GeneratePasswordRecoveryCode()
        {
            var rand = new Random();
            var  code = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                var randomNum = rand.Next(0, 9);
                code.Append(randomNum);
            }
            return  code.ToString();
        }
    }
}
