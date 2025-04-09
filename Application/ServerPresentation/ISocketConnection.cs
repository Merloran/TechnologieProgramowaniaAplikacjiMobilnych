namespace ServerPresentation
{
    public interface ISocketConnection
    {
        public event Action<string> onGetMessage;
        public event Action onClose;
        public event Action onError;

        public Task SendAsync(string message);
        public Task DisconnectAsync();
        public Task SendTask(string message);
    }
}
