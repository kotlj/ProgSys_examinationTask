using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessManager
{
    class ProcessMgr
    {
        public static async Task MainDisplay()
        {
            while (true)
            {
                Thread.Sleep(2000);
                Console.Clear();
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    Console.WriteLine($"PID: {process.Id}\t\tName: {process.ProcessName}");
                }
                Console.WriteLine("Options:\n1 - kill process by PID\n2 - Start file\n3 - Start cmd with command\n0 - exit");
                char choise = (Console.ReadLine())[0];
                if (choise == '1')
                {
                    await ProcessKillMenu();
                }
                else if (choise == '2')
                {
                    await FileStartMenu();
                }
                else if (choise == '3')
                {
                    await CommandMenu();
                }
                else if (choise == '0')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Invalid option");
                }
            }
        }

        private async static Task CommandMenu()
        {
            await Task.Delay(1);
            while (true)
            {
                Console.WriteLine("Enter command or leave blank for exit:\n");
                string command = Console.ReadLine();
                try
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = "cmd.exe";
                    info.Arguments = (command.First<char>() == '/' ? command : $"/c {command}");

                    Process process = Process.Start(info);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Wow, even here can be error... literaly no clue how you catch it: {e}");
                }
            }
        }

        private async static Task FileStartMenu()
        {
            await Task.Delay(1);
            while (true)
            {
                Console.WriteLine("Leave blank for exit\nEnter path to file:\n");
                string path = Console.ReadLine();
                if (File.Exists(path))
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = path;
                    try
                    {
                        Process proc = Process.Start(info);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error: {e}");
                    }
                }
                else if(path == "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("wrong path, file name, or programm dont have a permission to see that file");
                }
            }
        }

        private async static Task ProcessKillMenu()
        {
            await Task.Delay(1);
            while (true)
            {
                Console.WriteLine("Leave blank for exit\nEnter PID:\n");
                string check = Console.ReadLine();
                if (int.TryParse(check, out int PID))
                {
                    try
                    {
                        Process process = Process.GetProcessById(PID);
                        process.Kill();
                        Console.WriteLine("Process killed succesfuly");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An error occurred: {e}\nMaybe check PID and try aggain");
                    }
                }
                else if (check == "")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong PID format");
                }
            }
        }
    }
}

namespace ProgSys_ExaminationTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await ProcessManager.ProcessMgr.MainDisplay();
        }
    }
}
