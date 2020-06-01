using Limilabs.Client.IMAP;
using Limilabs.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace SMTP
{
     class Program
     {
          static void Main(string[] args)
          {
               SendMessage("irina.timus@ati.utm.md", "irina.timus@ati.utm.md", "21pink_roses", "Test", "Mesaj de testare");
               Console.ReadKey();
               ShowMessages("irina.timus@ati.utm.md", "21pink_roses");

               Console.ReadKey();
          }

          public static void SendMessage(string from, string to, string password, string subject, string message)
          {
               MailMessage mail = new MailMessage();
               
               mail.From = new MailAddress(from);
               mail.To.Add(to);
               mail.Subject = subject;
               mail.Body = "<h1>" + message + "</h1>";
               mail.IsBodyHtml = true;

               SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
                    
               smtp.Credentials = new NetworkCredential(from, password);
               smtp.EnableSsl = true;
               smtp.Send(mail);


               Console.WriteLine("Message was successfully sent to " + to);
               
          }

          public static void ShowMessages(string Email, string password)
          {
               Console.WriteLine("InboxMessages:");
               Imap imap = new Imap();
               imap.Connect("outlook.office365.com");
               imap.UseBestLogin(Email, password);
               imap.SelectInbox();
               List<long> uids = imap.Search(Flag.New);
               foreach (long uid in uids)
               {
                    IMail email = new MailBuilder().CreateFromEml(imap.GetMessageByUID(uid));
                    if (!email.Subject.Contains(".dll"))
                         Console.WriteLine(email.Subject);
               }
               imap.Close();
               
          }

     }
}
