namespace _2021;

using System.Diagnostics;
using System.Text;
using static Utils;

public class Day16
{
    public class Packet
    {
        public int Version { get; set; }
        public int TypeId { get; set; }
        public ulong Value { get; set; }
        public List<Packet> SubPackets { get; set; } = new List<Packet>();
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day16));
        var hex = input[0];
        var binaryString = ToBinaryString(hex);
        var (_, packet) = ParsePacket(binaryString, 0);
        var versionSum = IteratePacket(packet).Select(packet => packet.Version).Sum();
        Assert.Equal(993, versionSum);
    }


    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day16));
        var hex = input[0];
        var binaryString = ToBinaryString(hex);
        var (_, packet) = ParsePacket(binaryString, 0);
        Assert.Equal(144595909277ul, packet.Value);
    }

    static String ToBinaryString(string hexString) 
    {
        var binary = hexString.Select(hexChar =>
        {
            var value = Convert.ToUInt32(new string(hexChar, 1), 16);
            var binaryValue = Convert.ToString(value, 2);
            var fourBits = binaryValue.Length < 4 ?
                 new string('0', 4 - binaryValue.Length) + binaryValue :
                 binaryValue;
            return fourBits;
        });
        var binaryString = string.Join(string.Empty, binary);
        return binaryString;
    }

    IEnumerable<Packet> IteratePacket(Packet packet) =>
         new [] {packet }.Concat(packet.SubPackets.SelectMany(subPacket => IteratePacket(subPacket)));

    static (int, Packet) ParsePacket(string binaryString, int position)
    {
        var result = new Packet();

        var (position1, version) = ParseVersion(binaryString, position);
        result.Version = version;

        var (position2, typeId) = ParseTypeId(binaryString, position1);
        result.TypeId = typeId;

        if (typeId != 4)
        {
            var (position3, lengthTypeId) = ParseLengthTypeId(binaryString, position2);
            if (lengthTypeId == 0)
            {
                var (position4, totalLengthOfSubPackets) = ParseSubpacketsTotalLengthInBits(binaryString, position3);
                var totalLengthSoFar = 0;
                var currentPosition = position4;
                while (totalLengthSoFar < totalLengthOfSubPackets)
                {
                    var (newPosition, subPacket) = ParsePacket(binaryString, currentPosition);
                    totalLengthSoFar += newPosition - currentPosition;
                    currentPosition = newPosition;
                    result.SubPackets.Add(subPacket); 
                }
                AssignValueFromSubPackets(result);
                return (currentPosition, result);
            }
            else
            {
                var (position4, numberOfSubPackets) = ParseSubpacketsLength(binaryString, position3);
                var numberOfSubPacketsSoFar = 0;
                var currentPosition = position4;
                while (numberOfSubPacketsSoFar < numberOfSubPackets)
                {
                    var (newPosition, subPacket) = ParsePacket(binaryString, currentPosition);
                    numberOfSubPacketsSoFar += 1;
                    currentPosition = newPosition;
                    result.SubPackets.Add(subPacket); 
                }
                AssignValueFromSubPackets(result);
                return (currentPosition, result);
            }
        }
        else
        {
            var (position3, value) = ParseLiteral(binaryString, position2);
            result.Value = value;
            return (position3, result);
        }
    }

    static void AssignValueFromSubPackets(Packet packet)
    {
        var subValues = packet.SubPackets.Select(x => x.Value);

        switch (packet.TypeId)
        {
            case 0:
                packet.Value = subValues.Aggregate(0ul, (sum, value) => sum + value);
                break;
            case 1:
                packet.Value = subValues.Aggregate(1ul, (product, value) => product * value);
                break;
            case 2:
                packet.Value = subValues.Aggregate(ulong.MaxValue, (min, value) => value < min ? value : min);
                break;
            case 3: 
                packet.Value = subValues.Aggregate(ulong.MinValue, (max, value) => value > max ? value : max);
                break;
            case 5:
                packet.Value = packet.SubPackets[0].Value > packet.SubPackets[1].Value ? 1ul : 0ul;
                break;
            case 6:
                packet.Value = packet.SubPackets[0].Value < packet.SubPackets[1].Value ? 1ul : 0ul;
                break;
            case 7:
                packet.Value = packet.SubPackets[0].Value == packet.SubPackets[1].Value ? 1ul : 0ul;
                break;
            default: 
                break;
        }
    }

    static (int, ulong) ParseLiteral(string binaryString, int position)
    {
        bool shouldContinue = true;
        var literalPosition = position;
        var numberBuilder = new StringBuilder();
        while (shouldContinue)
        {
            var nextFiveBits = binaryString[literalPosition..(literalPosition + 5)];
            shouldContinue = nextFiveBits[0] == '1';
            numberBuilder.Append(nextFiveBits.Skip(1).ToArray());
            literalPosition += 5;
        }
        var resultString = numberBuilder.ToString();
        return (literalPosition, Convert.ToUInt64(resultString, 2));
    }

    static (int, int) ParseVersion(string binaryString, int position)
    {
        var version = Convert.ToInt32(binaryString[position..(position + 3)], 2);
        return (position + 3, version);
    }

    static (int, int) ParseTypeId(string binaryString, int position)
    {
        var typeId = Convert.ToInt32(binaryString[position..(position + 3)], 2);
        return (position + 3, typeId);;
    }

    static (int, int) ParseLengthTypeId(string binaryString, int position)
    {
        return (position + 1, binaryString[position] == '0' ? 0 : 1);
    }

    static (int, uint) ParseSubpacketsTotalLengthInBits(string binaryString, int position)
    {
        var lengthInBits = Convert.ToUInt32(binaryString[position..(position + 15)], 2);
        return (position + 15, lengthInBits);
    }

    static (int, uint) ParseSubpacketsLength(string binaryString, int position)
    {
        var numberOfSubPackets = Convert.ToUInt32(binaryString[position..(position + 11)], 2);
        return (position + 11, numberOfSubPackets);
    }
}