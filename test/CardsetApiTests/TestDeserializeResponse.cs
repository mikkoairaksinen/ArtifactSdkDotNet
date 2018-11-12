using ArtifactSdkDotNet.CardsetApi;
using Utils;
using Xunit;

namespace CardsetApiTests
{
    public class TestDeserializeResponse
    {
        [Theory]
        [InlineData("00.json")]
        [InlineData("01.json")]
        public void SucceedDeserializeCardSet( string file )
        {
            var json = System.IO.File.ReadAllText($"../../../{file}");
            var cardset = JsonUtils.DeserializeFromArtifactApi<CardsetApiJsonResponse>(json).Cardset; 
            
            Assert.NotNull(cardset);
            Assert.NotNull(cardset.SetInfo);
            Assert.NotEmpty(cardset.CardList);
        }
        
    }
}