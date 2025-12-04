
using GraphSetting.Loaders;
namespace GraphSetting.Matrix;



public class MatrixD
{
    public (string, int)[,] positionsD;
    private List<(string, (int, int))> PCalcud { get; set; } = new List<(string, (int, int))>();
    
    public MatrixR matrixR { get; private set; }
    
    public UniqueElectronicComponents ComonentsManager { get; private set; }
    
    // private static (int rows, int cols) GetClosestMatrixSize(int n)
    // {
    //     if (n <= 0)
    //         throw new ArgumentException("n должно быть положительным числом.");
    //
    //     int bestRows = 1;
    //     int bestCols = n;
    //     int bestDiff = int.MaxValue;
    //
    //     for (int rows = 1; rows <= n; rows++)
    //     {
    //         int cols = (int)Math.Ceiling((double)n / rows);
    //         if (Math.Abs(rows - cols) <= 1)
    //         {
    //             int diff = Math.Abs(rows * cols - n);
    //             if (diff < bestDiff)
    //             {
    //                 bestDiff = diff;
    //                 bestRows = rows;
    //                 bestCols = cols;
    //             }
    //         }
    //     }
    //
    //     return (bestRows, bestCols);
    // }
    
    private static (int rows, int cols) GetClosestMatrixSize(int n)
    {
        if (n <= 0)
            throw new ArgumentException("n должно быть положительным числом.");

        int bestRows = 1;
        int bestCols = n;
        int bestDiff = int.MaxValue;

        for (int rows = 1; rows <= n; rows++)
        {
            int cols = (int)Math.Ceiling((double)n / rows);

            // разница между сторонами
            int shapeDiff = Math.Abs(rows - cols);

            // лишние клетки
            int diff = rows * cols - n;

            // критерий выбора
            if (diff >= 0 && shapeDiff < bestDiff)
            {
                bestDiff = shapeDiff;
                bestRows = rows;
                bestCols = cols;
            }
        }

        return (bestRows, bestCols);
    }
    
    public MatrixD(UniqueElectronicComponents comonentsManager, MatrixR matrixR)
    {
        this.matrixR = matrixR;
        this.ComonentsManager = comonentsManager;
        var componentCount = MatrixD.GetClosestMatrixSize(this.ComonentsManager.CountUniqueElectronicComponents);
        this.positionsD = new (string, int)[componentCount.rows,componentCount.cols];
        for (int row = 0; row < componentCount.rows; row++)
        {
            for (int col = 0; col < componentCount.cols; col++)
                this.positionsD[row, col] = ("", 0);
        }
        this.PCalculate();
    }

    private void PCalculate()
    {
        Console.WriteLine("--Схема интегральной платы");
        // Засовываем P в PCalcud
        for (int row = 0; row < this.positionsD.GetLength(0); row++)
        {
            for (int col = 0; col < this.positionsD.GetLength(1); col++)
            {
                var P = "P" + (row * this.positionsD.GetLength(1) + col + 1).ToString();
                Console.Write($"{P, 4} ");
                PCalcud.Add((P, (row, col)));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /*public void MakeDMatrix()
    {
        foreach (var P1 in this.PCalcud)
        {
            foreach (var P2 in this.PCalcud)
            {
                // Console.Write($" {P1.Item1, 3}{P2.Item1, 3}");
                Console.Write($"{CalcDistance(P1.Item1, P2.Item1),4} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }*/

    private double CalcDistance(string P1, string P2)
    {
        (string, (int y, int x))? p1 = this.PCalcud.Where(p => p.Item1.Equals(P1))
            .Select(p => (p.Item1, p.Item2))
            .FirstOrDefault();
        (string, (int y, int x))? p2 = this.PCalcud.Where(p => p.Item1.Equals(P2))
            .Select(p => (p.Item1, p.Item2))
            .FirstOrDefault();

        if (p1 == null || p2 == null)
            throw new Exception("P1 или P2 не найдены");

        var dist = Math.Sqrt(Math.Pow(p1.Value.Item2.x - p2.Value.Item2.x, 2) + Math.Pow( p1.Value.Item2.y - p2.Value.Item2.y, 2));
        return Math.Round(dist, 2);
    }
    
    public void PrintMatrinx()
    {
        Console.Write("     ");
        foreach (var component in this.PCalcud)
        {
            // заголовок
            Console.Write($"{component.Item1, 5}");
        }
        Console.Write("\n");
        
        foreach (var P1 in this.PCalcud)
        {
            Console.Write($"{P1.Item1,5}");
            foreach (var P2 in this.PCalcud)
            {
                // Console.Write($" {P1.Item1, 3}{P2.Item1, 3}");
                Console.Write($"{CalcDistance(P1.Item1, P2.Item1),5}");
            }
            Console.WriteLine();
        }
    }
    
}