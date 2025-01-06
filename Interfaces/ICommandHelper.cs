namespace DirectoryCLI.Interfaces
{
    internal interface ICommandHelper
    {
        string Truncate(string value, int maxLength);
        string AddCommand(string[] arguments);
        void ExecuteProcess(string commandString);
        string FormatBytes(long bytes);
        string AddParameters(string[] arguments);
        string[] RemoveNullOrEmpty(string[] arguments);
    }
}
