namespace Data;

public interface IInput
{
    public string Direction { get; }
    public static IInput Create(string direction)
    {
        return new Input(direction);
    }
}