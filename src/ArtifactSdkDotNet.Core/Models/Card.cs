using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArtifactSdkDotNet.Core
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardType
    {
        Hero,
        Stronghold,
        Item,
        Pathing,
        [EnumMember(Value="Passive Ability")]
        Creep,
        PassiveAbility,
        Spell,
        Ability,
        Improvement

    }

[JsonConverter(typeof(StringEnumConverter))]
    public enum CardReferenceType
    {
        References,
        Includes,
        [EnumMember(Value = "passive_ability")]
        PassiveAbility,
        [EnumMember(Value = "active_ability")]
        ActiveAbility,
    }

    public class CardReference
    {
        public int CardId { get; private set; }

        [JsonProperty(PropertyName = "ref_type")]
        public CardReferenceType ReferenceType { get; private set; }
        public int Count { get; private set; }

    }

    public class Card
    {
        public int CardId { get; private set; }
        public int BaseCardId { get; private set; }
        public CardType CardType { get; private set; }
        public Dictionary<string, string> CardName { get; private set; }
        public Dictionary<string, string> CardText { get; private set; }
        public Dictionary<string, string> MiniImage { get; private set; }
        public Dictionary<string, string> LargeImage { get; private set; }
        public Dictionary<string, string> IngameImage { get; private set; }
        public bool IsGreen { get; private set; }
        public bool IsRed { get; private set; }
        public bool IsBlack { get; private set; }
        public bool IsBlue { get; private set; }
        public int Attack { get; private set; }
        public int HitPoints { get; private set; }
        public CardReference[] References { get; private set; }
    }
}