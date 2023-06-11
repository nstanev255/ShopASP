using Newtonsoft.Json;

namespace ShopASP.Constants;

public static class Constants
{
    public static Dictionary<string, string> Messages = LoadMessages();
    public static readonly int ItemsPerPage = 12;
    public static readonly string DOMAIN = "http://localhost:5276"; 

    /**
     * The generic categories are used to handle listing routes.
     * For example, if the user decides to go to the /playstation endpoint, then we will need to get
     * the playstation 4 and playstation 5 products. Using this constant we are able to map these values.
     */
    public static readonly string GENERIC_PC_CATEGORY = "pc";
    public static readonly string GENERIC_PLAYSTATION_CATEGORY = "playstation";
    public static readonly string GENERIC_XBOX_CATEGORY = "xbox";
    public static readonly string GENERIC_NINTENDO_CATEGORY = "nintendo";

    static Dictionary<string, string> LoadMessages()
    {
        using (StreamReader reader = new StreamReader("Constants/messages.json"))
        {
            string json = reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (result == null)
            {
                throw new Exception("Failed to load messages.");
            }

            return result;
        }
    }
}