using Microsoft.Extensions.Configuration;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Infra.Data.Exceptions;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.FileSavers
{
    public class FileSaver
    {
        public static Action<FileSaverOptions> Configuring;
        private static readonly FileSaverOptions configurationOptions = new();

        private readonly string saveDiractory;

        public FileSaver()
        {         
            OnConfiguring();
            saveDiractory = configurationOptions.SaveingDirectory;
        }

        protected virtual void OnConfiguring()
        {
            if (Configuring is null) throw new FileSaverConfigurationException();
            Configuring?.Invoke(configurationOptions);
            if (configurationOptions.SaveingDirectory is null) throw new ArgumentNullException();
            if (!Directory.Exists(configurationOptions.SaveingDirectory)) throw new SaveDiractoryPathException("Directory path doesn't exist.");
        }

        public bool Delete(string fileName)
        {
            var oldPath = Path.Combine(saveDiractory, fileName);
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
            var newPath = Path.Combine(saveDiractory, name);
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
