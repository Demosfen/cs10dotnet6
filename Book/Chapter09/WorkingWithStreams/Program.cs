﻿using System.Xml;
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using System.IO.Compression; // BrotliStream, GZipStream, CompressionMode

static void WorkWithCompression()
{
    string fileExt = "gzip";
// compress the XML output
    string filePath = Combine(
        CurrentDirectory, $"streams.{fileExt}");
    FileStream file = File.Create(filePath);
    Stream compressor = new GZipStream(file, CompressionMode.Compress);
    using (compressor)
    {
        using (XmlWriter xml = XmlWriter.Create(compressor))
        {
            xml.WriteStartDocument();
            xml.WriteStartElement("callsigns");
            foreach (string item in Viper.Callsigns)
            {
                xml.WriteElementString("callsign", item);
            }
// the normal call to WriteEndElement is not necessary
// because when the XmlWriter disposes, it will
// automatically end any elements of any depth
        }
    } // also closes the underlying stream
// output all the contents of the compressed file
    WriteLine("{0} contains {1:N0} bytes.",
        filePath, new FileInfo(filePath).Length);
    WriteLine($"The compressed contents:");
    WriteLine(File.ReadAllText(filePath));
// read a compressed file
    WriteLine("Reading the compressed XML file:");
    file = File.Open(filePath, FileMode.Open);
    Stream decompressor = new GZipStream(file,
        CompressionMode.Decompress);
    using (decompressor)
    {
        using (XmlReader reader = XmlReader.Create(decompressor))
        {
            while (reader.Read()) // read the next XML node
            {
// check if we are on an element node named callsign
                if ((reader.NodeType == XmlNodeType.Element)
                    && (reader.Name == "callsign"))
                {
                    reader.Read(); // move to the text inside element
                    WriteLine($"{reader.Value}"); // read its value
                }
            }
        }
    }
}


static void WorkWithXml()
{
    FileStream? xmlFileStream = null;
    XmlWriter? xml = null;
    try
    {
// define a file to write to
        string xmlFile = Combine(CurrentDirectory, "streams.xml");
// create a file stream
        xmlFileStream = File.Create(xmlFile);
// wrap the file stream in an XML writer helper
// and automatically indent nested elements
        xml = XmlWriter.Create(xmlFileStream,
            new XmlWriterSettings { Indent = true });
// write the XML declaration
        xml.WriteStartDocument();
// write a root element
        xml.WriteStartElement("callsigns");
// enumerate the strings writing each one to the stream
        foreach (string item in Viper.Callsigns)
        {
            xml.WriteElementString("callsign", item);
        }
        // write the close root element
        xml.WriteEndElement();
// close helper and stream
        xml.Close();
        xmlFileStream.Close();
// output all the contents of the file
        WriteLine($"{0} contains {1:N0} bytes.",
            arg0: xmlFile,
            arg1: new FileInfo(xmlFile).Length);
        WriteLine(File.ReadAllText(xmlFile));
    }
    catch (Exception ex)
    {
// if the path doesn't exist the exception will be caught
        WriteLine($"{ex.GetType()} says {ex.Message}");
    }
    finally
    {
        if (xml != null)
        {
            xml.Dispose();
            WriteLine("The XML writer's unmanaged resources have been disposed.");
            if (xmlFileStream != null)
            {
                xmlFileStream.Dispose();
                WriteLine("The file stream's unmanaged resources have been disposed.");
            }
        }
    }
}

WorkWithXml();
WorkWithCompression();
//WorkWithText();

static void WorkWithText()
{
// define a file to write to
    string textFile = Combine(CurrentDirectory, "streams.txt");
// create a text file and return a helper writer
    StreamWriter text = File.CreateText(textFile);
// enumerate the strings, writing each one
// to the stream on a separate line
    foreach (string item in Viper.Callsigns)
    {
        text.WriteLine(item);
    }
    text.Close(); // release resources
// output the contents of the file
    WriteLine("{0} contains {1:N0} bytes.",
        arg0: textFile,
        arg1: new FileInfo(textFile).Length);
    WriteLine(File.ReadAllText(textFile));
}

static class Viper
{
// define an array of Viper pilot call signs
    public static string[] Callsigns = new[]
    {
        "Husker", "Starbuck", "Apollo", "Boomer",
        "Bulldog", "Athena", "Helo", "Racetrack"
    };
}

