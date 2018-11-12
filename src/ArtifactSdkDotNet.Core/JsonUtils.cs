using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Utils
{
    public static class JsonUtils
    {

        public static T DeserializeFromArtifactApi<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, ArtifactApiSettings);
        }
        public static readonly JsonSerializerSettings ArtifactApiSettings = new JsonSerializerSettings()
        {
            ContractResolver = new NonPublicPropertiesResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        public class NonPublicPropertiesResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);
                if (member is PropertyInfo pi)
                {
                    prop.Readable = (pi.GetMethod != null);
                    prop.Writable = (pi.SetMethod != null);
                }
                return prop;
            }
        }

    }
}