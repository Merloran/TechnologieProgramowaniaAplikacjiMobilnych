using Logic;

namespace PresentationModel
{
    public interface IModel
    {
        private static IModel? instance;

        public static IModel CreateInstance(Action playerUpdateCallback)
        {
            return instance ??= new Model(ILogicAbstract.CreateInstance(playerUpdateCallback));
        }

        public IList<IModelPlayer> GetPlayers();
        public void MoveUp();
        public void MoveDown();
        public void MoveLeft();
        public void MoveRight();
    }
}
