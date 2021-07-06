namespace PetCatalog.Infra.Data.FileSavers
{
    public class FileSaverOptions
    {
        private string saveingDirectory { get; set; }

        public FileSaverOptions UseSaveDir(string path)
        {
            saveingDirectory = path;
            return this;
        }
    }
}