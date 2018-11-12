namespace ArtifactSdkDotNet.Config
{
    public static class CardSetApi
    {
        public static readonly string BaseUrl = "https://playartifact.com/cardset/";
        
        public class SetIds
        {
            public const string BaseSet = "00";
            public const string CallToArms = "01";

            public static readonly string[] All = {BaseSet, CallToArms};
        }
            
    }
}