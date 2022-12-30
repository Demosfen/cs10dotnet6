using Packt;
using Xunit;

namespace PrimeFactorsUnitTests;

public class PrimeFactorsUnitTests
{
    [Fact]
    public void Zero()
    {
        int a = 0;
        string expected = "0";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void One()
    {
        int a = 1;
        string expected = "1";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Four()
    {
        int a = 4;
        string expected = "2x2";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Seven()
    {
        int a = 7;
        string expected = "7";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Ten()
    {
        int a = 10;
        string expected = "2x5";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Nine()
    {
        int a = 9;
        string expected = "3x3";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void BigIntOne()
    {
        int a = 65345;
        string expected = "5x7x1867";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void BigIntTwo()
    {
        int a = 653459;
        string expected = "83x7873";
        PrimeFactors calc = new();
        string actual = calc.PrimeNumber(a);
        Assert.Equal(expected, actual);
    }
}