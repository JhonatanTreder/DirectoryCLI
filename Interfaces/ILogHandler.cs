namespace DirectoryCLI.Interfaces
{
    internal interface ILogHandler
    {
        void UserAndMachineName();
        void LogCommand(string command);
        void LogError(Exception exception, bool log);
        void ShowLog(string command, bool log);
        void ShowResult(string title, HashSet<string> items);
    }
}
