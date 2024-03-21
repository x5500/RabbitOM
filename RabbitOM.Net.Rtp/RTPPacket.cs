using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class RTPPacket
    {
        private RTPPacket() { }


        public byte Version { get; private set; }
        public bool HasPadding { get; private set; }
        public bool HasExtension { get; private set; }
        public ushort NumberOfCSRC { get; private set; }
        public bool Marker { get; private set; }
        public byte Type { get; private set; }
        public uint SequenceNumber { get; private set; }
        public uint Timestamp { get; private set; }
        public uint SSRC { get; private set; }
        public uint Extension { get; private set; }
        public int[] CSRCIdentifiers { get; private set; }
        public byte[] ExtensionData { get; private set; }
        public byte[] Payload { get; private set; }
        public byte[] Buffer { get; private set; }




        public bool TryValidate()
        {
            return Version == 2 && Payload != null && Payload.Length > 0;
        }


        // TODO: refactor this horrible code

        public static bool TryParse(byte[] buffer, out RTPPacket result)
        {
            result = null;

            if (buffer == null || buffer.Length < 12)
            {
                return false;
            }

            var packet = new RTPPacket
            {
                Version = (byte)(buffer[0] >> 6),
                HasPadding = (byte)((buffer[0] >> 5) & 0x1) == 1,
                HasExtension = (byte)((buffer[0] >> 4) & 0x1) == 1,
                NumberOfCSRC = (ushort)(buffer[0] & 0x0F),
                Marker = (byte)((buffer[1] >> 7) & 0x1) != 0,
                Type = (byte)(buffer[1] & 0x7F),
                SequenceNumber = ((uint)(buffer[2] << 8) + (uint)(buffer[3])) % (ushort.MaxValue + 1),
                Timestamp = (uint)(buffer[4] << 24) + (uint)(buffer[5] << 16) + (uint)(buffer[6] << 8) + (uint)(buffer[7] << 0),
                SSRC = (uint)(buffer[8] << 24) + (uint)(buffer[9] << 16) + (uint)(buffer[10] << 8) + buffer[11],
            };

            int offset = 12;
            int limit = offset + 4 * packet.NumberOfCSRC + (packet.HasExtension ? 4 : 0);

            if (limit >= buffer.Length)
            {
                return false;
            }

            if (packet.NumberOfCSRC > 0)
            {
                packet.CSRCIdentifiers = new int[packet.NumberOfCSRC];

                for (uint i = 0; i < packet.CSRCIdentifiers.Length; ++i)
                {
                    packet.CSRCIdentifiers[i] += buffer[i + offset++] << 24;
                    packet.CSRCIdentifiers[i] += buffer[i + offset++] << 16;
                    packet.CSRCIdentifiers[i] += buffer[i + offset++] << 8;
                    packet.CSRCIdentifiers[i] += buffer[i + offset++];
                }
            }

            if (packet.HasExtension)
            {
                packet.Extension = ((uint)buffer[offset] << 8) + (uint)(buffer[++offset] << 0);

                int extenstionSize = (buffer[++offset] << 8) + (buffer[++offset] << 0) * 4;

                offset += extenstionSize;
            }

            if (offset >= buffer.Length)
            {
                return false;
            }

            int payloadSize = buffer.Length - offset;

            if (packet.HasPadding)
            {
                payloadSize -= buffer[^1];
            }

            packet.Buffer = buffer;

            packet.Payload = new byte[payloadSize];

            System.Buffer.BlockCopy(packet.Buffer, (int)offset, packet.Payload, 0, packet.Payload.Length);

            result = packet;

            return true;
        }
    }
}