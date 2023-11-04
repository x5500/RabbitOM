﻿using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public abstract class RTSPClient : IRTSPClient 
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        public event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        public event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        public event EventHandler<RTSPConnectedEventArgs> Connected;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        public event EventHandler<RTSPDisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;

        /// <summary>
        /// Raise when the streaming is running
        /// </summary>
        public event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;

        /// <summary>
        /// Raise when the streaming is stopped
        /// </summary>
        public event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RTSPConnectionErrorEventArgs> ConnectionError;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RTSPAuthenticationErrorEventArgs> AuthenticationError;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RTSPProtocolErrorEventArgs> ProtocolError;







        private bool _isDisposed;







        /// <summary>
        /// Finalizer
        /// </summary>
        ~RTSPClient()
        {
            Dispose( false );
        }







        /// <summary>
        /// Gets the sync root
        /// </summary>
        public abstract object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Check if the client is connected
        /// </summary>
        public abstract bool IsConnected
        {
            get;
        }

        /// <summary>
        /// Check if the communication has been started
        /// </summary>
        public abstract bool IsCommunicationStarted
        {
            get;
        }

        /// <summary>
        /// Check if the object has been disposed
        /// </summary>
        public bool IsDisposed
        {
            get => _isDisposed;
        }






        /// <summary>
        /// Start the communication
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool StartCommunication();

        /// <summary>
        /// Stop the communication
        /// </summary>
        public abstract void StopCommunication();

        /// <summary>
        /// Stop the communication
        /// </summary>
        /// <param name="shutdownTimeout">the shutdown timeout</param>
        public abstract void StopCommunication( TimeSpan shutdownTimeout );

        /// <summary>
        /// This method will block the call thread until the client has establish the connection
        /// </summary>
        /// <param name="timeout">the time to wait in milliseconds</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///     <para>This method will returns false in case the communication is not established.</para>
        ///     <para>And it will returns true when the communication is connected or already connected.</para>
        /// </remarks>
        public bool WaitForConnection(int timeout)
        {
            return WaitForConnection( TimeSpan.FromSeconds(timeout) );
        }

        /// <summary>
        /// This method will block the call thread until the client has establish the connection
        /// </summary>
        /// <param name="timeout">the time to wait</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///     <para>This method will returns false in case the communication is not established.</para>
        ///     <para>And it will returns true when the communication is connected or already connected.</para>
        /// </remarks>
        public abstract bool WaitForConnection(TimeSpan timeout);

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">true when the dispose method is explictly call</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopCommunication();
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Throw an exception if the object is already disposed
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        protected void EnsureNotDispose()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(RTSPClient));
            }
        }








        /// <summary>
        /// Gets the idle timeout
        /// </summary>
        /// <returns>returns a value</returns>
        protected abstract TimeSpan GetIdleTimeout();

        /// <summary>
        /// Gets the retry timeout
        /// </summary>
        /// <returns>returns a value</returns>
        protected abstract TimeSpan GetRetryTimeout();

        /// <summary>
        /// Wait the cancelation
        /// </summary>
        /// <param name="span">the time span</param>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoWaitCancelation(TimeSpan span);
        
        /// <summary>
        /// Process a shutdown
        /// </summary>
        protected abstract bool DoShutdown();

        /// <summary>
        /// Process a connect operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoConnect();

        /// <summary>
        /// Process a disconnect operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoDisconnect();

        /// <summary>
        /// Process a get options operaiton
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoGetOptions();

        /// <summary>
        /// Process a setup operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoSetup();

        /// <summary>
        /// Process a play operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoPlay();

        /// <summary>
        /// Process a teardown operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoTeardown();

        /// <summary>
        /// Process a ping operation used to maintain the network communication with the remote rtsp source
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected abstract bool DoHeartBeat();






        /// <summary>
        /// Occurs when the communication has been started
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnCommunicationStarted( RTSPCommunicationStartedEventArgs e )
        {
            CommunicationStarted?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when the communication has been stopped
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnCommunicationStopped( RTSPCommunicationStoppedEventArgs e )
        {
            CommunicationStopped?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when the client is connected
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnConnected( RTSPConnectedEventArgs e )
        {
            Connected?.TryInvoke( this ,e );
        }

        /// <summary>
        /// Occurs when the client is disconnected
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnDisconnected( RTSPDisconnectedEventArgs e )
        {
            Disconnected?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when an data has been received
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnPacketReceived( RTSPPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when the streaming is started
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnStreamingStarted(RTSPStreamingStartedEventArgs e)
        {
            StreamingStarted?.TryInvoke(this, e);
        }

        /// <summary>
        /// Occurs when the streaming is stopped
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            StreamingStopped?.TryInvoke(this, e);
        }

        /// <summary>
        /// Occurs when the connection has failed
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnConnectionError(RTSPConnectionErrorEventArgs e)
        {
            ConnectionError?.TryInvoke(this, e);
        }

        /// <summary>
        /// Occurs when the authentication has failed
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnAuthenticationError( RTSPAuthenticationErrorEventArgs e )
        {
            AuthenticationError?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when a protocol error has been detected
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnProtocolError(RTSPProtocolErrorEventArgs e)
        {
            ProtocolError?.TryInvoke(this, e);
        }
    }
}
