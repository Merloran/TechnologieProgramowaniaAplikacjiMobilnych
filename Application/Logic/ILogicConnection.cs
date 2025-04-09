namespace Logic
{
    public interface ILogicConnection
    {
        public event Action<string>? log;
        public event Action? onStateChange;
        public event Action<string>? onGetMessage;
        public event Action? onError;
        public event Action? onDisconnect;


        public Task Connect(Uri uri);
        public Task Disconnect();
        public bool IsConnected();
        public Task SendAsync(string message);
    }
}
