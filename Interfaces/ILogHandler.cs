namespace DirectoryCLI.Interfaces
{
    internal interface ILogHandler
    {
        void UserAndMachineName();
        void TemplateCommandLog();
        void LogCommand(string command);
        void LogError(Exception exception, bool log);
        void ShowLog(bool log, string command);
        void ShowResult(string title, HashSet<string> items);
    }
}
