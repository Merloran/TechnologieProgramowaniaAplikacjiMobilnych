using ServerData;
namespace ServerLogic
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public interface ILogicAbstract
    {
        private static ILogicAbstract? instance;
        public IList<ILogicPlayer> GetPlayers();
        public Guid AddPlayer();
        public void MovePlayer(Guid playerId, Direction direction);
        public static ILogicAbstract CreateInstance(Action? updatePlayersCallback, IDataAbstract? data = null)
        {
            if (instance != null)
            {
                return instance;
            }

            IDataAbstract dataApi = data ?? IDataAbstract.CreateInstance();
            dataApi.onPlayersChange += updatePlayersCallback;
            return instance ??= new Logic(dataApi);

        }

    }
}