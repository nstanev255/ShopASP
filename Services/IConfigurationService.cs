using ShopASP.Configuration;

namespace ShopASP.Services;

public interface IConfigurationService
{
    public MailConfiguration MailConfiguration();
    public string GetShopEmail();
}