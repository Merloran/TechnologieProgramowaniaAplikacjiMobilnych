using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using Data;

namespace Logic
{
    internal class Logic : LogicAbstract
    {
        private DataAbstract? _data;

        private Action _updateCallback;
        private Action<bool> _reactiveElementsUpdateCallback;

        public Logic(DataAbstract? data, Action playerUpdateCallback, Action<bool> reactiveElementsUpdateCallback)
        {
            this._data = data;
            this._updateCallback = playerUpdateCallback;
            this._reactiveElementsUpdateCallback = reactiveElementsUpdateCallback;
            UpdateReactiveElements();
        }

        public override bool AddPlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            var player = IPlayer.Create(name, IVector2.Create(100, 100), 20.0f);
            player.PropertyChanged += UpdatePlayer;
            _data?.Add(player);

            UpdatePlayers(this, null);

            return true;
        }

        public override bool RemovePlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            _data?.Remove(name);
            return true;
        }

        public override int GetPlayerCount()
        {
            Debug.Assert(_data != null, nameof(_data) + " != null");
            return _data.GetPlayerCount();
        }

        public override void MovePlayer(string dir)
        {
            List<IPlayer>? players = _data?.GetPlayers();
            IInput input = IInput.Create(dir);
            Debug.Assert(players != null, nameof(players) + " != null");
            foreach (IPlayer player in players)
            {
                player.Move(input);
            }
        }

        private void UpdatePlayer(object sender, PropertyChangedEventArgs e)
        {
            _updateCallback.Invoke();
        }

        private void UpdatePlayers(object sender, NotifyCollectionChangedEventArgs? e)
        {
            _updateCallback.Invoke();
        }

        public override List<ILogicPlayer>? GetPlayers()
        {
            return _data?.GetPlayers()
                    .Select(player => new LogicPlayer(player))
                    .Cast<ILogicPlayer>()
                    .ToList();
        }

        private async void UpdateReactiveElements()
        {
            bool leftOrRight = false;
            while (true)
            {
                await Task.Delay(1000);
                leftOrRight = !leftOrRight;
                _reactiveElementsUpdateCallback.Invoke(leftOrRight);
            }
        }
    }
}
