using PetCatalog.Infre.RootFiles.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infre.RootFiles
{
    public class ImageFileHandler : IImageFileHandler
    {
        private readonly string rootPath;
        private readonly string folderPath;
        public ImageFileHandler(string rootPath,string imagesFolderPath)
        {
            this.rootPath = rootPath;
            this.folderPath = imagesFolderPath;
        }
        public async Task<bool> DeleteFileAsync(string name)
        {
            
        }

        public async Task<bool> SaveFileAsync(string name, byte[] fileData)
        {
            
        }
    }
}
