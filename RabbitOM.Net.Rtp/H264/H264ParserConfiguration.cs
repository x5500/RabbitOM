﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264ParserConfiguration
    {
        private readonly object _lock = new();

        private readonly HashSet<int> _supportedPayloads = new()
        {
            96
        };

        // O(1)

        public bool IsPayloadSupported( int value )
        {
            lock ( _lock )
            {
                return _supportedPayloads.Contains( value );
            }
        }

        public bool RegisterPayload( int value )
        {
            lock ( _lock )
            {
                return _supportedPayloads.Add( value );
            }
        }

        public bool UnRegisterPayload( int value )
        {
            lock ( _lock )
            {
                return _supportedPayloads.Remove( value );
            }
        }
    }
}