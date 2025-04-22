// Decompiled with JetBrains decompiler
// Type: TravelIT.Desktop.Repository.CustomEmail
// Assembly: TravelIT.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D324ECD1-D383-40BE-B0D0-5B5338374F3E
// Assembly location: Z:\TravellT\App\TravelIT.Desktop.exe


using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;


namespace CreditNote.Repository
{
  public class CustomEmail
  {
    private static string SMTPServer = Settings.Default.SMTPServer;
    private static string AdminEmail = Settings.Default.FromMail;
    private static string FriendlyName = Settings.Default.FromFriendlyName;
    private static string Port = Settings.Default.Port;
    //private static string Login = Settings.Default.EmailUser;
    //private static string EPassword = Settings.Default.EmailPassword;
    private static bool EnableSsl = Convert.ToBoolean(Settings.Default.EnableSsl);

        public static void SendMail(int mailID, string msg, string[,] att = null)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(AdminEmail, FriendlyName);
            var as1MailingList = new VitalDevContext().AS1_MailingList.Where(x => x.MailID == mailID).FirstOrDefault();
            mail.To.Add(as1MailingList.ToEmail);

            if (as1MailingList.CCEmail != null)
            {
                mail.CC.Add(as1MailingList.CCEmail);
            }

            if (as1MailingList.BCCEmail != null)
            {
                mail.Bcc.Add(as1MailingList.BCCEmail);
            }

            mail.Subject = "Vital Integration";
            if (att != null)
            {
                for (int index1 = 0; index1 < att.GetLength(0); ++index1)
                {
                    for (int index2 = 0; index2 < att.GetLength(1); index2 = index2 + 1 + 1)
                    {
                        var attachment = CreateAttachment(att[index1, index2], att[index1, index2 + 1]);
                        mail.Attachments.Add(attachment);
                    }
                }
            }
            mail.Body = msg;
            SMTPSend(mail);
        }

        public static Attachment CreateAttachment(string encryptedString, string fileName)
    {
      return new Attachment((Stream) new MemoryStream(Encoding.ASCII.GetBytes(encryptedString)), new ContentType("text/bzk"))
      {
        Name = fileName
      };
    }

    public static string Decrypt(string cipherText)
    {
      string password = "abc123";
      cipherText = cipherText.Replace(" ", "+");
      byte[] buffer = Convert.FromBase64String(cipherText);
      using (Aes aes = Aes.Create())
      {
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
        {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
        });
        aes.Key = rfc2898DeriveBytes.GetBytes(32);
        aes.IV = rfc2898DeriveBytes.GetBytes(16);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
          {
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.Close();
          }
          cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
        }
      }
      return cipherText;
    }

    private static void SMTPSend(MailMessage mail)
    {
      SmtpClient smtpClient = !(CustomEmail.Port == "") ? new SmtpClient(CustomEmail.SMTPServer, (int) Convert.ToInt16(CustomEmail.Port)) : new SmtpClient(CustomEmail.SMTPServer);
      //if (CustomEmail.Login != "" && CustomEmail.EPassword != "")
      //  smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(CustomEmail.Login, CustomEmail.Decrypt(CustomEmail.EPassword));
      smtpClient.EnableSsl = CustomEmail.EnableSsl;
      smtpClient.UseDefaultCredentials = false;
      smtpClient.Timeout = 300000;
      smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtpClient.Send(mail);
      mail.Dispose();
    }
  }
}
