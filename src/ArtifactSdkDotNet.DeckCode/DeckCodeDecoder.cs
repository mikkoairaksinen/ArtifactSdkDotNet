using System;
using System.Text;
using ArtifactSdkDotNet.Core;
using ArtifactSdkDotNet.Core.Exceptions;

namespace ArtifactSdkDotNet.DeckCode
{
    public static class DeckCodeDecoder
    {
        public static Deck ParseDeck(string deckCode)
        {
            var deckBytes = DecodeDeckStringToBytes(deckCode);
            return ParseDeckFromBytes(deckBytes);
        }

        private static byte[] DecodeDeckStringToBytes(string deckCode)
        {
            if (!deckCode.StartsWith(Config.DeckCode.DeckCodePrefix))
            {
                throw new InvalidDeckCodeException($"Invalid DeckCode, must start with {Config.DeckCode.DeckCodePrefix} ");
            }
            //remove prefix
            deckCode = deckCode.Substring(Config.DeckCode.DeckCodePrefix.Length);

            //revert url-safe character replacements
            deckCode = deckCode.Replace('-', '/');
            deckCode = deckCode.Replace('_', '=');

            return Convert.FromBase64String(deckCode);
        }

        private static Deck ParseDeckFromBytes(byte[] deckBytes)
        {
            int currentByteIndex = 0;
            int totalBytes = deckBytes.Length;

            byte versionAndHeroes = deckBytes[currentByteIndex++];

            int version = versionAndHeroes >> 4;
            if (version != Config.DeckCode.CurrentVersion && version != 1)
            {
                throw new InvalidDeckCodeException($"Mismatching version: {version}, current: {Config.DeckCode.CurrentVersion}");
            }

            //do checksum check
            int checksum = deckBytes[currentByteIndex++];
            var stringLength = 0;
            if (version > 1)
            {
                stringLength = deckBytes[currentByteIndex++];
            }
            var totalCardBytes = totalBytes - stringLength;
            //grab the string size
            {
                int computedChecksum = 0;
                for (int i = currentByteIndex; i < totalCardBytes; i++)
                {
                    computedChecksum += deckBytes[i];
                }
                int masked = computedChecksum & 0xFF;
                if (masked != checksum)
                {
                    throw new InvalidDeckCodeException($"Incorrect checksum: {masked}, expected: {checksum}");
                }
            }
            //read in our hero count (part of the bits are in the version, but we can overflow bits here
            int numHeroes = 0;
            if (!ReadVarEncodedUint32(versionAndHeroes, 3, deckBytes, ref currentByteIndex, totalCardBytes, ref numHeroes))
            {
                throw new InvalidDeckCodeException("No hero count found");
            }
            //read in the heroes to a tuple list where the first int is the heroId and the second is the turn number
            var definition = new Deck();
            int prevCardBase = 0;
            {
                for (int i = 0; i < numHeroes; ++i)
                {
                    int heroTurn = 0;
                    int heroCardId = 0;
                    if (!ReadSerializedCard(deckBytes, ref currentByteIndex, totalCardBytes, ref prevCardBase, ref heroTurn, ref heroCardId))
                    {
                        throw new InvalidDeckCodeException("Not enough hero data found");
                    }
                    definition.Heroes.Add(new Deck.Hero(heroCardId, heroTurn));
                }
            }
            prevCardBase = 0;
            while (currentByteIndex < totalCardBytes)
            {
                int cardCount = 0;
                int cardId = 0;
                if (!ReadSerializedCard(deckBytes, ref currentByteIndex, totalCardBytes, ref prevCardBase, ref cardCount, ref cardId))
                {
                    throw new InvalidDeckCodeException("Invalid card data");
                }
                definition.Cards.Add(new Deck.Card(cardId, cardCount));
            }
            if (currentByteIndex <= totalBytes)
            {
                definition.Name = Encoding.UTF8.GetString(deckBytes, deckBytes.Length - stringLength, stringLength);
            }
            return definition;
        }


        private static bool ReadBitsChunk(byte chunk, int numBits, int currShift, ref int outBits)
        {
            var continueBit = (1 << numBits);
            var newBits = chunk & (continueBit - 1);
            outBits |= (newBits << currShift);
            return (chunk & continueBit) != 0;
        }

        private static bool ReadVarEncodedUint32(byte baseValue, int baseBits, byte[] data, ref int currentIndex, int indexEnd, ref int outValue)
        {
            outValue = 0;
            var deltaShift = 0;
            if ((baseBits == 0) || ReadBitsChunk(baseValue, baseBits, deltaShift, ref outValue))
            {
                deltaShift += baseBits;
                while (true)
                {
                    //do we have more room?
                    if (currentIndex > indexEnd)
                    {
                        return false;
                    }
                    //read the bits from this next byte and see if we are done
                    var nextByte = data[currentIndex++];
                    if (!ReadBitsChunk(nextByte, 7, deltaShift, ref outValue))
                    {
                        break;
                    }
                    deltaShift += 7;
                }
            }
            return true;
        }

        private static bool ReadSerializedCard(byte[] data, ref int currentIndex, int indexEnd, ref int prevCardBase, ref int outCount, ref int outCardId)
        {
            //end of the memory block?
            if (currentIndex >= indexEnd)
            {
                return false;
            }
            //header contains the count (2 bits), a continue flag, and 5 bits of offset data. If we have 11 for the count bits we have the count
            //encoded after the offset
            byte header = data[currentIndex++];
            bool hasExtendedCount = ((header >> 6) == 0x03);
            //read in the delta, which has 5 bits in the header, then additional bytes while the value is set
            int cardDelta = 0;
            if (!ReadVarEncodedUint32(header, 5, data, ref currentIndex, indexEnd, ref cardDelta))
            {
                return false;
            }
            outCardId = prevCardBase + cardDelta;
            //now parse the count if we have an extended count
            if (hasExtendedCount)
            {
                if (!ReadVarEncodedUint32(0, 0, data, ref currentIndex, indexEnd, ref outCount))
                {
                    return false;
                }
            }
            else
            {
                //the count is just the upper two bits + 1 (since we don't encode zero)
                outCount = (header >> 6) + 1;
            }
            //update our previous card before we do the remap, since it was encoded without the remap
            prevCardBase = outCardId;
            return true;
        }
    }

}