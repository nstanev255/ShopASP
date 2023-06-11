using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShopASP.Configuration;
using ShopASP.Models.Mail;

namespace ShopASP.Services;

public class MailService : IMailService
{
    private readonly MailConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public MailService(IOptions<MailConfiguration> configuration)
    {
        _configuration = configuration.Value;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("api-key", _configuration.ApiKey);
    }

    public async Task SendEmail(Mail mail)
    {
        var request = new MailRequest();
        request.Sender = new MailPerson { Email = _configuration.Email, Name = _configuration.Name };
        request.To = new MailPerson { Email = mail.Recepient };
        request.Subject = mail.Subject;
        request.HtmlContent = mail.Body;

        var jsonString = JsonConvert.SerializeObject(request);
        Console.WriteLine(jsonString);
        try
        {
            await _httpClient.PostAsJsonAsync(_configuration.Url, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("Error sending email.");
        }


    }
}