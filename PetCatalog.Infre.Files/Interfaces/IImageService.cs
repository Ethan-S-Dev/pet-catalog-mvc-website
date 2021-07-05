using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infre.RootFiles.Interfaces
{
    public interface IImageFileHandler
    {
        Task<bool> SaveFileAsync(string path, byte[] fileData);

        Task<bool> DeleteFileAsync(string path);
    }
}
