namespace DirectoryCLI.Interfaces
{
    internal interface ICommandHelper
    {
        string Truncate(string value, int maxLength);
        string AddCommand(string[] arguments);
        string FormatBytes(long bytes);
        string[] RemoveNullOrEmpty(string[] arguments);
        void RunCommand(string commandString);
    }
}
