using Newtonsoft.Json;

namespace ShopASP.Models.Mail;

public class MailPerson
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
}