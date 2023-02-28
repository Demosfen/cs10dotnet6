using System.Diagnostics;

using static System.Console;
using static System.Diagnostics.Process;

namespace Packt.Shared;

public static class Recorder
{
    private static Stopwatch timer = new();
    private static long bytesPhysicalBefore = 0;
    private static long bytesVirtualBefore = 0;

    public static void Start()
    {
// инициируем очистку памяти сборщиками мусора
// очищается память, на которую никто не ссылается
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
// храним текущие затраты физической и виртуальной памяти
        bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
        bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
        timer.Restart();
    }

    public static void Stop()
    {
        timer.Stop();
        var bytesPhysicalAfter =
            GetCurrentProcess().WorkingSet64;
        var bytesVirtualAfter =
            GetCurrentProcess().VirtualMemorySize64;
        WriteLine("{0:N0} physical bytes used.",
            bytesPhysicalAfter - bytesPhysicalBefore);
        WriteLine("{0:N0} virtual bytes used.",
            bytesVirtualAfter - bytesVirtualBefore);
        WriteLine("{0} time span ellapsed.", timer.Elapsed);
        WriteLine("{0:N0} total milliseconds elapsed.",
            timer.ElapsedMilliseconds);
    }
}