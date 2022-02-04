using System.IO;
using Newtonsoft.Json;

public class JsonUtil
{
    public static string ToJson<T>(T obj)
    {
        JsonSerializer serializer = new JsonSerializer();
        StringWriter stringWriter = new StringWriter();

        using (JsonWriter writer = new JsonTextWriter(stringWriter))
        {
            serializer.Serialize(writer, obj);
        }

        return stringWriter.ToString();
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
