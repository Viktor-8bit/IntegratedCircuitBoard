


using System.Diagnostics;
using GraphSetting.Matrix;

using GraphSetting.Loaders;



const string fileName = "allegro_1.NET";

Console.WriteLine($"Загружен файл {fileName}");
var file = new AllegroFormatLoader(@"C:\Users\Ivan\RiderProjects\GraphSetting\GraphSetting\CommutationFiles\" 
                                   + fileName);

await file.FileLoad();

// Иничиализация матрицы R 
var R = new MatrixR(file.ComponentsManager);

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

