namespace GraphSetting.Matrix;

public class Coordinate
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public int DistanceTo(Coordinate other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;

        double dist = Math.Sqrt(dx * dx + dy * dy);
        return (int)Math.Ceiling(dist);
    }

}