using System.Text;
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
            
            Assert.True(Encoding.UTF8.GetByteCount(TestData.DeckWithTooLongName.Name) > ArtifactSdkDotNet.Config.DeckCode.MaxDeckNameLengthBytes );
            Assert.True(Encoding.UTF8.GetByteCount(deck.Name) <= ArtifactSdkDotNet.Config.DeckCode.MaxDeckNameLengthBytes );
            //in this case it's a pure ascii string so one character = 1 byte
            Assert.Equal(TestData.DeckWithTooLongName.Name.Substring(0, ArtifactSdkDotNet.Config.DeckCode.MaxDeckNameLengthBytes), deck.Name);
        }
        
        [Fact]
        public void SucceedTruncateNonAsciiName()
        {
            string encoded = DeckCodeEncoder.EncodeDeck(TestData.DeckWithNonAsciiName);
            Deck deck = DeckCodeDecoder.ParseDeck(encoded);

            Assert.True(Encoding.UTF8.GetByteCount(TestData.DeckWithNonAsciiName.Name) > ArtifactSdkDotNet.Config.DeckCode.MaxDeckNameLengthBytes );
            Assert.True(Encoding.UTF8.GetByteCount(deck.Name) <= ArtifactSdkDotNet.Config.DeckCode.MaxDeckNameLengthBytes );
            
            Assert.NotEqual(deck.Name, TestData.DeckWithNonAsciiName.Name);
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