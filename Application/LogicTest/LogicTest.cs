using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace LogicTest
{
    public class MockDataStorage : DataAbstract
    {
        public List<IPlayer> players = [];

        public override void Add(IPlayer player)
        {
            players.Add(player);
        }

        public override void Remove(string name)
        {
            players.Remove(players.Where(i => i.Name == name).Single());
        }

        public override int GetPlayerCount()
        {
            return players.Count;
        }

        public override List<IPlayer> GetPlayers()
        {
            return new List<IPlayer>(players);
        }

        public override void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber)
        {
            // do nothing
        }
    }

    [TestClass]
    public class LogicTest
    {
        // created just to pass as needed "Action" parameter that is called every time a player is moved
        public void DoNothing() { }
        public void DoNothing(bool b) { }

        [TestMethod]
        public void AddPlayerTest()
        {
            var dataStorage = new MockDataStorage();
            var logic = LogicAbstract.CreateInstance(DoNothing, DoNothing, dataStorage);
            logic.AddPlayer("Player");
            Assert.AreEqual(1, logic.GetPlayerCount());
        }

        [TestMethod]
        public void RemovePlayerTest()
        {
            var dataStorage = new MockDataStorage();
            var logic = LogicAbstract.CreateInstance(DoNothing, DoNothing, dataStorage);
            Assert.AreEqual(0, logic.GetPlayerCount());

            logic.AddPlayer("Player");
            logic.RemovePlayer("Player");
            Assert.AreEqual(0, logic.GetPlayerCount());
        }
    }
}