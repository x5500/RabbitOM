﻿using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264Parser
    {
        private readonly H264ParserConfiguration _configuration = new();

        public H264ParserConfiguration Configuration
        {
            get => _configuration;
        }

        // Time  complexiy : O(N) => but probably O(N,M)
        
        // Space complexiy : O(1) => but probably O(1,M)

        // Try to improve it

        public bool TryParse( RTPFrame frame , out H264NalUnitCollection result )
        {
            result = null;

            if ( frame == null )
            {
                return false;
            }

            H264NalUnitCollection nalunits = new();

            foreach ( var packet in frame.Packets )
            {
                if ( ! _configuration.IsPayloadSupported( packet.Type ) )
                {
                    return false;
                }
                
                if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) )
                {
                    nalunits.Enqueue( nalUnit );
                }
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }
    }
}