﻿using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client configuration
    /// </summary>
    public sealed class RTSPMulticastClientConfiguration : RTSPClientConfiguration
    {
        /// <summary>
        /// Represent the default port
        /// </summary>
        public const int DefaultPort = 61024;

        /// <summary>
        /// Represent the default TTL
        /// </summary>
        public const byte DefaultTTL = 5;






        private string  _address          = string.Empty;

        private int     _port             = DefaultPort;

        private byte    _ttl              = DefaultTTL;







        /// <summary>
        /// Gets the mulitcast address
        /// </summary>
        public string Address
        {
            get
            {
				lock ( SyncRoot )
				{
					return _address;
	            }
            }

            private set 
            {
                lock ( SyncRoot )
                {
                    _address = value ?? string.Empty;
                }
            }
        }


        /// <summary>
        /// Gets the udp port
        /// </summary>
        public int Port
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _port;
                }
            }

            private set
            {
                lock ( SyncRoot )
                {
                    _port = value;
                }
            }
        }

        /// <summary>
        /// Gets the the ttl
        /// </summary>
        public byte TTL
        {
            get
            {
                lock (SyncRoot)
                {
                    return _ttl;
                }
            }

            private set
            {
                lock (SyncRoot)
                {
                    _ttl = value;
                }
            }
        }








        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="address">the multicast address</param>
        /// <param name="port">the port</param>
        /// <returns>returns an instance</returns>
        public static RTSPMulticastClientConfiguration CreateConfiguration(string uri , string address, int port )
        {
            return new RTSPMulticastClientConfiguration()
            {
                Uri = uri,
                Address = address,
                Port = port,
            };
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="address">the address</param>
        /// <param name="port">the port</param>
        /// <param name="userName">the username</param>
        /// <param name="password">the password</param>
        /// <returns>returns an instance</returns>
        public static RTSPMulticastClientConfiguration CreateConfiguration(string uri,string address,int port, string userName,string password)
        {
            return new RTSPMulticastClientConfiguration()
            {
                Uri = uri,
                Address = address,
                Port = port,
                UserName = userName,
                Password = password
            };
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="address">the address</param>
        /// <param name="port">the port</param>
        /// <param name="userName">the username</param>
        /// <param name="password">the password</param>
        /// <param name="keepAliveType">the keep alive type</param>
        /// <param name="mediaFormat">the media format</param>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <param name="retriesInterval">the retries interval</param>
        /// <param name="keepAliveInterval">the keep alive interval</param>
        /// <returns>returns an instance</returns>
        public static RTSPMulticastClientConfiguration CreateConfiguration(
            string uri, 
            string address,
            int port ,
            string userName, 
            string password, 
            RTSPKeepAliveType keepAliveType, 
            RTSPMediaFormatType mediaFormat, 
            TimeSpan receiveTimeout,
            TimeSpan sendTimeout,
            TimeSpan retriesInterval,
            TimeSpan keepAliveInterval
            )
        {
            return new RTSPMulticastClientConfiguration()
            {
                Uri = uri,  
                Address = address,
                Port = port,
                UserName = userName,
                Password = password ,
                KeepAliveType = keepAliveType,
                MediaFormat = mediaFormat,
                ReceiveTimeout = receiveTimeout ,
                SendTimeout = sendTimeout,
                RetriesInterval = retriesInterval,
                KeepAliveInterval = keepAliveInterval,
            };
        }
    }
}
