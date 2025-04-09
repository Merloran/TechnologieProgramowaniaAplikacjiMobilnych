using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

internal class Player(string name, float x, float y, float speed) : IPlayer
{
    public string Name { get; set; } = name;
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    public float Speed { get; set; } = speed;
}