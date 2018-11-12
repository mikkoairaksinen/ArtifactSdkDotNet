using System.Collections.Generic;

namespace ArtifactSdkDotNet.Core
{
    public class Cardset
    {
        public string Version { get; private set; }
        public SetInfo SetInfo { get; private set; }
        public Card[] CardList { get; private set; }
    }

    public class SetInfo
    {
        public int SetId { get; private set; }
        public int PackItemDef { get; private set; }
        public Dictionary<string, string> Name { get; private set; }
    }
}