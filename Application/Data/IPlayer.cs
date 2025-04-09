using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

public interface IPlayer
{
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Speed { get; set; }

    public static IPlayer CreateInstance(string name, float x, float y, float speed)
    {
        return new Player(name, x, y, speed);
    }
}