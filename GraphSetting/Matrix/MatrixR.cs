namespace GraphSetting.Matrix;

using GraphSetting.Loaders;

public class MatrixR
{
    
    public UniqueElectronicComponents ComponentsManager { get; private set; }
    
    public List<(string, int)> ColWeights { get; private set; }
    
    public MatrixR(UniqueElectronicComponents componentsManager)
    {
        this.ComponentsManager = componentsManager;
        this.ComponentsManager.MakeRMatrix();
    }

    public void PrintMatrix()
    {
        Console.Write("    ");
        foreach ( var component in ComponentsManager.GetAllUniqueElectronicComponents())
        {
            // заголовок
            Console.Write($"{component, 4}");
        }
        Console.Write("\n");

        foreach (var (component, id) in ComponentsManager
                     .GetAllUniqueElectronicComponents()
                     .Select((component, index) => (component, index)))
        {
            Console.Write($"{component,4}");
            for (int i = 0; i < ComponentsManager.CountUniqueElectronicComponents; i++)
            {
                Console.Write($"{ComponentsManager.MatrixR[id, i],4}");
            }

            Console.Write("\n");
        }
    }

    public void ComputinColWeights()
    {
        List<(string, int)> weights = new List<(string, int)>();
        foreach (var (component, id) in ComponentsManager
                     .GetAllUniqueElectronicComponents()
                     .Select((component, index) => (component, index))) {
            int tmpWeight = 0;
            for (int i = 0; i < ComponentsManager.CountUniqueElectronicComponents; i++)
            {
                
                tmpWeight += this.ComponentsManager.MatrixR[i, id];
            }
            weights.Add((component, tmpWeight));
        }
        this.ColWeights = weights;
    }
}