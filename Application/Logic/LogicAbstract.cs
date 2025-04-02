using Data;

namespace Logic
{
    public abstract class LogicAbstract
    {
        public abstract bool AddPlayer(string name);
        public abstract bool RemovePlayer(string name);
        public abstract int GetPlayerCount();
        public abstract void MovePlayer(string dir);

        public abstract List<ILogicPlayer>? GetPlayers();

        public static LogicAbstract CreateInstance(
            Action playerUpdateCallback,
            Action<bool> reactiveUpdateCallback,
            DataAbstract? data = null)
        {
            return new Logic(data ?? DataAbstract.CreateInstance(),
                             playerUpdateCallback, 
                             reactiveUpdateCallback
                             );
        }
    }
}
