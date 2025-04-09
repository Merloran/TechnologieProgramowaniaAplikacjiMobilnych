using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ServerData
{
    public interface IPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public static IPlayer Create(string name, float x, float y, float speed)
        {
            return new Player(name, x, y, speed);
        }
    }
}