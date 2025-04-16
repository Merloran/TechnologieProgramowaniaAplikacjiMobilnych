using Data;
using Logic;

namespace LogicTest
{
    internal class DataMock : IDataAbstract
    {
        public IConnection connection { get; set; }

        public event Action onPlayersChanged;

        public IList<IPlayer> GetPlayers()
        {
            return new List<IPlayer>();
        }

        public Task MovePlayer(Data.Direction direction)
        {
            onPlayersChanged.Invoke();
            return Task.CompletedTask;
        }

        public void RequestUpdate()
        {
        }

        public IDisposable Subscribe(IObserver<List<IPlayer>> observer)
        {
            return null;
        }
    }


    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateTest()
        {
            ILogicAbstract logic = ILogicAbstract.CreateInstance(null, new DataMock());
            Assert.IsNotNull(logic);
        }
    }
}