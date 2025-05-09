using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ResumeScrenner
{
    public class EmailService
    {
        private string smtpServer;
        private int smtpPort;
        private string smtpUsername;
        private string smtpPassword;
        private string senderEmail;
        private string senderName;

        public EmailService(string server, int port, string username, string password, string email, string name)
        {
            smtpServer = "smtp.gmail.com";
            smtpPort = 587;
            smtpUsername = "sr.tom2005@gmail.com";
            smtpPassword = "tom&jerry";
            senderEmail = email;
            senderName = name;
        }

        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);

                mail.From = new MailAddress(senderEmail, senderName);
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;

                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mail);
                Logger.Log(LogLevel.Info, $"Email sent successfully to: {recipientEmail}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Error sending email to {recipientEmail}: {ex.Message}");
                return false;
            }
        }

        public bool SendCandidateStatusEmail(string candidateName, string recipientEmail, string status)
        {
            string subject = $"Resume Screener - Application {status}";

            string body = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; }}
                        .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='header'>
                        <h2>Resume Screener Notification</h2>
                    </div>
                    <div class='content'>
                        <p>Dear {candidateName},</p>
                        <p>Thank you for submitting your resume for our consideration.</p>
                        <p>We are pleased to inform you that your application has been <strong>{status}</strong>.</p>
                        <p>Our hiring team will contact you shortly with further details.</p>
                        <p>Best regards,<br/>The Hiring Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message. Please do not reply to this email.</p>
                    </div>
                </body>
                </html>";

            return SendEmailAsync(recipientEmail, subject, body).Result;
        }
    }
}