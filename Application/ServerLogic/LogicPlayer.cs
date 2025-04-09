using System;
using Data;

namespace ServerLogic
{
    internal class LogicPlayer(string name, float x, float y, float speed) : ILogicPlayer
    {
        public string Name { get; set; } = name;
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Speed { get; set; } = speed;
    }
}