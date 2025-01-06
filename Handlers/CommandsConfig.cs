using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DirectoryCLI.Handlers
{
    internal class CommandsConfig
    {

        protected static void DeleteInPosition(string[] items, int index)
        {
            ProcessStartInfo processInfo;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows (cmd)
                processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C del \"{items[index]}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // Linux ou macOS (bash)
                processInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"rm '{items[index]}'\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
            }

            else
            {
                throw new NotSupportedException("Sistema operacional não suportado.");
            }

            using var process = Process.Start(processInfo);
            if (process != null)
            {
                process.WaitForExit();
            }
        }
    }
}
