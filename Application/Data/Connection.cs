

namespace Data
{
    internal class Connection : IConnection
    {
        public event Action<string>? log;
        public event Action<string>? onGetMessage;

        public event Action? onStateChange;
        public event Action? onError;
        public event Action? onDisconnect;

        internal SocketConnection socketConnection { get; private set; }

        public async Task Connect(Uri uri)
        {
            try
            {
                socketConnection = (SocketConnection)await SocketConnection.Connect(uri, log);
                socketConnection.onGetMessage += (message) => onGetMessage?.Invoke(message);
                socketConnection.onError      += () => onError?.Invoke();
                socketConnection.onClose      += () => onDisconnect?.Invoke();
            }
            catch (Exception e)
            {
                log?.Invoke(e.Message);
                onError?.Invoke();
            }
        }

        public async Task Disconnect()
        {
            if (socketConnection != null)
            {
                await socketConnection.DisconnectAsync();
            }
        }

        public bool IsConnected()
        {
            return socketConnection != null;
        }

        public async Task SendAsync(string message)
        {
            if (socketConnection != null)
            {
                await socketConnection.SendAsync(message);
            }
        }
    }
}
