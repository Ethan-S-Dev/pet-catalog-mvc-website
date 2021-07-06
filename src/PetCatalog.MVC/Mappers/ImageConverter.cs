using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Mappers
{
    public class ImageConverter : IValueConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile sourceMember, ResolutionContext context)
        {
            using var stream = new MemoryStream();
            sourceMember.CopyTo(stream);
            stream.Position = 0;
            return stream.ToArray();
        }
    }
}
