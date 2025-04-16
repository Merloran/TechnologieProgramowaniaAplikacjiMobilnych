using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Data
{

    internal class Data : IDataAbstract
    {
        public IConnection connection { get; set; }

        private HashSet<IObserver<List<IPlayer>>> observers;

        private List<IPlayer> players;
        private Guid ourPlayerId;

        public Data(IConnection? connectionService)
        {
            connection = connectionService ?? new Connection();
            connection.onGetMessage += OnGetMessage;
            observers  = [];
        }

        ~Data()
        {
            List<IObserver<List<IPlayer>>> cachedObservers = observers.ToList();
            foreach (IObserver<List<IPlayer>>? observer in cachedObservers)
            {
                observer?.OnCompleted();
            }
        }

        public IList<IPlayer> GetPlayers()
        {
            return players;
        }

        public void RemovePlayer(string name)
        {
            players.Remove(players.Single(i => i.Name == name));
        }

        public async Task MovePlayer(Direction direction)
        {
            if (connection.IsConnected())
            {
                MovePlayerCommand cmd = new MovePlayerCommand
                {
                    Header = Headers.MovePlayerCommand,
                    PlayerId = ourPlayerId,
                    Direction = direction
                };

                await connection.SendAsync(JsonSerializer.Serialize(cmd));
            }
        }

        public void OnGetMessage(string message)
        {
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
                case Headers.JoinResponse:
                {
                    JoinResponse response = JsonSerializer.Deserialize<JoinResponse>(message);
                    ourPlayerId = response.GuidForPlayer;

                    RequestUpdate();
                    break;
                }
                case Headers.UpdatePlayersResponse:
                {
                    UpdatePlayersResponse response = JsonSerializer.Deserialize<UpdatePlayersResponse>(message);
                    players = [];
                    foreach (PlayerData p in response.Players)
                    {
                        players.Add(new Player(p.Name, p.X, p.Y, p.Speed));
                    }
                    foreach (IObserver<List<IPlayer>>? observer in observers)
                    {
                        observer.OnNext([..players]);
                    }

                    break;
                }
                case Headers.MovePlayerResponse:
                {
                    MovePlayerResponse response = JsonSerializer.Deserialize<MovePlayerResponse>(message);
                    break;
                }
            }
        }

        public void RequestUpdate()
        {
            GetPlayersCommand cmd = new GetPlayersCommand();
            connection.SendAsync(JsonSerializer.Serialize(cmd));
        }

        public IDisposable Subscribe(IObserver<List<IPlayer>> observer)
        {
            observers.Add(observer);
            return new DataDisposable(this, observer);
        }

        private void Unsubscribe(IObserver<List<IPlayer>> observer)
        {
            observers.Remove(observer);
        }

        private class DataDisposable : IDisposable
        {
            private readonly Data data;
            private readonly IObserver<List<IPlayer>> observer;

            public DataDisposable(Data data, IObserver<List<IPlayer>> observer)
            {
                this.data = data;
                this.observer = observer;
            }

            public void Dispose()
            {
                data.Unsubscribe(observer);
            }
        }
    }
}
