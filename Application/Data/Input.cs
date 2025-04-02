namespace Data;

internal enum InputEnum
{
    Up,
    Down,
    Left,
    Right,
}

internal class Input : IInput
{
    InputEnum _input { get; set; }

    public string Direction => _input.ToString().ToLower();

    public Input(string direction)
    {
        _input = direction switch
        {
            "up" => InputEnum.Up,
            "down" => InputEnum.Down,
            "left" => InputEnum.Left,
            "right" => InputEnum.Right,
            _ => throw new ArgumentException("Invalid direction")
        };
    }
    
    private Input(InputEnum input)
    {
        _input = input;
    }
    
}
