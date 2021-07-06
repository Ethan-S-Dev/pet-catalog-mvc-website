using PetCatalog.Infra.Data.FileContexts;
using System.IO;

namespace PetCatalog.Infra.Data.Contexts
{
    public class ImageFileContext : FileContext
    {
        protected override void OnCreation()
        {
            var data = File.ReadAllBytes("./res/default.png");
            Save("default.png", data);
        }
    }
}
