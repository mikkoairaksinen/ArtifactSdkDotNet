using System;
using System.Threading.Tasks;
using ArtifactSdkDotNet.CardsetApi;
using ArtifactSdkDotNet.Core;
using Xunit;

namespace CardsetApiTests
{
    public class TestDownloadCardset
    {
        [Theory]
        [InlineData(ArtifactSdkDotNet.Config.CardSetApi.SetIds.BaseSet)]
        [InlineData(ArtifactSdkDotNet.Config.CardSetApi.SetIds.CallToArms)]
        public async Task SucceedDownloadAllCardsets(string setId)
        {
            var cardset = await CardsetApi.GetCardset(setId);
            
            Assert.NotNull(cardset);
            Assert.NotNull(cardset.SetInfo);
            Assert.NotEmpty(cardset.CardList);
        }
    }
}
