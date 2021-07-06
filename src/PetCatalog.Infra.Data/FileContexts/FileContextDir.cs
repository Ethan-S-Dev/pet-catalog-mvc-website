using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Contexts
{
    public class FileContextDir
    {
        private readonly string path;
        public string Path => path;

        public event Action OnCreation;
        public event Action OnDeletion;

        public FileContextDir(string path)
        {           
            this.path = path;
        }

        public void EnsureDeleted()
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                OnDeletion?.Invoke();
            }      
        }
        public void EnsureCreated()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                OnCreation?.Invoke();
            }
        }
      
    }
}
