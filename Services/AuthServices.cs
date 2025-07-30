using BusinessObject;
using BusinessObject.Models;
using DataAccessLayer.RequestDTO;
using DataAccessLayer.ResponseDTO;
using Microsoft.EntityFrameworkCore;
using Services.Implements;
using System.Net;
using System.Net.Mail;

namespace Services
{
    public class AuthServices : IAuthServices
    {
        private readonly ApplicationDbContext dbContext;
        public AuthServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<AccountUpdateRes> GetAllAccounts()
        {
            var accounts = dbContext.Accounts
                .Include(a => a.User)
                .Where(a => !a.IsDelete)
                .Select(a => new AccountUpdateRes
                {
                    ID = a.ID,
                    IsDelete = a.IsDelete,
                    PhoneNumber = a.PhoneNumber,
                    Email = a.Email,
                    User = new UserUpdateRes
                    {
                        Role = a.User.Role,
                        DOB = a.User.DOB,
                        IsMale = a.User.IsMale
                    }
                })
                .ToList();

            if (accounts.Count() == 0 || !accounts.Any())
            {
                throw new Exception("No accounts found.");
            }

            return accounts;
        }

        public UserRes Login(LoginRequest loginRequest)
        {
            var account = dbContext.Accounts
                .Include(a => a.User)
                .Where(a => a.UserName == loginRequest.UserName && a.Password == loginRequest.Password && !a.IsDelete)
                .Select(b => new UserRes
                {
                    ID = b.User.ID,
                    Role = b.User.Role,
                    FullName = b.User.FullName,
                    CitizenId = b.User.CitizenId,
                    DOB = b.User.DOB,
                    AvtUrl = b.User.AvtUrl,
                    IsMale = b.User.IsMale
                })
                .FirstOrDefault();

            if (account == null) throw new Exception("Invalid username or password.");

            return account;
        }

        public AccountUpdateRes Register(RegisterReq req)
        {
            var existingAccount = dbContext.Accounts
                .FirstOrDefault(a => a.UserName == req.UserName || a.Email == req.Email || a.PhoneNumber == req.PhoneNumber);
            if (existingAccount != null) throw new Exception("Your information is already exist in the system");

            if (req.Password != req.ConfirmPassword) throw new Exception("Your confirm password is not match the password");

            var u = new User
            {
                FullName = req.Name,
                IsMale = req.IsMale,
                DOB = req.DOB,
                AvtUrl = "",
                CitizenId = "",
            };

            dbContext.Users.Add(u);
            dbContext.SaveChanges();

            var account = new Account
            {
                UserName = req.UserName,
                Password = req.Password,
                PhoneNumber = req.PhoneNumber,
                Email = req.Email,
                UserId = u.ID
            };

            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();

            u.AccountId = account.ID;
            dbContext.Users.Update(u);
            dbContext.Accounts.Update(account);

            var accountRes = new AccountUpdateRes
            {
                ID = account.ID,
                IsDelete = account.IsDelete,
                PhoneNumber = account.PhoneNumber,
                Email = account.Email,
                User = new UserUpdateRes
                {
                    FullName = u.FullName,
                    Role = u.Role,
                    DOB = u.DOB,
                    IsMale = u.IsMale
                }
            };

            return accountRes;
        }

        public void DeleteAccount(int id)
        {
            var account = dbContext.Accounts
                .Where(b => b.ID == id && !b.IsDelete)
                .FirstOrDefault();

            if (account == null) throw new Exception("Account not found or already deleted.");

            account.IsDelete = true;
            dbContext.Accounts.Update(account);
            dbContext.SaveChanges();
        }

        public void ResetPassword(ResetPassReq req)
        {
            var u = dbContext.Accounts
                .Include(a => a.User)
                .FirstOrDefault(a => a.UserName == req.UserName
                && a.PhoneNumber == req.PhoneNumber
                && a.User.CitizenId == req.CitizenId
                && a.User.DOB == req.DOB
                && a.Email == req.Email
                && !a.IsDelete);

            if (u == null) throw new Exception("User not exist or information is not correct");
            var newPassword = GenerateRandomPassword();

            u.Password = newPassword;
            dbContext.SaveChangesAsync();

            SendEmailAsync(u.Email, "Reset password request", BuildResetPasswordEmailBody(u.UserName, newPassword));
        }

        private string GenerateRandomPassword(int length = 6)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            var rand = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromEmail = "bda2k3@gmail.com";
            var fromPassword = "buxi vval vqdf myls";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
        private string BuildResetPasswordEmailBody(string userName, string newPassword)
        {
            return $@"
                    <html>
                        <head>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                background-color: #f9f9f9;
                padding: 20px;
                color: #333;
            }}
            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                max-width: 600px;
                margin: auto;
            }}
            .highlight {{
                color: #0056b3;
                font-weight: bold;
            }}
            .footer {{
                margin-top: 30px;
                font-size: 12px;
                color: #888;
            }}
            </style>
                </head>
                     <body>
                        <div class='container'>
                            <h2>🔐 Yêu cầu đặt lại mật khẩu</h2>
                            <p>Xin chào <span class='highlight'>{userName}</span>,</p>
                            <p>Bạn hoặc ai đó đã yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</p>
                            <p>Mật khẩu mới của bạn là:</p>
                            <p style='font-size: 18px; font-weight: bold; color: #d9534f;'>{newPassword}</p>
                            <p>Hãy đăng nhập và đổi mật khẩu ngay để đảm bảo an toàn.</p>
                            <p class='footer'>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này hoặc liên hệ bộ phận hỗ trợ.</p>
                        </div>
                    </body>
        </html>";
        }
    }
}
