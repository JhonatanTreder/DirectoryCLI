namespace DirectoryCLI.Interfaces
{
    internal interface IFileHandler
    {
        void Create(string[] arguments);
        void Delete(string[] arguments);
        void DeleteAllFiles(string[] arguments);
    }
}
