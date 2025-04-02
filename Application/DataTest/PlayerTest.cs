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
            IPlayer player = IPlayer.Create("Player", IVector2.Create(0.0f, 0.0f), 1.0f);
            Assert.AreEqual(player.Position, IVector2.Create(0, 0));
            player.Move(IInput.Create("up")); 
            Assert.AreEqual(player.Position, IVector2.Create(0, 1));
        }
    }
}