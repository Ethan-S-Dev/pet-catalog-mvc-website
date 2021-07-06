using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Exceptions
{
    public class FileSaverConfigurationException : Exception
    {
        public FileSaverConfigurationException() : base("No configuration provided.")
        {

        }
    }
}
