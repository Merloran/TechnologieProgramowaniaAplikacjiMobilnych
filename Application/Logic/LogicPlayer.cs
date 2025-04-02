using System;
using Data;

namespace Logic
{
    internal class LogicPlayer(IPlayer player) : ILogicPlayer
    {
        public string Name { get; } = player.Name;

        public float X { get; private set; } = player.X;
        public float Y { get; private set; } = player.Y;
    }
}