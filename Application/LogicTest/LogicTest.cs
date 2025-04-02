using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {
        private void DoNothing() { }
        private void DoNothingWithParameter(bool b) { }

        [TestMethod]
        public void AddPlayerTest()
        {
            DataAbstract? data = DataAbstract.CreateInstance();

            List<IPlayer> initialPlayers = data.GetPlayers();
            foreach (IPlayer player in initialPlayers)
            {
                data.Remove(player.Name);
            }

            LogicAbstract logic = LogicAbstract.CreateInstance(DoNothing, DoNothingWithParameter, data);

            logic.AddPlayer("TestPlayer");

            Assert.AreEqual(1, logic.GetPlayerCount());
            Assert.AreEqual(1, data.GetPlayers().Count);
            Assert.AreEqual("TestPlayer", data.GetPlayers()[0].Name);
        }

        [TestMethod]
        public void RemovePlayerTest()
        {
            DataAbstract? data = DataAbstract.CreateInstance();

            var initialPlayers = data.GetPlayers();
            foreach (IPlayer player in initialPlayers)
            {
                data.Remove(player.Name);
            }

            LogicAbstract logic = LogicAbstract.CreateInstance(DoNothing, DoNothingWithParameter, data);

            Assert.AreEqual(0, logic.GetPlayerCount());

            logic.AddPlayer("TestPlayer");
            Assert.AreEqual(1, logic.GetPlayerCount());

            logic.RemovePlayer("TestPlayer");

            Assert.AreEqual(0, logic.GetPlayerCount());
            Assert.AreEqual(0, data.GetPlayers().Count);
        }

        [TestMethod]
        public void MultiplePlayersTest()
        {
            DataAbstract? data = DataAbstract.CreateInstance();

            List<IPlayer> initialPlayers = data.GetPlayers();
            foreach (var player in initialPlayers)
            {
                data.Remove(player.Name);
            }

            LogicAbstract logic = LogicAbstract.CreateInstance(DoNothing, DoNothingWithParameter, data);

            logic.AddPlayer("Player1");
            logic.AddPlayer("Player2");
            logic.AddPlayer("Player3");

            Assert.AreEqual(3, logic.GetPlayerCount());

            logic.RemovePlayer("Player2");
            Assert.AreEqual(2, logic.GetPlayerCount());

            List<IPlayer> players = data.GetPlayers();
            Assert.IsTrue(players.Any(p => p.Name == "Player1"));
            Assert.IsTrue(players.Any(p => p.Name == "Player3"));
            Assert.IsFalse(players.Any(p => p.Name == "Player2"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataAbstract? data = DataAbstract.CreateInstance();
            List<IPlayer> players = data.GetPlayers();
            foreach (IPlayer player in players)
            {
                data.Remove(player.Name);
            }
        }
    }
}