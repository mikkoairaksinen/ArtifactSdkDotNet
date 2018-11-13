using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArtifactSdkDotNet.Core
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardType
    {
        None = 0,
        Hero,
        Stronghold,
        Item,
        Pathing,
        Creep,
        [EnumMember(Value= "Passive Ability")]
        PassiveAbility,
        Spell,
        Ability,
        Improvement
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardReferenceType
    {
        None = 0,
        [EnumMember(Value = "references")]
        References,
        [EnumMember(Value = "includes")]
        Includes,

        [EnumMember(Value = "passive_ability")]
        PassiveAbility,

        [EnumMember(Value = "active_ability")]
        ActiveAbility,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubType
    {
        None = 0,
        Weapon,
        Armor,
        Accessory,
        Consumable,
        Deed
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardRarity
    {
        Basic = 0,
        Common,
        Uncommon,
        Rare
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
        public string Illustrator { get; private set; }
        public CardRarity Rarity { get; private set; }
        public SubType SubType { get; private set; }
        public int GoldCost { get; private set; }
        public bool IsBlack { get; private set; }
        public bool IsBlue { get; private set; }
        public bool IsGreen { get; private set; }
        public bool IsRed { get; private set; }
        public int ManaCost { get; private set; }
        public int ItemDef { get; private set; }
        public int Attack { get; private set; }
        public int Armor { get; private set; }
        public int HitPoints { get; private set; }
        public CardReference[] References { get; private set; }

    }
}