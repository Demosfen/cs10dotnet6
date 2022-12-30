using static System.Console;

WriteLine("ENter 0-255 value:");
string? num1 = ReadLine();
WriteLine("Print second value:");
string? num2 = ReadLine();

try {
    decimal number1 = decimal.Parse(num1);
    decimal number2 = decimal.Parse(num2);
    decimal number3 = number1/number2;
    WriteLine($"Division result: {number3}");

}
catch (FormatException) {
    WriteLine("Only digits!");
}
catch (OverflowException) {
    WriteLine("Sjmething went wrong, OverflowEx!");
}
catch (DivideByZeroException) {
    WriteLine("Looks like you divide by zero!");
}