int x = 0;

var tomTask = PrintNameAsync("Tom");
//var bobTask = PrintNameAsync("Bob");
//var samTask = PrintNameAsync("Sam");

Console.WriteLine(x);

//await tomTask;
//await bobTask;
//await samTask;

// определение асинхронного метода
async Task PrintNameAsync(string name)
{
    await Task.Delay(3000);     // имитация продолжительной работы
    Console.WriteLine(name);
    x = 12;
}

Thread.Sleep(3000);
Console.WriteLine(x);

