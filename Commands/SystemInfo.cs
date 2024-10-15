using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using Console = Colorful.Console;
using DirectoryCLI.CommandStyles;

namespace DirectoryCLI.Commands
{
    internal class SystemInfo : CommandsConfig
    {

        public static void Execute ()
        {
            Console.WriteLine("Informações do sistema: ");
            Console.WriteLine();

            //Nome do computador
            Console.Write("Nome do dispositivo: ");

            colors.DarkGray();
            Console.WriteLine(DeviceName());
            Colors.WhiteText();
            //-----------------------------------------------

            //Versão
            Console.Write("Versão: ");

            colors.DarkGray();
            Console.WriteLine(MachineVersion());
            Colors.WhiteText();
            //-----------------------------------------------

            //Processador
            Console.Write("Processador: ");

            colors.DarkGray();
            Console.WriteLine(ProcessorName());
            Colors.WhiteText();
            //-----------------------------------------------

            //Memória RAM
            Console.Write("RAM instalada: ");

            colors.DarkGray();
            Console.WriteLine(TotalRAM());
            Colors.WhiteText();
            //-----------------------------------------------

            //Placa de vídeo
            Console.Write("Placa de vídeo: ");

            colors.DarkGray();
            Console.WriteLine(GraphicCard());
            Colors.WhiteText();
            //-----------------------------------------------

            //Armazenamento
            Drive();

            Colors.WhiteText();
            //-----------------------------------------------

            //Tipo do sistema
            Console.Write("Tipo do sistema: ");

            colors.DarkGray();
            Console.WriteLine($"Sistema operacional de {SystemType()}, {ProcessorType()}");
            Colors.WhiteText();
            //-----------------------------------------------

            Console.WriteLine();
        }
        public static string DeviceName()
        {
            using (var query = new ManagementObjectSearcher("SELECT CSName FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject desktop in result.Cast<ManagementObject>())
                {
                    return desktop["CSName"].ToString();
                }
            }

            return "Nome do dispositivo não encontrado!";
        }

        public static string ProcessorName()
        {
            using (var query = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject processor in result.Cast<ManagementObject>())
                {
                    return processor["Name"].ToString();
                }
            }

            return "Nome do processador não encontrado!";
        }

        public static string TotalRAM()
        {
            using (var query = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject memory in result.Cast<ManagementObject>())
                {
                    if (memory["Capacity"] != null)
                    {
                        ulong capacityInBytes = (ulong)memory["Capacity"];
                        double capacityInGB = capacityInBytes / (1024.0 * 1024.0 * 1024.0);

                        return $"{capacityInGB:F2} GB";
                    }
                }
            }

            return "Memory information not found";
        }

        public static string SystemType()
        {
            using (var query = new ManagementObjectSearcher("SELECT OSArchitecture FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject type in result.Cast<ManagementObject>())
                {
                    if (type["OSArchitecture"] != null)
                    {
                        return type["OSArchitecture"].ToString();
                    }
                }
            }

            return "Tipo não encontrado";
        }

        public static string ProcessorType()
        {
            switch (RuntimeInformation.OSArchitecture)
            {
                case Architecture.X86:

                    return "processador baseado em x86";

                case Architecture.Arm64:

                    return "processador baseado em Arm64";

                case Architecture.Arm:

                    return "processador baseado em Arm";

                case Architecture.X64:

                    return "processador baseado em x64";

                default:

                    return "arquitetura do processador indefinida";
            }

        }

        public static string MachineVersion()
        {
            string productName = "";
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("ProductName");
                        if (value != null)
                        {
                            productName = value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar o registro: " + ex.Message);
            }
            return productName;
        }

        public static string GraphicCard()
        {
            using (var query = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject gCard in result.Cast<ManagementObject>())
                {
                    if (gCard["Name"] != null)
                    {
                        return gCard["Name"].ToString();
                    }
                }
            }

            return "Placa de vídeo não identificada";
        }

        public static void Drive()
        {
            Colors colors = new Colors();
            DriveInfo[] drives = DriveInfo.GetDrives();

            Colors.WhiteText();
            Console.WriteLine("Armazenamento do sistema: ");
            Console.WriteLine();

            Array.ForEach(drives, (drive) =>
            {
                Colors.LightBlue();
                Console.WriteLine($"Drive {drive.Name}");

                if (drive.IsReady)
                {
                    Colors.WhiteText();
                    Console.Write("Espaço disponível livre: ");

                    colors.DarkGray();
                    Console.WriteLine(FormatBytes(drive.AvailableFreeSpace));

                    Colors.WhiteText();
                    Console.Write("Espaço total Livre: ");

                    colors.DarkGray();
                    Console.WriteLine($"Espaço total Livre: {FormatBytes(drive.TotalFreeSpace)}");

                    Colors.WhiteText();
                    Console.Write("Espaço Total: ");

                    colors.DarkGray();
                    Console.WriteLine($"Espaço Total: {FormatBytes(drive.TotalSize)}");

                    Colors.LightBlue();
                    Console.WriteLine("--------------------------------------------------------");
                }

                else
                {
                    colors.DarkRed();
                    Console.WriteLine($"Drive {drive.Name} não esta pronto.");
                }
                Console.WriteLine();
            });
        }

        private static string FormatBytes(long bytes)
        {
            if (bytes >= 0x1000000000000000) { return ((double)(bytes >> 50) / 1024).ToString("0.### EB"); }
            if (bytes >= 0x4000000000000) { return ((double)(bytes >> 40) / 1024).ToString("0.### PB"); }
            if (bytes >= 0x10000000000) { return ((double)(bytes >> 30) / 1024).ToString("0.### TB"); }
            if (bytes >= 0x40000000) { return ((double)(bytes >> 20) / 1024).ToString("0.### GB"); }
            if (bytes >= 0x100000) { return ((double)(bytes >> 10) / 1024).ToString("0.### MB"); }
            if (bytes >= 0x400) { return ((double)(bytes) / 1024).ToString("0.###") + " KB"; }

            return bytes.ToString("0 Bytes");
        }
    }
}
