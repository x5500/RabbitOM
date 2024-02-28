﻿/*
 EXPERIMENTATION of the next implementation of the rtp layer
*/

using System;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265StartPrefix
    {
        private readonly byte[] _values;

        public H265StartPrefix( byte[] values )
        {
            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            if ( values.Length == 0 )
            {
                throw new ArgumentException( nameof( values ) );
            }

            _values = values;
        }       



        public byte[] Values
        {
            get => _values;
        }





        public static bool StartsWith( byte[] buffer , H265StartPrefix prefix )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( prefix == null )
            {
                throw new ArgumentNullException( nameof( prefix ) );
            }

            int count = buffer.Length > prefix.Values.Length ? prefix.Values.Length : buffer.Length;

            while ( -- count >= 0 )
            {
                if ( buffer[ count ] != prefix.Values[ count ] )
                {
                    return false;
                }
            }

            return true;
        }
    }
}