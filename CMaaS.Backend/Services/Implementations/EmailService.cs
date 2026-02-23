using MailKit.Net.Smtp;
using MimeKit;
using CMaaS.Backend.Services.Interfaces;

namespace CMaaS.Backend.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailVerificationAsync(string email, string fullName, string verificationToken, string verificationLink)
        {
            try
            {
                var gmailSettings = _configuration.GetSection("GmailSettings");
                var smtpHost = gmailSettings["SmtpHost"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(gmailSettings["SmtpPort"] ?? "587");
                var smtpUser = gmailSettings["SmtpUser"];
                var smtpPassword = gmailSettings["SmtpPassword"];
                var senderEmail = gmailSettings["SenderEmail"];
                var senderName = gmailSettings["SenderName"] ?? "SchemaFlow";

                if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPassword))
                {
                    _logger.LogError("Gmail SMTP credentials are not configured.");
                    return false;
                }

                var emailContent = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                                <h2 style='color: #007bff; text-align: center;'>Email Verification Required</h2>
                                <p>Hello <strong>{fullName}</strong>,</p>
                                <p>Thank you for registering with SchemaFlow! To complete your registration, please verify your email address by clicking the button below:</p>
                                <div style='text-align: center; margin: 30px 0;'>
                                    <a href='{verificationLink}' style='display: inline-block; padding: 12px 30px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; font-weight: bold;'>
                                        Verify Email
                                    </a>
                                </div>
                                <p style='color: #666; font-size: 14px;'>Or copy and paste this link in your browser:</p>
                                <p style='color: #666; font-size: 12px; word-break: break-all;'>{verificationLink}</p>
                                <p style='color: #999; font-size: 12px; margin-top: 30px;'>This verification link will expire in 24 hours.</p>
                                <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />
                                <p style='color: #999; font-size: 12px; text-align: center;'>© 2025 SchemaFlow. All rights reserved.</p>
                            </div>
                        </body>
                    </html>
                ";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(fullName, email));
                message.Subject = "Verify Your Email - SchemaFlow";

                var bodyBuilder = new BodyBuilder { HtmlBody = emailContent };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpUser, smtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation($"Verification email sent successfully to {email}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending verification email to {email}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email, string fullName, string resetToken, string resetLink)
        {
            try
            {
                var gmailSettings = _configuration.GetSection("GmailSettings");
                var smtpHost = gmailSettings["SmtpHost"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(gmailSettings["SmtpPort"] ?? "587");
                var smtpUser = gmailSettings["SmtpUser"];
                var smtpPassword = gmailSettings["SmtpPassword"];
                var senderEmail = gmailSettings["SenderEmail"];
                var senderName = gmailSettings["SenderName"] ?? "SchemaFlow";

                if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPassword))
                {
                    _logger.LogError("Gmail SMTP credentials are not configured.");
                    return false;
                }

                var emailContent = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                                <h2 style='color: #007bff; text-align: center;'>Password Reset Request</h2>
                                <p>Hello <strong>{fullName}</strong>,</p>
                                <p>We received a request to reset your password. Click the button below to set a new password:</p>
                                <div style='text-align: center; margin: 30px 0;'>
                                    <a href='{resetLink}' style='display: inline-block; padding: 12px 30px; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px; font-weight: bold;'>
                                        Reset Password
                                    </a>
                                </div>
                                <p style='color: #666; font-size: 14px;'>Or copy and paste this link in your browser:</p>
                                <p style='color: #666; font-size: 12px; word-break: break-all;'>{resetLink}</p>
                                <p style='color: #ff6b6b; font-size: 12px; margin-top: 20px;'><strong>Important:</strong> This reset link will expire in 1 hour.</p>
                                <p style='color: #999; font-size: 12px; margin-top: 15px;'>If you didn't request this password reset, please ignore this email.</p>
                                <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />
                                <p style='color: #999; font-size: 12px; text-align: center;'>© 2025 SchemaFlow. All rights reserved.</p>
                            </div>
                        </body>
                    </html>
                ";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(fullName, email));
                message.Subject = "Reset Your Password - SchemaFlow";

                var bodyBuilder = new BodyBuilder { HtmlBody = emailContent };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpUser, smtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation($"Password reset email sent successfully to {email}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending password reset email to {email}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendWelcomeEmailAsync(string email, string fullName)
        {
            try
            {
                var gmailSettings = _configuration.GetSection("GmailSettings");
                var smtpHost = gmailSettings["SmtpHost"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(gmailSettings["SmtpPort"] ?? "587");
                var smtpUser = gmailSettings["SmtpUser"];
                var smtpPassword = gmailSettings["SmtpPassword"];
                var senderEmail = gmailSettings["SenderEmail"];
                var senderName = gmailSettings["SenderName"] ?? "SchemaFlow";

                if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPassword))
                {
                    _logger.LogError("Gmail SMTP credentials are not configured.");
                    return false;
                }

                var emailContent = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                                <h2 style='color: #007bff; text-align: center;'>Welcome to SchemaFlow!</h2>
                                <p>Hello <strong>{fullName}</strong>,</p>
                                <p>Welcome to SchemaFlow! Your email has been verified and your account is now fully activated.</p>
                                <p>You can now:</p>
                                <ul style='color: #666;'>
                                    <li>Create and manage content types</li>
                                    <li>Manage your content entries</li>
                                    <li>Control content visibility</li>
                                    <li>Create and manage API keys</li>
                                    <li>View dashboard statistics</li>
                                </ul>
                                <p style='margin-top: 20px;'>If you have any questions or need assistance, feel free to contact our support team.</p>
                                <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />
                                <p style='color: #999; font-size: 12px; text-align: center;'>© 2025 SchemaFlow. All rights reserved.</p>
                            </div>
                        </body>
                    </html>
                ";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(fullName, email));
                message.Subject = "Welcome to SchemaFlow!";

                var bodyBuilder = new BodyBuilder { HtmlBody = emailContent };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpUser, smtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation($"Welcome email sent successfully to {email}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending welcome email to {email}: {ex.Message}");
                return false;
            }
        }
    }
}
