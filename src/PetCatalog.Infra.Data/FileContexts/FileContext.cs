using PetCatalog.Infra.Data.Exceptions;
using System;
using System.IO;
using PetCatalog.Infra.Data.Contexts;

namespace PetCatalog.Infra.Data.FileContexts
{
    public partial class FileContext
    {
        private static bool isCreated;             
        private static FileContextDir saveDiractory;

        private readonly FileContextOptions options;
        private readonly string path;
        public FileContextDir Diractory => saveDiractory;
        public FileContext(FileContextOptions fileContextOptions)
        {
            options = fileContextOptions;
            if (!isCreated)
            {
                Configure(options);
                isCreated = true;
            }
            path = saveDiractory.Path;
        }

        private void Configure(FileContextOptions options)
        {
            if (options.SaveingDirectory is null) throw new ArgumentNullException();
            saveDiractory = new FileContextDir(options.SaveingDirectory);
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
