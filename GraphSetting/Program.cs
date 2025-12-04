


using System.Diagnostics;
using GraphSetting.Matrix;

using GraphSetting.Loaders;



const string fileName = "allegro_5.NET";

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
var D = new MatrixD(file.ComponentsManager, R);

D.PrintMatrinx();



// случайно раскидать 
    // список транзисторов
    List<(string, string)> tranzistorToP = new List<(string, string)>();
    var pcurrent = 1;
    foreach (var comp in compManager.GetAllUniqueElectronicComponents())
    {
        var toAdd = ("P" + pcurrent, comp);
        tranzistorToP.Add(toAdd);
        pcurrent++;
        Console.WriteLine(toAdd);
    }

    // конекты
    foreach (var conect in compManager.ElementsConnections)
    {
        Console.WriteLine(conect);
    }


    // сопоставление матрицы размещения и списка транзисторов
    


