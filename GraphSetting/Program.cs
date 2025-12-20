


using System.Diagnostics;
using GraphSetting.Matrix;

using GraphSetting.Loaders;
using GraphSetting.PlacementOptimizerBnb;


const string fileName = "allegro_111.NET";

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

// конекты
List<(string, string)> connects = compManager.ElementsConnections;


// Границы и ветви
PlacementOptimizerBnb placementOptimizer = new PlacementOptimizerBnb(
    matrixD:     D,
    matrixR:     R,
    comManager:  compManager
);

// foreach (var p in D.GetAllPositions())
//     Console.Write($"{p} ");
// Console.WriteLine();


Console.WriteLine("R");
placementOptimizer.RVector().BufferPrint();

Console.WriteLine("D");
placementOptimizer.DVector().BufferPrint();
Console.WriteLine();

// Console.WriteLine("RxD");
// Console.WriteLine($"{placementOptimizer.RVector() * placementOptimizer.DVector()}");

// Console.WriteLine("Расчет для P1");
// placementOptimizer.GetVectorByPosition("P1").BufferPrint();
//
// Console.WriteLine("Расчет для VD1");
// placementOptimizer.GetVectorByTranzition("VD1").BufferPrint();

// пример данных ("P1", "VD4")

List<(string, string)> Placement = new List<(string, string)>();
List<string> Positions = new List<string>();

for (int i = 1; i <= tranzistors.Count; i++) 
    Positions.Add($"P{i}");

// sorted колонки
List<string> sortedTranzistors = R.ColWeights
                                    .OrderBy(i => i.Item2)
                                    .Select(i => i.Item1)
                                    .ToList();

string firstTrnz = sortedTranzistors[0];

// Step I
List<(string, double)> tempPosition = new List<(string, double)>();

foreach (var position in Positions) {
    
        // TODO: есть проблема с размерностью подложки и транзисторов
        Vector d1 = placementOptimizer.GetVectorByPosition(position);
        Vector r1 = placementOptimizer.GetVectorByTranzition(firstTrnz);

        Vector __r = placementOptimizer.GetVectorWithoutTranzition(firstTrnz);
        Vector __d = placementOptimizer.GetVectorWithoutPosition(position);
        
        __r.BufferPrint();
        __d.BufferPrint();
        
        tempPosition.Add((position, d1 * r1 + __r * __d));
        
        Console.WriteLine($"{firstTrnz} & {position} r1 * d1");
        Console.WriteLine($"{d1 * r1}");
}

// Step II











