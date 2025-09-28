namespace GraphSetting.Matrix;

public class MatrixR
{
    
    public int[,] R { private set; get; }
    
    
    
    public Dictionary<string, int> ColWeights { private set; get; }
    
    public MatrixR(int sizeX, int sizeY)
    {
        R = new int[sizeX, sizeY];
        
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                R[i, j] = 0;
            }
        }
    }

    public void ComputingColWeights() => ColWeights = MatrixSupport.ComputeColWeights(R, "e");
    public void PrintMatrix() => MatrixSupport.PrintMatrix(R);
    
    public void SetWeight(string e1, string e2, int weight)
    {
        int parseE1 = int.Parse(e1.Replace("e", "")) - 1;
        int parseE2 = int.Parse(e2.Replace("e", "")) - 1;
        
        R[parseE1, parseE2] = weight;
        R[parseE2, parseE1] = weight;
    }
}