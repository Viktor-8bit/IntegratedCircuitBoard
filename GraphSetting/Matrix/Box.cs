namespace GraphSetting.Matrix;

public class Box
{
    public int[,] BoxGrid { private set; get; }
    
    public Dictionary<string, Coordinate> BoxCoordinates { private set; get; }
    
    public Box(int sizeX, int sizeY)
    {
        // например
        // [ П1, П2 ]
        // [ П3, П4 ] 
        this.BoxGrid = new int[sizeX, sizeY];
        
        // забъем пример в BoxCoordinates TODO: автоматизировать
        BoxCoordinates = new Dictionary<string, Coordinate>();
        
        BoxCoordinates.Add("P1", new Coordinate(0, 0));
        BoxCoordinates.Add("P2", new Coordinate(0, 1));
        BoxCoordinates.Add("P3", new Coordinate(1, 0));
        BoxCoordinates.Add("P4", new Coordinate(1, 1));
        
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                // пересечение Пi и Пj
                if (j == i)
                {
                    SetWeight(j, i, 0);
                }
                else
                {
                    var cor1 = this.BoxCoordinates["P" + (j + 1).ToString()];
                    var cor2 = this.BoxCoordinates["P" + (i + 1).ToString()];
                    SetWeight(i, j, cor1.DistanceTo(cor2));
                }
                
            }
        }
        
        Console.WriteLine("Матрица D");
        MatrixSupport.PrintMatrix(this.BoxGrid);
    }
    
    private void SetWeight(int p1, int p2, int weight)
    {
        BoxGrid[p1, p2] = weight;
        BoxGrid[p2, p1] = weight;
    }
}