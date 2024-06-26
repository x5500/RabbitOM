﻿using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class DefaultRTPSink : RTPSink
    {
        /// TODO: use the DIP and replace with an interface
        
        private readonly DefaultRTPFrameBuilder _builder = new();

        public override void Write( byte[] data )
        {
            if ( ! _builder.TryAddPacket( data ) )
            {
                return;
            }

            OnPacketReceived( new RTPPacketReceivedEventArgs( _builder.LastPacket ) );

            if ( _builder.CanBuildFrame() )
            {
                OnFrameReceived( new RTPFrameReceivedEventArgs( _builder.BuildFrame() ) );
            }
        }
        
        public override void Reset()
        {
            _builder.Clear();
        }

        protected override void Dispose( bool disposing)
        {
            if ( disposing )
            {
                _builder.Dispose();
            }
        }
    }
}