using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ArtifactSdkDotNet.Core;
using ArtifactSdkDotNet.Core.Exceptions;

namespace ArtifactSdkDotNet.DeckCode
{
    public static class DeckCodeEncoder
    {
        //expects array("heroes" => array(id, turn), "cards" => array(id, count), "name" => name)
        //	signature cards for heroes SHOULD NOT be included in "cards"

        public static string EncodeDeck( Deck deck )
        {
            deck.Heroes.Sort((h1, h2) => h1.Id.CompareTo(h2.Id));
            deck.Cards.Sort((c1, c2) => c1.Id.CompareTo(c2.Id));
            
            byte[] deckBytes = EncodeBytes(deck);
            return EncodeBytesToDeckCode(deckBytes);
        }

        private static byte[] EncodeBytes( Deck deck )
        {
            int countHeroes = deck.Heroes.Count;
            //$allCards = array_merge( $deckContents['heroes'], $deckContents['cards'] );
            using (var stream = new MemoryStream())
            {
                //our version and hero count
                byte versionAndHeroes = (byte) ( Config.DeckCode.CurrentVersion << 4 | ExtractNBitsWithCarry(countHeroes, 3) );
                stream.WriteByte(versionAndHeroes);
                //put a byte into the stream as a placeholder for the final checksum
                stream.WriteByte(0);
                long checksumIndex = stream.Length - 1;
                string name = "";
                if (!string.IsNullOrEmpty(deck.Name))
                {
                    //TODO: Make more sophisticated, maybe with HtmlPack
                    name = Regex.Replace(deck.Name, "<.*?>", String.Empty);
                    
                    //strip characters away until the byte count is 63 or less
                    while (Encoding.UTF8.GetByteCount(name) > Config.DeckCode.MaxDeckNameLengthBytes)
                    {
                        name = name.Substring(0, name.Length - 1);
                    }
                }

                stream.WriteByte((byte) Encoding.UTF8.GetByteCount(name));

                AddRemainingNumberToBuffer(countHeroes, 3, stream);
                int prevCardId = 0;
                foreach (var hero in deck.Heroes)
                {
                    AddCardToBuffer(hero.Id - prevCardId, hero.Turn, stream);
                    prevCardId = hero.Id;
                }

                //reset our card offset
                prevCardId = 0;
                //now all of the cards
                foreach (var card in deck.Cards)
                {
                    AddCardToBuffer(card.Id - prevCardId, card.Count, stream);
                    prevCardId = card.Id;
                }

                //write the string
                int preNameLength = (int) stream.Length;
                var nameBytes = Encoding.UTF8.GetBytes(name);

                stream.Write(nameBytes, 0, nameBytes.Length);

                var deckBytes = stream.ToArray();

                int firstDataIndex = Config.DeckCode.HeaderSize;

                var fullChecksum = ComputeChecksum(new ArraySegment<byte>(deckBytes, firstDataIndex, preNameLength - firstDataIndex));

                var smallCheckSum = (byte) ( fullChecksum & 0x0FF );
                deckBytes[checksumIndex] = smallCheckSum;
                return deckBytes;
            }
        }

        private static string EncodeBytesToDeckCode( byte[] deckBytes )
        {
            var deckCode = String.Concat(Config.DeckCode.DeckCodePrefix, Convert.ToBase64String(deckBytes));
            deckCode = deckCode.Replace('/', '-');
            deckCode = deckCode.Replace('=', '_');
            return deckCode;
        }

        private static int ExtractNBitsWithCarry( int value, int numBits )
        {
            int limitBit = 1 << numBits;
            int result = ( value & ( limitBit - 1 ) );
            if (value >= limitBit)
            {
                result |= limitBit;
            }

            return result;
        }

        //utility to write the rest of a number into a buffer. This will first strip the specified N bits off, and then write a series of bytes of the structure of 1 overflow bit and 7 data bits
        private static void AddRemainingNumberToBuffer( int value, int alreadyWrittenBits, Stream target )
        {
            value >>= alreadyWrittenBits;
            while (value > 0)
            {
                byte nextByte = (byte) ExtractNBitsWithCarry(value, 7);
                value >>= 7;
                target.WriteByte(nextByte);
            }
        }

        private static void AddCardToBuffer( int cardId, int parameter, Stream stream )
        {
            //this shouldn't ever be the case
            long countBytesStart = stream.Length;
            //determine our count. We can only store 2 bits, and we know the value is at least one, so we can encode values 1-5. However, we set both bits to indicate an 
            //extended parameter encoding
            var firstByteMaxCount = 0x03;
            var extendedParameter = ( parameter - 1 ) >= firstByteMaxCount;
            //determine our first byte, which contains our count, a continue flag, and the first few bits of our value
            var firstByteCount = extendedParameter ? firstByteMaxCount : parameter - 1;
            var firstByte = ( firstByteCount << 6 );
            firstByte |= ExtractNBitsWithCarry(cardId, 5);
            stream.WriteByte((byte) firstByte);
            //now continue writing out the rest of the number with a carry flag
            AddRemainingNumberToBuffer(cardId, 5, stream);
            //now if we overflowed on the count, encode the remaining count
            if (extendedParameter)
            {
                AddRemainingNumberToBuffer(parameter, 0, stream);
            }

            var countBytesEnd = stream.Length;
            if (countBytesEnd - countBytesStart > 11)
            {
                //something went horribly wrong
                throw new InvalidDeckCodeException("Something went horribly wrong");
            }
        }

        private static uint ComputeChecksum( ArraySegment<byte> bytes )
        {
            uint checksum = 0;
            foreach (var b in bytes)
            {
                checksum += b;
            }

            return checksum;
        }
    }
}