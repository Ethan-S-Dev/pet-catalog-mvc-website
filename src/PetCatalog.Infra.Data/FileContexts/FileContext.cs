using PetCatalog.Infra.Data.Exceptions;
using System;
using System.IO;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.Infra.Data.Interfaces;

namespace PetCatalog.Infra.Data.FileContexts
{
    public partial class FileContext : IFileContext
    {
        private readonly FileContextDir saveDiractory;
        private readonly string path;
        public FileContextDir Diractory => saveDiractory;
        public FileContext(FileContextOptions fileContextOptions)
        {
            if (fileContextOptions.SaveingDirectory is null) throw new ArgumentNullException();
            saveDiractory = new FileContextDir(fileContextOptions.SaveingDirectory);            
            saveDiractory.OnCreation += OnDirectoryCreation;
            path = saveDiractory.Path;
        }

        protected virtual void OnDirectoryCreation()
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
