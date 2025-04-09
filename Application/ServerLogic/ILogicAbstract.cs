using ServerData;
using System;
using System.Collections.Generic;
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
        public List<ILogicPlayer> GetPlayers();
        public Guid AddPlayer();
        public void MovePlayer(Guid playerId, Direction direction);
        public static ILogicAbstract CreateInstance(Action UpdatePlayersCallback, IDataAbstract? data = null)
        {
            IDataAbstract dataApi = data ?? IDataAbstract.CreateInstance();
            dataApi.onPlayersChange += UpdatePlayersCallback;
            return new Logic(dataApi);
        }

    }
}