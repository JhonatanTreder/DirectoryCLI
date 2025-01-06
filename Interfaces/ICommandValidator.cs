namespace DirectoryCLI.Interfaces
{
    internal interface ICommandValidator
    {
        void ArgumentsValidation(string[] arguments);
        bool IsValidZipCommand(string[] arguments);
        bool IsValidListCommand(string[] arguments);
        bool IsValidOpenCommand(string parameter);
        bool IsValidExtractCommand(string[] arguments);
        bool IsValidRenameCommand(string[] arguments);
    }
}
