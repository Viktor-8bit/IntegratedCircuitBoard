
using GraphSetting.Loaders;

namespace GraphSetting.Matrix;

public class MatrixQ
{
    public UniqueElectronicComponents ComponentManager {get; private set;}
    
    public MatrixQ(UniqueElectronicComponents componentManager)
    {
        this.ComponentManager = componentManager;
        this.ComponentManager.MakeQMatrix();
    }

    public void PrintMatrix()
    {
            Console.Write("        ");
            foreach ( var network in ComponentManager.GetAllNetworks())
            {
                // заголовок
                Console.Write($"{network, 8}");
            }
            Console.Write("\n");

            foreach (var (component, id) in ComponentManager.GetAllUniqueElectronicComponents()
                         .Select((component, index) => (component, index)))
            {
                Console.Write($"{component,8}");
                foreach (var (network, i) in ComponentManager.GetAllNetworks()
                             .Select((network, index) => (network, index)))
                {
                    Console.Write($"{ComponentManager.MatrixQ[i, id],8}");
                }

                Console.Write("\n");
            }
    }
    
}