namespace Data;

public interface IVector2
{
    public float X { get; set; }
    public float Y { get; set; }
    public static IVector2 Create(float x, float y)
    {
        return new Vector2(x, y);
    }
    
    public float Distance(IVector2 other);
    public bool Equals(object? obj);
    
    public static IVector2 operator+(IVector2 a, IVector2 b)
    {
        return IVector2.Create(a.X + b.X, a.Y + b.Y);
    }

    public static IVector2 operator-(IVector2 a, IVector2 b)
    {
        return IVector2.Create(a.X - b.X, a.Y - b.Y);
    }
    
    public static IVector2 operator*(IVector2 a, float b)
    {
        return IVector2.Create(a.X * b, a.Y * b);
    }
    
    public static IVector2 operator/(IVector2 a, float b)
    {
        return IVector2.Create(a.X / b, a.Y / b);
    }
}