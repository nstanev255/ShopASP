using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ShopASP.Api.Model;

public class GameModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("category")]
    public int Category { get; set; }
    
    [JsonProperty("cover")]
    public int Cover { get; set; }
    
    [JsonProperty("first_release_date")]
    public long FirstReleaseDate { get; set; }
    
    [JsonProperty("aggregated_rating")]
    public double AggregatedRating { get; set; }
    
    [JsonProperty("genres")]
    public List<int>? Genres { get; set; }
    
    [JsonProperty("involved_companies")]
    public List<int>? InvolvedCompanies { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("platforms")]
    public List<int>? Platforms { get; set; }
    
    [JsonProperty("release_dates")]
    public List<long>? ReleaseDates { get; set; }
    
    [JsonProperty("screenshots")]
    public List<long>? Screenshots { get; set; }
    
    [JsonProperty("summary")]
    public string Summary { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Category)}: {Category}, {nameof(Cover)}: {Cover}, {nameof(FirstReleaseDate)}: {FirstReleaseDate}, {nameof(AggregatedRating)}: {AggregatedRating}, {nameof(Genres)}: {Genres}, {nameof(InvolvedCompanies)}: {InvolvedCompanies}, {nameof(Name)}: {Name}, {nameof(Platforms)}: {Platforms}, {nameof(ReleaseDates)}: {ReleaseDates}, {nameof(Screenshots)}: {Screenshots}, {nameof(Summary)}: {Summary}";
    }
}