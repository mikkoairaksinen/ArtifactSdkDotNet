using System.Collections.Generic;

namespace ArtifactSdkDotNet.Core
{
    public class Deck
    {
        public string Name { get; set; }
        public List<Hero> Heroes { get; private set; } = new List<Hero>();
        public List<Card> Cards { get; private set; } = new List<Card>();

        public class Hero
        {
            public int Id { get; private set; }
            public int Turn { get; private set; }

            public Hero( int id, int turn )
            {
                Id = id;
                Turn = turn;
            }
        }

        public class Card
        {
            public int Id { get; private set; }
            public int Count { get; private set; }

            public Card( int id, int count )
            {
                Id = id;
                Count = count;
            }
        }
    }

}