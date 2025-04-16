namespace ServerData
{
    internal class Data : IDataAbstract
    {
        public event Action onPlayersChange;
        private readonly Dictionary<Guid, IPlayer> players = new();
        private readonly object playersLock = new();

        public Guid AddPlayer(string name, float x, float y, float speed)
        {
            Guid newGuid = Guid.NewGuid();
            lock (playersLock)
            {
                players.Add(newGuid, IPlayer.Create(name, x, y, speed));
            }
            onPlayersChange.Invoke();
            return newGuid;
        }

        public IList<IPlayer> GetPlayers()
        {
            List<IPlayer> result = [];
            lock (playersLock)
            {
                result.AddRange(players.Values.Select(item => item));
            }
            return result;
        }

        public void MovePlayer(Guid playerId, Direction direction)
        {
            lock (playersLock)
            {
                if (!players.TryGetValue(playerId, out IPlayer? player))
                {
                    return;
                }

                switch (direction)
                {
                    case Direction.Up:
                    {
                        player.Y -= player.Speed;
                        break;
                    }
                    case Direction.Down:
                    {
                        player.Y += player.Speed;
                        break;
                    }
                    case Direction.Left:
                    {
                        player.X -= player.Speed;
                        break;
                    }
                    case Direction.Right:
                    {
                        player.X += player.Speed;
                        break;
                    }
                }
                onPlayersChange.Invoke();
            }
        }

        public bool HasPlayer(Guid playerId)
        {
            lock (playersLock)
            {
                return players.ContainsKey(playerId);
            }
        }
    }
}
