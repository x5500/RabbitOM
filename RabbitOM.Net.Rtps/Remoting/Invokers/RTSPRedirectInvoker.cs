﻿using System;

namespace RabbitOM.Net.Rtps.Remoting.Invokers
{
    /// <summary>
    /// Represent the proxy invoker
    /// </summary>
    public sealed class RTSPRedirectInvoker : RTSPInvoker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        internal RTSPRedirectInvoker( RTSPProxy proxy )
            : base( proxy , RTSPMethodType.Redirect )
        {
        }
    }
}
