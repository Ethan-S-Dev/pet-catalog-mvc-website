using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface IImageService
    {
        bool SaveImage(string name, Stream data);

        bool UpdateImage(string oldName,string newName, Stream data);

        bool DeleteImage(string name);

        string ImageDir { get; }

        string DefaultName { get; }
    }
}
