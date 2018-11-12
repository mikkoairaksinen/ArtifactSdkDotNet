using System;
using System.Linq;
using ArtifactSdkDotNet.Core;
using ArtifactSdkDotNet.DeckCode;
using Xunit;

namespace DeckCodeTests
{
    public class TestDecode
    {
        [Theory]
        [InlineData(TestData.ExampleDeck1Code)]
        [InlineData(TestData.ExampleDeck2Code)]
        [InlineData(TestData.PreviewTournament1Code)]
        [InlineData(TestData.PreviewTournament2Code)]
        public void SucceedDecodeAllDecks(string deckCode)
        {
            Deck decoded = DeckCodeDecoder.ParseDeck(deckCode);
            
            Assert.NotNull(decoded);
            Assert.NotEmpty(decoded.Heroes);
            Assert.NotEmpty(decoded.Cards);
        }

        [Fact]
        public void SucceedDecodeExampleDeck1()
        {
            Deck deck = DeckCodeDecoder.ParseDeck(TestData.ExampleDeck1Code);
            
            Assert.Equal(TestData.ExampleDeck1.Name, deck.Name);
            Assert.Equal(TestData.ExampleDeck1.Heroes.Count, deck.Heroes.Count);
            Assert.Equal(TestData.ExampleDeck1.Cards.Count, deck.Cards.Count);
            foreach (var hero in TestData.ExampleDeck1.Heroes)
            {
                Assert.Contains(deck.Heroes, h => h.Id == hero.Id && h.Turn == hero.Turn);
            }

            foreach (var card in TestData.ExampleDeck1.Cards)
            {
                Assert.Contains(deck.Cards, c => c.Id == card.Id && c.Count == card.Count);
            }
        }
        
    }
}
