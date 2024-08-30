namespace DotnetMauiConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine(Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename));
        }
    }
}
