using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            observers  = new HashSet<IObserver<List<IPlayer>>>();
        }

        ~Data()
        {
            List<IObserver<List<IPlayer>>> cachedObservers = observers.ToList();
            foreach (IObserver<List<IPlayer>>? observer in cachedObservers)
            {
                observer?.OnCompleted();
            }
        }

        public List<IPlayer> GetPlayers()
        {
            return players;
        }

        public void RemovePlayer(string name)
        {
            players.Remove(players.Where(i => i.Name == name).Single());
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
            if (connection == null)
            {
                return;
            }

            string header = Headers.GetHeader(message);
            if (header == null)
            {
                return;
            }

            if (header == Headers.JoinResponse)
            {
                JoinResponse response = JsonSerializer.Deserialize<JoinResponse>(message);
                ourPlayerId = response.GuidForPlayer;

                RequestUpdate();
            }

            if (header == Headers.UpdatePlayersResponse)
            {
                UpdatePlayersResponse response = JsonSerializer.Deserialize<UpdatePlayersResponse>(message);
                players = new List<IPlayer>();
                foreach (PlayerData p in response.Players)
                {
                    players.Add(new Player(p.Name, p.X, p.Y, p.Speed));
                }
                foreach (IObserver<List<IPlayer>>? observer in observers)
                {
                    observer.OnNext(new List<IPlayer>(players));
                }
            }

            if (header == Headers.MovePlayerResponse)
            {
                MovePlayerResponse response = JsonSerializer.Deserialize<MovePlayerResponse>(message);
            }
        }

        public void RequestUpdate()
        {
            if (connection == null)
            {
                return;
            }
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
