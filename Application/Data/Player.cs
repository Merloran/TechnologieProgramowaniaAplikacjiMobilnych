using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

internal class Player(string name, IVector2 position, float speed) : IPlayer
{
    public string Name { get; private set; } = name;
    public IVector2 Position { get; private set; } = position;
    public float Speed { get; private set; } = speed;

    public IInput? _currentInput { get; private set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public void HandleInput(Input? input)
    {
        _currentInput = input;
    }
    
    public void Move(IInput input)
    {
        switch (input.Direction)
        {
            case "up":
            {
                Position.Y -= Speed;
                break;
            }
            case "down":
            {
                Position.Y += Speed;
                break;
            }
            case "left":
            {
                Position.X -= Speed;
                break;
            }
            case "right":
            {
                Position.X += Speed;
                break;
            }
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position"));
    }

    public float Radius => 50.0f;

    public float X => Position.X - Radius / 2.0f;

    public float Y => Position.Y - Radius / 2.0f;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}