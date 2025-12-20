
namespace GraphSetting.Matrix;

public enum Order
{
    R,
    D,
    NoN
}

public class Vector
{
    public Order Order { get; private set; }
    public List<double> Buffer
    {
        get
        {
            switch (this.Order)
            {
                case Order.R:
                    _buffer.Sort();
                    break;
                case Order.D:
                    _buffer.Sort((a, b) => b.CompareTo(a));
                    break;
            }
            return _buffer;
        }
        set => _buffer = value;
    }

    private List<double> _buffer = new List<double>();
    
    public Vector(Order order)
    {
        this.Order = order;
    }
    
    public void BufferPrint()
    {
        foreach (var i in Buffer)
            Console.Write($"{i} ");
        Console.WriteLine();
    }
    
    public static double operator *(Vector a, Vector b)
    {
        var result = 0d;
        int length = 0;
        
        if (a.Order == b.Order)
        {
            length = a.Buffer.Count;
        }
        else if (a.Order > b.Order)
        {
            length = b.Buffer.Count;
        }
        else
        {
            length = a.Buffer.Count;
        }
        
        for (int i = 0; i < length; i++)
        {
            result += a.Buffer[i] * b.Buffer[i];
        }  
        
        return result;
    }
}