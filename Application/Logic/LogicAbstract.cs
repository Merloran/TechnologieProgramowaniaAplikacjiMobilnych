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
        public List<ILogicPlayer> GetPlayers();
        public Task MovePlayer(Direction moveDirection);
        public static ILogicAbstract CreateInstance(Action playerUpdateCallback, IDataAbstract? data = null)
        {
            return new Logic(data ?? IDataAbstract.CreateInstance(null),
                             playerUpdateCallback);
        }
    }
}
