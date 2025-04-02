using Logic;

namespace PresentationModel
{
    public class Model(Action playerUpdateCallback, Action<bool> reactiveElementsUpdateCallback)
    {
        private LogicAbstract _logic = LogicAbstract.CreateInstance(playerUpdateCallback, reactiveElementsUpdateCallback);

        public void AddPlayer()
        {
            _logic.AddPlayer("test");
        }

        public void RemovePlayer()
        {
            _logic.RemovePlayer("test");
        }

        public void MoveUp()
        {
            _logic.MovePlayer("up");
        }

        public void MoveDown()
        {
            _logic.MovePlayer("down");
        }

        public void MoveLeft()
        {
            _logic.MovePlayer("left");
        }

        public void MoveRight()
        {
            _logic.MovePlayer("right");
        }

        public List<ModelPlayer>? GetPlayers()
        {
            return _logic.GetPlayers()
                         .Select(player => new ModelPlayer(player))
                         .ToList();
        }
    }
}
