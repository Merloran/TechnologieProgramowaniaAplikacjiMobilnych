using System.ComponentModel;

namespace Data
{
    internal class Vector2(float x, float y) : IVector2
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;

        public static Vector2 operator+(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator-(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator*(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator/(Vector2 a, float b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        public float Distance(IVector2 other)
        {
            return (float)Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Vector2 other)
            {
                return false;
            }
            const float epsilon = 1.0e-10f;

            return Math.Abs(other.X - this.X) <= epsilon && 
                   Math.Abs(other.X - this.X) <= epsilon;
        }
    }
}