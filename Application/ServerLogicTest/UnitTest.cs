using ServerData;
using ServerLogic;
using Direction = ServerData.Direction;

namespace ServerLogicTest
{
    internal class MockData : IDataAbstract
    {
        public static Guid testGuid = Guid.Empty;

        public event Action? onPlayersChange;

        public Guid AddPlayer(string name, float x, float y, float speed)
        {
            return testGuid;
        }

        IList<IPlayer> IDataAbstract.GetPlayers()
        {
            return GetPlayers();
        }

        public List<IPlayer> GetPlayers()
        {
            return [];
        }

        public bool HasPlayer(Guid playerId)
        {
            return false;
        }

        public void MovePlayer(Guid playerId, Direction direction)
        {
            onPlayersChange.Invoke();
        }
    }

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateTest()
        {
            ILogicAbstract logic = ILogicAbstract.CreateInstance(null, new MockData());
            Assert.IsNotNull(logic);
        }

        [TestMethod]
        public void MoveExceptionTest()
        {
            ILogicAbstract logic = ILogicAbstract.CreateInstance(null, new MockData());

            Action act = () => { logic.MovePlayer(Guid.Empty, ServerLogic.Direction.Up); };

            Assert.ThrowsException<KeyNotFoundException>(act);
        }

        [TestMethod]
        public void AddPlayerTest()
        {
            ILogicAbstract logic = ILogicAbstract.CreateInstance(null, new MockData());

            Assert.AreEqual(MockData.testGuid, logic.AddPlayer());
        }
    }
}