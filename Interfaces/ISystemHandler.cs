namespace DirectoryCLI.Interfaces
{
    internal interface ISystemHandler
    {
        void SystemInfo();
        Task OpenSite(string domain);
    }
}
