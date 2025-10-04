
using GraphSetting.Loaders;

namespace GraphSetting.Matrix;

public class MatrixQ
{
    public UniqueElectronicComponents ComponentManager {get; set;}


    public MatrixQ(UniqueElectronicComponents componentManager)
    {
        this.ComponentManager = componentManager;
    }
}