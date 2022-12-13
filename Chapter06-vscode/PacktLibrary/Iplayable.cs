using static System.Console;

namespace Packt.Shared;

public interface IPlayable
{
    void Play();
    void Pause();

    void Stop()
    {
        WriteLine("Default implemetnation of stop.");
    }
}