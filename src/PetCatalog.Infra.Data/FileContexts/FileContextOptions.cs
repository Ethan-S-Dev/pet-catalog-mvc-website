namespace PetCatalog.Infra.Data.FileContexts
{
    public class FileContextOptions
    {
        private string saveingDirectory;
        public string SaveingDirectory => saveingDirectory;

        public FileContextOptions UseSaveDir(string path)
        {
            saveingDirectory = path;
            return this;
        }
    }
}