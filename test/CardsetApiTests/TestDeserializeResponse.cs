using System;
using System.Linq;
using ArtifactSdkDotNet.CardsetApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Utils;
using Xunit;

namespace CardsetApiTests
{
    public class TestDeserializeResponse
    {
        [Theory]
        [InlineData("00.json")]
        [InlineData("01.json")]
        public void SucceedDeserializeCardSetWithoutDataLoss( string file )
        {
            var json = System.IO.File.ReadAllText($"../../../{file}");
            json = json.Replace("\n", "");
            var resp = JsonUtils.DeserializeFromArtifactApi<CardsetApiJsonResponse>(json);

            var json2 = JsonConvert.SerializeObject(resp, TestSettings);
            
            Assert.Equal(json, json2);
        }

        private JsonSerializerSettings TestSettings = new JsonSerializerSettings()
        {
            ContractResolver = new JsonUtils.NonPublicPropertiesResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy(),
            },
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
    }
}