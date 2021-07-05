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
            var oldPath = Path.Combine(ImageDir, name);
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

        public bool SaveImage(string name, Stream data)
        {
            var newPath = Path.Combine(ImageDir, name);
            try
            {
                using var file = File.Create(newPath);
                data.CopyTo(file);
            }
            catch
            {

            }
            
            return true;
        }

        public bool UpdateImage(string oldName, string newName, Stream data)
        {          
                if(SaveImage(newName, data))
                {
                    DeleteImage(oldName);
                    return true;
                }                    
                return false;

        }
    }
}
