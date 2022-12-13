using CA221212;

Random rnd = new();
List<Game> games = new();
using StreamReader sr = new("..\\..\\..\\RES\\bestgames.csv");
while (!sr.EndOfStream) games.Add(new Game(sr.ReadLine()!));

//f01
Console.WriteLine($"f1: összesen {games.Count} játék szerepel a listán!");

//f02
var grp = games.GroupBy(g => g.ReleaseYear)
    .Where(x => x.Count() > 10)
    .OrderByDescending(x => x.Count())
    .ToDictionary(x => x.Key, x=> x.Count());
Console.WriteLine($"f2: ezekben az években került több, mint 10 cím a listára:");
foreach (var kvp in grp) Console.WriteLine($"\t{kvp.Key}: {kvp.Value}db");

//Console.WriteLine("vagy:");
//Dictionary<int, int> dic = new();
//foreach (var g in games)
//    if (!dic.ContainsKey(g.ReleaseYear)) dic.Add(g.ReleaseYear, 1);
//    else dic[g.ReleaseYear]++;
//List<(int ev, int db)> lisata = new();
//foreach (var kvp in dic)
//    if (kvp.Value > 10) lisata.Add((kvp.Key, kvp.Value));
//for (int i = 0; i < lisata.Count - 1; i++)
//    for (int j = i + 1; j < lisata.Count; j++)
//        if (lisata[i].db < lisata[j].db)
//            (lisata[i], lisata[j]) = (lisata[j], lisata[i]);
//foreach (var e in lisata) Console.WriteLine($"\t{e.ev}: {e.db}db");

Game[] shooters = games.Where(g => g.Genre == "First-person shooter").ToArray();
string rndShooter = shooters[rnd.Next(shooters.Length)].Title;

Console.WriteLine(
    $"f3: összesen {shooters.Length} FPS került fel a listára,\n\t" +
    $"például: {rndShooter}");

Console.ReadKey(true);