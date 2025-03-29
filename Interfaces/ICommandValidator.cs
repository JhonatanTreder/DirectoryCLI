namespace DirectoryCLI.Interfaces
{
    internal interface ICommandValidator
    {
        bool ArgumentsValidation(string[] arguments, string command);
        bool IsValidZipCommand(string[] arguments);
        bool IsValidListCommand(string[] arguments);
        bool IsValidOpenCommand(string parameter);
        bool IsValidExtractCommand(string[] arguments);
        bool IsValidRenameCommand(string[] arguments);
    }
}
