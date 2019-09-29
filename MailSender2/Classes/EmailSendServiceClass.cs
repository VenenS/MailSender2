using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MailSender2.Classes
{
    class EmailSendServiceClass
    {
        #region vars
        private string strLogin; // email, c которого будет рассылаться почта
        private string strPassword; // пароль к email, с которого будет рассылаться почта
        private string strSmtp;// = "smtp.yandex.ru"; // smtp-server
        private int iSmtpPort;// = 25; // порт для smtp-server
        private string strBody; // текст письма для отправки
        private string strSubject; // тема письма для отправки
        #endregion
        public EmailSendServiceClass(string sLogin, string sPassword, string sBody, string sSubject,
            string sSmtp, int sPort)
        {
            strLogin = sLogin;
            strPassword = sPassword;
            strBody = sBody;
            strSubject = sSubject;
            iSmtpPort = sPort;
            strSmtp = sSmtp;
        }

        private void SendMail(string mail,string name) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody+"\nПисьмо от "+DateTime.Now;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                    View.SendEndWindow sendEnd = new View.SendEndWindow();
                    sendEnd.ShowDialog();
                }
                catch(Exception ex)
                {
                    View.ErorrWindow erorrWindow = new View.ErorrWindow();
                    erorrWindow.ShowDialog();
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());                   
                }
            }
        }

        public void SendMails(IQueryable<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendMail(email.Address, email.Name);
            }
        }
    }
}
