using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day16 : Day
    {
        private readonly string SampleInput1 = "D2FE28";
        private readonly string SampleInput2 = "38006F45291200";
        private readonly string SampleInput3 = "EE00D40C823060";
        private readonly string SampleInput4 = "8A004A801A8002F478";
        private readonly string SampleInput5 = "620080001611562C8802118E34";
        private readonly string SampleInput6 = "C0015000016115A2E0802F182340";
        private readonly string SampleInput7 = "A0016C880162017C3686B18A3D4780";

        private long versionTotal = 0;

        public override object ExecutePart1()
        {
            Input = SampleInput5;

            Console.WriteLine($"Input: {Input}");

            string binaryTransmission = string.Empty;
            foreach (var c in Input)
            {
                var binaryValue = GetBinaryValue(c.ToString());
                binaryTransmission = $"{binaryTransmission}{binaryValue}";
            }

            DecodePacket(binaryTransmission);

            Console.WriteLine($"Version total: {versionTotal}");

            //return versionTotal;
            return base.ExecutePart1();
        }



        private void DecodePacket(string transmission)
        {
            Console.WriteLine($"Decoding packet: {transmission}");

            var version = GetDecimalValue(string.Join("", transmission.Take(3)));
            Console.WriteLine($"Packet version: {version}");

            versionTotal += version;

            var typeId = GetDecimalValue(string.Join("", transmission.Skip(3).Take(3)));
            Console.WriteLine($"Packet Type ID: {typeId}");

            if (typeId == 4)
            {
                // Literal Value packet
                var val = DecodeLiteralValuePacket(transmission.Skip(6));
                Console.WriteLine($"Literal value: {val}");
            }
            else
            {
                // Operator Packet
                var lengthTypeId = transmission.Skip(6).First();
                if (lengthTypeId == '1')
                {
                    var subPacketCount = GetDecimalValue(string.Join("", transmission.Skip(7).Take(11)));
                    Console.WriteLine($"(decode) sub packet count: {subPacketCount}");
                    if (subPacketCount > 0)
                    {
                        var subPackets = GetSubPackets(transmission.Skip(18), subPacketCount);
                        foreach (var subPacket in subPackets)
                        {
                            DecodePacket(subPacket);
                        }
                    }
                }
                else
                {
                    var packetLength = GetDecimalValue(string.Join("", transmission.Skip(7).Take(15)));
                    Console.WriteLine($"(decode )packet length: {packetLength}");

                    var subPackets = GetSubPackets(transmission.Skip(22).Take(packetLength));
                    foreach (var subPacket in subPackets)
                    {
                        DecodePacket(subPacket);
                    }
                }
            }
        }

        private List<string> GetSubPackets(IEnumerable<char> input)
        {
            var subPackets = new List<string>();

            var typeId = GetDecimalValue(string.Join("", input.Skip(3).Take(3)));
            if (typeId == 4)
            {
                while (input.Any(x => x.Equals('1')))
                {
                    for (var i = 0; ; i++)
                    {
                        var bitGroup = input.Skip(6).Skip(i * 5).Take(5);
                        if (bitGroup.First().Equals('0'))
                        {
                            // Last group, end of packet
                            var totalPacketLength = 11 + (i * 5);

                            subPackets.Add(string.Join("", input.Skip(0).Take(totalPacketLength)));
                            input = input.Skip(totalPacketLength);
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Going to decode: {string.Join("", input)}");
                DecodePacket(string.Join("", input));
            }

            return subPackets;
        }

        private List<string> GetSubPackets(IEnumerable<char> input, int packetCount)
        {
            var subPackets = new List<string>();

            var typeId = GetDecimalValue(string.Join("", input.Skip(3).Take(3)));
            if (typeId == 4)
            {
                while (input.Any(x => x.Equals('1')))
                {
                    for (var i = 0; ; i++)
                    {
                        var bitGroup = input.Skip(6).Skip(i * 5).Take(5);
                        if (bitGroup.First().Equals('0'))
                        {
                            // Last group, end of packet
                            var totalPacketLength = 11 + (i * 5);

                            subPackets.Add(string.Join("", input.Skip(0).Take(totalPacketLength)));
                            input = input.Skip(totalPacketLength);
                            break;
                        }
                    }
                }
            }
            else
            {
                var lengthTypeId = input.Skip(6).First();
                if (lengthTypeId == '1')
                {
                    var subPacketCount = GetDecimalValue(string.Join("", input.Skip(7).Take(11)));
                    Console.WriteLine($"(GetSubPackets) sub packet count: {subPacketCount}");
                    if (subPacketCount > 0)
                    {
                        subPackets.AddRange(GetSubPackets(input.Skip(18), subPacketCount));
                    }
                }
                else
                {
                    var packetLength = GetDecimalValue(string.Join("", input.Skip(7).Take(15)));
                    Console.WriteLine($"(GetSubPackets) packet length: {packetLength}");

                    subPackets.AddRange(GetSubPackets(input.Skip(22).Take(packetLength)));
                    subPackets.AddRange(GetSubPackets(input.Skip(22 + packetCount)));
                }
            }

            return subPackets;
        }

        private int DecodeLiteralValuePacket(IEnumerable<char> packetPayload)
        {
            var literalValue = string.Empty;
            for (var i = 0; ; i++)
            {
                var val = packetPayload.Skip(i * 5).Take(5);
                if (val.First().Equals('1'))
                {
                    // Not the last group, keep reading
                    literalValue = $"{literalValue}{string.Join("", val.Skip(1))}";
                }
                else
                {
                    // Last group, end of packet
                    literalValue = $"{literalValue}{string.Join("", val.Skip(1))}";
                    break;
                }
            }

            return GetDecimalValue(literalValue);
        }

        private string GetBinaryValue(string hexValue)
        {
            return Convert.ToString(Convert.ToInt32(hexValue, 16), 2).PadLeft(4, '0');
        }

        private int GetDecimalValue(string binaryValue)
        {
            return Convert.ToInt32(binaryValue, 2);
        }
    }
}
