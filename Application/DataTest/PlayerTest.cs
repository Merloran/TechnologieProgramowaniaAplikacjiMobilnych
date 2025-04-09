using Data;
using System.ComponentModel;

namespace DataTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void MoveTest()
        {
            IPlayer player = IPlayer.CreateInstance("Player", 0.0f, 0.0f, 1.0f);
            Assert.AreEqual(player.X, 0);
            Assert.AreEqual(player.Y, 0);
        }
    }
}