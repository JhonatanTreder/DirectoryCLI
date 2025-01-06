using DirectoryCLI.CommandStyles;
using DirectoryCLI.Interfaces;
using Spectre.Console;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;
using System.Runtime.Versioning;

namespace DirectoryCLI.Handlers
{
    [SupportedOSPlatform("Linux")]
    [SupportedOSPlatform("MacOS")]
    [SupportedOSPlatform("Windows")]

    internal class SystemHandler : ISystemHandler
    {
        //------------------------------------------------------------------------------------------------------------------------------------------
        //OPEN-SITE
        public async Task OpenSite(string domain)
        {
            // Monta a URL a partir do domínio fornecido
            string url = "https://www." + domain + ".com";

            // Verifica se a URL é válida
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Console.WriteLine("URL inválida.");
                Console.WriteLine();
                return;
            }

            try
            {
                // Cria um HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Adiciona o cabeçalho User-Agent para simular um navegador
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");

                    // Faz uma requisição GET para verificar se o site está acessível
                    HttpResponseMessage response = await client.GetAsync(url);

                    Console.WriteLine($"Abrindo {domain}");
                    Console.WriteLine();

                    // Verifica se a resposta foi bem-sucedida
                    if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.OK)
                    {
                        // Abre o site no navegador
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
                            return;
                        }
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
        //------------------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------------------------------------
        //SYSTEM-INFO
        public void SystemInfo()
        {
            var systemTable = new Table();
            systemTable.AddColumn("  Informações do sistema:");

            systemTable.AddEmptyRow();
            systemTable.AddRow($" Nome do dispositivo:       {DeviceName()}");
            systemTable.AddRow($" Versão:                    {MachineVersion()}");
            systemTable.AddRow($" Processador:               {ProcessorName()}");
            systemTable.AddRow($" RAM instalada:             {TotalRAM()}");
            systemTable.AddRow($" Tipo do sistema:           Sistema operacional de {SystemType()}, {ProcessorType()}");
            systemTable.AddEmptyRow();

            AnsiConsole.Write(systemTable);
            Drive();  // Exibe as informações de armazenamento
        }

        private static string DeviceName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.MachineName;  // Nome do dispositivo no Windows
            }

            else
            {
                return ExecuteShellCommand("hostname")?.Trim() ?? "Nome do dispositivo não encontrado!";
            }
        }

        private static string ProcessorName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsProcessorName();  // Método específico do Windows
            }

            else
            {
                return ExecuteShellCommand("lscpu | grep 'Model name' | awk -F: '{print $2}'")?.Trim() ?? "Nome do processador não encontrado!";
            }
        }

        private static string TotalRAM()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsTotalRAM();  // Método específico do Windows
            }

            else
            {
                return ExecuteShellCommand("free -h | grep Mem | awk '{print $2}'")?.Trim() ?? "Memória não encontrada!";
            }
        }

        private static string SystemType()
        {
            return RuntimeInformation.OSArchitecture.ToString();
        }

        private static string ProcessorType()
        {
            switch (RuntimeInformation.OSArchitecture)
            {
                case Architecture.X86: return "processador baseado em x86";
                case Architecture.Arm: return "processador baseado em Arm";
                case Architecture.X64: return "processador baseado em x64";
                case Architecture.Wasm: return "processador baseado em Wasm";
                case Architecture.Armv6: return "processador baseado em Armv6";
                case Architecture.Arm64: return "processador baseado em Arm64";
                case Architecture.S390x: return "processador baseado em S390x";
                case Architecture.Ppc64le: return "processador baseado em Ppc64le";
                case Architecture.LoongArch64: return "processador baseado em LoongArch64";
                default: return "arquitetura do processador indefinida";
            }
        }

        private static string MachineVersion()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsVersion();  // Método específico do Windows
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
            driveTable.AddColumn("  Informações de armazenamento:");

            foreach (var drive in drives)
            {
                if (drive.IsReady)
                {
                    driveTable.AddEmptyRow();
                    driveTable.AddRow($" Drive: {drive.Name}");
                    driveTable.AddRow($" Espaço disponível livre: {commandHelper.FormatBytes(drive.AvailableFreeSpace)}");
                    driveTable.AddRow($" Espaço Total livre: {commandHelper.FormatBytes(drive.TotalFreeSpace)}");
                    driveTable.AddRow($" Espaço total: {commandHelper.FormatBytes(drive.TotalSize)}");
                }
            }

            AnsiConsole.Write(driveTable);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        // Métodos auxiliares específicos do Windows
        //------------------------------------------------------------------------------------------------------------------------------------------
        private static string GetWindowsProcessorName()
        {
            // Lógica original de WMI para Windows
            using (var query = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor"))
            {
                ManagementObjectCollection result = query.Get();
                foreach (ManagementObject processor in result.Cast<ManagementObject>())
                {
                    return processor["Name"]?.ToString() ?? "Nome do processador não encontrado!";
                }
            }
            return "Nome do processador não encontrado!";
        }

        private static string GetWindowsTotalRAM()
        {
            // Lógica original de WMI para Windows
            using (var query = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject memory in result.Cast<ManagementObject>())
                {
                    ulong capacityInBytes = (ulong)memory["Capacity"];
                    double capacityInGB = capacityInBytes / (1024.0 * 1024.0 * 1024.0);
                    return $"{capacityInGB:F2} GB";
                }

            }
            return "Memória não encontrada";
        }

        private static string GetWindowsVersion()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    return key?.GetValue("ProductName")?.ToString() ?? "Versão não encontrada!";
                }

            }

            catch (Exception ex)
            {
                return $"Erro ao acessar o registro: {ex.Message}";
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        // Comando Shell auxiliar para Linux/macOS
        //------------------------------------------------------------------------------------------------------------------------------------------
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

                using (var process = Process.Start(processInfo))
                {
                    if (process != null)
                    {

                        using (var reader = process.StandardOutput)
                        {
                            return reader.ReadToEnd();
                        }

                    }

                    else
                    {
                        Console.WriteLine("Erro ao iniciar o processo");
                        return null;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar comando shell: {ex.Message}");
                return null;
            }
        }
    }
}
