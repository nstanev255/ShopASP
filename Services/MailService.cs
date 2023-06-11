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
        request.To = new List<MailPerson> {new MailPerson { Email = mail.Recepient }};
        request.Subject = mail.Subject;
        request.HtmlContent = mail.Body;
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration.Url, request);
            if (!response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new Exception(responseString);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }


    }
}