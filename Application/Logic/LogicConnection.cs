
namespace Logic
{
    internal class LogicConnection : ILogicConnection
    {
        public event Action<string>? log;
        public event Action<string>? onGetMessage;

        public event Action? onStateChange;
        public event Action? onError;
        public event Action? onDisconnect;

        internal Data.IConnection connection { get; }

        public LogicConnection(Data.IConnection connection)
        {
            this.connection = connection;

            connection.onStateChange += () => onStateChange?.Invoke();
            connection.onGetMessage  += (message) => onGetMessage?.Invoke(message);
            connection.onError       += () => onError?.Invoke();
            connection.log           += (message) => log?.Invoke(message);
        }

        public async Task Connect(Uri uri)
        {
            await connection.Connect(uri);
        }

        public async Task Disconnect()
        {
            if (connection != null)
            {
                await connection.Disconnect();
            }
        }

        public bool IsConnected()
        {
            return connection != null && connection.IsConnected();
        }

        public async Task SendAsync(string message)
        {
            if (connection != null)
            {
                await connection.SendAsync(message);
            }
        }
    }
}
