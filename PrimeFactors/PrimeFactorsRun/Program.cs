using Packt;
using static System.Console;

CallPrimeFactor();

static void CallPrimeFactor()
{
    ConsoleKeyInfo cki;
    try{
    do
    {
        WriteLine("Type integer number: ");
        string? input = ReadLine();
            int n = int.Parse(input);
            string result = PrimeFactors.PrimeNumber(n);
            WriteLine($"The prime numbers of {n} is {result} \n");
            WriteLine("Repeat? Press the Escape (Esc) key to quit: \n");
            cki = ReadKey();
            WriteLine("\n");
    } while (cki.Key != ConsoleKey.Escape);  
    }
    catch (NullReferenceException)
    {
        WriteLine("No empty lines!");
    }
    catch (OverflowException)
    {
        WriteLine("Your number is too big or small.");
    }
    catch (FormatException)
    {
        WriteLine("Integer only!!!");
    }     
        
    WriteLine(@"Bye!");
}
