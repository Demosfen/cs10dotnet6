using static System.Console;

await foreach (int number in GetNumbersAsync())
{
    WriteLine($"Number: {number}");
}

static async IAsyncEnumerable<int> GetNumbersAsync()
{
    Random r = new();
// имитация работы
    await Task.Delay(r.Next(1500, 3000));
    yield return r.Next(0, 1001);
    await Task.Delay(r.Next(1500, 3000));
    yield return r.Next(0, 1001);
    await Task.Delay(r.Next(1500, 3000));
    yield return r.Next(0, 1001);
}
