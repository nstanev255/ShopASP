using ShopASP.Models.Mail;

namespace ShopASP.Services;

public interface IMailService
{
    public Task SendEmail(Mail mail);
}