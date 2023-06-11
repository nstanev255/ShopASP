using Microsoft.Extensions.Options;
using ShopASP.Configuration;

namespace ShopASP.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly MailConfiguration _mailConfiguration;

    public ConfigurationService(IOptions<MailConfiguration> mailConfiguration)
    {
        _mailConfiguration = mailConfiguration.Value;
    }

    public MailConfiguration MailConfiguration()
    {
        return _mailConfiguration;
    }

    public string GetShopEmail()
    {
        return _mailConfiguration.ShopEmail;
    }
}