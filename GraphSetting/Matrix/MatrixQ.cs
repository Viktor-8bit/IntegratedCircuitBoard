
using GraphSetting.Loaders;

namespace GraphSetting.Matrix;

public class MatrixQ
{
    public UniqueElectronicComponents ComponentManager {get; private set;}
    
    public MatrixQ(UniqueElectronicComponents componentManager)
    {
        this.ComponentManager = componentManager;
    }
}