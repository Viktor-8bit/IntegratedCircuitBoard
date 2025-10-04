


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


Console.WriteLine("Матрица R");
R.PrintMatrix();

Console.WriteLine("Веса колонок");

R.ComputinColWeights();

foreach (var (name, count) in R.ColWeights.Select(value => (value.Item1, value.Item2)))
{
    Console.Write($"{name, 4} - {count}, ");
}
Console.WriteLine();

Console.WriteLine("Нетворки");
foreach (var network in file.ComponentsManager.GetAllNetworks())
{
    Console.Write($"{network} ");
}

// матрица Q
var Q = new MatrixQ(file.ComponentsManager);

var D = new MatrixD();

