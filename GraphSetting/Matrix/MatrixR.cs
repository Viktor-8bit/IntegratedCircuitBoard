

using GraphSetting.PlacementOptimizerBnb;

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

    public Vector GetVectorR()
    {
        Vector r = new Vector(Order.R);
        
        int i = 0;
        int j = ComponentsManager.GetAllUniqueElectronicComponents().Count;

        while (i < j)
        {
            for (int k = i; k < j; k++)
            {
                if (k != i)
                {
                    var comp1 = ComponentsManager.GetAllUniqueElectronicComponents()[i];
                    var comp2 = ComponentsManager.GetAllUniqueElectronicComponents()[k];

                    var comp1Id = ComponentsManager.GetIDbyUniqueElectronicComponent(comp1);
                    var comp2Id = ComponentsManager.GetIDbyUniqueElectronicComponent(comp2);

                    r.Buffer.Add(ComponentsManager.MatrixR[comp1Id, comp2Id]);
                }
            }
            i++;
        }
        return r;
    }
    
    public Vector GetVectorByTranzition(string trnz)
    {
        var trnzID = ComponentsManager.GetIDbyUniqueElectronicComponent(trnz);
        var VbT = new  Vector(Order.R);
        for (int i = 0; i < ComponentsManager.GetAllUniqueElectronicComponents().Count; i++)
        {
            var otherTrnz = ComponentsManager.GetAllUniqueElectronicComponents()[i];
            if (!otherTrnz.Equals(trnz))
            {
                var tmpID = ComponentsManager.GetIDbyUniqueElectronicComponent(otherTrnz);
                VbT.Buffer.Add(ComponentsManager.MatrixR[tmpID, trnzID]);
            }
        }
        return VbT;
    }

    public Vector GetVectorWithoutTranzition(string trnz)
    {
        Vector r = new Vector(Order.R);
        int i = 0;
        int j = ComponentsManager.GetAllUniqueElectronicComponents().Count;
        while (i < j)
        {
            for (int k = i; k < j; k++)
            {
                if (k != i && (ComponentsManager.GetAllUniqueElectronicComponents()[i] != trnz && ComponentsManager.GetAllUniqueElectronicComponents()[k] != trnz))
                {
                    var comp1 = ComponentsManager.GetAllUniqueElectronicComponents()[i];
                    var comp2 = ComponentsManager.GetAllUniqueElectronicComponents()[k];

                    var comp1Id = ComponentsManager.GetIDbyUniqueElectronicComponent(comp1);
                    var comp2Id = ComponentsManager.GetIDbyUniqueElectronicComponent(comp2);

                    r.Buffer.Add(ComponentsManager.MatrixR[comp1Id, comp2Id]);
                }
            }
            i++;
        }
        return r;
    }
}