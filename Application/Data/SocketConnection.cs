using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class SocketConnection : ISocketConnection
    {
        private readonly ClientWebSocket webSocket;
        private readonly Action<string> log;
        private readonly Uri peer;

        public event Action<string> onGetMessage;
        public event Action onClose;
        public event Action onError;

        public SocketConnection(ClientWebSocket webSocket, Uri peer, Action<string> log)
        {
            this.webSocket = webSocket;
            this.peer = peer;
            this.log = log;
            Task.Run(ProcessConnection);
        }

        public Task DisconnectAsync()
        {
            return webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing socket", CancellationToken.None);
        }

        public async Task SendAsync(string message)
        {
            await SendTask(message);
        }

        public Task SendTask(string message)
        {
            return webSocket.SendAsync(Encoding.UTF8.GetBytes(message).ToArray(),
                                       WebSocketMessageType.Text, 
                                       true, 
                                       CancellationToken.None);
        }

        private void ProcessConnection()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    ArraySegment<byte> bufferView = new ArraySegment<byte>(buffer);
                    WebSocketReceiveResult result = webSocket.ReceiveAsync(bufferView, CancellationToken.None).Result;
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        onClose?.Invoke();
                        webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None).Wait();
                        return;
                    }

                    int count = result.Count;
                    while (!result.EndOfMessage)
                    {
                        if (count >= buffer.Length)
                        {
                            onClose?.Invoke();
                            webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, 
                                                 $"Message exceeded buffer size: {count}", 
                                                 CancellationToken.None).Wait();
                            return;
                        }

                        bufferView = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                        result = webSocket.ReceiveAsync(bufferView, CancellationToken.None).Result;
                        count += result.Count;
                    }
                    string message = Encoding.UTF8.GetString(buffer, 0, count);
                    onGetMessage?.Invoke(message);
                }
            }
            catch (Exception exception)
            {
                log($"Connection interrupted by: {exception}");
                webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, 
                                     "Connection interrupted by an exception", 
                                     CancellationToken.None).Wait();
            }
        }

        public static async Task<ISocketConnection> Connect(Uri peer, Action<string> log)
        {
            ClientWebSocket webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(peer, CancellationToken.None);
            switch (webSocket.State)
            {
                case WebSocketState.Open:
                {
                    log.Invoke($"Open socket to server {peer}");
                    ISocketConnection socket = new SocketConnection(webSocket, peer, log);
                    return socket;
                }
                default:
                {
                    log.Invoke($"Cannot connect, socket state: {webSocket.State}");
                    throw new WebSocketException($"Cannot connect to remote node status {webSocket.State}");
                }
            }
        }
    }
}
