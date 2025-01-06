namespace DirectoryCLI.Interfaces
{
    internal interface IDirectoryHandler
    {
        void ScanSize(string directory);
        void Open(string[] arguments);
        void Rename(string[] arguments);
        void ZipItem(string[] arguments);
        void MoveItem(string[] arguments);
        void ListItems(string[] arguments);
        void ExtractZipFile(string[] arguments);
    }
}
