using Newtonsoft.Json;
using ShopASP.Api.Model;

namespace ShopASP.Api;

public class GdbApi
{
    private readonly string _apiUrl =  "https://api.igdb.com/v4";
    private readonly string _clientId = "";
    private readonly string _authorization = "";

    private readonly string _gamesUrl = "/games";
    private readonly string _genresUrl = "/genres";
    private readonly HttpClient _httpClient;

    public GdbApi()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authorization}");
        _httpClient.DefaultRequestHeaders.Add("Client-Id", _clientId);
    }


    public async Task<List<GenreModel>?> GenreModels()
    {
        var content = new StringContent("fields *; limit 500;");
        var fullUrl = $"{_apiUrl}{_genresUrl}";
        var response = await _httpClient.PostAsync(fullUrl, content);
        Console.WriteLine("Response " + response.StatusCode);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<GenreModel>>(responseString);
    }

    public async Task<List<GameModel>?> Games(long offset)
    {
        var content =
            new StringContent(
                $"fields *; limit 500; offset {offset}; sort aggregated_rating desc; where aggregated_rating != null;");
        var fullUrl = $"{_apiUrl}{_gamesUrl}";
        var response = await _httpClient.PostAsync(fullUrl, content);

        Console.WriteLine("Response Games " + response.StatusCode);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<GameModel>>(responseString);

    }

}