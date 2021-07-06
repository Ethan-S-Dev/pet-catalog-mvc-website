namespace PetCatalog.Infra.Data.FileSavers
{
    public class FileSaverOptions
    {
        private string saveingDirectory;
        public string SaveingDirectory => saveingDirectory;

        public FileSaverOptions UseSaveDir(string path)
        {
            saveingDirectory = path;
            return this;
        }
    }
}