using ArtifactSdkDotNet.Core;

namespace ArtifactSdkDotNet.CardsetApi
{
    public class CardsetApiResponse
    {
        public string CdnRoot { get; private set; }
        public string Url { get; private set; }
        public long ExpireTime { get; private set; }
    }
}