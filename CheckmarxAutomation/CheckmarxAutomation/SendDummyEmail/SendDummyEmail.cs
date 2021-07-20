using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CheckmarxAutomation.SendDummyEmail
{
    public static class SendDummyEmail
    {
        public static async Task<FileStreamResult> Email()
        {
            string dummyEmail = "test@localhost.com";

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(dummyEmail);
            mailMessage.To.Add("dejan.caric@gmail.com");
            mailMessage.Subject = "Test subject";
            mailMessage.Body = "Test body";

            // mark as draft
            mailMessage.Headers.Add("X-Unsent", "1");

            // download image and save it as attachment
            using (var httpClient = new HttpClient())
            {
                var imageStream = await httpClient.GetStreamAsync(new Uri("http://dcaric.com/favicon.ico"));
                mailMessage.Attachments.Add(new Attachment(imageStream, "favicon.ico"));
            }

            var stream = new MemoryStream();
            ToEmlStream(mailMessage, stream, dummyEmail);

            stream.Position = 0;
            
            var result = new FileStreamResult(stream, "message/rfc822")
            { FileDownloadName = "test_email.eml" };

            return result;
    }

    private static void ToEmlStream(MailMessage msg, Stream str, string dummyEmail)
        {
            using (var client = new SmtpClient())
            {
                var id = Guid.NewGuid();

                var tempFolder = Path.Combine(Path.GetTempPath(), Assembly.GetExecutingAssembly().GetName().Name);

                tempFolder = Path.Combine(tempFolder, "MailMessageToEMLTemp");

                // create a temp folder to hold just this .eml file so that we can find it easily.
                tempFolder = Path.Combine(tempFolder, id.ToString());

                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                client.UseDefaultCredentials = true;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = tempFolder;
                client.Send(msg);

                // tempFolder should contain 1 eml file
                var filePath = Directory.GetFiles(tempFolder).Single();

                // create new file and remove all lines that start with 'X-Sender:' or 'From:'
                string newFile = Path.Combine(tempFolder, "modified.eml");
                using (var sr = new StreamReader(filePath))
                {
                    using (var sw = new StreamWriter(newFile))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (!line.StartsWith("X-Sender:") &&
                                !line.StartsWith("From:") &&
                                // dummy email which is used if receiver address is empty
                                !line.StartsWith("X-Receiver: " + dummyEmail) &&
                                // dummy email which is used if receiver address is empty
                                !line.StartsWith("To: " + dummyEmail))
                            {
                                sw.WriteLine(line);
                            }
                        }
                    }
                }

                // stream out the contents
                using (var fs = new FileStream(newFile, FileMode.Open))
                {
                    fs.CopyTo(str);
                }
            }
        }
    }
}
