﻿using static System.Console;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

// write to a text file in the project folder
Trace.Listeners.Add(new TextWriterTraceListener(
File.CreateText(Path.Combine(Environment.GetFolderPath(
Environment.SpecialFolder.DesktopDirectory), "log.txt"))));
// text writer is buffered, so this option calls
// Flush() on all listeners after writing
Trace.AutoFlush = true;

Debug.WriteLine("Debug says: I'm watching!");
Trace.WriteLine("Trace says: I'm watching!");

ConfigurationBuilder builder = new();
builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();
TraceSwitch ts = new(
displayName: "PacktSwitch",
description: "This switch is set via a JSON config.");
configuration.GetSection("PacktSwitch").Bind(ts);
Trace.WriteLineIf(ts.TraceError, "Trace error");
Trace.WriteLineIf(ts.TraceWarning, "Trace warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace information");
Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");