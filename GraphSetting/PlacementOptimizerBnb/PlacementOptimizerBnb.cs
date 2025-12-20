
using GraphSetting.Loaders;
using GraphSetting.Matrix;
namespace GraphSetting.PlacementOptimizerBnb;

public class PlacementOptimizerBnb
{
    public MatrixR MatrixR { get; private set; }
    public MatrixD MatrixD { get; private set; }
    public UniqueElectronicComponents ComManager { get; private set; }

    public PlacementOptimizerBnb(MatrixR matrixR, MatrixD matrixD, UniqueElectronicComponents comManager)
    {
        this.MatrixR = matrixR;
        this.MatrixD = matrixD;
        this.ComManager = comManager;
    }

    public Vector RVector()
        => MatrixR.GetVectorR();

    public Vector DVector()
        => MatrixD.GetVectorD();

    public Vector GetVectorByPosition(string position)
        => MatrixD.GetVectorByPosition(position);

    public Vector GetVectorByTranzition(string trnz)
        => MatrixR.GetVectorByTranzition(trnz);

    public Vector GetVectorWithoutPosition(string position)
        => throw new NotImplementedException();
    
    public Vector GetVectorWithoutTranzition(string trnz)
        => MatrixR.GetVectorWithoutTranzition(trnz); 
}