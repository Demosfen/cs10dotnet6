using static System.Console;
using System.Diagnostics;

// write to a text file in the project folder
Trace.Listeners.Add(new TextWriterTraceListener(
File.CreateText(Path.Combine(Environment.GetFolderPath(
Environment.SpecialFolder.DesktopDirectory), "log.txt"))));
// text writer is buffered, so this option calls
// Flush() on all listeners after writing
Trace.AutoFlush = true;

Debug.WriteLine("Debug says: I'm watching!");
Trace.WriteLine("Trace says: I'm watching!");