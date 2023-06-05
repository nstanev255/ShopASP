using Newtonsoft.Json;

namespace ShopASP.Constants;

public static class Constants
{
    public static Dictionary<string, string> Messages = LoadMessages();
    public static readonly int ItemsPerPage = 12;

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