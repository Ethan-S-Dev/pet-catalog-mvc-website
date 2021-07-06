using PetCatalog.Infra.Data.Exceptions;
using System;
using System.IO;
using PetCatalog.Infra.Data.Contexts;

namespace PetCatalog.Infra.Data.FileContexts
{
    public partial class FileContext
    {
        public static Action<FileContextOptions> Configuring;
        private static readonly FileContextOptions configurationOptions = new();
        private static FileContextDir saveDiractory;
        private static bool Created;             
        public FileContextDir Diractory => saveDiractory;
        private readonly string path;
        public FileContext()
        {    
            if(!Created)
            {
                Configure();
                Created = true;
            }
            path = saveDiractory.Path;
        }

        private void Configure()
        {
            if (Configuring is null) throw new FileSaverConfigurationException();
            Configuring?.Invoke(configurationOptions);
            if (configurationOptions.SaveingDirectory is null) throw new ArgumentNullException();
            saveDiractory = new FileContextDir(configurationOptions.SaveingDirectory);
            saveDiractory.OnCreation += OnCreation;
        }

        protected virtual void OnCreation()
        {
            
        }

        public bool Delete(string fileName)
        {
            var oldPath = Path.Combine(path, fileName);
            try
            {
                File.Delete(oldPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save(string name, byte[] data)
        {
            var newPath = Path.Combine(path, name);
            try
            {
                File.WriteAllBytes(newPath, data);
            }
            catch
            {

            }

            return true;
        }

        public bool Update(string oldName, string newName, byte[] data)
        {
            if (Save(newName, data))
            {
                Delete(oldName);
                return true;
            }
            return false;

        }
       
    }
}
