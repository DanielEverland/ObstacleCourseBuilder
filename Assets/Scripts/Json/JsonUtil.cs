using Newtonsoft.Json;

public class JsonUtil
{
    public static string ToJson<T>(T obj)
    {
        return JsonConvert.ToString(obj);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
