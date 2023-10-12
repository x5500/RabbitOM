﻿using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent an audio codec
    /// </summary>
    public sealed class G711CCodecInfo : G711Codec
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public G711CCodecInfo()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samplingRate">the sampling rate</param>
        /// <param name="channels">the channels</param>
        public G711CCodecInfo( int samplingRate , int channels )
            : base( samplingRate , channels )
        {
        }

        /// <summary>
        /// Gets the sampling rate
        /// </summary>
        public override CodecType Type
        {
            get => CodecType.G711C;
        }
    }
}
