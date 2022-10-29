using System;

namespace Packt;
/// <summary>
/// Exercise 4.2. New class
/// </summary>
public class PrimeFactors
{
    /// <summary>
    /// Returns string with prime numbers of input int
    /// </summary>
    /// <param name="n">Input int number</param>
    /// <returns>String of prime numbers</returns>
    public static string PrimeNumber(int n)
    {
        string primeNumbers = string.Empty;
        int b = 2;
        int c;
        while (n % b == 0)
        {
            switch(n / b)
            {
                case 0:
                    primeNumbers += "0";
                    return primeNumbers;
                case 1:
                    primeNumbers += "2";
                    return primeNumbers;                  
                default: 
                    primeNumbers += "2x";
                    n /= b;
                    break;
            }
        }
        b = 3;
        c = (int)Math.Floor(Math.Sqrt(n)) + 1;
        while (b < c)
        {
            if (n % b == 0)
            {
                primeNumbers += b.ToString() + "x";
                n = n / b;
                c = (int)Math.Floor(Math.Sqrt(n)) + 1;
            }
            else b += 2;
        }
        primeNumbers += n.ToString();
        return primeNumbers;
    }
}
