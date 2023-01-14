using static System.Console;
using static System.IO.Directory;
using static System.Environment;

static void OutputFileSystemInfo()
{
    WriteLine("{0,-33} {1}", 
        arg0: "Path.PathSeparator",
        arg1: System.IO.Path.PathSeparator);
    WriteLine("{0,-33} {1}", 
        arg0: "Path.DirectorySeparatorChar",
        arg1: System.IO.Path.DirectorySeparatorChar);
    WriteLine("{0,-33} {1}", 
        arg0: "Directory.GetCurrentDirectory()",
        arg1: GetCurrentDirectory());
    WriteLine("{0,-33} {1}", 
        arg0: "Environment.CurrentDirectory",
        arg1: CurrentDirectory);
    WriteLine("{0,-33} {1}", 
        arg0: "Environment.SystemDirectory",
        arg1: SystemDirectory);
    WriteLine("{0,-33} {1}", 
        arg0: "Path.GetTempPath()",
        arg1: System.IO.Path.GetTempPath());
    
    WriteLine("GetFolderPath(SpecialFolder");
    WriteLine("{0,-33} {1}", 
        arg0: " .System)",
        arg1: GetFolderPath(SpecialFolder.System));
    WriteLine("{0,-33} {1}", 
        arg0: " .ApplicationData)",
        arg1: GetFolderPath(SpecialFolder.ApplicationData));
    WriteLine("{0,-33} {1}", 
        arg0: " .MyDocuments)",
        arg1: GetFolderPath(SpecialFolder.MyDocuments));
    WriteLine("{0,-33} {1}", 
        arg0: " .Personal)",
        arg1: GetFolderPath(SpecialFolder.Personal));
}

//OutputFileSystemInfo();

static void WorkWithDrives()
{
    WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}",
        "NAME", "TYPE", "FORMAT", "SIZE (BYTES)", "FREE SPACE");
    foreach (DriveInfo drive in DriveInfo.GetDrives())
    {
        if (drive.IsReady)
        {
            WriteLine(
                "{0,-30} | {1,-10} | {2,-7} | {3,18:N0} | {4,18:N0}",
                drive.Name, drive.DriveType, drive.DriveFormat,
                drive.TotalSize, drive.AvailableFreeSpace);
        }
        else
        {
            WriteLine("{0,-30} | {1,-10}", drive.Name, drive.DriveType);
        }
    }
}

//WorkWithDrives();

static void WorkWithDirectories()
{ 
    // define a directory path for a new folder
    // starting in the user's folder
    string newFolder = System.IO.Path.Combine(GetFolderPath(SpecialFolder.Personal),
        "Code", "Chapter09", "NewFolder");
    
    WriteLine($"Working with: {newFolder}");
    
// check if it exists
    WriteLine($"Does it exist? {Exists(newFolder)}");
    
// create directory
    WriteLine("Creating it...");
    CreateDirectory(newFolder);
    WriteLine($"Does it exist? {Exists(newFolder)}");
    Write("Confirm the directory exists, and then press ENTER: ");
    ReadLine();
    
// delete directory
    WriteLine("Deleting it...");
    Delete(newFolder, recursive: true);
    WriteLine($"Does it exist? {Exists(newFolder)}");
}

//WorkWithDirectories();

static void WorkWithFiles()
{
// define a directory path to output files
// starting in the user's folder
    string dir = Path.Combine(
        GetFolderPath(SpecialFolder.Personal),
        "Code", "Chapter09", "OutputFiles");
    CreateDirectory(dir);
    
// define file paths
    string textFile = Path.Combine(dir, "Dummy.txt");
    string backupFile = Path.Combine(dir, "Dummy.bak");
    WriteLine($"Working with: {textFile}");
    
// check if a file exists
    WriteLine($"Does it exist? {File.Exists(textFile)}");
    
// create a new text file and write a line to it
    StreamWriter textWriter = File.CreateText(textFile);
    textWriter.WriteLine("Hello, C#!");
    textWriter.Close(); // close file and release resources
    WriteLine($"Does it exist? {File.Exists(textFile)}");
    
// copy the file, and overwrite if it already exists
    File.Copy(sourceFileName: textFile,
        destFileName: backupFile, overwrite: true);
    WriteLine(
        $"Does {backupFile} exist? {File.Exists(backupFile)}");
    Write("Confirm the files exist, and then press ENTER: ");
    ReadLine();
    
// delete file
    File.Delete(textFile);
    WriteLine($"Does it exist? {File.Exists(textFile)}");
    
// read from the text file backup
    WriteLine($"Reading contents of {backupFile}:");
    StreamReader textReader = File.OpenText(backupFile);
    WriteLine(textReader.ReadToEnd());
    textReader.Close();
    
    // Managing paths
    WriteLine($"Folder Name: {Path.GetDirectoryName(textFile)}");
    WriteLine($"File Name: {Path.GetFileName(textFile)}");
    WriteLine("File Name without Extension: {0}",
        Path.GetFileNameWithoutExtension(textFile));
    WriteLine($"File Extension: {Path.GetExtension(textFile)}");
    WriteLine($"Random File Name: {Path.GetRandomFileName()}");
    WriteLine($"Temporary File Name: {Path.GetTempFileName()}");
}

WorkWithFiles();