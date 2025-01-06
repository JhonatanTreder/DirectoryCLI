using DirectoryCLI.Interfaces;
using System.Diagnostics;

namespace DirectoryCLI.Handlers
{
    internal class CommandHelper : ICommandHelper
    {
        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
        public string FormatBytes(long bytes)
        {
            if (bytes >= 0x1000000000000000) { return ((double)(bytes >> 50) / 1024).ToString("0.### EB"); }
            if (bytes >= 0x4000000000000) { return ((double)(bytes >> 40) / 1024).ToString("0.### PB"); }
            if (bytes >= 0x10000000000) { return ((double)(bytes >> 30) / 1024).ToString("0.### TB"); }
            if (bytes >= 0x40000000) { return ((double)(bytes >> 20) / 1024).ToString("0.### GB"); }
            if (bytes >= 0x100000) { return ((double)(bytes >> 10) / 1024).ToString("0.### MB"); }
            if (bytes >= 0x400) { return ((double)bytes / 1024).ToString("0.###") + " KB"; }

            return bytes.ToString("0 Bytes");
        }

        public string AddCommand(string[] arguments)
        {
            if (Directory.Exists(arguments[0]) && arguments.Length != 1)
            {
                return arguments[1];
            }

            else if (!Directory.Exists(arguments[0]) && arguments.Length >= 3)
            {
                return arguments[1];
            }

            else
            {
                return arguments[0];
            }
        }

        public void ExecuteProcess(string commandString)
        {
            Process.Start(new ProcessStartInfo(commandString) { UseShellExecute = true });
        }

        public string AddParameters(string[] arguments)
        {
            string commandString = "";

            commandString += string.Join(" ", arguments);

            return commandString.ToLower();
        }

        public string[] RemoveNullOrEmpty(string[] arguments)
        {
            if (arguments.Length == 0 || string.IsNullOrEmpty(arguments[0]))
            {
                return arguments;
            }

            List<string> newArgumentArray = new List<string>();

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!string.IsNullOrEmpty(arguments[i]))
                {
                    newArgumentArray.Add(arguments[i]);
                }
            }

            return newArgumentArray.ToArray();
        }
    }
}
