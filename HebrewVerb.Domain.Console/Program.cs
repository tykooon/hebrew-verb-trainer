using System.Runtime.InteropServices;

internal partial class Program
{
    private static void Main(string[] args)
    {
        SetConsoleOutputCP(65001);
        SetConsoleCP(65001);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCP(uint wCodePageID);

        RandomGufGeneration();

        ShoreshComponents();
    
    }
}