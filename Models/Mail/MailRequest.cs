using Newtonsoft.Json;

namespace ShopASP.Models.Mail;

public class MailRequest
{
    [JsonProperty("sender")]
    public MailPerson Sender { get; set; }
    
    [JsonProperty("to")]
    public List<MailPerson> To { get; set; }
    
    [JsonProperty("subject")]
    public string Subject { get; set; }
    [JsonProperty("htmlContent")]
    public string HtmlContent { get; set; }
}