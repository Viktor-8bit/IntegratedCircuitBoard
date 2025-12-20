

using GraphSetting.Matrix;
using GraphSetting.Loaders;
using GraphSetting.PlacementOptimizerBnb;


const string fileName = "allegro_2.NET";

Console.WriteLine($"Загружен файл {fileName}");
var file = new AllegroFormatLoader(@"/Users/ivan/RiderProjects/IntegratedCircuitBoard/GraphSetting/CommutationFiles/" + fileName);
await file.FileLoad();

UniqueElectronicComponents compManager = file.ComponentsManager;

// Иничиализация матрицы R 
var R = new MatrixR(compManager);

// матрица R
Console.WriteLine("-Матрица R");
R.PrintMatrix();

// считаем веса колонок
R.ComputinColWeights();

/*foreach (var weight in R.ColWeights.OrderBy(weight => weight.Item2).ToList())
{
    Console.Write(weight);
}
Console.WriteLine();*/

Console.WriteLine("-Матрица Q");
// матрица Q
var Q = new MatrixQ(file.ComponentsManager);

Q.PrintMatrix();

Console.WriteLine("-Матрица D");

// матрица D
var D = new MatrixD(file.ComponentsManager);

D.PrintMatrinx();

// список транзисторов
List<string> tranzistors = compManager.GetAllUniqueElectronicComponents();

// foreach (var tranzistor in tranzistors)
//     Console.Write($"{tranzistor} ");
// Console.WriteLine();

// Границы и ветви

PlacementOptimizerBnb placementOptimizer = new PlacementOptimizerBnb(
    matrixD:     D,
    matrixR:     R,
    comManager:  compManager
);

// foreach (var p in D.GetAllPositions())
//     Console.Write($"{p} ");
// Console.WriteLine();


// Console.WriteLine("\nR");
// placementOptimizer.RVector().BufferPrint();
//
// Console.WriteLine("D");
// placementOptimizer.DVector().BufferPrint();
// Console.WriteLine();

// Console.WriteLine("RxD");
// Console.WriteLine($"{placementOptimizer.RVector() * placementOptimizer.DVector()}");

// Console.WriteLine("Расчет для P1");
// placementOptimizer.GetVectorByPosition("P1").BufferPrint();
//
// Console.WriteLine("Расчет для VD1");
// placementOptimizer.GetVectorByTranzition("VD1").BufferPrint();

// пример данных ("P1", "VD4")


List<(string, string)> PlacementToTrnz = new List<(string, string)>();
List<string> Positions = new List<string>();

for (int i = 1; i <= tranzistors.Count; i++) 
    Positions.Add($"P{i}");

// sorted колонки
List<string> sortedTranzistors = R.ColWeights
                                    .OrderBy(i => i.Item2)
                                    .Select(i => i.Item1)
                                    .ToList();

while (sortedTranzistors.Count > 0)
{
    
    string firstTrnz = sortedTranzistors[0];
    List<(string, double)> tempPosition = new List<(string, double)>();

    foreach (var position in Positions)
    {
        double tempResult = 0;
        
        // TODO: есть проблема с размерностью подложки и транзисторов
        Vector d1 = placementOptimizer.GetVectorByPosition(position);
        Vector r1 = placementOptimizer.GetVectorByTranzition(firstTrnz);

        Vector __r = placementOptimizer.GetVectorWithoutTranzition(firstTrnz);
        Vector __d = placementOptimizer.GetVectorWithoutPosition(position);

        tempResult = (d1 * r1) +(__r * __d);
        
        // __r.BufferPrint();
        // __d.BufferPrint();
        //
        // r1.BufferPrint();
        // d1.BufferPrint();
        
        tempPosition.Add((position, tempResult));
    }
    
    
    sortedTranzistors.RemoveAt(0);

    string positionToPlace = tempPosition.OrderBy(i => i.Item2).First().Item1;
    Positions.RemoveAt(Positions.IndexOf(positionToPlace));
    
    PlacementToTrnz.Add((
            positionToPlace,
            firstTrnz
        )
    );
}


foreach (var ptz in PlacementToTrnz)
{
    Console.Write($"({ptz.Item1} {ptz.Item2}) ");
}










