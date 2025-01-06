namespace DirectoryCLI.Interfaces
{
    internal interface IFolderHandler
    {
        void Create(string[] arguments);
        void Delete(string[] arguments);
        void DeleteSubdirectories(string directory);
    }
}
