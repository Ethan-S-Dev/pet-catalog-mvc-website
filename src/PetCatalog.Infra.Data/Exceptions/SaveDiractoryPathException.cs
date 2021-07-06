using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Exceptions
{
    public class SaveDiractoryPathException : Exception
    {
        public SaveDiractoryPathException(string message = null):base(message)
        {

        }
    }
}
