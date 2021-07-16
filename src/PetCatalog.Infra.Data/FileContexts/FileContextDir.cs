using System;
using System.IO;

namespace PetCatalog.Infra.Data.Contexts
{
    public class FileContextDir
    {
        private readonly string path;
        public string Path => path;
        public bool IsCreated => Directory.Exists(path);

        public Action OnCreation;
        public Action OnDeletion;

        public FileContextDir(string path)
        {
            this.path = path;
        }

        public void EnsureDeleted()
        {
            if (IsCreated)
            {
                Directory.Delete(path, true);
                OnDeletion?.Invoke();
            }
        }

        public void EnsureCreated()
        {
            if (!IsCreated)
            {
                Directory.CreateDirectory(path);
                OnCreation?.Invoke();
            }
        }
    }
}

