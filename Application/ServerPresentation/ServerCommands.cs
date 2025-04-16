namespace ServerPresentation
{
    public enum Headers
    {
        GetPlayersCommand,
        MovePlayerCommand,
        JoinResponse,
        UpdatePlayersResponse,
        MovePlayerResponse,
    }

    public abstract class ServerCommand
    {
        public Headers Header { get; set; }
    }

    [Serializable]
    public class GetPlayersCommand : ServerCommand
    {
    }

    [Serializable]
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    public class MovePlayerCommand : ServerCommand
    {
        public Guid TransactionId { get; set; }
        public Guid PlayerId { get; set; }
        public Direction Direction { get; set; }
    }

    public abstract class ServerResponse
    {
        public Headers Header { get; set; }
    }

    public class JoinResponse : ServerResponse
    {
        public Guid GuidForPlayer { get; set; }
    }

    public class PlayerData
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }
    }

    public class UpdatePlayersResponse : ServerResponse
    {
        public ICollection<PlayerData> Players { get; set; }
    }

    public class MovePlayerResponse : ServerResponse
    {
        public Guid TransactionId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
