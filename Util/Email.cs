using System.Net;
using System.Net.Mail;

namespace Util
{
    public class Email
    {
        public string From { get; set; }
        public List<string> ToList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Smtp { get; set; }
        public string NameToShow { get; set; }
    }

    public class Message
    {
        public  List<string> ToList { get; set;}
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class EmailSender
    {
        MailMessage message { get; set; }
        SmtpClient client { get; set; }
        Email Email { get; set; }  

        public string Result { get; set; }
        public EmailSender(Email email) 
        {
           Email =email; 
        }

        public bool Send() 
        {
            try
            {
                message = new MailMessage();
                message.From = new MailAddress(Email.From, Email.NameToShow);

                foreach (var to in Email.ToList)
                {
                    message.To.Add(to);
                }

                message.Subject = Email.Subject;
                message.IsBodyHtml = true;
                message.Body = Email.Body;

                client = new SmtpClient(Email.Smtp, Email.Port);
                client.EnableSsl = true; // Utilizar SSL para conexiones seguras
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Email.From, Email.Password);

                client.Send(message);

                Result = "Email sent";
                return true;
            }
            catch(Exception e)
            {
                Result= e.Message;
                return false;
            }
        }
    }
}