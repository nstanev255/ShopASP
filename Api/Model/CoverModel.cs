using Newtonsoft.Json;

namespace ShopASP.Api.Model;

public class CoverModel
{
    public int Id { get; set; }
    [JsonProperty("image_id")]
    public string ImageId { get; set; }
    public string Url { get; set; }
}