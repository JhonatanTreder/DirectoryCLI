using DirectoryCLI.CommandStyles;
using DirectoryCLI.Interfaces;
using Spectre.Console;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace DirectoryCLI.Handlers
{
    [SupportedOSPlatform("Linux")]
    [SupportedOSPlatform("MacOS")]
    [SupportedOSPlatform("Windows")]
    internal class SystemHandler : ISystemHandler
    {
        //------------------------------------------------------------------------------------------------------------------------------------------
        // Comando 'open-site'.
        public async Task OpenSite(string domain)
        {
            string url = "https://www." + domain + ".com";

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Console.WriteLine("URL inválida.");
                Console.WriteLine();
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                    HttpResponseMessage response = await client.GetAsync(url);

                    Console.WriteLine($"Abrindo {domain}");
                    Console.WriteLine();

                    if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.OK)
                    {
                        OpenBrowser(url);
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao acessar o site: {response.StatusCode}");
                        Console.WriteLine();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao tentar acessar o site: {ex.Message}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar abrir o site: {ex.Message}");
                Console.WriteLine();
            }
        }

        // Método auxiliar para abrir o navegador
        private static void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                Console.WriteLine("Sistema operacional não suportado para abrir sites.");
                Console.WriteLine();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------------------------------------
        // Comando 'system-info'.
        public void SystemInfo()
        {
            var systemTable = new Table();
            systemTable.AddColumn("Informações do sistema:");

            systemTable.AddEmptyRow();
            systemTable.AddRow($"Nome do dispositivo:       {DeviceName()}");
            systemTable.AddRow($"Versão:                    {MachineVersion()}");
            systemTable.AddRow($"Processador:               {ProcessorName()}");
            systemTable.AddRow($"RAM instalada:             {TotalRAM()}");
            systemTable.AddRow($"Tipo do sistema:           Sistema operacional de {SystemType()}, {ProcessorType()}");
            systemTable.AddEmptyRow();

            AnsiConsole.Write(systemTable);
            Drive();  // Exibe as informações de armazenamento
        }
        //------------------------------------------------------------------------------------------------------------------------------------------

        // Métodos auxiliares para o comando 'system-info'.

        private static string DeviceName()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                Environment.MachineName : ExecuteShellCommand("hostname")?
                .Trim() ?? "Nome do dispositivo não encontrado!";
        }

        private static string ProcessorName()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                GetWindowsProcessorName() : ExecuteShellCommand("lscpu | grep 'Model name' | awk -F: '{print $2}'")?
                .Trim() ?? "Nome do processador não encontrado!";
        }

        private static string TotalRAM()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                GetWindowsTotalRAM() : ExecuteShellCommand("free -h | grep Mem | awk '{print $2}'")?
                .Trim() ?? "Memória não encontrada!";
        }

        private static string SystemType() => RuntimeInformation.OSArchitecture.ToString();

        private static string ProcessorType()
        {
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X86 => "processador baseado em x86",
                Architecture.Arm => "processador baseado em Arm",
                Architecture.X64 => "processador baseado em x64",
                _ => "arquitetura do processador indefinida"
            };
        }

        private static string MachineVersion()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsVersion();
            }

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return ExecuteShellCommand("lsb_release -d | awk -F: '{print $2}'")?.Trim() ?? "Versão não encontrada!";
            }

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return ExecuteShellCommand("sw_vers -productVersion")?.Trim() ?? "Versão não encontrada!";
            }
            return "Versão não encontrada!";
        }

        private static void Drive()
        {
            ICommandHelper commandHelper = new CommandHelper();
            DriveInfo[] drives = DriveInfo.GetDrives();

            var driveTable = new Table();
            driveTable.AddColumn("Informações de armazenamento:");

            foreach (var drive in drives)
            {
                if (drive.IsReady)
                {
                    driveTable.AddEmptyRow();
                    driveTable.AddRow($"Drive: {drive.Name}");
                    driveTable.AddRow($"Espaço disponível livre: {commandHelper.FormatBytes(drive.AvailableFreeSpace)}");
                    driveTable.AddRow($"Espaço Total livre: {commandHelper.FormatBytes(drive.TotalFreeSpace)}");
                    driveTable.AddRow($"Espaço total: {commandHelper.FormatBytes(drive.TotalSize)}");
                }
            }
            AnsiConsole.Write(driveTable);
        }

        // Métodos auxiliares específicos do Windows
        private static string GetWindowsProcessorName()
        {
            using var query = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
            foreach (ManagementObject processor in query.Get())
            {
                return processor["Name"]?.ToString() ?? "Nome do processador não encontrado!";
            }
            return "Nome do processador não encontrado!";
        }

        private static string GetWindowsTotalRAM()
        {
            using var query = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");
            ulong capacityInBytes = 0;
            foreach (ManagementObject memory in query.Get())
            {
                capacityInBytes += (ulong)memory["Capacity"];
            }
            return $"{capacityInBytes / (1024.0 * 1024.0 * 1024.0):F2} GB";
        }

        private static string GetWindowsVersion()
        {
            try
            {
                using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                return key?.GetValue("ProductName")?.ToString() ?? "Versão não encontrada!";
            }
            catch (Exception ex)
            {
                return $"Erro ao acessar o registro: {ex.Message}";
            }
        }

        // Comando Shell auxiliar para Linux/macOS
        private static string? ExecuteShellCommand(string command)
        {
            try
            {
                var processInfo = new ProcessStartInfo("bash", $"-c \"{command}\"")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processInfo);
                return process?.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar comando shell: {ex.Message}");
                return null;
            }
        }

        // ExecCommand para rodar comandos de CLI
        public static void ExecCommand(string[] arguments)
        {
            ICommandHelper commandHelper = new CommandHelper();

            string commandString = string.Join(' ', arguments);
            commandHelper.RunCommand(commandString);
        }
    }
}
