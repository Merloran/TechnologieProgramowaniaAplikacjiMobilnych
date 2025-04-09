using Logic;

namespace PresentationModel
{
    public interface IModel
    {
        public static IModel Create(Action playerUpdateCallback)
        {
            return new Model(ILogicAbstract.CreateInstance(playerUpdateCallback));
        }

        public List<IModelPlayer> GetPlayers();
        public void MoveUp();
        public void MoveDown();
        public void MoveLeft();
        public void MoveRight();
    }
}
