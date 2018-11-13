using System.Collections.Generic;
using System.ComponentModel;

namespace ArtifactSdkDotNet.Core
{
    public class Cardset
    {
        public int Version { get; private set; }
        public SetInfo SetInfo { get; private set; }
        public Card[] CardList { get; private set; }
    }

    public class SetInfo
    {
        [DefaultValue(-1)]
        public int SetId { get; private set; }
        [DefaultValue(-1)]
        public int PackItemDef { get; private set; }
        public Dictionary<string, string> Name { get; private set; }
    }
}