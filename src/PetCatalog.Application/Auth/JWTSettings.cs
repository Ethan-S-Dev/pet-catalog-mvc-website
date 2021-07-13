using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Auth
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public int JwtExpiresIn { get; set; }

        public int RefreshExpiresIn { get; set; }
    }
}
