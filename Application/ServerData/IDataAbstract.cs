namespace ServerData
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public interface IDataAbstract
    {
        public event Action onPlayersChange;
        public abstract List<IPlayer> GetPlayers();

        public static IDataAbstract CreateInstance()
        {
            return new Data();
        }

        public void MovePlayer(Guid playerId, Direction direction);
        public bool HasPlayer(Guid playerId);
        public Guid AddPlayer(string name, float x, float y, float speed);
    }
}
