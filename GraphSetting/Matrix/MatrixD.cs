namespace GraphSetting.Matrix;

public class MatrixD
{
    
    public int[,] P;
    private MatrixR r;
    public Box box { private set; get; }
    
    public MatrixD(MatrixR r)
    {
        this.r = r;
        P = new int[r.R.GetLength(0), r.R.GetLength(1)];
        
        // сортируем e по уменьшению value
        var toList = 
            from q in this.r.ColWeights.ToList() 
            orderby q.Value 
            select q.Key;

        // foreach (var key in toList)
        // {
        //     Console.Write(key + " ");
        // }
        // Console.WriteLine();
        
        box = new Box(r.R.GetLength(0), r.R.GetLength(1));

    }
    
}