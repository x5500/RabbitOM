﻿using System;
using System.IO;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264Decoder : IDisposable
    {
        private readonly H264ParameterSet _parameterSet;

        private readonly MemoryStream _stream;




        public H264Decoder( byte[] sps , byte[] pps )
        {
            _parameterSet = new H264ParameterSet( sps , pps );

            _stream = new MemoryStream();
        }





        public bool TryDecode( H264NalUnitCollection nalunits , out byte[] result )
        {
            result = default;

            if ( nalunits == null )
            {
                return false;
            }

            while ( nalunits.Any() )
            {
                H264NalUnit nalunit = nalunits.Dequeue();

                if ( ! nalunit.TryValidate() || nalunit.CanSkip() )
                {
                    continue;
                }

                if ( ! nalunit.IsSingle )
                {
                    continue;
                }

                if ( nalunit.IsSPS )
                {
                    OnDecodingSPS( nalunit );
                }
                else if ( nalunit.IsPPS )
                {
                    OnDecodingPPS( nalunit );
                }
                else
                {
                    OnDecoding( nalunit );
                }
            }

            _stream.Flush();

            result = _stream.ToArray();

            return result.Length > 0;
        }

        public void Reset()
        {
            _stream.SetLength( 0 );
        }

        public void Dispose()
        {
            _stream.Dispose();
        }





        private static void OnDecodingFU_A( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetFuA();

            throw new NotImplementedException();
        }

        private static void OnDecodingFU_B( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetFuB();

            throw new NotImplementedException();
        }

        private static void OnDecodingSTAP_A( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetStapA();

            throw new NotImplementedException();
        }

        private static void OnDecodingSTAP_B( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetStapB();

            throw new NotImplementedException();
        }

        private static void OnDecodingSPS( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetSPS();

            throw new NotImplementedException();
        }

        private static void OnDecodingPPS( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetPPS();

            throw new NotImplementedException();
        }

        private static void OnDecoding( H264NalUnit nalunit )
        {
            var values = nalunit.Payload.GetAllData();
            
            throw new NotImplementedException();
        }
    }
}