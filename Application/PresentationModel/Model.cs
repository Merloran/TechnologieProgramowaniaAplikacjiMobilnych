using Logic;

namespace PresentationModel
{
    public class Model : IModel
    {
        public Action playerUpdateCallback;
        private ILogicAbstract logic { get; }


        public Model(ILogicAbstract logic)
        {
            this.logic = logic;
        }

        public List<IModelPlayer> GetPlayers()
        {
            return logic.GetPlayers()
                        .Select(player => new ModelPlayer(player.Name, player.X, player.Y, player.Speed))
                        .Cast<IModelPlayer>()
                        .ToList();
        }

        public void MoveUp()
        {
            Task.Run(async () => await logic.MovePlayer(Direction.Up));
        }

        public void MoveDown()
        {
            Task.Run(async () => await logic.MovePlayer(Direction.Down));
        }

        public void MoveLeft()
        {
            Task.Run(async () => await logic.MovePlayer(Direction.Left));
        }

        public void MoveRight()
        {
            Task.Run(async () => await logic.MovePlayer(Direction.Right));
        }
    }
}
