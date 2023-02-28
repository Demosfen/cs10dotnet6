using System.Diagnostics;

using static System.Console;

OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();

// WriteLine("Running methods synchronously on one thread.");
// MethodA();
// MethodB();
// MethodC();
// WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");

WriteLine("Running methods asynchronously on multiple threads.");
Task taskA = new(MethodA);
taskA.Start();
var taskB = Task.Factory.StartNew(MethodB);
var taskC = Task.Run(MethodC);
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");

static void OutputThreadInfo()
{
    Thread t = Thread.CurrentThread;
    WriteLine(
        "Thread Id: {0}, Priority: {1}, Background: {2}, Name: {3}",
        t.ManagedThreadId, t.Priority,
        t.IsBackground, t.Name ?? "null");
}

static void MethodA()
{
    WriteLine("Starting Method A...");
    OutputThreadInfo();
    Thread.Sleep(3000); // имитируем три секунды работы
    WriteLine("Finished Method A.");
}

static void MethodB()
{
    WriteLine("Starting Method B...");
    OutputThreadInfo();
    Thread.Sleep(2000); // имитируем две секунды работы
    WriteLine("Finished Method B.");
}

static void MethodC()
{
    WriteLine("Starting Method C...");
    OutputThreadInfo();
    Thread.Sleep(1000); // имитируем одну секунду работы
    WriteLine("Finished Method C.");
}