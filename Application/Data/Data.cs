using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Data
{
    internal class Data : DataAbstract
    {
        private ObservableCollection<IPlayer> _players = [];

        public override void Add(IPlayer player)
        {
            _players.Add(player);
        }

        public override void Remove(string name)
        {
            _players.Remove(_players.Single(i => i.Name == name));
        }

        public override int GetPlayerCount()
        {
            return _players.Count;
        }

        public override void AddSubscriber(Action<object, NotifyCollectionChangedEventArgs> subscriber)
        {
            _players.CollectionChanged += new NotifyCollectionChangedEventHandler(subscriber);
        }

        public override List<IPlayer> GetPlayers()
        {
            return [.._players];
        }
    }
}
