using ArtifactSdkDotNet.Core;
using Newtonsoft.Json;

namespace ArtifactSdkDotNet.CardsetApi
{
    public class CardsetApiJsonResponse
    {
        [JsonProperty(PropertyName = "card_set")]
        public Cardset Cardset { get; private set; }
    }
}