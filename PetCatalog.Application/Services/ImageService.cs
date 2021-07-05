using PetCatalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string dirPath;
        private readonly string defaultName;
        public ImageService(string saveLocation,string defaultName)
        {
            dirPath = saveLocation;
            this.defaultName = defaultName;
        }

        public string ImageDir => dirPath;
        public string DefaultName => defaultName;

        public bool DeleteImage(string name)
        {
            throw new NotImplementedException();
        }

        public bool SaveImage(string name, Stream data)
        {
            throw new NotImplementedException();
        }

        public bool UpdateImage(string oldName, string newName, Stream data)
        {
            var oldPath = Path.Combine(ImageDir, oldName);
            var newPath = Path.Combine(ImageDir, newName);
            try
            {
                using var file = File.Create(newPath);
                data.CopyTo(file);
                File.Delete(oldPath);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
