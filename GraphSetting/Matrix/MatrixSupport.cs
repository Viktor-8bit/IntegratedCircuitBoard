namespace GraphSetting.Matrix;


public class MatrixSupport
{
    public static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public static Dictionary<string, int> ComputeColWeights(int[,] matrix, string matrixType)
    {
        var dict = new Dictionary<string, int>();

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        
        // left -> right
        for (int i = 0; i < cols; i++)
        {
            // up -> down
            var temp = 0;
            for (int j = 0; j < rows; j++)
            {
                 temp += matrix[j, i];
            }
            dict.Add(matrixType + i.ToString(), temp);
        }
        
        return dict;
    }
}