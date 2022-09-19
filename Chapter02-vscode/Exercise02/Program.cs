using static System.Console;

string dashes = new string('-',84);
string commonFormat = "{0,-8} {1,-17} {2,26} {3,30}";
string header = string.Format(commonFormat, "Type", "Byte(s) of memory", "Min","Max");
const int marginLeft = 36;

WriteLine(dashes);
WriteLine(header);
WriteLine(dashes);

WriteLine($"sbyte {sizeof(sbyte), 4} {sbyte.MinValue, marginLeft} {sbyte.MaxValue, marginLeft}");
WriteLine($"byte  {sizeof(byte), 4} {byte.MinValue, marginLeft} {byte.MaxValue, marginLeft}");
WriteLine($"short {sizeof(short), 4} {short.MinValue, marginLeft} {short.MaxValue, marginLeft}");
WriteLine($"ushort {sizeof(ushort), 3} {ushort.MinValue, marginLeft} {ushort.MaxValue, marginLeft}");
WriteLine($"int {sizeof(int), 6} {int.MinValue, marginLeft} {int.MaxValue, marginLeft}");
WriteLine($"uint {sizeof(uint), 5} {uint.MinValue, marginLeft} {uint.MaxValue, marginLeft}");
WriteLine($"long {sizeof(long), 5} {long.MinValue, marginLeft} {long.MaxValue, marginLeft}");
WriteLine($"ulong {sizeof(ulong), 4} {ulong.MinValue, marginLeft} {ulong.MaxValue, marginLeft}");
WriteLine($"float {sizeof(float), 4} {float.MinValue, marginLeft} {float.MaxValue, marginLeft}");
WriteLine($"double {sizeof(double), 3} {double.MinValue, marginLeft} {double.MaxValue, marginLeft}");
WriteLine($"decimal {sizeof(decimal), 2} {decimal.MinValue, marginLeft} {decimal.MaxValue, marginLeft}");

WriteLine(dashes);