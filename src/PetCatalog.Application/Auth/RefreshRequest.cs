using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Auth
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
