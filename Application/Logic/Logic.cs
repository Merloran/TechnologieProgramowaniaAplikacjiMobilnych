using Data;

namespace Logic
{
    internal class Logic : ILogicAbstract, IObserver<List<IPlayer>>
    {
        public ILogicConnection connection { get; }

        public Action updateCallback;

        private IDataAbstract data { get; }
        private List<ILogicPlayer> players;
        private IDisposable dataSubscriptionHandle;

        public Logic(IDataAbstract data, Action playerUpdateCallback)
        {
            this.data = data;
            this.updateCallback = playerUpdateCallback;
            data.Subscribe(this);

            if (data.connection == null)
            {
                return;
            }

            connection = new LogicConnection(data.connection);

            connection.onStateChange += OnStateChanged;
            connection.onDisconnect  += OnStateChanged;
            connection.onError       += OnStateChanged;
            connection.log           += Log;

            Task.Run(() => connection.Connect(new Uri(@"ws://localhost:13337")));
        }

        public IList<ILogicPlayer> GetPlayers()
        {
            return players;
        }

        public async Task MovePlayer(Direction moveDirection)
        {
            await data.MovePlayer((Data.Direction)moveDirection);
        }

        private void OnStateChanged()
        {
            bool actual = data.connection.IsConnected();

            if (!actual)
            {
                Task.Run(() => connection.Connect(new Uri(@"ws://localhost:13337")));
            }
            else
            {
                data.RequestUpdate();
            }
        }

        private void Log(string text)
        {
            Console.WriteLine(text);
        }

        public void OnCompleted()
        {
            dataSubscriptionHandle?.Dispose();
        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(List<IPlayer> value)
        {
            players = value.Select(player => new LogicPlayer(player.Name, player.X, player.Y, player.Speed))
                           .Cast<ILogicPlayer>()
                           .ToList();
            updateCallback?.Invoke();
        }
    }
}
