using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace ServerPresentation
{
    internal class SocketConnection : ISocketConnection
    {
        public event Action<string> onGetMessage;
        public event Action onClose;
        public event Action onError;

        private readonly WebSocket webSocket;
        private IPEndPoint remoteEndPoint;
        private Action<string> log;
        private Uri uri;
        public Task DisconnectAsync()
        {
            return webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                                        "Closing started...",
                                        CancellationToken.None);
        }

        public SocketConnection(WebSocket webSocket, IPEndPoint remoteEndPoint)
        {
            this.webSocket      = webSocket;
            this.remoteEndPoint = remoteEndPoint;
            Task.Factory.StartNew(() => ProcessConnection(webSocket));
        }

        public async Task SendAsync(string message)
        {
            await SendTask(message);
        }

        public Task SendTask(string message)
        {
            if (webSocket.State != WebSocketState.Open)
            {
                return Task.CompletedTask;
            }

            return webSocket.SendAsync(Encoding.UTF8.GetBytes(message).ToArray(),
                                       WebSocketMessageType.Text, 
                                       true, 
                                       CancellationToken.None);
        }

        private void ProcessConnection(WebSocket ws)
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                ArraySegment<byte> bufferView = new ArraySegment<byte>(buffer);
                WebSocketReceiveResult receiveResult = ws.ReceiveAsync(bufferView, CancellationToken.None).Result;
                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    onClose?.Invoke();
                    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None);
                    return;
                }
                int count = receiveResult.Count;
                while (!receiveResult.EndOfMessage)
                {
                    if (count >= buffer.Length)
                    {
                        onClose?.Invoke();
                        ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                        return;
                    }
                    bufferView = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    receiveResult = ws.ReceiveAsync(bufferView, CancellationToken.None).Result;
                    count += receiveResult.Count;
                }
                string _message = Encoding.UTF8.GetString(buffer, 0, count);
                onGetMessage?.Invoke(_message);
            }
        }

        public static async Task StartServer(int p2pPort, Action<ISocketConnection> onConnection)
        {
            Uri uri = new Uri($@"http://localhost:{p2pPort}/");
            await ServerLoop(uri, onConnection);
        }

        private static async Task ServerLoop(Uri uri, Action<ISocketConnection> onConnection)
        {
            HttpListener server = new HttpListener();
            server.Prefixes.Add(uri.ToString());
            server.Start();
            while (true)
            {
                HttpListenerContext hc = await server.GetContextAsync();
                if (!hc.Request.IsWebSocketRequest)
                {
                    hc.Response.StatusCode = 400;
                    hc.Response.Close();
                }
                HttpListenerWebSocketContext context = await hc.AcceptWebSocketAsync(null);
                ISocketConnection ws = new SocketConnection(context.WebSocket, hc.Request.RemoteEndPoint);
                onConnection?.Invoke(ws);
            }
        }
    }
}
