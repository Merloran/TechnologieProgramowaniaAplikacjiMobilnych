using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

public interface IPlayer : INotifyPropertyChanged
{
    IInput? _currentInput { get; }
    void Move(IInput direction);
    float Radius { get; }
    float X { get; }
    float Y { get; }
    
    string Name { get; }
    
    IVector2 Position { get; }
    
    public static IPlayer Create(string name, IVector2 position, float speed)
    {
        return new Player(name, position, speed);
    }
}