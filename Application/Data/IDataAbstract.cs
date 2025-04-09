using System.Collections.Specialized;

namespace Data
{
    public interface IDataAbstract : IObservable<List<IPlayer>>
    {
        public IConnection connection { get; }
        public abstract List<IPlayer> GetPlayers();

        public Task MovePlayer(Direction direction);
        public void RequestUpdate();
        public static IDataAbstract CreateInstance(IConnection? connection)
        {
            return new Data(connection);
        }
    }
}
