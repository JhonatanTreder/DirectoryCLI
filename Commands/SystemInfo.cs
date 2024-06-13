using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class SystemInfo : CommandsConfig
    {
        public static string DeviceName()
        {
            using (var query = new ManagementObjectSearcher("SELECT CSName FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection result = query.Get();

                foreach (ManagementObject desktop in result)
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

                foreach (ManagementObject processor in result)
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

                foreach (ManagementObject memory in result)
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
    }
}
