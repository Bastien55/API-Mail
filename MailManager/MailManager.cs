using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace API_Mail.MailManager
{
    public class MailManager
    {
        const string MAIL_SERV = "recipient@example.com";

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public SmtpClient Client { get; set; }

        private static MailManager _instance { get; set; }

        private MailManager() 
        {

        }

        public static MailManager Instance 
        { 
            get
            {
                if(_instance == null)
                    _instance = new MailManager();
                return _instance;
            } 
        }

        public void SetParam(string server, int port)
        {
            SmtpServer = server;
            SmtpPort = port;

            Client = new SmtpClient(SmtpServer, SmtpPort);
        }

        public void ReceiveData(string data)
        {
            if (data == null)
                return ;

            var mailUser = data.Split(',')[1].Split(':')[1].Trim();

            Console.WriteLine(mailUser);

            MailMessage mail = new MailMessage(MAIL_SERV, mailUser);

            mail.Subject = "Test";
            mail.Body = "Est ce bien vos données : " + data;

            Client.Send(mail);
        }
    }
}
