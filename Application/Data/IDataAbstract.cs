namespace Data
{
    public interface IDataAbstract : IObservable<List<IPlayer>>
    {
        private static IDataAbstract? instance; 
        public IConnection connection { get; }
        public IList<IPlayer> GetPlayers();

        public Task MovePlayer(Direction direction);
        public void RequestUpdate();
        public static IDataAbstract CreateInstance(IConnection? connection)
        {
            return instance ??= new Data(connection);
        }
    }
}
