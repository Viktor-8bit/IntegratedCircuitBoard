


using GraphSetting.Matrix;

using GraphSetting.Loaders;

var file = new AllegroFormatLoader(@"C:\Users\amomomogugus\RiderProjects\IntegratedCircuitBoard\GraphSetting\CommutationFiles\allegro_1.NET");

await file.FileLoad();

// Иничиализация матрицы R 
var R = new MatrixR(4, 4);



// добавляем все связи TODO: автоматизировать
// R.SetWeight("e1", "e2", 2);
// R.SetWeight("e1", "e3", 1);
// R.SetWeight("e1", "e4", 3);
//
// R.SetWeight("e2", "e3", 4);
// R.SetWeight("e2", "e4", 2);
//
// R.SetWeight("e3", "e4", 1);

// вычисляем веса колонок
R.ComputingColWeights();

Console.WriteLine("Матрица R");
R.PrintMatrix();

// проверяем что колонки посчитались верно
var Rkeys = "";
var Rvalues = "";

foreach (var r in R.ColWeights.Keys)
{
    Rkeys += r + " ";
    Rvalues += R.ColWeights[r] + "  ";
}

// пока не трогать 
// var D = new MatrixD(R);


