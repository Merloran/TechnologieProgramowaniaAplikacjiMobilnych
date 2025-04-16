using Newtonsoft.Json.Linq;
using ServerLogic;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ServerPresentation
{
    internal class Program
    {
        private readonly ILogicAbstract logic;
        private ISocketConnection? connection;

        private Program()
        {
            logic = ILogicAbstract.CreateInstance(UpdatePlayers);
        }

        private async Task StartConnection()
        {
            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                await SocketConnection.StartServer(13337, OnConnect);
            }
        }

        private async void OnConnect(ISocketConnection connection)
        {
            Console.WriteLine($"Connected with {connection}");

            connection.onGetMessage += OnMessage;
            connection.onError      += OnError;
            connection.onClose      += OnClose;

            this.connection = connection;

            // Create player for this connection
            Guid newPlayerId = logic.AddPlayer();

            JoinResponse joinResponse = new JoinResponse
            {
                Header        = Headers.JoinResponse,
                GuidForPlayer = newPlayerId,
            };
            await connection.SendAsync(JsonSerializer.Serialize(joinResponse));
        }

        private async void OnMessage(string message)
        {
            if (connection == null)
            {
                return;
            }

            Headers? header = null;
            JObject obj = JObject.Parse(message);
            if (obj.TryGetValue("Header", out JToken? value))
            {
                header = value.ToObject<Headers>();
            }

            switch (header)
            {
                case null:
                    return;
                case Headers.MovePlayerCommand:
                {
                    MovePlayerCommand cmd = JsonSerializer.Deserialize<MovePlayerCommand>(message);

                    MovePlayerResponse response = new MovePlayerResponse
                    {
                        TransactionId = cmd.TransactionId
                    };
                    try
                    {
                        logic.MovePlayer(cmd.PlayerId, (ServerLogic.Direction)cmd.Direction);
                        response.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex} --- Failed to move the player");
                        response.IsSuccess = false;
                    }
                    await connection.SendAsync(JsonSerializer.Serialize(response));

                    UpdatePlayers();
                    return;
                }
                case Headers.GetPlayersCommand:
                    UpdatePlayers();
                    return;
            }
        }

        private async void UpdatePlayers()
        {
            if (connection == null) 
            { 
                return; 
            }

            UpdatePlayersResponse response = new UpdatePlayersResponse()
            {
                Header = Headers.UpdatePlayersResponse,
                Players = logic
                    .GetPlayers()
                    .Select(p => new PlayerData { Name = p.Name, X = p.X, Y = p.Y, Speed = p.Speed })
                    .ToArray()
            };
            await connection.SendAsync(JsonSerializer.Serialize(response));
        }

        private void OnError()
        {
            Console.WriteLine("Connection error");
        }

        private void OnClose()
        {
            Console.WriteLine("Connection closed");
            connection = null;
        }

        private static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.StartConnection();
        }
    }
}
