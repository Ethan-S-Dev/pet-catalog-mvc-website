using Microsoft.Extensions.Configuration;
using PetCatalog.Infra.Data.FileContexts;
using System.IO;

namespace PetCatalog.Infra.Data.Contexts
{
    public class ImageFileContext : FileContext
    {
        private readonly string defaultImage;
        private readonly string defaultImagePath;
        public ImageFileContext(FileContextOptions options,IConfiguration configuration) : base(options)
        {
            defaultImage = configuration["DefaultImageName"];
            defaultImagePath = configuration["DefaultImagePath"];
        }
        protected override void OnDirectoryCreation()
        {
            var data = File.ReadAllBytes(defaultImagePath);
            Save(defaultImage, data);
        }
        protected override string GetDefaultFile()
        {
            return defaultImage;
        }
    }
}
