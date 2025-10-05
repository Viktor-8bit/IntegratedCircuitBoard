
using GraphSetting.Loaders;
namespace GraphSetting.Matrix;

public class MatrixD
{
    

    public UniqueElectronicComponents ComonentsManager { get; private set; }

    public MatrixD(UniqueElectronicComponents comonentsManager)
    {
        this.ComonentsManager = comonentsManager;
        
    }
    
}