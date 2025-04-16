using Data;

namespace Logic
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
        public Task MovePlayer(Direction moveDirection);
        public static ILogicAbstract CreateInstance(Action playerUpdateCallback, IDataAbstract? data = null)
        {
            return instance ??= new Logic(data ?? IDataAbstract.CreateInstance(null), playerUpdateCallback);
        }
    }
}
