using ArtifactSdkDotNet.Core;

namespace DeckCodeTests
{
    public class TestData
    {
        public const string ExampleDeck1Code = "ADCJWkTZX05uwGDCRV4XQGy3QGLmqUBg4GQJgGLGgO7AaABR3JlZW4vQmxhY2sgRXhhbXBsZQ__";
        public const string ExampleDeck2Code = "ADCJQUQI30zuwEYg2ABeF1Bu94BmWIBTEkLtAKlAZakAYmHh0JsdWUvUmVkIEV4YW1wbGU_";
        public const string PreviewTournament1Code = "ADCJbsAYH2ovAEMEg+4XYGB5t0BBFgDRwQJBSQBAQUSCwVGBgMPPQFECAUaAkYIBwIM";
        public const string PreviewTournament2Code = "ADCJZQAZX28uwENEQm4XQJs3QEFwQQMBxICUg0GEAgQBQQJDyABMQIMTV8H";

        public static readonly Deck ExampleDeck1 = new Deck
        {
            Name = "Green/Black Example",
            Heroes =
            {
                new Deck.Hero(4005, 2),  // Debbi the Cunning
                new Deck.Hero(10014, 1), // Lycan
                new Deck.Hero(10017, 3), // Chen
                new Deck.Hero(10026, 1), // Rix
                new Deck.Hero(10047, 1)  // Phantom Assassin
            },
            Cards =
            {
                new Deck.Card(3000, 2),  // 2x Traveller's Cloak
                new Deck.Card(3001, 1),  // 1x Leather Armor
                new Deck.Card(10091, 3), // 3x Revtel Convoy
                new Deck.Card(10102, 3), // 3x Thunderhide Pack
                new Deck.Card(10128, 3), // 3x Untested Grunt
                new Deck.Card(10165, 3), // 3x Iron Fog Goldmine
                new Deck.Card(10168, 3), // 3x Selemene's Favor
                new Deck.Card(10169, 3), // 3x Mist of Avernus
                new Deck.Card(10185, 3), // 3x Steam Cannon
                new Deck.Card(10223, 1), // 1x Red Mist Maul
                new Deck.Card(10234, 3), // 3x Poaching Knife
                new Deck.Card(10260, 1), // 1x Horn of the Alpha
                new Deck.Card(10263, 1), // 1x Apotheosis Blade
                new Deck.Card(10322, 3), // 3x Payday
                new Deck.Card(10354, 3), // 3x Pick Off
            }

        };

        public static readonly Deck DeckWithTooLongName = new Deck
        {
            Name = "Very very very very very very very very very very very very very long deckname",
            Heroes =
            {
                new Deck.Hero(4005, 2),  // Debbi the Cunning
                new Deck.Hero(10014, 1), // Lycan
                new Deck.Hero(10017, 3), // Chen
                new Deck.Hero(10026, 1), // Rix
                new Deck.Hero(10047, 1)  // Phantom Assassin
            },
            Cards =
            {
                new Deck.Card(3000, 2),  // 2x Traveller's Cloak
                new Deck.Card(3001, 1),  // 1x Leather Armor
                new Deck.Card(10091, 3), // 3x Revtel Convoy
                new Deck.Card(10102, 3), // 3x Thunderhide Pack
                new Deck.Card(10128, 3), // 3x Untested Grunt
                new Deck.Card(10165, 3), // 3x Iron Fog Goldmine
                new Deck.Card(10168, 3), // 3x Selemene's Favor
                new Deck.Card(10169, 3), // 3x Mist of Avernus
                new Deck.Card(10185, 3), // 3x Steam Cannon
                new Deck.Card(10223, 1), // 1x Red Mist Maul
                new Deck.Card(10234, 3), // 3x Poaching Knife
                new Deck.Card(10260, 1), // 1x Horn of the Alpha
                new Deck.Card(10263, 1), // 1x Apotheosis Blade
                new Deck.Card(10322, 3), // 3x Payday
                new Deck.Card(10354, 3), // 3x Pick Off
            }
        };
        public static readonly Deck DeckWithNonAsciiName = new Deck
        {
            Name = "😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊😊",
            Heroes =
            {
                new Deck.Hero(4005, 2),  // Debbi the Cunning
                new Deck.Hero(10014, 1), // Lycan
                new Deck.Hero(10017, 3), // Chen
                new Deck.Hero(10026, 1), // Rix
                new Deck.Hero(10047, 1)  // Phantom Assassin
            },
            Cards =
            {
                new Deck.Card(3000, 2),  // 2x Traveller's Cloak
                new Deck.Card(3001, 1),  // 1x Leather Armor
                new Deck.Card(10091, 3), // 3x Revtel Convoy
                new Deck.Card(10102, 3), // 3x Thunderhide Pack
                new Deck.Card(10128, 3), // 3x Untested Grunt
                new Deck.Card(10165, 3), // 3x Iron Fog Goldmine
                new Deck.Card(10168, 3), // 3x Selemene's Favor
                new Deck.Card(10169, 3), // 3x Mist of Avernus
                new Deck.Card(10185, 3), // 3x Steam Cannon
                new Deck.Card(10223, 1), // 1x Red Mist Maul
                new Deck.Card(10234, 3), // 3x Poaching Knife
                new Deck.Card(10260, 1), // 1x Horn of the Alpha
                new Deck.Card(10263, 1), // 1x Apotheosis Blade
                new Deck.Card(10322, 3), // 3x Payday
                new Deck.Card(10354, 3), // 3x Pick Off
            }
        };

        public static readonly Deck DeckWithHtmlTagsInName = new Deck
        {
            Name = "<div>This <b>is</b> a<br> tagged name</div>",
            Heroes =
            {
                new Deck.Hero(4005, 2), // Debbi the Cunning
                new Deck.Hero(10014, 1), // Lycan
                new Deck.Hero(10017, 3), // Chen
                new Deck.Hero(10026, 1), // Rix
                new Deck.Hero(10047, 1) // Phantom Assassin
            },
            Cards =
            {
                new Deck.Card(3000, 2), // 2x Traveller's Cloak
                new Deck.Card(3001, 1), // 1x Leather Armor
                new Deck.Card(10091, 3), // 3x Revtel Convoy
                new Deck.Card(10102, 3), // 3x Thunderhide Pack
                new Deck.Card(10128, 3), // 3x Untested Grunt
                new Deck.Card(10165, 3), // 3x Iron Fog Goldmine
                new Deck.Card(10168, 3), // 3x Selemene's Favor
                new Deck.Card(10169, 3), // 3x Mist of Avernus
                new Deck.Card(10185, 3), // 3x Steam Cannon
                new Deck.Card(10223, 1), // 1x Red Mist Maul
                new Deck.Card(10234, 3), // 3x Poaching Knife
                new Deck.Card(10260, 1), // 1x Horn of the Alpha
                new Deck.Card(10263, 1), // 1x Apotheosis Blade
                new Deck.Card(10322, 3), // 3x Payday
                new Deck.Card(10354, 3), // 3x Pick Off
            }
        };
    }
}