using ArtifactSdkDotNet.Core;
using ArtifactSdkDotNet.DeckCode;
using Newtonsoft.Json;
using Xunit;

namespace DeckCodeTests
{
    public class TestEncode
    {
        [Fact]
        public void SucceedEncodeDeck()
        {
            string encoded = DeckCodeEncoder.EncodeDeck(TestData.ExampleDeck1);
            Assert.Equal(TestData.ExampleDeck1Code, encoded);
        }

        [Fact]
        public void SucceedTruncateTooLongName()
        {
            string encoded = DeckCodeEncoder.EncodeDeck(TestData.DeckWithTooLongName);
            Deck deck = DeckCodeDecoder.ParseDeck(encoded);
            
            Assert.Equal(TestData.DeckWithTooLongName.Name.Substring(0, 63), deck.Name);
        }

        [Fact]
        public void SucceedStripHtmlTags()
        {
            string encoded = DeckCodeEncoder.EncodeDeck(TestData.DeckWithHtmlTagsInName);
            Deck deck = DeckCodeDecoder.ParseDeck(encoded);
            
            Assert.Equal("This is a tagged name", deck.Name);
        }

        [Fact]
        public void SucceedDecodeBack()
        {
            string encoded = DeckCodeEncoder.EncodeDeck(TestData.ExampleDeck1);
            var deck = DeckCodeDecoder.ParseDeck(encoded);
            
            // "deep equality" check
            string json = JsonConvert.SerializeObject(TestData.ExampleDeck1);
            string json2 = JsonConvert.SerializeObject(deck);
            
            Assert.Equal(json, json2);
        }
    }
}